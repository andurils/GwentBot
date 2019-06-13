using System;
using AutoIt;
using GwentBot.Model;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;

namespace GwentBot.PageObjects
{
    internal class GameSessionPage : PageObject
    {
        internal readonly Game game;

        internal GameSessionPage(
            IGwentStateChecker gwentStateChecker, IWaitingService waitingService, Game game) :
            base(gwentStateChecker, waitingService)
        {
            this.game = game;
        }

        internal bool MyTurnPlay
        {
            get
            {
                return gwentStateChecker.GetCurrentGameSessionStates() ==
                    GameSessionStates.MyTurnPlay;
            }
        }

        internal MatchResultsRewardsScreenPage GiveUp()
        {
            var gameSessionState = GameSessionStates.Unknown;

            if (gwentStateChecker.GetCurrentGameSessionStates() ==
                GameSessionStates.OpponentSurrenderedMessageBox)
            {
                AutoItX.MouseClick("left", 427, 275);
                return new MatchResultsRewardsScreenPage(gwentStateChecker, waitingService, game);
            }

            int seconds = 30;
            for (; seconds != 0; seconds--)
            {
                gameSessionState = gwentStateChecker.GetCurrentGameSessionStates();
                if ((gameSessionState == GameSessionStates.MyTurnPlay ||
                     gameSessionState == GameSessionStates.EnemyTurnPlay ||
                     gameSessionState == GameSessionStates.OpponentChangesCards))
                    break;
                waitingService.Wait(1);
            }
            if (seconds == 0)
                throw new Exception($"Это не страница {GetType()}");

            AutoItX.Send("{ESC}");

            seconds = 30;
            for (; seconds != 0; seconds--)

            {
                if (gwentStateChecker.GetCurrentGameSessionStates() ==
                   GameSessionStates.GiveUpMessageBox)
                    break;
                waitingService.Wait(1);
            }
            if (seconds == 0)
                throw new Exception($"Это не страница {GetType()}");

            AutoItX.Send("{ENTER}");

            return new MatchResultsRewardsScreenPage(gwentStateChecker, waitingService, game);
        }

        protected override bool VerifyingPage()
        {
            return gwentStateChecker.GetCurrentGameSessionStates() !=
                GameSessionStates.Unknown;
        }
    }
}