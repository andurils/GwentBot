// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using AutoIt;
using GwentBot.StateAbstractions;
using System;
using System.Threading;

namespace GwentBot.PageObjects.Abstract
{
    internal abstract class PageObject
    {
        protected readonly IGwentStateChecker gwentStateChecker;
        protected readonly IWaitingService waitingService;

        /// <summary>
        /// Ожидает валидного состояния игры 30 секунд. Если состояние валидно создает новый объект.
        /// Если 30 секунд состояние игры не валидно выбрасывает исключение.
        /// </summary>
        /// <param name="gwentStateChecker">Любой класс реализующий IGwentStateChecker</param>
        public PageObject(IGwentStateChecker gwentStateChecker, IWaitingService waitingService)
        {
            this.gwentStateChecker = gwentStateChecker;
            this.waitingService = waitingService;

            int second = 30;
            for (; second != 0; second--)
            {
                if (VerifyingPage()) //-V3068
                    break;
                this.waitingService.Wait(1);
            }
            if (second == 0)
                throw new Exception($"Это не страница {this.GetType()}");

            AutoItX.AutoItSetOption("MouseCoordMode", 2);
            AutoItX.AutoItSetOption("PixelCoordMode", 2);
        }

        protected abstract bool VerifyingPage();
    }
}