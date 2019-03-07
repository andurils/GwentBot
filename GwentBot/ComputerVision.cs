using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GwentBot
{
    internal class ComputerVision
    {
        private readonly string gameWindowTatle = "Gwent";
        internal enum GlobalGameStates
        {
            Unknown,
            Main,
            GameModesTab,
            ArenaModeTab,
        }

        internal GlobalGameStates GetCurrentGlobalGameStatus()
        {
            Mat gameScreen = GetGameScreenshot().ToMat();


            foreach (int itemValue in Enum.GetValues(typeof(GlobalGameStates)))
            {
                var item = (GlobalGameStates)itemValue;

                switch (item)
                {
                    case GlobalGameStates.Main:
                        if (CheckMainGlobalGameStates(gameScreen))
                            return item;
                        break;
                    case GlobalGameStates.GameModesTab:
                        if (CheckGameModesTabGlobalGameStates(gameScreen))
                            return item;
                        break;
                    case GlobalGameStates.ArenaModeTab:
                        if (CheckArenaModeTabGlobalGameStates(gameScreen))
                            return item;
                        break;
                }
            }
            return GlobalGameStates.Unknown;
        }

        private bool CheckMainGlobalGameStates(Mat gameScreen)
        {
            if (CheckGameModesTabGlobalGameStates(gameScreen))
                return false;

            var tempPos = PatternSearchROI(gameScreen,
                new Mat(@"PatternsForCV\Main-OutButton.png"),
                new Rect(758, 428, 90, 45));

            if (tempPos == Rect.Empty)
                return false;
            else
                return true;


            return false;
        }

        private bool CheckGameModesTabGlobalGameStates(Mat gameScreen)
        {
            var tempPos = PatternSearchROI(gameScreen,
                new Mat(@"PatternsForCV\GameModesTab-DeckDropDownArrow.jpg"),
                new Rect(493, 363, 46, 37));

            if (tempPos == Rect.Empty)
                return false;
            else
                return true;
        }

        private bool CheckArenaModeTabGlobalGameStates(Mat gameScreen)
        {
            var tempPos = PatternSearch(gameScreen, 
                new Mat(@"PatternsForCV\ArenaModeTab-ContractText.png"));


            if (tempPos == Rect.Empty)
                return false;
            else
                return true;
        }

        #region OpenCVGeneralmethods

        /// <summary>
        /// Ищет объекты в определенной части озображения изображении по заданному шаблону.
        /// Если объект не найден возвращает Rect со всеми полями -1
        /// </summary>
        /// <param name="gameScreen">Изображение в котором нужно искать шаблон</param>
        /// <param name="temp">Шаблон</param>
        /// <param name="thresHold">Допустимая погрешность</param>
        /// <returns>Возвращает координаты найденного шаблона. Координаты приведены к координатам
        /// gameScreen. Если шаблон не найден то вернется Rect.Empty</returns>
        internal Rect PatternSearchROI(Mat gameScreen, Mat temp, Rect regionOfInterest, double thresHold = 0.95)
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
                tempPos.Height); ;
        }

        /// <summary>
        /// Ищет объекты в изображении по заданному шаблону.
        /// Если объект не найден возвращает Rect со всеми полями -1
        /// </summary>
        /// <param name="gameScreen">Изображение в котором нужно искать шаблон</param>
        /// <param name="temp">Шаблон</param>
        /// <param name="thresHold">Допустимая погрешность</param>
        /// <returns>Возвращает координаты найденного шаблона.
        ///  Если шаблон не найден то вернется Rect.Empty</returns>
        internal Rect PatternSearch(Mat gameScreen, Mat temp, double thresHold = 0.95)
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
                    double minval, maxval;
                    OpenCvSharp.Point minloc, maxloc;
                    Cv2.MinMaxLoc(res, out minval, out maxval, out minloc, out maxloc);

                    if (maxval >= thresHold)
                    {
                        tempPos = new Rect(new OpenCvSharp.Point(maxloc.X, maxloc.Y), new OpenCvSharp.Size(tplMat.Width, tplMat.Height));

                        //Draw a rectangle of the matching area
                        Cv2.Rectangle(refMat, tempPos, Scalar.LimeGreen, 2);

                        //Fill in the res Mat so you don't find the same area again in the MinMaxLoc
                        Rect outRect;
                        Cv2.FloodFill(res, maxloc, new Scalar(0), out outRect, new Scalar(0.1), new Scalar(1.0), FloodFillFlags.FixedRange);
                    }
                    else
                        break;

                }

                return tempPos;
            }
        }

        #endregion OpenCV Test

        #region CreatingGameWindowImage

        internal bool IsGameWindowFullVisible()
        {
            bool result = false;
            var gameWindowRect = GetGameWindowRectangle();
            if (gameWindowRect.X + gameWindowRect.Width < Screen.PrimaryScreen.Bounds.Width
                && gameWindowRect.Y + gameWindowRect.Height < Screen.PrimaryScreen.Bounds.Height)
                result = true;

            return result;
        }

        /// <summary>
        /// Возвращает скриншот рабочей зоны окна игры.
        /// Перед скриншотом нужно проверять видимость игрового окна с помощью: isGameWindowFullVisible()
        /// </summary>
        /// <returns></returns>
        private Bitmap GetGameScreenshot()
        {
            var gameWindowRect = GetGameWindowWorkZoneRectangle();
            Bitmap gameScreen = GetScreenshotScreenArea(gameWindowRect);

            return gameScreen;
        }

        private Rectangle GetGameWindowRectangle()
        {
            DwmGetWindowAttribute(GetGameProcess().MainWindowHandle,
                9,
                out var rect,
                Marshal.SizeOf(typeof(WinApiRect)));

            return Rectangle.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        internal Rectangle GetGameWindowWorkZoneRectangle()
        {
            var rect = GetGameWindowRectangle();

            var borderSize = SystemInformation.BorderSize;
            int titleHeight = SystemInformation.CaptionHeight
                + SystemInformation.FixedFrameBorderSize.Height * 2
                + SystemInformation.BorderSize.Height;

            var workAreaRect = new Rectangle(
                rect.X + borderSize.Width,
                rect.Y + titleHeight + borderSize.Height,
                rect.Width - borderSize.Width * 2,
                rect.Height - titleHeight - borderSize.Height * 2);

            return workAreaRect;
        }

        private Bitmap GetScreenshotScreenArea(Rectangle rect)
        {
            var bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            using (var graphics = Graphics.FromImage(bmp))
            {
                graphics.CopyFromScreen(rect.Left, rect.Top, 0, 0, rect.Size, CopyPixelOperation.SourceCopy);
            }

            return bmp;
        }

        #endregion CreatingGameWindowImage

        #region WorkWithTheGameProcess

        internal bool IsGameWindowActive()
        {
            var gameProcess = GetGameProcess();

            var fwHwnd = GetForegroundWindow();
            var pid = 0;
            GetWindowThreadProcessId(fwHwnd, ref pid);
            var foregroundWindow = Process.GetProcessById(pid);
            var isGameActive = gameProcess != null
                               && foregroundWindow.Id == gameProcess.Id;
            return isGameActive;
        }

        private Process GetGameProcess()
        {
            return Process.GetProcessesByName(gameWindowTatle).FirstOrDefault();
        }

        #endregion WorkWithTheGameProcess

        #region DebugLog

        private readonly bool testRun = true;
        private int logImgNum;

        private void LogImage(Bitmap bmp)
        {
            if (Directory.Exists("outTestImg") == false)
                Directory.CreateDirectory("outTestImg");
            bmp.ToMat().ImWrite("outTestImg/" + logImgNum + ".tif");
            logImgNum++;
        }

        #endregion DebugLog

        #region WinApiSupportMethods

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hwnd, ref int pid);

        [DllImport(@"dwmapi.dll")]
        private static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out WinApiRect pvAttribute,
int cbAttribute);

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        private struct WinApiRect
        {
            public readonly int Left;
            public readonly int Top;
            public readonly int Right;
            public readonly int Bottom;

            public Rectangle ToRectangle()
            {
                return Rectangle.FromLTRB(Left, Top, Right, Bottom);
            }
        }

        #endregion WinApiSupportMethods


    }
}
