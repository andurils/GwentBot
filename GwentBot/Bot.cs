using GwentBot.ComputerVision;
using GwentBot.PageObjects;
using GwentBot.PageObjects.SupportObjects;
using System;
using System.Threading.Tasks;
using GwentBot.StateAbstractions;

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

            var pageFactory = new PageObjectFactory();

            await Task.Run(() =>
            {
                GameStatusChanged?.Invoke("Работаю");
                while (IsWork)
                {
                    if (screenShotCreator.IsGameWindowFullVisible())
                    {
                        try
                        {
                            if (cv.GetCurrentGlobalGameStates() !=
                                GlobalGameStates.GameModesTab)
                            {
                                pageFactory.CheckAndClearGlobalMessageBoxes();

                                pageFactory
                                    .CheckAndClearOpponentSurrenderedMessageBox();
                            }

                            var gameModes = new GameModesPage(cv, new DefaultWaitingService())
                                .GotoSeasonalGameMode()
                                .EndMulligan()
                                .GiveUp()
                                .ClosePageStatistics();
                        }
                        catch (Exception e)
                        {
                            GameStatusChanged?.Invoke(e.Message + e.StackTrace);
                        }

                        //IsWork = false;
                    }
                }
                GameStatusChanged?.Invoke("Не работаю");
            });
        }

        public void StopWork()
        {
            IsWork = false;
        }
    }
}