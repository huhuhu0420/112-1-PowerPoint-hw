using System.Drawing;

namespace PowerPoint.State
{
    public class NormalState : IState
    {
        Model _model;
        
        public NormalState(Model model)
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
            _model.SelectShape(point);
            if (_model.GetSelectIndex() != -1)
            {
                context.SetState(new SelectedState(_model));
            }
        }
        
        /// <summary>
        /// move
        /// </summary>
        /// <param name="context"></param>
        /// <param name="point"></param>
        public void MouseMove(Context context, Point point, bool isPressed)
        {
        }
        
        /// <summary>
        /// up
        /// </summary>
        /// <param name="context"></param>
        /// <param name="point"></param>
        /// <param name="type"></param>
        public void MouseUp(Context context, Point point, ShapeType type)
        {
        }
        
        /// <summary>
        /// draw
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="isPressed"></param>
        public void Draw(IGraphics graphics, bool isPressed)
        {
            _model.DrawShapes(graphics);
        }
        
        /// <summary>
        /// get
        /// </summary>
        /// <returns></returns>
        public Model.ModelState GetState()
        {
            return Model.ModelState.Normal;
        }
    }
}