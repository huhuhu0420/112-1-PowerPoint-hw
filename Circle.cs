namespace PowerPoint
{
    public class Circle : Shape
    {
        public Circle(Point2 info) : base(info)
        {
            _shapeName = Constant.CIRCLE;
        }
    }
}