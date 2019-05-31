// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using AutoIt;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return new MainMenuPage(gwentStateChecker, waitingService);
        }

        internal GameSessionPage StartGame()
        {
            AutoItX.MouseClick("left", 430, 201);
            return new GameSessionPage(gwentStateChecker, waitingService);
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