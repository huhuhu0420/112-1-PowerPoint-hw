using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;

namespace PowerPoint
{
    public class Model
    {
        public event ModelChangedEventHandler _modelChanged;
        public delegate void ModelChangedEventHandler();

        /// <summary>
        /// insert shape
        /// </summary>
        public void InsertShape(ShapeType type)
        {
            _shapes.Add(_shapeFactory.CreateShape(type));
            NotifyModelChanged();
        }
        
        /// <summary>
        /// remove shape
        /// </summary>
        /// <param name="index"></param>
        public void RemoveShape(int index)
        {
            _shapes.RemoveAt(index);
            NotifyModelChanged();
        }

        /// <summary>
        /// get shape
        /// </summary>
        /// <returns></returns>
        public BindingList<Shape> GetShapes()
        {
            return _shapes;
        }
        
        /// <summary>
        /// press
        /// </summary>
        /// <param name="point"></param>
        /// <param name="type"></param>
        public void PressedPointer(Point point, ShapeType type)
        {
            if (point.X > 0 && point.Y > 0)
            {
                _firstPoint.X = point.X;
                _firstPoint.Y = point.Y;
                _hint = _shapeFactory.CreateShape(type);
                _hint.SetPoint1(_firstPoint);
            }
        }
        
        /// <summary>
        /// move
        /// </summary>
        /// <param name="point"></param>
        public void MovedPointer(Point point)
        {
            _hint.SetPoint2(point);
            NotifyModelChanged();
        }
        
        /// <summary>
        /// release
        /// </summary>
        /// <param name="point"></param>
        /// <param name="type"></param>
        public void ReleasedPointer(Point point, ShapeType type)
        {
            Shape hint = _shapeFactory.CreateShape(type);
            hint.SetPoint1(_firstPoint);
            hint.SetPoint2(point);
            _shapes.Add(hint);
            NotifyModelChanged();
            // Debug.Print(_lines.Count.ToString());
        }
        
        /// <summary>
        /// clear
        /// </summary>
        public void Clear()
        {
            _shapes.Clear();
            NotifyModelChanged();
        }
        
        /// <summary>
        /// draw
        /// </summary>
        /// <param name="graphics"></param>
        public void Draw(IGraphics graphics)
        {
            foreach (Shape aLine in _shapes)
                aLine.Draw(graphics);
            // Debug.Print("draw");
        }

        /// <summary>
        /// drae
        /// </summary>
        /// <param name="graphics"></param>
        public void DrawHint(IGraphics graphics)
        {
            _hint.Draw(graphics);
            Debug.Print("draw");
        }
        
        /// <summary>
        /// notify
        /// </summary>
        void NotifyModelChanged()
        {
            if (_modelChanged != null)
            {
                _modelChanged();
            }
            // ModelChanged?.Invoke();  // better usage but cannot use cause it is bad smell :(
        }

        private readonly BindingList<Shape> _shapes = new BindingList<Shape>();
        private readonly ShapeFactory _shapeFactory = new ShapeFactory();
        Shape _hint;
        
        private Point _firstPoint = new Point(0, 0);
    }
}