// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using AutoIt;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;
using System;

namespace GwentBot.PageObjects
{
    internal class MainMenuPage : PageObject
    {
        public MainMenuPage(IGwentStateChecker gwentStateChecker, IWaitingService waitingService)
            : base(gwentStateChecker, waitingService)
        {
        }

        protected override bool VerifyingPage()
        {
            return this.gwentStateChecker.GetCurrentGlobalGameStates() ==
                GlobalGameStates.MainMenu;
        }

        internal GameModesPage GoToGameModesPage()
        {
            AutoItX.MouseClick("left", 425, 240, 1);
            return new GameModesPage(this.gwentStateChecker, this.waitingService);
        }

        internal ArenaModePage GoToArenaModePage()
        {
            AutoItX.MouseClick("left", 565, 258, 1);
            return new ArenaModePage(this.gwentStateChecker, this.waitingService);
        }
    }
}