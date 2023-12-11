using System.ComponentModel;
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
            _shapes.Add(_shapeFactory.CreateShape(type));
            NotifyModelChanged();
        }
        
        /// <summary>
        /// insert shape
        /// </summary>
        /// <param name="shape"></param>
        public virtual void InsertShapeByShape(Shape shape)
        {
            _shapes.Add(shape);
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
        
        public void SetCanvasSize(int width, int height)
        {
            _canvasWidth = width;
            _canvasHeight = height;
        }

        /// <summary>
        /// get shape
        /// </summary>
        /// <returns></returns>
        public virtual BindingList<Shape> GetShapes()
        {
            return _shapes;
        }
        
    }
}