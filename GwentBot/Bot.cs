using GwentBot.ComputerVision;
using System;
using System.Threading;
using System.Threading.Tasks;
using GwentBot.WorkWithProcess;

namespace GwentBot
{
    public class Bot
    {
        public bool IsWork { get; private set; }
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

                        if(globalStat != GlobalGameStates.Unknown)
                            GameStatusChanged(Enum.GetName(globalStat.GetType(), globalStat));

                        if (startGameStates != StartGameStates.Unknown)
                            GameStatusChanged(Enum.GetName(startGameStates.GetType(), startGameStates));

                        if (friendlyGameStat != FriendlyGameStartStates.Unknown)
                            GameStatusChanged(Enum.GetName(friendlyGameStat.GetType(), friendlyGameStat));

                    }

                    Thread.Sleep(1000);
                }
            });
        }

        public void StopWork()
        {
            IsWork = false;
        }


    }
}