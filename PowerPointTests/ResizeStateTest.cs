using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;
using System.Windows.Forms;
using PowerPoint.State;

namespace PowerPoint.State.Tests
{
    [TestClass]
    public class ResizeStateTests
    {
        private Mock<Model> _mockModel;
        private ResizeState _resizeState;
        PrivateObject _privatResizeState;

        // test
        [TestInitialize]
        public void Setup()
        {
            _mockModel = new Mock<Model>();
            _resizeState = new ResizeState(_mockModel.Object);
            _privatResizeState = new PrivateObject(_resizeState);
        }
        
        // test
        [TestMethod]
        public void MouseDownTest()
        {
            var point = new PointF(1, 1);
            var mockContext = new Mock<Context>(_mockModel.Object);

            _resizeState.MouseDown(mockContext.Object, point, ShapeType.LINE);
        }

        // test
        [TestMethod]
        public void MouseMoveTest_WhenPressed()
        {
            var point = new PointF(1, 1);

            _privatResizeState.SetField("_location", Model.Location.Bottom);
            _resizeState.MouseMove(null, point, true);

            _mockModel.Verify(m => m.ResizeShape(point, Model.Location.Bottom), Times.Once);
        }

        // test
        [TestMethod]
        public void MouseMoveTest_WhenNotPressedAndInShapeCorner()
        {
            var point = new PointF(1, 1);
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
            var point = new PointF(1, 1);
            var mockContext = new Mock<Context>(_mockModel.Object);
            _mockModel.Setup(m => m.IsInShapeCorner(point)).Returns(Model.Location.None);
            

            _resizeState.MouseMove(mockContext.Object, point, false);

            _mockModel.Verify(m => m.ResizeShape(point, Model.Location.Bottom), Times.Never);
            mockContext.Verify(c => c.SetState(It.IsAny<SelectedState>()), Times.Once);
        }
        
        // test
        [TestMethod]
        public void MouseUpTest()
        {
            var mockContext = new Mock<Context>(_mockModel.Object);
            var point = new PointF(1, 1);
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
        
        // test
        [TestMethod]
        public void GetCursorForLocationTest()
        {
            _privatResizeState.SetField("_location", Model.Location.Bottom);
            var cursor = _resizeState.GetCursorForLocation();
            Assert.AreEqual(Cursors.SizeNS, cursor);
            _privatResizeState.SetField("_location", Model.Location.Left);
            cursor = _resizeState.GetCursorForLocation();
            Assert.AreEqual(Cursors.SizeWE, cursor);
            _privatResizeState.SetField("_location", Model.Location.LeftBottom);
            cursor = _resizeState.GetCursorForLocation();
            Assert.AreEqual(Cursors.SizeNESW, cursor);
            _privatResizeState.SetField("_location", Model.Location.RightBottom);
            cursor = _resizeState.GetCursorForLocation();
            Assert.AreEqual(Cursors.SizeNWSE, cursor);
            _privatResizeState.SetField("_location", Model.Location.None);
            cursor = _resizeState.GetCursorForLocation();
            Assert.AreEqual(Cursors.Default, cursor);
        }
    }
}