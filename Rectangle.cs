namespace PowerPoint
{
    public class Rectangle : Shape 
    {
        public Rectangle(Point2 info) : base(info)
        {
            _shapeName = Constant.RECTANGLE;
        }
    }
}