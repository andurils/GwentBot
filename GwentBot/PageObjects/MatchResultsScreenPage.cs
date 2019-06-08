using GwentBot.Model;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;
using System;

namespace GwentBot.PageObjects
{
    internal class MatchResultsScreenPage : PageObject
    {
        public MatchResultsScreenPage(
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