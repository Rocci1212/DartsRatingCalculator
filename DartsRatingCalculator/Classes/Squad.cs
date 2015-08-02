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
        string Name, Sponsor, City;
        Campaign _Campaign;
        DartsPlayer[] DartsPlayers;

        public static void GetSquadsFromDesc(string squadDesc, Campaign campaign, ref Squad awaySquad, ref Squad homeSquad)
        {
            SqlConnection connSql = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connSql.Open();

            squadDesc = squadDesc.Substring(squadDesc.IndexOf(":") + 1).Trim();

            string[] x = squadDesc.Split(new string[] { " at " }, StringSplitOptions.None);
            if (x.Length < 2)
                throw (new InvalidOperationException());
            if (x.Length > 2)
                throw (new NotImplementedException());
            if (x.Length == 2)
            {
                SqlCommand cmdSql = new SqlCommand("select * from squad where name = @name and campaign = @campaign", connSql);

                cmdSql.Parameters.AddWithValue("@name", x[0]);
                cmdSql.Parameters.AddWithValue("@campaign", campaign.id);

                using (SqlDataReader rReader = cmdSql.ExecuteReader())
                {
                    if (rReader.Read())
                    {
                        awaySquad = new Squad();
                        awaySquad.SquadId = Convert.ToInt32(rReader["Id"]);
                        awaySquad.Name = Convert.ToString(rReader["Name"]);
                        awaySquad.Sponsor = Convert.ToString(rReader["Sponsor"]);
                        awaySquad.City = Convert.ToString(rReader["City"]);
                        awaySquad._Campaign = campaign;
                    }
                    else
                        throw new Exception("Away Squad not found!");
                }

                cmdSql = new SqlCommand("select * from squad where name = @name and campaign = @campaign", connSql);

                cmdSql.Parameters.AddWithValue("@name", x[1]);
                cmdSql.Parameters.AddWithValue("@campaign", campaign.id);

                using (SqlDataReader rReader = cmdSql.ExecuteReader())
                {
                    if (rReader.Read())
                    {
                        homeSquad = new Squad();
                        homeSquad.SquadId = Convert.ToInt32(rReader["Id"]);
                        homeSquad.Name = Convert.ToString(rReader["Name"]);
                        homeSquad.Sponsor = Convert.ToString(rReader["Sponsor"]);
                        homeSquad.City = Convert.ToString(rReader["City"]);
                        homeSquad._Campaign = campaign;
                    }
                    else
                        throw new Exception("Home Squad not found!");
                }

            }

            connSql.Close();
        }

        public DartsPlayer GetPlayerByName(string name)
        {
            // TODO return a player
            DartsPlayer player = new DartsPlayer(0);

            return player;
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
