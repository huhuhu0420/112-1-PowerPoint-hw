using System.Collections.Generic;

namespace PowerPoint
{
    public class Model
    {
        /// <summary>
        /// insert shape
        /// </summary>
        public void InsertRow(ShapeType type)
        {
            _shapes.Add(_shapeFactory.CreateShape(type));
        }

        private List<Shape> _shapes = new List<Shape>();
        private ShapeFactory _shapeFactory = new ShapeFactory();
    }
}