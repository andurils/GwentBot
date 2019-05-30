// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using GwentBot.WorkWithProcess;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GwentBot.ComputerVision
{
    internal class AnyWindowScreenShotCreator : IWindowScreenShotCreator
    {
        public AnyWindowScreenShotCreator(IProcessInformation processInformation)
        {
            WorkingProcessInformation = processInformation;
        }

        public IProcessInformation WorkingProcessInformation { get; private set; }

        public Bitmap GetGameScreenshot()
        {
            var gameWindowRect = GetGameWindowWorkZoneRectangle();
            Bitmap gameScreen = GetScreenshotScreenArea(gameWindowRect);

            return gameScreen;
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

        public bool IsGameWindowFullVisible()
        {
            if (false == WorkingProcessInformation.IsGameWindowActive())
                return false;

            bool result = false;
            var gameWindowRect = GetGameWindowRectangle();
            if (gameWindowRect.X + gameWindowRect.Width < Screen.PrimaryScreen.Bounds.Width &&
                gameWindowRect.Y + gameWindowRect.Height < Screen.PrimaryScreen.Bounds.Height)
                result = true;

            return result;
        }

        [DllImport(@"dwmapi.dll")]
        private static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute,
            out WinApiRect pvAttribute,
            int cbAttribute);

        private Rectangle GetGameWindowRectangle()
        {
            DwmGetWindowAttribute(WorkingProcessInformation.GetGameProcess().MainWindowHandle,
                9,
                out var rect,
                Marshal.SizeOf(typeof(WinApiRect)));

            return Rectangle.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom);
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
    }
}