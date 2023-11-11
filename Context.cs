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
            _state.MouseDown(point, type);
        }

        public void MouseMove(Point point)
        {
            _state.MouseMove(point);
        }
        
        private IState _state;
    }
}