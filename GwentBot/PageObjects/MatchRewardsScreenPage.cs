using GwentBot.Model;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;
using System;

namespace GwentBot.PageObjects
{
    internal class MatchRewardsScreenPage : PageObject
    {
        public MatchRewardsScreenPage(
            IGwentStateChecker gwentStateChecker, IWaitingService waitingService, Game game) :
            base(gwentStateChecker, waitingService)
        {
        }

        protected override bool VerifyingPage()
        {
            throw new NotImplementedException();
        }
    }
}