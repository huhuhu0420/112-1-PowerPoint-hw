using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace PowerPoint.Command.Tests
{
    [TestClass]
    public class DrawCommandTests
    {
        private DrawCommand _drawCommand;
        private Mock<Model> _mockModel;
        private Shape _shape;
        private int _index;

        // test
        [TestInitialize]
        public void Setup()
        {
            _mockModel = new Mock<Model>();
            _shape = new Shape();
            _index = 0;
            _drawCommand = new DrawCommand(_mockModel.Object, _shape, _index, 0);
        }

        // test
        [TestMethod]
        public void ExecuteInsertsShapeAtGivenIndex()
        {
            _drawCommand.Execute();

            _mockModel.Verify(m => m.InsertShapeByShape(_shape, _index), Times.Once);
        }

        // test
        [TestMethod]
        public void UnexecuteRemovesLastShape()
        {
            _mockModel.Setup(m => m.GetShapes()).Returns(new BindingList<Shape>() { _shape });

            _drawCommand.Undo();

            _mockModel.Verify(m => m.RemoveShapeByIndex(It.IsAny<int>()), Times.Once);
        }
    }
}