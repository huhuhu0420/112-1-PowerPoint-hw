using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;
using PowerPoint.State;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;

namespace PowerPoint.Tests
{
    [TestClass]
    public class ModelTests1
    {
        private Model _model;
        private PrivateObject _privateModel;
        private Mock<IGraphics> _mockGraphics;
        
        [TestInitialize]
        public void Setup()
        {
            _model = new Model();
            _privateModel = new PrivateObject(_model);
            _mockGraphics = new Mock<IGraphics>();
        }

        [TestMethod]
        public void ResizeShape()
        {
            var point = new Point(3000, 0);
            _model.InsertShape(ShapeType.LINE);
            _model.InsertShape(ShapeType.LINE);
            _privateModel.SetField("_selectIndex", -1);
            var shapes = _model.GetShapes();
            _privateModel.SetField("_select", shapes[0]);

            _model.ResizeShape(point, Model.Location.RightBottom);
        }

        [TestMethod]
        public void ResizeShapeRightBottomResizesShapeWhenSelectIndexIsNotNegative()
        {
            var point = new Point(3000, 0);
            _model.InsertShape(ShapeType.LINE);
            _model.InsertShape(ShapeType.LINE);
            _privateModel.SetField("_selectIndex", 0);
            var shapes = _model.GetShapes();
            _privateModel.SetField("_select", shapes[0]);

            _model.ResizeShape(point, Model.Location.RightBottom);
            shapes = _model.GetShapes();
            Assert.AreEqual(point.X, shapes[0].GetPoint2().X);
            point = new Point(-1, -1);
            _model.ResizeShape(point, Model.Location.RightBottom);
        }

        [TestMethod]
        public void ResizeShapeLeftBottomResizesShapeWhenSelectIndexIsNotNegative()
        {
            var point = new Point(0, 0);
            _model.InsertShape(ShapeType.LINE);
            _model.InsertShape(ShapeType.LINE);
            _privateModel.SetField("_selectIndex", 0);
            var shapes = _model.GetShapes();
            _privateModel.SetField("_select", shapes[0]);

            _model.ResizeShape(point, Model.Location.LeftBottom);
            shapes = _model.GetShapes();
            Assert.AreEqual(point.X, shapes[0].GetPoint1().X);
            point = new Point(3000, -1);
            _model.ResizeShape(point, Model.Location.LeftBottom);
        }

        [TestMethod]
        public void ResizeShapeLeftTopResizesShapeWhenSelectIndexIsNotNegative()
        {
            var point = new Point(0, 0);
            _model.InsertShape(ShapeType.LINE);
            _model.InsertShape(ShapeType.LINE);
            _privateModel.SetField("_selectIndex", 0);
            var shapes = _model.GetShapes();
            _privateModel.SetField("_select", shapes[0]);

            _model.ResizeShape(point, Model.Location.LeftTop);
            shapes = _model.GetShapes();
            Assert.AreEqual(point.X, shapes[0].GetPoint1().X);
            point = new Point(3000, 3000);
            _model.ResizeShape(point, Model.Location.LeftTop);
        }

        [TestMethod]
        public void ResizeShapeRightTopResizesShapeWhenSelectIndexIsNotNegative()
        {
            var point = new Point(3000, 0);
            _model.InsertShape(ShapeType.LINE);
            _model.InsertShape(ShapeType.LINE);
            _privateModel.SetField("_selectIndex", 0);
            var shapes = _model.GetShapes();
            _privateModel.SetField("_select", shapes[0]);

            _model.ResizeShape(point, Model.Location.RightTop);
            shapes = _model.GetShapes();
            Assert.AreEqual(point.X, shapes[0].GetPoint2().X);
            point = new Point(-3000, 3000);
            _model.ResizeShape(point, Model.Location.RightTop);
        }

        [TestMethod]
        public void ResizeShapeLeftResizesShapeWhenSelectIndexIsNotNegative()
        {
            var point = new Point(0, 0);
            _model.InsertShape(ShapeType.LINE);
            _model.InsertShape(ShapeType.LINE);
            _privateModel.SetField("_selectIndex", 0);
            var shapes = _model.GetShapes();
            _privateModel.SetField("_select", shapes[0]);

            _model.ResizeShape(point, Model.Location.Left);
            shapes = _model.GetShapes();
            Assert.AreEqual(point.X, shapes[0].GetPoint1().X);
            point = new Point(3000, -1);
            _model.ResizeShape(point, Model.Location.Left);
        }

        [TestMethod]
        public void ResizeShapeRightResizesShapeWhenSelectIndexIsNotNegative()
        {
            var point = new Point(3000, 0);
            _model.InsertShape(ShapeType.LINE);
            _model.InsertShape(ShapeType.LINE);
            _privateModel.SetField("_selectIndex", 0);
            var shapes = _model.GetShapes();
            _privateModel.SetField("_select", shapes[0]);

            _model.ResizeShape(point, Model.Location.Right);
            shapes = _model.GetShapes();
            Assert.AreEqual(point.X, shapes[0].GetPoint2().X);
            point = new Point(-3000, -1);
            _model.ResizeShape(point, Model.Location.Right);
        }

        [TestMethod]
        public void ResizeShapeTopResizesShapeWhenSelectIndexIsNotNegative()
        {
            var point = new Point(10, 0);
            _model.InsertShape(ShapeType.LINE);
            _model.InsertShape(ShapeType.LINE);
            _privateModel.SetField("_selectIndex", 0);
            var shapes = _model.GetShapes();
            _privateModel.SetField("_select", shapes[0]);

            _model.ResizeShape(point, Model.Location.Top);
            shapes = _model.GetShapes();
            Assert.AreEqual(point.Y, shapes[0].GetPoint1().Y);
            point = new Point(-1, 3000);
            _model.ResizeShape(point, Model.Location.Top);
        }

        [TestMethod]
        public void ResizeShapeBottomResizesShapeWhenSelectIndexIsNotNegative()
        {
            var point = new Point(10, 3000);
            _model.InsertShape(ShapeType.LINE);
            _model.InsertShape(ShapeType.LINE);
            _privateModel.SetField("_selectIndex", 0);
            var shapes = _model.GetShapes();
            _privateModel.SetField("_select", shapes[0]);

            _model.ResizeShape(point, Model.Location.Bottom);
            shapes = _model.GetShapes();
            Assert.AreEqual(point.Y, shapes[0].GetPoint2().Y);
            point = new Point(-1, -3000);
            _model.ResizeShape(point, Model.Location.Bottom);
        }
    }
}