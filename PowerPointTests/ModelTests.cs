using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;
using PowerPoint.State;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;

namespace PowerPoint.Tests
{
    [TestClass]
    public class ModelTests
    {
        private Model _model;
        private PrivateObject _privateModel;
        private Mock<IGraphics> _mockGraphics;
        private Mock<Context> _mockContext;

        [TestInitialize]
        public void Setup()
        {
            _model = new Model();
            _privateModel = new PrivateObject(_model);
            _mockGraphics = new Mock<IGraphics>();
            _mockContext = new Mock<Context>(_model);
        }

        [TestMethod]
        public void InitializeResizeShapeTest()
        {
            _privateModel.Invoke("InitializeResizeShape");

            var resizeShape = (Dictionary<Model.Location, Action<Point>>)_privateModel.GetField("_resizeShape");

            Assert.IsNotNull(resizeShape[Model.Location.Left]);
            Assert.IsNotNull(resizeShape[Model.Location.Right]);
            Assert.IsNotNull(resizeShape[Model.Location.Top]);
            Assert.IsNotNull(resizeShape[Model.Location.Bottom]);
            Assert.IsNotNull(resizeShape[Model.Location.LeftTop]);
            Assert.IsNotNull(resizeShape[Model.Location.LeftBottom]);
            Assert.IsNotNull(resizeShape[Model.Location.RightTop]);
            Assert.IsNotNull(resizeShape[Model.Location.RightBottom]);
        }

        [TestMethod]
        public void HandleStateChangedTest()
        {
            var mockState = new Mock<IState>();
            bool wasCalled = false;
            _model._stateChanged += (IState) =>
            {
                wasCalled = true;
            };

            _privateModel.Invoke("HandleStateChanged", mockState.Object);

            Assert.IsTrue(wasCalled);
        }

        // test
        [TestMethod]
        public void MouseDownTest()
        {
            var point = new Point(1, 1);

            _model.MouseDown(point, ShapeType.LINE);

            _mockContext.Verify(m => m.MouseDown(point, ShapeType.LINE), Times.Never);
        }

        // test
        [TestMethod]
        public void MouseMoveTest()
        {
            var point = new Point(1, 1);

            _model.MouseMove(point);

            _mockContext.Verify(m => m.MouseMove(point), Times.Never);
        }

        // test
        [TestMethod]
        public void MouseUpTest()
        {
            var point = new Point(1, 1);

            _model.MouseUp(point, ShapeType.LINE);

            _mockContext.Verify(m => m.MouseUp(point, ShapeType.LINE), Times.Never);
        }

        // test
        [TestMethod]
        public void DrawTest()
        {
            _model.Draw(_mockGraphics.Object);
            _mockGraphics.Verify(graphics => graphics.DrawLine( It.IsAny<Pen>(), It.IsAny<Point>(), It.IsAny<Point>()), Times.Never);
        }

        [TestMethod]
        public void PressedPointerTest()
        {
            var point = new Point(10, 10);

            _privateModel.Invoke("PressedPointer", point, ShapeType.LINE);

            Assert.IsNotNull(_privateModel.GetField("_hint"));
            Assert.AreEqual(point, _privateModel.GetField("_firstPoint"));
        }

        // test
        [TestMethod]
        public void InsertShapeTest()
        {
            _model.InsertShape(ShapeType.LINE);
            Assert.AreEqual(1, _model.GetShapes().Count);
        }
        
        [TestMethod]
        public void IsInShapeCornerTest()
        {
            var point = new Point(20, 20);
            _model.InsertShape(ShapeType.LINE);
            _model.GetShapes()[0].SetPoint1(point);
            _model.GetShapes()[0].SetPoint2(new Point(20, 20));

            Model.Location result = _model.IsInShapeCorner(point);
            Assert.AreEqual(Model.Location.None, result);
            _privateModel.SetField("_selectIndex", 0);
            result = _model.IsInShapeCorner(point);
            Assert.AreEqual(Model.Location.LeftTop, result);
        }

        // test
        [TestMethod]
        public void SelectShapeTest()
        {
            var point = new Point(1, 1);
            var point2 = new Point(100, 100);
            _model.InsertShape(ShapeType.LINE);
            _model.GetShapes()[0].SetPoint1(point);
            _model.GetShapes()[0].SetPoint2(new Point(2, 2));

            _model.SelectShape(point2);
            _model.SelectShape(point);
            Assert.AreEqual(0, _model.GetSelectIndex());
        }

        [TestMethod]
        public void MovedPointerTest()
        {
            var point = new Point(10, 10);
            _privateModel.SetField("_hint", new Shape());

            _privateModel.Invoke("MovedPointer", point);

            Assert.AreEqual(point, ((Shape)_privateModel.GetField("_hint")).GetPoint2());
        }

        // test
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
            _privateModel.SetField("_selectIndex", -1);
            _model.MoveShape(newPoint);
        }

        // test
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
        public void ClearTest()
        {
            _model.InsertShape(ShapeType.LINE);
            _model.Clear();

            Assert.AreEqual(0, _model.GetShapes().Count);
        }

        // test
        [TestMethod]
        public void DrawShapesTest()
        {
            _model.InsertShape(ShapeType.LINE);
            System.ComponentModel.BindingList<Shape> _shapes = _model.GetShapes();
            Point _point = _shapes[0].GetCenterPoint();
            _model.SelectShape(_point);
            _model.DrawShapes(_mockGraphics.Object);
            _mockGraphics.Verify(graphics => graphics.DrawLine( It.IsAny<Pen>(), It.IsAny<Point>(), It.IsAny<Point>()), Times.Once);
        }

        [TestMethod]
        public void DrawSelectTest()
        {
            _privateModel.SetField("_select", new Shape());

            _privateModel.Invoke("DrawSelect", _mockGraphics.Object);

            _mockGraphics.Verify(g => g.DrawSelect(It.IsAny<Pen>(), It.IsAny<Point>(), It.IsAny<Point>()), Times.Once);
        }

        [TestMethod]
        public void DrawHintTest()
        {
            _privateModel.SetField("_hint", new Shape());

            _privateModel.Invoke("DrawHint", _mockGraphics.Object);

            _mockGraphics.Verify(g => g.DrawRectangle(It.IsAny<Pen>(), It.IsAny<Point>(), It.IsAny<Point>()), Times.Never);
        }

        [TestMethod]
        public void NotifyModelChangedTest()
        {
            bool wasCalled = false;
            _model._modelChanged += () => wasCalled = true;

            _privateModel.Invoke("NotifyModelChanged");

            Assert.IsTrue(wasCalled);
        }

        [TestMethod]
        public void GetSelectIndexTest()
        {
            _privateModel.SetField("_selectIndex", 1);

            int result = (int)_privateModel.Invoke("GetSelectIndex");

            Assert.AreEqual(1, result);
        }

        // test
        [TestMethod]
        public void SetModelStateTest()
        {
            _model.SetModelState(Model.ModelState.Drawing);
            _model.SetModelState(Model.ModelState.Normal);

            _mockContext.Verify(m => m.SetState(It.IsAny<DrawingState>()), Times.Never);
        }
    }
}