// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using GwentBot.ComputerVision;
using GwentBot.PageObjects;
using GwentBot.PageObjects.SupportObjects;
using GwentBot.StateAbstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GwentBot
{
    public class Bot
    {
        public event Action<string> GameStatusChanged;

        private bool IsWork { get; set; }

        public async void StartWorkAsync()
        {
            IsWork = true;

            //await Task.Run(() =>
            //{
            //    var screenShotCreator = new GwentWindowScreenShotCreator();
            //    var cv = new OpenCvGwentStateChecker(screenShotCreator);
            //    while (IsWork)
            //    {
            //        if (screenShotCreator.IsGameWindowFullVisible())
            //        {
            //            var globalStat = cv.GetCurrentGlobalGameStates();
            //            var startGameStates = cv.GetCurrentStartGameStates();
            //            var friendlyGameStat = cv.GetCurrentFriendlyGameStartStates();
            //            var coinTossStat = cv.GetCurrentCoinTossStates();
            //            var gameSesStat = cv.GetCurrentGameSessionStates();
            //            var notif = cv.GetCurrentNotifications();

            //            if (globalStat != GlobalGameStates.Unknown)
            //                GameStatusChanged?.Invoke(Enum.GetName(globalStat.GetType(), globalStat));
            //            else if (startGameStates != StartGameStates.Unknown)
            //                GameStatusChanged?.Invoke(Enum.GetName(startGameStates.GetType(), startGameStates));
            //            else if (friendlyGameStat != FriendlyGameStartStates.Unknown)
            //                GameStatusChanged?.Invoke(Enum.GetName(friendlyGameStat.GetType(), friendlyGameStat));
            //            else if (coinTossStat != CoinTossStates.Unknown)
            //                GameStatusChanged?.Invoke(Enum.GetName(coinTossStat.GetType(), coinTossStat));
            //            else if (gameSesStat != GameSessionStates.Unknown)
            //                GameStatusChanged?.Invoke(Enum.GetName(gameSesStat.GetType(), gameSesStat));
            //            else
            //                GameStatusChanged?.Invoke("Unknown");

            //            if (notif != Notifications.NoNotifications)
            //                GameStatusChanged?.Invoke(Enum.GetName(notif.GetType(), notif));
            //        }

            //        Thread.Sleep(500);
            //    }
            //});

            var screenShotCreator = new GwentWindowScreenShotCreator();
            var cv = new OpenCvGwentStateChecker(screenShotCreator);

            await Task.Run(() =>
            {
                GameStatusChanged?.Invoke("Работаю");
                while (this.IsWork)
                {
                    if (screenShotCreator.IsGameWindowFullVisible())
                    {
                        //var mainPage = new MainMenuPage(cv, new DefaultWaitingService())
                        //.GotoGameModesPage()
                        //.GotoMainMenuPage()
                        //.GotoArenaModePage()
                        //.GotoMainMenuPage();

                        try
                        {
                            var gameSession = new GameSessionPage(
                                cv, new DefaultWaitingService(), new Model.Game(
                                    new Model.Deck(), new Model.User("Пользоватеь")));

                            GameStatusChanged?.Invoke("Объект создан");

                            var result = gameSession.GiveUp();

                            GameStatusChanged?.Invoke("Все");

                            IsWork = false;
                        }
                        catch (Exception e)
                        {
                            GameStatusChanged?.Invoke("Ошибка");
                        }

                        //IsWork = false;
                    }
                }
            });
        }

        public void StopWork()
        {
            IsWork = false;
        }
    }
}