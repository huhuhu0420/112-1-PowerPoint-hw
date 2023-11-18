using System.Drawing;

namespace PowerPoint.State
{
    public class DrawingState : IState
    {
        readonly Model _model;
        
        public DrawingState(Model model)
        {
            _model = model;
        }
        
        /// <summary>
        /// down
        /// </summary>
        /// <param name="context"></param>
        /// <param name="point"></param>
        /// <param name="type"></param>
        public void MouseDown(Context context, Point point, ShapeType type)
        {
            _model.PressedPointer(point, type);
            // Debug.Print("drawing");
        }
        
        /// <summary>
        /// move
        /// </summary>
        /// <param name="context"></param>
        /// <param name="point"></param>
        public void MouseMove(Context context, Point point, bool isPressed)
        {
            if (isPressed)
            {
                _model.MovedPointer(point);
            }
        }
        
        /// <summary>
        /// up
        /// </summary>
        /// <param name="context"></param>
        /// <param name="point"></param>
        /// <param name="type"></param>
        public void MouseUp(Context context, Point point, ShapeType type)
        {
            _model.ReleasedPointer(point, type);
            context.SetState(new NormalState(_model));
        }
        
        /// <summary>
        /// draw
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="isPressed"></param>
        public void Draw(IGraphics graphics, bool isPressed)
        {
            if (isPressed)
            {
                _model.DrawHint(graphics);
            }
            _model.DrawShapes(graphics);
        }
        
        /// <summary>
        /// get
        /// </summary>
        /// <returns></returns>
        public Model.ModelState GetState()
        {
            return Model.ModelState.Drawing;
        }
    }
}