using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Core.Game;

namespace Model.Core.Pieces
{
    public class ManChecker : Piece
    {
        public ManChecker(bool color, (int, int) Position) : base(color, Position) { }

        public override bool Move((int Row, int Col) target, GameState s)
        {
            // Добавить условие на отсутствие шашки своего цвета
            if (Position.Row - target.Row == (Color ? 1 : -1) && Math.Abs(Position.Col - target.Col) == 1 
                && s.Map[target.Row, target.Col] == 0)
            {
                return true;
            }
            return false;
        }
        public override bool Eat((int Row, int Col) target, GameState s)
        {
            // Добавить условие на присутствие шашки противоположного цвета
            if (Math.Abs(Position.Row - target.Row) == 2 && Math.Abs(Position.Col - target.Col) == 2
                && s.Map[target.Row, target.Col] == 0 && IsPieceBetween(target, s))
            {
                return true;
            }
            return false;
        }
        public override void PossibleMoves(GameState s)
        {
            List<(int, int)> moves = new List<(int, int)>();
            for (int i = 0; i<s.Map.GetLength(0); i++)
            {
                for (int j = 0; j<s.Map.GetLength(1); j++)
                {
                    if (Move((i,j), s))
                    {
                        moves.Add((i,j));
                    }
                }
            }
            Moves = moves.ToArray();
        }
        public override void PossibleEats(GameState s)
        {
            List<(int, int)> moves = new List<(int, int)>();
            for (int i = 0; i < s.Map.GetLength(0); i++)
            {
                for (int j = 0; j < s.Map.GetLength(1); j++)
                {
                    if (Eat((i, j), s))
                    {
                        moves.Add((i, j));
                    }
                }
            }
            Eats = moves.ToArray();
        }
        private bool IsPieceBetween((int Row, int Col) target, GameState s)
        {
            int mr = (target.Row + Position.Row) / 2, mc = (target.Col + Position.Col) / 2;
            if (s.Pieces.ContainsKey((mr, mc)))
            {
                var p = s.Pieces[(mr, mc)];
                if (p.Color != this.Color) return true;
            }
            return false;
        }
    }
    public class QueenChecker : Piece
    {
        public QueenChecker(bool color, (int, int) Position) : base(color, Position) { }
        public override bool Move((int Row, int Col) target, GameState s)
        {
            if (s.Map[target.Row, target.Col] != 0) return false;
            if (Math.Abs(target.Row - Position.Row) != Math.Abs(target.Col - Position.Col))
                return false;
            return !HasPiecesBetween(target, s);
        }
        public override bool Eat((int Row, int Col) target, GameState s)
        {
            int rowDiff = target.Row - Position.Row;
            int colDiff = target.Col - Position.Col;

            if (Math.Abs(rowDiff) != Math.Abs(colDiff)) return false;
            if (s.Map[target.Row, target.Col] != 0) return false;

            int rowStep = Math.Sign(rowDiff);
            int colStep = Math.Sign(colDiff);

            var piecesBetween = new List<Piece>();
            for (int i = 1; i < Math.Abs(rowDiff); i++)
            {
                int checkRow = Position.Row + i * rowStep;
                int checkCol = Position.Col + i * colStep;

                if (s.Pieces.ContainsKey((checkRow, checkCol)))
                {
                    piecesBetween.Add(s.Pieces[(checkRow, checkCol)]);
                }
            }

            if (piecesBetween.Count != 1)  return false;

            return piecesBetween[0].Color != this.Color;
        }

        public override void PossibleMoves(GameState s)
        {
            var moves = new List<(int, int)>();
            foreach (var direction in new[] { (1, 1), (1, -1), (-1, 1), (-1, -1) })
            {
                for (int step = 1; step < s.Map.GetLength(0); step++)
                {
                    int newRow = Position.Row + direction.Item1 * step;
                    int newCol = Position.Col + direction.Item2 * step;
                    if (newRow < 0 || newRow >= s.Map.GetLength(0) ||
                        newCol < 0 || newCol >= s.Map.GetLength(1)) break;

                    if (Move((newRow, newCol), s))
                    {
                        moves.Add((newRow, newCol));
                    }
                    else
                    {
                        break;
                    }
                }
            }

            Moves = moves.ToArray();
        }
        public override void PossibleEats(GameState s)
        {
            var eats = new List<(int, int)>();

            for (int row = 0; row < s.Map.GetLength(0); row++)
            {
                for (int col = 0; col < s.Map.GetLength(1); col++)
                {
                    if (Eat((row, col), s))
                    {
                        eats.Add((row, col));
                    }
                }
            }

            Eats = eats.ToArray();
        }
        private bool HasPiecesBetween((int Row, int Col) target, GameState s)
        {
            return GetPiecesBetween(target, s).Count > 0;
        }

        private List<Piece> GetPiecesBetween((int Row, int Col) target, GameState s)
        {
            var pieces = new List<Piece>();

            int rowStep = Math.Sign(target.Row - Position.Row);
            int colStep = Math.Sign(target.Col - Position.Col);

            int steps = Math.Abs(target.Row - Position.Row);

            for (int i = 1; i < steps; i++)
            {
                int checkRow = Position.Row + i * rowStep;
                int checkCol = Position.Col + i * colStep;

                if (s.Pieces.ContainsKey((checkRow, checkCol)))
                {
                    pieces.Add(s.Pieces[(checkRow, checkCol)]);
                }
            }

            return pieces;
        }
    }
}
