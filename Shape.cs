using System;
using System.Drawing;

namespace PowerPoint
{
    public class Shape
    {
        public Shape (Point2 info) {
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
            return "(" + _info.TopLeft.X + "," + _info.TopLeft.Y + ")" + ", "
                + "(" + _info.DownRight.X + "," + _info.DownRight.Y + ")";
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
        RECTANGLE
    }
}