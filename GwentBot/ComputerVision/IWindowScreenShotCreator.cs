using GwentBot.WorkWithProcess;
using System.Drawing;

namespace GwentBot.ComputerVision
{
    internal interface IWindowScreenShotCreator
    {
        IProcessInformation WorkingProcessInformation { get; }

        /// <summary>
        /// Возвращает скриншот рабочей зоны окна.
        /// Перед скриншотом нужно проверять видимость окна с помощью: IsGameWindowFullVisible()
        /// </summary>
        /// <returns></returns>
        Bitmap GetGameScreenshot();

        Rectangle GetGameWindowWorkZoneRectangle();

        bool IsGameWindowFullVisible();
    }
}