namespace Сheckers
{
    partial class StartWindow
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
            this.NewGame = new System.Windows.Forms.Button();
            this.ContinueGame = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.PlayAIGame = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.txtSaveFolder = new System.Windows.Forms.TextBox();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.BrowsPath = new System.Windows.Forms.FolderBrowserDialog();
            this.JSON = new System.Windows.Forms.CheckBox();
            this.XML = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // NewGame
            // 
            this.NewGame.Location = new System.Drawing.Point(77, 33);
            this.NewGame.Name = "NewGame";
            this.NewGame.Size = new System.Drawing.Size(365, 121);
            this.NewGame.TabIndex = 0;
            this.NewGame.Text = "New Game";
            this.NewGame.UseVisualStyleBackColor = true;
            this.NewGame.Click += new System.EventHandler(this.NewGameClick);
            // 
            // ContinueGame
            // 
            this.ContinueGame.Location = new System.Drawing.Point(184, 178);
            this.ContinueGame.Name = "ContinueGame";
            this.ContinueGame.Size = new System.Drawing.Size(506, 129);
            this.ContinueGame.TabIndex = 1;
            this.ContinueGame.Text = "Continue";
            this.ContinueGame.UseVisualStyleBackColor = true;
            this.ContinueGame.Click += new System.EventHandler(this.ContinueClick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(22, 178);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(141, 128);
            this.button3.TabIndex = 2;
            this.button3.Text = "Saves";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.SavesClick);
            // 
            // PlayAIGame
            // 
            this.PlayAIGame.Location = new System.Drawing.Point(448, 33);
            this.PlayAIGame.Name = "PlayAIGame";
            this.PlayAIGame.Size = new System.Drawing.Size(368, 121);
            this.PlayAIGame.TabIndex = 3;
            this.PlayAIGame.Text = "Play AI";
            this.PlayAIGame.UseVisualStyleBackColor = true;
            this.PlayAIGame.Click += new System.EventHandler(this.PlayWithAIClick);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(713, 178);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(140, 129);
            this.button5.TabIndex = 4;
            this.button5.Text = "Games History";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.GamesHistoryClick);
            // 
            // txtSaveFolder
            // 
            this.txtSaveFolder.Location = new System.Drawing.Point(184, 313);
            this.txtSaveFolder.Name = "txtSaveFolder";
            this.txtSaveFolder.Size = new System.Drawing.Size(506, 22);
            this.txtSaveFolder.TabIndex = 5;
            // 
            // btnBrowseFolder
            // 
            this.btnBrowseFolder.Location = new System.Drawing.Point(184, 342);
            this.btnBrowseFolder.Name = "btnBrowseFolder";
            this.btnBrowseFolder.Size = new System.Drawing.Size(149, 101);
            this.btnBrowseFolder.TabIndex = 6;
            this.btnBrowseFolder.Text = "Brows...";
            this.btnBrowseFolder.UseVisualStyleBackColor = true;
            this.btnBrowseFolder.Click += new System.EventHandler(this.btnBrowseFolder_Click);
            // 
            // JSON
            // 
            this.JSON.AutoSize = true;
            this.JSON.Checked = true;
            this.JSON.CheckState = System.Windows.Forms.CheckState.Checked;
            this.JSON.Location = new System.Drawing.Point(359, 383);
            this.JSON.Name = "JSON";
            this.JSON.Size = new System.Drawing.Size(65, 20);
            this.JSON.TabIndex = 7;
            this.JSON.Text = "JSON";
            this.JSON.UseVisualStyleBackColor = true;
            this.JSON.CheckedChanged += new System.EventHandler(this.JSON_CheckedChanged);
            // 
            // XML
            // 
            this.XML.AutoSize = true;
            this.XML.Location = new System.Drawing.Point(359, 420);
            this.XML.Name = "XML";
            this.XML.Size = new System.Drawing.Size(55, 20);
            this.XML.TabIndex = 8;
            this.XML.Text = "XML";
            this.XML.UseVisualStyleBackColor = true;
            this.XML.CheckedChanged += new System.EventHandler(this.XML_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(359, 364);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Choose How to Save your games";
            // 
            // StartWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 494);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.XML);
            this.Controls.Add(this.JSON);
            this.Controls.Add(this.btnBrowseFolder);
            this.Controls.Add(this.txtSaveFolder);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.PlayAIGame);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.ContinueGame);
            this.Controls.Add(this.NewGame);
            this.Name = "StartWindow";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button NewGame;
        private System.Windows.Forms.Button ContinueGame;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button PlayAIGame;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox txtSaveFolder;
        private System.Windows.Forms.Button btnBrowseFolder;
        private System.Windows.Forms.FolderBrowserDialog BrowsPath;
        private System.Windows.Forms.CheckBox JSON;
        private System.Windows.Forms.CheckBox XML;
        private System.Windows.Forms.Label label1;
    }
}