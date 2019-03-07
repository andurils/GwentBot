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
        public bool isWork { get; private set; }

        public async void StartWorkAsync()
        {
            isWork = true;

            await Task.Run(() =>
            {
                while (isWork)
                {


                }
            });
        }

        public void StopWork()
        {
            isWork = false;
        }


    }
}