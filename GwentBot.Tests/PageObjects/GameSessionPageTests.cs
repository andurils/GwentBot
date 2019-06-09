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
    public class GameSessionPageTests
    {
        [TestMethod]
        public void VerifyingPageTest_GlobalGameStatesArenaModeTab_CorrectNewObject()
        {
            // Arrage
            var gwentStateChecker = new Mock<IGwentStateChecker>();
            gwentStateChecker.Setup(o => o.GetCurrentGameSessionStates())
                .Returns(GameSessionStates.MyTurnPlay);

            var waitingService = new Mock<IWaitingService>();
            waitingService.Setup(o => o.Wait(It.IsAny<int>()));

            // Act
            var arenaModePage = new GameSessionPage(
                gwentStateChecker.Object,
                waitingService.Object
                , new Game(new Deck("sdf"), new User("sdf")));

            // Assert
            Assert.IsNotNull(arenaModePage);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void VerifyingPageTest_GlobalGameStatesUnknown_NotCorrectNewObject()
        {
            // Arrage
            var gwentStateChecker = new Mock<IGwentStateChecker>();
            gwentStateChecker.Setup(o => o.GetCurrentGameSessionStates())
                .Returns(GameSessionStates.Unknown);

            var waitingService = new Mock<IWaitingService>();
            waitingService.Setup(o => o.Wait(It.IsAny<int>()));

            // Act
            new GameSessionPage(
                gwentStateChecker.Object,
                waitingService.Object
                , new Game(new Deck("sdf"), new User("sdf")));
            // Assert  - Expects exception
        }
    }
}