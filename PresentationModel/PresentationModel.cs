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
        private ModelState _modelState = ModelState.Normal;
        readonly Model _model = new Model();
        
        public enum ModelState
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
        
        /// <summary>
        /// set
        /// </summary>
        /// <param name="state"></param>
        public void SetModelState(ModelState state)
        {
            _model.SetModelState(state);
            _modelState = state;
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
        /// delete
        /// </summary>
        public void DeleteShape()
        {
            _model.RemoveShape();
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
            _model.RemoveShapeByIndex(index);
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