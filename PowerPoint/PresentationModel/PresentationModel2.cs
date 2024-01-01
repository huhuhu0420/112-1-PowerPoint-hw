using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint.PresentationModel
{
    public partial class PresentationModel
    {

        /// <summary>
        /// delete
        /// </summary>
        public void DeletePage()
        {
            _model.DeletePage();
        }

        /// <summary>
        /// get
        /// </summary>
        /// <returns></returns>
        public int GetPageIndex()
        {
            return _model.GetPageIndex();
        }

        /// <summary>
        /// save
        /// </summary>
        public async void Save()
        {
            _isSaveButtonEnabled = false;
            HandlePropertyChanged();
            var task = Task.Run(() => _model.Save());
            await task;
            _isSaveButtonEnabled = true;
            HandlePropertyChanged();
        }

        /// <summary>
        /// load
        /// </summary>
        public void Load()
        {
            _model.Load();
        }

        /// <summary>
        /// delete
        /// </summary>
        public void DeleteDriveFile()
        {
            _model.DeleteDriveFile();
        }

        public bool IsLineButtonChecked
        {
            get
            {
                return _isButtonChecked[(int)ShapeType.LINE];
            }
        }

        public bool IsRectangleButtonChecked
        {
            get
            {
                return _isButtonChecked[(int)ShapeType.RECTANGLE];
            }
        }

        public bool IsCircleButtonChecked
        {
            get
            {
                return _isButtonChecked[(int)ShapeType.CIRCLE];
            }
        }

        public bool IsMouseButtonChecked
        {
            get
            {
                return _isButtonChecked[(int)ShapeType.ARROW];
            }
        }

        public bool IsSaveButtonEnabled
        {
            get
            {
                return _isSaveButtonEnabled;
            }
        }
    }
}
