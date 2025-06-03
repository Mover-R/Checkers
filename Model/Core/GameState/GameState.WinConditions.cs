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
            Debug.WriteLine($"{CanMove()}, {CountWhite}, {CountBlack}, {CheckDraw()}");
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
            Debug.WriteLine(OfferDrawWhite);
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
        public bool ShouldAIAcceptDraw(AIGameState aiGame)
        {
            if (!(aiGame.IsAITurn && (OfferDrawWhite || OfferDrawBlack)))
                return false;

            int aiScore = aiGame.CalculateScore(aiGame.IsPlayerWhite);
            int playerScore = aiGame.CalculateScore(aiGame.IsPlayerWhite);

            // Компьютер соглашается на ничью, если у него меньше очков
            return aiScore <= playerScore;
        }
    }
}
