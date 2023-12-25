using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace PowerPoint.Command.Tests
{
    [TestClass]
    public class RemoveCommandTests
    {
        private RemoveCommand _removeCommand;
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
            _removeCommand = new RemoveCommand(_mockModel.Object, _shape, _index, 0);
        }

        // test
        [TestMethod]
        public void ExecuteRemovesShapeAtGivenIndex()
        {
            _removeCommand.Execute();

            _mockModel.Verify(m => m.RemoveShapeByIndex(_index), Times.Once);
        }

        // test
        [TestMethod]
        public void UnexecuteInsertsShapeAtGivenIndex()
        {
            _removeCommand.Undo();

            _mockModel.Verify(m => m.InsertShapeByShape(_shape, _index), Times.Once);
        }
    }
}