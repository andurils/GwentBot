using GwentBot.ComputerVision;
using GwentBot.PageObjects;
using GwentBot.PageObjects.SupportObjects;
using System;
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

            var screenShotCreator = new GwentWindowScreenShotCreator();
            var cv = new OpenCvGwentStateChecker(screenShotCreator);

            await Task.Run(() =>
            {
                GameStatusChanged?.Invoke("Работаю");
                while (IsWork)
                {
                    if (screenShotCreator.IsGameWindowFullVisible())
                    {
                        try
                        {
                            var gameModes = new GameModesPage(cv, new DefaultWaitingService())
                        .GotoClassicGameMode()
                        .EndMulligan()
                        .GiveUp()
                        .ClosePageStatistics();
                        }
                        catch (Exception e)
                        {
                            GameStatusChanged?.Invoke(e.Message);
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