using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoIt;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;

namespace GwentBot.PageObjects
{
    internal class WelcomeScreen : PageObject
    {
        public WelcomeScreen(
            IGwentStateChecker gwentStateChecker, IWaitingService waitingService) :
            base(gwentStateChecker, waitingService)
        {
        }

        internal MainMenuPage GotoMainMenu()
        {
            AutoItX.MouseClick("left", 427, 453);
            return new MainMenuPage(gwentStateChecker, waitingService);
        }

        protected override bool VerifyingPage()
        {
            return gwentStateChecker.GetCurrentStartGameStates() ==
                   StartGameStates.WelcomeScreen;
        }

        protected override void WaitingGameReadiness(int seconds = 30)
        {
            do
            {
                waitingService.Wait(1);
            } while (gwentStateChecker.GetCurrentStartGameStates() ==
                     StartGameStates.GameLoadingScreen);
            base.WaitingGameReadiness();
        }
    }
}