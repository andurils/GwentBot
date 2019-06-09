using AutoIt;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;

namespace GwentBot.PageObjects
{
    internal class RewardsTabPage : PageObject
    {
        internal RewardsTabPage(
            IGwentStateChecker gwentStateChecker, IWaitingService waitingService) :
            base(gwentStateChecker, waitingService)
        {
        }

        internal MainMenuPage CloseRewardsTab()
        {
            AutoItX.MouseClick("left", 427, 453);
            return new MainMenuPage(gwentStateChecker, waitingService);
        }

        protected override bool VerifyingPage()
        {
            return gwentStateChecker.GetCurrentNotifications() ==
                Notifications.RewardsTab;
        }
    }
}