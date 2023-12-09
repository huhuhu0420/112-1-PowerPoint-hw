namespace PowerPoint.Command
{
    public interface ICommand
    {
        /// <summary>
        /// execute
        /// </summary>
        void Execute();
        
        /// <summary>
        /// unexecute
        /// </summary>
        void UnExecute();
    }
}