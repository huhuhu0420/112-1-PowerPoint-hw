using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;
using PowerPoint.State;

namespace PowerPoint.State.Tests
{
    [TestClass]
    public class DrawingStateTests
    {
        private Mock<Model> _mockModel;
        private DrawingState _drawingState;

        // test
        [TestInitialize]
        public void Setup()
        {
            _mockModel = new Mock<Model>();
            _drawingState = new DrawingState(_mockModel.Object);
        }

        // test
        [TestMethod]
        public void MouseDownTest()
        {
            var point = new Point(1, 1);
            var type = ShapeType.RECTANGLE;

            _drawingState.MouseDown(null, point, type);

            _mockModel.Verify(m => m.PressedPointer(point, type), Times.Once);
        }

        // test
        [TestMethod]
        public void MouseMoveTest_WhenPressed()
        {
            var point = new Point(1, 1);

            _drawingState.MouseMove(null, point, true);

            _mockModel.Verify(m => m.MovedPointer(point), Times.Once);
        }

        // test
        [TestMethod]
        public void MouseMoveTest_WhenNotPressed()
        {
            var point = new Point(1, 1);

            _drawingState.MouseMove(null, point, false);

            _mockModel.Verify(m => m.MovedPointer(point), Times.Never);
        }

        // test
        [TestMethod]
        public void MouseUpTest()
        {
            var mockContext = new Mock<Context>(_mockModel.Object);
            var point = new Point(1, 1);
            var type = ShapeType.RECTANGLE;

            _drawingState.MouseUp(mockContext.Object, point, type);

            _mockModel.Verify(m => m.ReleasedPointer(point, type), Times.Once);
            mockContext.Verify(c => c.SetState(It.IsAny<NormalState>()), Times.Once);
        }

        // test
        [TestMethod]
        public void DrawTest_WhenPressed()
        {
            var mockGraphics = new Mock<IGraphics>();

            _drawingState.Draw(mockGraphics.Object, true);

            _mockModel.Verify(m => m.DrawHint(mockGraphics.Object), Times.Once);
            _mockModel.Verify(m => m.DrawShapes(mockGraphics.Object), Times.Once);
        }

        // test
        [TestMethod]
        public void DrawTest_WhenNotPressed()
        {
            var mockGraphics = new Mock<IGraphics>();

            _drawingState.Draw(mockGraphics.Object, false);

            _mockModel.Verify(m => m.DrawHint(mockGraphics.Object), Times.Never);
            _mockModel.Verify(m => m.DrawShapes(mockGraphics.Object), Times.Once);
        }

        // test
        [TestMethod]
        public void GetStateTest()
        {
            var state = _drawingState.GetState();

            Assert.AreEqual(Model.ModelState.Drawing, state);
        }
    }
}