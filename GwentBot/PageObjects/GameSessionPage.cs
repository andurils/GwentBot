using System;
using GwentBot.GameInput;
using GwentBot.Model;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;

namespace GwentBot.PageObjects
{
    internal class GameSessionPage : PageObject
    {
        internal readonly Game game;

        internal GameSessionPage(
            IGwentStateChecker stateChecker,
            IWaitingService waitingService,
            IInputDeviceEmulator inputEmulator,
            Game game) :
            base(stateChecker, waitingService, inputEmulator)
        {
            this.game = game;
        }

        internal bool MyTurnPlay
        {
            get
            {
                return stateChecker.GetCurrentGameSessionStates() ==
                    GameSessionStates.MyTurnPlay;
            }
        }

        internal MatchResultsRewardsScreenPage GiveUp()
        {
            var gameSessionState = GameSessionStates.Unknown;

            if (stateChecker.GetCurrentGameSessionStates() ==
                GameSessionStates.OpponentSurrenderedMessageBox)
            {
                inputEmulator.MouseClick(427, 275);
                return new MatchResultsRewardsScreenPage(
                    stateChecker, waitingService, inputEmulator, game);
            }

            int seconds = 30;
            for (; seconds != 0; seconds--)
            {
                gameSessionState = stateChecker.GetCurrentGameSessionStates();
                if ((gameSessionState == GameSessionStates.MyTurnPlay ||
                     gameSessionState == GameSessionStates.EnemyTurnPlay ||
                     gameSessionState == GameSessionStates.OpponentChangesCards))
                    break;
                waitingService.Wait(1);
            }
            if (seconds == 0)
                throw new Exception($"Это не страница {GetType()}");

            inputEmulator.Send("{ESC}");

            seconds = 30;
            for (; seconds != 0; seconds--)

            {
                if (stateChecker.GetCurrentGameSessionStates() ==
                   GameSessionStates.GiveUpMessageBox)
                    break;
                waitingService.Wait(1);
            }
            if (seconds == 0)
                throw new Exception($"Это не страница {GetType()}");

            inputEmulator.Send("{ENTER}");

            return new MatchResultsRewardsScreenPage(
                stateChecker, waitingService, inputEmulator, game);
        }

        protected override bool VerifyingPage()
        {
            return stateChecker.GetCurrentGameSessionStates() !=
                GameSessionStates.Unknown;
        }
    }
}