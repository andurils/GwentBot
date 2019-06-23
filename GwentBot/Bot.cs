using GwentBot.ComputerVision;
using GwentBot.PageObjects;
using GwentBot.PageObjects.SupportObjects;
using System;
using System.Threading;
using System.Threading.Tasks;
using GwentBot.GameInput;
using GwentBot.Model;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;
using GwentBot.WorkWithProcess;
using NLog;

namespace GwentBot
{
    public class Bot
    {
        private Logger logger;

        public Bot()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        public event Action<string> GameStatusChanged;

        private bool IsWork { get; set; }

        public async void StartWorkAsync()
        {
            IsWork = true;

            var screenShotCreator = new GwentWindowScreenShotCreator();
            var cv = new OpenCvGwentStateChecker(screenShotCreator);
            var inputEmulator = new AutoitInputDeviceEmulator();

            var pageFactory = new PageObjectFactory();

            await Task.Run(() =>
            {
                GameStatusChanged?.Invoke("Работаю");
                while (IsWork)
                {
                    try
                    {
                        if (screenShotCreator.IsGameWindowFullVisible())
                        {
                            if (PageObject.IsPagesTooLongNotChanged())
                            {
                                if (GwentProcessStarter.CloseProcess())
                                    GwentProcessStarter.StartProcess();
                            }

                            if (cv.GetCurrentGlobalGameStates() !=
                                GlobalGameStates.GameModesTab)
                            {
                                pageFactory.CheckAndClearGlobalMessageBoxes();

                                pageFactory
                                    .CheckAndClearOpponentSurrenderedMessageBox();

                                pageFactory
                                    .CheckAndClearGameSessionExceptionMessageBoxes();

                                var gameSess = cv.GetCurrentGameSessionStates();
                                GameStatusChanged?.Invoke(gameSess.ToString());

                                if (gameSess != GameSessionStates.Unknown)
                                {
                                    if (gameSess == GameSessionStates.MatchResultsScreen ||
                                       gameSess == GameSessionStates.MatchRewardsScreen)
                                        new MatchResultsRewardsScreenPage(cv, new DefaultWaitingService(), inputEmulator,
                                                new Game(new Deck(""), new User("")))
                                            .ClosePageStatistics();
                                    else
                                        new GameSessionPage(cv, new DefaultWaitingService(), inputEmulator,
                                            new Game(new Deck(""), new User("")))
                                        .GiveUp()
                                        .ClosePageStatistics();
                                }
                                pageFactory.StartGame()?.GotoGameModesPage();
                            }

                            var gameModes = new GameModesPage(cv, new DefaultWaitingService(), inputEmulator)
                                .GotoSeasonalGameMode()
                                .EndMulligan()
                                .GiveUp()
                                .ClosePageStatistics();

                        }
                        else
                        {
                            GwentProcessStarter.CloseCrashReport();
                            if (GwentProcessStarter.WindowExists() == false)
                                GwentProcessStarter.StartProcess();
                        }
                    }
                    catch (Exception e)
                    {
                        GameStatusChanged?.Invoke(e.Message);
                        if (!e.Message.Contains("Это не страница"))
                            logger.Error(e.Message + e.StackTrace);
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