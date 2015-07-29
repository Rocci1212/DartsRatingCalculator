using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DartsRatingCalculator.Utility
{
    // the purpose of this class is to take a webpage and rip information from them
    // there are 3 types of pages which can be farmed - a standings page, which will
    // be manually fed in, a team/squad page, which will be farmed from the standings page
    // and the match table, which will be farmed from the squad pages
    public static class FarmingFunctions
    {
        public static void FarmStandingsPage(string url)
        {
            WebRequest webRequest = WebRequest.Create(url);
            WebResponse webResponse = webRequest.GetResponse();
            Stream responseStream = webResponse.GetResponseStream();
            StreamReader rReaader = new StreamReader(responseStream);
            string sWebpageText = rReaader.ReadToEnd();

            FarmCampaigns(sWebpageText);
        }

        public static void FarmCampaigns(string webpageText)
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(webpageText);

            var campaignText = 
                doc.DocumentNode.SelectNodes("//h3")[2].InnerText.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries)[1];

            var campaignHeaders = doc.DocumentNode.SelectNodes("//h4");
            var standingsTables = doc.DocumentNode.SelectNodes("//table[@id='standings_table']");

            if (campaignHeaders.Count != standingsTables.Count)
                throw (new InvalidOperationException());

            for (int i = 0; i < standingsTables.Count; i++)
            {
                Campaign campaignToCreate = new Campaign();

                campaignToCreate._Season = (Season)Enum.Parse(typeof(Season), campaignText.Split(' ')[0]);
                campaignToCreate.Year = Convert.ToInt32(campaignText.Split(' ')[1]);

                var campaignText2 = campaignHeaders[i].InnerHtml.ToString();
                string conferenceText = campaignText2.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];
                string classText = campaignText2.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[2];

                // get the conference
                switch (conferenceText)
                {
                    case "Bos":
                        campaignToCreate._Conference = Conference.Boston;
                        break;
                    case "Cent":
                        campaignToCreate._Conference = Conference.Central;
                        break;
                    case "NS":
                        campaignToCreate._Conference = Conference.NorthShore;
                        break;
                    case "SS":
                        campaignToCreate._Conference = Conference.SouthShore;
                        break;
                }


                // get the class, identifier
                if (classText.Substring(0, 2) == "SA")
                    campaignToCreate._Class = Class.SuperA;
                else
                {
                    campaignToCreate._Class = (Class)Enum.Parse(typeof(Class), classText.Substring(0, 1));
                    campaignToCreate.Identifier = Convert.ToInt32(classText.Substring(1, 1));
                }

                int j = Campaign.GetNewOrExistingCampaignId(ref campaignToCreate);
                FarmTeams(standingsTables[i].OuterHtml, j);
            }
        }

        public static void FarmTeams(string webpageText, int campaignId)
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(webpageText);

            var x = doc.DocumentNode.SelectNodes("//a");

            foreach (var y in x)
            {
                var z = y.Attributes["href"].Value;
                var thereShouldBeSomethingAfterZ = Convert.ToInt32(z.Substring(z.LastIndexOf('=') + 1));

                Squad.InsertNewSquad(thereShouldBeSomethingAfterZ, campaignId);
            }
        }

        public static void FarmTeamPage(int teamId)
        {
            string url = "http://stats.mmdl.org/index.php?view=team&teamid=" + teamId.ToString();
            WebRequest webRequest = WebRequest.Create(url);
            WebResponse webResponse = webRequest.GetResponse();
            Stream responseStream = webResponse.GetResponseStream();
            StreamReader rReaader = new StreamReader(responseStream);
            string sWebpageText = rReaader.ReadToEnd();

            FarmSquadInfo(sWebpageText, teamId);
        }

        public static void FarmSquadInfo(string webpageText, int teamId)
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(webpageText);

            webpageText = doc.DocumentNode.SelectSingleNode("//div[@id='main_content']").InnerHtml;
            doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(webpageText);

            var teamName = doc.DocumentNode.SelectSingleNode("//h3").InnerHtml;
            var teamInfo = doc.DocumentNode.SelectNodes("//p")[1].InnerHtml;

            var sponsor = teamInfo.Substring(0, teamInfo.LastIndexOf(','));
            var location = teamInfo.Substring(teamInfo.LastIndexOf(',') + 1).Trim();

            Squad.UpdateSquadInfo(teamId, teamName, sponsor, location);

            var playerDoc = new HtmlAgilityPack.HtmlDocument();
            playerDoc.LoadHtml(doc.DocumentNode.SelectSingleNode("//div").OuterHtml);

            foreach (var y in playerDoc.DocumentNode.SelectNodes("//a"))
            {
                var z = y.Attributes["href"].Value;
                var thereShouldBeSomethingAfterZ = Convert.ToInt32(z.Substring(z.LastIndexOf('=') + 1));
                var a = y.InnerHtml;

                DartsPlayer.InsertNewPlayer(thereShouldBeSomethingAfterZ, a, teamId);
                //Squad.InsertNewSquad(thereShouldBeSomethingAfterZ, campaignId);
            }

            var teamMatchDoc = new HtmlAgilityPack.HtmlDocument();
            teamMatchDoc.LoadHtml(doc.DocumentNode.SelectSingleNode("//table[@id='team_match_table']").OuterHtml);

            foreach (var m in teamMatchDoc.DocumentNode.SelectNodes("//a"))
            {
                var z = m.Attributes["href"].Value;
                var thereShouldBeSomethingAfterZ = Convert.ToInt32(z.Substring(z.LastIndexOf('=') + 1));
                var a = m.InnerHtml;

                Match.InsertMatchHeader(thereShouldBeSomethingAfterZ, a, teamId);
            }
        }

        public static void FarmMatchPage(string url)
        {
            // get the webpage
            WebRequest webRequest = WebRequest.Create("http://stats.mmdl.org/index.php?view=match&matchid=36527");
            WebResponse webResponse = webRequest.GetResponse();
            Stream responseStream = webResponse.GetResponseStream();
            StreamReader rReader = new StreamReader(responseStream);
            string sWebpageText = rReader.ReadToEnd();

            // make sure that the webpage contains the match table
            if (!sWebpageText.Contains(@"<table id=""match_table"">"))
                throw new InvalidOperationException();

            // html agility pack to parse
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(sWebpageText);

            // get the match number
            var h3 = doc.DocumentNode.SelectNodes("//h3");
            var matchDesc = h3[2].InnerHtml;

            // get the squads competing
            var h4 = doc.DocumentNode.SelectNodes("//h4");
            var squadDesc = h4[0].InnerHtml;

            // i am such a fucking hack
            var em = doc.DocumentNode.SelectNodes("//em");
            var campaignDesc = em[0].InnerHtml;

            Match y = new Match(matchDesc, squadDesc, campaignDesc);

            // get the match table html
            var docMatchTable = doc.DocumentNode.SelectSingleNode("//table[@id='match_table']");
            doc.LoadHtml(docMatchTable.OuterHtml);

            var tableRows = docMatchTable.SelectNodes("//tr");
            //int i = 0;
            for (int i = 0; i < tableRows.ToList<HtmlAgilityPack.HtmlNode>().Count; i++)
            {
                if (tableRows[i].FirstChild.Name == "th")
                {
                    string sGameType = tableRows[i].InnerText;
                }
                else
                {
                    var x = tableRows[i].ChildNodes;
                    List<string> rowValues = new List<string>();
                    for (int j = 0; j < x.ToList<HtmlAgilityPack.HtmlNode>().Count; j++)
                    {

                        if (tableRows[i].ChildNodes[j].Name == "td")
                        {
                            rowValues.Add(tableRows[i].ChildNodes[j].InnerText);
                        }
                    }
                }
            }
        }
    }
}
