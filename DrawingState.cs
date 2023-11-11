using System.Diagnostics;
using System.Drawing;

namespace PowerPoint
{
    public class DrawingState : IState
    {
        Model _model;
        
        public DrawingState(Model model)
        {
            _model = model;
        }
        public void MouseDown(Context context, Point point, ShapeType type)
        {
            _model.PressedPointer(point, type);
            Debug.Print("drawing");
        }
        
        public void MouseMove(Context context, Point point)
        {
            _model.MovedPointer(point);
        }
        
        public void MouseUp(Context context, Point point, ShapeType type)
        {
            _model.ReleasedPointer(point, type);
            context.SetState(new NormalState(_model));
        }
    }
}