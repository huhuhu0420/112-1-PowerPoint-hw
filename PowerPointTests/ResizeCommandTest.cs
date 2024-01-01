using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;
using Castle.Components.DictionaryAdapter;
using PowerPoint.Command;

namespace PowerPoint.Tests
{
    [TestClass]
    public class ResizeCommandTests
    {
        private ResizeCommand _command;
        private Mock<Model> _mockModel;
        private Mock<Shape> _mockShape;
        private Mock<Shape> _mockNewShape;
        private int _index;

        [TestInitialize]
        public void Setup()
        {
            _mockModel = new Mock<Model>();
            _mockShape = new Mock<Shape>();
            _mockNewShape = new Mock<Shape>();
            _index = 0;
            _command = new ResizeCommand(_mockModel.Object, _mockShape.Object, _mockNewShape.Object, _index);
        }

        [TestMethod]
        public void ExecuteReplacesShapeAtIndex()
        {
            _mockModel.Setup(m => m.GetShapes()).Returns(new System.ComponentModel.BindingList<Shape>() { _mockShape.Object });
            _command.Execute();
            _mockModel.Verify(m => m.GetShapes(), Times.Once);
        }

        [TestMethod]
        public void UndoRevertsShapeAtIndex()
        {
            _mockModel.Setup(m => m.GetShapes()).Returns(new System.ComponentModel.BindingList<Shape>() { _mockShape.Object });
            _command.Undo();
            _mockModel.Verify(m => m.GetShapes(), Times.Once);
        }
    }
}