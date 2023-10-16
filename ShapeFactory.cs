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
            const int MAX = 30;
            Point2 info = new Point2(new Point(_random.Next(0, MAX), _random.Next(0, MAX)),
                                    new Point(_random.Next(0, MAX), _random.Next(0, MAX)));
            switch (type)
            {
                case ShapeType.LINE:
                    return new Line(info);
                case ShapeType.RECTANGLE:
                    return new Rectangle(info);
                case ShapeType.CIRCLE:
                    return new Circle(info);
            }
            return new Line(info);
        }

        readonly Random _random = new Random();
    }
}