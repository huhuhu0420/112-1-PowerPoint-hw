using System.Collections.Generic;

namespace PowerPoint.Command
{
    public class CommandManager
    {
        public void Execute(ICommand command)
        {
            command.Execute();
            _commandHistory.Add(command);
        }   
        
        public void Undo()
        {
            if (_commandHistory.Count == 0)
            {
                return;
            }
            
            var command = _commandHistory[_commandHistory.Count - 1];
            command.UnExecute();
            _redoHistory.Add(command);
            _commandHistory.RemoveAt(_commandHistory.Count - 1);
        }

        public void Redo()
        {
            if (_redoHistory.Count == 0)
            {
                return;
            }
            
            var command = _redoHistory[_redoHistory.Count - 1];
            command.Execute();
            _commandHistory.Add(command);
            _redoHistory.RemoveAt(_redoHistory.Count - 1);
            
        }
        
        private List<ICommand> _commandHistory = new List<ICommand>();
        private List<ICommand> _redoHistory = new List<ICommand>();
    }
}