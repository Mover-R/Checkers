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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.txtSaveFolder = new System.Windows.Forms.TextBox();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.BrowsPath = new System.Windows.Forms.FolderBrowserDialog();
            this.JSON = new System.Windows.Forms.CheckBox();
            this.XML = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(77, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(365, 121);
            this.button1.TabIndex = 0;
            this.button1.Text = "New Game";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.NewGameClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(184, 178);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(506, 129);
            this.button2.TabIndex = 1;
            this.button2.Text = "Continue";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.ContinueClick);
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
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(448, 33);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(368, 121);
            this.button4.TabIndex = 3;
            this.button4.Text = "Play AI";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.PlayWithAIClick);
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
            this.btnBrowseFolder.Location = new System.Drawing.Point(184, 357);
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
            this.JSON.Location = new System.Drawing.Point(359, 398);
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
            this.XML.Location = new System.Drawing.Point(359, 438);
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
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "StartWindow";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox txtSaveFolder;
        private System.Windows.Forms.Button btnBrowseFolder;
        private System.Windows.Forms.FolderBrowserDialog BrowsPath;
        private System.Windows.Forms.CheckBox JSON;
        private System.Windows.Forms.CheckBox XML;
        private System.Windows.Forms.Label label1;
    }
}