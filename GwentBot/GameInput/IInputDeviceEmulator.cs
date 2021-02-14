using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GwentBot.GameInput
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IInputDeviceEmulator
    {
        void MouseClick(int x, int y, bool moveWithoutDelayAfterClick = true, int numClicks = 1, string button = "left");

        void MouseMove(int x, int y);

        void Send(string sendText, int mode = 0);
    }
}