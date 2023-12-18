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
            int maxX = _canvasWidth;
            int maxY = _canvasHeight;
            PointF point1 = new PointF(Global.TopLeftX, Global.TopLeftY);
            PointF point2 = new PointF(Global.BottomRightX, Global.BottomRightY);
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
        
        /// <summary>
        /// set
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public virtual void SetCanvasSize(int width, int height)
        {
            _canvasWidth = width;
            _canvasHeight = height;
        }

        readonly Random _random = new Random();
        private int _canvasWidth = Constant.FOUR_HUNDRED;
        private int _canvasHeight = Constant.FOUR_HUNDRED;
    }
}