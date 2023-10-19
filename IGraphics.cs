using System.Drawing;

namespace PowerPoint
{
    public interface IGraphics
    {
        void ClearAll();
        void DrawLine(PointD point1, PointD point2);
    }
}