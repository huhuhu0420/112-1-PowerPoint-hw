using System.ComponentModel;
using System.Diagnostics;
using PowerPoint.Command;
using PowerPoint.State;

namespace PowerPoint
{
    public partial class Model
    {
        public event Pages.PagesChangedEventHandler _pagesChanged;
        
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
        public virtual void SetCanvasSize(int width, int height)
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
        
        /// <summary>
        /// set
        /// </summary>
        /// <param name="commandManager"></param>
        public virtual void SetCommandManager(CommandManager commandManager)
        {
            _commandManager = commandManager;
        }

        /// <summary>
        /// get
        /// </summary>
        /// <returns></returns>
        public virtual int GetPageIndex()
        {
            return _pageIndex;
        }
        
        /// <summary>
        /// set
        /// </summary>
        /// <param name="index"></param>
        public virtual void SetPageIndex(int index)
        {
            _pageIndex = index;
            _selectIndex = -1;
            _select = null;
            _shapes = _pages.GetPage(index);
            NotifyModelChanged();
        }
        
        /// <summary>
        /// add page
        /// </summary>
        public virtual void AddPage()
        {
            _pages.AddPage();
            AddPageCommand addPageCommand = new AddPageCommand(this, _pages.GetPage(_pages.GetPageCount() - 1), _pages.GetPageCount() - 1, _pageIndex);
            _commandManager.Execute(addPageCommand);
        }
        
        /// <summary>
        /// insert page
        /// </summary>
        /// <param name="index"></param>
        /// <param name="page"></param>
        public virtual void InsertPageByIndex(int index, BindingList<Shape> page)
        {
            _pages.AddPageByIndex(index, page);
        }
        
        /// <summary>
        /// delete
        /// </summary>
        public virtual void DeletePage()
        {
            _pages.RemovePageByIndex(_pageIndex);
            if (_pageIndex >= _pages.GetPageCount())
            {
                _pageIndex--;
            }
            SetPageIndex(_pageIndex);
        }
        
        /// <summary>
        /// get pages
        /// </summary>
        /// <returns></returns>
        public virtual Pages GetPages()
        {
            return _pages;
        }
        
        public virtual void HandlePageChanged(bool isAdd, int index)
        {
            if (_pagesChanged != null)
            {
                _pagesChanged(isAdd, index);
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
        
    }
}