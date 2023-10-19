using System.Drawing;

namespace PowerPoint.PresentationModel
{
    public class WindowsFormsGraphicsAdaptor : IGraphics
    {
        Graphics _graphics;
        public WindowsFormsGraphicsAdaptor(Graphics graphics)
        {
            this._graphics = graphics;
        }
        public void ClearAll()
        {
        }
        public void DrawLine(PointD point1, PointD point2)
        {
            _graphics.DrawLine(Pens.Black, (float) point1.X, (float) point1.Y, (float) point2.X,
                (float) point2.Y);
        }
    }
}