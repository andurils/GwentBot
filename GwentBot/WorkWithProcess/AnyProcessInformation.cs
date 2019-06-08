using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace GwentBot.WorkWithProcess
{
    internal class AnyProcessInformation : IProcessInformation
    {
        public AnyProcessInformation(string mainWindowProcessTitle)
        {
            MainWindowProcessTitle = mainWindowProcessTitle;
        }

        public string MainWindowProcessTitle { get; private set; }

        public Process GetGameProcess()
        {
            return Process.GetProcessesByName(MainWindowProcessTitle).FirstOrDefault();
        }

        public bool IsGameRunning()
        {
            return (GetGameProcess() != null);
        }

        public bool IsGameWindowActive()
        {
            if (IsGameRunning() == false)
                return false;

            var gameProcess = GetGameProcess();

            var fwHwnd = GetForegroundWindow();
            var pid = 0;
            GetWindowThreadProcessId(fwHwnd, ref pid);
            var foregroundWindow = Process.GetProcessById(pid);
            var isGameActive = gameProcess != null
                               && foregroundWindow.Id == gameProcess.Id;
            return isGameActive;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hwnd, ref int pid);
    }
}