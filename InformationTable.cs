using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Сheckers
{
    public partial class InformationTable : Form
    {
        public InformationTable()
        {
            InitializeComponent();
            this.Size = new Size(400, 200);
        }
        public static void ShowEndGameForm(Color backColor, string message, Color? textColor = null)
        {
            var endForm = new InformationTable()
            {
                BackColor = backColor,
                Text = message,
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog
            };
            var label = new Label()
            {
                Text = message,
                Font = new Font("Arial", 20, FontStyle.Bold),
                AutoSize = true,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            if (textColor.HasValue)
            {
                label.ForeColor = textColor.Value;
            }
            else
            {
                label.ForeColor = (backColor.GetBrightness() > 0.5f) ? Color.Black : Color.White;
            }

            endForm.Controls.Add(label);
            endForm.ShowDialog();
        }
    }
}
