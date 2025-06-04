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
using Model.Data.SaveLoad;
using Model;
using Model.Core.Game.AI;
using System.IO;
using System.Runtime.CompilerServices;
using Сheckers.references;

namespace Сheckers
{
    public partial class StartWindow : Form
    {
        public static Serializer serializerJSON = new SerializeGameJSON();
        public static Serializer serializerXML = new SerializeGameXML();
        private bool serializeJSON = true;
        private bool serializeXML = true;

        private Bitmap NewGameImage;
        private Bitmap ContinueGameImage;
        private Bitmap PlayAIGameImage;

        public static string SaveFolderPath { get; private set; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Saves");
        static StartWindow()
        {
        }
        public StartWindow()
        {
            InitializeComponent();
            NewGameImage = Loader.LoadImage("references/newgame.png", NewGame.Size.Width, NewGame.Size.Height);
            ContinueGameImage = Loader.LoadImage("references/Continue.png", ContinueGame.Width, ContinueGame.Height);
            PlayAIGameImage = Loader.LoadImage("references/newgamevsai.png", PlayAIGame.Width, PlayAIGame.Height);
            txtSaveFolder.Text = SaveFolderPath;
            NewGame.Image = NewGameImage; NewGame.Text = "";
            NewGame.FlatStyle = FlatStyle.Flat;
            NewGame.FlatAppearance.BorderSize = 0;

            ContinueGame.Image = ContinueGameImage;ContinueGame.Text = "";
            ContinueGame.FlatStyle = FlatStyle.Flat;
            ContinueGame.FlatAppearance.BorderSize = 0;

            PlayAIGame.Image = PlayAIGameImage; PlayAIGame.Text = "";
            PlayAIGame.FlatStyle = FlatStyle.Flat;
            PlayAIGame.FlatAppearance.BorderSize = 0;
        }

        private void SavesClick(object sender, EventArgs e)
        {

        }

        private void ContinueClick(object sender, EventArgs e)
        {
            var game = serializerJSON.DeSerializeGame();

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
            var AIgameState = new AIGameState(true);
            var NewGame = new GameWindow(AIgameState);
            NewGame.IsAiGame = true;

            NewGame.FormClosed += (s, args) => this.Show();

            this.Hide();
            NewGame.Show();
        }

        private void GamesHistoryClick(object sender, EventArgs e)
        {

        }

        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Выберите папку для сохранения игр";
                folderDialog.SelectedPath = SaveFolderPath;
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    SaveFolderPath = folderDialog.SelectedPath;
                    txtSaveFolder.Text = SaveFolderPath;

                    serializerJSON.SelectFolder(SaveFolderPath);
                }
            }
        }
        private void JSON_CheckedChanged(object sender, EventArgs e)
        {
            serializeJSON = !serializeJSON;
        }

        private void XML_CheckedChanged(object sender, EventArgs e)
        {
            serializeXML = !serializeXML;
        }
    }
}
