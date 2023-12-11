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
        PrivateObject _privateLine;

        // test
        [TestInitialize]
        public void Initialize()
        {
            _mockGraphics = new Mock<IGraphics>();
            _line = new Line();
            _line = new Line(new PointF(1, 1), new PointF(2, 2));
            _privateLine = new PrivateObject(_line);
        }

        // test
        [TestMethod]
        public void DrawTest()
        {
            _line.SetLineType(Line.LineType.None);
            _line.Draw(_mockGraphics.Object);
            _mockGraphics.Verify(g => g.DrawLine(It.IsAny<Pen>(), It.IsAny<PointF>(), It.IsAny<PointF>()), Times.Once);
            _line.SetLineType(Line.LineType.LeftTop);
            _line.Draw(_mockGraphics.Object);
            _mockGraphics.Verify(g => g.DrawLine(It.IsAny<Pen>(), It.IsAny<PointF>(), It.IsAny<PointF>(), Line.LineType.LeftTop), Times.Once);
        }

        // test
        [TestMethod]
        public void LineConstructorTest_WhenPoint1IsGreaterThanPoint2()
        {
            var line = new Line(new PointF(2, 2), new PointF(1, 1));

            Assert.AreEqual(ShapeType.LINE, line.GetType());
            Assert.AreEqual(new PointF(1, 1), line.GetPoint1());
            Assert.AreEqual(new PointF(2, 2), line.GetPoint2());
        }

        // test
        [TestMethod]
        public void LineConstructorTest_WhenPoint1IsLessThanPoint2()
        {
            var line = new Line(new PointF(1, 1), new PointF(2, 2));

            Assert.AreEqual(ShapeType.LINE, line.GetType());
            Assert.AreEqual(new PointF(1, 1), line.GetPoint1());
            Assert.AreEqual(new PointF(2, 2), line.GetPoint2());
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

            Assert.AreEqual(Line.LineType.RightTop, _privateLine.GetField("_lineType"));
        }
    }
}