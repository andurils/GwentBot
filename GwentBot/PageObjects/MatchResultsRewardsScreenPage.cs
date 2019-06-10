using GwentBot.Model;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;
using System;
using AutoIt;

namespace GwentBot.PageObjects
{
    internal class MatchResultsRewardsScreenPage : PageObject
    {
        internal readonly Game game;

        public MatchResultsRewardsScreenPage(
            IGwentStateChecker gwentStateChecker, IWaitingService waitingService, Game game) :
            base(gwentStateChecker, waitingService)
        {
            this.game = game;
        }

        internal GameModesPage ClosePageStatistics()
        {
            game.MatchResultsScreenBitmap = gwentStateChecker.GetGameScreenshotBitmap();
            ClickNextButton();
            bool? onlineGame = null;
            for (int tick = 0; tick < 10; tick++)
            {
                if (gwentStateChecker.GetCurrentGameSessionStates() ==
                    GameSessionStates.MatchRewardsScreen)
                {
                    onlineGame = true;
                    break;
                }

                if (gwentStateChecker.GetCurrentGlobalGameStates() ==
                    GlobalGameStates.HeavyLoading)
                {
                    onlineGame = false;
                    break;
                }
                waitingService.Wait(1);
            }

            switch (onlineGame)
            {
                case true:
                    game.MatchRewardsScreenBitmap = gwentStateChecker.GetGameScreenshotBitmap();
                    ClickNextButton();
                    break;

                case null:
                    throw new Exception($"Это не страница {GetType()}");
                    break;
            }

            return new GameModesPage(gwentStateChecker, waitingService);
        }

        protected override bool VerifyingPage()
        {
            return (gwentStateChecker.GetCurrentGameSessionStates() ==
                    GameSessionStates.MatchResultsScreen);
        }

        private void ClickNextButton()
        {
            AutoItX.MouseClick("left", 427, 453);
        }
    }
}