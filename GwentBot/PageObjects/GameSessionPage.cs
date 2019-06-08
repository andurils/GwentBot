// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using AutoIt;
using GwentBot.Model;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;

namespace GwentBot.PageObjects
{
    internal class GameSessionPage : PageObject
    {
        internal Game game;

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

        internal MatchResultsScreenPage GiveUp()
        {
            do
            {
                waitingService.Wait(1);
            } while (gwentStateChecker.GetCurrentGameSessionStates() !=
                GameSessionStates.MyTurnPlay);

            AutoItX.Send("{ESC}");
            do
            {
                waitingService.Wait(1);
            } while (gwentStateChecker.GetCurrentGameSessionStates() !=
                GameSessionStates.GiveUpMessageBox);
            AutoItX.Send("{ENTER}");

            return new MatchResultsScreenPage(gwentStateChecker, waitingService, game);
        }

        protected override bool VerifyingPage()
        {
            return gwentStateChecker.GetCurrentGameSessionStates() !=
                GameSessionStates.Unknown;
        }
    }
}