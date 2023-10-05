namespace PowerPoint
{
    public class Line : Shape
    {
        public Line(Point2 info) : base(info)
        { 
            _shapeName = Constant.LINE;
        }
    }
}