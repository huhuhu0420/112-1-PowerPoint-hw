using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace PowerPoint.State
{
    public class ResizeState : IState
    {
        private readonly Model _model;
        
        public ResizeState (Model model, Model.Location location)
        {
            _model = model;
            _location = location;
        }

        /// <summary>
        /// down
        /// </summary>
        /// <param name="context"></param>
        /// <param name="point"></param>
        /// <param name="type"></param>
        public void MouseDown(Context context, Point point, ShapeType type)
        {
        }

        /// <summary>
        /// move
        /// </summary>
        /// <param name="context"></param>
        /// <param name="point"></param>
        /// <param name="isPressed"></param>
        public void MouseMove(Context context, Point point, bool isPressed)
        {
            if (isPressed)
            {
                _model.ResizeShape(point, _location);
                return;
            }
            if (_model.IsInShapeCorner(point) == Model.Location.None)
            {
                context.SetState(new SelectedState(_model));
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
        
        /// <summary>
        /// get
        /// </summary>
        /// <returns></returns>
        public Model.ModelState GetState()
        {
            return Model.ModelState.Resize;
        }
        
        /// <summary>
        /// get
        /// </summary>
        /// <param name="location"></param>
        public Model.Location GetLocation()
        {
            return _location;
        }
        
        /// <summary>
        /// get
        /// </summary>
        /// <returns></returns>
        public Cursor GetCursorForLocation()
        {
            if (_location == Model.Location.RightBottom || _location == Model.Location.LeftTop)
            {
                return Cursors.SizeNWSE;
            }
            else if (_location == Model.Location.LeftBottom || _location == Model.Location.RightTop)
            {
                return Cursors.SizeNESW;
            }
            else if (_location == Model.Location.Left || _location == Model.Location.Right)
            {
                return Cursors.SizeWE;
            }
            else if (_location == Model.Location.Top || _location == Model.Location.Bottom)
            {
                return Cursors.SizeNS;
            }
            return Cursors.Default;
        }

        readonly Model.Location _location;
    }
}