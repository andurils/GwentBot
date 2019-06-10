using GwentBot.Model;
using GwentBot.StateAbstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GwentBot.Tests.Model
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void GameIWonCoinPropTest_true_false()
        {
            // Arrage
            var deck = new Deck("TestDeck");
            var user = new User("TestUser");
            var game = new Game(deck, user);
            game.IWonCoin = false;

            // Act
            game.IWonCoin = true;

            // Assert
            Assert.IsFalse((bool)game.IWonCoin);
        }

        [TestMethod]
        public void PropertyInitializationTest()
        {
            // Arrage
            var deck = new Deck("TestDeck");
            var user = new User("TestUser");
            var game = new Game(deck, user);
            bool result;

            // Act
            result = deck.Name == game.Deck.Name &
                user.UserName == game.User.UserName;

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PropertyIsOnlineGameTest()
        {
            // Arrage
            var gwentStateChecker = new Mock<IGwentStateChecker>();
            gwentStateChecker.Setup(o => o.GetCurrentGlobalGameStates())
                .Returns(GlobalGameStates.Unknown);

            var deck = new Deck("TestDeck");
            var user = new User("TestUser");
            var game = new Game(deck, user);
            game.MatchRewardsScreenBitmap = gwentStateChecker.Object.GetGameScreenshotBitmap();

            // Act
            bool result = game.IsOnlineGame;

            // Assert
            Assert.IsTrue(result);
        }
    }
}