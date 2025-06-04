using Model.Core.Game;
using Model.Core.Game.AI;
using Model.Core.Pieces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Сheckers
{
    public partial class GameWindow
    {
        private GameState gameState;
        private const int cellSize = 50;
        private const int mapSize = 8;
        public bool IsAiGame { get; set; } = false;
        public int CellSize => cellSize;
        public int MapSize => mapSize;

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
                        button.BackgroundImage = BlackPlaceBackground;
                    }
                    else
                    {
                        button.BackgroundImage = WhitePlaceBackground;
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
                        if (p.Eats.Length == 0)
                        {
                            gameState.SwitchPlayer();
                        }
                        else
                        {
                            ResetSelection();
                            selectedButton = b;
                            HighlightPossibleMoves(pos);
                        }
                    }
                    else if (piece.Moves.Contains(pos))
                    {
                        if (gameState.MovePiece(fromPos, pos)) InformationTable.ShowPromoteQueenForm();
                        ResetSelection();
                        gameState.SwitchPlayer();
                    }
                    UpdateBoard();
                }
                else
                {
                    FirstClick(pos, b);
                }
            }
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
            {
                buttons[eatPos].BackgroundImage = (eatPos.Item1 + eatPos.Item2) % 2 != 0
                    ? BlackPlaceBackgroundHighlitedRed
                    : WhitePlaceBackgroundHighlitedRed;
            }

            if (piece.Eats.Length == 0)
                foreach (var movePos in piece.Moves)
                {
                    buttons[movePos].BackgroundImage = (movePos.Item1 + movePos.Item2) % 2 != 0
                        ? BlackPlaceBackgroundHighlitedGreen
                        : WhitePlaceBackgroundHighlitedGreen;
                }

        }
        private void ResetSelection()
        {
            selectedButton = null;

            foreach (var btn in buttons.Values)
            {
                var btnPos = (btn.Location.Y / cellSize, btn.Location.X / cellSize);
                btn.BackgroundImage = (btnPos.Item1 + btnPos.Item2) % 2 != 0
                    ? BlackPlaceBackground
                    : WhitePlaceBackground;
            }
            selectedButton = null;
        }
        private void UpdateBoard(bool flag = true)
        {
            if (IsAiGame)
            {
                Debug.WriteLine("ITS AI TIME");
                if (gameState is AIGameState s) s.MakeAIMove();
                else Debug.WriteLine("STRANGE..... NOT AN AI");
            }
            foreach (var button in buttons)
            {
                if ((button.Key.Item1 + button.Key.Item2) % 2 != 0)
                {
                    button.Value.BackgroundImage = BlackPlaceBackground;
                }
                else
                {
                    button.Value.BackgroundImage = WhitePlaceBackground;
                }
                button.Value.Image = null;
                button.Value.Text = "";
            }
            foreach (var pos in gameState.Pieces.Keys)
            {
                var piece = gameState.Pieces[pos];
                var button = buttons[pos];

                if (piece is QueenChecker)
                {
                    button.Image = piece.Color ? WhiteFigureQueen : BlackFigureQueen;
                }
                else
                {
                    button.Image = piece.Color ? WhiteFigure : BlackFigure;
                }
            }

            WinInfo();
            gameState.RemoveDraw();
        }
    }
}
