using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;

namespace PowerPoint.PresentationModel
{
    public class PresentationModel
    {
        Model _model;
        public event Model.ModelChangedEventHandler _modelChanged;
        private bool _isPressed = false;
        private ShapeType _shpaeType = ShapeType.CIRCLE;

        public PresentationModel(Model model)
        {
            this._model = model;
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
                _model.DrawHint(graphic, _shpaeType);
            }
        }

        public void PointerPressed(PointD point)
        {
            _model.PointerPressed(point);
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
                _model.PointerReleased(point, _shpaeType);
            }
        }

        public void Clear()
        {
            _isPressed = false;
            _model.Clear();
        }

    }
}