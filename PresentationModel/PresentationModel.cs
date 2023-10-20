using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;

namespace PowerPoint.PresentationModel
{
    public class PresentationModel
    {   
        public event Model.ModelChangedEventHandler ModelChanged;
        private bool _isPressed = false;
        readonly Model _model = new Model();

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

        /// <summary>
        /// model
        /// </summary>
        public PresentationModel()
        {
            _model.ModelChanged += HandleModelChanged;
        }

        /// <summary>
        /// model change
        /// </summary>
        public void HandleModelChanged()
        {
            ModelChanged?.Invoke();
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
        public void PressedPointer(PointDouble point)
        {
            if (IsDrawing)
            {
                _model.PressedPointer(point, Type);
                _isPressed = true;
                
            }
        }
        
        /// <summary>
        /// move
        /// </summary>
        /// <param name="point"></param>
        public void MovedPointer(PointDouble point)
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
        public void ReleasedPointer(PointDouble point)
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

    }
}