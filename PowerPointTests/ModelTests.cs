using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;
using PowerPoint.State;

namespace PowerPoint.Tests
{
    [TestClass]
    public class ModelTests
    {
        private Mock<ShapeFactory> _mockShapeFactory;
        private Model _model;

        [TestInitialize]
        public void Setup()
        {
            _mockShapeFactory = new Mock<ShapeFactory>();
            _model = new Model(_mockShapeFactory.Object);
        }

        [TestMethod]
        public void InsertShape_CallsCreateShapeInShapeFactory()
        {
            _model.InsertShape(ShapeType.LINE);

            _mockShapeFactory.Verify(m => m.CreateShape(ShapeType.LINE), Times.Once);
        }

        [TestMethod]
        public void RemoveShape_RemovesShapeFromShapes()
        {
            _model.InsertShape(ShapeType.LINE);
            _model.RemoveShape();

            Assert.AreEqual(0, _model.GetShapes().Count);
        }

        [TestMethod]
        public void MouseDown_CallsMouseDownInContext()
        {
            var point = new Point(1, 1);

            _model.MouseDown(point, ShapeType.LINE);

            _mockContext.Verify(m => m.MouseDown(point, ShapeType.LINE), Times.Once);
        }

        [TestMethod]
        public void MouseMove_CallsMouseMoveInContext()
        {
            var point = new Point(1, 1);

            _model.MouseMove(point);

            _mockContext.Verify(m => m.MouseMove(point), Times.Once);
        }

        [TestMethod]
        public void MouseUp_CallsMouseUpInContext()
        {
            var point = new Point(1, 1);

            _model.MouseUp(point, ShapeType.LINE);

            _mockContext.Verify(m => m.MouseUp(point, ShapeType.LINE), Times.Once);
        }

        [TestMethod]
        public void Clear_ClearsShapes()
        {
            _model.InsertShape(ShapeType.LINE);
            _model.Clear();

            Assert.AreEqual(0, _model.GetShapes().Count);
        }

        [TestMethod]
        public void SetModelState_SetsStateInContext()
        {
            _model.SetModelState(Model.ModelState.Drawing);

            _mockContext.Verify(m => m.SetState(It.IsAny<DrawingState>()), Times.Once);
        }
    }
}