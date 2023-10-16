using System.Drawing;

namespace PowerPoint
{
    public class Circle : Shape
    {
        public Circle(PointF point1, PointF point2) : base(point1, point2)
        {
            _shapeName = Constant.CIRCLE;
        }
    }
}