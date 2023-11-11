using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace PowerPoint
{
    public class Model
    {
        public event ModelChangedEventHandler _modelChanged;
        public delegate void ModelChangedEventHandler();

        public Model()
        {
            _context = new Context(this);
        }

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
        /// mouse down
        /// </summary>
        /// <param name="point"></param>
        /// <param name="type"></param>
        public void MouseDown(Point point, ShapeType type)
        {
            _context.MouseDown(point, type);
        }

        public void MouseMove(Point point)
        {
            _context.MouseMove(point);
        }
        
        public void MouseUp(Point point, ShapeType type)
        {
            _context.MouseUp(point, type);
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
        /// select
        /// </summary>
        /// <param name="point"></param>
        public void SelectShape(Point point)
        {
            for (int i = _shapes.Count-1; i>=0; i--)
            {
                if (_shapes[i].IsInShape(point))
                {
                    Debug.Print(i.ToString());
                    _select = _shapeFactory.CreateShape(_shapes[i].Type, _shapes[i].GetPoint1(), _shapes[i].GetPoint2());
                    break;
                }
            }
        }

        public void DrawSelect(IGraphics graphics)
        {
            _select.Draw(graphics);
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
            Shape hint = _shapeFactory.CreateShape(type, _firstPoint, point);
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
            // Debug.Print("draw");
            foreach (Shape aLine in _shapes)
                aLine.Draw(graphics);
            // Debug.Print("draw");
            if (_select != null)
            {
                _select.DrawSelect(graphics);
            }
        }

        /// <summary>
        /// drae
        /// </summary>
        /// <param name="graphics"></param>
        public void DrawHint(IGraphics graphics)
        {
            _hint.Draw(graphics);
            // Debug.Print("draw");
        }
        
        /// <summary>
        /// notify
        /// </summary>
        public void NotifyModelChanged()
        {
            if (_modelChanged != null)
            {
                _modelChanged();
            }
            // ModelChanged?.Invoke();  // better usage but cannot use cause it is bad smell :(
        }
        
        public void SetModelState(PresentationModel.PresentationModel.ModelState modelState)
        {
            if (modelState == PresentationModel.PresentationModel.ModelState.Normal)
            {
                _context.SetState(new NormalState(this));
            }
            else if (modelState == PresentationModel.PresentationModel.ModelState.Drawing)
            {
                _context.SetState(new DrawingState(this));
            }
        }

        private readonly BindingList<Shape> _shapes = new BindingList<Shape>();
        private readonly ShapeFactory _shapeFactory = new ShapeFactory();
        Shape _hint;
        private Shape _select;
        private Point _firstPoint = new Point(0, 0);
        private Context _context;
    }
}