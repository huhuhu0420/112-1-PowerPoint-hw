using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;
using PowerPoint.State;

namespace PowerPoint.Tests
{
    [TestClass]
    public class ContextTests
    {
        private Mock<Model> _mockModel;
        private Context _context;

        // test
        [TestInitialize]
        public void Initialize()
        {
            _mockModel = new Mock<Model>();
            _context = new Context(_mockModel.Object);
        }

        // test
        [TestMethod]

        public void SetStateTest()
        {
            var mockState = new Mock<IState>();
            _context._stateChanged += (state) =>
            {
                Assert.AreEqual(mockState.Object, state); 
            };
            _context.SetState(mockState.Object);

            Assert.AreEqual(mockState.Object, _context.GetState());
        }

        // test
        [TestMethod]

        public void MouseDownTest()
        {
            var mockState = new Mock<IState>();
            _context.SetState(mockState.Object);
            var point = new Point(1, 1);

            _context.MouseDown(point, ShapeType.LINE);

            mockState.Verify(m => m.MouseDown(_context, point, ShapeType.LINE), Times.Once);
        }

        // test
        [TestMethod]

        public void MouseMoveTest()
        {
            var mockState = new Mock<IState>();
            _context.SetState(mockState.Object);
            var point = new Point(1, 1);

            _context.MouseMove(point);

            mockState.Verify(m => m.MouseMove(_context, point, It.IsAny<bool>()), Times.Once);
        }

        // test
        [TestMethod]

        public void MouseUpTest()
        {
            var mockState = new Mock<IState>();
            _context.SetState(mockState.Object);
            var point = new Point(1, 1);

            _context.MouseUp(point, ShapeType.LINE);

            mockState.Verify(m => m.MouseUp(_context, point, ShapeType.LINE), Times.Once);
        }

        // test
        [TestMethod]

        public void DrawTest()
        {
            var mockState = new Mock<IState>();
            _context.SetState(mockState.Object);
            var mockGraphics = new Mock<IGraphics>();

            _context.Draw(mockGraphics.Object);

            mockState.Verify(m => m.Draw(mockGraphics.Object, It.IsAny<bool>()), Times.Once);
        }

        // test
        [TestMethod]

        public void GetStateTest()
        {
            var mockState = new Mock<IState>();
            _context.SetState(mockState.Object);

            Assert.AreEqual(mockState.Object, _context.GetState());
        }
    }
}