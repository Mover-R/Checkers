using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class GameState
    {
        public int Score { get; private set; }
        public int CountWhite { get; private set; }
        public int CountBlack { get; private set; }
        public Dictionary<(int, int), Piece> Pieces { get; private set; }
        public int[,] Map { get; }
        public bool WhiteMove { get; private set; } = true;

        public GameState()
        {
            Map = new int[,] {
            { 0, 2, 0, 2, 0, 2, 0, 2 },
            { 2, 0, 2, 0, 2, 0, 2, 0 },
            { 0, 2, 0, 2, 0, 2, 0, 2 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 0, 1, 0, 1, 0, 1, 0 },
            { 0, 1, 0, 1, 0, 1, 0, 1 },
            { 1, 0, 1, 0, 1, 0, 1, 0 }
            /*
            { 0, 0, 0, 0, 0, 2, 0, 2 },
            { 1, 0, 0, 0, 0, 0, 2, 0 },
            { 0, 0, 0, 2, 0, 2, 0, 2 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 2, 0, 0 },
            { 1, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0 }
            */
            };

            int cntWhite = 0, cntBlack = 0;
            Pieces = new Dictionary<(int, int), Piece>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Map[i, j] == 1)
                    { Pieces[(i, j)] = new ManChecker(true, (i, j)); cntWhite++; }
                    else if (Map[i, j] == 2)
                    { Pieces[(i, j)] = new ManChecker(false, (i, j)); cntBlack++; }
                }
            }
            CountBlack = cntBlack;
            CountWhite = cntWhite;
        }

        public GameState(int[,] map, Dictionary<(int, int), Piece> pieces, bool whiteMove)
        {
            Map = map;
            Pieces = pieces;
            int cntWhite = 0, cntBlack = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Map[i, j] == 1)
                    { cntWhite++; }
                    else if (Map[i, j] == 2)
                    { cntBlack++; }
                }
            }
            CountBlack = cntBlack;
            CountWhite = cntWhite;
            WhiteMove = whiteMove;
        }

        public void UpdateBoardMoves()
        {
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Pieces.ContainsKey((i, j)))
                    {
                        var p = Pieces[(i, j)];
                        p.PossibleMoves(this);
                        p.PossibleEats(this);
                    }
                }
            }
            foreach (var p in Pieces)
            {
                if (p.Value.Eats.Length != 0) p.Value.Moves = new (int, int)[0];
            }
        }
        public bool CanEat()
        {
            bool flag = false;
            foreach (var p in Pieces.Values)
            {
                if (p.Color == WhiteMove && p.Eats.Length != 0)
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        public bool CanMove()
        {
            bool flag = false;
            foreach (var p in Pieces.Values)
            {
                if (p.Color == WhiteMove && p.Moves.Length != 0)
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        public void MovePiece((int row, int col) from, (int row, int col) to)
        {
            if (!Pieces.ContainsKey(from) || Pieces.ContainsKey(to)) return;

            var piece = Pieces[from];
            if (!piece.Moves.Contains(to)) return;
            Pieces.Remove(from);
            piece.Position = to;
            Pieces[to] = piece;

            Map[from.row, from.col] = 0;
            Map[to.row, to.col] = WhiteMove ? 1 : 2;

            Promote(to);
        }
        public void EatPiece((int row, int col) from, (int row, int col) to)
        {
            if (!Pieces.ContainsKey(from) || Pieces.ContainsKey(to)) return;


            var piece = Pieces[from];

            if (piece is ManChecker)
            {
                int mr = (from.row + to.row) / 2, mc = (from.col + to.col) / 2;

                if (Map[mr, mc] == 2) CountBlack--;
                else CountWhite--;

                Pieces.Remove(from);
                Pieces.Remove((mr, mc));
                Pieces[to] = piece;
                piece.Position = to;

                Map[from.row, from.col] = 0;
                Map[mr, mc] = 0;
                Map[to.row, to.col] = WhiteMove ? 1 : 2;

                Promote(to);
            }
            else
            {
                int rowStep = Math.Sign(to.row - from.row);
                int colStep = Math.Sign(to.col - from.col);

                for (int i = 1; i < Math.Abs(to.row - from.row); i++)
                {
                    int checkRow = from.row + i * rowStep;
                    int checkCol = from.col + i * colStep;
                    var checkPos = (checkRow, checkCol);

                    if (Pieces.TryGetValue(checkPos, out var enemy) && enemy.Color != piece.Color)
                    {
                        Pieces.Remove(checkPos);
                        if (Map[checkRow, checkCol] == 2) CountBlack--;
                        else CountWhite--;
                        Map[checkRow, checkCol] = 0;
                        break;
                    }
                }

                Pieces.Remove(from);
                Pieces[to] = piece;
                piece.Position = to;

                Map[from.row, from.col] = 0;
                Map[to.row, to.col] = piece.Color ? 1 : 2;
            }
        }
        public void SwitchPlayer()
        {
            WhiteMove = !WhiteMove;
        }
        public void Promote((int row, int col) pos)
        {
            var check = Pieces[pos];
            int x = check.Position.Row;
            if (check is QueenChecker) return;
            if (check.Color && x != 0) return;
            else if (!check.Color && x != 7) return;

            var p = new QueenChecker(Pieces[pos].Color, pos);
            Pieces.Remove(pos);
            Pieces[pos] = p;
        }
    }
}
