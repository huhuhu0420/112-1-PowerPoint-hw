using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;
using PowerPoint;

namespace PowerPoint.Tests
{
    [TestClass]
    public class LineTests
    {
        private Mock<IGraphics> _mockGraphics;
        private Line _line;

        // test
        [TestInitialize]
        public void Setup()
        {
            _mockGraphics = new Mock<IGraphics>();
            _line = new Line();
            _line = new Line(new Point(1, 1), new Point(2, 2));
        }

        // test
        [TestMethod]
        public void DrawTest()
        {
            _line.Draw(_mockGraphics.Object);

            _mockGraphics.Verify(g => g.DrawLine(It.IsAny<Pen>(), It.IsAny<Point>(), It.IsAny<Point>()), Times.Once);
        }

        // test
        [TestMethod]
        public void LineConstructorTest_WhenPoint1IsGreaterThanPoint2()
        {
            var line = new Line(new Point(2, 2), new Point(1, 1));

            Assert.AreEqual(ShapeType.LINE, line.Type);
            Assert.AreEqual(new Point(2, 2), line.GetPoint1());
            Assert.AreEqual(new Point(1, 1), line.GetPoint2());
        }

        // test
        [TestMethod]
        public void LineConstructorTest_WhenPoint1IsLessThanPoint2()
        {
            var line = new Line(new Point(1, 1), new Point(2, 2));

            Assert.AreEqual(ShapeType.LINE, line.Type);
            Assert.AreEqual(new Point(1, 1), line.GetPoint1());
            Assert.AreEqual(new Point(2, 2), line.GetPoint2());
        }

        // test
        [TestMethod]
        public void SetLineTypeTest()
        {
            _line.SetLineType(Line.LineType.LeftTop);

            Assert.AreEqual(Line.LineType.LeftTop, _line.GetLineType());
        }

        // test
        [TestMethod]
        public void GetLineTypeTest()
        {
            _line.SetLineType(Line.LineType.RightTop);

            Assert.AreEqual(Line.LineType.RightTop, _line.GetLineType());
        }
    }
}