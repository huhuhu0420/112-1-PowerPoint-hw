using System.Drawing;

namespace PowerPoint
{
    public partial class Shape
    {
        // isin shape
        public bool IsInShapeRightBottom(PointF point)
        {
            float maxX = _point1.X > _point2.X ? _point1.X : _point2.X;
            float maxY = _point1.Y > _point2.Y ? _point1.Y : _point2.Y;
            if (point.X >= maxX - Constant.FOUR && point.X <= maxX + Constant.FOUR && point.Y >= maxY - Constant.FOUR && point.Y <= maxY + Constant.FOUR)
            {
                return true;
            }

            return false;
        }        
        
        // isin shape
        public bool IsInShapeLeftTop(PointF point)
        {
            float minX = _point1.X < _point2.X ? _point1.X : _point2.X;
            float minY = _point1.Y < _point2.Y ? _point1.Y : _point2.Y;
            if (point.X >= minX - Constant.FOUR && point.X <= minX + Constant.FOUR && point.Y >= minY - Constant.FOUR && point.Y <= minY + Constant.FOUR)
            {
                return true;
            }

            return false;
        }
                
        // isin shape
        public bool IsInShapeRightTop(PointF point)
        {
            float maxX = _point1.X > _point2.X ? _point1.X : _point2.X;
            float minY = _point1.Y < _point2.Y ? _point1.Y : _point2.Y;
            if (point.X >= maxX - Constant.FOUR && point.X <= maxX + Constant.FOUR && point.Y >= minY - Constant.FOUR && point.Y <= minY + Constant.FOUR)
            {
                return true;
            }

            return false;
        }
        
        // isin shape
        public bool IsInShapeLeftBottom(PointF point)
        {
            float minX = _point1.X < _point2.X ? _point1.X : _point2.X;
            float maxY = _point1.Y > _point2.Y ? _point1.Y : _point2.Y;
            if (point.X >= minX - Constant.FOUR && point.X <= minX + Constant.FOUR && point.Y >= maxY - Constant.FOUR && point.Y <= maxY + Constant.FOUR)
            {
                return true;
            }

            return false;
        }
        
        // isin shape
        public bool IsInShapeLeft(PointF point)
        {
            float minX = _point1.X < _point2.X ? _point1.X : _point2.X;
            float minY = _point1.Y < _point2.Y ? _point1.Y : _point2.Y;
            float maxY = _point1.Y > _point2.Y ? _point1.Y : _point2.Y;
            float middleY = (minY + maxY) / Constant.TWO;
            if (point.X >= minX - Constant.FOUR && point.X <= minX + Constant.FOUR && point.Y >= middleY - Constant.FOUR && point.Y <= middleY + Constant.FOUR)
            {
                return true;
            }

            return false;
        }
        
        // isin shape
        public bool IsInShapeRight(PointF point)
        {
            float maxX = _point1.X > _point2.X ? _point1.X : _point2.X;
            float minY = _point1.Y < _point2.Y ? _point1.Y : _point2.Y;
            float maxY = _point1.Y > _point2.Y ? _point1.Y : _point2.Y;
            float middleY = (minY + maxY) / Constant.TWO;
            if (point.X >= maxX - Constant.FOUR && point.X <= maxX + Constant.FOUR && point.Y >= middleY - Constant.FOUR && point.Y <= middleY + Constant.FOUR)
            {
                return true;
            }

            return false;
        }
        
        // isin shape
        public bool IsInShapeTop(PointF point)
        {
            float minX = _point1.X < _point2.X ? _point1.X : _point2.X;
            float maxX = _point1.X > _point2.X ? _point1.X : _point2.X;
            float minY = _point1.Y < _point2.Y ? _point1.Y : _point2.Y;
            float middleX = (minX + maxX) / Constant.TWO;
            if (point.X >= middleX - Constant.FOUR && point.X <= middleX + Constant.FOUR && point.Y >= minY - Constant.FOUR && point.Y <= minY + Constant.FOUR)
            {
                return true;
            }

            return false;
        }
        
        // isin shape
        public bool IsInShapeBottom(PointF point)
        {
            float minX = _point1.X < _point2.X ? _point1.X : _point2.X;
            float maxX = _point1.X > _point2.X ? _point1.X : _point2.X;
            float maxY = _point1.Y > _point2.Y ? _point1.Y : _point2.Y;
            float middleX = (minX + maxX) / Constant.TWO;
            if (point.X >= middleX - Constant.FOUR && point.X <= middleX + Constant.FOUR && point.Y >= maxY - Constant.FOUR && point.Y <= maxY + Constant.FOUR)
            {
                return true;
            }

            return false;
        }
        
        // isin shape
        public Model.Location IsInShapeCorner(PointF point)
        {
            foreach (var isInShapeCorner in _isInShapeCorner)
            {
                if (isInShapeCorner.Value(point))
                {
                    return isInShapeCorner.Key;
                }
            }

            return Model.Location.None;
        }
        
        // encode
        public virtual string GetEncode()
        {
            var encode =  _type + Constant.COMMA + _point1.X + Constant.COMMA + _point1.Y + Constant.COMMA + _point2.X + Constant.COMMA + _point2.Y;
            if (_type == ShapeType.LINE)
            {
                encode += (Constant.COMMA + ((Line)this).GetLineTypeResult());
            }
            return encode;
        }
    }
}