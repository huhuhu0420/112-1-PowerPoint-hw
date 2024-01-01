using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using PowerPoint.Command;
using PowerPoint.State;

namespace PowerPoint.PresentationModel
{
    public partial class PresentationModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning disable IDE1006 // Naming Styles
        public event Model.ModelChangedEventHandler _modelChanged;
#pragma warning restore IDE1006 // Naming Styles
        public delegate void CursorChangedEventHandler(Cursor cursor);
#pragma warning disable IDE1006 // Naming Styles
        public event CursorChangedEventHandler _cursorChanged;
#pragma warning restore IDE1006 // Naming Styles
#pragma warning disable IDE1006 // Naming Styles
        public event CommandManager.HandleUndoRedoHistoryEventHandler _undoRedoHistoryChanged;
#pragma warning restore IDE1006 // Naming Styles
        public event Pages.PagesChangedEventHandler _pagesChanged;
        readonly Model _model;

        private WindowsFormsGraphicsAdaptor _graphic;

        public PresentationModel(Model model)
        {
            _model = model;
            _model._modelChanged += HandleModelChanged;
            _model._stateChanged += HandleStateChange;
            _model._pagesChanged += HandlePagesChanged;
            _model._undoRedoHistoryChanged += HandleUndoRedoHistoryChanged;
            _isButtonChecked[(int)ShapeType.ARROW] = true;
        }

        public void HandlePagesChanged(bool isadd, int index)
        {
            if (_pagesChanged != null)
            {
#pragma warning disable IDE1005 // Delegate invocation can be simplified.
                _pagesChanged(isadd, index);
#pragma warning restore IDE1005 // Delegate invocation can be simplified.
            }
        }

        /// <summary>
        /// handle
        /// </summary>
        /// <param name="isUndo"></param>
        /// <param name="isRedo"></param>
        public void HandleUndoRedoHistoryChanged(bool isUndo, bool isRedo)
        {
            if (_undoRedoHistoryChanged != null)
            {
#pragma warning disable IDE1005 // Delegate invocation can be simplified.
                _undoRedoHistoryChanged(isUndo, isRedo);
#pragma warning restore IDE1005 // Delegate invocation can be simplified.
            }
        }

        public ShapeType Type
        {
            get;
            set;
        }
        
        /// <summary>
        /// set
        /// </summary>
        /// <param name="state"></param>
        public void SetModelState(Model.ModelState state)
        {
            _model.SetModelState(state);
        }
        
        /// <summary>
        /// model change
        /// </summary>
        public void HandleModelChanged()
        {
            if (_modelChanged != null)
            {
#pragma warning disable IDE1005 // Delegate invocation can be simplified.
                _modelChanged();
#pragma warning restore IDE1005 // Delegate invocation can be simplified.
            }
            //ModelChanged?.Invoke();
        }
        
        /// <summary>
        /// handle prperty changed
        /// </summary>
        public void HandlePropertyChanged()
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Constant.IS_LINE_CHECKED));
                PropertyChanged(this, new PropertyChangedEventArgs(Constant.IS_RECTANGLE_CHECKED));
                PropertyChanged(this, new PropertyChangedEventArgs(Constant.IS_CIRCLE_CHECKED));
                PropertyChanged(this, new PropertyChangedEventArgs(Constant.IS_MOUSE_CHECKED));
                PropertyChanged(this, new PropertyChangedEventArgs(Constant.IS_SAVE_BUTTON_ENABLED));
            }
            if (!_isButtonChecked[(int)ShapeType.ARROW])
            {
                SetModelState(Model.ModelState.Drawing);
            }
        }
        
        /// <summary>
        /// cursor
        /// </summary>
        /// <param name="state"></param>
        public void HandleStateChange(IState state)
        {
            if (state is SelectedState)
            {
                _cursorChanged(Cursors.Arrow);
            }
            else if (state is NormalState)
            {
                _cursorChanged(Cursors.Arrow);
            }
            else if (state is DrawingState)
            {
                _cursorChanged(Cursors.Cross);
            }
            else if (state is ResizeState)
            {
                _cursorChanged(((ResizeState)state).GetCursorForLocation());
            }
        }
        
        /// <summary>
        /// delete
        /// </summary>
        public bool DeleteShape()
        {
            if (_model.GetSelectIndex() != -1)
            {
                _model.HandleRemoveShape(-1);
                return true;
            }
            return false;
        }

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="graphics"></param>
        public void Draw(System.Drawing.Graphics graphics)
        {
            _graphic = new WindowsFormsGraphicsAdaptor(graphics); 
            // Debug.Print("draw1");
            _model.Draw(_graphic);
        }

        /// <summary>
        /// press
        /// </summary>
        /// <param name="point"></param>
        public void PressedPointer(PointF point)
        {
            _model.MouseDown(point, Type);
        }
        
        /// <summary>
        /// move
        /// </summary>
        /// <param name="point"></param>
        public void MovedPointer(PointF point)
        {
            _model.MouseMove(point);
        }

        /// <summary>
        /// release
        /// </summary>
        /// <param name="point"></param>
        public void ReleasedPointer(PointF point)
        {
            _model.MouseUp(point, Type);
            for (int i = 0; i < _isButtonChecked.Length; i++)
            {
                _isButtonChecked[i] = false;
            }
            _isButtonChecked[(int)ShapeType.ARROW] = true;
            HandlePropertyChanged();
        }

        /// <summary>
        /// clear
        /// </summary>
        public void Clear()
        {
            _model.Clear();
        }

        /// <summary>
        /// get 
        /// </summary>
        /// <returns></returns>
        public BindingList<Shape> GetShapes()
        {
            return _model.GetShapes();
        }

        /// <summary>
        /// remove shape
        /// </summary>
        /// <param name="index"></param>
        public void RemoveShape(int index)
        {
            _model.HandleRemoveShape(index);
        }

        /// <summary>
        /// insert
        /// </summary>
        /// <param name="type"></param>
        public void InsertShape(ShapeType type)
        {
            _model.InsertShape(type);
        }

        /// <summary>
        /// btn
        /// </summary>
        /// <param name="button"></param>
        /// <param name="index"></param>
        public void HandleButtonClick(int index)
        {
            Type = (ShapeType)index;
            SetModelState(Model.ModelState.Drawing);
            if (index == (int)ShapeType.ARROW)
            {
                SetModelState(Model.ModelState.Normal);
            }
            for (int i = 0; i < _isButtonChecked.Length; i++)
            {
                _isButtonChecked[i] = false;
            }
            _isButtonChecked[index] = true;
            HandlePropertyChanged();
        }
        
        /// <summary>
        /// undo
        /// </summary>
        public void Undo()
        {
            _model.Undo();
        }
        
        /// <summary>
        /// redo
        /// </summary>
        public void Redo()
        {
            _model.Redo();
        }
        
        /// <summary>
        /// set
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetCanvasSize(int width, int height)
        {
            _model.SetCanvasSize(width, height);
        }

        /// <summary>
        /// set
        /// </summary>
        /// <param name="index"></param>
        public void SetPageIndex(int index)
        {
            _model.SetPageIndex(index);
        }
        
        /// <summary>
        /// add
        /// </summary>
        public void AddPage()
        {
            _model.AddPage();
        }

        readonly bool[] _isButtonChecked = { false, false, false, false };
        private bool _isSaveButtonEnabled = true;
        private bool _isLoadButtonEnabled = true;
    }
}