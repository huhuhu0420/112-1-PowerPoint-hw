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
            _shape.SetPoint1(new PointF(0, 0));
            _shape.SetPoint2(new PointF(10, 10));

            var result = _shape.IsInShape(new PointF(5, 5));

            Assert.IsTrue(result);
        }

        // test
        [TestMethod]
        public void IsInShape_WithPointOutside_ReturnsFalse()
        {
            _shape.SetPoint1(new PointF(0, 0));
            _shape.SetPoint2(new PointF(10, 10));

            var result = _shape.IsInShape(new PointF(15, 15));

            Assert.IsFalse(result);
        }

        // test
        [TestMethod]
        public void IsInCorner_WithPointInCorner_ReturnsTrue()
        {
            _shape.SetPoint1(new PointF(0, 0));
            _shape.SetPoint2(new PointF(10, 10));

            var result = _shape.IsInShape(new PointF(10, 10));

            Assert.IsTrue(result);
        }

        // test
        [TestMethod]
        public void IsInCorner_WithPointOutsideCorner_ReturnsFalse()
        {
            _shape.SetPoint1(new PointF(0, 0));
            _shape.SetPoint2(new PointF(10, 10));

            var result = _shape.IsInShape(new PointF(15, 15));

            Assert.IsFalse(result);
        }
        
        // test
        [TestMethod]
        public void IsInShapeReturnsTrueWhenPointIsInShape()
        {
            _shape.SetPoint1(new PointF(0, 0));
            _shape.SetPoint2(new PointF(10, 10));

            bool result = _shape.IsInShape(new PointF(5, 5));

            Assert.IsTrue(result);
        }

        // test
        [TestMethod]
        public void IsInShapeReturnsFalseWhenPointIsOutsideShape()
        {
            _shape.SetPoint1(new PointF(0, 0));
            _shape.SetPoint2(new PointF(10, 10));

            bool result = _shape.IsInShape(new PointF(15, 15));

            Assert.IsFalse(result);
        }

        // test
        [TestMethod]
        public void IsInShapeCornerReturnsLeftTop()
        {
            _shape.SetPoint1(new PointF(0, 0));
            _shape.SetPoint2(new PointF(10, 10));

            Model.Location result = _shape.IsInShapeCorner(new PointF(0, 0));

            Assert.AreEqual(Model.Location.LeftTop, result);
        }
        
        // test
        [TestMethod]
        public void IsInShapeCornerReturnsRightTop()
        {
            _shape.SetPoint1(new PointF(0, 0));
            _shape.SetPoint2(new PointF(10, 10));

            Model.Location result = _shape.IsInShapeCorner(new PointF(10, 0));

            Assert.AreEqual(Model.Location.RightTop, result);
        }
        
        // test
        [TestMethod]
        public void IsInShapeCornerReturnsLeftBottom()
        {
            _shape.SetPoint1(new PointF(0, 0));
            _shape.SetPoint2(new PointF(10, 10));

            Model.Location result = _shape.IsInShapeCorner(new PointF(0, 10));

            Assert.AreEqual(Model.Location.LeftBottom, result);
        }
        
        // test
        [TestMethod]
        public void IsInShapeCornerReturnsRightBottom()
        {
            _shape.SetPoint1(new PointF(0, 0));
            _shape.SetPoint2(new PointF(10, 10));

            Model.Location result = _shape.IsInShapeCorner(new PointF(10, 10));

            Assert.AreEqual(Model.Location.RightBottom, result);
        }
        
        // test
        [TestMethod]
        public void IsInShapeCornerReturnsLeft()
        {
            _shape.SetPoint1(new PointF(0, 0));
            _shape.SetPoint2(new PointF(10, 10));

            Model.Location result = _shape.IsInShapeCorner(new PointF(0, 5));

            Assert.AreEqual(Model.Location.Left, result);
        }
        
        // test
        [TestMethod]
        public void IsInShapeCornerReturnsTop()
        {
            _shape.SetPoint1(new PointF(0, 0));
            _shape.SetPoint2(new PointF(10, 10));

            Model.Location result = _shape.IsInShapeCorner(new PointF(5, 0));

            Assert.AreEqual(Model.Location.Top, result);
        }
        
        // test
        [TestMethod]
        public void IsInShapeCornerReturnsRight()
        {
            _shape.SetPoint1(new PointF(0, 0));
            _shape.SetPoint2(new PointF(10, 10));

            Model.Location result = _shape.IsInShapeCorner(new PointF(10, 5));

            Assert.AreEqual(Model.Location.Right, result);
        }
        
        // test
        [TestMethod]
        public void IsInShapeCornerReturnsBottom()
        {
            _shape.SetPoint1(new PointF(0, 0));
            _shape.SetPoint2(new PointF(10, 10));

            Model.Location result = _shape.IsInShapeCorner(new PointF(5, 10));

            Assert.AreEqual(Model.Location.Bottom, result);
        }

        // test
        [TestMethod]
        public void IsInShapeCornerReturnsNoneWhenPointIsNotInCorner()
        {
            _shape.SetPoint1(new PointF(0, 0));
            _shape.SetPoint2(new PointF(10, 10));

            Model.Location result = _shape.IsInShapeCorner(new PointF(5, 5));

            Assert.AreEqual(Model.Location.None, result);
        }

        // test
        [TestMethod]
        public void GetLineTypeReturnsCorrectTypeBasedOnPoints()
        {
            _shape.SetPoint1(new PointF(100, 100));
            _shape.SetPoint2(new PointF(10, 10));

            Line.LineType result = _shape.GetLineType();

            Assert.AreEqual(Line.LineType.RightBottom, result);
            Line line = new Line();
            line.SetLineType(Line.LineType.None);
            line.SetPoint1(new PointF(-1, -1));
            line.SetPoint2(new PointF(-1, -1));
            Assert.AreEqual(Line.LineType.None, line.GetLineType2(0, 0, 0, 0));
            Assert.AreEqual(Line.LineType.LeftBottom, line.GetLineType2(-1, 0, 0, -1));
            Assert.AreEqual(Line.LineType.LeftTop, line.GetLineType2(-1, -1, 0, 0));
            Assert.AreEqual(Line.LineType.RightTop, line.GetLineType2(0, -1, -1, 0));
        }
    }
}