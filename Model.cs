﻿using System.Collections.Generic;
using System.ComponentModel;

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

        public ref BindingList<Shape> GetShapes()
        {
            return ref _shapes;
        }

        private BindingList<Shape> _shapes = new BindingList<Shape>();
        private ShapeFactory _shapeFactory = new ShapeFactory();
    }
}