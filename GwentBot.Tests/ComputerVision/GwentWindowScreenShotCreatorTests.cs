using System;
using System.Text;
using System.Collections.Generic;
using GwentBot.ComputerVision;
using GwentBot.WorkWithProcess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GwentBot.Tests.ComputerVision
{
    /// <summary>
    /// Summary description for GwentWindowScreenShotCreatorTests
    /// </summary>
    [TestClass]
    public class GwentWindowScreenShotCreatorTests
    {
        [TestMethod]
        public void NewObjectCreationTest()
        {
            // Arrage
            var screenShotCreator = new GwentWindowScreenShotCreator();
            // Act
            var result = screenShotCreator.WorkingProcessInformation is GwentProcessInformation;
            // Assert
            Assert.IsTrue(result);
        }
    }
}