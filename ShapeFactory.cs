using System;
using System.Drawing;

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
            Point2 info = new Point2(new Point(random.Next(0, 30), random.Next(0, 30)),
                                    new Point(random.Next(0, 30), random.Next(0, 30)));
            switch (type)
            {
                case ShapeType.LINE:
                    return new Line(info);
                case ShapeType.RECTANGLE:
                    return new Rectangle(info);
            }
            return new Line(info);
        }

        Random random = new Random();
    }
}