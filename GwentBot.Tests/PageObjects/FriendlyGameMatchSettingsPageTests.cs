using GwentBot.PageObjects;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace GwentBot.Tests.PageObjects
{
    [TestClass]
    public class FriendlyGameMatchSettingsPageTests
    {
        [TestMethod]
        public void VerifyingPageTest_FriendlyGameStartStatesUnknown_CorrectNewObject()
        {
            // Arrage
            var gwentStateChecker = new Mock<IGwentStateChecker>();
            gwentStateChecker.Setup(o => o.GetCurrentFriendlyGameStartStates())
                .Returns(FriendlyGameStartStates.MatchSettings);

            var waitingService = new Mock<IWaitingService>();
            waitingService.Setup(o => o.Wait(It.IsAny<int>()));
            // Act
            var result = new FriendlyGameMatchSettingsPage(
                gwentStateChecker.Object,
                waitingService.Object);
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void VerifyingPageTest_FriendlyGameStartStatesUnknown_NotCorrectNewObject()
        {
            // Arrage
            var gwentStateChecker = new Mock<IGwentStateChecker>();
            gwentStateChecker.Setup(o => o.GetCurrentFriendlyGameStartStates())
                .Returns(FriendlyGameStartStates.Unknown);

            var waitingService = new Mock<IWaitingService>();
            waitingService.Setup(o => o.Wait(It.IsAny<int>()));
            // Act
            new FriendlyGameMatchSettingsPage(
                gwentStateChecker.Object,
                waitingService.Object);
            // Assert  - Expects exception
        }
    }
}