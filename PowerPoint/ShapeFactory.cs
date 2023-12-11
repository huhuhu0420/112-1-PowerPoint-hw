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
            PointF point1 = new PointF(_random.Next(0, MAX), _random.Next(0, MAX));
            PointF point2 = new PointF(_random.Next(0, MAX), _random.Next(0, MAX));
            switch (type)
            {
                case ShapeType.LINE:
                    var line = new Line(point1, point2);
                    line.SetLineType(Line.LineType.None);
                    return line;
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
        public virtual Shape CreateShape(ShapeType type, PointF point1, PointF point2)
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