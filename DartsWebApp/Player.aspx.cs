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
    public partial class Player : Page
    {
        SqlConnection connSql = new SqlConnection(Properties.Settings.Default.ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            string playerId, realm, param;
            if (Request.QueryString["q"] == null)
            {
                playerId = "12164";
                realm = "all";
                param = "-1";
            }
            else if (Request.QueryString["q"].Split('_').Length == 1)
            {
                playerId = Request.QueryString["q"];
                realm = "all";
                param = "-1";
            }
            else if (Request.QueryString["q"].Split('_').Length == 2)
            {
                playerId = Request.QueryString["q"].Split('_')[0];
                realm = Request.QueryString["q"].Split('_')[1];
                param = "-1";
            }
            else
            {
                playerId = Request.QueryString["q"].Split('_')[0];
                realm = Request.QueryString["q"].Split('_')[1];
                param = Request.QueryString["q"].Split('_')[2];
            }
            
            connSql.Open();

            FillForm(Convert.ToInt32(playerId), realm, param);
        }

        private void FillForm(int playerId, string realm, string param)
        {
            

            SqlDataAdapter adpSql = new SqlDataAdapter("exec getplayergamehistory @player, @realm", connSql);
            adpSql.SelectCommand.Parameters.AddWithValue("@player", playerId);
            adpSql.SelectCommand.Parameters.AddWithValue("@realm", realm);
            adpSql.SelectCommand.Parameters.AddWithValue("@param1", param);
            DataTable dstdata = new DataTable("Data");
            adpSql.Fill(dstdata);

            grdPlayer.DataSource = dstdata;
            grdPlayer.DataBind();

            lblPlayer.Text = dstdata.Rows[0]["Name"].ToString();
            lblPlayer.NavigateUrl = String.Format(
                "{0:F0}", "http://stats.mmdl.org/index.php?view=player&playerid=" + dstdata.Rows[0]["Player"].ToString());
            lblRating.Text = "Rating: " + String.Format(
                "{0:f0}", (dstdata.Rows[dstdata.Rows.Count - 1]["PostRating"]));
        }
    }
}