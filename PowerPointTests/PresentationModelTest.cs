using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;
using System.Windows.Forms;
using PowerPoint.State;

namespace PowerPoint.PresentationModel.Tests
{
    [TestClass]
    public class PresentationModelTests
    {
        private Mock<Model> _mockModel;
        private PresentationModel _presentationModel;
        private ShapeFactory _shapeFactory;
        PrivateObject _privatePresentationModel;

        [TestInitialize]
        public void Setup()
        {
            _mockModel = new Mock<Model>();
            _presentationModel = new PresentationModel(_mockModel.Object);
            _privatePresentationModel = new PrivateObject(_presentationModel);
        }

        [TestMethod]
        public void SetModelStateTest()
        {
            _presentationModel.SetModelState(Model.ModelState.Drawing);

            _mockModel.Verify(m => m.SetModelState(Model.ModelState.Drawing), Times.Once);
        }
        
        [TestMethod]
        public void HandleModelChangedTest()
        {
            bool isCalled = false;
            _presentationModel._modelChanged += () => { isCalled = true; };
            _presentationModel.HandleModelChanged();
            Assert.IsTrue(isCalled);
        }
        
        [TestMethod]
        public void HandlePropertyChangedTest()
        {
            bool isCalled = false;
            _presentationModel.PropertyChanged += (sender, e) => { isCalled = true; };
            _presentationModel.HandlePropertyChanged();
            Assert.IsTrue(isCalled);
        }
        
        [TestMethod]
        public void HandleStateChangeTest()
        {
            bool isCalled = false;
            _presentationModel.HandleStateChange(new NormalState(_mockModel.Object));
            _presentationModel.CursorChanged += (cursor) => { isCalled = true; };
            _presentationModel.HandleStateChange(new NormalState(_mockModel.Object));
            _presentationModel.HandleStateChange(new DrawingState(_mockModel.Object));
            _presentationModel.HandleStateChange(new ResizeState(_mockModel.Object));
            _presentationModel.HandleStateChange(new SelectedState(_mockModel.Object));
            Assert.IsTrue(isCalled);
        }

        [TestMethod]
        public void DeleteShapeTest()
        {
            _presentationModel.DeleteShape();
            _mockModel.Verify(m => m.RemoveShape(), Times.Once);
        }

        [TestMethod]
        public void DrawTest()
        {
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(100, 100));
            _presentationModel.Draw(graphics);
            WindowsFormsGraphicsAdaptor _graphic = _privatePresentationModel.GetField("_graphic") as WindowsFormsGraphicsAdaptor;
            _mockModel.Verify(m => m.Draw(_graphic), Times.Once);
        }

        [TestMethod]
        public void PressedPointerTest()
        {
            var point = new Point(1, 1);

            _presentationModel.PressedPointer(point);

            _mockModel.Verify(m => m.MouseDown(point, It.IsAny<ShapeType>()), Times.Once);
        }

        [TestMethod]
        public void MovedPointerTest()
        {
            var point = new Point(1, 1);

            _presentationModel.MovedPointer(point);

            _mockModel.Verify(m => m.MouseMove(point), Times.Once);
        }

        [TestMethod]
        public void ReleasedPointerTest()
        {
            var point = new Point(1, 1);

            _presentationModel.ReleasedPointer(point);

            _mockModel.Verify(m => m.MouseUp(point, It.IsAny<ShapeType>()), Times.Once);
        }

        [TestMethod]
        public void ClearTest()
        {
            _presentationModel.Clear();
            _mockModel.Verify(m => m.Clear(), Times.Once);
        }
        
        [TestMethod]
        public void GetShapesTest()
        {
            _presentationModel.GetShapes();
            _mockModel.Verify(m => m.GetShapes(), Times.Once);
        }

        [TestMethod]
        public void RemoveShapeTest()
        {
            _presentationModel.RemoveShape(1);

            _mockModel.Verify(m => m.RemoveShapeByIndex(1), Times.Once);
        }

        [TestMethod]
        public void InsertShapeTest()
        {
            _presentationModel.InsertShape(ShapeType.LINE);

            _mockModel.Verify(m => m.InsertShape(ShapeType.LINE), Times.Once);
        }

        [TestMethod]
        public void HandleButtonClickTest()
        {
            _presentationModel.HandleButtonClick(0);

            Assert.AreEqual(ShapeType.LINE, _presentationModel.Type);
        }

        [TestMethod]
        public void IsLineButtonCheckedTest()
        {
            _presentationModel.HandleButtonClick(0);

            Assert.IsTrue(_presentationModel.IsLineButtonChecked);
        }
        
        [TestMethod]
        public void IsRectangleButtonCheckedTest()
        {
            _presentationModel.HandleButtonClick(1);

            Assert.IsTrue(_presentationModel.IsRectangleButtonChecked);
        }
        
        [TestMethod]
        public void IsCircleButtonCheckedTest()
        {
            _presentationModel.HandleButtonClick(2);

            Assert.IsTrue(_presentationModel.IsCircleButtonChecked);
        }
        
        [TestMethod]
        public void IsArrowButtonCheckedTest()
        {
            _presentationModel.HandleButtonClick(3);

            Assert.IsTrue(_presentationModel.IsMouseButtonChecked);
        }
    }
}