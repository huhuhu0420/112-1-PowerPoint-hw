using System;
using System.Drawing;

namespace PowerPoint
{
    public class Shape
    {
        public Shape (int id) {
            _id = id;
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
            return "{" + _info.TopLeft.X + "," + _info.TopLeft.Y + "}" 
                + "{" + _info.DownRight.X + "," + _info.DownRight.Y + "}";
        }

        /// <summary>
        /// get id
        /// </summary>
        /// <returns></returns>
        public int GetId()
        {
            return _id;
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
        protected int _id = 0;
    }
    
    public enum ShapeType
    {
        LINE,
        RECTANGLE
    }
}