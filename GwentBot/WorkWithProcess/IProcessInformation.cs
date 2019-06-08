using System.Diagnostics;

namespace GwentBot.WorkWithProcess
{
    internal interface IProcessInformation
    {
        string MainWindowProcessTitle { get; }

        Process GetGameProcess();

        bool IsGameRunning();

        bool IsGameWindowActive();
    }
}