using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PowerPoint
{
    public class Pages
    {
        public delegate void PagesChangedEventHandler(bool isAdd, int index);
#pragma warning disable IDE1006 // Naming Styles
        public event PagesChangedEventHandler _pagesChanged;
#pragma warning restore IDE1006 // Naming Styles
        public Pages()
        {
            _pages = new List<BindingList<Shape>>();
        }
        
        /// <summary>
        /// add
        /// </summary>
        public virtual void AddPage()
        {
            _pages.Add(new BindingList<Shape>());
            _pagesChanged?.Invoke(true, _pages.Count - 1);
        }
        
        /// <summary>
        /// add
        /// </summary>
        /// <param name="index"></param>
        /// <param name="page"></param>
        public virtual void AddPageByIndex(int index, BindingList<Shape> page)
        {
            _pages.Insert(index, page);
            _pagesChanged?.Invoke(true, index);
        }
        
        /// <summary>
        /// remove
        /// </summary>
        /// <param name="index"></param>
        public virtual void RemovePageByIndex(int index)
        {
            if (_pages.Count == 1)
            {
                return;
            }
            _pages.RemoveAt(index);
            _pagesChanged?.Invoke(false, index);
        }

        /// <summary>
        /// clear
        /// </summary>
        public virtual void Clear()
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
        public virtual BindingList<Shape> GetPage(int index)
        {
            return _pages[index];
        }
        
        /// <summary>
        /// get
        /// </summary>
        /// <returns></returns>
        public virtual int GetPageCount()
        {
            return _pages.Count;
        }

        /// <summary>
        /// encode
        /// </summary>
        /// <returns></returns>
        public virtual string GetEncode()
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
        
        private readonly List<BindingList<Shape>> _pages;
    }
}