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
        void DrawLine(Point point1, Point point2);

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        void DrawRectangle(Point point1, Point point2);

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        void DrawCircle(Point point1, Point point2);
    }
}