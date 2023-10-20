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
        public void Draw(PointD point1, PointD point2, ShapeType type)
        {
            switch (type)
            {
                case ShapeType.LINE:
                    _graphics.DrawLine(Pens.DodgerBlue, (float) point1.X, (float) point1.Y, (float) point2.X,
                        (float) point2.Y);
                    break;
                case ShapeType.RECTANGLE:
                    _graphics.DrawRectangle(Pens.DodgerBlue, (float) point1.X, (float) point1.Y, (float) (point2.X-point1.X),
                    (float) (point2.Y - point1.Y));
                    break;
                case ShapeType.CIRCLE:
                    _graphics.DrawEllipse(Pens.DodgerBlue, (float) point1.X, (float) point1.Y, (float) (point2.X-point1.X),
                                                              (float) (point2.Y - point1.Y));
                    break;
            }
        }
    }
}