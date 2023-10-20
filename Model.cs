using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

        public void PointerPressed(PointD point)
        {
            if (point.X > 0 && point.Y > 0)
            {
                _firstPoint.X = point.X;
                _firstPoint.Y = point.Y;
                _hint.Point1.X = _firstPoint.X;
                _hint.Point1.Y = _firstPoint.Y;
            }
        }
        
        public void PointerMoved(PointD point)
        {
            _hint.Point2.X = point.X;
            _hint.Point2.Y = point.Y;
            NotifyModelChanged();
        }
        
        public void PointerReleased(PointD point, ShapeType type)
        {
            Shape hint = _shapeFactory.CreateShape(type);
            hint.Point1.X = _firstPoint.X;
            hint.Point1.Y = _firstPoint.Y;
            hint.Point2.X = point.X;
            hint.Point2.Y = point.Y;
            _lines.Add(hint);
            NotifyModelChanged();
            // Debug.Print(_lines.Count.ToString());
        }

        public void Clear()
        {
            _lines.Clear();
            NotifyModelChanged();
        }
        
        public void Draw(IGraphics graphics)
        {
            foreach (Shape aLine in _lines)
                aLine.Draw(graphics);
            // Debug.Print("draw");
        }

        public void DrawHint(IGraphics graphics, ShapeType type)
        {
            _hint.Type = type;
            _hint.Draw(graphics);
        }
        
        void NotifyModelChanged()
        {
            if (_modelChanged != null)
                _modelChanged();
            // Debug.Print("model change");
        }

        private readonly BindingList<Shape> _shapes = new BindingList<Shape>();
        private readonly ShapeFactory _shapeFactory = new ShapeFactory();

        Shape _hint = new Shape();
        List<Shape> _lines = new List<Shape>();
        
        private PointD _firstPoint = new PointD(0, 0);
        public event ModelChangedEventHandler _modelChanged;

        public delegate void ModelChangedEventHandler();
    }
}