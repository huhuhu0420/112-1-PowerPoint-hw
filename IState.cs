using System.Drawing;

namespace PowerPoint
{
    public interface IState
    {
        /// <summary>
        /// down
        /// </summary>
        /// <param name="context"></param>
        /// <param name="point"></param>
        /// <param name="type"></param>
        void MouseDown(Context context, Point point, ShapeType type);
        
        /// <summary>
        /// move
        /// </summary>
        /// <param name="context"></param>
        /// <param name="point"></param>
        void MouseMove(Context context, Point point);
        
        /// <summary>
        /// up
        /// </summary>
        /// <param name="context"></param>
        /// <param name="point"></param>
        /// <param name="type"></param>
        void MouseUp(Context context, Point point, ShapeType type);

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="isPressed"></param>
        void Draw(IGraphics graphics, bool isPressed);
    }
}