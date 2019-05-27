// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
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