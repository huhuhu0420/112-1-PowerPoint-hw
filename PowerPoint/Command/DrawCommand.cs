namespace PowerPoint.Command
{
    public class DrawCommand : ICommand
    {
        public DrawCommand(Model model, Shape shape)
        {
            _model = model;
            _shape = shape; 
        }
        
        /// <summary>
        /// execute
        /// </summary>
        void ICommand.Execute()
        {
            _model.InsertShapeByShape(_shape);
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
    }
}