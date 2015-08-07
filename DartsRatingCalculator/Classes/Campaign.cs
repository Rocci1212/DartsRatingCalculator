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
        public int ID { get; private set; } // autonumber
        public Season _Season { get; private set; }
        public int Year { get; private set; }
        public Class _Class { get; private set; }
        public Conference _Conference { get; private set; }
        public int? Identifier { get; private set; }

        public Campaign(int id, Season season, int year, Class _class, Conference conference, int? identifier)
        {
            ID = id;
            _Season = season;
            Year = year;
            _Class = _class;
            _Conference = conference;
            Identifier = identifier;
        }

        public static Campaign GetCampaign(int campaignId)
        {
            Campaign campaign = new Campaign(0, Season.Fall, 0, Class.SuperA, Conference.Boston, null);
            SqlConnection connSql = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connSql.Open();

            SqlCommand cmdSql = new SqlCommand("GetCampaign", connSql);
            cmdSql.CommandType = System.Data.CommandType.StoredProcedure;
            cmdSql.Parameters.AddWithValue("@CampaignID", campaignId);

            using (SqlDataReader rReader = cmdSql.ExecuteReader())
            {
                if (rReader.Read())
                {
                    if (rReader["Identifier"] != DBNull.Value)
                        campaign = new Campaign(
                            campaignId, (Season)rReader["Season"],
                            Convert.ToInt32(rReader["Year"]),
                            (Class)rReader["Class"],
                            (Conference)rReader["Conference"],
                            Convert.ToInt32(rReader["Identifier"]));
                    else
                        campaign = new Campaign(
                            campaignId, (Season)rReader["Season"],
                            Convert.ToInt32(rReader["Year"]),
                            (Class)rReader["Class"],
                            (Conference)rReader["Conference"],
                            null);
                }
            }

            connSql.Close();

            return campaign;
        }

        public static Campaign GetCampaignFromDesc(string campaignDesc)
        {
            int id;
            Season season;
            int year;
            Class _class;
            Conference conference = new Conference();
            int? identifier = null;

            // split the campaign description
            string[] attributes = campaignDesc.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // get the conference
            switch (attributes[1])
            {
                case "Bos":
                    conference = Conference.Boston;
                    break;
                case "Cent":
                    conference = Conference.Central;
                    break;
                case "NS":
                    conference = Conference.NorthShore;
                    break;
                case "SS":
                    conference = Conference.SouthShore;
                    break;
            }

            // get the class, identifier
            if (attributes[2].Substring(0, 2) == "SA")
                _class = Class.SuperA;
            else
            {
                _class = (Class)Enum.Parse(typeof(Class), attributes[2].Substring(0, 1));
                identifier = Convert.ToInt32(attributes[2].Substring(1, 1));
            }

            // get the season and year
            season = (Season)Enum.Parse(typeof(Season), attributes[3]);
            year = Convert.ToInt32(attributes[4]);

            // pull the id from the database
            id = CommitCampaign(season, year, _class, conference, identifier);

            return new Campaign(id, season, year, _class, conference, identifier);
        }

        public static int CommitCampaign(Season season, int year, Class _class, Conference conference, int? identifier)
        {
            SqlConnection connSql = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connSql.Open();

            SqlCommand cmdSql = new SqlCommand("CommitCampaign", connSql);
            cmdSql.CommandType = System.Data.CommandType.StoredProcedure;
            cmdSql.Parameters.AddWithValue("@Season", Convert.ToInt32(season));
            cmdSql.Parameters.AddWithValue("@Year", year);
            cmdSql.Parameters.AddWithValue("@Class", Convert.ToInt32(_class));
            cmdSql.Parameters.AddWithValue("@Conference", Convert.ToInt32(conference));
            if (identifier != null)
                cmdSql.Parameters.AddWithValue("@Identifier", Convert.ToInt32(identifier));

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

            return returnValue;
        }
    }
}
