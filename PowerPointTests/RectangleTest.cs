using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;
using PowerPoint;

namespace PowerPoint.Tests
{
    [TestClass]
    public class RectangleTests
    {
        private Mock<IGraphics> _mockGraphics;
        private Rectangle _rectangle;

        // test
        [TestInitialize]
        public void Setup()
        {
            _mockGraphics = new Mock<IGraphics>();
            _rectangle = new Rectangle();
            _rectangle = new Rectangle(new PointF(1, 1), new PointF(2, 2));
        }

        // test
        [TestMethod]
        public void DrawTest()
        {
            _rectangle.Draw(_mockGraphics.Object);

            _mockGraphics.Verify(g => g.DrawRectangle(It.IsAny<Pen>(), It.IsAny<PointF>(), It.IsAny<PointF>()), Times.Once);
        }

        // test
        [TestMethod]
        public void RectangleConstructorTest_WhenPoint1IsGreaterThanPoint2()
        {
            var rectangle = new Rectangle(new PointF(2, 2), new PointF(1, 1));

            Assert.AreEqual(ShapeType.RECTANGLE, rectangle.GetShapeType());
            Assert.AreEqual(new PointF(1, 1), rectangle.GetPoint1());
            Assert.AreEqual(new PointF(2, 2), rectangle.GetPoint2());
        }

        // test
        [TestMethod]
        public void RectangleConstructorTest_WhenPoint1IsLessThanPoint2()
        {
            var rectangle = new Rectangle(new PointF(1, 1), new PointF(2, 2));

            Assert.AreEqual(ShapeType.RECTANGLE, rectangle.GetShapeType());
            Assert.AreEqual(new PointF(1, 1), rectangle.GetPoint1());
            Assert.AreEqual(new PointF(2, 2), rectangle.GetPoint2());
        }
    }
}