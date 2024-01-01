using System.Diagnostics;
using System.Drawing;

namespace PowerPoint.Command
{
    public class ResizeCommand : ICommand
    {
        public ResizeCommand(Model model, Shape shape, Shape newShape, int index)
        {
            _model = model;
            _shape = _shapeFactory.CreateShape(shape.GetShapeType(), shape.GetPoint1(), shape.GetPoint2());
            _newShape = _shapeFactory.CreateShape(newShape.GetShapeType(), newShape.GetPoint1(), newShape.GetPoint2());
            _index = index;
        }
        
        // exe
        public void Execute()
        {
            _model.GetShapes()[_index] = _newShape;
            _model.SelectShape(new PointF(0,0));
            _model.NotifyModelChanged();
        }
        
        // undo
        public void Undo()
        {
            _model.GetShapes()[_index] = _shape;
            _model.SetSelectNull();
            _model.NotifyModelChanged();
        }
        
        readonly Model _model;
        readonly int _index;
        readonly Shape _shape;
        readonly Shape _newShape;
        readonly ShapeFactory _shapeFactory = new ShapeFactory();
    }
}