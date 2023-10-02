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
                    return new Line(_serialNumber++);
                case ShapeType.RECTANGLE:
                    return new Rectangle(_serialNumber++);
            }
            return new Line(_serialNumber++);
        }

        private static int _serialNumber = 0;
    }
}