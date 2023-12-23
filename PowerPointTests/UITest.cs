using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OpenQA.Selenium.Appium.Interactions;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using PowerPoint;
using PointerInputDevice = OpenQA.Selenium.Interactions.PointerInputDevice;

namespace PowerPointTests
{
    [TestClass()]
    public class PowerPointTests
    {
        private const string CIRCLE = "○";
        private const string PANEL1 = "panel1";
        const string MENU_FORM = "MenuForm";
        const string DATA_GRID = "dataGridView1";
        const string SHAPE_CHINESE = "形狀";
        const string INFO_CHINESE = "資訊";
        const string CIRCLE_CHINESE = "圓形";
        
        private Robot _robot;
        private WindowsElement _canvas;

        /// <summary>
        /// Launches the Calculator
        /// </summary>
        [TestInitialize()]
        public void Initialize()
        {
            var projectName = "PowerPoint";
            string solutionPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            var targetAppPath = Path.Combine(solutionPath, projectName, "bin", "Debug", "PowerPoint.exe");
            // Assert.AreEqual("hi", targetAppPath);
            _robot = new Robot(targetAppPath, MENU_FORM);
            _canvas = _robot.FindElementById(PANEL1);
        }
        
        // get info
        public string GetInfo(Point point1, Point point2)
        {
            return FormatCoordinate(point1) + Constant.COMMA + FormatCoordinate(point2);
        }
        
        // format coordinate
        private string FormatCoordinate(PointF point)
        {
            return Constant.PARENTHESIS1 + (int)point.X + Constant.COMMA + (int)point.Y + Constant.PARENTHESIS2;
        }
        
        // test
        public Interaction CreateMoveTo(PointerInputDevice device, int x, int y)
        {
            var size = _canvas.Size;
            return device.CreatePointerMove(_canvas, x - size.Width / 2, y - size.Height / 2, TimeSpan.Zero);
        }
        
        // draw
        public void DrawShape(string shapeName, Point leftTop, Point rightBottom)
        {
            _robot.ClickByElementName(shapeName);
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice pointer = new PointerInputDevice(PointerKind.Pen);
            actionBuilder
                .AddAction(CreateMoveTo(pointer, leftTop.X, leftTop.Y))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(CreateMoveTo(pointer, rightBottom.X, rightBottom.Y))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left));
            _robot.PerformAction(actionBuilder.ToActionSequenceList());
        }

        // get state
        public AccessibleStates GetButtonState(string name)
        {
            string stateValue = _robot.FindElementByName(name).GetAttribute("LegacyState");
            return (AccessibleStates)Enum.Parse(typeof(AccessibleStates), stateValue);
        }

        // test
        [TestCleanup()]
        public void Cleanup()
        {
            _robot.CleanUp();
        }

        // test
        [TestMethod]
        public void TestCircle()
        {
            _robot.ClickByElementName(CIRCLE);
            Point point1 = new Point(Constant.ONE_HUNDRED, Constant.ONE_HUNDRED);
            Point point2 = new Point(Constant.TWO_HUNDRED, Constant.TWO_HUNDRED);
            Assert.AreEqual(AccessibleStates.Checked, GetButtonState(CIRCLE) & AccessibleStates.Checked);
            DrawShape(CIRCLE, point1, point2);
            Assert.AreEqual(CIRCLE_CHINESE, _robot.FindElementByName(SHAPE_CHINESE + " Row 0").Text);
            Assert.AreEqual(GetInfo(point1, point2), _robot.FindElementByName(INFO_CHINESE + " Row 0").Text);
        }
    }
}
