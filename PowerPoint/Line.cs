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

        public void HandleLineType()
        {
            int minX = _point1.X < _point2.X ? _point1.X : _point2.X;
            int minY = _point1.Y < _point2.Y ? _point1.Y : _point2.Y;
            int maxX = _point1.X > _point2.X ? _point1.X : _point2.X;
            int maxY = _point1.Y > _point2.Y ? _point1.Y : _point2.Y;
            if (minX == _point1.X && minY == _point1.Y)
            {
                _lineType = LineType.LeftTop;
            }
            else if (maxX == _point1.X && minY == _point1.Y)
            {
                _lineType = LineType.RightTop;
            }
            else if (minX == _point1.X && maxY == _point1.Y)
            {
                _lineType = LineType.LeftBottom;
            }
            else if (maxX == _point1.X && maxY == _point1.Y)
            {
                _lineType = LineType.RightBottom;
            }
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
        
        /// <summary>
        /// get line type
        /// </summary>
        /// <returns></returns>
        public LineType GetLineType()
        {
            return _lineType;
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