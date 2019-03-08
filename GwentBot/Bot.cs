using System;
using System.Threading;
using System.Threading.Tasks;

namespace GwentBot
{
    internal class Bot
    {
        public bool isWork { get; private set; }
        internal event Action<string> GameStatusChanged;

        public async void StartWorkAsync()
        {
            isWork = true;

            await Task.Run(() =>
            {
                var cv = new ComputerVision();
                while (isWork)
                {
                    if (cv.IsGameWindowActive())
                    {
                        //var globalStatus = cv.GetCurrentGlobalGameStatus();
                        var startGameStates = cv.GetCurrentStartGameStates();

                        GameStatusChanged(Enum.GetName(startGameStates.GetType(), startGameStates));
                    }

                    Thread.Sleep(1000);
                }
            });
        }

        public void StopWork()
        {
            isWork = false;
        }


    }
}