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

        [TestInitialize]
        public void Setup()
        {
            _mockModel = new Mock<Model>();
            _presentationModel = new PresentationModel(_mockModel.Object);
        }

        [TestMethod]
        public void SetModelStateTest()
        {
            _presentationModel.SetModelState(Model.ModelState.Drawing);

            _mockModel.Verify(m => m.SetModelState(Model.ModelState.Drawing), Times.Once);
        }

        [TestMethod]
        public void DeleteShapeTest()
        {
            _presentationModel.DeleteShape();

            _mockModel.Verify(m => m.RemoveShape(), Times.Once);
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
    }
}