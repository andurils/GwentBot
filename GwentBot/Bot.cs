using AutoIt;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GwentBot
{
    internal class Bot
    {
        private readonly string gameWindowTatle = "Gwent";

        public bool isWork { get; private set; }

        public async void StartWorkAsync()
        {
            isWork = true;

            await Task.Run(() =>
            {
                while (isWork)
                {
                    if (IsGameWindowActive())
                    {
                        var gameScreen = GetGameScreenshot();
                        var gameScreenRect = GetGameWindowWorkZoneRectangle();

                        var cordBattleInviteBtn = RunTemplateMatch(
                            gameScreen,
                            new Bitmap("testImg/InvitingToFight-temp.jpg"));

                        if (cordBattleInviteBtn.X != -1 && cordBattleInviteBtn.Y != -1)
                        {
                            AutoItX.MouseClick(
                                "LEFT",
                                (gameScreenRect.X + cordBattleInviteBtn.X) + cordBattleInviteBtn.Width / 2,
                                (gameScreenRect.Y + cordBattleInviteBtn.Y) + cordBattleInviteBtn.Width / 2);

                            for (int i = 0; i < 20; i++)
                            {
                                gameScreen = GetGameScreenshot();
                                gameScreenRect = GetGameWindowWorkZoneRectangle();

                                var startGameBtn = RunTemplateMatch(
                                    gameScreen,
                                    new Bitmap("testImg/Start_a_friendly_match-temp.jpg"),
                                    0.8);

                                if (startGameBtn.X != -1 && startGameBtn.Y != -1)
                                {
                                    AutoItX.MouseClick(
                                        "LEFT",
                                        (gameScreenRect.X + startGameBtn.X) + startGameBtn.Width / 2,
                                        (gameScreenRect.Y + startGameBtn.Y) + startGameBtn.Width / 2);
                                }

                                    AutoItX.Sleep(1000);
                            }
                        }
                    }
                    AutoItX.Sleep(3000);
                }
            });
        }

        public void StopWork()
        {
            isWork = false;
        }

        #region OpenCV Test

        public void TestOpenCV()
        {
            var dsf = RunTemplateMatch(new Bitmap("testImg/Invitingtofight-src.jpg"),
                new Bitmap("testImg/Invitingtofight-temp.jpg"));
        }

        /// <summary>
        /// Ищет объекты в изображении по заданному шаблону. Возвращает 
        /// Если объект не найден возвращает Rect со всеми полями -1
        /// </summary>
        /// <param name="gameScreen"></param>
        /// <param name="temp"></param>
        /// <returns></returns>
        public Rect RunTemplateMatch(Bitmap gameScreen, Bitmap temp, double thresHold = 0.95)
        {
            // Источник кода: https://github.com/shimat/opencvsharp/issues/182

            Rect tempPos = new Rect(-1, -1, -1, -1);

            using (Mat refMat = gameScreen.ToMat())
            using (Mat tplMat = temp.ToMat())
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

        public bool IsGameWindowFullVisible()
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

        public Rectangle GetGameWindowWorkZoneRectangle()
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