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
        
        // test
        [TestInitialize]
        public void Initialize()
        {
            _model = new Model();
            _privateModel = new PrivateObject(_model);
        }

        // test
        [TestMethod]

        public void ResizeShape()
        {
            var point = new Point(Constant.THREE_THOUSAND, 0);
            _model.InsertShape(ShapeType.LINE);
            _model.InsertShape(ShapeType.LINE);
            _privateModel.SetField(Constant.SELECT_INDEX, -1);
            var shapes = _model.GetShapes();
            _privateModel.SetField(Constant.SELECT, shapes[0]);

            _model.ResizeShape(point, Model.Location.RightBottom);
        }

        // test
        [TestMethod]

        public void ResizeShapeRightBottomResizesShapeWhenSelectIndexIsNotNegative()
        {
            var point = new Point(Constant.THREE_THOUSAND, 0);
            _model.InsertShape(ShapeType.LINE);
            _model.InsertShape(ShapeType.LINE);
            _privateModel.SetField(Constant.SELECT_INDEX, 0);
            var shapes = _model.GetShapes();
            _privateModel.SetField(Constant.SELECT, shapes[0]);

            _model.ResizeShape(point, Model.Location.RightBottom);
            shapes = _model.GetShapes();
            Assert.AreEqual(point.X, shapes[0].GetPoint2().X);
            point = new Point(-1, -1);
            _model.ResizeShape(point, Model.Location.RightBottom);
        }

        // test
        [TestMethod]

        public void ResizeShapeLeftBottomResizesShapeWhenSelectIndexIsNotNegative()
        {
            var point = new Point(0, 0);
            _model.InsertShape(ShapeType.LINE);
            _model.InsertShape(ShapeType.LINE);
            _privateModel.SetField(Constant.SELECT_INDEX, 0);
            var shapes = _model.GetShapes();
            _privateModel.SetField(Constant.SELECT, shapes[0]);

            _model.ResizeShape(point, Model.Location.LeftBottom);
            shapes = _model.GetShapes();
            Assert.AreEqual(point.X, shapes[0].GetPoint1().X);
            point = new Point(Constant.THREE_THOUSAND, -1);
            _model.ResizeShape(point, Model.Location.LeftBottom);
        }

        // test
        [TestMethod]

        public void ResizeShapeLeftTopResizesShapeWhenSelectIndexIsNotNegative()
        {
            var point = new Point(0, 0);
            _model.InsertShape(ShapeType.LINE);
            _model.InsertShape(ShapeType.LINE);
            _privateModel.SetField(Constant.SELECT_INDEX, 0);
            var shapes = _model.GetShapes();
            _privateModel.SetField(Constant.SELECT, shapes[0]);

            _model.ResizeShape(point, Model.Location.LeftTop);
            shapes = _model.GetShapes();
            Assert.AreEqual(point.X, shapes[0].GetPoint1().X);
            point = new Point(Constant.THREE_THOUSAND, Constant.THREE_THOUSAND);
            _model.ResizeShape(point, Model.Location.LeftTop);
        }

        // test
        [TestMethod]

        public void ResizeShapeRightTopResizesShapeWhenSelectIndexIsNotNegative()
        {
            var point = new Point(Constant.THREE_THOUSAND, 0);
            _model.InsertShape(ShapeType.LINE);
            _model.InsertShape(ShapeType.LINE);
            _privateModel.SetField(Constant.SELECT_INDEX, 0);
            var shapes = _model.GetShapes();
            _privateModel.SetField(Constant.SELECT, shapes[0]);

            _model.ResizeShape(point, Model.Location.RightTop);
            shapes = _model.GetShapes();
            Assert.AreEqual(point.X, shapes[0].GetPoint2().X);
            point = new Point(Constant.NEGATIVE_THREE_THOUSAND, Constant.THREE_THOUSAND);
            _model.ResizeShape(point, Model.Location.RightTop);
        }

        // test
        [TestMethod]

        public void ResizeShapeLeftResizesShapeWhenSelectIndexIsNotNegative()
        {
            var point = new Point(0, 0);
            _model.InsertShape(ShapeType.LINE);
            _model.InsertShape(ShapeType.LINE);
            _privateModel.SetField(Constant.SELECT_INDEX, 0);
            var shapes = _model.GetShapes();
            _privateModel.SetField(Constant.SELECT, shapes[0]);

            _model.ResizeShape(point, Model.Location.Left);
            shapes = _model.GetShapes();
            Assert.AreEqual(point.X, shapes[0].GetPoint1().X);
            point = new Point(Constant.THREE_THOUSAND, -1);
            _model.ResizeShape(point, Model.Location.Left);
        }

        // test
        [TestMethod]

        public void ResizeShapeRightResizesShapeWhenSelectIndexIsNotNegative()
        {
            var point = new Point(Constant.THREE_THOUSAND, 0);
            _model.InsertShape(ShapeType.LINE);
            _model.InsertShape(ShapeType.LINE);
            _privateModel.SetField(Constant.SELECT_INDEX, 0);
            var shapes = _model.GetShapes();
            _privateModel.SetField(Constant.SELECT, shapes[0]);

            _model.ResizeShape(point, Model.Location.Right);
            shapes = _model.GetShapes();
            Assert.AreEqual(point.X, shapes[0].GetPoint2().X);
            point = new Point(Constant.NEGATIVE_THREE_THOUSAND, -1);
            _model.ResizeShape(point, Model.Location.Right);
        }

        // test
        [TestMethod]

        public void ResizeShapeTopResizesShapeWhenSelectIndexIsNotNegative()
        {
            var point = new Point(Constant.TEN, 0);
            _model.InsertShape(ShapeType.LINE);
            _model.InsertShape(ShapeType.LINE);
            _privateModel.SetField(Constant.SELECT_INDEX, 0);
            var shapes = _model.GetShapes();
            _privateModel.SetField(Constant.SELECT, shapes[0]);

            _model.ResizeShape(point, Model.Location.Top);
            shapes = _model.GetShapes();
            Assert.AreEqual(point.Y, shapes[0].GetPoint1().Y);
            point = new Point(-1, Constant.THREE_THOUSAND);
            _model.ResizeShape(point, Model.Location.Top);
        }

        // test
        [TestMethod]

        public void ResizeShapeBottomResizesShapeWhenSelectIndexIsNotNegative()
        {
            var point = new Point(Constant.TEN, Constant.THREE_THOUSAND);
            _model.InsertShape(ShapeType.LINE);
            _model.InsertShape(ShapeType.LINE);
            _privateModel.SetField(Constant.SELECT_INDEX, 0);
            var shapes = _model.GetShapes();
            _privateModel.SetField(Constant.SELECT, shapes[0]);

            _model.ResizeShape(point, Model.Location.Bottom);
            shapes = _model.GetShapes();
            Assert.AreEqual(point.Y, shapes[0].GetPoint2().Y);
            point = new Point(-1, Constant.NEGATIVE_THREE_THOUSAND);
            _model.ResizeShape(point, Model.Location.Bottom);
        }
    }
}