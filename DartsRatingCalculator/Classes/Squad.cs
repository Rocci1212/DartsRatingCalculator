using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartsRatingCalculator
{
    public class Squad
    {
        // for example, http://stats.mmdl.org/index.php?view=team&teamid=6246 would have a squad id of 6246
        int SquadId;
        string Name, Sponsor, Location;
        Campaign _Campaign;
        DartsPlayer[] DartsPlayers;

        public static void GetSquadsFromDesc(string squadDesc, Campaign campaign, ref Squad awaySquad, ref Squad homeSquad)
        {
            string[] x = squadDesc.Split(new string[] { " at " }, StringSplitOptions.None);
            if (x.Length < 2)
                throw (new InvalidOperationException());
            if (x.Length > 2)
                throw (new NotImplementedException());
            if (x.Length == 2)
            {
                // look up the squads by name from the database
            }
        }

        public static void InsertNewSquad(int squadId, int campaignId)
        {
            SqlConnection connSql = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connSql.Open();

            SqlCommand cmdSql = new SqlCommand("InsertNewSquad", connSql);
            cmdSql.CommandType = System.Data.CommandType.StoredProcedure;
            cmdSql.Parameters.AddWithValue("@SquadId", squadId);
            cmdSql.Parameters.AddWithValue("@CampaignId", campaignId);

            cmdSql.ExecuteNonQuery();

            connSql.Close();
        }

        internal static void UpdateSquadInfo(int squadId, string teamName, string sponsor, string city)
        {
            SqlConnection connSql = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connSql.Open();

            SqlCommand cmdSql = new SqlCommand("UpdateSquadInfo", connSql);
            cmdSql.CommandType = System.Data.CommandType.StoredProcedure;
            cmdSql.Parameters.AddWithValue("@SquadId", squadId);
            cmdSql.Parameters.AddWithValue("@TeamName", teamName);
            cmdSql.Parameters.AddWithValue("@Sponsor", sponsor);
            cmdSql.Parameters.AddWithValue("@City", city);

            cmdSql.ExecuteNonQuery();

            connSql.Close();
        }
    }
}
