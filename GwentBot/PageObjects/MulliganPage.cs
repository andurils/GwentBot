using System;
using GwentBot.GameInput;
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
            IGwentStateChecker stateChecker,
            IWaitingService waitingService,
            IInputDeviceEmulator inputEmulator,
            Game game) :
            base(stateChecker, waitingService, inputEmulator)
        {
            this.game = game;
            game.IWonCoin = iWonCoin;
        }

        internal GameSessionPage EndMulligan()
        {
            if (stateChecker.GetCurrentGameSessionStates() ==
                GameSessionStates.OpponentSurrenderedMessageBox)
            {
                return new GameSessionPage(stateChecker, waitingService, inputEmulator, game);
            }

            inputEmulator.MouseClick(375, 451);
            for (int tick = 0; tick < 30; tick++)
            {
                if (stateChecker.GetCurrentGameSessionStates() ==
                   GameSessionStates.EndMulliganMessageBox)
                    break;
                waitingService.Wait(1);
            }
            inputEmulator.Send("{ENTER}");
            return new GameSessionPage(stateChecker, waitingService, inputEmulator, game);
        }

        internal GameSessionPage HideMulganPage()
        {
            inputEmulator.MouseClick(484, 451);
            return new GameSessionPage(stateChecker, waitingService, inputEmulator, game);
        }

        protected override bool VerifyingPage()
        {
            return stateChecker
                .GetCurrentGameSessionStates() == GameSessionStates.Mulligan;
        }

        protected override void WaitingGameReadiness(int seconds = 30)
        {
            int tick = 10;
            for (; tick != 0; tick--)
            {
                if (stateChecker.GetCurrentGameSessionStates() ==
                    GameSessionStates.SearchRival)
                    break;
                waitingService.Wait(1);
            }
            if (tick == 0)
                throw new Exception($"Это не страница {GetType()}");

            do
            {
                waitingService.Wait(1);
            } while (stateChecker.GetCurrentGameSessionStates() ==
                     GameSessionStates.SearchRival);

            tick = 10;
            for (; tick != 0; tick--)
            {
                if (stateChecker.GetCurrentGlobalGameStates() ==
                   GlobalGameStates.HeavyLoading)
                    break;
                waitingService.Wait(1);
            }
            if (tick == 0)
                throw new Exception($"Это не страница {GetType()}");

            do
            {
                waitingService.Wait(1);
            } while (stateChecker.GetCurrentGlobalGameStates() ==
                     GlobalGameStates.HeavyLoading);

            tick = 10;
            for (; tick != 0; tick--)
            {
                var coinState = stateChecker.GetCurrentCoinTossStates();
                if (coinState != CoinTossStates.Unknown & coinState != CoinTossStates.StartToss)
                    iWonCoin = coinState == CoinTossStates.CoinWon ? true : false;

                if (iWonCoin != null)
                    break;
                waitingService.Wait(1);
            }
            if (tick == 0)
                throw new Exception($"Это не страница {GetType()}");

            base.WaitingGameReadiness(seconds);
        }
    }
}