using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Model.Core.Game;

namespace Model.Core.Pieces
{
    public abstract class Piece : IPiece
    {
        public (int Row, int Col) Position { get; set; }
        public bool Color { get; private set; }
        public (int, int)[] Moves { get; set; }
        public (int, int)[] Eats { get; set; }
        protected Piece(bool color, (int, int) Position) 
        { 
            this.Color = color;
            this.Position = Position;
        }

        public abstract bool Move((int Row, int Col) target, GameState s);
        public abstract bool Eat((int Row, int Col) target, GameState s);
        public abstract void PossibleMoves(GameState s);
        public abstract void PossibleEats(GameState s);
    }
}
