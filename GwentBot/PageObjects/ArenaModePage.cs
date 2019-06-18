using GwentBot.GameInput;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;

namespace GwentBot.PageObjects
{
    internal class ArenaModePage : PageObject
    {
        public ArenaModePage(
            IGwentStateChecker stateChecker,
            IWaitingService waitingService,
            IInputDeviceEmulator inputEmulator) :
            base(stateChecker, waitingService, inputEmulator)
        {
        }

        internal MainMenuPage GotoMainMenuPage()
        {
            inputEmulator.MouseClick(428, 457);
            return new MainMenuPage(stateChecker, waitingService, inputEmulator);
        }

        protected override bool VerifyingPage()
        {
            return stateChecker.GetCurrentGlobalGameStates() ==
                GlobalGameStates.ArenaModeTab;
        }
    }
}