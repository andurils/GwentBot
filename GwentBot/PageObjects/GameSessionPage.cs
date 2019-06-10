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
            do
            {
                waitingService.Wait(1);
                gameSessionState = gwentStateChecker.GetCurrentGameSessionStates();
            } while ((gameSessionState != GameSessionStates.MyTurnPlay) &
                     (gameSessionState != GameSessionStates.EnemyTurnPlay &
                      gameSessionState != GameSessionStates.OpponentChangesCards));

            AutoItX.Send("{ESC}");
            do
            {
                waitingService.Wait(1);
            } while (gwentStateChecker.GetCurrentGameSessionStates() !=
                GameSessionStates.GiveUpMessageBox);
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