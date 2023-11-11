using System.Drawing;

namespace PowerPoint
{
    public interface IState
    {
        void MouseDown(Point point, ShapeType type);
        void MouseMove(Point point);
    }
}