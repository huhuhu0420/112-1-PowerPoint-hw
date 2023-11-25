using System.Diagnostics;
using System.Drawing;

namespace PowerPoint
{
    public class Line : Shape
    {
        public Line(Point point1, Point point2) : base(point1, point2)
        { 
            _shapeName = Constant.LINE;
            Type = ShapeType.LINE;
            _point1 = point1;
            _point2 = point2;
            HandleLineType();
            if (_point1.X > _point2.X)
            {
                var temp = point1.X;
                _point1.X = _point2.X;
                _point2.X = temp;
            }
            if (_point1.Y > _point2.Y)
            {
                var temp = point1.Y;
                _point1.Y = _point2.Y;
                _point2.Y = temp;
            }
            // Debug.Print(_lineType.ToString());
        }

        public Line()
        {
            _shapeName = Constant.LINE;
            Type = ShapeType.LINE;
            _lineType = LineType.None;
        }

        /// <summary>
        /// type
        /// </summary>
        public void HandleLineType()
        {
            _lineType = GetLineType();
        }

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(IGraphics graphics)
        {
            Pen pen = new Pen(Color.DodgerBlue, Constant.THREE);
            if (_lineType == LineType.None)
            {
                graphics.DrawLine(pen,_point1, _point2);
            }
            else
            {
                graphics.DrawLine(pen,_point1, _point2, _lineType);
            }
        }
        
        /// <summary>
        /// set line type
        /// </summary>
        /// <param name="lineType"></param>
        public void SetLineType(LineType lineType)
        {
            _lineType = lineType;
        }
        
        public enum LineType
        {
            LeftTop,
            RightTop,
            LeftBottom,
            RightBottom,
            None
        }
        
        private LineType _lineType;
    }
}