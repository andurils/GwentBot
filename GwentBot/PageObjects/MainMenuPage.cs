using GwentBot.GameInput;
using GwentBot.PageObjects.Abstract;
using GwentBot.PageObjects.Elements;
using GwentBot.StateAbstractions;

namespace GwentBot.PageObjects
{
    internal class MainMenuPage : PageObject
    {
        public MainMenuPage(
            IGwentStateChecker stateChecker,
            IWaitingService waitingService,
            IInputDeviceEmulator inputEmulator) :
            base(stateChecker, waitingService, inputEmulator)
        {
            Notifications = new NotificationsElement(stateChecker, waitingService, inputEmulator);
        }

        internal NotificationsElement Notifications { get; }

        internal ArenaModePage GotoArenaModePage()
        {
            inputEmulator.MouseClick(565, 258);
            return new ArenaModePage(stateChecker, waitingService, inputEmulator);
        }

        internal GameModesPage GotoGameModesPage()
        {
            inputEmulator.MouseClick(428, 254);
            return new GameModesPage(stateChecker, waitingService, inputEmulator);
        }

        protected override bool VerifyingPage()
        {
            return stateChecker.GetCurrentGlobalGameStates() ==
                GlobalGameStates.MainMenu;
        }
    }
}