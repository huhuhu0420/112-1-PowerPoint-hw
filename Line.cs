﻿using System.Drawing;

namespace PowerPoint
{
    public class Line : Shape
    {
        public Line(Point point1, Point point2) : base(point1, point2)
        { 
            _shapeName = Constant.LINE;
            Type = ShapeType.LINE;
        }

        public Line()
        {
            _shapeName = Constant.LINE;
            Type = ShapeType.LINE;
        }
    }
}