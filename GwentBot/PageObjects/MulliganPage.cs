using AutoIt;
using GwentBot.Model;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;

namespace GwentBot.PageObjects
{
    internal class MulliganPage : PageObject
    {
        //Для работы с этим полем из методов VerifyingPage и WaitingGameReadiness
        //нужно создавать дополнительные поля
        internal Game game;

        private bool? iWonCoin = null;

        internal MulliganPage(
            IGwentStateChecker gwentStateChecker, IWaitingService waitingService, Game game) :
            base(gwentStateChecker, waitingService)
        {
            this.game = game;
            game.IWonCoin = iWonCoin;
        }

        internal GameSessionPage EndMulligan()
        {
            AutoItX.MouseClick("left", 375, 451);
            do
            {
                waitingService.Wait(1);
            } while (gwentStateChecker.GetCurrentGameSessionStates() !=
                GameSessionStates.EndMulliganMessageBox);
            AutoItX.Send("{ENTER}");
            return new GameSessionPage(gwentStateChecker, waitingService, game);
        }

        internal GameSessionPage HideMulganPage()
        {
            AutoItX.MouseClick("left", 484, 451);
            return new GameSessionPage(gwentStateChecker, waitingService, game);
        }

        protected override bool VerifyingPage()
        {
            return gwentStateChecker
                .GetCurrentGameSessionStates() == GameSessionStates.Mulligan;
        }

        protected override void WaitingGameReadiness(int seconds = 30)
        {
            while (iWonCoin == null)
            {
                var coinTossState = gwentStateChecker.GetCurrentCoinTossStates();
                if (coinTossState == CoinTossStates.CoinWon)
                    iWonCoin = true;
                if (coinTossState == CoinTossStates.CoinLost)
                    iWonCoin = false;
            }

            base.WaitingGameReadiness(seconds);
        }
    }
}