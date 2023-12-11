using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;
using PowerPoint;

namespace PowerPoint.Tests
{
    [TestClass]
    public class CircleTests
    {
        private Mock<IGraphics> _mockGraphics;
        private Circle _circle;

        // test
        [TestInitialize]
        public void Setup()
        {
            _mockGraphics = new Mock<IGraphics>();
            _circle = new Circle();
            _circle = new Circle(new PointF(1, 1), new PointF(2, 2));
        }

        // test
        [TestMethod]
        public void DrawTest()
        {
            _circle.Draw(_mockGraphics.Object);

            _mockGraphics.Verify(g => g.DrawCircle(It.IsAny<Pen>(), It.IsAny<PointF>(), It.IsAny<PointF>()), Times.Once);
        }

        // test
        [TestMethod]
        public void CircleConstructorTest_WhenPoint1IsGreaterThanPoint2()
        {
            var circle = new Circle(new PointF(2, 2), new PointF(1, 1));

            Assert.AreEqual(ShapeType.CIRCLE, circle.GetShapeType());
            Assert.AreEqual(new PointF(1, 1), circle.GetPoint1());
            Assert.AreEqual(new PointF(2, 2), circle.GetPoint2());
        }

        // test
        [TestMethod]
        public void CircleConstructorTest_WhenPoint1IsLessThanPoint2()
        {
            var circle = new Circle(new PointF(1, 1), new PointF(2, 2));

            Assert.AreEqual(ShapeType.CIRCLE, circle.GetShapeType());
            Assert.AreEqual(new PointF(1, 1), circle.GetPoint1());
            Assert.AreEqual(new PointF(2, 2), circle.GetPoint2());
        }
    }
}