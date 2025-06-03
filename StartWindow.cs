using SaveLoad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;

namespace Сheckers
{
    public partial class StartWindow : Form
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void SavesClick(object sender, EventArgs e)
        {

        }

        private void ContinueClick(object sender, EventArgs e)
        {
            var fs = new Serializer();
            var game = fs.DeSerializeGame();

            var NewGame = new GameWindow(game);

            NewGame.FormClosed += (s, args) => this.Show();

            this.Hide();
            NewGame.Show();
        }

        private void NewGameClick(object sender, EventArgs e)
        {
            var NewGame = new GameWindow();

            NewGame.FormClosed += (s, args) => this.Show();

            this.Hide();
            NewGame.Show();
        }

        private void PlayWithAIClick(object sender, EventArgs e)
        {

        }

        private void GamesHistoryClick(object sender, EventArgs e)
        {

        }
    }
}
