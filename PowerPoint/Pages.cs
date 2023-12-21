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
        
        public void AddPage()
        {
            _pages.Add(new BindingList<Shape>());
        }
        
        public BindingList<Shape> GetPage(int index)
        {
            return _pages[index];
        }
        
        private List<BindingList<Shape>> _pages;
    }
}