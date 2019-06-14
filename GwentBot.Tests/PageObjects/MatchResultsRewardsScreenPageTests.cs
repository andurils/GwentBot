using System;
using GwentBot.Model;
using GwentBot.PageObjects;
using GwentBot.PageObjects.Abstract;
using GwentBot.StateAbstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GwentBot.Tests.PageObjects
{
    [TestClass]
    public class MatchResultsRewardsScreenPageTests
    {
        [TestMethod]
        public void VerifyingPageTest_MatchResultsScreen_CorrectNewObject()
        {
            // Arrage
            var gwentStateChecker = new Mock<IGwentStateChecker>();
            gwentStateChecker.Setup(o => o.GetCurrentGameSessionStates())
                .Returns(GameSessionStates.MatchResultsScreen);

            var waitingService = new Mock<IWaitingService>();
            waitingService.Setup(o => o.Wait(It.IsAny<int>()));
            // Act
            var result = new MatchResultsRewardsScreenPage(
                gwentStateChecker.Object,
                waitingService.Object
                , new Game(new Deck("sdf"), new User("sdf")));
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void VerifyingPageTest_MatchRewardsScreen_CorrectNewObject()
        {
            // Arrage
            var gwentStateChecker = new Mock<IGwentStateChecker>();
            gwentStateChecker.Setup(o => o.GetCurrentGameSessionStates())
                .Returns(GameSessionStates.MatchRewardsScreen);

            var waitingService = new Mock<IWaitingService>();
            waitingService.Setup(o => o.Wait(It.IsAny<int>()));
            // Act
            var result = new MatchResultsRewardsScreenPage(
                gwentStateChecker.Object,
                waitingService.Object
                , new Game(new Deck("sdf"), new User("sdf")));
            // Assert
            Assert.IsNotNull(result);
        }
    }
}