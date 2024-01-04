using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;
using System.Threading.Tasks;
using PowerPoint.Command;
using PowerPoint.State;

namespace PowerPoint.Tests
{
    [TestClass]
    public class ModelTests2
    {
        private Model _model;
        PrivateObject _privateModel;
        private Mock<Context> _mockContext;
        private Mock<CommandManager> _mockCommandManager;

        // test
        [TestInitialize]
        public void Initialize()
        {
            _model = new Model();
            _mockContext = new Mock<Context>(_model);
            _privateModel = new PrivateObject(_model);
            _mockCommandManager = new Mock<CommandManager>();
            _model.SetContext(_mockContext.Object);
            _model.SetCommandManager(_mockCommandManager.Object);
        }

        // test
        [TestMethod]

        public void RemoveShapeTest()
        {
            _model.InsertShape(ShapeType.LINE);
            _privateModel.SetField(Constant.SELECT_INDEX, 0);
            _model.RemoveShape();

            Assert.AreEqual(0, _model.GetShapes().Count);
        }

        // test
        [TestMethod]

        public void RemoveShapeByIndexTest()
        {
            _model.InsertShape(ShapeType.LINE);
            _privateModel.SetField(Constant.SELECT_INDEX, 1);
            _model.RemoveShapeByIndex(0);

            Assert.AreEqual(0, _model.GetShapes().Count);
        }
        
        // test
        [TestMethod]

        public void InsertShapeTest()
        {
            _model.InsertShape(ShapeType.LINE);
            Assert.AreEqual(1, _model.GetShapes().Count);
        }
        
        // test
        [TestMethod]

        public void SetContestTest()
        {
            _model.SetContext(_mockContext.Object);
            var context = _privateModel.GetField(Constant.CONTEXT);
            Assert.AreEqual(_mockContext.Object, context);
        }        

        // test
        [TestMethod]

        public void SetCanvasSizeTest()
        {
            var width = Constant.TEN;
            var height = Constant.TEN;
            _model.InsertShape(ShapeType.LINE);
            _privateModel.SetField(Constant.SELECT, _model.GetShapes()[0]);
            _model.SetCanvasSize(width, height);
            var canvasWidth = _privateModel.GetField(Constant.CANVAS_WIDTH);
            Assert.AreEqual(width, canvasWidth);
        }
        
        // test
        [TestMethod]

        public void UndoTest()
        {
            _model.Undo();
            _mockCommandManager.Verify(m => m.Undo(), Times.Once);
        }
        
        // test

        [TestMethod]

        public void RedoTest()
        {
            _model.Redo();
            _mockCommandManager.Verify(m => m.Redo(), Times.Once);
        }

        // test
        [TestMethod]

        public void HandleMoveShapeTest()
        {
            _model.HandleMoveShape(0, new SizeF(1, 1));
            _mockCommandManager.Verify(m => m.Execute(It.IsAny<MoveCommand>()), Times.Once);
        }

        // test
        [TestMethod]

        public void HandleRemoveShapeTest()
        {
            _model.InsertShape(ShapeType.LINE);
            _privateModel.SetField(Constant.SELECT_INDEX, 0);
            _model.HandleRemoveShape(-1);
            _mockCommandManager.Verify(m => m.Execute(It.IsAny<RemoveCommand>()), Times.Once);
        }
        
        // test
        [TestMethod]

        public void InsertShapeByShape()
        {
            var shape = new Line();
            _model.InsertShapeByShape(shape, 0);
            Assert.AreEqual(1, _model.GetShapes().Count);
        }
        
        // test
        [TestMethod]

        public void SetUndoRedoHistoryTest()
        {
            bool isCalled = false;
            _model._undoRedoHistoryChanged += (bool isUndo, bool isRedo) => 
            {
                isCalled = true; 
            };
            _model.SetUndoRedoHistory(true, true);
            Assert.IsTrue(isCalled);
        }
        
        // test
        [TestMethod]

        public void GetPageIndexTest()
        {
            _model.SetPageIndex(0);
            Assert.AreEqual(0, _model.GetPageIndex());
        }
        
        // test
        [TestMethod]

        public void SetPageIndexTest()
        {
            _model.SetPageIndex(0);
            Assert.AreEqual(0, _model.GetPageIndex());
        }
        
        // test
        [TestMethod]

        public void AddPageTest()
        {
            _model.AddPage();
            Assert.AreEqual(1, _model.GetPageIndex());
        }
        
        // test
        [TestMethod]

        public void DeletePageTest()
        {
            _model.DeletePage();
            Assert.AreEqual(0, _model.GetPageIndex());
        }
        
        // test
        [TestMethod]

        public void DeletePageByIndexTest()
        {
            _model.DeletePageByIndex(0);
            Assert.AreEqual(0, _model.GetPageIndex());
        }
        
        // test
        [TestMethod]

        public void InsertPageByIndexTest()
        {
            _model.InsertPageByIndex(0, new BindingList<Shape>());
            Assert.AreEqual(0, _model.GetPageIndex());
        }
        
        // test
        [TestMethod]

        public void GetPagesTest()
        {
            _model.SetSelectNull();
            _model.SetSelectNull();
            _model.InsertPageByIndex(0, new BindingList<Shape>());
            var pages = _model.GetPages();
            Assert.AreEqual(2, pages.GetPageCount());
        }
        
        // test
        [TestMethod]

        public async Task ReadFileTest()
        {
            _model.InsertShape(ShapeType.LINE);
            Task task = Task.Run(() => _model.Save());
            await task;
            _model.Load();
            _model.ReadFile();
            _model.DeleteDriveFile();
            Assert.AreEqual(0, _model.GetPageIndex());
        }
        
        // test
        [TestMethod]

        public void ReadShapeTest()
        {
            const string LINE = "LINE";
            const string ZERO = "0";
            const string LEFT_TOP = "LeftTop";
            var info = new string[] { LINE, ZERO, ZERO, ZERO, ZERO, LEFT_TOP };
            _model.ReadShape(info);
            Assert.AreEqual(1, _model.GetShapes().Count);
        }
        
        // test
        [TestMethod]

        public void HandleResizeShapeTest()
        {
            _model.InsertShape(ShapeType.LINE);
            _privateModel.SetField(Constant.SELECT_INDEX, 0);
            _model.HandleResizeShape(0, new Circle());
            _mockCommandManager.Verify(m => m.Execute(It.IsAny<ResizeCommand>()), Times.Once);
        }
    }
}