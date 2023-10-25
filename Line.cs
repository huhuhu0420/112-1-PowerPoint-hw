using System.Drawing;

namespace PowerPoint
{
    public class Line : Shape
    {
        public Line(Point point1, Point point2) : base(point1, point2)
        { 
            _shapeName = Constant.LINE;
        }

        public Line()
        {
            _shapeName = Constant.LINE;
        }

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(IGraphics graphics)
        {
            graphics.DrawLine(_point1, _point2);
        }
    }
}