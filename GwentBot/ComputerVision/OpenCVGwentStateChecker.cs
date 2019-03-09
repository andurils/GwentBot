using OpenCvSharp;
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

        public GameSessionStates GetCurrentGameSessionStates()
        {
            throw new NotImplementedException();
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
                        case GlobalGameStates.MainMenu:
                            if (CheckGgsMainMenu(gameScreen))
                                return item;
                            break;

                        case GlobalGameStates.GameModesTab:
                            if (CheckGgsGameModesTab(gameScreen))
                                return item;
                            break;

                        case GlobalGameStates.ArenaModeTab:
                            if (CheckGgsArenaModeTab(gameScreen))
                                return item;
                            break;

                        case GlobalGameStates.HeavyLoading:
                            if (CheckGgsHeavyLoading(gameScreen))
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

        #region FriendlyGameStartStates Checks

        private bool CheckFgssLoadingMatchSettings(Mat gameScreen)
        {
            return false;
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
            return false;
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

            Rect tempPos = Rect.Empty;

            using (Mat refMat = gameScreen)
            using (Mat tplMat = temp)
            using (Mat res = new Mat(refMat.Rows - tplMat.Rows + 1, refMat.Cols - tplMat.Cols + 1, MatType.CV_32FC1))
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
                        tempPos = new Rect(new Point(maxloc.X, maxloc.Y), new Size(tplMat.Width, tplMat.Height));

                        //Draw a rectangle of the matching area
                        Cv2.Rectangle(refMat, tempPos, Scalar.LimeGreen, 2);

                        //Fill in the res Mat so you don't find the same area again in the MinMaxLoc
                        Cv2.FloodFill(res, maxloc, new Scalar(0), out _, new Scalar(0.1), new Scalar(1.0), FloodFillFlags.FixedRange);
                    }
                    else
                        break;
                }

                return tempPos;
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

        #endregion OpenCVGeneralmethods
    }
}