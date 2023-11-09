﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Security.Authentication.ExtendedProtection;
using System.Windows;

namespace PowerPoint
{
    public class Shape
    {
        public Shape (Point point1, Point point2)
        {
            _point1 = point1;
            _point2 = point2;
        }

        public Shape()
        {
            
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
        private string FormatCoordinate(Point point)
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
        public virtual void DrawSelect(IGraphics graphics)
        {
        }

        public bool IsInShape(Point point)
        {
            if (_point1.X <= point.X && point.X <= _point2.X && _point1.Y <= point.Y && point.Y < _point2.Y)
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
            get;
            set;
        }

        /// <summary>
        /// set
        /// </summary>
        /// <param name="point"></param>
        public void SetPoint1(Point point)
        {
            _point1 = point;
        }

        /// <summary>
        /// set
        /// </summary>
        /// <param name="point"></param>
        public void SetPoint2(Point point)
        {
            _point2 = point;
        }

        protected string _shapeName = "";
        protected Point _point1 = new Point(0, 0);
        protected Point _point2 = new Point(0, 0);
    }
    
    public enum ShapeType
    {
        LINE,
        RECTANGLE,
        CIRCLE,
        ARROW
    }
}