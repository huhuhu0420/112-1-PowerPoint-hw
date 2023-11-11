using System.Drawing;

namespace PowerPoint
{
    public class Context
    {
        
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
        
        public void MouseDown(Point point, ShapeType type)
        {
            _state.MouseDown(this, point, type);
        }

        public void MouseMove(Point point)
        {
            _state.MouseMove(this, point);
        }
        
        public void MouseUp(Point point, ShapeType type)
        {
            _state.MouseUp(this, point, type);
        }
        
        private IState _state;
    }
}