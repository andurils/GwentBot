using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Mat panda = new Mat(@"Images/test01.png", ImreadModes.AnyColor);
            Rect roi = new Rect(710, 55, 155, 45);//首先要用个rect确定我们的兴趣区域在哪
            Mat ImageROI = new Mat(panda, roi);//新建一个mat，把roi内的图像加载到里面去。
            Cv2.ImShow("ROI 兴趣区域", ImageROI);
            //Cv2.ImShow("滚滚", panda);
            Cv2.WaitKey();
        }
    }
}
