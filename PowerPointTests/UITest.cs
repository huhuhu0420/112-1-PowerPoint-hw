using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;
using OpenQA.Selenium.Appium;
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
        private const string RECTANGLE = "⬜";
        private const string LINE = "📏";
        private const string PANEL1 = "panel1";
        private const string UNDO = "⟲";
        private const string REDO = "⟳";
        private const string NEW_PAGE = "📰";
        private const string SAVE = "💾";
        private const string LOAD = "⬇";
        private const string OK = "OK";
        private const string FLOW_LAYOUT_PANEL1 = "flowLayoutPanel1";
        private const string SLIDE = "Slide";
        const string MENU_FORM = "MenuForm";
        const string DATA_GRID = "dataGridView1";
        const string SHAPE_CHINESE = "形狀";
        const string INFO_CHINESE = "資訊";
        const string DELETE_CHINESE = "刪除";
        const string CIRCLE_CHINESE = "圓形";
        const string RECTANGLE_CHINESE = "矩形";
        const string LINE_CHINESE = "線";
        const string NEW_CHINESE = "新增";
        
        private Robot _robot;
        private WindowsElement _canvas;
        private WindowsElement _flowLayoutPanel1;
        private Random _random;

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
            _random = new Random();
            _flowLayoutPanel1 = _robot.FindElementById(FLOW_LAYOUT_PANEL1);
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

        public IReadOnlyCollection<AppiumWebElement> GetSlide()
        {
            return _flowLayoutPanel1.FindElementsByAccessibilityId(SLIDE);
            // return _robot.FindElementById(FLOW_LAYOUT_PANEL1)
            // .FindElementsByClassName("WindowsForms10.Window.8.app.0.141b42a_r8_ad1");
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
            _robot.ClickByElementName(DELETE_CHINESE + " Row 0");
        }
        
        // test
        [TestMethod]
        public void TestRectangle()
        {
            _robot.ClickByElementName(RECTANGLE);
            Point point1 = new Point(Constant.ONE_HUNDRED, Constant.ONE_HUNDRED);
            Point point2 = new Point(Constant.TWO_HUNDRED, Constant.TWO_HUNDRED);
            Assert.AreEqual(AccessibleStates.Checked, GetButtonState(RECTANGLE) & AccessibleStates.Checked);
            DrawShape(RECTANGLE, point1, point2);
            Assert.AreEqual(RECTANGLE_CHINESE, _robot.FindElementByName(SHAPE_CHINESE + " Row 0").Text);
            Assert.AreEqual(GetInfo(point1, point2), _robot.FindElementByName(INFO_CHINESE + " Row 0").Text);
            _robot.ClickByElementName(DELETE_CHINESE + " Row 0");
        }
        
        // test
        [TestMethod]
        public void TestLine()
        {
            _robot.ClickByElementName(LINE);
            Point point1 = new Point(Constant.ONE_HUNDRED, Constant.ONE_HUNDRED);
            Point point2 = new Point(Constant.TWO_HUNDRED, Constant.TWO_HUNDRED);
            Assert.AreEqual(AccessibleStates.Checked, GetButtonState(LINE) & AccessibleStates.Checked);
            DrawShape(LINE, point1, point2);
            Assert.AreEqual(LINE_CHINESE, _robot.FindElementByName(SHAPE_CHINESE + " Row 0").Text);
            Assert.AreEqual(GetInfo(point1, point2), _robot.FindElementByName(INFO_CHINESE + " Row 0").Text);
            _robot.ClickByElementName(DELETE_CHINESE + " Row 0");
        }
        
        // test
        [TestMethod]
        public void TestDataGridViewCircle()
        {
            Point point1 = new Point(_random.Next(0, _canvas.Size.Width), _random.Next(0, _canvas.Size.Height));
            Point point2 = new Point(_random.Next(point1.X, _canvas.Size.Width), _random.Next(point1.Y, _canvas.Size.Height));
            _robot.FindElementByName("Open").Click();
            _robot.FindElementByName(CIRCLE_CHINESE).Click();
            _robot.ClickByElementName(NEW_CHINESE);
            _robot.FindElementByName(Constant.TOP_LEFT_X).SendKeys(point1.X.ToString());
            _robot.FindElementByName(Constant.TOP_LEFT_Y).SendKeys(point1.Y.ToString());
            _robot.FindElementByName(Constant.BOTTOM_RIGHT_X).SendKeys(point2.X.ToString());
            _robot.FindElementByName(Constant.BOTTOM_RIGHT_Y).SendKeys(point2.Y.ToString());
            _robot.FindElementByName("OK").Click();
            Assert.AreEqual(CIRCLE_CHINESE, _robot.FindElementByName(SHAPE_CHINESE + " Row 0").Text);
            Assert.AreEqual(GetInfo(point1, point2), _robot.FindElementByName(INFO_CHINESE + " Row 0").Text);
        }
        
        // test
        [TestMethod]
        public void TestDataGridViewRectangle()
        {
            Point point1 = new Point(_random.Next(0, _canvas.Size.Width), _random.Next(0, _canvas.Size.Height));
            Point point2 = new Point(_random.Next(point1.X + 1, _canvas.Size.Width), _random.Next(point1.Y + 1, _canvas.Size.Height));
            _robot.FindElementByName("Open").Click();
            _robot.FindElementByName(RECTANGLE_CHINESE).Click();
            _robot.ClickByElementName(NEW_CHINESE);
            _robot.FindElementByName(Constant.TOP_LEFT_X).SendKeys(point1.X.ToString());
            _robot.FindElementByName(Constant.TOP_LEFT_Y).SendKeys(point1.Y.ToString());
            _robot.FindElementByName(Constant.BOTTOM_RIGHT_X).SendKeys(point2.X.ToString());
            _robot.FindElementByName(Constant.BOTTOM_RIGHT_Y).SendKeys(point2.Y.ToString());
            _robot.FindElementByName("OK").Click();
            Assert.AreEqual(RECTANGLE_CHINESE, _robot.FindElementByName(SHAPE_CHINESE + " Row 0").Text);
            Assert.AreEqual(GetInfo(point1, point2), _robot.FindElementByName(INFO_CHINESE + " Row 0").Text);
        }
        
        // test
        [TestMethod]
        public void TestDataGridViewLine()
        {
            Point point1 = new Point(_random.Next(0, _canvas.Size.Width), _random.Next(0, _canvas.Size.Height));
            Point point2 = new Point(_random.Next(point1.X + 1, _canvas.Size.Width), _random.Next(point1.Y + 1, _canvas.Size.Height));
            _robot.FindElementByName("Open").Click();
            _robot.FindElementByName(LINE_CHINESE).Click();
            _robot.ClickByElementName(NEW_CHINESE);
            _robot.FindElementByName(Constant.TOP_LEFT_X).SendKeys(point1.X.ToString());
            _robot.FindElementByName(Constant.TOP_LEFT_Y).SendKeys(point1.Y.ToString());
            _robot.FindElementByName(Constant.BOTTOM_RIGHT_X).SendKeys(point2.X.ToString());
            _robot.FindElementByName(Constant.BOTTOM_RIGHT_Y).SendKeys(point2.Y.ToString());
            _robot.FindElementByName("OK").Click();
            Assert.AreEqual(LINE_CHINESE, _robot.FindElementByName(SHAPE_CHINESE + " Row 0").Text);
            Assert.AreEqual(GetInfo(point1, point2), _robot.FindElementByName(INFO_CHINESE + " Row 0").Text);
        }
        
        // test
        [TestMethod]
        public void TestMoveShape()
        {
            Point point1 = new Point(0, 0);
            Point point2 = new Point(Constant.ONE_HUNDRED, Constant.ONE_HUNDRED);
            Point middlePoint = new Point((point1.X + point2.X) / Constant.TWO, (point1.Y + point2.Y) / Constant.TWO);
            
            DrawShape(CIRCLE, point1, point2);
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice pointer = new PointerInputDevice(PointerKind.Pen);
            actionBuilder
                .AddAction(CreateMoveTo(pointer, point1.X, point1.Y))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(CreateMoveTo(pointer, point2.X, point2.Y))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left))
                .AddAction(CreateMoveTo(pointer, middlePoint.X, middlePoint.Y))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(CreateMoveTo(pointer, middlePoint.X + Constant.ONE_HUNDRED, middlePoint.Y))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left));
            _robot.PerformAction(actionBuilder.ToActionSequenceList());
            Assert.AreEqual(GetInfo(new Point(Constant.ONE_HUNDRED, 0), new Point(Constant.TWO_HUNDRED, Constant.ONE_HUNDRED)), _robot.FindElementByName(INFO_CHINESE + " Row 0").Text);
        }
        
        // test
        [TestMethod]
        public void TestDrawUndoRedo()
        {
            Point point1 = new Point(0, 0);
            Point point2 = new Point(Constant.ONE_HUNDRED, Constant.ONE_HUNDRED);
            DrawShape(CIRCLE, point1 , point2);
            _robot.FindElementByName(UNDO).Click();
            Assert.AreEqual("0", _robot.FindElementById(DATA_GRID).GetAttribute("Grid.RowCount"));
            _robot.FindElementByName(REDO).Click();
            Assert.AreEqual(CIRCLE_CHINESE, _robot.FindElementByName(SHAPE_CHINESE + " Row 0").Text);
            Assert.AreEqual(GetInfo(point1, point2), _robot.FindElementByName(INFO_CHINESE + " Row 0").Text);
        }
        
        // test
        [TestMethod]
        public void TestMoveUndoRedo()
        {
            Point point1 = new Point(0, 0);
            Point point2 = new Point(Constant.ONE_HUNDRED, Constant.ONE_HUNDRED);
            Point middlePoint = new Point((point1.X + point2.X) / Constant.TWO, (point1.Y + point2.Y) / Constant.TWO);
            DrawShape(CIRCLE, point1, point2);
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice pointer = new PointerInputDevice(PointerKind.Pen);
            actionBuilder
                .AddAction(CreateMoveTo(pointer, point1.X, point1.Y))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(CreateMoveTo(pointer, point2.X, point2.Y))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left))
                .AddAction(CreateMoveTo(pointer, middlePoint.X, middlePoint.Y))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(CreateMoveTo(pointer, middlePoint.X + Constant.ONE_HUNDRED, middlePoint.Y))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left));
            _robot.PerformAction(actionBuilder.ToActionSequenceList());
            _robot.FindElementByName(UNDO).Click();
            Assert.AreEqual(GetInfo(point1, point2), _robot.FindElementByName(INFO_CHINESE + " Row 0").Text);
            _robot.FindElementByName(REDO).Click();
            Assert.AreEqual(GetInfo(new Point(Constant.ONE_HUNDRED, 0), new Point(Constant.TWO_HUNDRED, Constant.ONE_HUNDRED)), _robot.FindElementByName(INFO_CHINESE + " Row 0").Text);
        }
        
        // test
        [TestMethod]
        public void TestDataGridViewUndoRedo()
        {
            Point point1 = new Point(_random.Next(0, _canvas.Size.Width), _random.Next(0, _canvas.Size.Height));
            Point point2 = new Point(_random.Next(point1.X + 1, _canvas.Size.Width), _random.Next(point1.Y + 1, _canvas.Size.Height));
            _robot.FindElementByName("Open").Click();
            _robot.FindElementByName(CIRCLE_CHINESE).Click();
            _robot.ClickByElementName(NEW_CHINESE);
            _robot.FindElementByName(Constant.TOP_LEFT_X).SendKeys(point1.X.ToString());
            _robot.FindElementByName(Constant.TOP_LEFT_Y).SendKeys(point1.Y.ToString());
            _robot.FindElementByName(Constant.BOTTOM_RIGHT_X).SendKeys(point2.X.ToString());
            _robot.FindElementByName(Constant.BOTTOM_RIGHT_Y).SendKeys(point2.Y.ToString());
            _robot.FindElementByName("OK").Click();
            Assert.AreEqual(CIRCLE_CHINESE, _robot.FindElementByName(SHAPE_CHINESE + " Row 0").Text);
            Assert.AreEqual(GetInfo(point1, point2), _robot.FindElementByName(INFO_CHINESE + " Row 0").Text);
            _robot.FindElementByName(UNDO).Click();
            Assert.AreEqual("0", _robot.FindElementById(DATA_GRID).GetAttribute("Grid.RowCount"));
            _robot.FindElementByName(REDO).Click();
            Assert.AreEqual(CIRCLE_CHINESE, _robot.FindElementByName(SHAPE_CHINESE + " Row 0").Text);
            Assert.AreEqual(GetInfo(point1, point2), _robot.FindElementByName(INFO_CHINESE + " Row 0").Text);
        }
        
        // test
        [TestMethod]
        public void TestResizeUndoRedo()
        {
            Point point1 = new Point(Constant.TEN, Constant.TEN);
            Point point2 = new Point(Constant.ONE_HUNDRED, Constant.ONE_HUNDRED);
            Point newPoint2 = new Point(Constant.TWO_HUNDRED, Constant.TWO_HUNDRED);
            Point middlePoint = new Point((point1.X + point2.X) / Constant.TWO, (point1.Y + point2.Y) / Constant.TWO);
            DrawShape(CIRCLE, point1, point2);
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice pointer = new PointerInputDevice(PointerKind.Pen);
            actionBuilder
                .AddAction(CreateMoveTo(pointer, point2.X, point2.Y))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(CreateMoveTo(pointer, Constant.TWO_HUNDRED, Constant.TWO_HUNDRED))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left))
                .AddAction(CreateMoveTo(pointer, middlePoint.X, middlePoint.Y))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left))
                .AddAction(CreateMoveTo(pointer, 0, 0))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left));
            _robot.PerformAction(actionBuilder.ToActionSequenceList());
            _robot.Sleep(1.0);
            _robot.FindElementByName(UNDO).Click();
            Assert.AreEqual(GetInfo(point1, point2), _robot.FindElementByName(INFO_CHINESE + " Row 0").Text);
            _robot.FindElementByName(REDO).Click();
            Assert.AreEqual(GetInfo(point1, newPoint2), _robot.FindElementByName(INFO_CHINESE + " Row 0").Text);
        }
        
        // test
        [TestMethod]
        public void TestWindowResize()
        {
            _robot.Manage().Window.Size = new Size(500, 300);
            var ratio = (float)_canvas.Size.Width / (float)_canvas.Size.Height;
            Assert.IsTrue(Math.Abs(ratio - 16.0/9.0) < 0.1);
            var slides = GetSlide();
            foreach (var slide in slides)
            {
                var slideRatio = (float)slide.Size.Width / (float)slide.Size.Height;
                Assert.IsTrue(Math.Abs(slideRatio - 16.0/9.0) < 0.1);
            }
        }
        
        // test
        [TestMethod]
        public void TestNewDeletePage()
        {
            _robot.ClickByElementName(NEW_PAGE);
            Assert.AreEqual(2, GetSlide().Count);
            Actions actions = new Actions(_robot.GetDriver());
            actions.SendKeys(OpenQA.Selenium.Keys.Delete).Perform();
            Assert.AreEqual(1, GetSlide().Count);
        }
        
        // test
        [TestMethod]
        public void TestSaveLoad()
        {
            DrawShape(CIRCLE, new Point(0, 0), new Point(Constant.ONE_HUNDRED, Constant.ONE_HUNDRED));
            _robot.ClickByElementName(SAVE);
            _robot.ClickByElementName(OK);
            Assert.AreEqual(AccessibleStates.Unavailable, GetButtonState(SAVE) & AccessibleStates.Unavailable);
            DrawShape(RECTANGLE, new Point(Constant.ONE_HUNDRED, Constant.ONE_HUNDRED), new Point(Constant.TWO_HUNDRED, Constant.TWO_HUNDRED));
            Thread.Sleep(Constant.DELAY + Constant.THOUSAND * 2);
            Assert.AreNotEqual(AccessibleStates.Unavailable, GetButtonState(SAVE) & AccessibleStates.Unavailable);
            _robot.ClickByElementName(DELETE_CHINESE + " Row 0");
            Thread.Sleep(Constant.FIVE_HUNDRED);
            _robot.ClickByElementName(DELETE_CHINESE + " Row 0");
            _robot.ClickByElementName(LOAD);
            _robot.ClickByElementName(OK);
            Assert.AreEqual(GetInfo(new Point(0, 0), new Point(Constant.ONE_HUNDRED, Constant.ONE_HUNDRED)), _robot.FindElementByName(INFO_CHINESE + " Row 0").Text);
        }
        
        // test
        [TestMethod]
        public void IntegrationTest()
        {
            _robot.Manage().Window.Size = new Size(1600, 1000);
            DrawHouse();
            _robot.ClickByElementName(NEW_PAGE);
            DrawCat();
            _robot.ClickByElementName(SAVE);
            _robot.ClickByElementName(OK);
            Thread.Sleep(Constant.DELAY + Constant.THOUSAND * 2);
            Actions actions = new Actions(_robot.GetDriver());
            actions.SendKeys(OpenQA.Selenium.Keys.Delete).Perform();
            _robot.ClickByElementName(LOAD);
            _robot.ClickByElementName(OK);
        }

        public void DrawHouse()
        {
            ActionBuilder actionBuilder = new ActionBuilder();
            ActionBuilder actionBuilder2 = new ActionBuilder();
            PointerInputDevice pointer = new PointerInputDevice(PointerKind.Pen);
            // Draw the base of the house
            DrawShape(RECTANGLE, new Point(100, 200), new Point(300, 400));
            actionBuilder
                .AddAction(CreateMoveTo(pointer, 100, 200))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(CreateMoveTo(pointer, 150, 300))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left))
                .AddAction(CreateMoveTo(pointer, 0, 0))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left));
            _robot.PerformAction(actionBuilder.ToActionSequenceList());
            _robot.Sleep(1.0);
            _robot.FindElementByName(UNDO).Click();

            // Draw the roof of the house
            DrawShape(LINE, new Point(100, 200), new Point(200, 100));
            DrawShape(LINE, new Point(200, 100), new Point(300, 200));

            // Draw the door of the house
            _robot.FindElementByName("Open").Click();
            _robot.FindElementByName(RECTANGLE_CHINESE).Click();
            _robot.ClickByElementName(NEW_CHINESE);
            _robot.FindElementByName(Constant.TOP_LEFT_X).SendKeys("180");
            _robot.FindElementByName(Constant.TOP_LEFT_Y).SendKeys("300");
            _robot.FindElementByName(Constant.BOTTOM_RIGHT_X).SendKeys("220");
            _robot.FindElementByName(Constant.BOTTOM_RIGHT_Y).SendKeys("400");
            _robot.FindElementByName("OK").Click();

            // Draw the windows of the house
            DrawShape(LINE, new Point(120, 250), new Point(160, 290));
            DrawShape(LINE, new Point(120, 250), new Point(160, 250));
            DrawShape(LINE, new Point(120, 290), new Point(160, 290));
            DrawShape(LINE, new Point(140, 250), new Point(140, 290));

            // Draw the doorknob
            DrawShape(CIRCLE, new Point(210, 350), new Point(220, 360));
            actionBuilder2
                .AddAction(CreateMoveTo(pointer, 210, 350))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(CreateMoveTo(pointer, 182, 350))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left));
            _robot.PerformAction(actionBuilder2.ToActionSequenceList());
        }

        public void DrawCat()
        {
            DrawShape(CIRCLE, new Point(100, 100), new Point(400, 400));
        }
    }
}
