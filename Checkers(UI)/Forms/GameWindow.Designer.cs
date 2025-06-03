namespace Сheckers
{
    partial class GameWindow
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.OfferDrawBlack = new System.Windows.Forms.Button();
            this.OfferDrawWhite = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(736, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(204, 60);
            this.button1.TabIndex = 0;
            this.button1.Text = "To Main Menu";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ToMainMenuClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(736, 111);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(203, 67);
            this.button2.TabIndex = 1;
            this.button2.Text = "Save Progress";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.SaveProgresClick);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(736, 195);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(77, 69);
            this.button3.TabIndex = 2;
            this.button3.Text = "Forward";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.ForwardClick);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(736, 294);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(77, 69);
            this.button4.TabIndex = 3;
            this.button4.Text = "Back";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.BackClick);
            // 
            // OfferDrawBlack
            // 
            this.OfferDrawBlack.Location = new System.Drawing.Point(576, 26);
            this.OfferDrawBlack.Name = "OfferDrawBlack";
            this.OfferDrawBlack.Size = new System.Drawing.Size(142, 89);
            this.OfferDrawBlack.TabIndex = 4;
            this.OfferDrawBlack.Text = "OfferDrawBlack";
            this.OfferDrawBlack.UseVisualStyleBackColor = true;
            this.OfferDrawBlack.Click += new System.EventHandler(this.OfferDrawBlack_Click);
            // 
            // OfferDrawWhite
            // 
            this.OfferDrawWhite.Location = new System.Drawing.Point(576, 394);
            this.OfferDrawWhite.Name = "OfferDrawWhite";
            this.OfferDrawWhite.Size = new System.Drawing.Size(142, 89);
            this.OfferDrawWhite.TabIndex = 5;
            this.OfferDrawWhite.Text = "OfferDrawWhite";
            this.OfferDrawWhite.UseVisualStyleBackColor = true;
            this.OfferDrawWhite.Click += new System.EventHandler(this.OfferDrawWhite_Click);
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(964, 495);
            this.Controls.Add(this.OfferDrawWhite);
            this.Controls.Add(this.OfferDrawBlack);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DoubleBuffered = true;
            this.Name = "GameWindow";
            this.Text = "GameWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameWindow_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button OfferDrawBlack;
        private System.Windows.Forms.Button OfferDrawWhite;
    }
}

