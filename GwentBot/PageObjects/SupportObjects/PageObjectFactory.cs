using AutoIt;
using GwentBot.ComputerVision;
using GwentBot.Model;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;

namespace GwentBot.PageObjects.SupportObjects
{
    internal class PageObjectFactory
    {
        protected readonly IGwentStateChecker gwentStateChecker;
        protected readonly IWaitingService waitingService;

        public PageObjectFactory()
        {
            var screenShotCreator = new GwentWindowScreenShotCreator();
            gwentStateChecker = new OpenCvGwentStateChecker(screenShotCreator);

            this.waitingService = new DefaultWaitingService();

            AutoItX.AutoItSetOption("MouseCoordMode", 2);
            AutoItX.AutoItSetOption("PixelCoordMode", 2);
        }

        internal void CheckAndClearGlobalMessageBoxes()
        {
            var globalMessageBoxes = gwentStateChecker.GetCurrentGlobalMessageBoxes();
            if (globalMessageBoxes != GlobalMessageBoxes.NoMessageBoxes)
            {
                AutoItX.MouseClick("left", 427, 275);
            }
        }

        internal void CheckAndClearOpponentSurrenderedMessageBox()
        {
            if (gwentStateChecker.GetCurrentGameSessionStates() ==
                GameSessionStates.OpponentSurrenderedMessageBox)
            {
                AutoItX.MouseClick("left", 427, 275);
                new MatchResultsRewardsScreenPage(gwentStateChecker, waitingService,
                    new Game(new Deck("empty"), new User("empty")))
                    .ClosePageStatistics();
            }
        }
    }
}