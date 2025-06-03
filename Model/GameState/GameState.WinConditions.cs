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
        public int CheckWin()
        {
            Debug.WriteLine($"{CanMove()}, {CountWhite}, {CountBlack}");
            if (!CanMove()) return CountWhite - CountBlack > 0 ? 1 : 2;
            if (CountWhite == 0) return 2;
            else if (CountBlack == 0) return 1;
            return 0;
        }
    }
}
