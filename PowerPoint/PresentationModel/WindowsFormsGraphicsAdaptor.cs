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
        /// draw
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <param name="type"></param>
        public void DrawLine(Pen pen, Point point1, Point point2)
        {
            _graphics.DrawLine(pen, (float)point1.X, (float)point1.Y, (float)point2.X,
                (float)point2.Y);
        }

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        public void DrawRectangle(Pen pen, Point point1, Point point2)
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
            _graphics.DrawRectangle(pen, fix1, fix2, width, height);
        }

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        public void DrawCircle(Pen pen, Point point1, Point point2)
        {
            _graphics.DrawEllipse(pen, (float)point1.X, (float)point1.Y, (float)(point2.X - point1.X),
            (float)(point2.Y - point1.Y));
        }
        
        public void DrawSelectPoint(Pen pen, Point point1, Point point2)
        {
            const int radius = 3;
            const int diameter = radius * 2;
            _graphics.DrawEllipse(pen, point1.X - radius, point1.Y - radius, diameter, diameter);
            _graphics.DrawEllipse(pen, point2.X - radius, point2.Y - radius, diameter, diameter);
            _graphics.DrawEllipse(pen, point1.X - radius, point2.Y - radius, diameter, diameter);
            _graphics.DrawEllipse(pen, point2.X - radius, point1.Y - radius, diameter, diameter);
            int fix1 = (point1.X + point2.X) / 2;
            int fix2 = (point1.Y + point2.Y) / 2;
            _graphics.DrawEllipse(pen, fix1 - radius, point1.Y - radius, diameter, diameter);
            _graphics.DrawEllipse(pen, fix1 - radius, point2.Y - radius, diameter, diameter);
            _graphics.DrawEllipse(pen, point1.X - radius, fix2 - radius, diameter, diameter);
            _graphics.DrawEllipse(pen, point2.X - radius, fix2 - radius, diameter, diameter);
        }

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        public void DrawSelect(Pen pen, Point point1, Point point2)
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
            _graphics.DrawRectangle(pen, fix1, fix2, width, height);
            Pen pointPen = new Pen(Color.DeepPink, 1);
            DrawSelectPoint(pointPen, point1, point2);
        }
    }
}