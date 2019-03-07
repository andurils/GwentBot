using System.Threading.Tasks;

namespace GwentBot
{
    internal class Bot
    {
        public bool isWork { get; private set; }

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
                        if (cv.GetCurrentGlobalGameStatus() == ComputerVision.GlobalGameStates.ArenaModeTab)
                        {
                            AutoIt.AutoItX.MouseMove(100, 100);
                            isWork = false;
                        }

                    }


                }
            });
        }

        public void StopWork()
        {
            isWork = false;
        }


    }
}