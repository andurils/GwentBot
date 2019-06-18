using GwentBot.Model;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using GwentBot.GameInput;

namespace GwentBot.PageObjects
{
    internal class MatchResultsRewardsScreenPage : PageObject
    {
        internal readonly Game game;

        public MatchResultsRewardsScreenPage(
            IGwentStateChecker stateChecker,
            IWaitingService waitingService,
            IInputDeviceEmulator inputEmulator,
            Game game) :
            base(stateChecker, waitingService, inputEmulator)
        {
            this.game = game;
        }

        internal GameModesPage ClosePageStatistics()
        {
            var resultsPage = stateChecker.GetCurrentGameSessionStates();
            switch (resultsPage)
            {
                case GameSessionStates.MatchResultsScreen:
                    game.MatchResultsScreenBitmap = stateChecker.GetGameScreenshotBitmap();
                    ClickNextButton();
                    break;

                case GameSessionStates.MatchRewardsScreen:
                    game.MatchRewardsScreenBitmap = stateChecker.GetGameScreenshotBitmap();
                    SaveLogExpImage(game.MatchRewardsScreenBitmap);
                    ClickNextButton();
                    return new GameModesPage(stateChecker, waitingService, inputEmulator);
            }

            bool? onlineGame = null;
            for (int tick = 0; tick < 10; tick++)
            {
                if (stateChecker.GetCurrentGameSessionStates() ==
                    GameSessionStates.MatchRewardsScreen)
                {
                    onlineGame = true;
                    break;
                }

                if (stateChecker.GetCurrentGlobalGameStates() ==
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
                    game.MatchRewardsScreenBitmap = stateChecker.GetGameScreenshotBitmap();
                    SaveLogExpImage(game.MatchRewardsScreenBitmap);
                    ClickNextButton();
                    break;

                case null:
                    throw new Exception($"Это не страница {GetType()}");
            }

            return new GameModesPage(stateChecker, waitingService, inputEmulator);
        }

        protected override bool VerifyingPage()
        {
            return (stateChecker.GetCurrentGameSessionStates() ==
                    GameSessionStates.MatchResultsScreen) ||
                   (stateChecker.GetCurrentGameSessionStates() ==
                   GameSessionStates.MatchRewardsScreen);
        }

        private void ClickNextButton()
        {
            inputEmulator.MouseClick(427, 453);
        }

        private void SaveLogExpImage(byte[] bitmapByte)
        {
            Bitmap reciveBitmap;

            using (var ms = new MemoryStream(bitmapByte))
            {
                reciveBitmap = (Bitmap)Image.FromStream(ms);
            }
            Rectangle levelAndExpRect = new Rectangle(550, 160, 130, 130);
            Bitmap cropBitmap = reciveBitmap.Clone(levelAndExpRect, reciveBitmap.PixelFormat);
            var now = DateTime.Now;

            cropBitmap
                .Save($"TestImg/{now.Day}.{now.Month}.{now.Year} {now.Hour}-{now.Minute}-{now.Second}.bmp");
        }
    }
}