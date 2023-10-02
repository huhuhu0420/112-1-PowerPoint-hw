using System.Drawing;

namespace PowerPoint
{
    public class Shape
    {
        public string GetShapeName()
        {
            return _shapeName;
        }

        public Point2 GetInfo()
        {
            return _info;
        }

        public int GetID()
        {
            return _id;
        }
        
        private string _shapeName;
        private Point2 _info;
        private int _id;
    }
    
    public enum ShapeType
    {
        LINE,
        RECTANGLE
    }
}