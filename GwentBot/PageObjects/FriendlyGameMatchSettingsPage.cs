using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;
using System;
using GwentBot.GameInput;
using GwentBot.Model;

namespace GwentBot.PageObjects
{
    internal class FriendlyGameMatchSettingsPage : PageObject
    {
        internal FriendlyGameMatchSettingsPage(
            IGwentStateChecker stateChecker,
            IWaitingService waitingService,
            IInputDeviceEmulator inputEmulator) :
            base(stateChecker, waitingService, inputEmulator)
        {
        }

        internal MainMenuPage CancelFriendlyGame()
        {
            inputEmulator.MouseClick(428, 457);
            do
            {
                waitingService.Wait(1);
            } while (stateChecker.GetCurrentFriendlyGameStartStates() !=
                FriendlyGameStartStates.CancelGameMessageBox);
            inputEmulator.Send("{ENTER}");
            return new MainMenuPage(stateChecker, waitingService, inputEmulator);
        }

        internal MulliganPage StartGame()
        {
            inputEmulator.MouseClick(430, 201);
            Game game = new Game(new Deck("DefaultGame"), new User("MyName"));
            return new MulliganPage(stateChecker, waitingService, inputEmulator, game);
        }

        protected override bool VerifyingPage()
        {
            return stateChecker.GetCurrentFriendlyGameStartStates() ==
                FriendlyGameStartStates.MatchSettings;
        }

        protected override void WaitingGameReadiness(int seconds = 30)
        {
            do
            {
                waitingService.Wait(1);
            } while (stateChecker.GetCurrentFriendlyGameStartStates() ==
            FriendlyGameStartStates.LoadingMatchSettings);

            base.WaitingGameReadiness();
        }
    }
}