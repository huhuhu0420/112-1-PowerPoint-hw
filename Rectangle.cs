using System.Drawing;

namespace PowerPoint
{
    public class Rectangle : Shape 
    {
        public Rectangle(PointDouble point1, PointDouble point2) : base(point1, point2)
        {
            _shapeName = Constant.RECTANGLE;
            Type = ShapeType.RECTANGLE;
        }

        public Rectangle()
        {
            _shapeName = Constant.RECTANGLE;
            Type = ShapeType.RECTANGLE;
        }
    }
}