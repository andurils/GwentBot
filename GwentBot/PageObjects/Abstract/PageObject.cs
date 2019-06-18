using AutoIt;
using GwentBot.StateAbstractions;
using System;
using GwentBot.GameInput;

namespace GwentBot.PageObjects.Abstract
{
    internal abstract class PageObject
    {
        protected readonly IInputDeviceEmulator inputEmulator;
        protected readonly IGwentStateChecker stateChecker;
        protected readonly IWaitingService waitingService;

        /// <summary>
        /// Ожидает валидного состояния игры 30 секунд. Если состояние валидно создает новый объект.
        /// Если 30 секунд состояние игры не валидно выбрасывает исключение.
        /// </summary>
        /// <param name="stateChecker">Любой класс реализующий IGwentStateChecker</param>
        /// <param name="waitingService">Класс инкапсулирующий метод ожидания</param>
        /// <param name="inputEmul">Класс эмуляции устройств ввода</param>
        public PageObject(
            IGwentStateChecker stateChecker,
            IWaitingService waitingService,
            IInputDeviceEmulator inputEmulator)
        {
            this.stateChecker = stateChecker;
            this.waitingService = waitingService;
            this.inputEmulator = inputEmulator;

            WaitingGameReadiness();

            AutoItX.AutoItSetOption("MouseCoordMode", 2);
            AutoItX.AutoItSetOption("PixelCoordMode", 2);
        }

        protected abstract bool VerifyingPage();

        protected virtual void WaitingGameReadiness(int seconds = 30)
        {
            seconds = seconds < 0 ? 0 : seconds;

            for (; seconds != 0; seconds--)
            {
                if (VerifyingPage())
                    break;
                waitingService.Wait(1);
            }
            if (seconds == 0)
                throw new Exception($"Это не страница {GetType()}");
        }
    }
}