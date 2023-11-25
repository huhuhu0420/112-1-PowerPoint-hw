using System.Drawing;
using System.Runtime.InteropServices;
using PowerPoint.State;

namespace PowerPoint
{
    public class Context
    {
        public delegate void StateChangedEventHandler(IState state);
        public event StateChangedEventHandler _stateChanged;
        public Context(Model model)
        {
            _model = model;
            _state = new NormalState(_model);
        }
        
        /// <summary>
        /// set
        /// </summary>
        /// <param name="state"></param>
        public virtual void SetState(IState state)
        {
            _state = state;
            if (_state is ResizeState)
            {
                ((ResizeState)_state).SetLocation(_location);
            }
            if (_stateChanged != null)
            {
                _stateChanged(state);
            }
        }
        
        /// <summary>
        /// down
        /// </summary>
        /// <param name="point"></param>
        /// <param name="type"></param>
        public virtual void MouseDown(Point point, ShapeType type)
        {
            _isPressed = true;
            _state.MouseDown(this, point, type);
        }

        /// <summary>
        /// move
        /// </summary>
        /// <param name="point"></param>
        public virtual void MouseMove(Point point)
        {
            _state.MouseMove(this, point, _isPressed);
        }
        
        /// <summary>
        /// up
        /// </summary>
        /// <param name="point"></param>
        /// <param name="type"></param>
        public virtual void MouseUp(Point point, ShapeType type)
        {
            _isPressed = false;
            _state.MouseUp(this, point, type);
        }
        
        /// <summary>
        /// draw
        /// </summary>
        /// <param name="graphics"></param>
        public virtual void Draw(IGraphics graphics)
        {
            _state.Draw(graphics, _isPressed);
        }
        
        /// <summary>
        /// set
        /// </summary>
        /// <param name="location"></param>
        public virtual void SetLocation(Model.Location location)
        {
            _location = location;
        }
        
        private IState _state;
        private Model _model;
        private bool _isPressed = false;
        private Model.Location _location;
    }
}