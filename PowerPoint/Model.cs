using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using PowerPoint.State;

namespace PowerPoint
{
    public class Model
    {
        public delegate void ModelChangedEventHandler();
        public event ModelChangedEventHandler _modelChanged;
        public event Context.StateChangedEventHandler _stateChanged;

        public Model()
        {
            _shapeFactory = new ShapeFactory();
            _hint = new Shape();
            _firstPoint = new Point(0, 0);
            _lastPoint = new Point(0, 0);
        }

        public void SetContext(Context context)
        {
            _context = context;
            _context._stateChanged += HandleStateChanged;
        }
        
        /// <summary>
        /// state changed
        /// </summary>
        public void HandleStateChanged(IState state)
        {
            if (_stateChanged != null)
            {
                _stateChanged(state);
            }
        }

        /// <summary>
        /// insert shape
        /// </summary>
        public virtual void InsertShape(ShapeType type)
        {
            _shapes.Add(_shapeFactory.CreateShape(type));
            NotifyModelChanged();
        }

        /// <summary>
        /// remove
        /// </summary>
        public virtual void RemoveShape()
        {
            if (_selectIndex != -1)
            {
                RemoveShapeByIndex(_selectIndex);
                _select = null;
                _selectIndex = -1;
                NotifyModelChanged();
            }
        }
        
        /// <summary>
        /// remove shape
        /// </summary>
        /// <param name="index"></param>
        public virtual void RemoveShapeByIndex(int index)
        {
            _shapes.RemoveAt(index);
            if (_selectIndex == index)
            {
                _select = null;
                _selectIndex = -1;
            }
            else if (_selectIndex > index)
            {
                _selectIndex--;
            }
            NotifyModelChanged();
        }

        /// <summary>
        /// get shape
        /// </summary>
        /// <returns></returns>
        public virtual BindingList<Shape> GetShapes()
        {
            return _shapes;
        }

        /// <summary>
        /// mouse down
        /// </summary>
        /// <param name="point"></param>
        /// <param name="type"></param>
        public virtual void MouseDown(Point point, ShapeType type)
        {
            _context.MouseDown(point, type);
        }

        /// <summary>
        /// move
        /// </summary>
        /// <param name="point"></param>
        public virtual void MouseMove(Point point)
        {
            _context.MouseMove(point);
            _lastPoint = point;
        }
        
        /// <summary>
        /// up
        /// </summary>
        /// <param name="point"></param>
        /// <param name="type"></param>
        public virtual void MouseUp(Point point, ShapeType type)
        {
            _context.MouseUp(point, type);
        }

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="graphics"></param>
        public virtual void Draw(IGraphics graphics)
        {
            _context.Draw(graphics);
        }
        
        /// <summary>
        /// press
        /// </summary>
        /// <param name="point"></param>
        /// <param name="type"></param>
        public virtual void PressedPointer(Point point, ShapeType type)
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
        /// is in
        /// </summary>
        /// <param name="point"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual bool IsInShape(Point point, int index)
        {
            if (_shapes[index].IsInShape(point))
            {
                // Debug.Print(index.ToString());
                _select = _shapeFactory.CreateShape(_shapes[index].Type, _shapes[index].GetPoint1(), _shapes[index].GetPoint2());
                _lastPoint = point;
                _selectIndex = index;
                return true;
            }
            return false;
        }
        
        public virtual bool IsInShapeCorner(Point point)
        {
            if (_selectIndex == -1)
            {
                return false;
            }
            if (_shapes[_selectIndex].IsInShapeCorner(point) != Location.None)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// select
        /// </summary>
        /// <param name="point"></param>
        public virtual void SelectShape(Point point)
        {
            bool isSelect = false;
            for (int i = _shapes.Count - 1; i >= 0; i --)
            {
                isSelect = IsInShape(point, i);
                if (isSelect)
                {
                    break;
                }
            }
            if (!isSelect)
            {
                _select = null;
                _selectIndex = -1;
            }
            NotifyModelChanged();
        }

        /// <summary>
        /// move
        /// </summary>
        /// <param name="point"></param>
        public virtual void MovedPointer(Point point)
        {
            _hint.SetPoint2(point);
            NotifyModelChanged();
        }

        /// <summary>
        /// move
        /// </summary>
        /// <param name="point"></param>
        public virtual void MoveShape(Point point)
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
            NotifyModelChanged();
        }
        
        /// <summary>
        /// release
        /// </summary>
        /// <param name="point"></param>
        /// <param name="type"></param>
        public virtual void ReleasedPointer(Point point, ShapeType type)
        {
            Shape hint = _shapeFactory.CreateShape(type, _firstPoint, point);
            _shapes.Add(hint);
            NotifyModelChanged();
            // Debug.Print(_lines.Count.ToString());
        }
        
        /// <summary>
        /// clear
        /// </summary>
        public virtual void Clear()
        {
            _shapes.Clear();
            NotifyModelChanged();
        }
        
        /// <summary>
        /// draw
        /// </summary>
        /// <param name="graphics"></param>
        public virtual void DrawShapes(IGraphics graphics)
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
        public virtual void DrawHint(IGraphics graphics)
        {
            _hint.Draw(graphics);
            // Debug.Print("draw");
        }

        /// <summary>
        /// resize
        /// </summary>
        /// <param name="point"></param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void ResizeShape(Point point)
        {
            if (_selectIndex == -1)
            {
                return;
            }
            if (point.X < _shapes[_selectIndex].GetPoint1().X + 1)
            {
                point.X = _shapes[_selectIndex].GetPoint1().X + 1;
            }
            if (point.Y < _shapes[_selectIndex].GetPoint1().Y + 1)
            {
                point.Y = _shapes[_selectIndex].GetPoint1().Y + 1;
            }
            _shapes[_selectIndex].SetPoint2(point);
            _select.SetPoint2(point);
            NotifyModelChanged();
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
        
        /// <summary>
        /// get
        /// </summary>
        /// <returns></returns>
        public virtual int GetSelectIndex()
        {
            return _selectIndex;
        }
        
        /// <summary>
        /// set state
        /// </summary>
        /// <param name="modelState"></param>
        public virtual void SetModelState(ModelState modelState)
        {
            if (modelState == ModelState.Normal)
            {
                _context.SetState(new NormalState(this));
            }
            else if (modelState == ModelState.Drawing)
            {
                _context.SetState(new DrawingState(this));
            }
        }

        public enum ModelState
        {
            Normal,
            Drawing,
            Selected,
            Resize
        } 
        
        public enum Location
        {
            Left,
            Right,
            Top,
            Bottom,
            LeftTop,
            LeftBottom,
            RightTop,
            RightBottom,
            None
        }
        
        private readonly BindingList<Shape> _shapes = new BindingList<Shape>();
        private readonly ShapeFactory _shapeFactory;
        Shape _hint;
        private Shape _select;
        private Point _firstPoint = new Point(0, 0);
        private Point _lastPoint = new Point(0, 0);
        private int _selectIndex = -1;
        private Context _context;
    }
}