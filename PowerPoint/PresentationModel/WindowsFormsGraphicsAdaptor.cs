using System.Diagnostics;
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
        
        public void DrawLine(Pen pen, Point point1, Point point2, Line.LineType lineType)
        {
            Debug.Print(lineType.ToString());
            switch (lineType)
            {
                case Line.LineType.LeftTop:
                    _graphics.DrawLine(pen, (float)point1.X, (float)point1.Y, (float)point2.X,
                        (float)point2.Y);
                    break;
                case Line.LineType.RightTop:
                    _graphics.DrawLine(pen, (float)point2.X, (float)point1.Y, (float)point1.X,
                        (float)point2.Y);
                    break;
                case Line.LineType.LeftBottom:
                    _graphics.DrawLine(pen, (float)point1.X, (float)point2.Y, (float)point2.X,
                        (float)point1.Y);
                    break;
                case Line.LineType.RightBottom:
                    _graphics.DrawLine(pen, (float)point2.X, (float)point2.Y, (float)point1.X,
                        (float)point1.Y);
                    break;
            }
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
        
        /// <summary>
        /// draw
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        public void DrawSelectPoint(Pen pen, Point point1, Point point2)
        {
            const int RADIUS = Constant.THREE;
            const int DIAMETER = RADIUS * Constant.TWO;
            _graphics.DrawEllipse(pen, point1.X - RADIUS, point1.Y - RADIUS, DIAMETER, DIAMETER);
            _graphics.DrawEllipse(pen, point2.X - RADIUS, point2.Y - RADIUS, DIAMETER, DIAMETER);
            _graphics.DrawEllipse(pen, point1.X - RADIUS, point2.Y - RADIUS, DIAMETER, DIAMETER);
            _graphics.DrawEllipse(pen, point2.X - RADIUS, point1.Y - RADIUS, DIAMETER, DIAMETER);
            int fix1 = (point1.X + point2.X) / Constant.TWO;
            int fix2 = (point1.Y + point2.Y) / Constant.TWO;
            _graphics.DrawEllipse(pen, fix1 - RADIUS, point1.Y - RADIUS, DIAMETER, DIAMETER);
            _graphics.DrawEllipse(pen, fix1 - RADIUS, point2.Y - RADIUS, DIAMETER, DIAMETER);
            _graphics.DrawEllipse(pen, point1.X - RADIUS, fix2 - RADIUS, DIAMETER, DIAMETER);
            _graphics.DrawEllipse(pen, point2.X - RADIUS, fix2 - RADIUS, DIAMETER, DIAMETER);
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