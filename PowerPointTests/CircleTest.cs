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

        [TestInitialize]
        public void Setup()
        {
            _mockGraphics = new Mock<IGraphics>();
            _circle = new Circle();
            _circle = new Circle(new Point(1, 1), new Point(2, 2));
        }

        [TestMethod]
        public void DrawTest()
        {
            _circle.Draw(_mockGraphics.Object);

            _mockGraphics.Verify(g => g.DrawCircle(It.IsAny<Pen>(), It.IsAny<Point>(), It.IsAny<Point>()), Times.Once);
        }

        [TestMethod]
        public void CircleConstructorTest_WhenPoint1IsGreaterThanPoint2()
        {
            var circle = new Circle(new Point(2, 2), new Point(1, 1));

            Assert.AreEqual(ShapeType.CIRCLE, circle.Type);
            Assert.AreEqual(new Point(1, 1), circle.GetPoint1());
            Assert.AreEqual(new Point(2, 2), circle.GetPoint2());
        }

        [TestMethod]
        public void CircleConstructorTest_WhenPoint1IsLessThanPoint2()
        {
            var circle = new Circle(new Point(1, 1), new Point(2, 2));

            Assert.AreEqual(ShapeType.CIRCLE, circle.Type);
            Assert.AreEqual(new Point(1, 1), circle.GetPoint1());
            Assert.AreEqual(new Point(2, 2), circle.GetPoint2());
        }
    }
}