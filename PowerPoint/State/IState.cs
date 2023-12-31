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
        void MouseDown(Context context, PointF point, ShapeType type);
        
        /// <summary>
        /// move
        /// </summary>
        /// <param name="context"></param>
        /// <param name="point"></param>
        void MouseMove(Context context, PointF point, bool isPressed);
        
        /// <summary>
        /// up
        /// </summary>
        /// <param name="context"></param>
        /// <param name="point"></param>
        /// <param name="type"></param>
        void MouseUp(Context context, PointF point, ShapeType type);

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="isPressed"></param>
        void Draw(IGraphics graphics, bool isPressed);
        
        /// <summary>
        /// get
        /// </summary>
        /// <returns></returns>
        Model.ModelState GetState();
    }
}