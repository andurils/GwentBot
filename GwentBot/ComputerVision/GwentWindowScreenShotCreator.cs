using GwentBot.WorkWithProcess;

namespace GwentBot.ComputerVision
{
    internal class GwentWindowScreenShotCreator : AnyWindowScreenShotCreator
    {
        internal GwentWindowScreenShotCreator() : base(new GwentProcessInformation())
        {
        }
    }
}