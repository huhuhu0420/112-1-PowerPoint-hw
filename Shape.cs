using System;
using System.Diagnostics;
using System.Drawing;
using System.Security.Authentication.ExtendedProtection;
using System.Windows;

namespace PowerPoint
{
    public class Shape
    {
        public Shape (PointDouble point1, PointDouble point2)
        {
            Point1 = point1;
            Point2 = point2;
        }

        public Shape()
        {
            
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
            return FormatCoordinate(Point1) + Constant.COMMA + FormatCoordinate(Point2);
        }

        /// <summary>
        /// format coordinate
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private string FormatCoordinate(PointDouble point)
        {
            return Constant.PARENTHESIS1 + point.X + Constant.COMMA + point.Y + Constant.PARENTHESIS2;
        }

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="graphics"></param>
        public void Draw(IGraphics graphics)
        {
            graphics.Draw(Point1, Point2, Type);
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

        public PointDouble Point1
        {
            get
            {
                return _point1;
            }
            set
            {
                _point1 = value;
            }
        }

        public PointDouble Point2
        {
            get
            {
                return _point2;
            }
            set
            {
                _point2 = value;
            }
        }

        public ShapeType Type
        {
            get;
            set;
        }

        protected string _shapeName = "";
        protected PointDouble _point1 = new PointDouble(0, 0);
        protected PointDouble _point2 = new PointDouble(0, 0);
    }
    
    public enum ShapeType
    {
        LINE,
        RECTANGLE,
        CIRCLE
    }
}