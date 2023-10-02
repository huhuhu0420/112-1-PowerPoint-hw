using System.Collections.Generic;

namespace PowerPoint
{
    public class Model
    {
        public void InsertShape()
        {
            ShapeType type = ShapeType.LINE;
            _shapes.Add(_shapeFactory.CreateShape(type));
        }

        private List<Shape> _shapes;
        private ShapeFactory _shapeFactory;
    }
}