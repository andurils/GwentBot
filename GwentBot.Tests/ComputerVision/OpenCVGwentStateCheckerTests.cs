using System;
using GwentBot.ComputerVision;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;

namespace GwentBot.Tests.ComputerVision
{
    [TestClass]
    public class OpenCvGwentStateCheckerTests
    {
        #region GlobalGameStates Checks

        [TestMethod]
        public void GetCurrentGlobalGameStates_ArenaModeTabSrc_IdentifierArenaModeTab()
        {
            //arrage
            var gameScreenshotPath = @"ComputerVision\GlobalGameStates\ArenaModeTabSrc.png";
            var stateChecker = CreationOpenCvGwentStateChecker(gameScreenshotPath);

            //act
            var result = stateChecker.GetCurrentGlobalGameStates();

            //assert
            Assert.AreEqual(GlobalGameStates.ArenaModeTab, result);
        }

        [DataTestMethod]
        [DataRow(@"ComputerVision\GlobalGameStates\GameModesTabSrc.png")]
        [DataRow(@"ComputerVision\GameModesTab\MonsterTableSrc.png")]
        [DataRow(@"ComputerVision\GameModesTab\NilfgaardTable.png")]
        [DataRow(@"ComputerVision\GameModesTab\NorthTableSrc.png")]
        [DataRow(@"ComputerVision\GameModesTab\ScoiataelTable.png")]
        [DataRow(@"ComputerVision\GameModesTab\SkelligeTable.png")]
        [DataRow(@"ComputerVision\FriendlyGameStartStates\MatchSettingsSrc.png")]
        public void GetCurrentGlobalGameStates_GameModesTabSrc_IdentifierGameModesTab(string srcPath)
        {
            //arrage
            var gameScreenshotPath = srcPath;
            var stateChecker = CreationOpenCvGwentStateChecker(gameScreenshotPath);

            //act
            var result = stateChecker.GetCurrentGlobalGameStates();

            //assert
            Assert.AreEqual(GlobalGameStates.GameModesTab, result);
        }

        [TestMethod]
        public void GetCurrentGlobalGameStates_HeavyLoadingSrc_IdentifierHeavyLoading()
        {
            //arrage
            var gameScreenshotPath = @"ComputerVision\GlobalGameStates\HeavyLoadingSrc.png";
            var stateChecker = CreationOpenCvGwentStateChecker(gameScreenshotPath);

            //act
            var result = stateChecker.GetCurrentGlobalGameStates();

            //assert
            Assert.AreEqual(GlobalGameStates.HeavyLoading, result);
        }

        [TestMethod]
        public void GetCurrentGlobalGameStates_MainMenuSrc_IdentifierMainMenu()
        {
            //arrage
            var gameScreenshotPath = @"ComputerVision\GlobalGameStates\MainMenuSrc.png";
            var stateChecker = CreationOpenCvGwentStateChecker(gameScreenshotPath);

            //act
            var result = stateChecker.GetCurrentGlobalGameStates();

            //assert
            Assert.AreEqual(GlobalGameStates.MainMenu, result);
        }

        [TestMethod]
        public void GetCurrentGlobalGameStates_UnknownSrc_IdentifierUnknown()
        {
            //arrage
            var gameScreenshotPath = @"ComputerVision\TestSrcImg\AlwaysUnknownSrc.png";
            var stateChecker = CreationOpenCvGwentStateChecker(gameScreenshotPath);

            //act
            var result = stateChecker.GetCurrentGlobalGameStates();

            //assert
            Assert.AreEqual(GlobalGameStates.Unknown, result);
        }

        #endregion GlobalGameStates Checks

        #region StartGameStates Checks

        [TestMethod]
        public void GetCurrentStartGameStates_GameLoadingScreenSrc_IdentifierGameLoadingScreen()
        {
            //arrage
            var gameScreenshotPath = @"ComputerVision\StartGameStates\GameLoadingScreen.png";
            var stateChecker = CreationOpenCvGwentStateChecker(gameScreenshotPath);

            //act
            var result = stateChecker.GetCurrentStartGameStates();

            //assert
            Assert.AreEqual(StartGameStates.GameLoadingScreen, result);
        }

        [TestMethod]
        public void GetCurrentStartGameStates_UnknownSrc_IdentifierUnknown()
        {
            //arrage
            var gameScreenshotPath = @"ComputerVision\TestSrcImg\AlwaysUnknownSrc.png";
            var stateChecker = CreationOpenCvGwentStateChecker(gameScreenshotPath);

            //act
            var result = stateChecker.GetCurrentStartGameStates();

            //assert
            Assert.AreEqual(StartGameStates.Unknown, result);
        }

        [TestMethod]
        public void GetCurrentStartGameStates_WelcomeScreenSrc_IdentifierWelcomeScreen()
        {
            //arrage
            var gameScreenshotPath = @"ComputerVision\StartGameStates\WelcomeScreen.png";
            var stateChecker = CreationOpenCvGwentStateChecker(gameScreenshotPath);

            //act
            var result = stateChecker.GetCurrentStartGameStates();

            //assert
            Assert.AreEqual(StartGameStates.WelcomeScreen, result);
        }

        #endregion StartGameStates Checks

        #region FriendlyGameStartStates Checks

        [DataTestMethod]
        [DataRow(@"ComputerVision\FriendlyGameStartStates\LoadingMatchSettingsSrc\MonsterTable.png")]
        [DataRow(@"ComputerVision\FriendlyGameStartStates\LoadingMatchSettingsSrc\NilfgaardTable.png")]
        [DataRow(@"ComputerVision\FriendlyGameStartStates\LoadingMatchSettingsSrc\NorthTable.png")]
        [DataRow(@"ComputerVision\FriendlyGameStartStates\LoadingMatchSettingsSrc\ScoiataelTable.png")]
        [DataRow(@"ComputerVision\FriendlyGameStartStates\LoadingMatchSettingsSrc\SkelligeTable.png")]
        public void GetCurrentFriendlyGameStartStates_LoadingMatchSettingsSrc_IdentifierLoadingMatchSettings(string srcPath)
        {
            //arrage
            var gameScreenshotPath = srcPath;
            var stateChecker = CreationOpenCvGwentStateChecker(gameScreenshotPath);

            //act
            var result = stateChecker.GetCurrentFriendlyGameStartStates();

            //assert
            Assert.AreEqual(FriendlyGameStartStates.LoadingMatchSettings, result);
        }

        [TestMethod]
        public void GetCurrentFriendlyGameStartStates_MatchSettingsSrc_IdentifierMatchSettings()
        {
            //arrage
            var gameScreenshotPath = @"ComputerVision\FriendlyGameStartStates\MatchSettingsSrc.png";
            var stateChecker = CreationOpenCvGwentStateChecker(gameScreenshotPath);

            //act
            var result = stateChecker.GetCurrentFriendlyGameStartStates();

            //assert
            Assert.AreEqual(FriendlyGameStartStates.MatchSettings, result);
        }

        [TestMethod]
        public void GetCurrentFriendlyGameStartStates_UnknownSrc_IdentifierUnknown()
        {
            //arrage
            var gameScreenshotPath = @"ComputerVision\TestSrcImg\AlwaysUnknownSrc.png";
            var stateChecker = CreationOpenCvGwentStateChecker(gameScreenshotPath);

            //act
            var result = stateChecker.GetCurrentFriendlyGameStartStates();

            //assert
            Assert.AreEqual(FriendlyGameStartStates.Unknown, result);
        }

        [DataTestMethod]
        [DataRow(@"ComputerVision\FriendlyGameStartStates\WaitingReadinessOpponentSrc\MonsterTable.png")]
        [DataRow(@"ComputerVision\FriendlyGameStartStates\WaitingReadinessOpponentSrc\NilfgaardTable.png")]
        [DataRow(@"ComputerVision\FriendlyGameStartStates\WaitingReadinessOpponentSrc\NorthTable.png")]
        [DataRow(@"ComputerVision\FriendlyGameStartStates\WaitingReadinessOpponentSrc\ScoiataelTable.png")]
        [DataRow(@"ComputerVision\FriendlyGameStartStates\WaitingReadinessOpponentSrc\SkelligeTable.png")]
        public void GetCurrentFriendlyGameStartStates_WaitingReadinessOpponentSrc_IdentifierWaitingReadinessOpponent(string strPath)
        {
            throw new NotImplementedException();
        }

        #endregion FriendlyGameStartStates Checks

        #region Support Test Method

        private OpenCvGwentStateChecker CreationOpenCvGwentStateChecker(string gameScreenshot)
        {
            var shotCreatorMock = new Mock<IWindowScreenShotCreator>();
            shotCreatorMock.Setup(o => o.IsGameWindowFullVisible()).Returns(true);
            shotCreatorMock.Setup(o => o.GetGameScreenshot()).Returns(
                new Bitmap(gameScreenshot));

            return new OpenCvGwentStateChecker(shotCreatorMock.Object);
        }

        #endregion
    }
}