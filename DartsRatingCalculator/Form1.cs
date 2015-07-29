using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Moserware.Skills;
using Moserware.Skills.TrueSkill;
using System.Data.SqlClient;

// legacy code
namespace DartsRatingCalculator
{
    // TODO Extend Rating to have a display rating: 25 = 5000, representing 50% chance of beating a new player

    public partial class Form1 : Form
    {
        SqlConnection connSql = new SqlConnection(Properties.Settings.Default.ConnectionString);

        public Form1()
        {
            InitializeComponent();
            connSql.Open();
        }

        public double CalculateWinProbability(Squad team1, Squad team2)
        {
            /*
                The probability that P1 defeats P2 is defined as
             * CDF (with 0 as mean and 1 as std deviation) of ((P_mu1 - P_mu2) / (root(2) * sum(P_sigmas)))
             * */
            throw new NotImplementedException();
        }

        private void sample(object sender, EventArgs e)
        {
            // Here's the most simple case: you have two players and one wins 
            // against the other.

            // Let's new up two players. Note that the argument passed into to Player
            // can be anything. This allows you to wrap any object. Here I'm just 
            // using a simple integer to represent the player, but you could just as
            // easily pass in a database entity representing a person/user or any
            // other custom class you have.

            var player1 = new Player(1);
            var player2 = new Player(2);
            var player3 = new Player(3);
            var player4 = new Player(4);
            var player5 = new Player(5);
            var player6 = new Player(6);
            var player7 = new Player(7);
            var player8 = new Player(8);

            // The algorithm has several parameters that can be tweaked that are
            // found in the "GameInfo" class. If you're just starting out, simply
            // use the defaults:
            var gameInfo = GameInfo.DefaultGameInfo;
            //gameInfo.DynamicsFactor

            Rating y = new Rating(30.887, 1);

            // A "Team" is a collection of "Player" objects. Here we have a team
            // that consists of single players.

            // Note that for each player on the team, we indicate that they have
            // the "DefaultRating" which means that the algorithm has never seen
            // them before. In a real implementation, you'd pull this previous
            // rating for the player based on the player.Id value. It could come
            // from a database.
            var team1 = new Team(player1, y);
            var team2 = new Team(player2, y);
            var team3 = new Team(player3, y);
            var team4 = new Team(player4, y);
            var team5 = new Team(player5, y);
            var team6 = new Team(player6, y);
            var team7 = new Team(player7, y);
            var team8 = new Team(player8, y);

            // We bundle up all of our teams together so that we can feed them to
            // the algorithm.
            var teams = Teams.Concat(team1, team2, team3, team4, team5, team6, team7, team8);

            // Before we know the actual results of the game, we can ask the 
            // calculator for what it perceives as the quality of the match (higher
            // means more fair/equitable)
            //AssertMatchQuality(0.447, TrueSkillCalculator.CalculateMatchQuality(gameInfo, teams));

            // This is the key line. We ask the calculator to calculate new ratings
            // Pay careful attention to the numbers at the end. This indicates that
            // team1 came in first place and team2 came in second place. TrueSkill
            // is flexible and allows scenarios such as team1 and team2 drawing which
            // could be represented as "1,1" since they both came in first place.
            var newRatings = TrueSkillCalculator.CalculateNewRatings(gameInfo, teams, 1, 2, 3, 4, 5, 6, 7, 8);

            var x = TrueSkillCalculator.CalculateMatchQuality(gameInfo, teams);

            // The result of the calculation is a dictionary mapping the players to
            // their new rating. Here we get the ratings out for each player
            var player1NewRating = newRatings[player1];
            var player2NewRating = newRatings[player2];
            var player3NewRating = newRatings[player3];
            var player4NewRating = newRatings[player4];
            var player5NewRating = newRatings[player5];
            var player6NewRating = newRatings[player6];
            var player7NewRating = newRatings[player7];
            var player8NewRating = newRatings[player8];

            // In a real implementation, you'd store these values in a persistent
            // store like a database (note that you can use the player.Id to map
            // the Player class to the class of your choice.
            //AssertRating(29.39583201999924, 7.171475587326186, player1NewRating);
            //AssertRating(20.60416798000076, 7.171475587326186, player2NewRating);
        }

        private void cbGameType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbCompetitorA2.Visible = tbPreMeanA2.Visible = tbPreDeviationA2.Visible = tbPostMeanA2.Visible = tbPostDeviationA2.Visible =
                cbCompetitorH2.Visible = tbPreMeanH2.Visible = tbPreDeviationH2.Visible = tbPostMeanH2.Visible = tbPostDeviationH2.Visible =
                cbGameType.Text.EndsWith("Doubles") || cbGameType.Text.EndsWith("Triples");

            cbCompetitorA3.Visible = tbPreMeanA3.Visible = tbPreDeviationA3.Visible = tbPostMeanA3.Visible = tbPostDeviationA3.Visible =
                cbCompetitorH3.Visible = tbPreMeanH3.Visible = tbPreDeviationH3.Visible = tbPostMeanH3.Visible = tbPostDeviationH3.Visible =
                cbGameType.Text.EndsWith("Triples");
        }

        private void cmdNewMatch_Click(object sender, EventArgs e)
        {
            (new Form2(this)).ShowDialog();
        }

        private void cbCompetitor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbCombo = (ComboBox)sender;
            string ctrlName = cbCombo.Name;
            List<string> comboList = (List<string>)cbCombo.Tag;

            string s = ctrlName.Substring(ctrlName.Length - 2);
            TextBox tbPreMean = this.Controls.Find("tbPreMean" + s, true).FirstOrDefault() as TextBox;
            TextBox tbPreDeviation = this.Controls.Find("tbPreDeviation" + s, true).FirstOrDefault() as TextBox;
            TextBox tbPostMean = this.Controls.Find("tbPostMean" + s, true).FirstOrDefault() as TextBox;
            TextBox tbPostDeviation = this.Controls.Find("tbPostDeviation" + s, true).FirstOrDefault() as TextBox;

            if (cbCombo.Text == "<Add Team Member...>")
            {
                tbPreMean.Text = tbPreDeviation.Text = tbPostMean.Text = tbPostDeviation.Text = "";
                if (s.StartsWith("A"))
                    (new Form3(this, Convert.ToInt32(tbAwayTeamID.Text))).ShowDialog();
                else
                    (new Form3(this, Convert.ToInt32(tbHomeTeamID.Text))).ShowDialog();
            }
            else if (cbCombo.Text == "")
            {
                tbPreMean.Text = tbPreDeviation.Text = tbPostMean.Text = tbPostDeviation.Text = "";
            }
            else
            {
                tbPostMean.Text = tbPostDeviation.Text = "";
                SqlCommand cmdSql = new SqlCommand("select * from tblCompetitor where id = " + comboList[cbCombo.SelectedIndex].ToString(), connSql);

                using (SqlDataReader rReader = cmdSql.ExecuteReader())
                {
                    if (rReader.Read())
                    {
                        tbPreMean.Text = Convert.ToDecimal(rReader["RatingMean"]).ToString("f5");
                        tbPreDeviation.Text = Convert.ToDecimal(rReader["RatingDeviation"]).ToString("f5");
                    }
                    else
                        tbPreMean.Text = tbPreDeviation.Text = "";

                    rReader.Close();
                }
            }

            rbHome.Checked = rbAway.Checked = false;
        }

        private void cmdPost_Click(object sender, EventArgs e)
        {
            if (rbAway.Checked == rbHome.Checked)
                throw new InvalidOperationException();

            SqlCommand cmdSql = new SqlCommand("insert into tblGame select " + tbMatchId.Text + ", '" + cbGameType.Text + "'", connSql);
            cmdSql.ExecuteNonQuery();

            string sGameID = "";
            cmdSql.CommandText = "select @@identity from tblGame";
            using (SqlDataReader rReader = cmdSql.ExecuteReader())
            {
                if (rReader.Read())
                    sGameID = rReader[0].ToString();

                rReader.Close();
            }

            if (!chkInvalidGame.Checked)
            {
                double partialPlayPct = 1;
                if (cbCompetitorA3.Visible)
                    partialPlayPct = 1 / 3;
                else if (cbCompetitorA2.Visible)
                    partialPlayPct = 1 / 2;

                string competitorA1id = ((List<string>)cbCompetitorA1.Tag)[cbCompetitorA1.SelectedIndex];
                string competitorH1id = ((List<string>)cbCompetitorH1.Tag)[cbCompetitorH1.SelectedIndex];
                string competitorA2id = "0";
                string competitorH2id = "0";
                string competitorA3id = "0";
                string competitorH3id = "0";

                Player competitorA1 = new Player(competitorA1id, partialPlayPct);
                Player competitorH1 = new Player(competitorH1id, partialPlayPct);
                Player competitorA2 = new Player(0);
                Player competitorH2 = new Player(-1);
                Player competitorA3 = new Player(-2);
                Player competitorH3 = new Player(-3);

                Team awayTeam = new Team();
                Team homeTeam = new Team();
                GameInfo gameInfo = GameInfo.DefaultGameInfo;
                gameInfo.DrawProbability = 0;

                awayTeam.AddPlayer(competitorA1, new Rating(Convert.ToDouble(tbPreMeanA1.Text), Convert.ToDouble(tbPreDeviationA1.Text)));
                homeTeam.AddPlayer(competitorH1, new Rating(Convert.ToDouble(tbPreMeanH1.Text), Convert.ToDouble(tbPreDeviationH1.Text)));

                if (cbCompetitorA2.Visible)
                {
                    competitorA2id = ((List<string>)cbCompetitorA2.Tag)[cbCompetitorA2.SelectedIndex];
                    competitorH2id = ((List<string>)cbCompetitorH2.Tag)[cbCompetitorH2.SelectedIndex];
                    competitorA2 = new Player(competitorA2id, partialPlayPct);
                    competitorH2 = new Player(competitorH2id, partialPlayPct);

                    awayTeam.AddPlayer(competitorA2, new Rating(Convert.ToDouble(tbPreMeanA2.Text), Convert.ToDouble(tbPreDeviationA2.Text)));
                    homeTeam.AddPlayer(competitorH2, new Rating(Convert.ToDouble(tbPreMeanH2.Text), Convert.ToDouble(tbPreDeviationH2.Text)));
                }

                if (cbCompetitorA3.Visible)
                {
                    competitorA3id = ((List<string>)cbCompetitorA3.Tag)[cbCompetitorA3.SelectedIndex];
                    competitorH3id = ((List<string>)cbCompetitorH3.Tag)[cbCompetitorH3.SelectedIndex];
                    competitorA3 = new Player(competitorA3id, partialPlayPct);
                    competitorH3 = new Player(competitorH3id, partialPlayPct);

                    awayTeam.AddPlayer(competitorA3, new Rating(Convert.ToDouble(tbPreMeanA3.Text), Convert.ToDouble(tbPreDeviationA3.Text)));
                    homeTeam.AddPlayer(competitorH3, new Rating(Convert.ToDouble(tbPreMeanH3.Text), Convert.ToDouble(tbPreDeviationH3.Text)));
                }

                var teams = Teams.Concat(awayTeam, homeTeam);
                IDictionary<Player, Rating> newRatings;
                if (rbAway.Checked)
                    newRatings = TrueSkillCalculator.CalculateNewRatings(gameInfo, teams, 1, 2);
                else // (rbHome.Checked)
                    newRatings = TrueSkillCalculator.CalculateNewRatings(gameInfo, teams, 2, 1);

                UpdateCompetitor(sGameID, tbAwayTeamID.Text, competitorA1id, rbAway.Checked, newRatings[competitorA1]);
                UpdateCompetitor(sGameID, tbHomeTeamID.Text, competitorH1id, rbHome.Checked, newRatings[competitorH1]);

                if (cbCompetitorA2.Visible)
                {
                    UpdateCompetitor(sGameID, tbAwayTeamID.Text, competitorA2id, rbAway.Checked, newRatings[competitorA2]);
                    UpdateCompetitor(sGameID, tbHomeTeamID.Text, competitorH2id, rbHome.Checked, newRatings[competitorH2]);
                }
                if (cbCompetitorA3.Visible)
                {
                    UpdateCompetitor(sGameID, tbAwayTeamID.Text, competitorA3id, rbAway.Checked, newRatings[competitorA3]);
                    UpdateCompetitor(sGameID, tbHomeTeamID.Text, competitorH3id, rbHome.Checked, newRatings[competitorH3]);
                }
            }
            else
            {
                if (cbCompetitorA1.Text != "")
                    UpdateCompetitorWithNoRateChange(
                        sGameID, tbAwayTeamID.Text, ((List<string>)cbCompetitorA1.Tag)[cbCompetitorA1.SelectedIndex], rbAway.Checked);
                if (cbCompetitorA2.Text != "")
                    UpdateCompetitorWithNoRateChange(
                        sGameID, tbAwayTeamID.Text, ((List<string>)cbCompetitorA2.Tag)[cbCompetitorA2.SelectedIndex], rbAway.Checked);
                if (cbCompetitorA3.Text != "")
                    UpdateCompetitorWithNoRateChange(
                        sGameID, tbAwayTeamID.Text, ((List<string>)cbCompetitorA3.Tag)[cbCompetitorA3.SelectedIndex], rbAway.Checked);
                if (cbCompetitorH1.Text != "")
                    UpdateCompetitorWithNoRateChange(
                        sGameID, tbHomeTeamID.Text, ((List<string>)cbCompetitorH1.Tag)[cbCompetitorH1.SelectedIndex], rbHome.Checked);
                if (cbCompetitorH2.Text != "")
                    UpdateCompetitorWithNoRateChange(
                        sGameID, tbHomeTeamID.Text, ((List<string>)cbCompetitorH2.Tag)[cbCompetitorH2.SelectedIndex], rbHome.Checked);
                if (cbCompetitorH3.Text != "")
                    UpdateCompetitorWithNoRateChange(
                        sGameID, tbHomeTeamID.Text, ((List<string>)cbCompetitorH3.Tag)[cbCompetitorH3.SelectedIndex], rbHome.Checked);
            }

            cbCompetitorA1.SelectedIndex = cbCompetitorH1.SelectedIndex =
                    cbCompetitorA2.SelectedIndex = cbCompetitorH2.SelectedIndex =
                    cbCompetitorA3.SelectedIndex = cbCompetitorH3.SelectedIndex = 0;
            rbAway.Checked = rbHome.Checked = false;
        }

        private void UpdateCompetitorWithNoRateChange(string sGameId, string sTeamIdText, string competitorId, bool IsWinner)
        {
            SqlCommand cmdSql = new SqlCommand("", connSql);

            cmdSql.CommandText = "insert into tblGameCompetitor select " + sGameId + ", " + sTeamIdText + ", " + competitorId + ", " +
                Convert.ToInt32(IsWinner) + ", RatingMean, RatingDeviation, RatingMean, RatingDeviation" +
               // newRating.Mean.ToString("f5") + ", " + newRating.StandardDeviation.ToString("f5") +
                " from tblCompetitor where id = " + competitorId;
            cmdSql.ExecuteNonQuery();

            cmdSql.CommandText = "update tblCompetitor set wins = wins + " + Convert.ToInt32(IsWinner) +
                ", losses = losses + " + Convert.ToInt32(!IsWinner) +
                //", ratingMean = " + newRating.Mean.ToString("f5") +
                //", ratingDeviation = " + newRating.StandardDeviation.ToString("f5") +
                " where id = " + competitorId;
            cmdSql.ExecuteNonQuery();
        }
        
        private void UpdateCompetitor(string sGameId, string sTeamIdText, string competitorId, bool IsWinner, Rating newRating)
        {
            SqlCommand cmdSql = new SqlCommand("", connSql);

            cmdSql.CommandText = "insert into tblGameCompetitor select " + sGameId + ", " + sTeamIdText + ", " + competitorId + ", " +
                Convert.ToInt32(IsWinner) + ", RatingMean, RatingDeviation, " +
                newRating.Mean.ToString("f5") + ", " + newRating.StandardDeviation.ToString("f5") +
                " from tblCompetitor where id = " + competitorId;
            cmdSql.ExecuteNonQuery();

            cmdSql.CommandText = "update tblCompetitor set wins = wins + " + Convert.ToInt32(IsWinner) +
                ", losses = losses + " + Convert.ToInt32(!IsWinner) +
                ", ratingMean = " + newRating.Mean.ToString("f5") +
                ", ratingDeviation = " + newRating.StandardDeviation.ToString("f5") +
                " where id = " + competitorId;
            cmdSql.ExecuteNonQuery();
        }

        private void rbResult_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkInvalidGame.Checked)
            { 
                double partialPlayPct = 1;
                if (cbCompetitorA3.Visible)
                    partialPlayPct = 1 / 3;
                else if (cbCompetitorA2.Visible)
                    partialPlayPct = 1 / 2;

                Player competitorH1 = new Player(1, partialPlayPct);
                Player competitorA1 = new Player(2, partialPlayPct);
                Player competitorH2 = new Player(3, partialPlayPct);
                Player competitorA2 = new Player(4, partialPlayPct);
                Player competitorH3 = new Player(5, partialPlayPct);
                Player competitorA3 = new Player(6, partialPlayPct);

                Team awayTeam = new Team();
                Team homeTeam = new Team();
                GameInfo gameInfo = GameInfo.DefaultGameInfo;
                gameInfo.DrawProbability = 0;

                try
                {
                    homeTeam.AddPlayer(competitorH1, new Rating(Convert.ToDouble(tbPreMeanH1.Text), Convert.ToDouble(tbPreDeviationH1.Text)));
                    awayTeam.AddPlayer(competitorA1, new Rating(Convert.ToDouble(tbPreMeanA1.Text), Convert.ToDouble(tbPreDeviationA1.Text)));

                    if (cbCompetitorA2.Visible)
                    {
                        homeTeam.AddPlayer(competitorH2, new Rating(Convert.ToDouble(tbPreMeanH2.Text), Convert.ToDouble(tbPreDeviationH2.Text)));
                        awayTeam.AddPlayer(competitorA2, new Rating(Convert.ToDouble(tbPreMeanA2.Text), Convert.ToDouble(tbPreDeviationA2.Text)));
                    }
                    if (cbCompetitorA3.Visible)
                    {
                        homeTeam.AddPlayer(competitorH3, new Rating(Convert.ToDouble(tbPreMeanH3.Text), Convert.ToDouble(tbPreDeviationH3.Text)));
                        awayTeam.AddPlayer(competitorA3, new Rating(Convert.ToDouble(tbPreMeanA3.Text), Convert.ToDouble(tbPreDeviationA3.Text)));
                    }
                }
                catch (FormatException ex)
                {
                    return;
                }

                var teams = Teams.Concat(awayTeam, homeTeam);
                IDictionary<Player, Rating> newRatings;
                if (rbAway.Checked)
                    newRatings = TrueSkillCalculator.CalculateNewRatings(gameInfo, teams, 1, 2);
                else if (rbHome.Checked)
                    newRatings = TrueSkillCalculator.CalculateNewRatings(gameInfo, teams, 2, 1);
                else
                    throw new InvalidOperationException();

                tbPostMeanA1.Text = newRatings[competitorA1].Mean.ToString("f5");
                tbPostDeviationA1.Text = newRatings[competitorA1].StandardDeviation.ToString("f5");
                tbPostMeanH1.Text = newRatings[competitorH1].Mean.ToString("f5");
                tbPostDeviationH1.Text = newRatings[competitorH1].StandardDeviation.ToString("f5");

                if (cbCompetitorA2.Visible)
                {
                    tbPostMeanA2.Text = newRatings[competitorA2].Mean.ToString("f5");
                    tbPostDeviationA2.Text = newRatings[competitorA2].StandardDeviation.ToString("f5");
                    tbPostMeanH2.Text = newRatings[competitorH2].Mean.ToString("f5");
                    tbPostDeviationH2.Text = newRatings[competitorH2].StandardDeviation.ToString("f5");
                }
                if (cbCompetitorA3.Visible)
                {
                    tbPostMeanA3.Text = newRatings[competitorA3].Mean.ToString("f5");
                    tbPostDeviationA3.Text = newRatings[competitorA3].StandardDeviation.ToString("f5");
                    tbPostMeanH3.Text = newRatings[competitorH3].Mean.ToString("f5");
                    tbPostDeviationH3.Text = newRatings[competitorH3].StandardDeviation.ToString("f5");
                }
            }
            else
            {
                tbPostMeanA1.Text = tbPreMeanA1.Text;
                tbPostMeanA2.Text = tbPreMeanA2.Text;
                tbPostMeanA3.Text = tbPreMeanA3.Text;
                tbPostMeanH1.Text = tbPreMeanH1.Text;
                tbPostMeanH2.Text = tbPreMeanH2.Text;
                tbPostMeanH3.Text = tbPreMeanH3.Text;
                tbPostDeviationA1.Text = tbPreDeviationA1.Text;
                tbPostDeviationA2.Text = tbPreDeviationA2.Text;
                tbPostDeviationA3.Text = tbPreDeviationA3.Text;
                tbPostDeviationH1.Text = tbPreDeviationH1.Text;
                tbPostDeviationH2.Text = tbPreDeviationH2.Text;
                tbPostDeviationH3.Text = tbPreDeviationH3.Text;
            }
        }
    }
}
