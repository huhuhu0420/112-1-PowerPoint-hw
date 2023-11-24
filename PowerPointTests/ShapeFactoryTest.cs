using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace PowerPoint.Tests
{
    [TestClass]
    public class ShapeFactoryTests
    {
        private ShapeFactory _shapeFactory;

        // test
        [TestInitialize]
        public void SetUp()
        {
            _shapeFactory = new ShapeFactory();
        }

        // test
        [TestMethod]
        public void CreateShapeWithShapeTypeRectangleTest()
        {
            var shape = _shapeFactory.CreateShape(ShapeType.RECTANGLE);

            Assert.IsInstanceOfType(shape, typeof(Rectangle));
        }

        // test
        [TestMethod]
        public void CreateShapeWithShapeTypeCircleTest()
        {
            var shape = _shapeFactory.CreateShape(ShapeType.CIRCLE);

            Assert.IsInstanceOfType(shape, typeof(Circle));
        }

        // test
        [TestMethod]
        public void CreateShapeWithUnknownShapeTypeTest()
        {
            var shape = _shapeFactory.CreateShape((ShapeType)999);

            Assert.IsInstanceOfType(shape, typeof(Line));
        }

        // test
        [TestMethod]
        public void CreateShapeWithPointsAndShapeTypeRectangleTest()
        {
            var point1 = new Point(10, 20);
            var point2 = new Point(30, 40);

            var shape = _shapeFactory.CreateShape(ShapeType.RECTANGLE, point1, point2);

            Assert.IsInstanceOfType(shape, typeof(Rectangle));
            Assert.AreEqual(point1, shape.GetPoint1());
            Assert.AreEqual(point2, shape.GetPoint2());
        }

        // test
        [TestMethod]
        public void CreateShapeWithPointsAndShapeTypeCircleTest()
        {
            var point1 = new Point(10, 20);
            var point2 = new Point(30, 40);

            var shape = _shapeFactory.CreateShape(ShapeType.CIRCLE, point1, point2);

            Assert.IsInstanceOfType(shape, typeof(Circle));
            Assert.AreEqual(point1, shape.GetPoint1());
            Assert.AreEqual(point2, shape.GetPoint2());
        }

        // test
        [TestMethod]
        public void CreateShapeWithPointsAndUnknownShapeTypeTest()
        {
            var point1 = new Point(10, 20);
            var point2 = new Point(30, 40);

            var shape = _shapeFactory.CreateShape((ShapeType)999, point1, point2);

            Assert.IsInstanceOfType(shape, typeof(Line));
            Assert.AreEqual(point1, shape.GetPoint1());
            Assert.AreEqual(point2, shape.GetPoint2());
        }
    }
}