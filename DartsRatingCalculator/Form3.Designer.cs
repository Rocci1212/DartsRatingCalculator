namespace DartsRatingCalculator
{
    partial class Form3
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
            this.rbNewPlayer = new System.Windows.Forms.RadioButton();
            this.rbChooseExistingPlayer = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cbTeam = new System.Windows.Forms.ComboBox();
            this.tbPlayerId = new System.Windows.Forms.TextBox();
            this.cmdOk = new System.Windows.Forms.Button();
            this.tvExistingPlayers = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // rbNewPlayer
            // 
            this.rbNewPlayer.AutoSize = true;
            this.rbNewPlayer.Location = new System.Drawing.Point(12, 12);
            this.rbNewPlayer.Name = "rbNewPlayer";
            this.rbNewPlayer.Size = new System.Drawing.Size(113, 17);
            this.rbNewPlayer.TabIndex = 0;
            this.rbNewPlayer.TabStop = true;
            this.rbNewPlayer.Text = "Create New Player";
            this.rbNewPlayer.UseVisualStyleBackColor = true;
            // 
            // rbChooseExistingPlayer
            // 
            this.rbChooseExistingPlayer.AutoSize = true;
            this.rbChooseExistingPlayer.Enabled = false;
            this.rbChooseExistingPlayer.Location = new System.Drawing.Point(12, 61);
            this.rbChooseExistingPlayer.Name = "rbChooseExistingPlayer";
            this.rbChooseExistingPlayer.Size = new System.Drawing.Size(132, 17);
            this.rbChooseExistingPlayer.TabIndex = 0;
            this.rbChooseExistingPlayer.TabStop = true;
            this.rbChooseExistingPlayer.Text = "Choose Existing Player";
            this.rbChooseExistingPlayer.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(260, 20);
            this.textBox1.TabIndex = 1;
            // 
            // cbTeam
            // 
            this.cbTeam.Enabled = false;
            this.cbTeam.FormattingEnabled = true;
            this.cbTeam.Location = new System.Drawing.Point(12, 84);
            this.cbTeam.Name = "cbTeam";
            this.cbTeam.Size = new System.Drawing.Size(260, 21);
            this.cbTeam.TabIndex = 2;
            this.cbTeam.SelectedIndexChanged += new System.EventHandler(this.cbTeam_SelectedIndexChanged);
            // 
            // tbPlayerId
            // 
            this.tbPlayerId.Location = new System.Drawing.Point(172, 60);
            this.tbPlayerId.Name = "tbPlayerId";
            this.tbPlayerId.Size = new System.Drawing.Size(100, 20);
            this.tbPlayerId.TabIndex = 4;
            this.tbPlayerId.Text = "0";
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(197, 290);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(75, 23);
            this.cmdOk.TabIndex = 5;
            this.cmdOk.Text = "button1";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // tvExistingPlayers
            // 
            this.tvExistingPlayers.Enabled = false;
            this.tvExistingPlayers.Location = new System.Drawing.Point(12, 111);
            this.tvExistingPlayers.Name = "tvExistingPlayers";
            this.tvExistingPlayers.Size = new System.Drawing.Size(260, 173);
            this.tvExistingPlayers.TabIndex = 6;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 320);
            this.Controls.Add(this.tvExistingPlayers);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.tbPlayerId);
            this.Controls.Add(this.cbTeam);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.rbChooseExistingPlayer);
            this.Controls.Add(this.rbNewPlayer);
            this.Name = "Form3";
            this.Text = "Add New Player";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbNewPlayer;
        private System.Windows.Forms.RadioButton rbChooseExistingPlayer;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox cbTeam;
        private System.Windows.Forms.TextBox tbPlayerId;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.TreeView tvExistingPlayers;
    }
}