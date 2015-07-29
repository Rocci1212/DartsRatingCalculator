using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moserware.Skills;
using System.Data.SqlClient;

namespace DartsRatingCalculator
{
    public class CampaignRecord
    {
        public Campaign _Campaign;
        public int Wins, Losses;
    }

    public class ClassRating
    {
        public Class _Class;
        public Rating _Rating;
    }

    public class CampaignRating
    {
        public Campaign _Campaign;
        public Rating _Rating;
    }
    
    public class DartsPlayer : Player
    {
        // for example http://stats.mmdl.org/index.php?view=player&playerid=12164 would have a player id of 12164
        public int Id;
        public string Name;
        public Rating _Rating; // all time rating
        Dictionary<Campaign, Rating> CampaignRatings;
        Dictionary<Class, Rating> ClassRating;

        public DartsPlayer(string name, string teamId) : base(null)
        {
            Name = name;
            //Id  = 
        }
        
        public DartsPlayer(int playerId) : base(playerId)
        {
            
        }

        internal static void InsertNewPlayer(int playerId, string playerName, int teamId)
        {
            SqlConnection connSql = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connSql.Open();

            SqlCommand cmdSql = new SqlCommand("InsertNewPlayer", connSql);
            cmdSql.CommandType = System.Data.CommandType.StoredProcedure;
            cmdSql.Parameters.AddWithValue("@PlayerId", playerId);
            cmdSql.Parameters.AddWithValue("@PlayerName", playerName);
            cmdSql.Parameters.AddWithValue("@SquadId", teamId);
            cmdSql.Parameters.AddWithValue("@RatingMean", GameInfo.DefaultGameInfo.DefaultRating.Mean);
            cmdSql.Parameters.AddWithValue("@RatingDeviation", GameInfo.DefaultGameInfo.DefaultRating.StandardDeviation);

            cmdSql.ExecuteNonQuery();

            connSql.Close();
        }
    }
}
