using System.Drawing;

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
            _state.MouseDown(this, point, type);
        }

        /// <summary>
        /// move
        /// </summary>
        /// <param name="point"></param>
        public void MouseMove(Point point)
        {
            _state.MouseMove(this, point);
        }
        
        /// <summary>
        /// up
        /// </summary>
        /// <param name="point"></param>
        /// <param name="type"></param>
        public void MouseUp(Point point, ShapeType type)
        {
            _state.MouseUp(this, point, type);
        }
        
        private IState _state;
        private Model _model;
    }
}