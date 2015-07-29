namespace DartsRatingCalculator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmdPost = new System.Windows.Forms.Button();
            this.nmYear = new System.Windows.Forms.NumericUpDown();
            this.cbSeason = new System.Windows.Forms.ComboBox();
            this.cbLocation = new System.Windows.Forms.ComboBox();
            this.tbIdentifier = new System.Windows.Forms.TextBox();
            this.cbClass = new System.Windows.Forms.ComboBox();
            this.grpAway = new System.Windows.Forms.GroupBox();
            this.tbAwayTeamID = new System.Windows.Forms.TextBox();
            this.tbPostDeviationA3 = new System.Windows.Forms.TextBox();
            this.tbPreDeviationA3 = new System.Windows.Forms.TextBox();
            this.tbPostDeviationA2 = new System.Windows.Forms.TextBox();
            this.tbPostMeanA3 = new System.Windows.Forms.TextBox();
            this.tbPreDeviationA2 = new System.Windows.Forms.TextBox();
            this.tbPreMeanA3 = new System.Windows.Forms.TextBox();
            this.tbPostMeanA2 = new System.Windows.Forms.TextBox();
            this.tbPreMeanA2 = new System.Windows.Forms.TextBox();
            this.cbCompetitorA3 = new System.Windows.Forms.ComboBox();
            this.cbCompetitorA2 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbPostDeviationA1 = new System.Windows.Forms.TextBox();
            this.tbPreDeviationA1 = new System.Windows.Forms.TextBox();
            this.tbPostMeanA1 = new System.Windows.Forms.TextBox();
            this.tbPreMeanA1 = new System.Windows.Forms.TextBox();
            this.cbCompetitorA1 = new System.Windows.Forms.ComboBox();
            this.grpHome = new System.Windows.Forms.GroupBox();
            this.tbHomeTeamID = new System.Windows.Forms.TextBox();
            this.tbPostDeviationH3 = new System.Windows.Forms.TextBox();
            this.tbPostDeviationH2 = new System.Windows.Forms.TextBox();
            this.cbCompetitorH1 = new System.Windows.Forms.ComboBox();
            this.tbPreDeviationH3 = new System.Windows.Forms.TextBox();
            this.tbPreDeviationH2 = new System.Windows.Forms.TextBox();
            this.tbPreMeanH1 = new System.Windows.Forms.TextBox();
            this.tbPostMeanH3 = new System.Windows.Forms.TextBox();
            this.tbPostMeanH2 = new System.Windows.Forms.TextBox();
            this.tbPostMeanH1 = new System.Windows.Forms.TextBox();
            this.tbPreMeanH3 = new System.Windows.Forms.TextBox();
            this.tbPreMeanH2 = new System.Windows.Forms.TextBox();
            this.tbPreDeviationH1 = new System.Windows.Forms.TextBox();
            this.cbCompetitorH3 = new System.Windows.Forms.ComboBox();
            this.cbCompetitorH2 = new System.Windows.Forms.ComboBox();
            this.tbPostDeviationH1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdNewMatch = new System.Windows.Forms.Button();
            this.cbGameType = new System.Windows.Forms.ComboBox();
            this.tbMatchId = new System.Windows.Forms.TextBox();
            this.rbAway = new System.Windows.Forms.RadioButton();
            this.rbHome = new System.Windows.Forms.RadioButton();
            this.grpWinner = new System.Windows.Forms.GroupBox();
            this.chkInvalidGame = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nmYear)).BeginInit();
            this.grpAway.SuspendLayout();
            this.grpHome.SuspendLayout();
            this.grpWinner.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdPost
            // 
            this.cmdPost.Location = new System.Drawing.Point(731, 227);
            this.cmdPost.Name = "cmdPost";
            this.cmdPost.Size = new System.Drawing.Size(75, 23);
            this.cmdPost.TabIndex = 0;
            this.cmdPost.Text = "Post";
            this.cmdPost.UseVisualStyleBackColor = true;
            this.cmdPost.Click += new System.EventHandler(this.cmdPost_Click);
            // 
            // nmYear
            // 
            this.nmYear.Location = new System.Drawing.Point(12, 12);
            this.nmYear.Maximum = new decimal(new int[] {
            2015,
            0,
            0,
            0});
            this.nmYear.Minimum = new decimal(new int[] {
            2009,
            0,
            0,
            0});
            this.nmYear.Name = "nmYear";
            this.nmYear.Size = new System.Drawing.Size(56, 20);
            this.nmYear.TabIndex = 1;
            this.nmYear.Value = new decimal(new int[] {
            2009,
            0,
            0,
            0});
            // 
            // cbSeason
            // 
            this.cbSeason.FormattingEnabled = true;
            this.cbSeason.Items.AddRange(new object[] {
            "",
            "Fall",
            "Spring"});
            this.cbSeason.Location = new System.Drawing.Point(74, 11);
            this.cbSeason.Name = "cbSeason";
            this.cbSeason.Size = new System.Drawing.Size(55, 21);
            this.cbSeason.TabIndex = 2;
            this.cbSeason.Text = "Fall";
            // 
            // cbLocation
            // 
            this.cbLocation.FormattingEnabled = true;
            this.cbLocation.Items.AddRange(new object[] {
            "",
            "Boston",
            "Central",
            "North Shore",
            "South Shore"});
            this.cbLocation.Location = new System.Drawing.Point(196, 11);
            this.cbLocation.Name = "cbLocation";
            this.cbLocation.Size = new System.Drawing.Size(121, 21);
            this.cbLocation.TabIndex = 4;
            this.cbLocation.Text = "Boston";
            // 
            // tbIdentifier
            // 
            this.tbIdentifier.Location = new System.Drawing.Point(323, 11);
            this.tbIdentifier.Name = "tbIdentifier";
            this.tbIdentifier.Size = new System.Drawing.Size(32, 20);
            this.tbIdentifier.TabIndex = 5;
            this.tbIdentifier.Text = "1";
            // 
            // cbClass
            // 
            this.cbClass.FormattingEnabled = true;
            this.cbClass.Items.AddRange(new object[] {
            "",
            "S",
            "A",
            "B",
            "C",
            "D",
            "E"});
            this.cbClass.Location = new System.Drawing.Point(135, 11);
            this.cbClass.Name = "cbClass";
            this.cbClass.Size = new System.Drawing.Size(55, 21);
            this.cbClass.TabIndex = 6;
            this.cbClass.Text = "C";
            // 
            // grpAway
            // 
            this.grpAway.Controls.Add(this.tbAwayTeamID);
            this.grpAway.Controls.Add(this.tbPostDeviationA3);
            this.grpAway.Controls.Add(this.tbPreDeviationA3);
            this.grpAway.Controls.Add(this.tbPostDeviationA2);
            this.grpAway.Controls.Add(this.tbPostMeanA3);
            this.grpAway.Controls.Add(this.tbPreDeviationA2);
            this.grpAway.Controls.Add(this.tbPreMeanA3);
            this.grpAway.Controls.Add(this.tbPostMeanA2);
            this.grpAway.Controls.Add(this.tbPreMeanA2);
            this.grpAway.Controls.Add(this.cbCompetitorA3);
            this.grpAway.Controls.Add(this.cbCompetitorA2);
            this.grpAway.Controls.Add(this.label3);
            this.grpAway.Controls.Add(this.label2);
            this.grpAway.Controls.Add(this.label1);
            this.grpAway.Controls.Add(this.tbPostDeviationA1);
            this.grpAway.Controls.Add(this.tbPreDeviationA1);
            this.grpAway.Controls.Add(this.tbPostMeanA1);
            this.grpAway.Controls.Add(this.tbPreMeanA1);
            this.grpAway.Controls.Add(this.cbCompetitorA1);
            this.grpAway.Location = new System.Drawing.Point(12, 51);
            this.grpAway.Name = "grpAway";
            this.grpAway.Size = new System.Drawing.Size(391, 141);
            this.grpAway.TabIndex = 7;
            this.grpAway.TabStop = false;
            this.grpAway.Text = "Away";
            // 
            // tbAwayTeamID
            // 
            this.tbAwayTeamID.Location = new System.Drawing.Point(69, 19);
            this.tbAwayTeamID.Name = "tbAwayTeamID";
            this.tbAwayTeamID.Size = new System.Drawing.Size(24, 20);
            this.tbAwayTeamID.TabIndex = 16;
            // 
            // tbPostDeviationA3
            // 
            this.tbPostDeviationA3.Location = new System.Drawing.Point(337, 99);
            this.tbPostDeviationA3.Name = "tbPostDeviationA3";
            this.tbPostDeviationA3.Size = new System.Drawing.Size(45, 20);
            this.tbPostDeviationA3.TabIndex = 15;
            // 
            // tbPreDeviationA3
            // 
            this.tbPreDeviationA3.Location = new System.Drawing.Point(225, 99);
            this.tbPreDeviationA3.Name = "tbPreDeviationA3";
            this.tbPreDeviationA3.Size = new System.Drawing.Size(45, 20);
            this.tbPreDeviationA3.TabIndex = 14;
            // 
            // tbPostDeviationA2
            // 
            this.tbPostDeviationA2.Location = new System.Drawing.Point(337, 72);
            this.tbPostDeviationA2.Name = "tbPostDeviationA2";
            this.tbPostDeviationA2.Size = new System.Drawing.Size(45, 20);
            this.tbPostDeviationA2.TabIndex = 15;
            // 
            // tbPostMeanA3
            // 
            this.tbPostMeanA3.Location = new System.Drawing.Point(276, 99);
            this.tbPostMeanA3.Name = "tbPostMeanA3";
            this.tbPostMeanA3.Size = new System.Drawing.Size(55, 20);
            this.tbPostMeanA3.TabIndex = 13;
            // 
            // tbPreDeviationA2
            // 
            this.tbPreDeviationA2.Location = new System.Drawing.Point(225, 72);
            this.tbPreDeviationA2.Name = "tbPreDeviationA2";
            this.tbPreDeviationA2.Size = new System.Drawing.Size(45, 20);
            this.tbPreDeviationA2.TabIndex = 14;
            // 
            // tbPreMeanA3
            // 
            this.tbPreMeanA3.Location = new System.Drawing.Point(164, 99);
            this.tbPreMeanA3.Name = "tbPreMeanA3";
            this.tbPreMeanA3.Size = new System.Drawing.Size(55, 20);
            this.tbPreMeanA3.TabIndex = 12;
            // 
            // tbPostMeanA2
            // 
            this.tbPostMeanA2.Location = new System.Drawing.Point(276, 72);
            this.tbPostMeanA2.Name = "tbPostMeanA2";
            this.tbPostMeanA2.Size = new System.Drawing.Size(55, 20);
            this.tbPostMeanA2.TabIndex = 13;
            // 
            // tbPreMeanA2
            // 
            this.tbPreMeanA2.Location = new System.Drawing.Point(164, 72);
            this.tbPreMeanA2.Name = "tbPreMeanA2";
            this.tbPreMeanA2.Size = new System.Drawing.Size(55, 20);
            this.tbPreMeanA2.TabIndex = 12;
            // 
            // cbCompetitorA3
            // 
            this.cbCompetitorA3.FormattingEnabled = true;
            this.cbCompetitorA3.Location = new System.Drawing.Point(6, 99);
            this.cbCompetitorA3.Name = "cbCompetitorA3";
            this.cbCompetitorA3.Size = new System.Drawing.Size(152, 21);
            this.cbCompetitorA3.TabIndex = 11;
            this.cbCompetitorA3.SelectedIndexChanged += new System.EventHandler(this.cbCompetitor_SelectedIndexChanged);
            // 
            // cbCompetitorA2
            // 
            this.cbCompetitorA2.FormattingEnabled = true;
            this.cbCompetitorA2.Location = new System.Drawing.Point(6, 72);
            this.cbCompetitorA2.Name = "cbCompetitorA2";
            this.cbCompetitorA2.Size = new System.Drawing.Size(152, 21);
            this.cbCompetitorA2.TabIndex = 11;
            this.cbCompetitorA2.SelectedIndexChanged += new System.EventHandler(this.cbCompetitor_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(273, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Post-Match Rating";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Pre-Match Rating";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Competitor";
            // 
            // tbPostDeviationA1
            // 
            this.tbPostDeviationA1.Location = new System.Drawing.Point(337, 45);
            this.tbPostDeviationA1.Name = "tbPostDeviationA1";
            this.tbPostDeviationA1.Size = new System.Drawing.Size(45, 20);
            this.tbPostDeviationA1.TabIndex = 8;
            // 
            // tbPreDeviationA1
            // 
            this.tbPreDeviationA1.Location = new System.Drawing.Point(225, 45);
            this.tbPreDeviationA1.Name = "tbPreDeviationA1";
            this.tbPreDeviationA1.Size = new System.Drawing.Size(45, 20);
            this.tbPreDeviationA1.TabIndex = 7;
            // 
            // tbPostMeanA1
            // 
            this.tbPostMeanA1.Location = new System.Drawing.Point(276, 45);
            this.tbPostMeanA1.Name = "tbPostMeanA1";
            this.tbPostMeanA1.Size = new System.Drawing.Size(55, 20);
            this.tbPostMeanA1.TabIndex = 6;
            // 
            // tbPreMeanA1
            // 
            this.tbPreMeanA1.Location = new System.Drawing.Point(164, 45);
            this.tbPreMeanA1.Name = "tbPreMeanA1";
            this.tbPreMeanA1.Size = new System.Drawing.Size(55, 20);
            this.tbPreMeanA1.TabIndex = 4;
            // 
            // cbCompetitorA1
            // 
            this.cbCompetitorA1.FormattingEnabled = true;
            this.cbCompetitorA1.Location = new System.Drawing.Point(6, 45);
            this.cbCompetitorA1.Name = "cbCompetitorA1";
            this.cbCompetitorA1.Size = new System.Drawing.Size(152, 21);
            this.cbCompetitorA1.TabIndex = 0;
            this.cbCompetitorA1.SelectedIndexChanged += new System.EventHandler(this.cbCompetitor_SelectedIndexChanged);
            // 
            // grpHome
            // 
            this.grpHome.Controls.Add(this.tbHomeTeamID);
            this.grpHome.Controls.Add(this.tbPostDeviationH3);
            this.grpHome.Controls.Add(this.tbPostDeviationH2);
            this.grpHome.Controls.Add(this.cbCompetitorH1);
            this.grpHome.Controls.Add(this.tbPreDeviationH3);
            this.grpHome.Controls.Add(this.tbPreDeviationH2);
            this.grpHome.Controls.Add(this.tbPreMeanH1);
            this.grpHome.Controls.Add(this.tbPostMeanH3);
            this.grpHome.Controls.Add(this.tbPostMeanH2);
            this.grpHome.Controls.Add(this.tbPostMeanH1);
            this.grpHome.Controls.Add(this.tbPreMeanH3);
            this.grpHome.Controls.Add(this.tbPreMeanH2);
            this.grpHome.Controls.Add(this.tbPreDeviationH1);
            this.grpHome.Controls.Add(this.cbCompetitorH3);
            this.grpHome.Controls.Add(this.cbCompetitorH2);
            this.grpHome.Controls.Add(this.tbPostDeviationH1);
            this.grpHome.Controls.Add(this.label4);
            this.grpHome.Controls.Add(this.label6);
            this.grpHome.Controls.Add(this.label5);
            this.grpHome.Location = new System.Drawing.Point(409, 51);
            this.grpHome.Name = "grpHome";
            this.grpHome.Size = new System.Drawing.Size(391, 141);
            this.grpHome.TabIndex = 8;
            this.grpHome.TabStop = false;
            this.grpHome.Text = "Home";
            // 
            // tbHomeTeamID
            // 
            this.tbHomeTeamID.Location = new System.Drawing.Point(69, 19);
            this.tbHomeTeamID.Name = "tbHomeTeamID";
            this.tbHomeTeamID.Size = new System.Drawing.Size(24, 20);
            this.tbHomeTeamID.TabIndex = 16;
            // 
            // tbPostDeviationH3
            // 
            this.tbPostDeviationH3.Location = new System.Drawing.Point(337, 99);
            this.tbPostDeviationH3.Name = "tbPostDeviationH3";
            this.tbPostDeviationH3.Size = new System.Drawing.Size(45, 20);
            this.tbPostDeviationH3.TabIndex = 28;
            // 
            // tbPostDeviationH2
            // 
            this.tbPostDeviationH2.Location = new System.Drawing.Point(337, 72);
            this.tbPostDeviationH2.Name = "tbPostDeviationH2";
            this.tbPostDeviationH2.Size = new System.Drawing.Size(45, 20);
            this.tbPostDeviationH2.TabIndex = 28;
            // 
            // cbCompetitorH1
            // 
            this.cbCompetitorH1.FormattingEnabled = true;
            this.cbCompetitorH1.Location = new System.Drawing.Point(6, 45);
            this.cbCompetitorH1.Name = "cbCompetitorH1";
            this.cbCompetitorH1.Size = new System.Drawing.Size(152, 21);
            this.cbCompetitorH1.TabIndex = 16;
            this.cbCompetitorH1.SelectedIndexChanged += new System.EventHandler(this.cbCompetitor_SelectedIndexChanged);
            // 
            // tbPreDeviationH3
            // 
            this.tbPreDeviationH3.Location = new System.Drawing.Point(225, 99);
            this.tbPreDeviationH3.Name = "tbPreDeviationH3";
            this.tbPreDeviationH3.Size = new System.Drawing.Size(46, 20);
            this.tbPreDeviationH3.TabIndex = 27;
            // 
            // tbPreDeviationH2
            // 
            this.tbPreDeviationH2.Location = new System.Drawing.Point(225, 72);
            this.tbPreDeviationH2.Name = "tbPreDeviationH2";
            this.tbPreDeviationH2.Size = new System.Drawing.Size(46, 20);
            this.tbPreDeviationH2.TabIndex = 27;
            // 
            // tbPreMeanH1
            // 
            this.tbPreMeanH1.Location = new System.Drawing.Point(164, 45);
            this.tbPreMeanH1.Name = "tbPreMeanH1";
            this.tbPreMeanH1.Size = new System.Drawing.Size(55, 20);
            this.tbPreMeanH1.TabIndex = 17;
            // 
            // tbPostMeanH3
            // 
            this.tbPostMeanH3.Location = new System.Drawing.Point(276, 99);
            this.tbPostMeanH3.Name = "tbPostMeanH3";
            this.tbPostMeanH3.Size = new System.Drawing.Size(55, 20);
            this.tbPostMeanH3.TabIndex = 26;
            // 
            // tbPostMeanH2
            // 
            this.tbPostMeanH2.Location = new System.Drawing.Point(276, 72);
            this.tbPostMeanH2.Name = "tbPostMeanH2";
            this.tbPostMeanH2.Size = new System.Drawing.Size(55, 20);
            this.tbPostMeanH2.TabIndex = 26;
            // 
            // tbPostMeanH1
            // 
            this.tbPostMeanH1.Location = new System.Drawing.Point(276, 45);
            this.tbPostMeanH1.Name = "tbPostMeanH1";
            this.tbPostMeanH1.Size = new System.Drawing.Size(55, 20);
            this.tbPostMeanH1.TabIndex = 18;
            // 
            // tbPreMeanH3
            // 
            this.tbPreMeanH3.Location = new System.Drawing.Point(164, 99);
            this.tbPreMeanH3.Name = "tbPreMeanH3";
            this.tbPreMeanH3.Size = new System.Drawing.Size(55, 20);
            this.tbPreMeanH3.TabIndex = 25;
            // 
            // tbPreMeanH2
            // 
            this.tbPreMeanH2.Location = new System.Drawing.Point(164, 72);
            this.tbPreMeanH2.Name = "tbPreMeanH2";
            this.tbPreMeanH2.Size = new System.Drawing.Size(55, 20);
            this.tbPreMeanH2.TabIndex = 25;
            // 
            // tbPreDeviationH1
            // 
            this.tbPreDeviationH1.Location = new System.Drawing.Point(225, 45);
            this.tbPreDeviationH1.Name = "tbPreDeviationH1";
            this.tbPreDeviationH1.Size = new System.Drawing.Size(45, 20);
            this.tbPreDeviationH1.TabIndex = 19;
            // 
            // cbCompetitorH3
            // 
            this.cbCompetitorH3.FormattingEnabled = true;
            this.cbCompetitorH3.Location = new System.Drawing.Point(6, 99);
            this.cbCompetitorH3.Name = "cbCompetitorH3";
            this.cbCompetitorH3.Size = new System.Drawing.Size(152, 21);
            this.cbCompetitorH3.TabIndex = 24;
            this.cbCompetitorH3.SelectedIndexChanged += new System.EventHandler(this.cbCompetitor_SelectedIndexChanged);
            // 
            // cbCompetitorH2
            // 
            this.cbCompetitorH2.FormattingEnabled = true;
            this.cbCompetitorH2.Location = new System.Drawing.Point(6, 72);
            this.cbCompetitorH2.Name = "cbCompetitorH2";
            this.cbCompetitorH2.Size = new System.Drawing.Size(152, 21);
            this.cbCompetitorH2.TabIndex = 24;
            this.cbCompetitorH2.SelectedIndexChanged += new System.EventHandler(this.cbCompetitor_SelectedIndexChanged);
            // 
            // tbPostDeviationH1
            // 
            this.tbPostDeviationH1.Location = new System.Drawing.Point(337, 45);
            this.tbPostDeviationH1.Name = "tbPostDeviationH1";
            this.tbPostDeviationH1.Size = new System.Drawing.Size(45, 20);
            this.tbPostDeviationH1.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(273, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Post-Match Rating";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Competitor";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(161, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Pre-Match Rating";
            // 
            // cmdNewMatch
            // 
            this.cmdNewMatch.Location = new System.Drawing.Point(650, 227);
            this.cmdNewMatch.Name = "cmdNewMatch";
            this.cmdNewMatch.Size = new System.Drawing.Size(75, 23);
            this.cmdNewMatch.TabIndex = 9;
            this.cmdNewMatch.Text = "New Match";
            this.cmdNewMatch.UseVisualStyleBackColor = true;
            this.cmdNewMatch.Click += new System.EventHandler(this.cmdNewMatch_Click);
            // 
            // cbGameType
            // 
            this.cbGameType.FormattingEnabled = true;
            this.cbGameType.Items.AddRange(new object[] {
            "301 Singles",
            "301 Doubles",
            "501 Singles",
            "501 Doubles",
            "601 Triples",
            "Cricket Singles",
            "Cricket Doubles"});
            this.cbGameType.Location = new System.Drawing.Point(447, 10);
            this.cbGameType.Name = "cbGameType";
            this.cbGameType.Size = new System.Drawing.Size(121, 21);
            this.cbGameType.TabIndex = 10;
            this.cbGameType.SelectedIndexChanged += new System.EventHandler(this.cbGameType_SelectedIndexChanged);
            // 
            // tbMatchId
            // 
            this.tbMatchId.Location = new System.Drawing.Point(768, 10);
            this.tbMatchId.Name = "tbMatchId";
            this.tbMatchId.Size = new System.Drawing.Size(32, 20);
            this.tbMatchId.TabIndex = 5;
            // 
            // rbAway
            // 
            this.rbAway.AutoSize = true;
            this.rbAway.Location = new System.Drawing.Point(6, 19);
            this.rbAway.Name = "rbAway";
            this.rbAway.Size = new System.Drawing.Size(51, 17);
            this.rbAway.TabIndex = 13;
            this.rbAway.TabStop = true;
            this.rbAway.Text = "Away";
            this.rbAway.UseVisualStyleBackColor = true;
            this.rbAway.CheckedChanged += new System.EventHandler(this.rbResult_CheckedChanged);
            // 
            // rbHome
            // 
            this.rbHome.AutoSize = true;
            this.rbHome.Location = new System.Drawing.Point(97, 19);
            this.rbHome.Name = "rbHome";
            this.rbHome.Size = new System.Drawing.Size(53, 17);
            this.rbHome.TabIndex = 14;
            this.rbHome.TabStop = true;
            this.rbHome.Text = "Home";
            this.rbHome.UseVisualStyleBackColor = true;
            this.rbHome.CheckedChanged += new System.EventHandler(this.rbResult_CheckedChanged);
            // 
            // grpWinner
            // 
            this.grpWinner.Controls.Add(this.rbAway);
            this.grpWinner.Controls.Add(this.rbHome);
            this.grpWinner.Location = new System.Drawing.Point(18, 198);
            this.grpWinner.Name = "grpWinner";
            this.grpWinner.Size = new System.Drawing.Size(200, 52);
            this.grpWinner.TabIndex = 15;
            this.grpWinner.TabStop = false;
            this.grpWinner.Text = "Winner";
            // 
            // chkInvalidGame
            // 
            this.chkInvalidGame.AutoSize = true;
            this.chkInvalidGame.Location = new System.Drawing.Point(589, 11);
            this.chkInvalidGame.Name = "chkInvalidGame";
            this.chkInvalidGame.Size = new System.Drawing.Size(88, 17);
            this.chkInvalidGame.TabIndex = 16;
            this.chkInvalidGame.Text = "Invalid Game";
            this.chkInvalidGame.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(395, 214);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 17;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 262);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.chkInvalidGame);
            this.Controls.Add(this.grpWinner);
            this.Controls.Add(this.cbGameType);
            this.Controls.Add(this.cmdNewMatch);
            this.Controls.Add(this.grpHome);
            this.Controls.Add(this.grpAway);
            this.Controls.Add(this.cbClass);
            this.Controls.Add(this.tbMatchId);
            this.Controls.Add(this.tbIdentifier);
            this.Controls.Add(this.cbLocation);
            this.Controls.Add(this.cbSeason);
            this.Controls.Add(this.nmYear);
            this.Controls.Add(this.cmdPost);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.nmYear)).EndInit();
            this.grpAway.ResumeLayout(false);
            this.grpAway.PerformLayout();
            this.grpHome.ResumeLayout(false);
            this.grpHome.PerformLayout();
            this.grpWinner.ResumeLayout(false);
            this.grpWinner.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdPost;
        private System.Windows.Forms.GroupBox grpAway;
        private System.Windows.Forms.GroupBox grpHome;
        private System.Windows.Forms.Button cmdNewMatch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.NumericUpDown nmYear;
        public System.Windows.Forms.ComboBox cbSeason;
        public System.Windows.Forms.ComboBox cbLocation;
        public System.Windows.Forms.TextBox tbIdentifier;
        public System.Windows.Forms.ComboBox cbClass;
        public System.Windows.Forms.TextBox tbPostDeviationA2;
        public System.Windows.Forms.TextBox tbPreDeviationA2;
        public System.Windows.Forms.TextBox tbPostMeanA2;
        public System.Windows.Forms.TextBox tbPreMeanA2;
        public System.Windows.Forms.ComboBox cbCompetitorA2;
        public System.Windows.Forms.TextBox tbPostDeviationA1;
        public System.Windows.Forms.TextBox tbPreDeviationA1;
        public System.Windows.Forms.TextBox tbPostMeanA1;
        public System.Windows.Forms.TextBox tbPreMeanA1;
        public System.Windows.Forms.ComboBox cbCompetitorA1;
        public System.Windows.Forms.TextBox tbPostDeviationH2;
        public System.Windows.Forms.ComboBox cbCompetitorH1;
        public System.Windows.Forms.TextBox tbPreDeviationH2;
        public System.Windows.Forms.TextBox tbPreMeanH1;
        public System.Windows.Forms.TextBox tbPostMeanH2;
        public System.Windows.Forms.TextBox tbPostMeanH1;
        public System.Windows.Forms.TextBox tbPreMeanH2;
        public System.Windows.Forms.TextBox tbPreDeviationH1;
        public System.Windows.Forms.ComboBox cbCompetitorH2;
        public System.Windows.Forms.TextBox tbPostDeviationH1;
        public System.Windows.Forms.ComboBox cbGameType;
        public System.Windows.Forms.TextBox tbAwayTeamID;
        public System.Windows.Forms.TextBox tbHomeTeamID;
        public System.Windows.Forms.TextBox tbMatchId;
        private System.Windows.Forms.RadioButton rbAway;
        private System.Windows.Forms.RadioButton rbHome;
        private System.Windows.Forms.GroupBox grpWinner;
        public System.Windows.Forms.TextBox tbPostDeviationA3;
        public System.Windows.Forms.TextBox tbPreDeviationA3;
        public System.Windows.Forms.TextBox tbPostMeanA3;
        public System.Windows.Forms.TextBox tbPreMeanA3;
        public System.Windows.Forms.ComboBox cbCompetitorA3;
        public System.Windows.Forms.TextBox tbPostDeviationH3;
        public System.Windows.Forms.TextBox tbPreDeviationH3;
        public System.Windows.Forms.TextBox tbPostMeanH3;
        public System.Windows.Forms.TextBox tbPreMeanH3;
        public System.Windows.Forms.ComboBox cbCompetitorH3;
        private System.Windows.Forms.CheckBox chkInvalidGame;
        private System.Windows.Forms.TextBox textBox1;
    }
}

