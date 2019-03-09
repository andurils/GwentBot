using GwentBot.ComputerVision;
using GwentBot.WorkWithProcess;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GwentBot
{
    public class Bot
    {
        public bool isWork { get; private set; }
        public event Action<string> GameStatusChanged;

        public async void StartWorkAsync()
        {
            isWork = true;

            await Task.Run((Action)(() =>
            {
                var screenShotCreator = new GwentWindowScreenShotCreator();
                var cv = new OpenCVGwentStateChecker(screenShotCreator);
                while (isWork)
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
            }));
        }

        public void StopWork()
        {
            isWork = false;
        }


    }
}