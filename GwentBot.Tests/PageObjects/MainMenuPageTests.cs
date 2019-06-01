using GwentBot.PageObjects;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GwentBot.Tests.PageObjects
{
    [TestClass]
    public class MainMenuPageTests
    {
        [TestMethod]
        public void CheckWorkNotification_FriendlyDuel_RightReceivedNotification()
        {
            //arrage
            var gwentStateChecker = new Mock<IGwentStateChecker>();
            gwentStateChecker.Setup(o => o.GetCurrentGlobalGameStates())
                .Returns(GlobalGameStates.MainMenu);
            gwentStateChecker.Setup(o => o.GetCurrentNotifications())
                .Returns(Notifications.FriendlyDuel);

            var waitingService = new Mock<IWaitingService>();
            waitingService.Setup(o => o.Wait(It.IsAny<int>()));

            var mainMenuPage = new MainMenuPage(
                gwentStateChecker.Object,
                waitingService.Object);
            //act
            Notifications result = mainMenuPage.Notifications
                .CheckReceivedNotifications();
            //assert
            Assert.AreEqual(Notifications.FriendlyDuel, result);
        }

        [TestMethod]
        public void VerifyingPageTest_GlobalGameStatesMainMenu_CorrectNewObject()
        {
            //arrage
            //act
            MainMenuPage mainMenuPage = GetNewMainMenuPage();
            //assert
            Assert.IsNotNull(mainMenuPage);
        }

        [TestMethod]
        public void VerifyingPageTest_GlobalGameStatesUnknown_NotCorrectNewObject()
        {
            try
            {
                //arrage
                var gwentStateChecker = new Mock<IGwentStateChecker>();
                gwentStateChecker.Setup(o => o.GetCurrentGlobalGameStates())
                    .Returns(GlobalGameStates.Unknown);

                var waitingService = new Mock<IWaitingService>();
                waitingService.Setup(o => o.Wait(It.IsAny<int>()));
                //act
                var mainMenuPage = new MainMenuPage(
                    gwentStateChecker.Object,
                    waitingService.Object);
                //assert
            }
            catch (System.Exception)
            {
                Assert.IsTrue(true);
            }
        }

        private MainMenuPage GetNewMainMenuPage()
        {
            var gwentStateChecker = new Mock<IGwentStateChecker>();
            gwentStateChecker.Setup(o => o.GetCurrentGlobalGameStates())
                .Returns(GlobalGameStates.MainMenu);

            var waitingService = new Mock<IWaitingService>();
            waitingService.Setup(o => o.Wait(It.IsAny<int>()));

            return new MainMenuPage(gwentStateChecker.Object, waitingService.Object);
        }
    }
}