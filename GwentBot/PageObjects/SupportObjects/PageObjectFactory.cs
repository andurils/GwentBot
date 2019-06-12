using AutoIt;
using GwentBot.Model;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;

namespace GwentBot.PageObjects.SupportObjects
{
    internal class PageObjectFactory
    {
        protected readonly IGwentStateChecker gwentStateChecker;
        protected readonly IWaitingService waitingService;

        public PageObjectFactory(
            IGwentStateChecker gwentStateChecker, IWaitingService waitingServic)
        {
            this.gwentStateChecker = gwentStateChecker;
            this.waitingService = waitingService;
        }

        internal void CheckAndClearGlobalMessageBoxes()
        {
            var globalMessageBoxes = gwentStateChecker.GetCurrentGlobalMessageBoxes();
            if (globalMessageBoxes != GlobalMessageBoxes.NoMessageBoxes)
            {
                AutoItX.MouseClick("left", 427, 275);
            }
        }
    }
}