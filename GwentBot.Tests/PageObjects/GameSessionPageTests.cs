using GwentBot.Model;
using GwentBot.PageObjects;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using GwentBot.GameInput;

namespace GwentBot.Tests.PageObjects
{
    [TestClass]
    public class GameSessionPageTests
    {
        [TestMethod]
        public void VerifyingPageTest_GameSessionStates_CorrectNewObject()
        {
            // Arrage
            var gwentStateChecker = new Mock<IGwentStateChecker>();
            gwentStateChecker.Setup(o => o.GetCurrentGameSessionStates())
                .Returns(GameSessionStates.MyTurnPlay);

            var waitingService = new Mock<IWaitingService>();
            waitingService.Setup(o => o.Wait(It.IsAny<int>()));

            var inputEmulator = new Mock<IInputDeviceEmulator>();

            // Act
            var arenaModePage = new GameSessionPage(
                gwentStateChecker.Object,
                waitingService.Object,
                inputEmulator.Object,
                new Game(new Deck("sdf"), new User("sdf")));

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

            var inputEmulator = new Mock<IInputDeviceEmulator>();

            // Act
            new GameSessionPage(
                gwentStateChecker.Object,
                waitingService.Object,
                inputEmulator.Object,
                new Game(new Deck("sdf"), new User("sdf")));
            // Assert  - Expects exception
        }

        [TestMethod]
        public void VerifyingPageTest_OpponentSurrenderedMessageBox_CorrectNewObject()
        {
            // Arrage
            var gwentStateChecker = new Mock<IGwentStateChecker>();
            gwentStateChecker.Setup(o => o.GetCurrentGameSessionStates())
                .Returns(GameSessionStates.OpponentSurrenderedMessageBox);

            var waitingService = new Mock<IWaitingService>();
            waitingService.Setup(o => o.Wait(It.IsAny<int>()));

            var inputEmulator = new Mock<IInputDeviceEmulator>();

            // Act
            var arenaModePage = new GameSessionPage(
                gwentStateChecker.Object,
                waitingService.Object,
                inputEmulator.Object,
                new Game(new Deck("sdf"), new User("sdf")));

            // Assert
            Assert.IsNotNull(arenaModePage);
        }
    }
}