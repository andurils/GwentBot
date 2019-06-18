using GwentBot.GameInput;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;

namespace GwentBot.PageObjects.Elements
{
    internal class NotificationsElement : PageObject
    {
        internal NotificationsElement(
            IGwentStateChecker stateChecker,
            IWaitingService waitingService,
            IInputDeviceEmulator inputEmulator) :
            base(stateChecker, waitingService, inputEmulator)
        {
        }

        internal FriendlyGameMatchSettingsPage AcceptFriendlyDuel()
        {
            AcceptAnyNotification();
            return new FriendlyGameMatchSettingsPage(stateChecker, waitingService, inputEmulator);
        }

        internal RewardsTabPage AcceptRewards()
        {
            AcceptAnyNotification();
            return new RewardsTabPage(stateChecker, waitingService, inputEmulator);
        }

        internal Notifications CheckReceivedNotifications()
        {
            return stateChecker.GetCurrentNotifications();
        }

        protected override bool VerifyingPage()
        {
            return true;
        }

        private void AcceptAnyNotification()
        {
            inputEmulator.MouseClick(823, 66);
        }
    }
}