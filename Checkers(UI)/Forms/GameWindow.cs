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
        private const int cellSize = 50;
        private const int mapSize = 8;
        private readonly static Image WhiteFigure;
        private readonly static Image BlackFigure;
        private readonly static Image WhiteFigureQueen;
        private readonly static Image BlackFigureQueen;
        private readonly static Image BackGround;

        private GameState gameState;
        private Dictionary<(int, int), Button> buttons;
        private Button selectedButton = null;
        private AIGameState AI;
        public bool IsAiGame { get; set; }
        public int CellSize => cellSize;
        public int MapSize => mapSize;
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void GameWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            StartWindow.serializerJSON.SerializeGame(this.gameState);
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
            Init(st);
        }
        public GameWindow(AIGameState ai)
        {
            InitializeComponent();
            AI = ai;
            this.FormClosed += (s, args) => { };
            this.Text = "Checkers";
            GameState st = new GameState();
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

        public void CreateMap()
        {
            this.Width = cellSize * (mapSize + 8); this.Height = cellSize * (mapSize + 2);
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    var button = new Button();
                    button.Location = new Point(j * cellSize + cellSize / 2, i * cellSize + cellSize / 2);
                    button.Size = new Size(cellSize, cellSize);
                    button.Click += new EventHandler(OnButtonPress);
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;
                    if ((i + j) % 2 != 0)
                    {
                        button.BackColor = Color.Gray;
                    }
                    else
                    {
                        button.BackColor = Color.White;
                    }
                    if (gameState.Map[i, j] == 1)
                    {
                        button.Image = WhiteFigure;
                    }
                    else if (gameState.Map[i, j] == 2)
                    {
                        button.Image = BlackFigure;
                    }
                    this.Controls.Add(button);
                    buttons[(i, j)] = button;
                }
            }
        }
        private void OnButtonPress(object sender, EventArgs e)
        {
            var b = sender as Button;
            (int row, int col) pos = (b.Location.Y / cellSize, b.Location.X / cellSize);

            if (selectedButton == null)
            {
                Debug.WriteLine("First Clic");
                FirstClick(pos, b);
            }
            else
            {
                Debug.WriteLine("Second clic");
                (int row, int col) fromPos = (selectedButton.Location.Y / cellSize, selectedButton.Location.X / cellSize);
                var piece = gameState.Pieces[fromPos];

                Debug.WriteLine($"{fromPos}, {pos}");
                bool isValidMove = piece.Eats.Contains(pos) || (piece.Eats.Length == 0 && piece.Moves.Contains(pos));

                Debug.WriteLine(string.Join(", ", piece.Eats));
                Debug.WriteLine(string.Join(", ", piece.Moves));

                if (isValidMove)
                {
                    if (piece.Eats.Contains(pos))
                    {
                        if (gameState.EatPiece(fromPos, pos)) InformationTable.ShowPromoteQueenForm();
                        gameState.UpdateBoardMoves();
                        var p = gameState.Pieces[pos];
                        ResetSelection();
                        if (p.Eats.Length == 0) { gameState.SwitchPlayer(); }
                        else
                        {
                            ResetSelection();
                            selectedButton = b;
                            HighlightPossibleMoves(pos);
                        }
                    } else if (piece.Moves.Contains(pos))
                    {
                        if (gameState.MovePiece(fromPos, pos)) InformationTable.ShowPromoteQueenForm();
                        ResetSelection();
                        gameState.SwitchPlayer();
                    }
                }
                else
                {
                    FirstClick(pos, b);
                }
            }
            UpdateBoard();
            gameState.UpdateBoardMoves();
            GameState.Add(gameState);
            Debug.WriteLine(GameState.History.Count);
        }

        private void FirstClick((int row, int col) pos, Button b)
        {
            if (!gameState.Pieces.ContainsKey(pos))
            {
                // На этой кнопке не находится шашка
                return;
            }
            var piece = gameState.Pieces[pos];
            Debug.WriteLine(string.Join(", ", piece.Eats));
            Debug.WriteLine(string.Join(", ", piece.Moves));
            if (gameState.CanEat() && piece.Eats.Length == 0 || piece.Color != gameState.WhiteMove)
            {
                // Данная фигура не может съесть но на доске доступны ходы чтобы съесть
                return;
            }
            ResetSelection();

            selectedButton = b;
            selectedButton.BackColor = Color.Green;
            HighlightPossibleMoves(pos);
        }
        private void HighlightPossibleMoves((int row, int col) pos)
        {
            if (!gameState.Pieces.ContainsKey(pos)) return;
            var piece = gameState.Pieces[pos];
            foreach (var eatPos in piece.Eats)
                buttons[eatPos].BackColor = Color.Red;
            if (piece.Eats.Length == 0)
                foreach (var movePos in piece.Moves)
                {
                    buttons[movePos].BackColor = Color.LightGreen;
                }            
        }
        private void ResetSelection()
        {
            selectedButton = null;
            foreach (var pos in buttons.Keys)
            {
                buttons[pos].BackColor = (pos.Item1 + pos.Item2) % 2 != 0 ? Color.Gray : Color.White;
            }
            selectedButton = null;
        }
        private void UpdateBoard()
        {
            if (IsAiGame)
            {
                Debug.WriteLine("ITS AI TIME");
                AI = new AIGameState(gameState, true);
                AI.MakeAIMove(gameState);
            }
            foreach (var button in buttons.Values)
            {
                button.Image = null;
                button.Text = "";
            }
            foreach (var pos in gameState.Pieces.Keys)
            {
                var piece = gameState.Pieces[pos];
                var button = buttons[pos];

                if (piece is QueenChecker)
                {
                    button.Image = piece.Color ? WhiteFigureQueen : BlackFigureQueen;
                } else
                {
                    button.Image = piece.Color ? WhiteFigure : BlackFigure;
                }
            }

            WinInfo();
            gameState.RemoveDraw();
        }
        private void WinInfo()
        {
            switch (gameState.CheckWin())
            {
                case 0: // Игра продолжается
                    break;
                case 1: // Победа белых
                    InformationTable.ShowEndGameForm(Color.White, "Белые победили!");
                    break;
                case 2: // Победа черных
                    InformationTable.ShowEndGameForm(Color.Black, "Черные победили!", Color.White); // Белый текст на черном фоне
                    break;
                case 3: // Ничья
                    InformationTable.ShowEndGameForm(Color.Gray, "Ничья!");
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

        private void timer1_Tick(object sender, EventArgs e)
        {

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
            gameState.BlackDraw();
            WinInfo();
        }
    }
}
