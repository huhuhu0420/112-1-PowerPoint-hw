using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;
using PowerPoint.State;

namespace PowerPoint.Tests
{
    [TestClass]
    public class NormalStateTests
    {
        private Mock<Model> _mockModel;
        private NormalState _normalState;
        private PrivateObject _privateModel;

        // test
        [TestInitialize]
        public void Setup()
        {
            _mockModel = new Mock<Model>();
            _normalState = new NormalState(_mockModel.Object);
            _privateModel = new PrivateObject(_mockModel.Object);
        }

        // test
        [TestMethod]
        public void MouseDownTest()
        {
            var point = new Point(1, 1);
            var mockContext = new Mock<Context>(_mockModel.Object);

            _mockModel.SetupSequence(model => model.GetSelectIndex()).Returns(0);
            _normalState.MouseDown(mockContext.Object, point, ShapeType.LINE);

            _mockModel.Verify(m => m.SelectShape(point), Times.Once);
        }

        // test
        [TestMethod]
        public void MouseMoveTest()
        {
            var point = new Point(1, 1);
            var mockContext = new Mock<Context>(_mockModel.Object);

            _normalState.MouseMove(mockContext.Object, point, false);

            _mockModel.Verify(m => m.SelectShape(It.IsAny<Point>()), Times.Never);
        }

        // test
        [TestMethod]
        public void MouseUpTest()
        {
            var point = new Point(1, 1);
            var mockContext = new Mock<Context>(_mockModel.Object);

            _normalState.MouseUp(mockContext.Object, point, ShapeType.LINE);

            _mockModel.Verify(m => m.SelectShape(It.IsAny<Point>()), Times.Never);
        }

        // test
        [TestMethod]
        public void DrawTest()
        {
            var mockGraphics = new Mock<IGraphics>();

            _normalState.Draw(mockGraphics.Object, false);

            _mockModel.Verify(m => m.DrawShapes(mockGraphics.Object), Times.Once);
        }

        // test
        [TestMethod]
        public void GetStateTest()
        {
            var state = _normalState.GetState();

            Assert.AreEqual(Model.ModelState.Normal, state);
        }
    }
}