using System.Diagnostics;
using System.Drawing;

namespace PowerPoint.Command
{
    public class ResizeCommand : ICommand
    {
        public ResizeCommand(Model model, Shape shape, int index, PointF point1, PointF point2)
        {
            _model = model;
            _shape = shape;
            _index = index;
            _newPoint1 = point1;
            _newPoint2 = point2;
            _point1 = shape.GetPoint1();
            _point2 = shape.GetPoint2();
            Debug.Print(_point1.ToString());
            Debug.Print(_point2.ToString());
            Debug.Print(_newPoint1.ToString());
            Debug.Print(_newPoint2.ToString());
        }
        
        public void Execute()
        {
            var shapes = _model.GetShapes();
            shapes[_index].SetPoint1(_newPoint1);
            shapes[_index].SetPoint2(_newPoint2);
        }
        
        public void Undo()
        {
            var shapes = _model.GetShapes();
            shapes[_index].SetPoint1(_point1);
            shapes[_index].SetPoint2(_point2);
            Debug.Print(_point1.ToString());
            Debug.Print(_point2.ToString());
        }
        
        readonly Model _model;
        readonly Shape _shape;
        readonly int _index;
        readonly PointF _newPoint1;
        readonly PointF _newPoint2;
        readonly PointF _point1;
        readonly PointF _point2;
    }
}