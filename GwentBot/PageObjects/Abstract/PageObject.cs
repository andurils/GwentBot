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
        private static TimeSpan lastTimeCreationNewPage;

        /// <summary>
        /// Ожидает валидного состояния игры 30 секунд. Если состояние валидно создает новый объект.
        /// Если 30 секунд состояние игры не валидно выбрасывает исключение.
        /// 等待有效的游戏状态30秒钟。 如果游戏状态在30秒内未有效抛出异常。状态有效，则创建一个新对象。
        /// </summary>
        /// <param name="stateChecker"> IGwentStateChecker 实现类</param>
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
            lastTimeCreationNewPage = DateTime.Now.TimeOfDay;
        }

        public static bool IsPagesTooLongNotChanged()
        {
            if (lastTimeCreationNewPage == default(TimeSpan))
                return false;

            var result = false;
            var timeNow = DateTime.Now.TimeOfDay;
            var timeDifference = timeNow - lastTimeCreationNewPage;
            if (timeDifference > new TimeSpan(0, 5, 0))
                result = true;

            return result;
        }

        public static void ResetLastCreationTimePage()
        {
            lastTimeCreationNewPage = default(TimeSpan);
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