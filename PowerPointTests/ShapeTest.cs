using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace PowerPoint.Tests
{
    [TestClass]
    public class ShapeTests
    {
        private Shape _shape;

        // test
        [TestInitialize]
        public void SetUp()
        {
            _shape = new Shape();
        }

        // test
        [TestMethod]
        public void GetShapeName_ReturnsEmptyString()
        {
            var result = _shape.ShapeName;

            Assert.AreEqual(string.Empty, result);
        }

        // test
        [TestMethod]
        public void GetInfo_WithDefaultPoints_ReturnsCorrectFormat()
        {
            var result = _shape.Info;

            Assert.AreEqual("(0, 0), (0, 0)", result);
        }

        // test
        [TestMethod]
        public void IsInShape_WithPointInside_ReturnsTrue()
        {
            _shape.SetPoint1(new Point(0, 0));
            _shape.SetPoint2(new Point(10, 10));

            var result = _shape.IsInShape(new Point(5, 5));

            Assert.IsTrue(result);
        }

        // test
        [TestMethod]
        public void IsInShape_WithPointOutside_ReturnsFalse()
        {
            _shape.SetPoint1(new Point(0, 0));
            _shape.SetPoint2(new Point(10, 10));

            var result = _shape.IsInShape(new Point(15, 15));

            Assert.IsFalse(result);
        }

        // test
        [TestMethod]
        public void IsInCorner_WithPointInCorner_ReturnsTrue()
        {
            _shape.SetPoint1(new Point(0, 0));
            _shape.SetPoint2(new Point(10, 10));

            var result = _shape.IsInShape(new Point(10, 10));

            Assert.IsTrue(result);
        }

        // test
        [TestMethod]
        public void IsInCorner_WithPointOutsideCorner_ReturnsFalse()
        {
            _shape.SetPoint1(new Point(0, 0));
            _shape.SetPoint2(new Point(10, 10));

            var result = _shape.IsInShape(new Point(15, 15));

            Assert.IsFalse(result);
        }
    }
}