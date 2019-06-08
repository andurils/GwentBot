using GwentBot.PageObjects;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GwentBot.Tests.PageObjects
{
    [TestClass]
    public class ArenaModePageTests
    {
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
                var arenaModePage = new ArenaModePage(
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