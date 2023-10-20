using System.Drawing;

namespace PowerPoint
{
    public interface IGraphics
    {
        /// <summary>
        /// clear
        /// </summary>
        void ClearAll();

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <param name="type"></param>
        void Draw(PointDouble point1, PointDouble point2, ShapeType type);
    }
}