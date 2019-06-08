using AutoIt;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;

namespace GwentBot.PageObjects.Elements
{
    internal class NotificationsElement : PageObject
    {
        internal NotificationsElement(
            IGwentStateChecker gwentStateChecker, IWaitingService waitingService) :
            base(gwentStateChecker, waitingService)
        {
        }

        internal FriendlyGameMatchSettingsPage AcceptFriendlyDuel()
        {
            AcceptAnyNotification();
            return new FriendlyGameMatchSettingsPage(gwentStateChecker, waitingService);
        }

        internal RewardsTabPage AcceptRewards()
        {
            AcceptAnyNotification();
            return new RewardsTabPage(gwentStateChecker, waitingService);
        }

        internal Notifications CheckReceivedNotifications()
        {
            return this.gwentStateChecker.GetCurrentNotifications();
        }

        protected override bool VerifyingPage()
        {
            return true;
        }

        private void AcceptAnyNotification()
        {
            AutoItX.MouseClick("left", 823, 66);
        }
    }
}