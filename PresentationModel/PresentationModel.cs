using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;

namespace PowerPoint.PresentationModel
{
    public class PresentationModel
    {
        Model _model = new Model();
        public event Model.ModelChangedEventHandler _modelChanged;
        private bool _isPressed = false;

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
        }

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="graphics"></param>
        public void Draw(System.Drawing.Graphics graphics)
        {
            var graphic = new WindowsFormsGraphicsAdaptor(graphics); 
            Debug.Print("draw1");
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
        public void PointerPressed(PointD point)
        {
            if (IsDrawing)
            {
                _model.PointerPressed(point, Type);
                _isPressed = true;
                
            }
        }
        
        /// <summary>
        /// move
        /// </summary>
        /// <param name="point"></param>
        public void PointerMoved(PointD point)
        {
            if (IsDrawing)
            {
                if (_isPressed)
                {
                    _model.PointerMoved(point);
                }
            }
        }

        /// <summary>
        /// release
        /// </summary>
        /// <param name="point"></param>
        public void PointerReleased(PointD point)
        {
            if (IsDrawing)
            {
                if (_isPressed) {
                    _isPressed = false;
                    _model.PointerReleased(point, Type);
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