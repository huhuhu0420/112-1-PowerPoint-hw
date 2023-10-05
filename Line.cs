namespace PowerPoint
{
    public class Line : Shape
    {
        public Line(Point2 info) : base(info)
        {
            const string LINENAME = "線";
            _shapeName = LINENAME;
        }
    }


}