using System.Drawing;

namespace PowerPoint
{
    public interface IGraphics
    {
        void ClearAll();
        void Draw(PointD point1, PointD point2);
    }
}