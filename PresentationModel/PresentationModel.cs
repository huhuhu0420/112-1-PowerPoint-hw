using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace PowerPoint.PresentationModel
{
    public class PresentationModel
    {   
        public event Model.ModelChangedEventHandler _modelChanged;
        private bool _isPressed = false;
        private State _state = State.Normal;
        readonly Model _model = new Model();
        
        public enum State
        {
            Normal,
            Drawing
        } 

        public ShapeType Type
        {
            get;
            set;
        }

        public bool IsDrawing
        {
            get;
            set;
        }
        
        public void SetState(State state)
        {
            _state = state;
        }

        /// <summary>
        /// model
        /// </summary>
        public PresentationModel()
        {
            _model._modelChanged += HandleModelChanged;
        }

        /// <summary>
        /// model change
        /// </summary>
        public void HandleModelChanged()
        {
            if (_modelChanged != null)
            {
                _modelChanged();
            }
            //ModelChanged?.Invoke();
        }

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="graphics"></param>
        public void Draw(System.Drawing.Graphics graphics)
        {
            var graphic = new WindowsFormsGraphicsAdaptor(graphics); 
            // Debug.Print("draw1");
            _model.Draw(graphic);
            if (_isPressed)
            {
                _model.DrawHint(graphic);
            }
        }

        /// <summary>
        /// press
        /// </summary>
        /// <param name="point"></param>
        public void PressedPointer(Point point)
        {
            if (IsDrawing)
            {
                _model.PressedPointer(point, Type);
                _isPressed = true;
            }
            else
            {
                _model.SelectShape(point);
            }
        }
        
        /// <summary>
        /// move
        /// </summary>
        /// <param name="point"></param>
        public void MovedPointer(Point point)
        {
            if (IsDrawing)
            {
                if (_isPressed)
                {
                    _model.MovedPointer(point);
                }
            }
        }

        /// <summary>
        /// release
        /// </summary>
        /// <param name="point"></param>
        public void ReleasedPointer(Point point)
        {
            if (IsDrawing)
            {
                if (_isPressed) 
                {
                    _isPressed = false;
                    _model.ReleasedPointer(point, Type);
                }
                IsDrawing = false;
            }
        }

        /// <summary>
        /// clear
        /// </summary>
        public void Clear()
        {
            _isPressed = false;
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
            _model.RemoveShape(index);
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
        /// canvas
        /// </summary>
        /// <param name="button"></param>
        /// <param name="cursor"></param>
        public void HandleCanvasRelease(ToolStripButton []button)
        {
            foreach (var aButton in button)
            {
                aButton.Checked = false;
            }
            
        }

        /// <summary>
        /// btn
        /// </summary>
        /// <param name="button"></param>
        /// <param name="index"></param>
        public void HandleButtonClick(ToolStripButton []button, int index)
        {
            foreach (var aButton in button)
            {
                aButton.Checked = false;
            }
            button[index].Checked = true;
        }
    }
}