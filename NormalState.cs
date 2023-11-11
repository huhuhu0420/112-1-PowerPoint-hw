using System;
using System.Drawing;

namespace PowerPoint
{
    public class NormalState : IState
    {
        Model _model;
        
        public NormalState(Model model)
        {
            _model = model;
        }
        public void MouseDown(Context context, Point point, ShapeType type)
        {
            _model.SelectShape(point);
        }
        
        public void MouseMove(Context context, Point point)
        {
        }
        
        public void MouseUp(Context context, Point point, ShapeType type)
        {
        }
    }
}