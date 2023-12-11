using System.Drawing;

namespace PowerPoint
{
    public class Rectangle : Shape 
    {
        public Rectangle(PointF point1, PointF point2) : base(point1, point2)
        {
            _shapeName = Constant.RECTANGLE;
            Type = ShapeType.RECTANGLE;
            _point1 = point1;
            _point2 = point2;
            if (_point1.X > _point2.X)
            {
                var temp = point1.X;
                _point1.X = _point2.X;
                _point2.X = temp;
            }
            if (_point1.Y > _point2.Y)
            {
                var temp = point1.Y;
                _point1.Y = _point2.Y;
                _point2.Y = temp;
            }
        }

        public Rectangle()
        {
            _shapeName = Constant.RECTANGLE;
            Type = ShapeType.RECTANGLE;
        }

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(IGraphics graphics)
        {
            Pen pen = new Pen(Color.DodgerBlue, Constant.THREE);
            graphics.DrawRectangle(pen, _point1, _point2);
        }
    }
}