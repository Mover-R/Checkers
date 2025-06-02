using SaveLoad;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace Сheckers
{
    public partial class GameWindow : Form
    {
        private const int cellSize = 50;
        private const int mapSize = 8;
        private readonly Image WhiteFigure;
        private readonly Image BlackFigure;
        private readonly Image WhiteFigureQueen;
        private readonly Image BlackFigureQueen;
        private readonly Image BackGround;

        private GameState gameState;
        private Dictionary<(int, int), Button> buttons;
        private Button selectedButton = null;
        public int CellSize => cellSize;
        public int MapSize => mapSize;
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void GameWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            var fs = new Serializer();
            fs.SerializeGame(this.gameState);
        }
        public GameWindow(GameState st)
        {
            InitializeComponent();
            this.FormClosed += (s, args) => {
            };
            WhiteFigure = new Bitmap(new Bitmap(@"C:\Users\denis\source\repos\Сheckers\references\checker_white.png"),
                                    new Size(cellSize - 1, cellSize - 1));
            BlackFigure = new Bitmap(new Bitmap(@"C:\Users\denis\source\repos\Сheckers\references\checker_black.png"),
                                    new Size(cellSize - 1, cellSize - 1));
            WhiteFigureQueen = new Bitmap(new Bitmap(@"C:\Users\denis\source\repos\Сheckers\references\queen_white_balanced.png"),
                                    new Size(cellSize - 1, cellSize - 1));
            BlackFigureQueen = new Bitmap(new Bitmap(@"C:\Users\denis\source\repos\Сheckers\references\queen_black_balanced.png"),
                                    new Size(cellSize - 1, cellSize - 1));
            this.Text = "Checkers";
            Init(st);
        }
        public GameWindow()
        {
            InitializeComponent();
            this.FormClosed += (s, args) => { };
            WhiteFigure = new Bitmap(new Bitmap(@"C:\Users\denis\source\repos\Сheckers\references\checker_white.png"),
                                    new Size(cellSize - 1, cellSize - 1));
            BlackFigure = new Bitmap(new Bitmap(@"C:\Users\denis\source\repos\Сheckers\references\checker_black.png"),
                                    new Size(cellSize - 1, cellSize - 1));
            WhiteFigureQueen = new Bitmap(new Bitmap(@"C:\Users\denis\source\repos\Сheckers\references\queen_white_balanced.png"),
                                    new Size(cellSize - 1, cellSize - 1));
            BlackFigureQueen = new Bitmap(new Bitmap(@"C:\Users\denis\source\repos\Сheckers\references\queen_black_balanced.png"),
                                    new Size(cellSize - 1, cellSize - 1));
            this.Text = "Checkers";
            GameState st = new GameState();
            Init(st);
        }

        public void Init(GameState s)
        {
            buttons = new Dictionary<(int, int), Button>();
            gameState = s;
            gameState.UpdateBoardMoves();
            CreateMap();
        }

        public void CreateMap()
        {
            this.Width = cellSize * (mapSize + 5); this.Height = cellSize * (mapSize + 2);
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
                        gameState.EatPiece(fromPos, pos);
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
                        gameState.MovePiece(fromPos, pos);
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
            var fs = new Serializer();
            fs.SerializeGame(this.gameState);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

    }
}
