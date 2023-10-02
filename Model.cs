using System.Collections.Generic;

namespace PowerPoint
{
    public class Model
    {
        /// <summary>
        /// insert shape
        /// </summary>
        public void InsertShape()
        {
            ShapeType type = ShapeType.LINE;
            _shapes.Add(_shapeFactory.CreateShape(type));
        }

        private List<Shape> _shapes = new List<Shape>();
        private ShapeFactory _shapeFactory = new ShapeFactory();
    }
}