using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;
using PowerPoint.State;

namespace PowerPoint.State.Tests
{
    [TestClass]
    public class SelectedStateTests
    {
        private Mock<Model> _mockModel;
        private SelectedState _selectedState;
        PrivateObject _privateSelectedState;

        // test
        [TestInitialize]
        public void Setup()
        {
            _mockModel = new Mock<Model>();
            _selectedState = new SelectedState(_mockModel.Object);
            _privateSelectedState = new PrivateObject(_selectedState);
        }

        // test
        [TestMethod]
        public void MouseDownTest_WhenShapeIsSelected()
        {
            var point = new PointF(1, 1);
            var mockContext = new Mock<Context>(_mockModel.Object);

            _mockModel.Setup(m => m.GetSelectIndex()).Returns(0);

            _selectedState.MouseDown(mockContext.Object, point, ShapeType.RECTANGLE);

            _mockModel.Verify(m => m.SelectShape(point), Times.Once);
            mockContext.Verify(c => c.SetState(It.IsAny<NormalState>()), Times.Never);
        }

        // test
        [TestMethod]
        public void MouseDownTest_WhenNoShapeIsSelected()
        {
            var point = new PointF(1, 1);
            var mockContext = new Mock<Context>(_mockModel.Object);

            _mockModel.Setup(m => m.GetSelectIndex()).Returns(-1);

            _selectedState.MouseDown(mockContext.Object, point, ShapeType.RECTANGLE);

            _mockModel.Verify(m => m.SelectShape(point), Times.Once);
            mockContext.Verify(c => c.SetState(It.IsAny<NormalState>()), Times.Once);
        }

        // test
        [TestMethod]
        public void MouseMoveTest_WhenPressed()
        {
            var point = new PointF(1, 1);
            var mockContext = new Mock<Context>(_mockModel.Object);

            _mockModel.Setup(m => m.IsInShapeCorner(point)).Returns(Model.Location.None);
            _selectedState.MouseMove(mockContext.Object, point, true);

            _mockModel.Verify(m => m.MoveShape(point), Times.Once);
            mockContext.Verify(c => c.SetState(It.IsAny<ResizeState>()), Times.Never);
        }

        // test
        [TestMethod]
        public void MouseMoveTest_WhenNotPressedAndInShapeCorner()
        {
            var point = new PointF(1, 1);
            var mockContext = new Mock<Context>(_mockModel.Object);

            _mockModel.Setup(m => m.IsInShapeCorner(point)).Returns(Model.Location.Bottom);

            _selectedState.MouseMove(mockContext.Object, point, false);

            _mockModel.Verify(m => m.MoveShape(point), Times.Never);
            mockContext.Verify(c => c.SetState(It.IsAny<ResizeState>()), Times.Once);
        }

        // test
        [TestMethod]
        public void MouseMoveTest_WhenNotPressedAndNotInShapeCorner()
        {
            var point = new PointF(1, 1);
            var mockContext = new Mock<Context>(_mockModel.Object);

            _mockModel.Setup(m => m.IsInShapeCorner(point)).Returns(Model.Location.Bottom);

            _selectedState.MouseMove(mockContext.Object, point, false);

            _mockModel.Verify(m => m.MoveShape(point), Times.Never);
        }

        // test
        [TestMethod]
        public void MouseUpTest()
        {
            var mockContext = new Mock<Context>(_mockModel.Object);
            var point = new PointF(1, 1);
            var type = ShapeType.RECTANGLE;

            _selectedState.MouseUp(mockContext.Object, point, type);
        }

        // test
        [TestMethod]
        public void DrawTest()
        {
            var mockGraphics = new Mock<IGraphics>();

            _selectedState.Draw(mockGraphics.Object, false);

            _mockModel.Verify(m => m.DrawShapes(mockGraphics.Object), Times.Once);
        }

        // test
        [TestMethod]
        public void GetStateTest()
        {
            var state = _selectedState.GetState();

            Assert.AreEqual(Model.ModelState.Selected, state);
        }
    }
}