using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerPoint;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PowerPoint.State;

namespace PowerPoint.Tests
{
    [TestClass()]
    public class ModelTests
    {
        private Model _model;
        PrivateObject _privateModel; 
        private ShapeFactory _shapeFactory = new ShapeFactory();
        
        [TestInitialize()]
        public void Initialize()
        {
            _model = new Model();
            _privateModel = new PrivateObject(_model);
        }

        [TestMethod()]
        public void InsertShapeTest()
        {
            _model.InsertShape(0);
            Assert.AreEqual(1, _model.GetShapes().Count);
        }

        [TestMethod()]
        public void RemoveShapeTest()
        {
            _model.InsertShape(0);
            _privateModel.SetField("_selectIndex", 0);
            _model.RemoveShape();
            Assert.AreEqual(0, _model.GetShapes().Count);
        }

        [TestMethod()]
        public void RemoveShapeByIndexTest()
        {
            _model.InsertShape(0);
            _model.RemoveShapeByIndex(0);
            Assert.AreEqual(0, _model.GetShapes().Count);
        }

        [TestMethod()]
        public void GetShapesTest()
        {
            _model.InsertShape(0);
            _model.GetShapes();
            Assert.AreEqual(1, _model.GetShapes().Count);
        }

        [TestMethod()]
        public void MouseDownTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void MouseMoveTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void MouseUpTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DrawTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PressedPointerTest()
        {
            Point _point = new Point(3, 3);
            _model.PressedPointer(_point, 0);
            Shape _hint = _privateModel.GetField("_hint") as Shape;
            Assert.AreEqual(_point, _hint.GetPoint1());
        }

        [TestMethod()]
        public void IsInShapeTest()
        {
            _model.InsertShape(0);
            BindingList<Shape> _shapes = _model.GetShapes();
            Point _point = _shapes[0].GetCenterPoint();
            _model.IsInShape(_point, 0);
        }

        [TestMethod()]
        public void SelectShapeTest()
        {
            _model.InsertShape(0);
            BindingList<Shape> _shapes = _model.GetShapes();
            Point _point = _shapes[0].GetCenterPoint();
            _model.SelectShape(_point);
            Assert.AreEqual(0, _privateModel.GetField("_selectIndex"));
        }

        [TestMethod()]
        public void MovedPointerTest()
        {
            Point _point = new Point(3, 3);
            _model.PressedPointer(_point, 0);
            _model.MovedPointer(_point);
            Shape _hint = _privateModel.GetField("_hint") as Shape;
            Assert.AreEqual(_point, _hint.GetPoint2());
        }

        [TestMethod()]
        public void MoveShapeTest()
        {
            Point _point = new Point(3, 3);
            _model.PressedPointer(_point, 0);
            _model.MovedPointer(_point);
            _model.MoveShape(_point);
            Shape _hint = _privateModel.GetField("_hint") as Shape;
            Assert.AreEqual(_point, _hint.GetPoint2());
        }

        [TestMethod()]
        public void ReleasedPointerTest()
        {
            Point _point = new Point(3, 3);
            _model.PressedPointer(_point, 0);
            _model.MovedPointer(_point);
            _model.ReleasedPointer(_point, 0);
            Assert.AreEqual(1, _model.GetShapes().Count);
        }

        [TestMethod()]
        public void ClearTest()
        {
            _model.InsertShape(0);
            _model.Clear();
            Assert.AreEqual(0, _model.GetShapes().Count);
        }

        [TestMethod()]
        public void DrawShapesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DrawHintTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void NotifyModelChangedTest()
        {
            bool _isChanged = false;
            _model._modelChanged += () => { _isChanged = true; };
            _model.NotifyModelChanged();
            Assert.IsTrue(_isChanged);
        }
        
        [TestMethod()]
        public void GetSelectIndexTest()
        {
            _privateModel.SetField("_selectIndex", 0);
            Assert.AreEqual(0, _model.GetSelectIndex());
        }

        [TestMethod()]
        public void SetModelStateTest()
        {
            _model.SetModelState(0);
            IState _state = new NormalState(_model);
            _model.SetModelState(0);
            Context _context = _privateModel.GetField("_context") as Context;
            Assert.IsTrue(_context.GetState() is NormalState);
        }
    }
}