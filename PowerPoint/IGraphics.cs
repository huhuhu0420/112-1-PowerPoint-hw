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
        void DrawLine(Pen pen, Point point1, Point point2);

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        void DrawLine(Pen pen, Point point1, Point point2, Line.LineType lineType);

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        void DrawRectangle(Pen pen, Point point1, Point point2);

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        void DrawCircle(Pen pen, Point point1, Point point2);
        
        /// <summary>
        /// draw
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        void DrawSelect(Pen pen, Point point1, Point point2);
    }
}