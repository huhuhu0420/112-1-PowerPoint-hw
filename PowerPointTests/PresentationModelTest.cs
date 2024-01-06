using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using PowerPoint.State;

namespace PowerPoint.PresentationModel.Tests
{
    [TestClass]
    public class PresentationModelTests
    {
        private Mock<Model> _mockModel;
        private PresentationModel _presentationModel;
        PrivateObject _privatePresentationModel;

        // test
        [TestInitialize]
        public void Setup()
        {
            _mockModel = new Mock<Model>();
            _presentationModel = new PresentationModel(_mockModel.Object);
            _privatePresentationModel = new PrivateObject(_presentationModel);
        }

        // test
        [TestMethod]
        public void SetModelStateTest()
        {
            _presentationModel.SetModelState(Model.ModelState.Drawing);

            _mockModel.Verify(m => m.SetModelState(Model.ModelState.Drawing), Times.Once);
        }
        
        // test
        [TestMethod]
        public void HandleModelChangedTest()
        {
            bool isCalled = false;
            _presentationModel._modelChanged += () => { isCalled = true; };
            _presentationModel.HandleModelChanged();
            Assert.IsTrue(isCalled);
        }
        
        // test
        [TestMethod]
        public void HandlePropertyChangedTest()
        {
            bool isCalled = false;
            _presentationModel.PropertyChanged += (sender, e) => { isCalled = true; };
            _presentationModel.HandlePropertyChanged();
            Assert.IsTrue(isCalled);
        }
        
        // test
        [TestMethod]
        public void HandleStateChangeTest()
        {
            bool isCalled = false;
            _presentationModel._cursorChanged += (cursor) => { isCalled = true; };
            _presentationModel.HandleStateChange(new NormalState(_mockModel.Object));
            _presentationModel.HandleStateChange(new DrawingState(_mockModel.Object));
            _presentationModel.HandleStateChange(new ResizeState(_mockModel.Object));
            _presentationModel.HandleStateChange(new SelectedState(_mockModel.Object));
            Assert.IsTrue(isCalled);
        }

        // test
        [TestMethod]
        public void DeleteShapeTest()
        {
            _presentationModel.DeleteShape();
            _mockModel.Verify(m => m.HandleRemoveShape(It.IsAny<int>()), Times.Once);
        }

        // test
        [TestMethod]
        public void DrawTest()
        {
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(100, 100));
            _presentationModel.Draw(graphics);
            WindowsFormsGraphicsAdaptor _graphic = _privatePresentationModel.GetField("_graphic") as WindowsFormsGraphicsAdaptor;
            _mockModel.Verify(m => m.Draw(_graphic), Times.Once);
        }

        // test
        [TestMethod]
        public void PressedPointerTest()
        {
            var point = new PointF(1, 1);

            _presentationModel.PressedPointer(point);

            _mockModel.Verify(m => m.MouseDown(point, It.IsAny<ShapeType>()), Times.Once);
        }

        // test
        [TestMethod]
        public void MovedPointerTest()
        {
            var point = new PointF(1, 1);

            _presentationModel.MovedPointer(point);

            _mockModel.Verify(m => m.MouseMove(point), Times.Once);
        }

        // test
        [TestMethod]
        public void ReleasedPointerTest()
        {
            var point = new PointF(1, 1);

            _presentationModel.ReleasedPointer(point);

            _mockModel.Verify(m => m.MouseUp(point, It.IsAny<ShapeType>()), Times.Once);
        }

        // test
        [TestMethod]
        public void ClearTest()
        {
            _presentationModel.Clear();
            _mockModel.Verify(m => m.Clear(), Times.Once);
        }
        
        // test
        [TestMethod]
        public void GetShapesTest()
        {
            _presentationModel.GetShapes();
            _mockModel.Verify(m => m.GetShapes(), Times.Once);
        }

        // test
        [TestMethod]
        public void RemoveShapeTest()
        {
            _presentationModel.RemoveShape(1);

            _mockModel.Verify(m => m.HandleRemoveShape(1), Times.Once);
        }

        // test
        [TestMethod]
        public void InsertShapeTest()
        {
            _presentationModel.InsertShape(ShapeType.LINE);

            _mockModel.Verify(m => m.InsertShape(ShapeType.LINE), Times.Once);
        }

        // test
        [TestMethod]
        public void HandleButtonClickTest()
        {
            _presentationModel.HandleButtonClick(0);

            Assert.AreEqual(ShapeType.LINE, _presentationModel.Type);
        }

        // test
        [TestMethod]
        public void IsLineButtonCheckedTest()
        {
            _presentationModel.HandleButtonClick(0);

            Assert.IsTrue(_presentationModel.IsLineButtonChecked);
        }
        
        // test
        [TestMethod]
        public void IsRectangleButtonCheckedTest()
        {
            _presentationModel.HandleButtonClick(1);

            Assert.IsTrue(_presentationModel.IsRectangleButtonChecked);
        }
        
        // test
        [TestMethod]
        public void IsCircleButtonCheckedTest()
        {
            _presentationModel.HandleButtonClick(2);

            Assert.IsTrue(_presentationModel.IsCircleButtonChecked);
        }
        
        // test
        [TestMethod]
        public void IsArrowButtonCheckedTest()
        {
            _presentationModel.HandleButtonClick(3);

            Assert.IsTrue(_presentationModel.IsMouseButtonChecked);
        }
        
        // test
        [TestMethod]
        public void UndoTest()
        {
            _presentationModel.Undo();
            _mockModel.Verify(m => m.Undo(), Times.Once);
        }
        
        // test
        [TestMethod]
        public void RedoTest()
        {
            _presentationModel.Redo();
            _mockModel.Verify(m => m.Redo(), Times.Once);
        }
        
        // test
        [TestMethod]
        public void SetCanvasSizeTest()
        {
            _presentationModel.SetCanvasSize(100, 100);
            _mockModel.Verify(m => m.SetCanvasSize(100, 100), Times.Once);
        }
        
        // test
        [TestMethod]
        public void HandleUndoRedoHistoryChangedTest()
        {
            bool isCalled = false;
            _presentationModel._undoRedoHistoryChanged += (bool isUndo, bool isRedo) => { isCalled = true; };
            _presentationModel.HandleUndoRedoHistoryChanged(true, true);
            Assert.IsTrue(isCalled);
        }
        
        // test
        [TestMethod]
        public void SetPageIndexTest()
        {
            _presentationModel.SetPageIndex(1);
            _mockModel.Verify(m => m.SetPageIndex(1), Times.Once);
        }
        
        // test
        [TestMethod]
        public void AddPageTest()
        {
            _presentationModel.AddPage();
            _mockModel.Verify(m => m.AddPage(), Times.Once);
        }
        
        // test
        [TestMethod]
        public void DeletePageTest()
        {
            _presentationModel.DeletePage();
            _mockModel.Verify(m => m.DeletePage(), Times.Once);
        }
        
        // test
        [TestMethod]
        public void GetPageIndexTest()
        {
            _presentationModel.GetPageIndex();
            _mockModel.Verify(m => m.GetPageIndex(), Times.Once);
        }
        
        // test
        [TestMethod]
        public  void SaveTest()
        { 
            Task task = Task.Run(() => _presentationModel.Save());
            task.Wait();
            _mockModel.Verify(m => m.Save(), Times.Once);
        }
        
        // test
        [TestMethod]
        public void LoadTest()
        {
            _presentationModel.Load();
            _mockModel.Verify(m => m.Load(), Times.Once);
        }
        
        // test
        [TestMethod]
        public void DeleteDriveFileTest()
        {
            _presentationModel.DeleteDriveFile();
            _mockModel.Verify(m => m.DeleteDriveFile(), Times.Once);
            _mockModel.Setup(model => model.GetSelectIndex()).Returns(-1);
            _presentationModel.DeleteShape();
        }
        
        // test
        [TestMethod]
        public void IsSaveButtonEnabledTest()
        {
            Assert.IsTrue(_presentationModel.IsSaveButtonEnabled);
        }
        
        // test
        [TestMethod]
        public void HandlePangesChangedTest()
        {
            bool isCalled = false;
            _presentationModel._pagesChanged += (bool isUndo, int index) => { isCalled = true; };
            _presentationModel.HandlePagesChanged(true, 1);
            Assert.IsTrue(isCalled);
        }
        
        // test
        [TestMethod]
        public void DeleteTest()
        {
            _mockModel.Setup(model => model.GetSelectIndex()).Returns(0);
            _presentationModel.Delete(1);
            _mockModel.Setup(model => model.GetSelectIndex()).Returns(-1);
            _mockModel.Setup(model => model.GetPageIndex()).Returns(1);
            _presentationModel.AddPage();
            _presentationModel.Delete(1);
            Assert.AreEqual(1, _presentationModel.GetPageIndex());
        }
    }
}