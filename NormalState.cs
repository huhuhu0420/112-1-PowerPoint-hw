using System;
using System.Diagnostics;
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
            _model.NotifyModelChanged();
            Debug.Print("notify");
        }
        
        public void MouseMove(Context context, Point point)
        {
        }
        
        public void MouseUp(Context context, Point point, ShapeType type)
        {
        }
    }
}