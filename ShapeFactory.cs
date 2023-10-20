﻿using System;
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
            const int MAX = 200;
            PointDouble point1 = new PointDouble(_random.Next(0, MAX), _random.Next(0, MAX));
            PointDouble point2 = new PointDouble(_random.Next(0, MAX), _random.Next(0, MAX));
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