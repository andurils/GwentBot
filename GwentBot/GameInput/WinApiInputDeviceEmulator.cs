using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutoIt;

namespace GwentBot.GameInput
{
    internal class WinApiInputDeviceEmulator : IInputDeviceEmulator
    {
        public void MouseClick(int x, int y, bool moveAfterClick = true, int numClicks = 1, string button = "left")
        {
            throw new NotImplementedException();
        }

        public void MouseMove(int x, int y)
        {
            var tempPos = AutoItX.MouseGetPos();
            var currMousePos = new Point(tempPos.X, tempPos.Y);
            var endMousePos = new Point(x, y);

            var rand = new Random();

            var posList = new List<Point>()
            {
                currMousePos,
                new Point(rand.Next(1, 800), rand.Next(1, 800)),
                new Point(rand.Next(1, 800), rand.Next(1, 800)),
                endMousePos
            };

            var result = GetCurve(posList);

            Point priviosPoint = currMousePos;
            foreach (var point in result)
            {
                if (priviosPoint == null)
                {
                    tempPos = AutoItX.MouseGetPos();
                    priviosPoint = new Point(tempPos.X, tempPos.Y);
                }

                var diffPoint = priviosPoint.GetDifference(point);
                mouse_event(MouseEventFlags.MOVE, diffPoint.X, diffPoint.Y, 0, 0);
                priviosPoint = point;
                AutoItX.Sleep(10);
            }
        }

        public void Send(string sendText, int mode = 0)
        {
            throw new NotImplementedException();
        }

        #region Private Method

        [Flags]
        public enum MouseEventFlags : uint
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }

        [DllImport("user32.dll")]
        private static extern void mouse_event(MouseEventFlags dwFlags, int dx, int dy, uint dwData,
            int dwExtraInfo);

        #endregion Private Method

        #region Bezier curve method

        private int Factorial(int n)
        {
            int res = 1;
            for (int i = 1; i <= n; i++)
                res *= i;
            return res;
        }

        private List<Point> GetCurve(List<Point> pointsArray)
        {
            float step = 0.01f;

            List<Point> result = new List<Point>();
            for (float t = 0; t < 1; t += step)
            {
                float ytmp = 0;
                float xtmp = 0;
                for (int i = 0; i < pointsArray.Count; i++)
                {
                    float b = Polinom(i, pointsArray.Count - 1, t);
                    xtmp += pointsArray[i].X * b;
                    ytmp += pointsArray[i].Y * b;
                }
                result.Add(new Point(xtmp, ytmp));
            }

            return result;
        }

        private float Polinom(int i, int n, float t)
        {
            return (Factorial(n) / (Factorial(i) * Factorial(n - i))) *
                   (float)Math.Pow(t, i) * (float)Math.Pow(1 - t, n - i);
        }

        private class Point
        {
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public Point(float x, float y) :
                this((int)Math.Round(x), (int)Math.Round(y))
            {
            }

            public int X { get; }
            public int Y { get; }

            public Point GetDifference(Point otherPoint)
            {
                return new Point(otherPoint.X - X, otherPoint.Y - Y);
            }
        }

        #endregion Bezier curve method
    }
}