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
            var shotCreatorMock = new Mock<IWindowScreenShotCreator>();
            shotCreatorMock.Setup(o => o.IsGameWindowFullVisible()).Returns(true);
            shotCreatorMock.Setup(o => o.GetGameScreenshot()).Returns(
                new Bitmap(@"ComputerVision\GlobalGameStates\ArenaModeTabSrc.png"));

            var stateChecker = new OpenCvGwentStateChecker(shotCreatorMock.Object);

            var result = stateChecker.GetCurrentGlobalGameStates();

            Assert.AreEqual(GlobalGameStates.ArenaModeTab, result);
        }

        [DataTestMethod]
        [DataRow(@"ComputerVision\GlobalGameStates\GameModesTabSrc.png")]
        [DataRow(@"ComputerVision\GameModesTab\Monster TableSrc.png")]
        [DataRow(@"ComputerVision\GameModesTab\NilfgaardTable.png")]
        [DataRow(@"ComputerVision\GameModesTab\NorthTableSrc.png")]
        [DataRow(@"ComputerVision\GameModesTab\ScoiataelTable.png")]
        [DataRow(@"ComputerVision\GameModesTab\SkelligeTable.png")]
        public void GetCurrentGlobalGameStates_GameModesTabSrc_IdentifierGameModesTab(string srcpath)
        {
            var shotCreatorMock = new Mock<IWindowScreenShotCreator>();
            shotCreatorMock.Setup(o => o.IsGameWindowFullVisible()).Returns(true);
            shotCreatorMock.Setup(o => o.GetGameScreenshot()).Returns(
                new Bitmap(srcpath));

            var stateChecker = new OpenCvGwentStateChecker(shotCreatorMock.Object);

            var result = stateChecker.GetCurrentGlobalGameStates();

            Assert.AreEqual(GlobalGameStates.GameModesTab, result);
        }

        [TestMethod]
        public void GetCurrentGlobalGameStates_HeavyLoadingSrc_IdentifierHeavyLoading()
        {
            var shotCreatorMock = new Mock<IWindowScreenShotCreator>();
            shotCreatorMock.Setup(o => o.IsGameWindowFullVisible()).Returns(true);
            shotCreatorMock.Setup(o => o.GetGameScreenshot()).Returns(
                new Bitmap(@"ComputerVision\GlobalGameStates\HeavyLoadingSrc.png"));

            var stateChecker = new OpenCvGwentStateChecker(shotCreatorMock.Object);

            var result = stateChecker.GetCurrentGlobalGameStates();

            Assert.AreEqual(GlobalGameStates.HeavyLoading, result);
        }

        [TestMethod]
        public void GetCurrentGlobalGameStates_MainMenuSrc_IdentifierMainMenu()
        {
            var shotCreatorMock = new Mock<IWindowScreenShotCreator>();
            shotCreatorMock.Setup(o => o.IsGameWindowFullVisible()).Returns(true);
            shotCreatorMock.Setup(o => o.GetGameScreenshot()).Returns(
                new Bitmap(@"ComputerVision\GlobalGameStates\MainMenuSrc.png"));

            var stateChecker = new OpenCvGwentStateChecker(shotCreatorMock.Object);

            var result = stateChecker.GetCurrentGlobalGameStates();

            Assert.AreEqual(GlobalGameStates.MainMenu, result);
        }

        [TestMethod]
        public void GetCurrentGlobalGameStates_UnknownSrc_IdentifierUnknown()
        {
            var shotCreatorMock = new Mock<IWindowScreenShotCreator>();
            shotCreatorMock.Setup(o => o.IsGameWindowFullVisible()).Returns(true);
            shotCreatorMock.Setup(o => o.GetGameScreenshot()).Returns(
                new Bitmap(@"ComputerVision\TestSrcImg\AlwaysUnknownSrc.png"));

            var stateChecker = new OpenCvGwentStateChecker(shotCreatorMock.Object);

            var result = stateChecker.GetCurrentGlobalGameStates();

            Assert.AreEqual(GlobalGameStates.Unknown, result);
        }

        #endregion GlobalGameStates Checks

        #region StartGameStates Checks

        [TestMethod]
        public void GetCurrentStartGameStates_GameLoadingScreenSrc_IdentifierGameLoadingScreen()
        {
            var shotCreatorMock = new Mock<IWindowScreenShotCreator>();
            shotCreatorMock.Setup(o => o.IsGameWindowFullVisible()).Returns(true);
            shotCreatorMock.Setup(o => o.GetGameScreenshot()).Returns(
                new Bitmap(@"ComputerVision\StartGameStates\GameLoadingScreen.png"));

            var stateChecker = new OpenCvGwentStateChecker(shotCreatorMock.Object);

            var result = stateChecker.GetCurrentStartGameStates();

            Assert.AreEqual(StartGameStates.GameLoadingScreen, result);
        }

        [TestMethod]
        public void GetCurrentStartGameStates_UnknownSrc_IdentifierUnknown()
        {
            var shotCreatorMock = new Mock<IWindowScreenShotCreator>();
            shotCreatorMock.Setup(o => o.IsGameWindowFullVisible()).Returns(true);
            shotCreatorMock.Setup(o => o.GetGameScreenshot()).Returns(
                new Bitmap(@"ComputerVision\TestSrcImg\AlwaysUnknownSrc.png"));

            var stateChecker = new OpenCvGwentStateChecker(shotCreatorMock.Object);

            var result = stateChecker.GetCurrentStartGameStates();

            Assert.AreEqual(StartGameStates.Unknown, result);
        }

        [TestMethod]
        public void GetCurrentStartGameStates_WelcomeScreenSrc_IdentifierWelcomeScreen()
        {
            var shotCreatorMock = new Mock<IWindowScreenShotCreator>();
            shotCreatorMock.Setup(o => o.IsGameWindowFullVisible()).Returns(true);
            shotCreatorMock.Setup(o => o.GetGameScreenshot()).Returns(
                new Bitmap(@"ComputerVision\StartGameStates\WelcomeScreen.png"));

            var stateChecker = new OpenCvGwentStateChecker(shotCreatorMock.Object);

            var result = stateChecker.GetCurrentStartGameStates();

            Assert.AreEqual(StartGameStates.WelcomeScreen, result);
        }

        #endregion StartGameStates Checks

        #region FriendlyGameStartStates Checks

        [TestMethod]
        public void GetCurrentFriendlyGameStartStates_LoadingMatchSettingsSrc_IdentifierLoadingMatchSettings()
        {
        }

        [TestMethod]
        public void GetCurrentFriendlyGameStartStates_MatchSettingsSrc_IdentifierMatchSettings()
        {
            var shotCreatorMock = new Mock<IWindowScreenShotCreator>();
            shotCreatorMock.Setup(o => o.IsGameWindowFullVisible()).Returns(true);
            shotCreatorMock.Setup(o => o.GetGameScreenshot()).Returns(
                new Bitmap(@"ComputerVision\FriendlyGameStartStates\MatchSettingsSrc.png"));

            var stateChecker = new OpenCvGwentStateChecker(shotCreatorMock.Object);

            var result = stateChecker.GetCurrentFriendlyGameStartStates();

            Assert.AreEqual(FriendlyGameStartStates.MatchSettings, result);
        }

        [TestMethod]
        public void GetCurrentFriendlyGameStartStates_UnknownSrc_IdentifierUnknown()
        {
            var shotCreatorMock = new Mock<IWindowScreenShotCreator>();
            shotCreatorMock.Setup(o => o.IsGameWindowFullVisible()).Returns(true);
            shotCreatorMock.Setup(o => o.GetGameScreenshot()).Returns(
                new Bitmap(@"ComputerVision\TestSrcImg\AlwaysUnknownSrc.png"));

            var stateChecker = new OpenCvGwentStateChecker(shotCreatorMock.Object);

            var result = stateChecker.GetCurrentFriendlyGameStartStates();

            Assert.AreEqual(FriendlyGameStartStates.Unknown, result);
        }

        [TestMethod]
        public void GetCurrentFriendlyGameStartStates_WaitingReadinessOpponentSrc_IdentifierWaitingReadinessOpponent()
        {
        }

        #endregion FriendlyGameStartStates Checks
    }
}