// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
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