using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Data.SqlClient;

namespace DartsRatingCalculator
{
    public partial class TestingForm : Form
    {
        public TestingForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            // fall 09 supera
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=7");

            // fall 2009
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=5&conferenceid=1");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=5&conferenceid=2");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=5&conferenceid=4");

            // spring 10 supera
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=8");

            // spring 10
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=9&conferenceid=1");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=9&conferenceid=2");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=9&conferenceid=3");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=9&conferenceid=4");

            // fall 10 supera
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=11");

            // fall 10
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=10&conferenceid=1");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=10&conferenceid=2");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=10&conferenceid=3");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=10&conferenceid=4");

            // spring 11
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=12&conferenceid=1");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=12&conferenceid=2");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=12&conferenceid=3");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=12&conferenceid=4");

            // fall 11
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=13&conferenceid=1");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=13&conferenceid=2");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=13&conferenceid=3");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=13&conferenceid=4");

            // spring 12
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=14&conferenceid=1");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=14&conferenceid=2");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=14&conferenceid=3");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=14&conferenceid=4");

            // fall 12
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=15&conferenceid=1");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=15&conferenceid=2");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=15&conferenceid=3");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=15&conferenceid=4");

            // spring 13
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=16&conferenceid=1");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=16&conferenceid=2");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=16&conferenceid=3");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=16&conferenceid=4");

            // fall 13
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=17&conferenceid=1");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=17&conferenceid=2");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=17&conferenceid=3");

            // spring 14
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=18&conferenceid=1");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=18&conferenceid=2");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=18&conferenceid=3");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=18&conferenceid=4");

            // fall 14
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=20&conferenceid=1");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=20&conferenceid=2");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=20&conferenceid=3");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=20&conferenceid=4");

            // spring 15
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=21&conferenceid=1");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=21&conferenceid=2");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=21&conferenceid=3");
            Utility.FarmingFunctions.FarmStandingsPage("http://stats.mmdl.org/index.php?view=standings&seasonid=21&conferenceid=4");
            */
            SqlConnection connSql = new SqlConnection(
                Gravoc.Encryption.Encryption.Decrypt(Properties.Settings.Default.ConnectionString));
            connSql.Open();
            
            SqlCommand cmdSql = new SqlCommand("select id from squad", connSql);
            /*
            using (SqlDataReader rReader = cmdSql.ExecuteReader())
            {
                while (rReader.Read())
                {
                    Utility.FarmingFunctions.FarmTeamPage(Convert.ToInt32(rReader[0]));
                }
            }
            */
            cmdSql = new SqlCommand("select * from match where id <> 3007 order by campaign, weekNumber, id", connSql);

            using (SqlDataReader rReader = cmdSql.ExecuteReader())
            {
                while (rReader.Read())
                {
                    Utility.FarmingFunctions.FarmMatchPage(Convert.ToInt32(rReader["ID"]), Convert.ToInt32(rReader["Campaign"]));
                }
            }

            connSql.Close();
        }
    }
}
