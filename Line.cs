using System.Drawing;

namespace PowerPoint
{
    public class Line : Shape
    {
        public Line(PointF point1, PointF point2) : base(point1, point2)
        { 
            _shapeName = Constant.LINE;
        }
    }
}