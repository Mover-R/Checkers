using Model.Core.Game;
using Model.Core.Pieces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core.Game.AI
{
    public class AIGameState : GameState
    {
        private readonly Random _random = new Random();
        private readonly bool _isPlayerWhite;
        public bool IsPlayerWhite => _isPlayerWhite;

        public AIGameState(bool isPlayerWhite) : base()
        {
            _isPlayerWhite = isPlayerWhite;
        }

        public AIGameState(int[,] map, Dictionary<(int, int), Piece> pieces, bool whiteMove, bool isPlayerWhite)
            : base(map, pieces, whiteMove)
        {
            _isPlayerWhite = isPlayerWhite;
        }
        public AIGameState(GameState game, bool isPlayerWhite)
            : base(game.Map, game.Pieces, game.WhiteMove)
        {
            _isPlayerWhite = isPlayerWhite;
        }

        public bool IsAITurn => WhiteMove != _isPlayerWhite;

        public void MakeAIMove(GameState state)
        {
            if (!IsAITurn) return;
            var allPossibleMoves = GetAllPossibleMoves(state);
            Debug.WriteLine("MOOOOOVE");
            if (allPossibleMoves.Count == 0)
            {
                Debug.WriteLine($"{string.Join(", ", allPossibleMoves)}, NOOOOO");
                return;
            }

            var randomMove = allPossibleMoves[_random.Next(allPossibleMoves.Count)];
            Debug.WriteLine(randomMove);
            state.MovePiece(randomMove.from, randomMove.to);
            state.SwitchPlayer();
        }

        private List<((int, int) from, (int, int) to)> GetAllPossibleMoves(GameState s)
        {
            var eat = new List<((int, int) from, (int, int) to)>();
            var move = new List<((int, int) from, (int, int) to)>();
            foreach (var p in s.Pieces.Values)
            {
                if (p.Color == IsAITurn) continue;
                Debug.WriteLine($"{p.Color} {p.Eats.Length} {p.Moves.Length}");
                foreach (var m in p.Eats) eat.Add((p.Position, m));
                foreach (var m in p.Moves)
                {
                    Debug.WriteLine((p.Position, m));
                    move.Add((p.Position, m));
                }
            }
            Debug.WriteLine($"{eat.Count}  {move.Count}  {string.Join(", ", eat)}  {string.Join(", ", move)}");
            return eat.Count > 0 ? eat : move;
        }

        public int CalculateScore(bool forWhite)
        {
            int score = 0;
            foreach (var piece in Pieces.Values)
            {
                if (piece.Color == forWhite)
                {
                    score += piece is QueenChecker ? 3 : 1;
                }
            }
            return score;
        }
    }
}
