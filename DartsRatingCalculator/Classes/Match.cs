using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartsRatingCalculator
{
    public class Match
    {
        // for example http://stats.mmdl.org/index.php?view=match&matchid=36527 would have a match id of 36527
        public int MatchId;
        public int WeekNumber;
        public Squad AwaySquad, HomeSquad;
        public Campaign _Campaign;
        public Game[] Games;

        public Match(string matchDesc, string squadDesc, string campaignDesc)
        {
            MatchId = Convert.ToInt32(matchDesc.Substring(matchDesc.IndexOf("#") + 1));
            WeekNumber = Convert.ToInt32(squadDesc.Substring(5, squadDesc.IndexOf(":") - 5));
            _Campaign = Campaign.GetCampaignFromDesc(campaignDesc);
            Squad.GetSquadsFromDesc(squadDesc, _Campaign, ref AwaySquad, ref HomeSquad);
        }

        public static void InsertMatchHeader(int matchId, string weekNumber, int teamId)
        {
            SqlConnection connSql = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connSql.Open();

            SqlCommand cmdSql = new SqlCommand("CommitMatch", connSql);
            cmdSql.CommandType = System.Data.CommandType.StoredProcedure;
            cmdSql.Parameters.AddWithValue("@MatchId", matchId);
            cmdSql.Parameters.AddWithValue("@WeekNumber", weekNumber);
            cmdSql.Parameters.AddWithValue("@SquadId", teamId);

            cmdSql.ExecuteNonQuery();

            connSql.Close();
        }
    }
}
