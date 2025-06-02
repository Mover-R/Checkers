using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Сheckers
{
    internal interface IPiece
    {
        (int Row, int Col) Position { get; }
        bool Color { get; }
        (int, int)[] Moves { get; }
        (int, int)[] Eats { get; }
        bool Move((int Row, int Col) target, GameState s);
        bool Eat((int Row, int Col) target, GameState s);
        void PossibleMoves(GameState s);

    }
}
