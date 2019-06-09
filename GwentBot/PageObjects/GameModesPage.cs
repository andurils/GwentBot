using AutoIt;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;

namespace GwentBot.PageObjects
{
    internal class GameModesPage : PageObject
    {
        public GameModesPage(
            IGwentStateChecker gwentStateChecker, IWaitingService waitingService) :
            base(gwentStateChecker, waitingService)
        {
        }

        internal MainMenuPage GotoMainMenuPage()
        {
            AutoItX.MouseClick("left", 427, 453);
            return new MainMenuPage(gwentStateChecker, waitingService);
        }

        protected override bool VerifyingPage()
        {
            return gwentStateChecker.GetCurrentGlobalGameStates() ==
                GlobalGameStates.GameModesTab;
        }
    }
}