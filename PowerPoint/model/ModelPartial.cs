using System;
using System.Drawing;
using PowerPoint.Command;

namespace PowerPoint
{
    public partial class Model
    {
        /// <summary>
        /// resize
        /// </summary>
        /// <param name="point"></param>
        public virtual void ResizeShape(PointF point, Model.Location location)
        {
            if (_selectIndex == -1)
            {
                return;
            }
            if (location != Location.None)
            {
                _resizeShape[location](point);
            }
        }

        /// <summary>
        /// resize
        /// </summary>
        /// <param name="point"></param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void ResizeShapeRightBottom(PointF point)
        {
            if (point.X < _shapes[_selectIndex].GetPoint1().X + 1)
            {
                point.X = _shapes[_selectIndex].GetPoint1().X + 1;
            }
            if (point.Y < _shapes[_selectIndex].GetPoint1().Y + 1)
            {
                point.Y = _shapes[_selectIndex].GetPoint1().Y + 1;
            }
            _shapes[_selectIndex].SetPoint2(point);
            _select.SetPoint2(point);
            NotifyModelChanged();
        }
        
        /// <summary>
        /// resize
        /// </summary>
        /// <param name="point"></param>
        public virtual void ResizeShapeLeftBottom(PointF point)
        {
            if (point.X > _shapes[_selectIndex].GetPoint2().X - 1)
            {
                point.X = _shapes[_selectIndex].GetPoint2().X - 1;
            }
            if (point.Y < _shapes[_selectIndex].GetPoint1().Y + 1)
            {
                point.Y = _shapes[_selectIndex].GetPoint1().Y + 1;
            }
            _shapes[_selectIndex].SetPoint1(new PointF(point.X, _shapes[_selectIndex].GetPoint1().Y));
            _shapes[_selectIndex].SetPoint2(new PointF(_shapes[_selectIndex].GetPoint2().X, point.Y));
            _select.SetPoint1(new PointF(point.X, _shapes[_selectIndex].GetPoint1().Y));
            _select.SetPoint2(new PointF(_shapes[_selectIndex].GetPoint2().X, point.Y));
            NotifyModelChanged();
        }
        
        /// <summary>
        /// resize
        /// </summary>
        /// <param name="point"></param>
        public virtual void ResizeShapeLeftTop(PointF point)
        {
            if (point.X > _shapes[_selectIndex].GetPoint2().X - 1)
            {
                point.X = _shapes[_selectIndex].GetPoint2().X - 1;
            }
            if (point.Y > _shapes[_selectIndex].GetPoint2().Y - 1)
            {
                point.Y = _shapes[_selectIndex].GetPoint2().Y - 1;
            }
            _shapes[_selectIndex].SetPoint1(point);
            _select.SetPoint1(point);
            NotifyModelChanged();
        }
        
        /// <summary>
        /// resize
        /// </summary>
        /// <param name="point"></param>
        public virtual void ResizeShapeRightTop(PointF point)
        {
            if (point.X < _shapes[_selectIndex].GetPoint1().X + 1)
            {
                point.X = _shapes[_selectIndex].GetPoint1().X + 1;
            }
            if (point.Y > _shapes[_selectIndex].GetPoint2().Y - 1)
            {
                point.Y = _shapes[_selectIndex].GetPoint2().Y - 1;
            }
            _shapes[_selectIndex].SetPoint1(new PointF(_shapes[_selectIndex].GetPoint1().X, point.Y));
            _shapes[_selectIndex].SetPoint2(new PointF(point.X, _shapes[_selectIndex].GetPoint2().Y));
            _select.SetPoint1(new PointF(_shapes[_selectIndex].GetPoint1().X, point.Y));
            _select.SetPoint2(new PointF(point.X, _shapes[_selectIndex].GetPoint2().Y));
            NotifyModelChanged();
        }
        
        /// <summary>
        /// resize
        /// </summary>
        /// <param name="point"></param>
        public virtual void ResizeShapeLeft(PointF point)
        {
            if (point.X > _shapes[_selectIndex].GetPoint2().X - 1)
            {
                point.X = _shapes[_selectIndex].GetPoint2().X - 1;
            }
            _shapes[_selectIndex].SetPoint1(new PointF(point.X, _shapes[_selectIndex].GetPoint1().Y));
            _select.SetPoint1(new PointF(point.X, _shapes[_selectIndex].GetPoint1().Y));
            NotifyModelChanged();
        }
        
        /// <summary>
        /// resize
        /// </summary>
        /// <param name="point"></param>
        public virtual void ResizeShapeRight(PointF point)
        {
            if (point.X < _shapes[_selectIndex].GetPoint1().X + 1)
            {
                point.X = _shapes[_selectIndex].GetPoint1().X + 1;
            }
            _shapes[_selectIndex].SetPoint2(new PointF(point.X, _shapes[_selectIndex].GetPoint2().Y));
            _select.SetPoint2(new PointF(point.X, _shapes[_selectIndex].GetPoint2().Y));
            NotifyModelChanged();
        }
        
        /// <summary>
        /// resize
        /// </summary>
        /// <param name="point"></param>
        public virtual void ResizeShapeTop(PointF point)
        {
            if (point.Y > _shapes[_selectIndex].GetPoint2().Y - 1)
            {
                point.Y = _shapes[_selectIndex].GetPoint2().Y - 1;
            }
            _shapes[_selectIndex].SetPoint1(new PointF(_shapes[_selectIndex].GetPoint1().X, point.Y));
            _select.SetPoint1(new PointF(_shapes[_selectIndex].GetPoint1().X, point.Y));
            NotifyModelChanged();
        }
        
        /// <summary>
        /// resize
        /// </summary>
        /// <param name="point"></param>
        public virtual void ResizeShapeBottom(PointF point)
        {
            if (point.Y < _shapes[_selectIndex].GetPoint1().Y + 1)
            {
                point.Y = _shapes[_selectIndex].GetPoint1().Y + 1;
            }
            _shapes[_selectIndex].SetPoint2(new PointF(_shapes[_selectIndex].GetPoint2().X, point.Y));
            _select.SetPoint2(new PointF(_shapes[_selectIndex].GetPoint2().X, point.Y));
            NotifyModelChanged();
        }
        
    }
}