using System.Drawing;

namespace PowerPoint
{
    public class Circle : Shape
    {
        public Circle(PointD point1, PointD point2) : base(point1, point2)
        {
            _shapeName = Constant.CIRCLE;
        }
    }
}