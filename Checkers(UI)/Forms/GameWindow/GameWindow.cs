using Model.Data.SaveLoad;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using Model;
using Model.Core.Game;
using Model.Core.Pieces;
using Model.Core.Game.AI;
using Сheckers.references;

namespace Сheckers
{
    public partial class GameWindow : Form
    {
        private readonly static Image WhiteFigure;
        private readonly static Image BlackFigure;
        private readonly static Image WhiteFigureQueen;
        private readonly static Image BlackFigureQueen;
        private readonly static Image WhitePlaceBackground;
        private readonly static Image BlackPlaceBackground;
        private readonly static Image WhitePlaceBackgroundHighlitedGreen;
        private readonly static Image BlackPlaceBackgroundHighlitedGreen;
        private readonly static Image WhitePlaceBackgroundHighlitedRed;
        private readonly static Image BlackPlaceBackgroundHighlitedRed;
        private readonly static Image OfferDraw;

        private Dictionary<(int, int), Button> buttons;
        private Button selectedButton = null;
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void GameWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            StartWindow.serializerJSON.SerializeGame(gameState);
        }
        static GameWindow()
        {
            string[] imagePaths = {
                "references/whitechecker.png",
                "references/blackchecker.png",
                "references/whiteking.png",
                "references/blackking.png"
            };

            WhiteFigure = Loader.LoadImage("references/whitechecker.png", cellSize);
            BlackFigure = Loader.LoadImage("references/blackchecker.png", cellSize);
            WhiteFigureQueen = Loader.LoadImage("references/whiteking.png", cellSize);
            BlackFigureQueen = Loader.LoadImage("references/blackking.png", cellSize);
            WhitePlaceBackground = Loader.LoadImage("references/checkers_board_stylized_White.png", cellSize);
            BlackPlaceBackground = Loader.LoadImage("references/checkers_board_stylized_Black.png", cellSize);

            WhitePlaceBackgroundHighlitedGreen = Loader.LoadImage("references/checkers_board_stylized_WhiteHighlited.png", cellSize);
            BlackPlaceBackgroundHighlitedGreen = Loader.LoadImage("references/checkers_board_stylized_BlackHighlited.png", cellSize);

            WhitePlaceBackgroundHighlitedRed = Loader.LoadImage("references/checkers_board_stylized_BlackHighlitedRed.png", cellSize);
            BlackPlaceBackgroundHighlitedRed = Loader.LoadImage("references/checkers_board_stylized_BlackHighlitedRed.png", cellSize);
        }
        public GameWindow(GameState st)
        {
            InitializeComponent();
            this.FormClosed += (s, args) => {
            };
            this.Text = "Checkers";
            Init(st);
        }
        public GameWindow()
        {
            InitializeComponent();
            this.FormClosed += (s, args) => { };
            this.Text = "Checkers";
            GameState st = new GameState();
            if (IsAiGame) st = new AIGameState(true);
            Init(st);
        }
        public void Init(GameState s)
        {
            buttons = new Dictionary<(int, int), Button>();
            gameState = s;
            GameState.Add(gameState);
            gameState.UpdateBoardMoves();
            CreateMap();
        }
        private void WinInfo()
        {
            switch (gameState.CheckWin())
            {
                case 0: // Игра продолжается
                    break;
                case 1: // Победа белых
                    InformationTable.ShowEndGameForm(Color.White, "Белые победили!");
                    this.Close();
                    break;
                case 2: // Победа черных
                    InformationTable.ShowEndGameForm(Color.Black, "Черные победили!", Color.White);
                    this.Close();
                    break;
                case 3: // Ничья
                    InformationTable.ShowEndGameForm(Color.Gray, "Ничья!");
                    this.Close();
                    break;
            }
        }

        private void ToMainMenuClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveProgresClick(object sender, EventArgs e)
        {
            Debug.WriteLine(StartWindow.SaveFolderPath);
            StartWindow.serializerJSON.SerializeGame(this.gameState);
        }
        private void ForwardClick(object sender, EventArgs e)
        {
            var st = GameState.Forward();
            if (st != null)
            {
                Debug.WriteLine("GO Forward");
                gameState = st;
            }
            UpdateBoard();
            gameState.UpdateBoardMoves();
        }

        private void BackClick(object sender, EventArgs e)
        {
            var st = GameState.Back();
            if (st != null)
            {
                Debug.WriteLine("GO Back");
                gameState = st;
            }
            UpdateBoard();
            gameState.UpdateBoardMoves();
        }

        private void OfferDrawWhite_Click(object sender, EventArgs e)
        {
            gameState.WhiteDraw();
            WinInfo();
        }

        private void OfferDrawBlack_Click(object sender, EventArgs e)
        {
            if (IsAiGame) return;
            gameState.BlackDraw();
            WinInfo();
        }
    }
}
