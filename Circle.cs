using System.Drawing;

namespace PowerPoint
{
    public class Circle : Shape
    {
        public Circle(PointDouble point1, PointDouble point2) : base(point1, point2)
        {
            _shapeName = Constant.CIRCLE;
            Type = ShapeType.CIRCLE;
        }

        public Circle()
        {
            _shapeName = Constant.CIRCLE;
            Type = ShapeType.CIRCLE;
        }
    }
}