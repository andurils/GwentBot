using GwentBot.PageObjects.Abstract;
using System;
using System.Threading;

namespace GwentBot.PageObjects.SupportObjects
{
    internal class DefaultWaitingService : IWaitingService
    {
        public void Wait(int seconds)
        {
            Thread.Sleep(new TimeSpan(0, 0, seconds));
        }
    }
}