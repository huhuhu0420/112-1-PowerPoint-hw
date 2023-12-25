using System.ComponentModel;
using System.Runtime.InteropServices;

namespace PowerPoint.Command
{
    public class RemovePageCommand : ICommand
    {
        public RemovePageCommand(Model model, BindingList<Shape> page, int index, int focusIndex)
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
            _model.DeletePageByIndex(_index);
        }
        
        /// <summary>
        /// undo
        /// </summary>
        public void Undo()
        {
            _model.InsertPageByIndex(_index, _page);
        }
        
        private BindingList<Shape> _page;
        private int _index;
        private int _focusIndex;
        private Model _model;
    }
}