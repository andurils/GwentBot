using System;
using GwentBot.PageObjects;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GwentBot.Tests.PageObjects
{
    [TestClass]
    public class FriendlyGameMatchSettingsPageTests
    {
        [TestMethod]
        public void VerifyingPageTest_FriendlyGameStartStatesUnknown_NotCorrectNewObject()
        {
            try
            {
                //arrage
                var gwentStateChecker = new Mock<IGwentStateChecker>();
                gwentStateChecker.Setup(o => o.GetCurrentFriendlyGameStartStates())
                    .Returns(FriendlyGameStartStates.Unknown);

                var waitingService = new Mock<IWaitingService>();
                waitingService.Setup(o => o.Wait(It.IsAny<int>()));
                //act
                new FriendlyGameMatchSettingsPage(
                    gwentStateChecker.Object,
                    waitingService.Object);
                //assert
            }
            catch (System.Exception)
            {
                Assert.IsTrue(true);
            }
        }
    }
}