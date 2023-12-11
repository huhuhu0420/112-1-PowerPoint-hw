using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace PowerPoint.Command.Tests
{
    [TestClass]
    public class AddCommandTests
    {
        private AddCommand _addCommand;
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
            _addCommand = new AddCommand(_mockModel.Object, _shape, _index);
        }

        // test
        [TestMethod]
        public void ExecuteInsertsShapeAtGivenIndex()
        {
            _addCommand.Execute();
            _mockModel.Verify(m => m.InsertShapeByShape(_shape, _index), Times.Once);
        }

        // test
        [TestMethod]
        public void UnexecuteRemovesLastShape()
        {
            _mockModel.Setup(m => m.GetShapes()).Returns(new BindingList<Shape>() {new Shape(), new Shape()});
            _addCommand.Unexecute();
            _mockModel.Verify(m => m.RemoveShapeByIndex(It.IsAny<int>()), Times.Once);
        }
    }
}