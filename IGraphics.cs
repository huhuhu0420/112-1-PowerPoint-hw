using System.Drawing;

namespace PowerPoint
{
    public interface IGraphics
    {
        void ClearAll();
        void Draw(PointF point1, PointF point2);
    }
}