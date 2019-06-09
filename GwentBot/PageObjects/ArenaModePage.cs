using AutoIt;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;

namespace GwentBot.PageObjects
{
    internal class ArenaModePage : PageObject
    {
        public ArenaModePage(
            IGwentStateChecker gwentStateChecker, IWaitingService waitingService) :
            base(gwentStateChecker, waitingService)
        {
        }

        internal MainMenuPage GotoMainMenuPage()
        {
            AutoItX.MouseClick("left", 428, 457);
            return new MainMenuPage(gwentStateChecker, waitingService);
        }

        protected override bool VerifyingPage()
        {
            return gwentStateChecker.GetCurrentGlobalGameStates() ==
                GlobalGameStates.ArenaModeTab;
        }
    }
}