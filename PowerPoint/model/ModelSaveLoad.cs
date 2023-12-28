using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace PowerPoint
{
    public partial class Model
    {
        // save
        public void Save()
        {
            var createText = "Hello and Welcome" + Environment.NewLine;
            
            // File.WriteAllText(_filePath, createText);
            // _service.UploadFile(FILENAME, "text/plain");
        }

        // load
        public void Load()
        {
            SetPageIndex(0);
            _pages.Clear();
            SetPageIndex(0);
            ReadFile();
        }
        
        // read file
        public void ReadFile()
        {
            using(var reader = new StreamReader(_filePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    Debug.Print(values[0]);
                    if (values[0] == "page")
                    {
                        Debug.Print("add page");
                        AddPage();
                    }
                    else
                    {
                        ReadShape(values);
                    }
                }
            }
        }

        public void ReadShape(string[] info)
        {
            var shapeName = info[(int)SavedIndex.ShapeName];
            Enum.TryParse<ShapeType>(shapeName, out var shapeType);
            Point point1 = new Point(int.Parse(info[(int)SavedIndex.LeftTopX]), int.Parse(info[(int)SavedIndex.LeftTopY]));
            Point point2 = new Point(int.Parse(info[(int)SavedIndex.RightBottomX]), int.Parse(info[(int)SavedIndex.RightBottomY]));
            Shape shape = _shapeFactory.CreateShape(shapeType, point1, point2);
            if (shapeType == ShapeType.LINE)
            {
                Enum.TryParse<Line.LineType>(info[(int)SavedIndex.LineType], out var lineType);
                ((Line)shape).SetLineType(lineType);
            }
            InsertShapeByShape(shape, _shapes.Count);
        }

        public enum SavedIndex
        {
            ShapeName,
            LeftTopX,
            LeftTopY,
            RightBottomX,
            RightBottomY,
            LineType,
        }
        
        GoogleDriveService _service;
        private string _solutionPath;
        private string _filePath;
        private string _applicationName = "PowerPoint";
    }
}