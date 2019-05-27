﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using GwentBot.ComputerVision;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GwentBot
{
    public class Bot
    {
        private bool IsWork { get; set; }
        public event Action<string> GameStatusChanged;

        public async void StartWorkAsync()
        {
            IsWork = true;

            await Task.Run(() =>
            {
                var screenShotCreator = new GwentWindowScreenShotCreator();
                var cv = new OpenCvGwentStateChecker(screenShotCreator);
                while (IsWork)
                {
                    if (screenShotCreator.IsGameWindowFullVisible())
                    {
                        var globalStat = cv.GetCurrentGlobalGameStates();
                        var startGameStates = cv.GetCurrentStartGameStates();
                        var friendlyGameStat = cv.GetCurrentFriendlyGameStartStates();
                        var coinTossStat = cv.GetCurrentCoinTossStates();
                        var gameSesStat = cv.GetCurrentGameSessionStates();
                        var notif = cv.GetCurrentNotifications();

                        if (globalStat != GlobalGameStates.Unknown)
                            GameStatusChanged?.Invoke(Enum.GetName(globalStat.GetType(), globalStat));
                        else if (startGameStates != StartGameStates.Unknown)
                            GameStatusChanged?.Invoke(Enum.GetName(startGameStates.GetType(), startGameStates));
                        else if (friendlyGameStat != FriendlyGameStartStates.Unknown)
                            GameStatusChanged?.Invoke(Enum.GetName(friendlyGameStat.GetType(), friendlyGameStat));
                        else if (coinTossStat != CoinTossStates.Unknown)
                            GameStatusChanged?.Invoke(Enum.GetName(coinTossStat.GetType(), coinTossStat));
                        else if (gameSesStat != GameSessionStates.Unknown)
                            GameStatusChanged?.Invoke(Enum.GetName(gameSesStat.GetType(), gameSesStat));
                        else
                            GameStatusChanged?.Invoke("Unknown");

                        if (notif != Notifications.NoNotifications)
                            GameStatusChanged?.Invoke(Enum.GetName(notif.GetType(), notif));
                    }

                    Thread.Sleep(500);
                }
            });
        }

        public void StopWork()
        {
            IsWork = false;
        }


    }
}