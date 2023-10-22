using System.Drawing;

namespace PowerPoint.PresentationModel
{
    public class WindowsFormsGraphicsAdaptor : IGraphics
    {
        readonly Graphics _graphics;
        public WindowsFormsGraphicsAdaptor(Graphics graphics)
        {
            this._graphics = graphics;
        }
        
        /// <summary>
        /// clear
        /// </summary>
        public void ClearAll()
        {
        }

        /// <summary>
        /// rect
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        public void HandleRectangle(Point point1, Point point2)
        {
            float width = System.Math.Abs(point2.X - point1.X);
            float height = System.Math.Abs(point2.Y - point1.Y);
            float fix1 = point1.X;
            float fix2 = point1.Y;
            if (point1.X > point2.X)
            {
                fix1 -= width;
            }
            if (point1.Y > point2.Y)
            {
                fix2 -= height;
            }
            _graphics.DrawRectangle(Pens.DodgerBlue, fix1, fix2, width, height);
        }
        
        /// <summary>
        /// draw
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <param name="type"></param>
        public void Draw(Point point1, Point point2, ShapeType type)
        {
            switch (type)
            {
                case ShapeType.LINE:
                    _graphics.DrawLine(Pens.DodgerBlue, (float)point1.X, (float)point1.Y, (float)point2.X,
                        (float)point2.Y);
                    break;
                case ShapeType.RECTANGLE:
                    HandleRectangle(point1, point2);
                    break;
                case ShapeType.CIRCLE:
                    _graphics.DrawEllipse(Pens.DodgerBlue, (float)point1.X, (float)point1.Y, (float)(point2.X - point1.X),
                    (float)(point2.Y - point1.Y));
                    break;
            }
        }
    }
}