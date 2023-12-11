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
        void ICommand.Execute()
        {
            _model.InsertShapeByShape(_shape, _index);
        }
        
        /// <summary>
        /// unexecute
        /// </summary>
        void ICommand.UnExecute()
        {
            _model.RemoveShapeByIndex(_model.GetShapes().Count - 1);
        }
        
        Model _model;
        Shape _shape;
        int _index;
    }
}