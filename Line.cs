namespace PowerPoint
{
    public class Line : Shape
    {
        public Line(int id) : base(id)
        {
            const string LINENAME = "線";
            _shapeName = LINENAME;
        }
    }


}