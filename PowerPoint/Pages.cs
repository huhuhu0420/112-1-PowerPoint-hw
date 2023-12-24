using System.Collections.Generic;
using System.ComponentModel;

namespace PowerPoint
{
    public class Pages
    {
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
        }
        
        /// <summary>
        /// add
        /// </summary>
        /// <param name="index"></param>
        public void AddPageByIndex(int index)
        {
            _pages.Insert(index, new BindingList<Shape>());
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
        
        private List<BindingList<Shape>> _pages;
    }
}