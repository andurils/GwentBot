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
    /// <summary>
    /// AutoIt  
    /// 脚本程序 用来在Windows GUI（用户界面）中进行自动操作。通过它可以组合使用模拟键击、鼠标移动和窗口/控件操作等来实现自动化任务
    /// 其他 http://blog.sina.com.cn/s/articlelist_1070064257_1_1.html
    /// AutoItSetOption说明 http://blog.sina.com.cn/s/blog_3fc7e2810100lfv3.html
    /// </summary>
    internal class AutoitInputDeviceEmulator : IInputDeviceEmulator
    {
        public AutoitInputDeviceEmulator()
        {
            //调整Autoit各种函数/参数的运作方式.

            //设置用于鼠标函数的坐标参照,可以是绝对位置也可以是相对当前激活窗口的坐标位置.
            //0 = 相对激活窗口的坐标
            //1 = 屏幕的绝对位置(默认)
            //2 = 相对激活窗口客户区的坐标
            AutoItX.AutoItSetOption("MouseCoordMode", 2);

            //设置用于象素函数的坐标参照,可以是绝对位置也可以是相对当前激活窗口的坐标位置.
            //0 = 相对激活窗口的坐标
            //1 = 屏幕的绝对位置(默认)
            //2 = 相对激活窗口客户区的坐标
            AutoItX.AutoItSetOption("PixelCoordMode", 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="moveWithoutDelayAfterClick"></param>
        /// <param name="numClicks"></param>
        /// <param name="button"></param>
        public void MouseClick(
            int x, int y, bool moveWithoutDelayAfterClick = true, int numClicks = 1, string button = "left")
        {
            var randDelley = new Random();
            int randSpeed = new Random().Next(8, 12);

            MouseMove(x, y);
            Thread.Sleep(randDelley.Next(100, 250));
            AutoItX.MouseClick(button, x, y, numClicks, randSpeed);
            Thread.Sleep(randDelley.Next(300, 600));

            if (moveWithoutDelayAfterClick)
            {
                int randX = new Random().Next(327, 527);
                int randY = new Random().Next(500, 530);
                MouseMove(randX, randY);
            }
            else
            {
                Task.Run(() =>
                {
                    Thread.Sleep(3000);
                    int randX = new Random().Next(327, 527);
                    int randY = new Random().Next(500, 530);
                    MouseMove(randX, randY);
                });
            }
        }

        public void MouseMove(int x, int y)
        {
            AutoItX.MouseMove(x, y);
        }

        public void Send(string sendText, int mode)
        {
            AutoItX.Send(sendText, mode);
        }

        #region Bezier curve method  贝塞尔曲线

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