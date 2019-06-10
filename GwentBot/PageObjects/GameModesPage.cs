using AutoIt;
using GwentBot.Model;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;

namespace GwentBot.PageObjects
{
    internal class GameModesPage : PageObject
    {
        public GameModesPage(
            IGwentStateChecker gwentStateChecker, IWaitingService waitingService) :
            base(gwentStateChecker, waitingService)
        {
        }

        internal MulliganPage GotoClassicGameMode()
        {
            AutoItX.MouseClick("left", 427, 194);
            Game game = new Game(new Deck("DefaultGame"), new User("MyName"));
            return new MulliganPage(gwentStateChecker, waitingService, game);
        }

        internal MainMenuPage GotoMainMenuPage()
        {
            AutoItX.MouseClick("left", 427, 453);
            return new MainMenuPage(gwentStateChecker, waitingService);
        }

        internal MulliganPage GotoSeasonalGameMode()
        {
            AutoItX.MouseClick("left", 242, 194);
            Game game = new Game(new Deck("DefaultGame"), new User("MyName"));
            return new MulliganPage(gwentStateChecker, waitingService, game);
        }

        internal MulliganPage GotoTrainingGameMode()
        {
            AutoItX.MouseClick("left", 616, 195);
            Game game = new Game(new Deck("DefaultGame"), new User("MyName"));
            return new MulliganPage(gwentStateChecker, waitingService, game);
        }

        protected override bool VerifyingPage()
        {
            return gwentStateChecker.GetCurrentGlobalGameStates() ==
                GlobalGameStates.GameModesTab;
        }
    }
}