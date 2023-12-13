namespace PowerPoint.Command
{
    public class AddCommand : ICommand
    {
        public AddCommand(Model model, Shape shape, int index)
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
            _model.InsertShapeByShape(_shape, _index);
        }
        
        /// <summary>
        /// unexecute
        /// </summary>
        public void Undo()
        {
            _model.RemoveShapeByIndex(_model.GetShapes().Count - 1);
        }

        readonly Model _model;
        readonly Shape _shape;
        readonly int _index;
    }
}