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
            for (int tick = 0; tick < 5; tick++)
            {
                if (stateChecker.GetCurrentGameSessionStates() ==
                   GameSessionStates.SearchRival)
                    break;
            }

            do
            {
                waitingService.Wait(1);
            } while (stateChecker.GetCurrentGameSessionStates() ==
                     GameSessionStates.SearchRival);

            for (int tick = 0; tick < 15; tick++)
            {
                var globalMessageBoxes = stateChecker.GetCurrentGlobalMessageBoxes();
                if (globalMessageBoxes != GlobalMessageBoxes.NoMessageBoxes)
                {
                    throw new Exception($"Это не страница {GetType()}");
                }

                if (stateChecker.GetCurrentGlobalGameStates() ==
                   GlobalGameStates.HeavyLoading)
                    break;
                waitingService.Wait(1);
            }

            do
            {
                waitingService.Wait(1);
            } while (stateChecker.GetCurrentGlobalGameStates() ==
                     GlobalGameStates.HeavyLoading);

            for (int tick = 0; tick < 15; tick++)
            {
                var globalMessageBoxes = stateChecker.GetCurrentGlobalMessageBoxes();
                if (globalMessageBoxes != GlobalMessageBoxes.NoMessageBoxes)
                {
                    throw new Exception($"Это не страница {GetType()}");
                }

                switch (stateChecker.GetCurrentCoinTossStates())
                {
                    case CoinTossStates.CoinWon:
                        iWonCoin = true;
                        break;

                    case CoinTossStates.CoinLost:
                        iWonCoin = false;
                        break;
                }
                if (iWonCoin != null)
                    break;
                waitingService.Wait(1);
            }

            for (int tick = 0; tick < 5; tick++)
            {
                if (stateChecker.GetCurrentGameSessionStates() ==
                    GameSessionStates.SessionPageOpen)
                    break;
                waitingService.Wait(1);
            }

            do
            {
                var globalMessageBoxes = stateChecker.GetCurrentGlobalMessageBoxes();
                if (globalMessageBoxes != GlobalMessageBoxes.NoMessageBoxes)
                {
                    throw new Exception($"Это не страница {GetType()}");
                }

                waitingService.Wait(1);
            } while (stateChecker.GetCurrentGameSessionStates() ==
                     GameSessionStates.SessionPageOpen);

            base.WaitingGameReadiness(seconds);
        }
    }
}