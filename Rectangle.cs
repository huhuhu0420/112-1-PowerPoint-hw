namespace PowerPoint
{
    public class Rectangle : Shape 
    {
        public Rectangle(int id) : base(id)
        {
            const string RECTANGLENAME = "矩形";
            _shapeName = RECTANGLENAME;
        }
    }
}