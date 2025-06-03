using Model.Core.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core.Game
{
    public partial class GameState
    {
        private static int index;
        public static List<(List<Piece>, bool)> History { get; private set; } = new List<(List<Piece>, bool)>();
        static GameState() { }
        public static void Add(GameState s)
        {
            var state = new GameState(
                   (int[,])s.Map.Clone(),
                   new Dictionary<(int, int), Piece>(s.Pieces),
                   s.WhiteMove);
            if (History.Count > index && index != -1)
            {
                //History[index] = ();
                return;
            }
            //History.Add(state);
            index++;
        }
        public static GameState Back()
        {
            if (History.Count == 0 || index <= 0)
            {
                return null;
            }

            var st = History[index - 1];
            index--;
            return null;
        }
        public static GameState Forward()
        {
            if (History.Count > index)
            {
                index++;
                return null;
            }
            return null;
        }
    }
}
