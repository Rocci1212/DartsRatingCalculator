using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public static ArrayList arrBadMatches = new ArrayList();

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
            int id;
            Season season;
            int year;
            Class _class;
            Conference conference = new Conference();
            int? identifier = null;

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
                season = (Season)Enum.Parse(typeof(Season), campaignText.Split(' ')[0]);
                year = Convert.ToInt32(campaignText.Split(' ')[1]);

                var campaignText2 = campaignHeaders[i].InnerHtml.ToString();
                string conferenceText;
                string classText;

                if (campaignText2 == "Division kikoro") // lol wtf
                    break;

                if (campaignText2 == "Division Boston") { conferenceText = "Bos"; classText = "SA"; }
                else if (campaignText2 == "Division Central") { conferenceText = "Cent"; classText = "SA"; }
                else if (campaignText2 == "Division North Shore") { conferenceText = "NS"; classText = "SA"; }
                else if (campaignText2 == "Division South Shore") { conferenceText = "SS"; classText = "SA"; }
                else
                {
                    conferenceText = campaignText2.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];
                    classText = campaignText2.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[2];
                }

                // get the conference
                switch (conferenceText.ToUpper())
                {
                    case "BOS":
                    case "BOSTON":
                        conference = Conference.Boston;
                        break;
                    case "CENT":
                    case "CENT.":
                    case "CENTRAL":
                        conference = Conference.Central;
                        break;
                    case "NORTH":
                        if (classText.ToUpper() == "SHORE")
                            classText = campaignText2.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[3];
                        conference = Conference.NorthShore;
                        break;
                    case "NS":
                        conference = Conference.NorthShore;
                        break;
                    case "SOUTH":
                        if (classText.ToUpper() == "SHORE")
                            classText = campaignText2.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[3];
                        conference = Conference.SouthShore;
                        break;
                    case "SS":
                        conference = Conference.SouthShore;
                        break;
                    default:
                        throw new Exception("Invalid Conference");
                        break;
                }


                // get the class, identifier
                if (classText.Length == 1)
                    _class = (Class)Enum.Parse(typeof(Class), classText);
                else if (classText.Substring(0, 2) == "SA" || classText.Substring(0, 2) == "Su")
                    _class = Class.SuperA;
                else
                {
                    _class = (Class)Enum.Parse(typeof(Class), classText.Substring(0, 1));
                    identifier = Convert.ToInt32(classText.Substring(1, 1));
                }

                id = Campaign.CommitCampaign(season, year, _class, conference, identifier);
                FarmSquad(standingsTables[i].OuterHtml, id);
            }
        }

        public static void FarmSquad(string webpageText, int campaignId)
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(webpageText);

            var squadLinks = doc.DocumentNode.SelectNodes("//a");

            foreach (var link in squadLinks)
            {
                var squadPage = link.Attributes["href"].Value;
                var squadId = Convert.ToInt32(squadPage.Substring(squadPage.LastIndexOf('=') + 1));

                Squad.CommitSquad(squadId, campaignId);
            }
        }

        public static void FarmTeamPage(int teamId)
        {
            string url = "http://stats.mmdl.org/index.php?view=team&teamid=" + teamId.ToString();
            WebRequest webRequest = WebRequest.Create(url);
            WebResponse webResponse = webRequest.GetResponse();
            Stream responseStream = webResponse.GetResponseStream();
            StreamReader rReader = new StreamReader(responseStream);
            string sWebpageText = rReader.ReadToEnd();

            FarmSquadInfo(sWebpageText, teamId);
        }

        public static void FarmSquadInfo(string webpageText, int teamId)
        {
            // manually add players for teamid 4034
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(webpageText);

            webpageText = doc.DocumentNode.SelectSingleNode("//div[@id='main_content']").InnerHtml;
            doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(webpageText);

            var teamName = doc.DocumentNode.SelectSingleNode("//h3").InnerHtml;
            var teamInfo = doc.DocumentNode.SelectNodes("//p")[1].InnerHtml;

            var sponsor = teamInfo.Substring(0, teamInfo.LastIndexOf(','));
            var city = teamInfo.Substring(teamInfo.LastIndexOf(',') + 1).Trim();

            Squad.CommitSquadDetails(teamId, teamName, sponsor, city);

            var playerDoc = new HtmlAgilityPack.HtmlDocument();
            playerDoc.LoadHtml(doc.DocumentNode.SelectSingleNode("//div").OuterHtml);

            foreach (var playerLink in playerDoc.DocumentNode.SelectNodes("//a"))
            {
                var z = playerLink.Attributes["href"].Value;
                var playerId = Convert.ToInt32(z.Substring(z.LastIndexOf('=') + 1));
                var playerName = playerLink.InnerHtml;

                DartsPlayer.InsertNewPlayer(playerId, playerName, teamId);
                //Squad.InsertNewSquad(thereShouldBeSomethingAfterZ, campaignId);
            }

            var teamMatchDoc = new HtmlAgilityPack.HtmlDocument();
            teamMatchDoc.LoadHtml(doc.DocumentNode.SelectSingleNode("//table[@id='team_match_table']").OuterHtml);

            foreach (var m in teamMatchDoc.DocumentNode.SelectNodes("//a"))
            {
                var matchLink = m.Attributes["href"].Value;
                var matchId = Convert.ToInt32(matchLink.Substring(matchLink.LastIndexOf('=') + 1));
                var weekNumber = m.InnerHtml;

                Match.InsertMatchHeader(matchId, weekNumber, teamId);
            }
        }
        

        public static void FarmMatchPage(int matchId, int campaignId)
        {
            // get the webpage
            WebRequest webRequest = WebRequest.Create("http://stats.mmdl.org/index.php?view=match&matchid=" + matchId.ToString());
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
            var h3nodes = doc.DocumentNode.SelectNodes("//h3");
            var matchDesc = h3nodes[2].InnerHtml; //

            // get the squads competing
            var h4nodes = doc.DocumentNode.SelectNodes("//h4");
            var squadDesc = h4nodes[0].InnerHtml; //

            // get the match table html
            var docMatchTable = doc.DocumentNode.SelectSingleNode("//table[@id='match_table']");
            doc.LoadHtml(docMatchTable.OuterHtml);

            var tableRows = docMatchTable.SelectNodes("//tr");

            int GameNumber = 0;
            string[] GameType = {"", ""};
            decimal IsHomeWin = 0;
            ArrayList HomeDartsPlayers, AwayDartsPlayers; HomeDartsPlayers = new ArrayList(); AwayDartsPlayers = new ArrayList();
            string AwayPlayersConcat = ""; string HomePlayersConcat = "";

            for (int i = 0; i < tableRows.ToList<HtmlAgilityPack.HtmlNode>().Count; i++)
            {
                // this is the last game.
                if (tableRows[i].FirstChild.Name == "th" && tableRows[i].InnerText == "Totals")
                {
                    // insert match into table
                    AwayPlayersConcat = ""; HomePlayersConcat = "";

                    foreach (string s in AwayDartsPlayers)
                    {
                        AwayPlayersConcat += (s + "|~|");
                    }

                    foreach (string s in HomeDartsPlayers)
                    {
                        HomePlayersConcat += (s + "|~|");
                    }

                    SqlConnection connSql = new SqlConnection(
                        Gravoc.Encryption.Encryption.Decrypt(Properties.Settings.Default.ConnectionString));
                    connSql.Open();

                    SqlCommand cmdSql = new SqlCommand("insert into GameFarm values (@m, @s, @num, @t, @homeWin, @ap, @hp)", connSql);
                    cmdSql.CommandType = System.Data.CommandType.Text;
                    cmdSql.Parameters.AddWithValue("@m", matchDesc);
                    cmdSql.Parameters.AddWithValue("@s", squadDesc);
                    cmdSql.Parameters.AddWithValue("@num", GameNumber);
                    cmdSql.Parameters.AddWithValue("@t", GameType[1]);
                    cmdSql.Parameters.AddWithValue("@homeWin", IsHomeWin);
                    cmdSql.Parameters.AddWithValue("@ap", AwayPlayersConcat);
                    cmdSql.Parameters.AddWithValue("@hp", HomePlayersConcat);
                    cmdSql.ExecuteNonQuery();

                    connSql.Close();

                    break;
                }
                else if (tableRows[i].FirstChild.Name == "th")
                {
                    // set the game type
                    GameType[0] = tableRows[i].InnerText;
                    GameType[1] = GameType[1] == "" ? GameType[0] : GameType[1];
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
                    switch (rowValues.Count)
                    {
                        case 2:
                            if (rowValues[0] != "&nbsp;" && rowValues[0].Trim() != "")
                            {
                                AwayDartsPlayers.Add(rowValues[0].Trim());
                            }
                            if (rowValues[1] != "&nbsp;" && rowValues[1].Trim() != "")
                            {
                                HomeDartsPlayers.Add(rowValues[1].Trim());
                            }
                            break;
                        case 5:
                            if (GameNumber != 0)
                            {
                                // insert match into table
                                AwayPlayersConcat = ""; HomePlayersConcat = "";
                                foreach (string s in AwayDartsPlayers)
                                {
                                    AwayPlayersConcat += (s + "|~|");
                                }

                                foreach (string s in HomeDartsPlayers)
                                {
                                    HomePlayersConcat += (s + "|~|");
                                }

                                SqlConnection connSql = new SqlConnection(
                                    Gravoc.Encryption.Encryption.Decrypt(Properties.Settings.Default.ConnectionString));
                                connSql.Open();

                                SqlCommand cmdSql = new SqlCommand("insert into GameFarm values (@m, @s, @num, @t, @homeWin, @ap, @hp)", connSql);
                                cmdSql.CommandType = System.Data.CommandType.Text;
                                cmdSql.Parameters.AddWithValue("@m", matchDesc);
                                cmdSql.Parameters.AddWithValue("@s", squadDesc);
                                cmdSql.Parameters.AddWithValue("@num", GameNumber);
                                cmdSql.Parameters.AddWithValue("@t", GameType[1]);
                                cmdSql.Parameters.AddWithValue("@homeWin", IsHomeWin);
                                cmdSql.Parameters.AddWithValue("@ap", AwayPlayersConcat);
                                cmdSql.Parameters.AddWithValue("@hp", HomePlayersConcat);
                                cmdSql.ExecuteNonQuery();

                                connSql.Close();
                            }

                            AwayDartsPlayers = new ArrayList(); HomeDartsPlayers = new ArrayList();

                            GameNumber++;
                            GameType[1] = GameType[0];

                            if (rowValues[1] != "&nbsp;" && rowValues[1].Trim() != "")
                            {
                                AwayDartsPlayers.Add(rowValues[1].Trim());
                            }
                            IsHomeWin = Math.Abs(1 - Convert.ToDecimal(rowValues[2]));
                            if (rowValues[3] != "&nbsp;" && rowValues[3].Trim() != "")
                            {
                                HomeDartsPlayers.Add(rowValues[3].Trim());
                            }
                            break;
                        default:
                            throw (new InvalidOperationException("weird table structure man"));
                    }
                }
            }
        }

        public static void FarmGame(string matchDescription, string squadDescription, int gameNumber, 
            string _gameType, int _isHomeWin, string awayPlayers, string homePlayers)
        {
            int matchID = Convert.ToInt32(matchDescription.Replace("Match #", ""));

            if (arrBadMatches.Contains(matchID))
                return;

            Campaign campaign = Campaign.GetCampaignFromMatch(matchID);
            Match match = new Match(matchDescription, squadDescription, campaign.ID);
            Squad AwaySquad = new Squad(); Squad HomeSquad = new Squad();
            Squad.GetSquadsFromDesc(squadDescription, campaign, ref AwaySquad, ref HomeSquad);

            Game game = new Game();
            game._GameType = Game.GetGameTypeFromString(_gameType);
            game.IsHomeWin = _isHomeWin == 1;
            game.GameNumber = gameNumber;

            SqlConnection connSql = new SqlConnection(
                        Gravoc.Encryption.Encryption.Decrypt(Properties.Settings.Default.ConnectionString));
            connSql.Open();

            SqlCommand cmdSql = new SqlCommand("update match set awaySquad = @a, homeSquad = @h where id = @m", connSql);
            cmdSql.Parameters.AddWithValue("@m", matchID);
            cmdSql.Parameters.AddWithValue("@a", AwaySquad.SquadId);
            cmdSql.Parameters.AddWithValue("@h", HomeSquad.SquadId);

            cmdSql.ExecuteNonQuery();
            connSql.Close();
            
            awayPlayers.Split(new string[] {"|~|"}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string playerName in awayPlayers.Split(new string[] { "|~|" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (!AwaySquad.DartsPlayers.ContainsKey(playerName))
                {
                    int n = DartsPlayer.CreateDummyPlayer(playerName, AwaySquad.SquadId, match.MatchId);
                    AwaySquad.DartsPlayers.Add(DartsPlayer.GetPlayer(n).Name, DartsPlayer.GetPlayer(n));
                }
                if (!game.AwayDartsPlayers.Contains(AwaySquad.DartsPlayers[playerName]))
                    game.AwayDartsPlayers.Add(AwaySquad.DartsPlayers[playerName]);
                else
                {
                    arrBadMatches.Add(match.MatchId);
                    match.MarkBadMatch();
                    return;
                }
            }

            foreach (string playerName in homePlayers.Split(new string[] { "|~|" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (!HomeSquad.DartsPlayers.ContainsKey(playerName))
                {
                    int n = DartsPlayer.CreateDummyPlayer(playerName, HomeSquad.SquadId, match.MatchId);
                    HomeSquad.DartsPlayers.Add(DartsPlayer.GetPlayer(n).Name, DartsPlayer.GetPlayer(n));
                }
                if (!game.HomeDartsPlayers.Contains(HomeSquad.DartsPlayers[playerName]))
                    game.HomeDartsPlayers.Add(HomeSquad.DartsPlayers[playerName]);
                else
                {
                    arrBadMatches.Add(match.MatchId);
                    match.MarkBadMatch();
                    return;
                }
            }

            game.CalculateAndCommitGame(match);
        }

        // this is old and not used anymore
        public static void FarmMatchPage1(int matchId, int campaignId)
        {
            // get the webpage
            WebRequest webRequest = WebRequest.Create("http://stats.mmdl.org/index.php?view=match&matchid=" + matchId.ToString());
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
            var h3nodes = doc.DocumentNode.SelectNodes("//h3");
            var matchDesc = h3nodes[2].InnerHtml;

            // get the squads competing
            var h4nodes = doc.DocumentNode.SelectNodes("//h4");
            var squadDesc = h4nodes[0].InnerHtml;

            Match match = new Match(matchDesc, squadDesc, campaignId);

            // get the match table html
            var docMatchTable = doc.DocumentNode.SelectSingleNode("//table[@id='match_table']");
            doc.LoadHtml(docMatchTable.OuterHtml);

            var tableRows = docMatchTable.SelectNodes("//tr");

            Game game = new Game();
            GameType gameType = new GameType();
            int gameNumber = 1;
            bool isHomeWin;
            DartsPlayer awayPlayer, homePlayer;
#if FALSE
            ValidateMatchTable(tableRows);
#endif
            for (int i = 0; i < tableRows.ToList<HtmlAgilityPack.HtmlNode>().Count; i++)
            {
                if (tableRows[i].FirstChild.Name == "th" && tableRows[i].InnerText == "Totals")
                {
                    game.CalculateAndCommitGame(match);
                    break;
                }
                else if (tableRows[i].FirstChild.Name == "th")
                {
                    gameType = Game.GetGameTypeFromString(tableRows[i].InnerText);
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
                    //match.AwaySquad.DartsPlayers.Add("Paul Cedrone", DartsPlayer.GetPlayer(10408));
                    switch (rowValues.Count)
                    {
                        case 2:
                            if (rowValues[0] != "&nbsp;" && rowValues[0].Trim() != "")
                            {
                                if (!match.AwaySquad.DartsPlayers.ContainsKey(rowValues[0].ToString()))
                                {
                                    int n = DartsPlayer.CreateDummyPlayer(rowValues[0].ToString(), match.AwaySquad.SquadId, match.MatchId);
                                    match.AwaySquad.DartsPlayers.Add(DartsPlayer.GetPlayer(n).Name, DartsPlayer.GetPlayer(n));
                                }
                                if (!game.AwayDartsPlayers.Contains(match.AwaySquad.DartsPlayers[rowValues[0].ToString()]))
                                    game.AwayDartsPlayers.Add(match.AwaySquad.DartsPlayers[rowValues[0].ToString()]);
                            }
                            if (rowValues[1] != "&nbsp;" && rowValues[1].Trim() != "")
                            {
                                if (!match.HomeSquad.DartsPlayers.ContainsKey(rowValues[1].ToString()))
                                {
                                    int n = DartsPlayer.CreateDummyPlayer(rowValues[1].ToString(), match.HomeSquad.SquadId, match.MatchId);
                                    match.HomeSquad.DartsPlayers.Add(DartsPlayer.GetPlayer(n).Name, DartsPlayer.GetPlayer(n));
                                }
                                if (!game.HomeDartsPlayers.Contains(match.HomeSquad.DartsPlayers[rowValues[1].ToString()]))
                                game.HomeDartsPlayers.Add(match.HomeSquad.DartsPlayers[rowValues[1].ToString()]);
                            }
                            break;
                        case 5:
                            if (game.GameNumber != 0)
                            {
                                game.CalculateAndCommitGame(match); // john - 
                            }

                            game = new Game();

                            game._GameType = gameType;
                            game.GameNumber = gameNumber; gameNumber++;
                            if (rowValues[1] != "&nbsp;" && rowValues[1].Trim() != "")
                            {
                                if (!match.AwaySquad.DartsPlayers.ContainsKey(rowValues[1].ToString()))
                                {
                                    int n = DartsPlayer.CreateDummyPlayer(rowValues[1].ToString(), match.AwaySquad.SquadId, match.MatchId);
                                    match.AwaySquad.DartsPlayers.Add(DartsPlayer.GetPlayer(n).Name, DartsPlayer.GetPlayer(n));
                                }
                                if (!game.AwayDartsPlayers.Contains(match.AwaySquad.DartsPlayers[rowValues[1].ToString()]))
                                    game.AwayDartsPlayers.Add(match.AwaySquad.DartsPlayers[rowValues[1].ToString()]);
                            }
                            game.IsHomeWin = (0 == Convert.ToInt32(rowValues[2]));
                            if (rowValues[3] != "&nbsp;" && rowValues[3].Trim() != "")
                            {
                                if (!match.HomeSquad.DartsPlayers.ContainsKey(rowValues[3].ToString()))
                                {
                                    int n = DartsPlayer.CreateDummyPlayer(rowValues[3].ToString(), match.HomeSquad.SquadId, match.MatchId);
                                    match.HomeSquad.DartsPlayers.Add(DartsPlayer.GetPlayer(n).Name, DartsPlayer.GetPlayer(n));
                                }
                                if (!game.HomeDartsPlayers.Contains(match.HomeSquad.DartsPlayers[rowValues[3].ToString()]))
                                    game.HomeDartsPlayers.Add(match.HomeSquad.DartsPlayers[rowValues[3].ToString()]);
                            }
                            //Game.InsertNew
                            break;
                        default:
                            throw (new InvalidOperationException("weird table structure man"));
                    }
                }
            }
        }

#if FALSE
        private static void ValidateMatchTable(HtmlAgilityPack.HtmlNodeCollection tableRows)
        {
            for (int i = 0; i < tableRows.ToList<HtmlAgilityPack.HtmlNode>().Count; i++)
            {
                if (tableRows[i].FirstChild.Name == "th" && tableRows[i].InnerText == "Totals")
                {
                    game.CalculateAndCommitGame(match);
                    break;
                }
                else if (tableRows[i].FirstChild.Name == "th")
                {
                    gameType = Game.GetGameTypeFromString(tableRows[i].InnerText);
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
                    //match.AwaySquad.DartsPlayers.Add("Paul Cedrone", DartsPlayer.GetPlayer(10408));
                    switch (rowValues.Count)
                    {
                        case 2:
                            if (rowValues[0] != "&nbsp;" && rowValues[0].Trim() != "")
                            {
                                if (!match.AwaySquad.DartsPlayers.ContainsKey(rowValues[0].ToString()))
                                {
                                    int n = DartsPlayer.CreateDummyPlayer(rowValues[0].ToString(), match.AwaySquad.SquadId);
                                    match.AwaySquad.DartsPlayers.Add(DartsPlayer.GetPlayer(n).Name, DartsPlayer.GetPlayer(n));
                                }
                                if (!game.AwayDartsPlayers.Contains(match.AwaySquad.DartsPlayers[rowValues[0].ToString()]))
                                    game.AwayDartsPlayers.Add(match.AwaySquad.DartsPlayers[rowValues[0].ToString()]);
                            }
                            if (rowValues[1] != "&nbsp;" && rowValues[1].Trim() != "")
                            {
                                if (!match.HomeSquad.DartsPlayers.ContainsKey(rowValues[1].ToString()))
                                {
                                    int n = DartsPlayer.CreateDummyPlayer(rowValues[1].ToString(), match.HomeSquad.SquadId);
                                    match.HomeSquad.DartsPlayers.Add(DartsPlayer.GetPlayer(n).Name, DartsPlayer.GetPlayer(n));
                                }
                                if (!game.HomeDartsPlayers.Contains(match.HomeSquad.DartsPlayers[rowValues[1].ToString()]))
                                    game.HomeDartsPlayers.Add(match.HomeSquad.DartsPlayers[rowValues[1].ToString()]);
                            }
                            break;
                        case 5:
                            if (game.GameNumber != 0)
                            {
                                game.CalculateAndCommitGame(match); // john - 
                            }

                            game = new Game();

                            game._GameType = gameType;
                            game.GameNumber = gameNumber; gameNumber++;
                            if (rowValues[1] != "&nbsp;" && rowValues[1].Trim() != "")
                            {
                                if (!match.AwaySquad.DartsPlayers.ContainsKey(rowValues[1].ToString()))
                                {
                                    int n = DartsPlayer.CreateDummyPlayer(rowValues[1].ToString(), match.AwaySquad.SquadId);
                                    match.AwaySquad.DartsPlayers.Add(DartsPlayer.GetPlayer(n).Name, DartsPlayer.GetPlayer(n));
                                }
                                if (!game.AwayDartsPlayers.Contains(match.AwaySquad.DartsPlayers[rowValues[1].ToString()]))
                                    game.AwayDartsPlayers.Add(match.AwaySquad.DartsPlayers[rowValues[1].ToString()]);
                            }
                            game.IsHomeWin = (0 == Convert.ToInt32(rowValues[2]));
                            if (rowValues[3] != "&nbsp;" && rowValues[3].Trim() != "")
                            {
                                if (!match.HomeSquad.DartsPlayers.ContainsKey(rowValues[3].ToString()))
                                {
                                    int n = DartsPlayer.CreateDummyPlayer(rowValues[3].ToString(), match.HomeSquad.SquadId);
                                    match.HomeSquad.DartsPlayers.Add(DartsPlayer.GetPlayer(n).Name, DartsPlayer.GetPlayer(n));
                                }
                                if (!game.HomeDartsPlayers.Contains(match.HomeSquad.DartsPlayers[rowValues[3].ToString()]))
                                    game.HomeDartsPlayers.Add(match.HomeSquad.DartsPlayers[rowValues[3].ToString()]);
                            }
                            //Game.InsertNew
                            break;
                        default:
                            throw (new InvalidOperationException("weird table structure man"));
                    }
                }
            }
        }
#endif
    }
}
