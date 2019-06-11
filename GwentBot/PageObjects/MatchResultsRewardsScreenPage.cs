using GwentBot.Model;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
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
                    SaveLogExpImage(game.MatchRewardsScreenBitmap);
                    ClickNextButton();
                    break;

                case null:
                    throw new Exception($"Это не страница {GetType()}");
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