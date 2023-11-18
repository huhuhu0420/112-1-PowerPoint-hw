using System.Drawing;

namespace PowerPoint
{
    public class Line : Shape
    {
        public Line(Point point1, Point point2) : base(point1, point2)
        { 
            _shapeName = Constant.LINE;
            Type = ShapeType.LINE;
                    }

        public Line()
        {
            _shapeName = Constant.LINE;
            Type = ShapeType.LINE;
        }

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(IGraphics graphics)
        {
            Pen pen = new Pen(Color.DodgerBlue, Constant.THREE);
            graphics.DrawLine(pen,_point1, _point2);
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
            RightTop
        }
        
        private LineType _lineType;
    }
}