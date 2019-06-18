using System;
using GwentBot.GameInput;
using GwentBot.PageObjects;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GwentBot.Tests.PageObjects
{
    [TestClass]
    public class RewardsTabPageTests
    {
        [TestMethod]
        public void VerifyingPageTest_RewardsTab_CorrectNewObject()
        {
            // Arrage
            var gwentStateChecker = new Mock<IGwentStateChecker>();
            gwentStateChecker.Setup(o => o.GetCurrentNotifications())
                .Returns(Notifications.RewardsTab);

            var waitingService = new Mock<IWaitingService>();
            waitingService.Setup(o => o.Wait(It.IsAny<int>()));

            var inputEmulator = new Mock<IInputDeviceEmulator>();
            // Act
            var result = new RewardsTabPage(
                gwentStateChecker.Object,
                waitingService.Object,
                inputEmulator.Object);
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(exceptionType: typeof(Exception))]
        public void VerifyingPageTest_RewardsTab_NotCorrectNewObject()
        {
            // Arrage
            var gwentStateChecker = new Mock<IGwentStateChecker>();
            gwentStateChecker.Setup(o => o.GetCurrentNotifications())
                .Returns(Notifications.NoNotifications);

            var waitingService = new Mock<IWaitingService>();
            waitingService.Setup(o => o.Wait(It.IsAny<int>()));

            var inputEmulator = new Mock<IInputDeviceEmulator>();
            // Act
            new RewardsTabPage(
                gwentStateChecker.Object,
                waitingService.Object,
                inputEmulator.Object);
            // Assert - Expects exception
        }
    }
}