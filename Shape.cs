using System;
using System.Drawing;
using System.Security.Authentication.ExtendedProtection;
using System.Windows;

namespace PowerPoint
{
    public class Shape
    {
        public Shape (PointD point1, PointD point2)
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
        private string FormatCoordinate(PointD point)
        {
            return Constant.PARENTHESIS1 + point.X + Constant.COMMA + point.Y + Constant.PARENTHESIS2;
        }

        public void DrawLine(IGraphics graphics)
        {
            graphics.DrawLine(Point1, Point2);
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

        public PointD Point1
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

        public PointD Point2
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

        protected string _shapeName = "";
        protected PointD _point1 = new PointD(0, 0);
        protected PointD _point2 = new PointD(0, 0);
    }
    
    public enum ShapeType
    {
        LINE,
        RECTANGLE,
        CIRCLE
    }
}