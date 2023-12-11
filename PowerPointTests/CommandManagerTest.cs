using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace PowerPoint.Command.Tests
{
    [TestClass]
    public class CommandManagerTests
    {
        private CommandManager _commandManager;
        private Mock<ICommand> _mockCommand;
        PrivateObject _privateObject;

        [TestInitialize]
        public void Setup()
        {
            _commandManager = new CommandManager();
            _mockCommand = new Mock<ICommand>();
            _privateObject = new PrivateObject(_commandManager);
            _commandManager._undoRedoHistoryChanged += (bool isUndo, bool isRedo) => { };
        }

        [TestMethod]
        public void ExecuteTest()
        {
            _commandManager.Execute(_mockCommand.Object);
            var commandHistory = (List<ICommand>)_privateObject.GetField("_commandHistory");
            var redoHistory = (List<ICommand>)_privateObject.GetField("_redoHistory");
            Assert.IsTrue(commandHistory.Contains(_mockCommand.Object));
            Assert.IsFalse(redoHistory.Contains(_mockCommand.Object));
        }

        [TestMethod]
        public void UndoTest()
        {
            _commandManager.Execute(_mockCommand.Object);
            _commandManager.Execute(_mockCommand.Object);
            _commandManager.Undo();
            var commandHistory = (List<ICommand>)_privateObject.GetField("_commandHistory");
            var redoHistory = (List<ICommand>)_privateObject.GetField("_redoHistory");
            Assert.IsTrue(redoHistory.Contains(_mockCommand.Object));
        }

        [TestMethod]
        public void UndoWhenNoCommandsInHistoryTest()
        {
            _commandManager.Undo();

            var commandHistory = (List<ICommand>)_privateObject.GetField("_commandHistory");
            Assert.IsFalse(commandHistory.Contains(_mockCommand.Object));
        }

        [TestMethod]
        public void RedoTest()
        {
            _commandManager.Execute(_mockCommand.Object);
            _commandManager.Execute(_mockCommand.Object);
            _commandManager.Undo();
            _commandManager.Redo();
            _commandManager.Undo();
            _commandManager.Undo();
            _commandManager.Redo();
            var commandHistory = (List<ICommand>)_privateObject.GetField("_commandHistory");
            Assert.IsTrue(commandHistory.Contains(_mockCommand.Object));
        }

        [TestMethod]
        public void RedoWhenNoCommandsInRedoHistoryTest()
        {
            _commandManager.Redo();
            var commandHistory = (List<ICommand>)_privateObject.GetField("_commandHistory");

            Assert.IsFalse(commandHistory.Contains(_mockCommand.Object));
        }
    }
}