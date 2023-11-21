using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;
using PowerPoint.State;

namespace PowerPoint.Tests
{
    [TestClass]
    public class ModelTests
    {
        private ShapeFactory _shapeFactory;
        private Model _model;
        PrivateObject _privateModel;
        private Mock<Context> _mockContext;

        [TestInitialize]
        public void Setup()
        {
            _shapeFactory = new ShapeFactory();
            _model = new Model(_shapeFactory);
            _mockContext = new Mock<Context>(_model);
            _privateModel = new PrivateObject(_model);
            _model.SetContext(_mockContext.Object);
        }

        [TestMethod]
        public void InsertShapeTest()
        {
            _model.InsertShape(ShapeType.LINE);
            Assert.AreEqual(1, _model.GetShapes().Count);
        }

        [TestMethod]
        public void RemoveShapeTest()
        {
            _model.InsertShape(ShapeType.LINE);
            _privateModel.SetField("_selectIndex", 0);
            _model.RemoveShape();

            Assert.AreEqual(0, _model.GetShapes().Count);
        }

        [TestMethod]
        public void MouseDownTest()
        {
            var point = new Point(1, 1);

            _model.MouseDown(point, ShapeType.LINE);

            _mockContext.Verify(m => m.MouseDown(point, ShapeType.LINE), Times.Once);
        }

        [TestMethod]
        public void MouseMoveTest()
        {
            var point = new Point(1, 1);

            _model.MouseMove(point);

            _mockContext.Verify(m => m.MouseMove(point), Times.Once);
        }

        [TestMethod]
        public void MouseUpTest()
        {
            var point = new Point(1, 1);

            _model.MouseUp(point, ShapeType.LINE);

            _mockContext.Verify(m => m.MouseUp(point, ShapeType.LINE), Times.Once);
        }

        [TestMethod]
        public void ClearTest()
        {
            _model.InsertShape(ShapeType.LINE);
            _model.Clear();

            Assert.AreEqual(0, _model.GetShapes().Count);
        }

        [TestMethod]
        public void SetModelStateTest()
        {
            _model.SetModelState(Model.ModelState.Drawing);

            _mockContext.Verify(m => m.SetState(It.IsAny<DrawingState>()), Times.Once);
        }

        [TestMethod]
        public void PressedPointerTest()
        {
            var _point = new Point(1, 1);
            _model.PressedPointer(_point, ShapeType.LINE);
            Shape _hint = _privateModel.GetField("_hint") as Shape;
            Assert.AreEqual(_point, _hint.GetPoint1());

        }

        [TestMethod]
        public void IsInShapeTest()
        {
            var point = new Point(1, 1);
            _model.InsertShape(ShapeType.LINE);
            _model.GetShapes()[0].SetPoint1(point);
            _model.GetShapes()[0].SetPoint2(new Point(2, 2));

            Assert.IsTrue(_model.IsInShape(point, 0));
        }

        [TestMethod]
        public void IsInShapeCornerTest()
        {
            var point = new Point(1, 1);
            _model.InsertShape(ShapeType.LINE);
            _model.GetShapes()[0].SetPoint1(point);
            _model.GetShapes()[0].SetPoint2(new Point(2, 2));
            _model.SelectShape(point);

            Assert.IsTrue(_model.IsInShapeCorner(point));
        }

        [TestMethod]
        public void SelectShapeTest()
        {
            var point = new Point(1, 1);
            _model.InsertShape(ShapeType.LINE);
            _model.GetShapes()[0].SetPoint1(point);
            _model.GetShapes()[0].SetPoint2(new Point(2, 2));

            _model.SelectShape(point);

            Assert.AreEqual(0, _model.GetSelectIndex());
        }

        [TestMethod]
        public void MoveShapeTest()
        {
            var point = new Point(1, 1);
            _model.InsertShape(ShapeType.LINE);
            _model.GetShapes()[0].SetPoint1(point);
            _model.GetShapes()[0].SetPoint2(new Point(2, 2));
            _model.SelectShape(point);

            var newPoint = new Point(2, 2);
            _model.MoveShape(newPoint);

            Assert.AreEqual(newPoint, _model.GetShapes()[0].GetPoint1());
        }

        [TestMethod]
        public void ReleasedPointerTest()
        {
            var point1 = new Point(1, 1);
            var point2 = new Point(2, 2);
            int initialCount = _model.GetShapes().Count;

            _model.PressedPointer(point1, ShapeType.LINE);
            _model.ReleasedPointer(point2, ShapeType.LINE);

            Assert.AreEqual(initialCount + 1, _model.GetShapes().Count);
        }

        [TestMethod]
        public void ResizeShapeTest()
        {
            var point1 = new Point(1, 1);
            var point2 = new Point(2, 2);
            _model.InsertShape(ShapeType.LINE);
            _model.GetShapes()[0].SetPoint1(point1);
            _model.GetShapes()[0].SetPoint2(point2);
            _model.SelectShape(point1);

            var newPoint2 = new Point(3, 3);
            _model.ResizeShape(newPoint2);

            Assert.AreEqual(newPoint2, _model.GetShapes()[0].GetPoint2());
        }

        [TestMethod]
        public void GetSelectIndexTest()
        {
            var point = new Point(1, 1);
            _model.InsertShape(ShapeType.LINE);
            _model.GetShapes()[0].SetPoint1(point);
            _model.GetShapes()[0].SetPoint2(new Point(2, 2));
            _model.SelectShape(point);

            Assert.AreEqual(0, _model.GetSelectIndex());
        }
    }
}