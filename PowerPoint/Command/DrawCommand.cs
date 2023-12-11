namespace PowerPoint.Command
{
    public class DrawCommand : ICommand
    {
        public DrawCommand(Model model, Shape shape, int index)
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
        public void UnExecute()
        {
            _model.RemoveShapeByIndex(_model.GetShapes().Count - 1);
        }

        
        Model _model;
        Shape _shape;
        int _index;
    }
}