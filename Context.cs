using System.Drawing;
using System.Runtime.InteropServices;
using PowerPoint.State;

namespace PowerPoint
{
    public class Context
    {
        public Context(Model model)
        {
            _model = model;
            _state = new NormalState(_model);
        }
        
        /// <summary>
        /// set
        /// </summary>
        /// <param name="state"></param>
        public void SetState(IState state)
        {
            _state = state;
        }
        
        /// <summary>
        /// get
        /// </summary>
        /// <returns></returns>
        public IState GetState()
        {
            return _state;
        }
        
        /// <summary>
        /// down
        /// </summary>
        /// <param name="point"></param>
        /// <param name="type"></param>
        public void MouseDown(Point point, ShapeType type)
        {
            _isPressed = true;
            _state.MouseDown(this, point, type);
        }

        /// <summary>
        /// move
        /// </summary>
        /// <param name="point"></param>
        public void MouseMove(Point point)
        {
            _state.MouseMove(this, point, _isPressed);
        }
        
        /// <summary>
        /// up
        /// </summary>
        /// <param name="point"></param>
        /// <param name="type"></param>
        public void MouseUp(Point point, ShapeType type)
        {
            _isPressed = false;
            _state.MouseUp(this, point, type);
        }
        
        /// <summary>
        /// draw
        /// </summary>
        /// <param name="graphics"></param>
        public void Draw(IGraphics graphics)
        {
            _state.Draw(graphics, _isPressed);
        }
        
        private IState _state;
        private Model _model;
        private bool _isPressed = false;
    }
}