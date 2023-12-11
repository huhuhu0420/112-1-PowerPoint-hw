using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;

namespace PowerPoint.Command.Tests
{
    [TestClass]
    public class MoveCommandTests
    {
        private MoveCommand _moveCommand;
        private Mock<Model> _mockModel;
        private int _index;
        private SizeF _bias;

        // test
        [TestInitialize]
        public void Setup()
        {
            _mockModel = new Mock<Model>();
            _index = 0;
            _bias = new SizeF(1, 1);
            _moveCommand = new MoveCommand(_mockModel.Object, _index, _bias);
        }

        // test
        [TestMethod]
        public void ExecuteMovesShapeByGivenBias()
        {
            _moveCommand.Execute();

            _mockModel.Verify(m => m.MoveShapeByBias(_bias, _index), Times.Once);
        }

        // test
        [TestMethod]
        public void UnexecuteMovesShapeByOppositeBias()
        {
            _moveCommand.Unexecute();

            _mockModel.Verify(m => m.MoveShapeByBias(new SizeF(-1 * _bias.Width, -1 * _bias.Height), _index), Times.Once);
        }
    }
}