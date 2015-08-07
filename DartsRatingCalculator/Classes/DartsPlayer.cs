using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moserware.Skills;
using System.Data.SqlClient;

namespace DartsRatingCalculator
{    
    public class DartsPlayer : Player
    {
        // for example http://stats.mmdl.org/index.php?view=player&playerid=12164 would have a player id of 12164
        public int Id;
        public string Name;
        public Rating _Rating; // all time rating
        public Dictionary<int, Rating> CampaignRatings = new Dictionary<int, Rating>();
        public Dictionary<int, Rating> ClassRatings = new Dictionary<int, Rating>();

        public DartsPlayer(string name, string teamId) : base(null)
        {
            Name = name;
            //Id  = 
        }
        
        public DartsPlayer(int playerId) : base(playerId)
        {
            Id = playerId;
        }

        public static int CreateDummyPlayer(string name, int squadId)
        {
            int i = -1;
            SqlConnection connSql = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connSql.Open();
            
            SqlCommand cmdSql = new SqlCommand("BlindCommitPlayer", connSql);
            cmdSql.CommandType = System.Data.CommandType.StoredProcedure;
            cmdSql.Parameters.AddWithValue("@PlayerName", name);
            cmdSql.Parameters.AddWithValue("@SquadId", squadId);

            using (SqlDataReader rReader = cmdSql.ExecuteReader())
            {
                if (rReader.Read())
                {
                    i = Convert.ToInt32(rReader[0]);
                }
            }

            connSql.Close();

            return i;
        }

        public static void InsertNewPlayer(int playerId, string playerName, int teamId)
        {
            SqlConnection connSql = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connSql.Open();

            SqlCommand cmdSql = new SqlCommand("CommitPlayer", connSql);
            cmdSql.CommandType = System.Data.CommandType.StoredProcedure;
            cmdSql.Parameters.AddWithValue("@PlayerId", playerId);
            cmdSql.Parameters.AddWithValue("@PlayerName", playerName);
            cmdSql.Parameters.AddWithValue("@SquadId", teamId);

            cmdSql.ExecuteNonQuery();

            connSql.Close();
        }

        public static DartsPlayer GetPlayer(int playerId)
        {
            DartsPlayer player = new DartsPlayer(playerId);

            SqlConnection connSql = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connSql.Open();

            SqlCommand cmdSql = new SqlCommand("GetPlayer", connSql);
            cmdSql.CommandType = System.Data.CommandType.StoredProcedure;
            cmdSql.Parameters.AddWithValue("@PlayerID", playerId);

            using (SqlDataReader rReader = cmdSql.ExecuteReader())
            {
                if (rReader.Read())
                {
                    player.Name = Convert.ToString(rReader["Name"]);
                    player._Rating = new Rating(Convert.ToDouble(rReader["RatingMean"]), Convert.ToDouble(rReader["RatingDeviation"]));
                }
            }

            cmdSql = new SqlCommand("GetPlayerClassRatings", connSql);
            cmdSql.CommandType = System.Data.CommandType.StoredProcedure;
            cmdSql.Parameters.AddWithValue("@PlayerID", playerId);

            using (SqlDataReader rReader = cmdSql.ExecuteReader())
            {
                while (rReader.Read())
                {
                    player.ClassRatings.Add(Convert.ToInt32(rReader["Class"]), 
                        new Rating(Convert.ToDouble(rReader["RatingMean"]), Convert.ToDouble(rReader["RatingDeviation"])));
                }
            }

            cmdSql = new SqlCommand("GetPlayerCampaignRatings", connSql);
            cmdSql.CommandType = System.Data.CommandType.StoredProcedure;
            cmdSql.Parameters.AddWithValue("@PlayerID", playerId);

            using (SqlDataReader rReader = cmdSql.ExecuteReader())
            {
                while (rReader.Read())
                {
                    player.CampaignRatings.Add(Convert.ToInt32(rReader["Campaign"]),
                        new Rating(Convert.ToDouble(rReader["RatingMean"]), Convert.ToDouble(rReader["RatingDeviation"])));
                }
            }

            connSql.Close();

            return player;
        }
    }
}
