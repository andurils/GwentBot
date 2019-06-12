using GwentBot.Model;
using GwentBot.PageObjects;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace GwentBot.Tests.PageObjects
{
    [TestClass]
    public class MulliganPageTests
    {
        [TestMethod]
        public void VerifyingPageTest_MulliganPageAndCoinWon_CorrectNewObject()
        {
            // Arrage
            var gwentStateChecker = new Mock<IGwentStateChecker>();
            gwentStateChecker.Setup(o => o.GetCurrentCoinTossStates())
                .Returns(CoinTossStates.CoinWon);
            gwentStateChecker.Setup(o => o.GetCurrentGameSessionStates())
                .Returns(GameSessionStates.Mulligan);
            gwentStateChecker.Setup(o => o.GetCurrentGlobalMessageBoxes())
                .Returns(GlobalMessageBoxes.NoMessageBoxes);
            gwentStateChecker.SetupSequence(o => o.GetCurrentGlobalGameStates())
                .Returns(GlobalGameStates.HeavyLoading)
                .Returns(GlobalGameStates.Unknown);

            var waitingService = new Mock<IWaitingService>();
            waitingService.Setup(o => o.Wait(It.IsAny<int>()));
            // Act
            var result = new MulliganPage(
                gwentStateChecker.Object,
                waitingService.Object
                , new Game(new Deck("sdf"), new User("sdf")));
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(exceptionType: typeof(Exception))]
        public void VerifyingPageTest_Unknown_NotCorrectNewObject()
        {
            // Arrage
            var gwentStateChecker = new Mock<IGwentStateChecker>();
            gwentStateChecker.Setup(o => o.GetCurrentCoinTossStates())
                .Returns(CoinTossStates.CoinWon);
            gwentStateChecker.Setup(o => o.GetCurrentGameSessionStates())
                .Returns(GameSessionStates.Unknown);
            gwentStateChecker.Setup(o => o.GetCurrentGlobalMessageBoxes())
                .Returns(GlobalMessageBoxes.NoMessageBoxes);
            gwentStateChecker.SetupSequence(o => o.GetCurrentGlobalGameStates())
                .Returns(GlobalGameStates.HeavyLoading)
                .Returns(GlobalGameStates.Unknown);

            var waitingService = new Mock<IWaitingService>();
            waitingService.Setup(o => o.Wait(It.IsAny<int>()));
            // Act
            new MulliganPage(
                gwentStateChecker.Object,
                waitingService.Object
                , new Game(new Deck("sdf"), new User("sdf")));
            // Assert - Expects exception
        }

        [TestMethod]
        public void VerifyingPageTestAlternate_MulliganPageAndCoinLost_CorrectNewObject()
        {
            // Arrage
            var gwentStateChecker = new Mock<IGwentStateChecker>();
            gwentStateChecker.Setup(o => o.GetCurrentCoinTossStates())
                .Returns(CoinTossStates.CoinLost);
            gwentStateChecker.Setup(o => o.GetCurrentGameSessionStates())
                .Returns(GameSessionStates.Mulligan);
            gwentStateChecker.Setup(o => o.GetCurrentGlobalMessageBoxes())
                .Returns(GlobalMessageBoxes.NoMessageBoxes);
            gwentStateChecker.SetupSequence(o => o.GetCurrentGlobalGameStates())
                .Returns(GlobalGameStates.HeavyLoading)
                .Returns(GlobalGameStates.Unknown);

            var waitingService = new Mock<IWaitingService>();
            waitingService.Setup(o => o.Wait(It.IsAny<int>()));
            // Act
            var result = new MulliganPage(
                gwentStateChecker.Object,
                waitingService.Object
                , new Game(new Deck("sdf"), new User("sdf")));
            // Assert
            Assert.IsNotNull(result);
        }
    }
}