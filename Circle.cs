using System.Drawing;

namespace PowerPoint
{
    public class Circle : Shape
    {
        public Circle(Point point1, Point point2) : base(point1, point2)
        {
            _shapeName = Constant.CIRCLE;
            Type = ShapeType.CIRCLE;
        }

        public Circle()
        {
            _shapeName = Constant.CIRCLE;
            Type = ShapeType.CIRCLE;
        }

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(IGraphics graphics)
        {
            graphics.DrawCircle(Point1, Point2);
        }
    }
}