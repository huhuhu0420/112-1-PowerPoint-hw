using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Security.Authentication.ExtendedProtection;
using System.Windows;

namespace PowerPoint
{
    public class Shape
    {
        public Shape (PointF point1, PointF point2)
        {
            _point1 = point1;
            _point2 = point2;
            AddIsInShapeCorner();
        }

        public Shape()
        {
            AddIsInShapeCorner(); 
        }
        
        /// <summary>
        /// add
        /// </summary>
        public void AddIsInShapeCorner()
        {
            _isInShapeCorner.Add(Model.Location.LeftTop, point => IsInShapeLeftTop(point));
            _isInShapeCorner.Add(Model.Location.RightBottom,(PointF point) => IsInShapeRightBottom(point));
            _isInShapeCorner.Add(Model.Location.RightTop, (PointF point) => IsInShapeRightTop(point));
            _isInShapeCorner.Add(Model.Location.LeftBottom, (PointF point) => IsInShapeLeftBottom(point));
            _isInShapeCorner.Add(Model.Location.Left, (PointF point) => IsInShapeLeft(point));
            _isInShapeCorner.Add(Model.Location.Right, (PointF point) => IsInShapeRight(point));
            _isInShapeCorner.Add(Model.Location.Top, (PointF point) => IsInShapeTop(point));
            _isInShapeCorner.Add(Model.Location.Bottom, (PointF point) => IsInShapeBottom(point));
        }

        /// <summary>
        /// get name
        /// </summary>
        /// <returns></returns>
        public string GetShapeName()
        {
            return _shapeName;
        }

        /// <summary>
        /// get info
        /// </summary>
        /// <returns></returns>
        public string GetInfo()
        {
            return FormatCoordinate(_point1) + Constant.COMMA + FormatCoordinate(_point2);
        }

        /// <summary>
        /// format coordinate
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private string FormatCoordinate(PointF point)
        {
            return Constant.PARENTHESIS1 + point.X + Constant.COMMA + point.Y + Constant.PARENTHESIS2;
        }

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="graphics"></param>
        public virtual void Draw(IGraphics graphics)
        {
        }

        /// <summary>
        /// draw select
        /// </summary>
        /// <param name="graphics"></param>
        public void DrawSelect(IGraphics graphics)
        {
            Pen pen = new Pen(Color.DeepPink, 1);
            PointF point1 = new PointF(_point1.X - Constant.TWO, _point1.Y - Constant.TWO);
            PointF point2 = new PointF(_point2.X + Constant.TWO, _point2.Y + Constant.TWO);
            graphics.DrawSelect(pen, point1, point2);
        }

        /// <summary>
        /// is in
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool IsInShape(PointF point)
        {
            float minX = _point1.X < _point2.X ? _point1.X : _point2.X;
            float maxX = _point1.X > _point2.X ? _point1.X : _point2.X;
            float minY = _point1.Y < _point2.Y ? _point1.Y : _point2.Y;
            float maxY = _point1.Y > _point2.Y ? _point1.Y : _point2.Y;
            if (minX <= point.X && point.X <= maxX && minY <= point.Y && point.Y <= maxY)
            {
                return true;
            }

            return false;
        }

        public string ShapeName
        {
            get
            {
                return GetShapeName();
            }
        }

        public string Info
        {
            get
            {
                return GetInfo();
            }
        }

        public ShapeType Type
        {
            set
            {
                _type = value;
            }
        }
        
        public ShapeType GetShapeType()
        {
            return _type;
        }

        /// <summary>
        /// set
        /// </summary>
        /// <param name="point"></param>
        public void SetPoint1(PointF point)
        {
            _point1 = point;
        }
        
        /// <summary>
        /// get
        /// </summary>
        /// <param name="point"></param>
        public PointF GetPoint1()
        {
            return _point1;
        }

        /// <summary>
        /// set
        /// </summary>
        /// <param name="point"></param>
        public void SetPoint2(PointF point)
        {
            _point2 = point;
        }

        /// <summary>
        /// get
        /// </summary>
        /// <returns></returns>
        public PointF GetPoint2()
        {
            return _point2;
        }
        
        /// <summary>
        /// get center
        /// </summary>
        /// <returns></returns>
        public PointF GetCenterPoint()
        {
            return new PointF((_point1.X + _point2.X) / Constant.TWO, (_point1.Y + _point2.Y) / Constant.TWO);
        }

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

        /// <summary>
        /// get
        /// </summary>
        /// <returns></returns>
        public Line.LineType GetLineType()
        {
            float minX = _point1.X < _point2.X ? _point1.X : _point2.X;
            float minY = _point1.Y < _point2.Y ? _point1.Y : _point2.Y;
            float maxX = _point1.X > _point2.X ? _point1.X : _point2.X;
            float maxY = _point1.Y > _point2.Y ? _point1.Y : _point2.Y;
            return GetLineType2(minX, minY, maxX, maxY);
        }

        /// <summary>
        /// get
        /// </summary>
        /// <param name="minX"></param>
        /// <param name="minY"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        /// <returns></returns>
        public Line.LineType GetLineType2(float minX, float minY, float maxX, float maxY)
        {
            if (minX == _point1.X && minY == _point1.Y)
            {
                return Line.LineType.LeftTop;
            }
            else if (maxX == _point1.X && minY == _point1.Y)
            {
                return Line.LineType.RightTop;
            }
            else if (minX == _point1.X && maxY == _point1.Y)
            {
                return Line.LineType.LeftBottom;
            }
            else if (maxX == _point1.X && maxY == _point1.Y)
            {
                return Line.LineType.RightBottom;
            }
            return Line.LineType.None;
        }

        protected string _shapeName = "";
        protected ShapeType _type = ShapeType.LINE;
        protected PointF _point1 = new PointF(0, 0);
        protected PointF _point2 = new PointF(0, 0);
        protected Dictionary<Model.Location, Func<PointF, bool>> _isInShapeCorner = new Dictionary<Model.Location, Func<PointF, bool>>();
    }
    
    public enum ShapeType
    {
        LINE,
        RECTANGLE,
        CIRCLE,
        ARROW
    }
}