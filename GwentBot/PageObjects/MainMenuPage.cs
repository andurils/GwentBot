using AutoIt;
using GwentBot.PageObjects.Abstract;
using GwentBot.PageObjects.Elements;
using GwentBot.StateAbstractions;

namespace GwentBot.PageObjects
{
    internal class MainMenuPage : PageObject
    {
        public MainMenuPage(
            IGwentStateChecker gwentStateChecker, IWaitingService waitingService) :
            base(gwentStateChecker, waitingService)
        {
            Notifications = new NotificationsElement(gwentStateChecker, waitingService);
        }

        internal NotificationsElement Notifications { get; }

        internal ArenaModePage GotoArenaModePage()
        {
            AutoItX.MouseClick("left", 565, 258);
            return new ArenaModePage(gwentStateChecker, waitingService);
        }

        internal GameModesPage GotoGameModesPage()
        {
            AutoItX.MouseClick("left", 428, 254);
            return new GameModesPage(gwentStateChecker, waitingService);
        }

        protected override bool VerifyingPage()
        {
            return gwentStateChecker.GetCurrentGlobalGameStates() ==
                GlobalGameStates.MainMenu;
        }
    }
}