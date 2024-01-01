using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.ComponentModel;
using PowerPoint.Command;

namespace PowerPoint.Tests
{
    [TestClass]
    public class AddPageCommandTests
    {
        private AddPageCommand _command;
        private Mock<Model> _mockModel;
        private BindingList<Shape> _page;
        private int _index;
        private int _focusIndex;

        [TestInitialize]
        public void Setup()
        {
            _mockModel = new Mock<Model>();
            _page = new BindingList<Shape>();
            _index = 0;
            _focusIndex = 0;
            _command = new AddPageCommand(_mockModel.Object, _page, _index, _focusIndex);
        }

        [TestMethod]
        public void ExecuteInsertsNewPage()
        {
            _mockModel.Setup(m => m.GetPages().GetPageCount()).Returns(1);
            _command.Execute();
            _mockModel.Verify(m => m.InsertPageByIndex(It.IsAny<int>(), It.IsAny<BindingList<Shape>>()), Times.Once);
        }

        [TestMethod]
        public void UndoDeletesPageByIndex()
        {
            _command.Undo();

            _mockModel.Verify(m => m.DeletePageByIndex(_index), Times.Once);
        }
    }
}