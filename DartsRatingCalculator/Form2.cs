using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// legacy code
namespace DartsRatingCalculator
{
    public partial class Form2 : Form
    {
        Form1 frmGameEntry;
        SqlConnection connSql = new SqlConnection(Properties.Settings.Default.ConnectionString);

        public Form2(Form1 frmGameEntry)
        {
            
            InitializeComponent();
            this.frmGameEntry = frmGameEntry;
            connSql.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> awayIndexList = (List<string>)cbAway.Tag;
            List<string> homeIndexList = (List<string>)cbHome.Tag;

            frmGameEntry.tbAwayTeamID.Text = Convert.ToString(awayIndexList[cbAway.SelectedIndex]);
            frmGameEntry.tbHomeTeamID.Text = Convert.ToString(homeIndexList[cbHome.SelectedIndex]);

            ComboFill.FillPlayer(frmGameEntry.cbCompetitorA1, frmGameEntry.tbAwayTeamID.Text);
            ComboFill.FillPlayer(frmGameEntry.cbCompetitorA2, frmGameEntry.tbAwayTeamID.Text);
            ComboFill.FillPlayer(frmGameEntry.cbCompetitorA3, frmGameEntry.tbAwayTeamID.Text);
            ComboFill.FillPlayer(frmGameEntry.cbCompetitorH1, frmGameEntry.tbHomeTeamID.Text);
            ComboFill.FillPlayer(frmGameEntry.cbCompetitorH2, frmGameEntry.tbHomeTeamID.Text);
            ComboFill.FillPlayer(frmGameEntry.cbCompetitorH3, frmGameEntry.tbHomeTeamID.Text);

            SqlCommand cmdSql = new SqlCommand(
                "insert into tblMatch select " + textBox1.Text + "," +
                Convert.ToString(homeIndexList[cbHome.SelectedIndex]) + "," +
                "'" + frmGameEntry.cbSeason.Text + "'," +
                frmGameEntry.nmYear.Value.ToString() + "," +
                "'" + frmGameEntry.cbClass.Text + "'," +
                "'" + frmGameEntry.cbLocation.Text + "'," +
                "'" + frmGameEntry.tbIdentifier.Text + "'", connSql);
            cmdSql.ExecuteNonQuery();

            cmdSql = new SqlCommand("select @@IDENTITY from tblMatch", connSql);
            using (SqlDataReader rReader = cmdSql.ExecuteReader())
            {
                if (rReader.Read())
                {
                    frmGameEntry.tbMatchId.Text = rReader[0].ToString();
                }
                rReader.Close();
            }

            Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string cmdSql = "select id, TeamName + '(' + Sponsor + ', ' + Location + ')' as descr from tblTeam " +
                "where year = " + frmGameEntry.nmYear.Value.ToString() +
                " and season = '" + frmGameEntry.cbSeason.Text + "'" +
                " and divisionClass = '" + frmGameEntry.cbClass.Text + "'" +
                " and divisionLocation = '" + frmGameEntry.cbLocation.Text + "'" +
                " and divisionIdentifier = '" + frmGameEntry.tbIdentifier.Text + "'";

            ComboFill.FillBySql(cbAway, cmdSql, "id", "descr");
            ComboFill.FillBySql(cbHome, cmdSql, "id", "descr");
        }
    }
}
