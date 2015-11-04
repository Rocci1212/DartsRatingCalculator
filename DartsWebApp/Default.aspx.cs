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
    public partial class _Default : Page
    {
        SqlConnection connSql = new SqlConnection(Properties.Settings.Default.ConnectionString);
        SqlConnection connSql2 = new SqlConnection(Properties.Settings.Default.ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Redirect("~/Player.aspx?q=10059_class");
        }

        protected void GoButton_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adpSqlPlayer = new SqlDataAdapter("exec searchPlayers @searchTerm", connSql);
            adpSqlPlayer.SelectCommand.Parameters.AddWithValue("@searchTerm", SearchTerm.Text);

            DataTable dstPlayer = new DataTable("Data");
            adpSqlPlayer.Fill(dstPlayer);

            grdPlayerSearch.DataSource = dstPlayer;
            grdPlayerSearch.DataBind();

            SqlDataAdapter adpSqlTeams = new SqlDataAdapter("exec searchTeams @searchTerm", connSql);
            adpSqlTeams.SelectCommand.Parameters.AddWithValue("@searchTerm", SearchTerm.Text);

            DataTable dstTeam = new DataTable("Data");
            adpSqlTeams.Fill(dstTeam);

            grdTeamSearch.DataSource = dstTeam;
            grdTeamSearch.DataBind();
        }
    }
}