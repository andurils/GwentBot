using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoIt;

namespace GwentBot.GameInput
{
    internal class AutoitInputDeviceEmulator : IInputDeviceEmulator
    {
        public AutoitInputDeviceEmulator()
        {
            AutoItX.AutoItSetOption("MouseCoordMode", 2);
            AutoItX.AutoItSetOption("PixelCoordMode", 2);
        }

        public void MouseClick(int x, int y, int numClicks = 1, string button = "left")
        {
            var randDelley = new Random();
            int randSpeed = new Random().Next(8, 12);

            MouseMove(x, y);
            Thread.Sleep(randDelley.Next(100, 250));
            AutoItX.MouseClick(button, x, y, numClicks, randSpeed);
            Thread.Sleep(randDelley.Next(300, 600));

            int randX = new Random().Next(327, 527);
            int randY = new Random().Next(500, 530);
            MouseMove(randX, randY);
        }

        public void MouseMove(int x, int y)
        {
            AutoItX.MouseMove(x, y);
        }

        public void Send(string sendText, int mode)
        {
            AutoItX.Send(sendText, mode);
        }

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
            float step = 0.05f;

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
        }

        #endregion Bezier curve method
    }
}