using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using PowerPoint.State;

namespace PowerPoint.PresentationModel
{
    public class PresentationModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning disable IDE1006 // Naming Styles
        public event Model.ModelChangedEventHandler _modelChanged;
#pragma warning restore IDE1006 // Naming Styles
        public delegate void CursorChangedEventHandler(Cursor cursor);
#pragma warning disable IDE1006 // Naming Styles
        public event CursorChangedEventHandler _cursorChanged;
#pragma warning restore IDE1006 // Naming Styles
        readonly Model _model;

        private WindowsFormsGraphicsAdaptor _graphic;

        public PresentationModel(Model model)
        {
            _model = model;
            _model._modelChanged += HandleModelChanged;
            _model._stateChanged += HandleStateChange;
            _isButtonChecked[(int)ShapeType.ARROW] = true;
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
        public void DeleteShape()
        {
            _model.HandleRemoveShape(-1);
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
        public void PressedPointer(Point point)
        {
            _model.MouseDown(point, Type);
        }
        
        /// <summary>
        /// move
        /// </summary>
        /// <param name="point"></param>
        public void MovedPointer(Point point)
        {
            _model.MouseMove(point);
        }

        /// <summary>
        /// release
        /// </summary>
        /// <param name="point"></param>
        public void ReleasedPointer(Point point)
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
            _model.HandleInsertShape(type);
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
        
        public bool IsLineButtonChecked
        {
            get 
            {
                return _isButtonChecked[(int)ShapeType.LINE]; 
            }
        }
        
        public bool IsRectangleButtonChecked
        {
            get 
            { 
                return _isButtonChecked[(int)ShapeType.RECTANGLE]; 
            }
        }
        
        public bool IsCircleButtonChecked
        {
            get 
            {
                return _isButtonChecked[(int)ShapeType.CIRCLE]; 
            }
        }
        
        public bool IsMouseButtonChecked
        {
            get
            {
                return _isButtonChecked[(int)ShapeType.ARROW]; 
            }
        }

        readonly bool[] _isButtonChecked = { false, false, false, false };
    }
}