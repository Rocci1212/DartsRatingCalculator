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
using Moserware.Skills;

// this is legacy code
namespace DartsRatingCalculator
{
    public partial class Form3 : Form
    {
        Form1 frmGameEntry;
        int teamID;
        SqlConnection connSql = new SqlConnection(Properties.Settings.Default.ConnectionString);

        public Form3(Form1 frmGameEntry, int teamID)
        {
            InitializeComponent();
            this.frmGameEntry = frmGameEntry;
            this.teamID = teamID;
            connSql.Open();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            rbNewPlayer.Checked = true;
            ComboFill.FillBySql(cbTeam,
                "select id, TeamName + ' (' + Season + ' ' + cast(year as nvarchar(4)) + ')' as Descr from tblTeam order by season, year, TeamName", 
                "id", "descr");
        }

        private void cbTeam_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            string sSql; SqlCommand cmdSql; Rating defaultRating = GameInfo.DefaultGameInfo.DefaultRating;
            
            if (rbNewPlayer.Checked)
            {
                sSql = "insert into tblCompetitor(CompetitorName, RatingMean, RatingDeviation) values ('" +
                    textBox1.Text.Replace("'", "''") + "', " +
                    defaultRating.Mean.ToString("f5") + ", " + defaultRating.StandardDeviation.ToString("f5") + ")";
                cmdSql = new SqlCommand(sSql, connSql); cmdSql.ExecuteNonQuery();

                sSql = "insert into tblTeamCompetitor(TeamID, CompetitorID) select top 1 " +
                    Convert.ToString(teamID) + ", @@IDENTITY from tblCompetitor";
                cmdSql = new SqlCommand(sSql, connSql); cmdSql.ExecuteNonQuery();
            }

            ComboFill.FillPlayer(frmGameEntry.cbCompetitorA1, frmGameEntry.tbAwayTeamID.Text);
            ComboFill.FillPlayer(frmGameEntry.cbCompetitorA2, frmGameEntry.tbAwayTeamID.Text);
            ComboFill.FillPlayer(frmGameEntry.cbCompetitorH1, frmGameEntry.tbHomeTeamID.Text);
            ComboFill.FillPlayer(frmGameEntry.cbCompetitorH2, frmGameEntry.tbHomeTeamID.Text);
            ComboFill.FillPlayer(frmGameEntry.cbCompetitorA3, frmGameEntry.tbAwayTeamID.Text);
            ComboFill.FillPlayer(frmGameEntry.cbCompetitorH3, frmGameEntry.tbHomeTeamID.Text);

            Close();
        }
    }
}
