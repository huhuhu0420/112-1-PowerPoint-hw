﻿using System.Drawing;

namespace PowerPoint
{
    public class Rectangle : Shape 
    {
        public Rectangle(Point point1, Point point2) : base(point1, point2)
        {
            _shapeName = Constant.RECTANGLE;
            Type = ShapeType.RECTANGLE;
        }

        public Rectangle()
        {
            _shapeName = Constant.RECTANGLE;
            Type = ShapeType.RECTANGLE;
        }

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(IGraphics graphics)
        {
            graphics.DrawRectangle(Point1, Point2);
        }
    }
}