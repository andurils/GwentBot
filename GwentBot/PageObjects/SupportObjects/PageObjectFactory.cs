using System;
using GwentBot.ComputerVision;
using GwentBot.GameInput;
using GwentBot.Model;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;

namespace GwentBot.PageObjects.SupportObjects
{
    internal class PageObjectFactory
    {
        protected readonly IGwentStateChecker gwentStateChecker;
        protected readonly IInputDeviceEmulator inputEmulator;
        protected readonly IWaitingService waitingService;

        public PageObjectFactory()
        {
            var screenShotCreator = new GwentWindowScreenShotCreator();
            gwentStateChecker = new OpenCvGwentStateChecker(screenShotCreator);
            inputEmulator = new AutoitInputDeviceEmulator();

            this.waitingService = new DefaultWaitingService();
        }

        internal void CheckAndClearGameSessionExceptionMessageBoxes()
        {
            if (gwentStateChecker.GetCurrentGameSessionExceptionMessageBoxes() !=
                GameSessionExceptionMessageBoxes.NoMessageBoxes)
            {
                inputEmulator.MouseClick(427, 275);
                new MatchResultsRewardsScreenPage(gwentStateChecker, waitingService, inputEmulator,
                        new Game(new Deck("empty"), new User("empty")))
                    .ClosePageStatistics();
            }
        }

        internal void CheckAndClearGlobalMessageBoxes()
        {
            var globalMessageBoxes = gwentStateChecker.GetCurrentGlobalMessageBoxes();
            if (globalMessageBoxes != GlobalMessageBoxes.NoMessageBoxes)
            {
                inputEmulator.MouseClick(427, 275);
            }
        }

        internal void CheckAndClearOpponentSurrenderedMessageBox()
        {
            if (gwentStateChecker.GetCurrentGameSessionStates() ==
                GameSessionStates.OpponentSurrenderedMessageBox)
            {
                inputEmulator.MouseClick(427, 275);
                new MatchResultsRewardsScreenPage(gwentStateChecker, waitingService, inputEmulator,
                    new Game(new Deck("empty"), new User("empty")))
                    .ClosePageStatistics();
            }
        }

        internal MainMenuPage StartGame()
        {
            if (gwentStateChecker.GetCurrentStartGameStates() !=
               StartGameStates.GameLoadingScreen)
                return null;

            return new GameLoadingScreenPage(gwentStateChecker, waitingService, inputEmulator)
                .GotoWelcomeScreen()
                .GotoMainMenu();
        }
    }
}