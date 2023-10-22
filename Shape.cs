using System;
using System.Diagnostics;
using System.Drawing;
using System.Security.Authentication.ExtendedProtection;
using System.Windows;

namespace PowerPoint
{
    public class Shape
    {
        public Shape (Point point1, Point point2)
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
        private string FormatCoordinate(Point point)
        {
            return Constant.PARENTHESIS1 + point.X + Constant.COMMA + point.Y + Constant.PARENTHESIS2;
        }

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="graphics"></param>
        public virtual void Draw(IGraphics graphics)
        {
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

        /// <summary>
        /// set
        /// </summary>
        /// <param name="point"></param>
        public void SetPoint1(Point point)
        {
            Point1 = point;
        }

        /// <summary>
        /// set
        /// </summary>
        /// <param name="point"></param>
        public void SetPoint2(Point point)
        {
            Point2 = point;
        }

        public Point Point1
        {
            get;
            set;
        }

        public Point Point2
        {
            get;
            set;
        }

        public ShapeType Type
        {
            get;
            set;
        }

        protected string _shapeName = "";
        protected Point _point1 = new Point(0, 0);
        protected Point _point2 = new Point(0, 0);
    }
    
    public enum ShapeType
    {
        LINE,
        RECTANGLE,
        CIRCLE
    }
}