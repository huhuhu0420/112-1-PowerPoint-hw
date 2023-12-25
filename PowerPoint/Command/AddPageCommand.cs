using System.ComponentModel;
using System.Runtime.InteropServices;

namespace PowerPoint.Command
{
    public class AddPageCommand : ICommand
    {
        public AddPageCommand(Model model, BindingList<Shape> page, int index, int focusIndex)
        {
            _page = page;
            _index = index;
            _model = model;
            _focusIndex = focusIndex;
        }
        
        /// <summary>
        /// execute
        /// </summary>
        public void Execute()
        {
            _model.InsertPageByIndex(_model.GetPages().GetPageCount() - 1, new BindingList<Shape>());
        }
        
        /// <summary>
        /// undo
        /// </summary>
        public void Undo()
        {
            _model.SetPageIndex(_index);
            _model.DeletePage();
            _model.SetPageIndex(_focusIndex);
        }
        
        private BindingList<Shape> _page;
        private int _index;
        private int _focusIndex;
        private Model _model;
    }
}