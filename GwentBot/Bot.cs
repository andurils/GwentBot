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
        public Bot()
        {
            AutoItX.AutoItSetOption("SendKeyDownDelay", 20);

            isWork = true;
        }

        private readonly bool testRun = true;
        private int logImgNum;

        private readonly string testImgPath = "testImg/testWind.tif";

        public void StopWokr()
        {
            isWork = false;
        }

        public bool isWork { get; private set; }

        public async void StartWorkAsync()
        {
            await Task.Run(() =>
            {
                while (isWork)
                {
                    if (IsGameWindowActive())
                        GetGameScreenshot();
                }
            });
        }











        private Bitmap GetGameScreenshot()
        {
            var gameProcWin = GetWindowRectangle();
            Bitmap gameScreen = null;
            var i = gameProcWin.Width + gameProcWin.Height;

            gameScreen = GetScreenshotWindow(gameProcWin);

            gameScreen = CropLetterbox(gameScreen);

            if (testRun)
                LogImage(gameScreen);

            return gameScreen;
        }


        private Rectangle GetWindowRectangle()
        {

            //var bitmap = new Mat(testImgPath).ToBitmap();

            //var units = GraphicsUnit.Point;
            //var bmpRectangleF = bitmap.GetBounds(ref units);
            //var bmpRectangle = Rectangle.Round(bmpRectangleF);

            //return bmpRectangle;


            var res = DwmGetWindowAttribute(GetGameProcess().MainWindowHandle,
                9,
                out var rect,
                Marshal.SizeOf(typeof(Rect)));

            return Rectangle.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        /// <summary>
        ///     Получить скриншот области экрана
        /// </summary>
        /// <param name="rect">Область скриншота</param>
        /// <returns></returns>
        private Bitmap GetScreenshotWindow(Rectangle rect)
        {
            var bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            using (var graphics = Graphics.FromImage(bmp))
            {
                graphics.CopyFromScreen(rect.Left, rect.Top, 0, 0, rect.Size, CopyPixelOperation.SourceCopy);
            }

            return bmp;
        }

        private Bitmap CropLetterbox(Bitmap bitmap)
        {
            LogImage(bitmap);

            var bounds = new Rectangle(0, 0, bitmap.Width, bitmap.Height);


            // Подрезка боковых рамок окна и заголовка окна
            var borderSize = SystemInformation.BorderSize;
            int titleHeight = (int)(SystemInformation.CaptionHeight * 1.5);

            var workAreaRec = new Rectangle(borderSize.Width, titleHeight + borderSize.Height,
                bitmap.Width - borderSize.Width * 2, bitmap.Height - titleHeight - borderSize.Height * 2);
            bitmap = bitmap.Clone(workAreaRec, bitmap.PixelFormat);

            //bounds = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            return bitmap;
        }

        private void LogImage(Bitmap bmp)
        {
            if (Directory.Exists("outTestImg") == false)
                Directory.CreateDirectory("outTestImg");
            bmp.ToMat().ImWrite("outTestImg/" + logImgNum + ".tif");
            logImgNum++;
        }

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
            return Process.GetProcessesByName("Gwent").FirstOrDefault();
        }

        #region WinApi

        [DllImport(@"dwmapi.dll")]
        private static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out Rect pvAttribute,
int cbAttribute);

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hwnd, ref int pid);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

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

        #endregion WInApi
    }
}
