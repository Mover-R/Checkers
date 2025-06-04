using Model.Core.Game.AI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core.Game
{
    public partial class GameState
    {
        private bool OfferDrawWhite { get; set; } = false;
        private bool OfferDrawBlack { get; set; } = false;
        public int CheckWin()
        {
            //Debug.WriteLine($"{CanMove()}, {CountWhite}, {CountBlack}, {CheckDraw()} {(this is AIGameState)} {ShouldAIAcceptDraw()} ");
            if ((this is AIGameState) && ShouldAIAcceptDraw()) return 3; 
            if (CheckDraw()) return 3;
            if (!CanMove() && !CanEat()) return CountWhite - CountBlack > 0 ? 1 : 2;
            if (CountWhite == 0) return 2;
            else if (CountBlack == 0) return 1;
            return 0;
        }
        public bool CheckDraw()
        {
            return OfferDrawWhite && OfferDrawBlack;
        }
        public void WhiteDraw()
        {
            OfferDrawWhite = !OfferDrawWhite;
            Debug.WriteLine($"WHITE OFFERS DRAW {OfferDrawWhite}");
        }
        public void BlackDraw()
        {
            OfferDrawBlack = !OfferDrawBlack;
            Debug.WriteLine(OfferDrawBlack);
        }
        public void RemoveDraw()
        {
            Debug.WriteLine("Removed Draw");
            OfferDrawWhite = false; OfferDrawBlack= false;
        }
        public bool ShouldAIAcceptDraw()
        {
            Debug.WriteLine($"WHY WHITE OFFERS? {OfferDrawWhite}");
            if (!OfferDrawWhite)
                return false;

            int aiScore = (this as AIGameState).CalculateScore((this as AIGameState).IsPlayerWhite);
            int playerScore = (this as AIGameState).CalculateScore((this as AIGameState).IsPlayerWhite);

            return aiScore <= playerScore;
        }
    }
}
