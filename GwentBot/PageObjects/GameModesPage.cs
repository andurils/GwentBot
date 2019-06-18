using GwentBot.GameInput;
using GwentBot.Model;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;

namespace GwentBot.PageObjects
{
    internal class GameModesPage : PageObject
    {
        public GameModesPage(
            IGwentStateChecker stateChecker,
            IWaitingService waitingService,
            IInputDeviceEmulator inputEmulator) :
            base(stateChecker, waitingService, inputEmulator)
        {
        }

        internal MulliganPage GotoClassicGameMode()
        {
            inputEmulator.MouseClick(427, 194);
            Game game = new Game(new Deck("DefaultGame"), new User("MyName"));
            return new MulliganPage(stateChecker, waitingService, inputEmulator, game);
        }

        internal MainMenuPage GotoMainMenuPage()
        {
            inputEmulator.MouseClick(427, 453);
            return new MainMenuPage(stateChecker, waitingService, inputEmulator);
        }

        internal MulliganPage GotoSeasonalGameMode()
        {
            inputEmulator.MouseClick(242, 194);
            Game game = new Game(new Deck("DefaultGame"), new User("MyName"));
            return new MulliganPage(stateChecker, waitingService, inputEmulator, game);
        }

        internal MulliganPage GotoTrainingGameMode()
        {
            inputEmulator.MouseClick(616, 195);
            Game game = new Game(new Deck("DefaultGame"), new User("MyName"));
            return new MulliganPage(stateChecker, waitingService, inputEmulator, game);
        }

        protected override bool VerifyingPage()
        {
            return stateChecker.GetCurrentGlobalGameStates() ==
                GlobalGameStates.GameModesTab;
        }
    }
}