using System.Diagnostics;
using System.Drawing;

namespace PowerPoint.State
{
    public class ResizeState : IState
    {
        private readonly Model _model;
        
        public ResizeState (Model model)
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
            if (_model.GetSelectIndex() == -1)
            {
                context.SetState(new NormalState(_model));
            }
        }

        /// <summary>
        /// move
        /// </summary>
        /// <param name="context"></param>
        /// <param name="point"></param>
        public void MouseMove(Context context, Point point, bool isPressed)
        {
            if (!_model.IsInShapeCorner(point))
            {
                context.SetState(new SelectedState(_model));
                Debug.Print("normal");
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
    }
}