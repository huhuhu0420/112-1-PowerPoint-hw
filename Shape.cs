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
        public Point2 GetInfo()
        {
            return _info;
        }

        /// <summary>
        /// get id
        /// </summary>
        /// <returns></returns>
        public int GetId()
        {
            return _id;
        }

        private string _shapeName = "";
        private Point2 _info;
        private int _id = 0;
    }
    
    public enum ShapeType
    {
        LINE,
        RECTANGLE
    }
}