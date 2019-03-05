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

        public Bot()
        {
            isWork = true;
        }

        public bool isWork { get; private set; }

        public async void StartWorkAsync()
        {
            await Task.Run(() =>
            {
                while (isWork)
                {
                    if (IsGameWindowActive())
                    {
                        var gameScreen = GetGameScreenshot();

                        if (testRun)
                            LogImage(gameScreen);

                        isWork = false;
                    }
                }
            });
        }

        public void StopWork()
        {
            isWork = false;
        }

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
                Marshal.SizeOf(typeof(Rect)));

            return Rectangle.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        private Rectangle GetGameWindowWorkZoneRectangle()
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
        private static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out Rect pvAttribute,
int cbAttribute);

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
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