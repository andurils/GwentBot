using GwentBot.GameInput;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;

namespace GwentBot.PageObjects
{
    internal class RewardsTabPage : PageObject
    {
        internal RewardsTabPage(
            IGwentStateChecker stateChecker,
            IWaitingService waitingService,
            IInputDeviceEmulator inputEmulator) :
            base(stateChecker, waitingService, inputEmulator)
        {
        }

        internal MainMenuPage CloseRewardsTab()
        {
            inputEmulator.MouseClick(427, 453);
            return new MainMenuPage(stateChecker, waitingService, inputEmulator);
        }

        protected override bool VerifyingPage()
        {
            return stateChecker.GetCurrentNotifications() ==
                Notifications.RewardsTab;
        }
    }
}