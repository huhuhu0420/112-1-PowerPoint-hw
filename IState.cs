using System.Drawing;

namespace PowerPoint
{
    public interface IState
    {
        void MouseDown(Context context, Point point, ShapeType type);
        void MouseMove(Context context, Point point);
        void MouseUp(Context context, Point point, ShapeType type);
    }
}