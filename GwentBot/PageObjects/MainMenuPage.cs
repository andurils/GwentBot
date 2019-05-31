// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using AutoIt;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;

namespace GwentBot.PageObjects
{
    internal class MainMenuPage : PageObject
    {
        public MainMenuPage(
            IGwentStateChecker gwentStateChecker, IWaitingService waitingService) :
            base(gwentStateChecker, waitingService)
        {
        }

        //TODO: Добавить элемент с нотификациями

        internal ArenaModePage GotoArenaModePage()
        {
            AutoItX.MouseClick("left", 565, 258);
            return new ArenaModePage(this.gwentStateChecker, this.waitingService);
        }

        internal GameModesPage GotoGameModesPage()
        {
            AutoItX.MouseClick("left", 425, 240);
            return new GameModesPage(this.gwentStateChecker, this.waitingService);
        }

        protected override bool VerifyingPage()
        {
            return this.gwentStateChecker.GetCurrentGlobalGameStates() ==
                GlobalGameStates.MainMenu;
        }
    }
}