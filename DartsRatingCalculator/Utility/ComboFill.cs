using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DartsRatingCalculator
{
    public static class ComboFill
    {
        static SqlConnection connSql = new SqlConnection(Properties.Settings.Default.ConnectionString);

        // Function that fills the combo based on Sql Query
        public static void FillBySql(ComboBox cbCombo, string sSql, string sCodeField, string sDescField)
        {
            if (connSql.State != System.Data.ConnectionState.Open)
                connSql.Open();

            string sValue = "";

            // get the current value
            if (cbCombo.SelectedIndex != -1 && cbCombo.Tag != null)
            {
                List<string> oldComboTag = (List<string>)cbCombo.Tag;
                sValue = oldComboTag[cbCombo.SelectedIndex];
            }   

            // clear the list
            cbCombo.Items.Clear();
            List<string> comboTag = new List<string>();

            // add blank
            cbCombo.Items.Add("");
            comboTag.Add("");

            // setup the command
            SqlCommand cmdSql = new SqlCommand(sSql, connSql);

            // execute the command
            SqlDataReader rReader = cmdSql.ExecuteReader();

            // go through the list
            while (rReader.Read())
            {
                comboTag.Add(Convert.ToString(rReader[sCodeField]));
                cbCombo.Items.Add((string)rReader[sDescField]);
            }

            // clean up
            rReader.Close();

            // set the codes
            cbCombo.Tag = comboTag;
            cbCombo.SelectedIndex = comboTag.IndexOf(sValue);
        }

        // Function that fills the combo based on Sql Query
        public static void FillPlayer(ComboBox cbCombo, string sTeamId)
        {
            string sSql = 
                "select CompetitorID, CompetitorName from tblTeamCompetitor x inner join tblCompetitor y on x.CompetitorID = y.ID where TeamID = " + sTeamId + 
                " order by CompetitorName";
            ComboFill.FillBySql(cbCombo, sSql, "CompetitorID", "CompetitorName");

            cbCombo.Items.Add("<Add Team Member...>");
            ((List<string>)cbCombo.Tag).Add("");

            // set the codes
            //cbCombo.Tag = comboTag;
        }
    }
}
