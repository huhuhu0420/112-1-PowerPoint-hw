using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
/// <summary>
/// Summary description for MainFormUITest
/// </summary>

namespace PowerPointTests
{
    [TestClass()]
    public class MainFormUITest
    {
        private Robot _robot;

        /// <summary>
        /// Launches the Calculator
        /// </summary>
        [TestInitialize()]
        public void Initialize()
        {
            var projectName = "PowerPoint";
            const string MENU_FORM = "MenuForm";
            string solutionPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            var targetAppPath = Path.Combine(solutionPath, projectName, "bin", "Debug", "PowerPoint.exe");
            // Assert.AreEqual("hi", targetAppPath);
            _robot = new Robot(targetAppPath, MENU_FORM);
        }

        /// <summary>
        /// Closes the launched program
        /// </summary>
        [TestCleanup()]
        public void Cleanup()
        {
            _robot.CleanUp();
        }

        /// <summary>
        /// Tests that the result of 123 + 321 should be 444
        /// </summary>
        [TestMethod]
        public void TestAdd()
        {
            _robot.ClickButton("circleButton");
        }
    }

}
