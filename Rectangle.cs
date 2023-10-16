using System.Drawing;

namespace PowerPoint
{
    public class Rectangle : Shape 
    {
        public Rectangle(PointF point1, PointF point2) : base(point1, point2)
        {
            _shapeName = Constant.RECTANGLE;
        }
    }
}