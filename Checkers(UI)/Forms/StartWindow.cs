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

namespace Сheckers
{
    public partial class StartWindow : Form
    {
        public static Serializer serializerJSON = new SerializeGameJSON();
        public static Serializer serializerXML = new SerializeGameXML();
        private bool serializeJSON = true;
        private bool serializeXML = true;
        public static string SaveFolderPath { get; private set; } = @"C:\Users\denis\source\repos\Сheckers\Saves";
        public StartWindow()
        {
            InitializeComponent();
            txtSaveFolder.Text = SaveFolderPath;
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
            var NewGame = new GameWindow();
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
