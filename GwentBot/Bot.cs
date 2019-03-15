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

                        if (globalStat != GlobalGameStates.Unknown)
                            GameStatusChanged(Enum.GetName(globalStat.GetType(), globalStat));
                        else if (startGameStates != StartGameStates.Unknown)
                            GameStatusChanged(Enum.GetName(startGameStates.GetType(), startGameStates));
                        else if (friendlyGameStat != FriendlyGameStartStates.Unknown)
                            GameStatusChanged(Enum.GetName(friendlyGameStat.GetType(), friendlyGameStat));
                        else if (coinTossStat != CoinTossStates.Unknown)
                            GameStatusChanged(Enum.GetName(coinTossStat.GetType(), coinTossStat));
                        else if (gameSesStat != GameSessionStates.Unknown)
                            GameStatusChanged(Enum.GetName(gameSesStat.GetType(), gameSesStat));
                        else
                            GameStatusChanged("Unknown");
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