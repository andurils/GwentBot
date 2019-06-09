using GwentBot.PageObjects;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

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
            // Act
            var arenaModePage = new GameModesPage(
                gwentStateChecker.Object,
                waitingService.Object);
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
            // Act
            new GameModesPage(
                gwentStateChecker.Object,
                waitingService.Object);
            // Assert  - Expects exception
        }
    }
}