using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;

namespace PowerPoint
{
    public class Model
    {
        public event ModelChangedEventHandler ModelChanged;
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
        public void PressedPointer(PointDouble point, ShapeType type)
        {
            if (point.X > 0 && point.Y > 0)
            {
                _firstPoint.X = point.X;
                _firstPoint.Y = point.Y;
                _hint.Point1.X = _firstPoint.X;
                _hint.Point1.Y = _firstPoint.Y;
                _hint.Type = type;
            }
        }
        
        /// <summary>
        /// move
        /// </summary>
        /// <param name="point"></param>
        public void MovedPointer(PointDouble point)
        {
            _hint.Point2.X = point.X;
            _hint.Point2.Y = point.Y;
            NotifyModelChanged();
        }
        
        /// <summary>
        /// release
        /// </summary>
        /// <param name="point"></param>
        /// <param name="type"></param>
        public void ReleasedPointer(PointDouble point, ShapeType type)
        {
            Shape hint = _shapeFactory.CreateShape(type);
            hint.Point1.X = _firstPoint.X;
            hint.Point1.Y = _firstPoint.Y;
            hint.Point2.X = point.X;
            hint.Point2.Y = point.Y;
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
        }
        
        /// <summary>
        /// notify
        /// </summary>
        void NotifyModelChanged()
        {
            ModelChanged?.Invoke();
            // Debug.Print("model change");
        }

        private readonly BindingList<Shape> _shapes = new BindingList<Shape>();
        private readonly ShapeFactory _shapeFactory = new ShapeFactory();
        readonly Shape _hint = new Shape();
        
        private readonly PointDouble _firstPoint = new PointDouble(0, 0);
    }
}