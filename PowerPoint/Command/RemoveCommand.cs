namespace PowerPoint.Command
{
    public class RemoveCommand : ICommand
    {
        public RemoveCommand(Model model, Shape shape, int index, int pageIndex)
        {
            _model = model;
            _shape = shape;
            _index = index;
            _pageIndex = pageIndex;
        }
        
        /// <summary>
        /// execute
        /// </summary>
        public void Execute()
        {
            _model.SetPageIndex(_pageIndex);
            _model.RemoveShapeByIndex(_index);
        }
        
        /// <summary>
        /// unexecute
        /// </summary>
        public void Undo()
        {
            _model.SetPageIndex(_pageIndex);
            _model.InsertShapeByShape(_shape, _index);
        }

        readonly Model _model;
        readonly Shape _shape;
        readonly int _index;
        private int _pageIndex;
    }
}