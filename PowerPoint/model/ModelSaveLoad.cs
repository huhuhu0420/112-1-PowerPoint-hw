using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PowerPoint
{
    public partial class Model
    {
        // save
        public virtual async Task Save()
        {
            var csv = _pages.GetEncode();
            // Debug.Print(csv);
            File.WriteAllText(_filePath, csv);
            if (_fileId == "")
                _fileId = await _service.UploadFile(_filePath, Constant.TEXT_PLAIN);
            else
                _service.UpdateFile(Constant.FILE_NAME, _fileId, Constant.TEXT_PLAIN);
            Thread.Sleep(Constant.DELAY);
        }

        // load
        public virtual void Load()
        {
            _commandManager.Clear();
            _service.DownloadFile(_fileId, _filePath);
            SetPageIndex(0);
            _pages.Clear();
            SetPageIndex(0);
            ReadFile();
        }
        
        // read file
        public virtual void ReadFile()
        {
            const char COMMA = ',';
            using(var reader = new StreamReader(_filePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(COMMA);
                    if (values[0] == Constant.PAGE)
                    {
                        AddPage();
                    }
                    else
                    {
                        ReadShape(values);
                    }
                }
            }
        }
        
        /// <summary>
        /// delete
        /// </summary>
        public virtual void DeleteDriveFile()
        {
            if (_fileId == "")
                return;
            _service.DeleteFile(_fileId);
        }

        /// <summary>
        /// read
        /// </summary>
        /// <param name="info"></param>
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
            LineType
        }

        readonly GoogleDriveService _service;
        private readonly string _solutionPath;
        private readonly string _filePath;
        private string _fileId = "";
    }
}