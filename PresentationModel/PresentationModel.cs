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

        public PresentationModel()
        {
            _model._modelChanged += HandleModelChanged;
        }

        public void HandleModelChanged()
        {
            if (_modelChanged != null)
            {
                _modelChanged();
            }
        }

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

        public void PointerPressed(PointD point)
        {
            _model.PointerPressed(point, Type);
            _isPressed = true;
        }
        
        public void PointerMoved(PointD point)
        {
            if (_isPressed)
            {
                _model.PointerMoved(point);
            }
        }

        public void PointerReleased(PointD point)
        {
            if (_isPressed)
            {
                _isPressed = false;
                _model.PointerReleased(point, Type);
            }
        }

        public void Clear()
        {
            _isPressed = false;
            _model.Clear();
        }

        public BindingList<Shape> GetShapes()
        {
            return _model.GetShapes();
        }

        public void RemoveShape(int index)
        {
            _model.RemoveShape(index);
        }

        public void InsertShape(ShapeType type)
        {
            _model.InsertShape(type);
        }

    }
}