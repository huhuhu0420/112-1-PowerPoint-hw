using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

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

        public void PointerPressed(PointF point)
        {
            if (point.X > 0 && point.Y > 0)
            {
                _firstPoint.X = point.X;
                _firstPoint.Y = point.Y;
                _isPressed = true;
                _hint.Point1.X = _firstPoint.X;
                _hint.Point1.Y = _firstPoint.Y;
            }
        }
        
        public void PointerMoved(double x, double y)
        {
            if (_isPressed)
            {
                _hint.Point2.X = x;
                _hint.Point2.Y = y;
                NotifyModelChanged();
            }
        }

        public void Clear()
        {
            _isPressed = false;
            _lines.Clear();
            NotifyModelChanged();
        }
        
        public void Draw(IGraphics graphics)
        {
            graphics.ClearAll();
            foreach (Line aLine in _lines)
                aLine.DrawLine(graphics);
            if (_isPressed)
                _hint.DrawLine(graphics);
        }
        
        void NotifyModelChanged()
        {
            if (_modelChanged != null)
                _modelChanged();
        }

        private readonly BindingList<Shape> _shapes = new BindingList<Shape>();
        private readonly ShapeFactory _shapeFactory = new ShapeFactory();

        Line _hint = new Line();
        List<Line> _lines = new List<Line>();
        
        private PointD _firstPoint = new PointD(0, 0);
        private bool _isPressed = false;
        public event ModelChangedEventHandler _modelChanged;

        public delegate void ModelChangedEventHandler();
    }
}