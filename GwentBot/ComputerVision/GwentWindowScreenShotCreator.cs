using GwentBot.WorkWithProcess;

namespace GwentBot.ComputerVision
{
    internal class GwentWindowScreenShotCreator : AnyWindowScreenShotCreator, IWindowScreenShotCreator
    {
        internal GwentWindowScreenShotCreator() : base(new GwentProcessInformation())
        {
        }
    }
}