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
            Utility.FarmingFunctions.FarmTeamPage(6243);
            Utility.FarmingFunctions.FarmMatchPage("http://stats.mmdl.org/index.php?view=match&matchid=35960");
        }
    }
}
