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

        // test
        [TestInitialize]
        public void Setup()
        {
            _mockModel = new Mock<Model>();
            _resizeState = new ResizeState(_mockModel.Object);
        }
        
        // test
        [TestMethod]
        public void MouseDownTest()
        {
            var point = new Point(1, 1);
            var mockContext = new Mock<Context>(_mockModel.Object);

            _resizeState.MouseDown(mockContext.Object, point, ShapeType.LINE);
        }

        // test
        [TestMethod]
        public void MouseMoveTest_WhenPressed()
        {
            var point = new Point(1, 1);

            _resizeState.MouseMove(null, point, true);

            _mockModel.Verify(m => m.ResizeShape(point, Model.Location.Bottom), Times.Once);
        }

        // test
        [TestMethod]
        public void MouseMoveTest_WhenNotPressedAndInShapeCorner()
        {
            var point = new Point(1, 1);
            var mockContext = new Mock<Context>(_mockModel.Object);

            _mockModel.Setup(m => m.IsInShapeCorner(point)).Returns(Model.Location.Bottom);

            _resizeState.MouseMove(mockContext.Object, point, false);

            _mockModel.Verify(m => m.ResizeShape(point, Model.Location.Bottom), Times.Never);
            mockContext.Verify(c => c.SetState(It.IsAny<SelectedState>()), Times.Never);
        }

        // test
        [TestMethod]
        public void MouseMoveTest_WhenNotPressedAndNotInShapeCorner()
        {
            var point = new Point(1, 1);
            var mockContext = new Mock<Context>(_mockModel.Object);

            _mockModel.Setup(m => m.IsInShapeCorner(point)).Returns(Model.Location.Bottom);

            _resizeState.MouseMove(mockContext.Object, point, false);

            _mockModel.Verify(m => m.ResizeShape(point, Model.Location.Bottom), Times.Never);
            mockContext.Verify(c => c.SetState(It.IsAny<SelectedState>()), Times.Once);
        }
        
        // test
        [TestMethod]
        public void MouseUpTest()
        {
            var mockContext = new Mock<Context>(_mockModel.Object);
            var point = new Point(1, 1);
            var type = ShapeType.RECTANGLE;

            _resizeState.MouseUp(mockContext.Object, point, type);
        }

        // test
        [TestMethod]
        public void DrawTest()
        {
            var mockGraphics = new Mock<IGraphics>();

            _resizeState.Draw(mockGraphics.Object, false);

            _mockModel.Verify(m => m.DrawShapes(mockGraphics.Object), Times.Once);
        }

        // test
        [TestMethod]
        public void GetStateTest()
        {
            var state = _resizeState.GetState();

            Assert.AreEqual(Model.ModelState.Resize, state);
        }
    }
}