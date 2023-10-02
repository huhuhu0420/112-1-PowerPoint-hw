namespace PowerPoint
{
    public class ShapeFactory
    {
        /// <summary>
        /// create shape
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Shape CreateShape(ShapeType type)
        {
            switch (type)
            {
                case ShapeType.LINE:
                    return new Line();
                case ShapeType.RECTANGLE:
                    return new Rectangle();
            }
            return new Line();
        }
    }
}