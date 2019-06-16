using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutoIt;

namespace GwentBot.GameInput
{
    internal class AutoitInputDeviceEmulator : IInputDeviceEmulator
    {
        public void MouseClick(int x, int y, int numClicks = 1, string button = "left")
        {
            throw new NotImplementedException();
        }

        public void MouseMove(int x, int y)
        {
            var setupPoints = new List<Point>()
            {
                new Point(0, 0),
                new Point(600, 100),
                new Point(100, 600),
                new Point(600, 600)
            };

            var pointsList = GetCurve(setupPoints);
            foreach (var point in pointsList)
            {
                AutoItX.MouseMove(point.X, point.Y, 3);
            }
        }

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
    }
}