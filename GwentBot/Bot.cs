using GwentBot.ComputerVision;
using System;
using System.Threading;
using System.Threading.Tasks;

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
                        var globalStatus = cv.GetCurrentGlobalGameStates();
                        //var startGameStates = cv.GetCurrentStartGameStates();
                        //var frendlyGameStates = cv.GetCurrentFriendlyGameStartStates();

                        GameStatusChanged(Enum.GetName(globalStatus.GetType(), globalStatus));
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