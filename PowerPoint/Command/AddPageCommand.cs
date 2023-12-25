using System.ComponentModel;
using System.Runtime.InteropServices;

namespace PowerPoint.Command
{
    public class AddPageCommand : ICommand
    {
        public AddPageCommand(Model model, BindingList<Shape> page, int index)
        {
            _page = page;
            _index = index;
            _model = model;
        }
        
        /// <summary>
        /// execute
        /// </summary>
        public void Execute()
        {
            _model.AddPage();
        }
        
        /// <summary>
        /// undo
        /// </summary>
        public void Undo()
        {
            _model.SetPageIndex(_index);
            _model.DeletePage();
        }
        
        private BindingList<Shape> _page;
        private int _index;
        private Model _model;
    }
}