using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PowerPoint
{
    public class Pages
    {
        public delegate void PagesChangedEventHandler(bool isAdd, int index);
        public event PagesChangedEventHandler _pagesChanged;
        public Pages()
        {
            _pages = new List<BindingList<Shape>>();
        }
        
        /// <summary>
        /// add
        /// </summary>
        public void AddPage()
        {
            _pages.Add(new BindingList<Shape>());
            if (_pagesChanged != null)
            {
                _pagesChanged(true, _pages.Count - 1);
            }
        }
        
        /// <summary>
        /// add
        /// </summary>
        /// <param name="index"></param>
        /// <param name="page"></param>
        public void AddPageByIndex(int index, BindingList<Shape> page)
        {
            _pages.Insert(index, page);
            if (_pagesChanged != null)
            {
                _pagesChanged(true, index);
            }
        }
        
        /// <summary>
        /// remove
        /// </summary>
        /// <param name="index"></param>
        public void RemovePageByIndex(int index)
        {
            if (_pages.Count == 1)
            {
                return;
            }
            _pages.RemoveAt(index);
            if (_pagesChanged != null)
            {
                _pagesChanged(false, index);
            }
        }

        /// <summary>
        /// clear
        /// </summary>
        public void Clear()
        {
            for (var i=_pages.Count-1; i>0; i--)
            {
                RemovePageByIndex(i);
            }
            _pages[0].Clear();
        }
        
        /// <summary>
        /// get
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public BindingList<Shape> GetPage(int index)
        {
            return _pages[index];
        }
        
        /// <summary>
        /// get
        /// </summary>
        /// <returns></returns>
        public int GetPageCount()
        {
            return _pages.Count;
        }

        /// <summary>
        /// encode
        /// </summary>
        /// <returns></returns>
        public string GetEncode()
        {
            var csv = new StringBuilder();
            for (var i = 0; i < _pages.Count; i++)
            {
                if (i != 0)
                {
                    csv.Append("page");
                    csv.Append("\n");
                }
                for (var j = 0; j < _pages[i].Count; j++)
                {
                    csv.Append(_pages[i][j].GetEncode());
                    csv.Append("\n");
                }
            }

            return csv.ToString();
        }
        
        private List<BindingList<Shape>> _pages;
    }
}