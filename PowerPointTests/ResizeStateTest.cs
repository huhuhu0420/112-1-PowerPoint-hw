using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;
using PowerPoint.State;

namespace PowerPoint.State.Tests
{
    [TestClass]
    public class ResizeStateTests
    {
        private Mock<Model> _mockModel;
        private ResizeState _resizeState;

        [TestInitialize]
        public void Setup()
        {
            _mockModel = new Mock<Model>();
            _resizeState = new ResizeState(_mockModel.Object);
        }
        
        [TestMethod]
        public void MouseDownTest()
        {
            var point = new Point(1, 1);
            var mockContext = new Mock<Context>(_mockModel.Object);

            _resizeState.MouseDown(mockContext.Object, point, ShapeType.LINE);
        }

        [TestMethod]
        public void MouseMoveTest_WhenPressed()
        {
            var point = new Point(1, 1);

            _resizeState.MouseMove(null, point, true);

            _mockModel.Verify(m => m.ResizeShape(point), Times.Once);
        }

        [TestMethod]
        public void MouseMoveTest_WhenNotPressedAndInShapeCorner()
        {
            var point = new Point(1, 1);
            var mockContext = new Mock<Context>(_mockModel.Object);

            _mockModel.Setup(m => m.IsInShapeCorner(point)).Returns(true);

            _resizeState.MouseMove(mockContext.Object, point, false);

            _mockModel.Verify(m => m.ResizeShape(point), Times.Never);
            mockContext.Verify(c => c.SetState(It.IsAny<SelectedState>()), Times.Never);
        }

        [TestMethod]
        public void MouseMoveTest_WhenNotPressedAndNotInShapeCorner()
        {
            var point = new Point(1, 1);
            var mockContext = new Mock<Context>(_mockModel.Object);

            _mockModel.Setup(m => m.IsInShapeCorner(point)).Returns(false);

            _resizeState.MouseMove(mockContext.Object, point, false);

            _mockModel.Verify(m => m.ResizeShape(point), Times.Never);
            mockContext.Verify(c => c.SetState(It.IsAny<SelectedState>()), Times.Once);
        }
        
        [TestMethod]
        public void MouseUpTest()
        {
            var mockContext = new Mock<Context>(_mockModel.Object);
            var point = new Point(1, 1);
            var type = ShapeType.RECTANGLE;

            _resizeState.MouseUp(mockContext.Object, point, type);
        }

        [TestMethod]
        public void DrawTest()
        {
            var mockGraphics = new Mock<IGraphics>();

            _resizeState.Draw(mockGraphics.Object, false);

            _mockModel.Verify(m => m.DrawShapes(mockGraphics.Object), Times.Once);
        }

        [TestMethod]
        public void GetStateTest()
        {
            var state = _resizeState.GetState();

            Assert.AreEqual(Model.ModelState.Resize, state);
        }
    }
}