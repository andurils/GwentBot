using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using AutoIt;
using System.Configuration;
using System.Collections.Specialized;
using System.Threading;

namespace GwentBot.WorkWithProcess
{
    internal static class GwentProcessStarter
    {
        internal static bool CloseCrashReport()
        {
            AutoItX.AutoItSetOption("WinTitleMatchMode", 3);

            bool result = false;
            var crashReport = "Crash Report";
            var fatalError = "Fatal error";

            if (AutoItX.WinExists(crashReport) == 1)
            {
                AutoItX.WinClose(crashReport);
                AutoItX.WinWaitClose(crashReport);
                result = true;
            }

            if (AutoItX.WinExists(fatalError) == 1)
            {
                AutoItX.WinClose(fatalError);
                AutoItX.WinWaitClose(fatalError);
                result = true;
            }

            return result;
        }

        internal static bool CloseProcess()
        {
            AutoItX.AutoItSetOption("WinTitleMatchMode", 3);
            bool result = AutoItX.WinClose("Gwent") == 1 ? true : false;
            if (result)
                AutoItX.WinWaitClose("Gwent");
            Thread.Sleep(10000);

            return result;
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

            if (programPath == string.Empty)
                programPath = ConfigurationManager.AppSettings.Get("GamePath");

            AutoItX.Run(programPath.ToString(), workingDir.ToString());
            AutoItX.AutoItSetOption("WinTitleMatchMode", 3);
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