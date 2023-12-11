using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;
using System.Windows.Forms;
using PowerPoint;
using PowerPoint.PresentationModel;
using PowerPoint.State;

namespace PowerPointTests
{
    [TestClass]
    public class WindowsFormsGraphicsAdaptorTest
    {
        private WindowsFormsGraphicsAdaptor _windowsFormsGraphicsAdaptor;
        // private Mock<Graphics> _mockGraphics;
        Graphics _graphics;
        
        // test
        [TestInitialize]
        public void Initialize()
        {
            // _mockGraphics = new Mock<Graphics>();
            _graphics = Graphics.FromImage(new Bitmap(1, 1));
            _windowsFormsGraphicsAdaptor = new WindowsFormsGraphicsAdaptor(_graphics);
        }
        
        // test
        [TestMethod]
        public void ClearAllTest()
        {
            _windowsFormsGraphicsAdaptor.ClearAll();
        }

        // test
        [TestMethod]
        public void DrawLineTest()
        {
            _windowsFormsGraphicsAdaptor.DrawLine(new Pen(Color.Black), new PointF(1, 1), new PointF(2, 2));
            // _mockGraphics.Verify(g => g.DrawLine(It.IsAny<Pen>(), It.IsAny<PointF>(), It.IsAny<PointF>()), Times.Once);
        }
        
        // test
        [TestMethod]
        public void DrawLineTest_WithLineType()
        {
            _windowsFormsGraphicsAdaptor.DrawLine(new Pen(Color.Black), new PointF(1, 1), new PointF(2, 2), Line.LineType.LeftTop);
            _windowsFormsGraphicsAdaptor.DrawLine(new Pen(Color.Black), new PointF(1, 1), new PointF(2, 2), Line.LineType.RightTop);
            _windowsFormsGraphicsAdaptor.DrawLine(new Pen(Color.Black), new PointF(1, 1), new PointF(2, 2), Line.LineType.LeftBottom);
            _windowsFormsGraphicsAdaptor.DrawLine(new Pen(Color.Black), new PointF(1, 1), new PointF(2, 2), Line.LineType.RightBottom);
        }
        
        // test
        [TestMethod]
        public void DrawRectangleTest()
        {
            _windowsFormsGraphicsAdaptor.DrawRectangle(new Pen(Color.Black), new PointF(1, 1), new PointF(2, 2));
            _windowsFormsGraphicsAdaptor.DrawRectangle(new Pen(Color.Black), new PointF(2, 2), new PointF(1, 1));
        }
        
        // test
        [TestMethod]
        public void DrawCircleeTest()
        {
            _windowsFormsGraphicsAdaptor.DrawCircle(new Pen(Color.Black), new PointF(1, 1), new PointF(2, 2));
        }
        
        // test
        [TestMethod]
        public void DrawSelectPointTest()
        {
            _windowsFormsGraphicsAdaptor.DrawSelectPoint(new Pen(Color.Black), new PointF(1, 1), new PointF(1, 1));
        }
        
        // test
        [TestMethod]
        public void DrawSelectTest()
        {
            _windowsFormsGraphicsAdaptor.DrawSelect(new Pen(Color.Black), new PointF(2, 2), new PointF(1, 1));
        }
    }
}