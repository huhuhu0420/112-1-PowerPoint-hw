namespace PowerPoint
{
    public class Rectangle : Shape 
    {
        public Rectangle(Point2 info) : base(info)
        {
            const string RECTANGLENAME = "矩形";
            _shapeName = RECTANGLENAME;
        }
    }
}