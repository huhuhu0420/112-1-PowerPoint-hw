using System;
using System.Drawing;
using System.Windows;

namespace PowerPoint
{
    public class Shape
    {
        public Shape (PointF point1, PointF point2)
        {
            _point1 = point1;
            _point2 = point2;
        }

        /// <summary>
        /// get name
        /// </summary>
        /// <returns></returns>
        public string GetShapeName()
        {
            return _shapeName;
        }

        /// <summary>
        /// get info
        /// </summary>
        /// <returns></returns>
        public string GetInfo()
        {
            return FormatCoordinate(_point1) + Constant.COMMA + FormatCoordinate(_point2);
        }

        /// <summary>
        /// format coordinate
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private string FormatCoordinate(PointF point)
        {
            return Constant.PARENTHESIS1 + point.X + Constant.COMMA + point.Y + Constant.PARENTHESIS2;
        }

        public void Draw(IGraphics graphics)
        {
            graphics.Draw(_point1, _point2);
        }

        public string ShapeName
        {
            get
            {
                return GetShapeName();
            }
        }

        public string Info
        {
            get
            {
                return GetInfo();
            }
        }

        protected string _shapeName = "";
        protected PointF _point1;
        protected PointF _point2;
    }
    
    public enum ShapeType
    {
        LINE,
        RECTANGLE,
        CIRCLE
    }
}