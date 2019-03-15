﻿using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;

namespace GwentBot.ComputerVision
{
    internal class OpenCvGwentStateChecker : IGwentStateChecker
    {
        public OpenCvGwentStateChecker(IWindowScreenShotCreator screenShotCreator)
        {
            ScreenShotCreator = screenShotCreator;
        }

        public IWindowScreenShotCreator ScreenShotCreator { get; }

        public FriendlyGameStartStates GetCurrentFriendlyGameStartStates()
        {
            using (Mat gameScreen = ScreenShotCreator.GetGameScreenshot().ToMat())
            {
                foreach (int itemValue in Enum.GetValues(typeof(FriendlyGameStartStates)))
                {
                    var item = (FriendlyGameStartStates)itemValue;

                    switch (item)
                    {
                        case FriendlyGameStartStates.LoadingMatchSettings:
                            if (CheckFgssLoadingMatchSettings(gameScreen))
                                return item;
                            break;

                        case FriendlyGameStartStates.MatchSettings:
                            if (CheckFgssMatchSettings(gameScreen))
                                return item;
                            break;

                        case FriendlyGameStartStates.WaitingReadinessOpponent:
                            if (CheckFgssWaitingReadinessOpponent(gameScreen))
                                return item;
                            break;
                    }
                }
            }
            return FriendlyGameStartStates.Unknown;
        }

        public CoinTossStates GetCurrentCoinTossStates()
        {
            using (Mat gameScreen = ScreenShotCreator.GetGameScreenshot().ToMat())
            {
                foreach (int itemValue in Enum.GetValues(typeof(CoinTossStates)))
                {
                    var item = (CoinTossStates)itemValue;

                    switch (item)
                    {
                        case CoinTossStates.StartToss:
                            if (CheckCtsStartToss(gameScreen))
                                return item;
                            break;
                        case CoinTossStates.СoinWon:
                            if (CheckCtsСoinWon(gameScreen))
                                return item;
                            break;
                        case CoinTossStates.CoinLost:
                            if (CheckCtsCoinLost(gameScreen))
                                return item;
                            break;
                    }
                }
            }
            return CoinTossStates.Unknown;
        }

        public GlobalGameStates GetCurrentGlobalGameStates()
        {
            using (Mat gameScreen = ScreenShotCreator.GetGameScreenshot().ToMat())
            {
                foreach (int itemValue in Enum.GetValues(typeof(GlobalGameStates)))
                {
                    var item = (GlobalGameStates)itemValue;

                    switch (item)
                    {
                        case GlobalGameStates.ArenaModeTab:
                            if (CheckGgsArenaModeTab(gameScreen))
                                return item;
                            break;

                        case GlobalGameStates.HeavyLoading:
                            if (CheckGgsHeavyLoading(gameScreen))
                                return item;
                            break;

                        case GlobalGameStates.GameModesTab:
                            if (CheckGgsGameModesTab(gameScreen))
                                return item;
                            break;

                        case GlobalGameStates.MainMenu:
                            if (CheckGgsMainMenu(gameScreen))
                                return item;
                            break;
                    }
                }
            }
            return GlobalGameStates.Unknown;
        }

        public StartGameStates GetCurrentStartGameStates()
        {
            using (Mat gameScreen = ScreenShotCreator.GetGameScreenshot().ToMat())
            {
                foreach (int itemValue in Enum.GetValues(typeof(StartGameStates)))
                {
                    var item = (StartGameStates)itemValue;

                    switch (item)
                    {
                        case StartGameStates.GameLoadingScreen:
                            if (CheckSgsGameLoadingScreen(gameScreen))
                                return item;
                            break;

                        case StartGameStates.WelcomeScreen:
                            if (CheckSgsWelcomeScreen(gameScreen))
                                return item;
                            break;
                    }
                }
            }
            return StartGameStates.Unknown;
        }

        public GameSessionStates GetCurrentGameSessionStates()
        {
            using (Mat gameScreen = ScreenShotCreator.GetGameScreenshot().ToMat())
            {
                foreach (int itemValue in Enum.GetValues(typeof(GameSessionStates)))
                {
                    var item = (GameSessionStates)itemValue;

                    switch (item)
                    {
                        case GameSessionStates.Mulligan:
                            if (CheckGssMulligan(gameScreen))
                                return item;
                            break;
                        case GameSessionStates.OpponentChangesCards:
                            if (CheckGssOpponentChangesCards(gameScreen))
                                return item;
                            break;
                        case GameSessionStates.MyTurnPlay:
                            if (CheckGssMyTurnPlay(gameScreen))
                                return item;
                            break;
                        case GameSessionStates.EnemyTurnPlay:
                            if (CheckGssEnemyTurnPlay(gameScreen))
                                return item;
                            break;
                    }
                }
            }
            return GameSessionStates.Unknown;
        }

        #region GameSessionStates Checks

        private bool CheckGssMulligan(Mat gameScreen)
        {
            var fullRectGameScreen = new Rect(0, 0, gameScreen.Width, gameScreen.Height);
            using (var localGameScreen = new Mat(gameScreen, fullRectGameScreen))
            {
                var tempPos = PatternSearchRoi(localGameScreen,
                        new Mat(@"ComputerVision\PatternsForCV\GameSessionStates\Mulligan-Text.png"),
                        new Rect(320, 440, 110, 25));

                return (tempPos != Rect.Empty);
            }
        }

        private bool CheckGssOpponentChangesCards(Mat gameScreen)
        {
            var fullRectGameScreen = new Rect(0, 0, gameScreen.Width, gameScreen.Height);
            using (var localGameScreen = new Mat(gameScreen, fullRectGameScreen))
            {
                var rectROI = new Rect(290, 5, 280, 45);

                var originalImgROI = new Mat(
                    localGameScreen,
                    rectROI);

                var gameScreenEditImgROI = GetNoiseFreeText(originalImgROI, 80);
                var patternMat = new Mat(@"ComputerVision\PatternsForCV\GameSessionStates\OpponentChangesCards-Text.png");

                var tempPos = PatternSearch(gameScreenEditImgROI, patternMat);

                return (tempPos != Rect.Empty);
            }
        }

        private bool CheckGssMyTurnPlay(Mat gameScreen)
        {
            var fullRectGameScreen = new Rect(0, 0, gameScreen.Width, gameScreen.Height);
            using (var localGameScreen = new Mat(gameScreen, fullRectGameScreen))
            {
                var tempPos = PatternSearchRoi(localGameScreen,
                        new Mat(@"ComputerVision\PatternsForCV\GameSessionStates\MyTurnPlay-PassButton.png"),
                        new Rect(800, 190, 47, 50));

                return (tempPos != Rect.Empty);
            }
        }

        private bool CheckGssEnemyTurnPlay(Mat gameScreen)
        {
            var fullRectGameScreen = new Rect(0, 0, gameScreen.Width, gameScreen.Height);
            using (var localGameScreen = new Mat(gameScreen, fullRectGameScreen))
            {
                var tempPos = PatternSearchRoi(localGameScreen,
                        new Mat(@"ComputerVision\PatternsForCV\GameSessionStates\EnemyTurnPlaySrc-Button.png"),
                        new Rect(800, 190, 47, 50));

                return (tempPos != Rect.Empty);
            }
        }

        #endregion

        #region CoinTossStates Checks

        private bool CheckCtsStartToss(Mat gameScreen)
        {
            var fullRectGameScreen = new Rect(0, 0, gameScreen.Width, gameScreen.Height);
            using (var localGameScreen = new Mat(gameScreen, fullRectGameScreen))
            {
                var tempPos = PatternSearchRoi(localGameScreen,
                        new Mat(@"ComputerVision\PatternsForCV\CoinTossStates\StartToss.png"),
                        new Rect(380, 200, 90, 80));

                return (tempPos != Rect.Empty);
            }
        }

        private bool CheckCtsСoinWon(Mat gameScreen)
        {
            var fullRectGameScreen = new Rect(0, 0, gameScreen.Width, gameScreen.Height);
            using (var localGameScreen = new Mat(gameScreen, fullRectGameScreen))
            {
                var tempPos = PatternSearchRoi(localGameScreen,
                        new Mat(@"ComputerVision\PatternsForCV\CoinTossStates\СoinWon.png"),
                        new Rect(380, 200, 90, 80));

                return (tempPos != Rect.Empty);
            }
        }

        private bool CheckCtsCoinLost(Mat gameScreen)
        {
            var fullRectGameScreen = new Rect(0, 0, gameScreen.Width, gameScreen.Height);
            using (var localGameScreen = new Mat(gameScreen, fullRectGameScreen))
            {
                var tempPos = PatternSearchRoi(localGameScreen,
                        new Mat(@"ComputerVision\PatternsForCV\CoinTossStates\CoinLost.png"),
                        new Rect(380, 200, 90, 80));

                return (tempPos != Rect.Empty);
            }
        }

        #endregion

        #region FriendlyGameStartStates Checks

        private bool CheckFgssLoadingMatchSettings(Mat gameScreen)
        {
            var fullRectGameScreen = new Rect(0, 0, gameScreen.Width, gameScreen.Height);
            using (var localGameScreen = new Mat(gameScreen, fullRectGameScreen))
            {
                var rectROI = new Rect(370, 370, 120, 20);

                var originalImgROI = new Mat(
                    localGameScreen,
                    rectROI);

                var editImgROI = GetNoiseFreeText(originalImgROI, 60);

                var tempPos = PatternSearch(editImgROI,
                        new Mat(@"ComputerVision\PatternsForCV\FriendlyGameStartStates\LoadingMatchSettings-Text.tif"));

                return (tempPos != Rect.Empty);
            }

        }

        private bool CheckFgssMatchSettings(Mat gameScreen)
        {
            var fullRectGameScreen = new Rect(0, 0, gameScreen.Width, gameScreen.Height);
            using (var localGameScreen = new Mat(gameScreen, fullRectGameScreen))
            {
                var tempPos = PatternSearchRoi(localGameScreen,
                        new Mat(@"ComputerVision\PatternsForCV\FriendlyGameStartStates\MatchSettings-HeaderText.png"),
                        new Rect(220, 10, 410, 80));

                return (tempPos != Rect.Empty);
            }
        }

        private bool CheckFgssWaitingReadinessOpponent(Mat gameScreen)
        {
            var fullRectGameScreen = new Rect(0, 0, gameScreen.Width, gameScreen.Height);
            using (var localGameScreen = new Mat(gameScreen, fullRectGameScreen))
            {
                var rectROI = new Rect(305, 370, 245, 20);

                var originalImgROI = new Mat(
                    localGameScreen,
                    rectROI);

                var editImgROI = GetNoiseFreeText(originalImgROI);

                var tempPos = PatternSearch(editImgROI,
                        new Mat(@"ComputerVision\PatternsForCV\FriendlyGameStartStates\WaitingReadinessOpponent-Text.tif"));

                return (tempPos != Rect.Empty);
            }
        }

        #endregion FriendlyGameStartStates Checks

        #region StartGameStates Checks

        private bool CheckSgsGameLoadingScreen(Mat gameScreen)
        {
            var fullRectGameScreen = new Rect(0, 0, gameScreen.Width, gameScreen.Height);
            using (var localGameScreen = new Mat(gameScreen, fullRectGameScreen))
            {
                var tempPos = PatternSearch(localGameScreen,
                        new Mat(@"ComputerVision\PatternsForCV\StartGameStates\GameLoadingScreen-GameNamePart.png"));

                return (tempPos != Rect.Empty);
            }
        }

        private bool CheckSgsWelcomeScreen(Mat gameScreen)
        {
            var fullRectGameScreen = new Rect(0, 0, gameScreen.Width, gameScreen.Height);
            using (var localGameScreen = new Mat(gameScreen, fullRectGameScreen))
            {
                var tempPos = PatternSearchRoi(localGameScreen,
                        new Mat(@"ComputerVision\PatternsForCV\StartGameStates\WelcomeScreen-HelloText.png"),
                        new Rect(330, 10, 200, 70));

                return (tempPos != Rect.Empty);
            }
        }

        #endregion StartGameStates Checks

        #region GlobalGameStates Checks

        private bool CheckGgsArenaModeTab(Mat gameScreen)
        {
            var fullRectGameScreen = new Rect(0, 0, gameScreen.Width, gameScreen.Height);
            using (var localGameScreen = new Mat(gameScreen, fullRectGameScreen))
            {
                var tempPos = PatternSearch(localGameScreen,
                        new Mat(@"ComputerVision\PatternsForCV\GlobalGameStates\ArenaModeTab-ContractText.png"));

                return (tempPos != Rect.Empty);
            }
        }

        private bool CheckGgsGameModesTab(Mat gameScreen)
        {
            var fullRectGameScreen = new Rect(0, 0, gameScreen.Width, gameScreen.Height);
            using (var localGameScreen = new Mat(gameScreen, fullRectGameScreen))
            {
                if (CheckFgssMatchSettings(gameScreen))
                    return false;

                var tempPos = PatternSearchRoi(localGameScreen,
                        new Mat(@"ComputerVision\PatternsForCV\GlobalGameStates\GameModesTab-DeckDropDownArrow.jpg"),
                        new Rect(493, 363, 46, 37));

                return (tempPos != Rect.Empty);
            }
        }

        private bool CheckGgsHeavyLoading(Mat gameScreen)
        {
            var fullRectGameScreen = new Rect(0, 0, gameScreen.Width, gameScreen.Height);
            using (var localGameScreen = new Mat(gameScreen, fullRectGameScreen))
            {
                var tempPos = PatternSearchRoi(localGameScreen,
                        new Mat(@"ComputerVision\PatternsForCV\GlobalGameStates\HeavyLoading-CardDescriptionAngle.png"),
                        new Rect(650, 25, 150, 125));

                return (tempPos != Rect.Empty);
            }
        }

        private bool CheckGgsMainMenu(Mat gameScreen)
        {
            var fullRectGameScreen = new Rect(0, 0, gameScreen.Width, gameScreen.Height);
            using (var localGameScreen = new Mat(gameScreen, fullRectGameScreen))
            {
                if (CheckGgsGameModesTab(localGameScreen))
                    return false;

                var tempPos = PatternSearchRoi(localGameScreen,
                    new Mat(@"ComputerVision\PatternsForCV\GlobalGameStates\MainMenu-OutButton.png"),
                    new Rect(758, 428, 90, 45));

                return (tempPos != Rect.Empty);
            }
        }

        #endregion GlobalGameStates Checks

        #region OpenCVGeneralmethods

        /// <summary>
        /// Ищет объекты в изображении по заданному шаблону.
        /// Если объект не найден возвращает Rect со всеми полями -1
        /// </summary>
        /// <param name="gameScreen">Изображение в котором нужно искать шаблон</param>
        /// <param name="temp">Шаблон</param>
        /// <param name="thresHold">Допустимая погрешность</param>
        /// <returns>Возвращает координаты найденного шаблона.
        ///  Если шаблон не найден то вернется Rect.Empty</returns>
        private Rect PatternSearch(Mat gameScreen, Mat temp, double thresHold = 0.95)
        {
            // Источник кода: https://github.com/shimat/opencvsharp/issues/182

            var fullRectGameScreen = new Rect(0, 0, gameScreen.Width, gameScreen.Height);
            using (var localGameScreen = new Mat(gameScreen, fullRectGameScreen))
            {
                Rect tempPos = Rect.Empty;

                using (Mat refMat = localGameScreen)
                using (Mat tplMat = temp)
                using (Mat res = new Mat(refMat.Rows - tplMat.Rows + 1,
                    refMat.Cols - tplMat.Cols + 1, MatType.CV_32FC1))
                {
                    //Convert input images to gray
                    Mat gref = refMat.CvtColor(ColorConversionCodes.BGR2GRAY);
                    Mat gtpl = tplMat.CvtColor(ColorConversionCodes.BGR2GRAY);

                    Cv2.MatchTemplate(gref, gtpl, res, TemplateMatchModes.CCoeffNormed);
                    Cv2.Threshold(res, res, 0.8, 1.0, ThresholdTypes.Tozero);

                    while (true)
                    {
                        Cv2.MinMaxLoc(res, out _, out double maxval, out _, out Point maxloc);

                        if (maxval >= thresHold)
                        {
                            tempPos = new Rect(new Point(maxloc.X, maxloc.Y),
                                new Size(tplMat.Width, tplMat.Height));

                            //Draw a rectangle of the matching area
                            Cv2.Rectangle(refMat, tempPos, Scalar.LimeGreen, 2);

                            //Fill in the res Mat so you don't find the same area again in the MinMaxLoc
                            Cv2.FloodFill(res, maxloc, new Scalar(0), out _,
                                new Scalar(0.1), new Scalar(1.0),
                                FloodFillFlags.FixedRange);
                        }
                        else
                            break;
                    }

                    return tempPos;
                } 
            }
        }

        /// <summary>
        /// Ищет объекты в определенной части озображения изображении по заданному шаблону.
        /// Если объект не найден возвращает Rect со всеми полями -1
        /// </summary>
        /// <param name="gameScreen">Изображение в котором нужно искать шаблон</param>
        /// <param name="temp">Шаблон</param>
        /// <param name="regionOfInterest">Область интереса в которой ищется шаблон</param>
        /// <param name="thresHold">Допустимая погрешность</param>
        /// <returns>Возвращает координаты найденного шаблона. Координаты приведены к координатам
        /// gameScreen. Если шаблон не найден то вернется Rect.Empty</returns>
        private Rect PatternSearchRoi(Mat gameScreen, Mat temp, Rect regionOfInterest, double thresHold = 0.95)
        {
            if (regionOfInterest != Rect.Empty)
                gameScreen = new Mat(gameScreen, regionOfInterest);

            Rect tempPos = PatternSearch(gameScreen, temp, thresHold);

            if (tempPos == Rect.Empty)
                return Rect.Empty;

            return new Rect(
                tempPos.X + regionOfInterest.X,
                tempPos.Y + regionOfInterest.Y,
                tempPos.Width,
                tempPos.Height);
        }

        /// <summary>
        /// Возвращает отделенный от фона белый текст.
        /// </summary>
        /// <param name="imgROI">Зона интереса на изображении</param>
        /// <param name="saturation">Насыщенность</param>
        /// <param name="value">Значение цвета</param>
        /// <returns>Белый текст на черном фоне</returns>
        private Mat GetNoiseFreeText(Mat imgROI, int saturation = 50, int value = 150)
        {
            var fullRectGameScreen = new Rect(0, 0, imgROI.Width, imgROI.Height);
            using (var localGameScreenROI = new Mat(imgROI, fullRectGameScreen))
            {
                var hsvChannels = localGameScreenROI.CvtColor(ColorConversionCodes.BGR2HSV)
                    .Split();

                Mat editImgROI = new Mat();
                localGameScreenROI.CopyTo(editImgROI);
                editImgROI.SetTo(Scalar.Black);

                for (int y = 0; y < localGameScreenROI.Cols; y++)
                {
                    for (int x = 0; x < localGameScreenROI.Rows; x++)
                    {
                        //var h = hsvChannels[0].At<char>(x, y);
                        var s = hsvChannels[1].At<char>(x, y);
                        var v = hsvChannels[2].At<char>(x, y);

                        if (s < saturation && v > value)
                        {
                            editImgROI.Set(x, y, new Vec3b(255, 255, 255));
                        }
                    }
                }

                return editImgROI;
            }
        }

        #endregion OpenCVGeneralmethods
    }
}