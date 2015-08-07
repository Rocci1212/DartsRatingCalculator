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
        public int SquadId;
        string Name, Sponsor, City;
        Campaign _Campaign;
        public Dictionary<string, DartsPlayer> DartsPlayers = new Dictionary<string, DartsPlayer>(); // so i can pull the player by his/her name

        public static void GetSquadsFromDesc(string squadDesc, Campaign campaign, ref Squad awaySquad, ref Squad homeSquad)
        {
            SqlConnection connSql = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connSql.Open();
            SqlCommand cmdSql;

            squadDesc = squadDesc.Substring(squadDesc.IndexOf(":") + 1).Trim();

            string[] x = squadDesc.Split(new string[] { " at " }, StringSplitOptions.None);
            if (x.Length < 2)
                throw (new InvalidOperationException());
            if (x.Length > 2)
            {
                cmdSql = new SqlCommand("GetSquad", connSql);
                cmdSql.CommandType = System.Data.CommandType.StoredProcedure;
                cmdSql.Parameters.AddWithValue("@Campaign", campaign.ID);

                string annoyingTeamNameThatContainsAt = "";

                using (SqlDataReader rReader = cmdSql.ExecuteReader())
                {
                    while (rReader.Read())
                    {
                        if (rReader["Name"].ToString().Contains(" at ") && squadDesc.Contains(rReader["Name"].ToString()))
                        {
                            annoyingTeamNameThatContainsAt = rReader["Name"].ToString();
                        }
                    }
                }

                if (squadDesc.IndexOf(annoyingTeamNameThatContainsAt) == 0)
                {
                    x = new string[] { annoyingTeamNameThatContainsAt, squadDesc.Substring(annoyingTeamNameThatContainsAt.Length + 4)};
                }
                else
                {
                    x = new string[] { squadDesc.Substring(0, squadDesc.IndexOf(annoyingTeamNameThatContainsAt) - 4), annoyingTeamNameThatContainsAt };
                }
            }
            if (x.Length == 2)
            {
                cmdSql = new SqlCommand("GetSquad", connSql);
                cmdSql.CommandType = System.Data.CommandType.StoredProcedure;

                cmdSql.Parameters.AddWithValue("@Name", x[0]);
                cmdSql.Parameters.AddWithValue("@Campaign", campaign.ID);

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
                        awaySquad.PopulatePlayers();
                    }
                    else
                        throw new Exception("Away Squad not found!");
                }

                cmdSql = new SqlCommand("GetSquad", connSql);
                cmdSql.CommandType = System.Data.CommandType.StoredProcedure;

                cmdSql.Parameters.AddWithValue("@Name", x[1]);
                cmdSql.Parameters.AddWithValue("@Campaign", campaign.ID);

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
                        homeSquad.PopulatePlayers();
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

        internal static void CommitSquad(int squadId, int campaignId)
        {
            SqlConnection connSql = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connSql.Open();

            SqlCommand cmdSql = new SqlCommand("CommitSquad", connSql);
            cmdSql.CommandType = System.Data.CommandType.StoredProcedure;
            cmdSql.Parameters.AddWithValue("@SquadId", squadId);
            cmdSql.Parameters.AddWithValue("@CampaignId", campaignId);

            cmdSql.ExecuteNonQuery();

            connSql.Close();
        }

        internal static void CommitSquadDetails(int squadId, string teamName, string sponsor, string city)
        {
            SqlConnection connSql = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connSql.Open();

            SqlCommand cmdSql = new SqlCommand("CommitSquadDetails", connSql);
            cmdSql.CommandType = System.Data.CommandType.StoredProcedure;
            cmdSql.Parameters.AddWithValue("@SquadId", squadId);
            cmdSql.Parameters.AddWithValue("@TeamName", teamName);
            cmdSql.Parameters.AddWithValue("@Sponsor", sponsor);
            cmdSql.Parameters.AddWithValue("@City", city);

            cmdSql.ExecuteNonQuery();

            connSql.Close();
        }

        private void PopulatePlayers()
        {
            SqlConnection connSql = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connSql.Open();

            SqlCommand cmdSql = new SqlCommand("GetSquadPlayers", connSql);
            cmdSql.CommandType = System.Data.CommandType.StoredProcedure;
            cmdSql.Parameters.AddWithValue("@SquadId", this.SquadId);

            using (SqlDataReader rReader = cmdSql.ExecuteReader())
            {
                while (rReader.Read())
                {
                    DartsPlayer player = DartsPlayer.GetPlayer(Convert.ToInt32(rReader["Player"]));

                    if (!DartsPlayers.ContainsKey(player.Name))
                        DartsPlayers.Add(player.Name, player);
                }
            }

            connSql.Close();
        }
    }
}
