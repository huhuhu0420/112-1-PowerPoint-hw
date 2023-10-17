using System.Drawing;

namespace PowerPoint
{
    public class Line : Shape
    {
        public Line(PointD point1, PointD point2) : base(point1, point2)
        { 
            _shapeName = Constant.LINE;
        }

        public Line()
        {
            
        }
    }
}