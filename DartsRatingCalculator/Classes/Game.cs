using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moserware.Skills;
using System.Data.SqlClient;

namespace DartsRatingCalculator
{
    public enum GameType
    {
        singles301 = 1,
        doubles301 = 2,
        singles501 = 5,
        doubles501 = 6,
        triples601 = 7,
        singlesCricket = 9,
        doublesCricket = 10
    }

    public class Game
    {
        public GameType _GameType;
        public List<DartsPlayer> AwayDartsPlayers = new List<DartsPlayer>();
        public List<DartsPlayer> HomeDartsPlayers = new List<DartsPlayer>();
        public int GameNumber;
        public bool IsHomeWin; // false if away wins, true if home wins

        public static GameType GetGameTypeFromString(string inputString)
        {
            switch (inputString)
            {
                case "301 Singles":
                    return GameType.singles301;
                case "301 Doubles":
                    return GameType.doubles301;
                case "501 Singles":
                    return GameType.singles501;
                case "501 Doubles":
                    return GameType.doubles501;
                case "601 Triples":
                    return GameType.triples601;
                case "Cricket Singles":
                    return GameType.singlesCricket;
                case "Cricket Doubles":
                    return GameType.doublesCricket;
                default:
                    throw new Exception("Game Type not found!");
            }
        }

        public Game()
        {
        }

        public void CalculateAndCommitGame(Match match)
        {
            // TODO: Weed out matches like this: http://stats.mmdl.org/index.php?view=match&matchid=33567
            List<Player> awayPlayers = new List<Player>(); 
            List<Player> homePlayers = new List<Player>();
            foreach (DartsPlayer x in AwayDartsPlayers.Concat(HomeDartsPlayers))
            {
                CommitGame(x, match, "initial");
            }
            CalculateNewRatings("player", match);
            CalculateNewRatings("class", match);
            CalculateNewRatings("campaign", match);
        }

        public void CalculateNewRatings(string type, Match match)
        {
            Team awayTeam = new Team();
            Team homeTeam = new Team();
            GameInfo gameInfo = GameInfo.DefaultGameInfo;
            gameInfo.DrawProbability = 0;

            foreach (DartsPlayer x in AwayDartsPlayers)
            {
                switch (type)
                {
                    case "player":
                        awayTeam.AddPlayer(x, x._Rating);
                        break;
                    case "class":
                        awayTeam.AddPlayer(x, x.ClassRatings[Convert.ToInt32(match._Campaign._Class)]);
                        break;
                    case "campaign":
                        awayTeam.AddPlayer(x, x.CampaignRatings[Convert.ToInt32(match._Campaign.ID)]);
                        break;
                }
            }
            foreach (DartsPlayer x in HomeDartsPlayers)
            {
                switch (type)
                {
                    case "player":
                        homeTeam.AddPlayer(x, x._Rating);
                        break;
                    case "class":
                        homeTeam.AddPlayer(x, x.ClassRatings[Convert.ToInt32(match._Campaign._Class)]);
                        break;
                    case "campaign":
                        homeTeam.AddPlayer(x, x.CampaignRatings[Convert.ToInt32(match._Campaign.ID)]);
                        break;
                }
            }

            if (AwayDartsPlayers.Count == 0 && HomeDartsPlayers.Count == 0)
                return;
            
            if (AwayDartsPlayers.Count != HomeDartsPlayers.Count)
                return;

            var teams = Teams.Concat(awayTeam, homeTeam);
            IDictionary<Player, Rating> newRatings;

            if (IsHomeWin)
                newRatings = TrueSkillCalculator.CalculateNewRatings(gameInfo, teams, 2, 1);
            else // IsAwayWin
                newRatings = TrueSkillCalculator.CalculateNewRatings(gameInfo, teams, 1, 2);

            foreach (DartsPlayer x in AwayDartsPlayers.Concat(HomeDartsPlayers))
            {
                switch (type)
                {
                    case "player":
                        x._Rating = newRatings[x];
                        break;
                    case "class":
                        x.ClassRatings[Convert.ToInt32(match._Campaign._Class)] = newRatings[x];
                        break;
                    case "campaign":
                        x.CampaignRatings[Convert.ToInt32(match._Campaign.ID)] = newRatings[x];
                        break;
                }

                CommitGame(x, match, type);
            }
        }

        public void CommitGame(DartsPlayer player, Match match, string ratingToAdjust)
        {
            SqlConnection connSql = new SqlConnection(
                Gravoc.Encryption.Encryption.Decrypt(Properties.Settings.Default.ConnectionString));
            connSql.Open();

            SqlCommand cmdSql = new SqlCommand("CommitGame", connSql);
            cmdSql.CommandType = System.Data.CommandType.StoredProcedure;

            cmdSql.Parameters.AddWithValue("@Match", match.MatchId);
            cmdSql.Parameters.AddWithValue("@GameNumber", GameNumber);
            cmdSql.Parameters.AddWithValue("@GameType", Convert.ToInt32(_GameType));
            cmdSql.Parameters.AddWithValue("@Player", player.Id);
            if (AwayDartsPlayers.Contains(player) ^ IsHomeWin)
                cmdSql.Parameters.AddWithValue("@Result", 1);
            else
                cmdSql.Parameters.AddWithValue("@Result", 0);
            cmdSql.Parameters.AddWithValue("@RatingToAdjust", ratingToAdjust);
            switch(ratingToAdjust)
            {
                case "player":
                    cmdSql.Parameters.AddWithValue("@NewRatingMean", player._Rating.Mean);
                    cmdSql.Parameters.AddWithValue("@NewRatingDeviation", player._Rating.StandardDeviation);
                    break;
                case "campaign":
                    cmdSql.Parameters.AddWithValue("@NewRatingMean", player.CampaignRatings[Convert.ToInt32(match._Campaign.ID)].Mean);
                    cmdSql.Parameters.AddWithValue("@NewRatingDeviation", player.CampaignRatings[Convert.ToInt32(match._Campaign.ID)].StandardDeviation);
                    break;
                case "class":
                    cmdSql.Parameters.AddWithValue("@NewRatingMean", player.ClassRatings[Convert.ToInt32(match._Campaign._Class)].Mean);
                    cmdSql.Parameters.AddWithValue("@NewRatingDeviation", player.ClassRatings[Convert.ToInt32(match._Campaign._Class)].StandardDeviation);
                    break;
                default:
                    cmdSql.Parameters.AddWithValue("@NewRatingMean", -1);
                    cmdSql.Parameters.AddWithValue("@NewRatingDeviation", -1);
                    break;
            }

            cmdSql.ExecuteNonQuery();

            connSql.Close();
        }
    }
}
