using GwentBot.GameInput;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;

namespace GwentBot.PageObjects
{
    internal class WelcomeScreen : PageObject
    {
        public WelcomeScreen(
            IGwentStateChecker stateChecker,
            IWaitingService waitingService,
            IInputDeviceEmulator inputEmulator) :
            base(stateChecker, waitingService, inputEmulator)
        {
        }

        internal MainMenuPage GotoMainMenu()
        {
            inputEmulator.MouseClick(427, 453);
            return new MainMenuPage(stateChecker, waitingService, inputEmulator);
        }

        protected override bool VerifyingPage()
        {
            return stateChecker.GetCurrentStartGameStates() ==
                   StartGameStates.WelcomeScreen;
        }

        protected override void WaitingGameReadiness(int seconds = 30)
        {
            do
            {
                waitingService.Wait(1);
            } while (stateChecker.GetCurrentStartGameStates() ==
                     StartGameStates.GameLoadingScreen);
            base.WaitingGameReadiness();
        }
    }
}