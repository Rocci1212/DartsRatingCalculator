using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Team : Page
    {
        string teamId;

        SqlConnection connSql = new SqlConnection(Properties.Settings.Default.ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["q"] == null)
            {
                teamId = "6246";
            }
            else if (Request.QueryString["q"].Split('_').Length == 1)
            {
                teamId = Request.QueryString["q"];
            }
            else if (Request.QueryString["q"].Split('_').Length == 2)
            {
                teamId = Request.QueryString["q"].Split('_')[0];
            }
            else
            {
                teamId = Request.QueryString["q"].Split('_')[0];
            }
            
            connSql.Open();

            FillForm(Convert.ToInt32(teamId));
        }

        private void FillForm(int teamId)
        {
            

            SqlDataAdapter adpSql = new SqlDataAdapter("exec getSquadPlayers @team", connSql);
            adpSql.SelectCommand.Parameters.AddWithValue("@team", teamId);
            DataTable dstdata = new DataTable("Data");
            adpSql.Fill(dstdata);

            grdPlayer.DataSource = dstdata;
            grdPlayer.DataBind();

            if (grdPlayer.Rows.Count > 0)
            {
                lblTeam.Text = dstdata.Rows[0]["SquadName"].ToString();
                lblTeam.NavigateUrl = String.Format(
                    "{0:F0}", "http://stats.mmdl.org/index.php?view=team&teamid=" + teamId);
            }
        }
    }
}