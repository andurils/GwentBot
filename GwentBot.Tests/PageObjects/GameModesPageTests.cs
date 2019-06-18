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
    public class GameModesPageTests
    {
        [TestMethod]
        public void VerifyingPageTest_GlobalGameStatesUnknown_CorrectNewObject()
        {
            // Arrage
            var gwentStateChecker = new Mock<IGwentStateChecker>();
            gwentStateChecker.Setup(o => o.GetCurrentGlobalGameStates())
                .Returns(GlobalGameStates.GameModesTab);

            var waitingService = new Mock<IWaitingService>();
            waitingService.Setup(o => o.Wait(It.IsAny<int>()));

            var inputEmulator = new Mock<IInputDeviceEmulator>();
            // Act
            var arenaModePage = new GameModesPage(
                gwentStateChecker.Object,
                waitingService.Object,
                inputEmulator.Object);
            // Assert
            Assert.IsNotNull(arenaModePage);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void VerifyingPageTest_GlobalGameStatesUnknown_NotCorrectNewObject()
        {
            // Arrage
            var gwentStateChecker = new Mock<IGwentStateChecker>();
            gwentStateChecker.Setup(o => o.GetCurrentGlobalGameStates())
                .Returns(GlobalGameStates.Unknown);

            var waitingService = new Mock<IWaitingService>();
            waitingService.Setup(o => o.Wait(It.IsAny<int>()));

            var inputEmulator = new Mock<IInputDeviceEmulator>();
            // Act
            new GameModesPage(
                gwentStateChecker.Object,
                waitingService.Object,
                inputEmulator.Object);
            // Assert  - Expects exception
        }
    }
}