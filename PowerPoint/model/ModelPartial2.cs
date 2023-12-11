using System.ComponentModel;
using System.Diagnostics;
using PowerPoint.Command;

namespace PowerPoint
{
    public partial class Model
    {
        /// <summary>
        /// init
        /// </summary>
        /// <param name="context"></param>
        public void SetContext(Context context)
        {
            _context = context;
            _context._stateChanged += HandleStateChanged;
        }
        
        /// <summary>
        /// insert shape
        /// </summary>
        public virtual void InsertShape(ShapeType type)
        {
            var shape = _shapeFactory.CreateShape(type);
            _shapes.Add(shape);
            HandleInsertShape(shape);
            NotifyModelChanged();
        }
        
        /// <summary>
        /// insert shape
        /// </summary>
        /// <param name="shape"></param>
        public virtual void InsertShapeByShape(Shape shape, int index)
        {
            _shapes.Insert(index, shape);
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
        /// SET
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetCanvasSize(int width, int height)
        {
            foreach (var shape in _shapes)
            {
                shape.Scale((float)width / (float)_canvasWidth);
            }
            if (_select != null)
            {
                _select.Scale((float)width / (float)_canvasWidth);
            }
            _canvasWidth = width;
            _canvasHeight = height;
            _shapeFactory.SetCanvasSize(width, height);
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
        
    }
}