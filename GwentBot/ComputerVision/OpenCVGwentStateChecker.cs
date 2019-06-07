// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using GwentBot.StateAbstractions;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.IO;

namespace GwentBot.ComputerVision
{
    internal class OpenCvGwentStateChecker : IGwentStateChecker
    {
        public OpenCvGwentStateChecker(IWindowScreenShotCreator screenShotCreator)
        {
            ScreenShotCreator = screenShotCreator;
        }

        public IWindowScreenShotCreator ScreenShotCreator { get; }

        public CoinTossStates GetCurrentCoinTossStates()
        {
            using (Mat gameScreen = ScreenShotCreator.GetGameScreenshot().ToMat())
            {
                if (GenericCheck(
                    gameScreen,
                    @"ComputerVision\PatternsForCV\CoinTossStates\StartToss.png",
                    new Rect(380, 200, 90, 80)))
                    return CoinTossStates.StartToss;

                if (GenericCheck(
                    gameScreen,
                    @"ComputerVision\PatternsForCV\CoinTossStates\СoinWon.png",
                    new Rect(380, 200, 90, 80)))
                    return CoinTossStates.CoinWon;

                if (GenericCheck(
                    gameScreen,
                    @"ComputerVision\PatternsForCV\CoinTossStates\CoinLost.png",
                    new Rect(380, 200, 90, 80)))
                    return CoinTossStates.CoinLost;
            }
            return CoinTossStates.Unknown;
        }

        public FriendlyGameStartStates GetCurrentFriendlyGameStartStates()
        {
            using (Mat gameScreen = ScreenShotCreator.GetGameScreenshot().ToMat())
            {
                if (CheckFgssLoadingMatchSettings(gameScreen))
                    return FriendlyGameStartStates.LoadingMatchSettings;

                if (CheckFgssMatchSettings(gameScreen))
                    return FriendlyGameStartStates.MatchSettings;

                if (CheckFgssWaitingReadinessOpponent(gameScreen))
                    return FriendlyGameStartStates.WaitingReadinessOpponent;

                if (GenericCheck(
                    gameScreen,
                    @"ComputerVision\PatternsForCV\Notifications\CancelGameMessageBox.png",
                    new Rect(315, 180, 230, 120)))
                    return FriendlyGameStartStates.CancelGameMessageBox;
            }
            return FriendlyGameStartStates.Unknown;
        }

        public GameSessionStates GetCurrentGameSessionStates()
        {
            using (Mat gameScreen = ScreenShotCreator.GetGameScreenshot().ToMat())
            {
                if (GenericCheck(
                    gameScreen,
                    @"ComputerVision\PatternsForCV\GameSessionStates\Mulligan-Text.png",
                    new Rect(320, 440, 110, 25)))
                    return GameSessionStates.Mulligan;

                if (CheckGssOpponentChangesCards(gameScreen))
                    return GameSessionStates.OpponentChangesCards;

                if (GenericCheck(
                    gameScreen,
                    @"ComputerVision\PatternsForCV\GameSessionStates\MyTurnPlay-PassButton.png",
                    new Rect(800, 190, 47, 50)))
                    return GameSessionStates.MyTurnPlay;

                if (GenericCheck(
                    gameScreen,
                    @"ComputerVision\PatternsForCV\GameSessionStates\EnemyTurnPlaySrc-Button.png",
                    new Rect(800, 190, 47, 50)))
                    return GameSessionStates.EnemyTurnPlay;

                if (GenericCheck(
                    gameScreen,
                    @"ComputerVision\PatternsForCV\GameSessionStates\WinAlert.png",
                    new Rect(310, 200, 230, 100)))
                    return GameSessionStates.WinAlert;

                if (GenericCheck(
                    gameScreen,
                    @"ComputerVision\PatternsForCV\GameSessionStates\LosingAlert.png",
                    new Rect(310, 200, 230, 100)))
                    return GameSessionStates.LosingAlert;

                if (GenericCheck(
                    gameScreen,
                    @"ComputerVision\PatternsForCV\GameSessionStates\MatchResultsScreen-VsText.png",
                    new Rect(380, 80, 100, 70)))
                    return GameSessionStates.MatchResultsScreen;

                if (GenericCheck(
                    gameScreen,
                    @"ComputerVision\PatternsForCV\GameSessionStates\MatchRewardsScreen-FlagWithExperience.png",
                    new Rect(650, 60, 150, 150)))
                    return GameSessionStates.MatchRewardsScreen;
            }
            return GameSessionStates.Unknown;
        }

        public GlobalGameStates GetCurrentGlobalGameStates()
        {
            using (Mat gameScreen = ScreenShotCreator.GetGameScreenshot().ToMat())
            {
                if (GenericCheck(
                    gameScreen,
                    @"ComputerVision\PatternsForCV\GlobalGameStates\ArenaModeTab-ContractText.png",
                    Rect.Empty))
                    return GlobalGameStates.ArenaModeTab;

                if (GenericCheck(
                    gameScreen,
                    @"ComputerVision\PatternsForCV\GlobalGameStates\HeavyLoading-CardDescriptionAngle.png",
                    new Rect(650, 25, 150, 125)))
                    return GlobalGameStates.HeavyLoading;

                if (CheckGgsGameModesTab(gameScreen))
                    return GlobalGameStates.GameModesTab;

                if (CheckGgsMainMenu(gameScreen))
                    return GlobalGameStates.MainMenu;
            }
            return GlobalGameStates.Unknown;
        }

        public Notifications GetCurrentNotifications()
        {
            using (Mat gameScreen = ScreenShotCreator.GetGameScreenshot().ToMat())
            {
                if (GenericCheck(
                    gameScreen,
                    @"ComputerVision\PatternsForCV\Notifications\FriendlyDuel.png",
                    new Rect(780, 40, 68, 50)))
                    return Notifications.FriendlyDuel;

                if (GenericCheck(
                    gameScreen,
                    @"ComputerVision\PatternsForCV\Notifications\ReceivedReward.png",
                    new Rect(780, 40, 68, 50)))
                    return Notifications.ReceivedReward;

                if (GenericCheck(
                    gameScreen,
                    @"ComputerVision\PatternsForCV\Notifications\RewardsTab.png",
                    new Rect(390, 440, 68, 25)))
                    return Notifications.RewardsTab;
            }
            return Notifications.NoNotifications;
        }

        public StartGameStates GetCurrentStartGameStates()
        {
            using (Mat gameScreen = ScreenShotCreator.GetGameScreenshot().ToMat())
            {
                if (GenericCheck(
                    gameScreen,
                    @"ComputerVision\PatternsForCV\StartGameStates\GameLoadingScreen-GameNamePart.png",
                    Rect.Empty))
                    return StartGameStates.GameLoadingScreen;

                if (GenericCheck(
                    gameScreen,
                    @"ComputerVision\PatternsForCV\StartGameStates\WelcomeScreen-HelloText.png",
                    new Rect(330, 10, 200, 70)))
                    return StartGameStates.WelcomeScreen;
            }
            return StartGameStates.Unknown;
        }

        public byte[] GetGameScreenshotBitmap()
        {
            byte[] result;

            using (var ms = new MemoryStream())
            {
                ScreenShotCreator.GetGameScreenshot()
                    .Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                result = ms.ToArray();
            }

            return result;
        }

        #region GameSessionStates Checks

        private bool CheckGssOpponentChangesCards(Mat gameScreen)
        {
            var fullRectGameScreen = new Rect(0, 0, gameScreen.Width, gameScreen.Height);
            using (var localGameScreen = new Mat(gameScreen, fullRectGameScreen))
            {
                var rectRoi = new Rect(290, 5, 280, 45);
                var originalImgRoi = new Mat(localGameScreen, rectRoi);
                var gameScreenEditImgRoi = GetNoiseFreeText(originalImgRoi, 80);
                var patternMat = new Mat(@"ComputerVision\PatternsForCV\GameSessionStates\OpponentChangesCards-Text.png");

                var tempPos = PatternSearch(gameScreenEditImgRoi, patternMat);

                return (tempPos != Rect.Empty);
            }
        }

        #endregion GameSessionStates Checks

        #region FriendlyGameStartStates Checks

        private bool CheckFgssLoadingMatchSettings(Mat gameScreen)
        {
            var fullRectGameScreen = new Rect(0, 0, gameScreen.Width, gameScreen.Height);
            using (var localGameScreen = new Mat(gameScreen, fullRectGameScreen))
            {
                var rectRoi = new Rect(370, 370, 120, 20);
                var originalImgRoi = new Mat(localGameScreen, rectRoi);
                var editImgRoi = GetNoiseFreeText(originalImgRoi, 60);

                var tempPos = PatternSearch(
                    editImgRoi,
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
                var rectRoi = new Rect(305, 370, 245, 20);
                var originalImgRoi = new Mat(localGameScreen, rectRoi);
                var editImgRoi = GetNoiseFreeText(originalImgRoi);

                var tempPos = PatternSearch(
                    editImgRoi,
                    new Mat(@"ComputerVision\PatternsForCV\FriendlyGameStartStates\WaitingReadinessOpponent-Text.tif"));

                return (tempPos != Rect.Empty);
            }
        }

        #endregion FriendlyGameStartStates Checks

        #region GlobalGameStates Checks

        private bool CheckGgsGameModesTab(Mat gameScreen)
        {
            var fullRectGameScreen = new Rect(0, 0, gameScreen.Width, gameScreen.Height);
            using (var localGameScreen = new Mat(gameScreen, fullRectGameScreen))
            {
                if (CheckFgssMatchSettings(gameScreen))
                    return false;

                var tempPos = PatternSearchRoi(
                    localGameScreen,
                    new Mat(@"ComputerVision\PatternsForCV\GlobalGameStates\GameModesTab-DeckDropDownArrow.png"),
                    new Rect(500, 360, 50, 40));

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

                var tempPos = PatternSearchRoi(
                    localGameScreen,
                    new Mat(@"ComputerVision\PatternsForCV\GlobalGameStates\MainMenu-OutButton.png"),
                    new Rect(758, 428, 90, 45));

                return (tempPos != Rect.Empty);
            }
        }

        #endregion GlobalGameStates Checks

        #region OpenCVGeneralmethods

        /// <summary>
        /// Обобщенный метод для проверок состояний
        /// </summary>
        /// <param name="gameScreen">Изображение в котором нужно искать шаблон</param>
        /// <param name="patternPath">Шаблон</param>
        /// <param name="gameScreenRoi">Область интереса в которой ищется шаблон. Если искать нужно
        /// по всему изображение нужно передать Rect.Empty</param>
        /// <returns>Результат поиска патерна в изображении</returns>
        private bool GenericCheck(Mat gameScreen, string patternPath, Rect gameScreenRoi)
        {
            var fullRectGameScreen = new Rect(0, 0, gameScreen.Width, gameScreen.Height);
            using (var localGameScreen = new Mat(gameScreen, fullRectGameScreen))
            {
                if (gameScreenRoi != Rect.Empty)
                {
                    var tempPos = PatternSearchRoi(
                        localGameScreen,
                        new Mat(patternPath),
                        gameScreenRoi);

                    return (tempPos != Rect.Empty);
                }
                else
                {
                    var tempPos = PatternSearch(localGameScreen, new Mat(patternPath));

                    return (tempPos != Rect.Empty);
                }
            }
        }

        /// <summary>
        /// Возвращает отделенный от фона белый текст.
        /// </summary>
        /// <param name="imgRoi">Зона интереса на изображении</param>
        /// <param name="saturation">Насыщенность</param>
        /// <param name="value">Значение цвета</param>
        /// <returns>Белый текст на черном фоне</returns>
        private Mat GetNoiseFreeText(Mat imgRoi, int saturation = 50, int value = 150)
        {
            var fullRectGameScreen = new Rect(0, 0, imgRoi.Width, imgRoi.Height);
            using (var localGameScreenRoi = new Mat(imgRoi, fullRectGameScreen))
            {
                var hsvChannels = localGameScreenRoi.CvtColor(ColorConversionCodes.BGR2HSV)
                    .Split();

                Mat editImgRoi = new Mat();
                localGameScreenRoi.CopyTo(editImgRoi);
                editImgRoi.SetTo(Scalar.Black);

                for (int y = 0; y < localGameScreenRoi.Cols; y++)
                {
                    for (int x = 0; x < localGameScreenRoi.Rows; x++)
                    {
                        //var h = hsvChannels[0].At<char>(x, y);
                        var s = hsvChannels[1].At<char>(x, y);
                        var v = hsvChannels[2].At<char>(x, y);

                        if (s < saturation && v > value)
                        {
                            editImgRoi.Set(x, y, new Vec3b(255, 255, 255));
                        }
                    }
                }

                return editImgRoi;
            }
        }

        /// <summary>
        /// Ищет объекты в изображении по заданному шаблону.
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
        /// Ищет объекты в определенной части изображения по заданному шаблону.
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

        #endregion OpenCVGeneralmethods
    }
}