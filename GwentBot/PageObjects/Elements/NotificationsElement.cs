// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using AutoIt;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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