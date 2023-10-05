using System.Collections.Generic;
using System.ComponentModel;

namespace PowerPoint
{
    public class Model
    {
        /// <summary>
        /// insert shape
        /// </summary>
        public void InsertShape(ShapeType type)
        {
            _shapes.Add(_shapeFactory.CreateShape(type));
        }
        
        /// <summary>
        /// remove shape
        /// </summary>
        /// <param name="index"></param>
        public void RemoveShape(int index)
        {
            _shapes.RemoveAt(index);
        }

        /// <summary>
        /// get shape
        /// </summary>
        /// <returns></returns>
        public BindingList<Shape> GetShapes()
        {
            return _shapes;
        }

        private readonly BindingList<Shape> _shapes = new BindingList<Shape>();
        private readonly ShapeFactory _shapeFactory = new ShapeFactory();
    }
}