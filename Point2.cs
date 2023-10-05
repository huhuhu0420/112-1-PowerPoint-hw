using System.Drawing;

namespace PowerPoint
{
    public struct Point2
    {
        public Point2(Point topLeft, Point downRight)
        {
            TopLeft = topLeft;
            DownRight = downRight;
        }
        public Point TopLeft
        { 
            get; set; 
        }
        public Point DownRight 
        { 
            get; set;
        }
    }
}