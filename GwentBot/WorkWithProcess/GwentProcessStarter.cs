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
using GwentBot.PageObjects.Abstract;

namespace GwentBot.WorkWithProcess
{
    internal static class GwentProcessStarter
    {
        /// <summary>
        /// 关闭游戏异常
        /// </summary>
        /// <returns></returns>
        internal static bool CloseCrashReport()
        {
            AutoItX.AutoItSetOption("WinTitleMatchMode", 3);

            bool result = false;
            var crashReport = "Crash Report";
            var fatalError = "Fatal error";

            // 游戏崩溃
            if (AutoItX.WinExists(crashReport) == 1)
            {
                //正常的关闭窗口（WinClose）  强行关闭窗口（WinKill）
                AutoItX.WinClose(crashReport);
                //等待窗口被关闭
                AutoItX.WinWaitClose(crashReport);
                result = true;
            }

            // 游戏致命错误
            if (AutoItX.WinExists(fatalError) == 1)
            {
                AutoItX.WinClose(fatalError);
                AutoItX.WinWaitClose(fatalError);
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 关闭游戏进程
        /// </summary>
        /// <returns></returns>

        internal static bool CloseProcess()
        {
            AutoItX.AutoItSetOption("WinTitleMatchMode", 3);
            bool result = AutoItX.WinClose("Gwent") == 1 ? true : false;
            if (result)
                AutoItX.WinWaitClose("Gwent");
            Thread.Sleep(10000);

            return result;
        }

        /// <summary>
        /// 启动游戏进程
        /// </summary>
        /// <returns></returns>
        internal static bool StartProcess()
        {
            AutoItX.AutoItSetOption("WinTitleMatchMode", 3);

            // 1971477531 国际服 1781625612 国服
            // 程序路径
            var programPath = Registry.GetValue(
                @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\GOG.com\Games\1971477531",
                "exe",
                null);
            // 工作目录
            var workingDir = Registry.GetValue(
                @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\GOG.com\Games\1971477531",
                "workingDir",
                null);

            if (programPath.ToString() == string.Empty)
                programPath = ConfigurationManager.AppSettings.Get("GamePath");

            do
            {
                CloseCrashReport();
                AutoItX.Run(programPath.ToString(), workingDir.ToString());
                // 等待窗口出现
                AutoItX.WinWait("Gwent");
                CloseCrashReport();
                // 等待窗口被激活
                AutoItX.WinActivate("Gwent");

            } while (!WindowExists());
            // 重置最新页面创建时间差（间隔）
            PageObject.ResetLastCreationTimePage();

            return WindowExists();
        }

        /// <summary>
        /// 窗口标题匹配是否存在
        /// </summary>
        /// <returns></returns>
        internal static bool WindowExists()
        {
            //更改窗口函数在执行搜索操作时的标题匹配模式.
            //1 = 只匹配标题的前面部分(默认)
            //2 = 标题的任意子串皆可匹配
            //3 = 完全匹配标题
            //4 = 高级模式,详情请查看 窗口标题与文本(高级)
            //-1 到 - 4 = 强制小写匹配.
            AutoItX.AutoItSetOption("WinTitleMatchMode", 3);
            return AutoItX.WinExists("Gwent") == 1 ? true : false;
        }
    }
}