using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;

namespace GwentBot.PageObjects
{
    internal class GameLoadingScreenPage : PageObject
    {
        public GameLoadingScreenPage(
            IGwentStateChecker gwentStateChecker, IWaitingService waitingService) :
            base(gwentStateChecker, waitingService)
        {
        }

        internal WelcomeScreen GotoWelcomeScreen()
        {
            AutoIt.AutoItX.MouseClick("left", 300, 300);
            return new WelcomeScreen(gwentStateChecker, waitingService);
        }

        protected override bool VerifyingPage()
        {
            return gwentStateChecker.GetCurrentStartGameStates() ==
                   StartGameStates.GameLoadingScreen;
        }
    }
}