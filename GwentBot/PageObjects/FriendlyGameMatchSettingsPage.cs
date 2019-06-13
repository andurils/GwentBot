using AutoIt;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;
using System;
using GwentBot.Model;

namespace GwentBot.PageObjects
{
    internal class FriendlyGameMatchSettingsPage : PageObject
    {
        internal FriendlyGameMatchSettingsPage(
            IGwentStateChecker gwentStateChecker, IWaitingService waitingService) :
            base(gwentStateChecker, waitingService)
        {
        }

        internal MainMenuPage CancelFriendlyGame()
        {
            AutoItX.MouseClick("left", 428, 457);
            do
            {
                waitingService.Wait(1);
            } while (gwentStateChecker.GetCurrentFriendlyGameStartStates() !=
                FriendlyGameStartStates.CancelGameMessageBox);
            AutoItX.Send("{ENTER}");
            return new MainMenuPage(gwentStateChecker, waitingService);
        }

        internal MulliganPage StartGame()
        {
            AutoItX.MouseClick("left", 430, 201);
            Game game = new Game(new Deck("DefaultGame"), new User("MyName"));
            return new MulliganPage(gwentStateChecker, waitingService, game);
        }

        protected override bool VerifyingPage()
        {
            return gwentStateChecker.GetCurrentFriendlyGameStartStates() ==
                FriendlyGameStartStates.MatchSettings;
        }

        protected override void WaitingGameReadiness(int seconds = 30)
        {
            do
            {
                waitingService.Wait(1);
            } while (gwentStateChecker.GetCurrentFriendlyGameStartStates() ==
            FriendlyGameStartStates.LoadingMatchSettings);

            base.WaitingGameReadiness();
        }
    }
}