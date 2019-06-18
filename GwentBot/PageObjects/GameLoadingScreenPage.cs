using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GwentBot.GameInput;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;

namespace GwentBot.PageObjects
{
    internal class GameLoadingScreenPage : PageObject
    {
        public GameLoadingScreenPage(
            IGwentStateChecker stateChecker,
            IWaitingService waitingService,
            IInputDeviceEmulator inputEmulator) :
            base(stateChecker, waitingService, inputEmulator)
        {
        }

        internal WelcomeScreen GotoWelcomeScreen()
        {
            inputEmulator.MouseClick(427, 453);
            return new WelcomeScreen(stateChecker, waitingService, inputEmulator);
        }

        protected override bool VerifyingPage()
        {
            return stateChecker.GetCurrentStartGameStates() ==
                   StartGameStates.GameLoadingScreen;
        }
    }
}