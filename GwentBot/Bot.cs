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
                        //var mainPage = new MainMenuPage(cv, new DefaultWaitingService())
                        //.GotoGameModesPage()
                        //.GotoMainMenuPage()
                        //.GotoArenaModePage()
                        //.GotoMainMenuPage();

                        try
                        {
                            var gameSession = new GameSessionPage(
                                cv, new DefaultWaitingService(), new Model.Game(
                                    new Model.Deck("Deck"),
                                    new Model.User("Пользоватеь")));

                            GameStatusChanged?.Invoke("Объект создан");

                            gameSession.GiveUp();

                            GameStatusChanged?.Invoke("Все");

                            IsWork = false;
                        }
                        catch
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