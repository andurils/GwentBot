using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using AutoIt;

namespace GwentBot.WorkWithProcess
{
    internal static class GwentProcessStarter
    {
        internal static bool CloseCrashReport()
        {
            AutoItX.AutoItSetOption("WinTitleMatchMode", 3);
            return AutoItX.WinClose("Crash Report") == 1 ? true : false;
        }

        internal static bool CloseProcess()
        {
            AutoItX.AutoItSetOption("WinTitleMatchMode", 3);
            return AutoItX.WinClose("Gwent") == 1 ? true : false;
        }

        internal static bool StartProcess()
        {
            AutoItX.AutoItSetOption("WinTitleMatchMode", 3);
            CloseCrashReport();

            var programPath = Registry.GetValue(
                @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\GOG.com\Games\1971477531",
                "exe",
                null);
            var workingDir = Registry.GetValue(
                @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\GOG.com\Games\1971477531",
                "workingDir",
                null);

            AutoItX.Run(programPath.ToString(), workingDir.ToString());
            AutoItX.WinWait("Gwent");
            AutoItX.WinActivate("Gwent");

            return AutoItX.WinExists("Gwent") == 1 ? true : false;
        }

        internal static bool WindowExists()
        {
            AutoItX.AutoItSetOption("WinTitleMatchMode", 3);
            var dsf = AutoItX.WinExists("Gwent");
            return AutoItX.WinExists("Gwent") == 1 ? true : false;
        }
    }
}