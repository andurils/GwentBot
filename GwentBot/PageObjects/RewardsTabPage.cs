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
            return new MainMenuPage(this.gwentStateChecker, this.waitingService);
        }

        protected override bool VerifyingPage()
        {
            return gwentStateChecker.GetCurrentNotifications() ==
                Notifications.RewardsTab;
        }
    }
}