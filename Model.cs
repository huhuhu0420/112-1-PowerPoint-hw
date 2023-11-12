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
        /// remove
        /// </summary>
        public void RemoveShape()
        {
            if (_selectIndex != -1)
            {
                RemoveShapeByIndex(_selectIndex);
                _select = null;
                _selectIndex = -1;
            }
        }
        
        /// <summary>
        /// remove shape
        /// </summary>
        /// <param name="index"></param>
        public void RemoveShapeByIndex(int index)
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

        /// <summary>
        /// move
        /// </summary>
        /// <param name="point"></param>
        public void MouseMove(Point point)
        {
            _context.MouseMove(point);
        }
        
        /// <summary>
        /// up
        /// </summary>
        /// <param name="point"></param>
        /// <param name="type"></param>
        public void MouseUp(Point point, ShapeType type)
        {
            _context.MouseUp(point, type);
        }

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="graphics"></param>
        public void Draw(IGraphics graphics)
        {
            _context.Draw(graphics);
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
            bool isSelect = false;
            for (int i = _shapes.Count-1; i>=0; i--)
            {
                if (_shapes[i].IsInShape(point))
                {
                    Debug.Print(i.ToString());
                    _select = _shapeFactory.CreateShape(_shapes[i].Type, _shapes[i].GetPoint1(), _shapes[i].GetPoint2());
                    _lastPoint = point;
                    _selectIndex = i;
                    isSelect = true;
                    break;
                }
                if (!isSelect)
                {
                    _select = null;
                    _selectIndex = -1;
                }
            }
            NotifyModelChanged();
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

        public void MoveShape(Point point)
        {
            Size bias = new Size(point.X - _lastPoint.X, point.Y - _lastPoint.Y);
            if (_selectIndex == -1)
            {
                return;
            }
            _shapes[_selectIndex].SetPoint1(_shapes[_selectIndex].GetPoint1() + bias);
            _shapes[_selectIndex].SetPoint2(_shapes[_selectIndex].GetPoint2() + bias);
            if (_select != null)
            {
                _select.SetPoint1(_select.GetPoint1() + bias);
                _select.SetPoint2(_select.GetPoint2() + bias);
            }
            _lastPoint = point;
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
        public void DrawShapes(IGraphics graphics)
        {
            // Debug.Print("draw");
            foreach (Shape aLine in _shapes)
                aLine.Draw(graphics);
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
        private Point _lastPoint = new Point(0, 0);
        private int _selectIndex = -1;
        private Context _context;
    }
}