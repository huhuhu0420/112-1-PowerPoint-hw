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
        public virtual Shape CreateShape(ShapeType type)
        {
            const int MAX = 440;
            Point point1 = new Point(_random.Next(0, MAX), _random.Next(0, MAX));
            Point point2 = new Point(_random.Next(0, MAX), _random.Next(0, MAX));
            switch (type)
            {
                case ShapeType.LINE:
                    return new Line(point1, point2);
                case ShapeType.RECTANGLE:
                    return new Rectangle(point1, point2);
                case ShapeType.CIRCLE:
                    return new Circle(point1, point2);
            }
            return new Line(point1, point2);
        }
        
        /// <summary>
        /// create with point
        /// </summary>
        /// <param name="type"></param>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public virtual Shape CreateShape(ShapeType type, Point point1, Point point2)
        {
            switch (type)
            {
                case ShapeType.LINE:
                    return new Line(point1, point2);
                case ShapeType.RECTANGLE:
                    return new Rectangle(point1, point2);
                case ShapeType.CIRCLE:
                    return new Circle(point1, point2);
            }
            return new Line(point1, point2);
        }

        readonly Random _random = new Random();
    }
}