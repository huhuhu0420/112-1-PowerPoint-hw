using System;
using System.Drawing;

namespace PowerPoint
{
    public class Shape
    {
        public Shape (Point2 info) 
        {
            _info = info;
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
            Point point1 = _info.TopLeft;
            Point point2 = _info.DownRight;
            return FormatCoordinate(point1) + Constant.COMMA + FormatCoordinate(point2);
        }

        /// <summary>
        /// format coordinate
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private string FormatCoordinate(Point point)
        {
            return Constant.PARENTHESIS1 + point.X + Constant.COMMA + point.Y + Constant.PARENTHESIS2;
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
        protected Point2 _info = new Point2();
    }
    
    public enum ShapeType
    {
        LINE,
        RECTANGLE,
        CIRCLE
    }
}