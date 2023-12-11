namespace PowerPoint.Command
{
    public class RemoveCommand : ICommand
    {
        public RemoveCommand(Model model, Shape shape, int index)
        {
            _model = model;
            _shape = shape;
            _index = index;
        }
        
        /// <summary>
        /// execute
        /// </summary>
        public void Execute()
        {
            _model.RemoveShapeByIndex(_index);
        }
        
        /// <summary>
        /// unexecute
        /// </summary>
        public void UnExecute()
        {
            _model.InsertShapeByShape(_shape, _index);
        }
        
        Model _model;
        Shape _shape;
        int _index;
    }
}