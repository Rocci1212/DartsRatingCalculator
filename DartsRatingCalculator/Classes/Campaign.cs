using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartsRatingCalculator
{
    public enum Class
    {
        SuperA = 0,
        A = 1,
        B = 2,
        C = 3,
        D = 4,
        E = 5
    }

    public enum Conference
    {
        Boston = 1,
        Central = 2,
        NorthShore = 3,
        SouthShore = 4
    }

    public enum Season
    {
        Fall = 0,
        Spring = 1
    }

    public class Campaign
    {
        public int id; // autonumber
        public Season _Season;
        public int Year;
        public Class _Class;
        public Conference _Conference;
        public int? Identifier;

        public static Campaign GetCampaignFromDesc(string campaignDesc)
        {
            // split the campaign description
            string[] attributes = campaignDesc.Split(' ');
            Campaign campaign = new Campaign();

            // get the conference
            switch (attributes[1])
            {
                case "Bos":
                    campaign._Conference = Conference.Boston;
                    break;
                case "Cent":
                    campaign._Conference = Conference.Central;
                    break;
                case "NS":
                    campaign._Conference = Conference.NorthShore;
                    break;
                case "SS":
                    campaign._Conference = Conference.SouthShore;
                    break;
            }

            // get the class, identifier
            if (attributes[2].Substring(0, 2) == "SA")
                campaign._Class = Class.SuperA;
            else
            { 
                campaign._Class = (Class)Enum.Parse(typeof(Class), attributes[2].Substring(0, 1));
                campaign.Identifier = Convert.ToInt32(attributes[2].Substring(1, 1));
            }

            // get the season and year
            campaign._Season = (Season)Enum.Parse(typeof(Season), attributes[3]);
            campaign.Year = Convert.ToInt32(attributes[4]);

            // pull the id from the database
            campaign.id = GetNewOrExistingCampaignId(ref campaign);

            return campaign;
        }

        public static int GetNewOrExistingCampaignId(ref Campaign campaign)
        {
            SqlConnection connSql = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connSql.Open();

            SqlCommand cmdSql = new SqlCommand("GetNewOrExistingCampaignIdFromProperties", connSql);
            cmdSql.CommandType = System.Data.CommandType.StoredProcedure;
            cmdSql.Parameters.AddWithValue("@Season", Convert.ToInt32(campaign._Season));
            cmdSql.Parameters.AddWithValue("@Year", campaign.Year);
            cmdSql.Parameters.AddWithValue("@Class", Convert.ToInt32(campaign._Class));
            cmdSql.Parameters.AddWithValue("@Conference", Convert.ToInt32(campaign._Conference));
            if (campaign.Identifier != null)
                cmdSql.Parameters.AddWithValue("@Identifier", Convert.ToInt32(campaign.Identifier));

            int returnValue = -1;
            
            using (SqlDataReader rReader = cmdSql.ExecuteReader())
            {
                if (rReader.Read())
                {
                    returnValue = Convert.ToInt32(rReader[0]);
                }
                else
                    throw (new IndexOutOfRangeException());

                rReader.Close();
            }

            connSql.Close();

            campaign.id = returnValue;

            return returnValue;
        }
    }
}
