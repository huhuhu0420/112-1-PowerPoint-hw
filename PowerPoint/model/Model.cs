using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using PowerPoint.Command;
using PowerPoint.State;

namespace PowerPoint
{
    public partial class Model
    {
        public delegate void ModelChangedEventHandler();
#pragma warning disable IDE1006 // Naming Styles
        public event ModelChangedEventHandler _modelChanged;
#pragma warning restore IDE1006 // Naming Styles
#pragma warning disable IDE1006 // Naming Styles
        public event Context.StateChangedEventHandler _stateChanged;
#pragma warning restore IDE1006 // Naming Styles

        public Model()
        {
            _shapeFactory = new ShapeFactory();
            _hint = new Shape();
            _firstPoint = new PointF(0, 0);
            _lastPoint = new PointF(0, 0);
            _resizeShape = new Dictionary<Location, Action<PointF>>();
            _context = new Context(this);
            _shapes = new BindingList<Shape>();
            _commandManager = new CommandManager();
            _commandManager._undoRedoHistoryChanged += SetUndoRedoHistory;
            _pages = new Pages();
            _pages.AddPage();
            _pages._pagesChanged += HandlePageChanged;
            _shapes = _pages.GetPage(0);
            _service = new GoogleDriveService(APPLICATION_NAME, CLIENT_SECRET_FILE_NAME);
            _solutionPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constant.MANY_DIRECTORY));
            _filePath = Path.Combine(_solutionPath, Constant.PROJECT_NAME, Constant.BINARY_DIRECTORY, Constant.DIRECTORY, Constant.FILE_NAME);
            InitializeResizeShape();
        }

        /// <summary>
        /// init
        /// </summary>
        public void InitializeResizeShape()
        {
            _resizeShape[Location.Left] = ResizeShapeLeft;
            _resizeShape[Location.Right] = ResizeShapeRight;
            _resizeShape[Location.Top] = ResizeShapeTop;
            _resizeShape[Location.Bottom] = ResizeShapeBottom;
            _resizeShape[Location.LeftTop] = ResizeShapeLeftTop;
            _resizeShape[Location.LeftBottom] = ResizeShapeLeftBottom;
            _resizeShape[Location.RightTop] = ResizeShapeRightTop;
            _resizeShape[Location.RightBottom] = ResizeShapeRightBottom;
        }
        
        /// <summary>
        /// state changed
        /// </summary>
        public void HandleStateChanged(IState state)
        {
            if (_stateChanged != null)
            {
#pragma warning disable IDE1005 // Delegate invocation can be simplified.
                _stateChanged(state);
#pragma warning restore IDE1005 // Delegate invocation can be simplified.
            }
        }

        /// <summary>
        /// mouse down
        /// </summary>
        /// <param name="point"></param>
        /// <param name="type"></param>
        public virtual void MouseDown(PointF point, ShapeType type)
        {
            _context.MouseDown(point, type);
            _firstPoint = point;
        }

        /// <summary>
        /// move
        /// </summary>
        /// <param name="point"></param>
        public virtual void MouseMove(PointF point)
        {
            _context.MouseMove(point);
            _lastPoint = point;
        }
        
        /// <summary>
        /// up
        /// </summary>
        /// <param name="point"></param>
        /// <param name="type"></param>
        public virtual void MouseUp(PointF point, ShapeType type)
        {
            _context.MouseUp(point, type);
            if (_context.GetState() == Model.ModelState.Selected && _firstPoint != _lastPoint)
            {
                HandleMoveShape(_selectIndex, new SizeF(_lastPoint.X - _firstPoint.X, _lastPoint.Y - _firstPoint.Y));
                // Debug.Print("move");
            }
            if (_context.GetState() == Model.ModelState.Resize && _firstPoint != _lastPoint)
            {
                HandleResizeShape(_selectIndex, _tempShape);
            }
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
        public virtual void PressedPointer(PointF point, ShapeType type)
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
        public virtual bool IsInShape(PointF point, int index)
        {
            if (_shapes[index].IsInShape(point))
            {
                // Debug.Print(index.ToString());
                _select = _shapeFactory.CreateShape(_shapes[index].GetShapeType(), _shapes[index].GetPoint1(), _shapes[index].GetPoint2());
                _lastPoint = point;
                _selectIndex = index;
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// isin
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual Location IsInShapeCorner(PointF point)
        {
            if (_selectIndex == -1)
            {
                return Location.None;
            }
            Location location = _shapes[_selectIndex].IsInShapeCorner(point);
            _context.SetLocation(location);
            return location;
        }

        /// <summary>
        /// select
        /// </summary>
        /// <param name="point"></param>
        public virtual void SelectShape(PointF point)
        {
            bool isSelect = false;
            for (int i = _shapes.Count - 1; i >= 0; i --)
            {
                isSelect = IsInShape(point, i);
                if (isSelect)
                {
                    _selectIndex = i;
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
        public virtual void MovedPointer(PointF point)
        {
            _hint.SetPoint2(point);
            NotifyModelChanged();
        }

        /// <summary>
        /// move
        /// </summary>
        /// <param name="point"></param>
        public virtual void MoveShape(PointF point)
        {
            SizeF bias = new SizeF(point.X - _lastPoint.X, point.Y - _lastPoint.Y);
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
        /// move
        /// </summary>
        /// <param name="bias"></param>
        public virtual void MoveShapeByBias(SizeF bias, int index)
        {
            _shapes[index].SetPoint1(_shapes[index].GetPoint1() + bias);
            _shapes[index].SetPoint2(_shapes[index].GetPoint2() + bias);
            if (_selectIndex == index && _select != null)
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
        public virtual void ReleasedPointer(PointF point, ShapeType type)
        {
            Shape hint = _shapeFactory.CreateShape(type, _firstPoint, point);
            _shapes.Add(hint);
            HandleDrawShape(hint);
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
        /// drae
        /// </summary>
        /// <param name="graphics"></param>
        public virtual void DrawHint(IGraphics graphics)
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
#pragma warning disable IDE1005 // Delegate invocation can be simplified.
                _modelChanged();
#pragma warning restore IDE1005 // Delegate invocation can be simplified.
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
        
        private BindingList<Shape> _shapes;
        private Pages _pages;
        private int _pageIndex = 0;
        
        private readonly ShapeFactory _shapeFactory;
        Shape _hint;
        private Shape _select;
        private PointF _firstPoint = new PointF(0, 0);
        private PointF _lastPoint = new PointF(0, 0);
        private int _selectIndex = -1;
        private Context _context;
        private readonly Dictionary<Model.Location, Action<PointF>> _resizeShape;
        private CommandManager _commandManager;
        private int _canvasWidth;
        const string APPLICATION_NAME = "DrawAnyWhere";
        const string CLIENT_SECRET_FILE_NAME = "clientSecret.json";
        private Shape _tempShape;
    }
}