using System.Collections.Generic;
using System.Diagnostics;

namespace PowerPoint.Command
{
    public class CommandManager
    {
        /// <summary>
        /// ex
        /// </summary>
        /// <param name="command"></param>
        public virtual void Execute(ICommand command)
        {
            _commandHistory.Add(command);
            _redoHistory.Clear();
        }   
        
        /// <summary>
        /// undo
        /// </summary>
        public virtual void Undo()
        {
            if (_commandHistory.Count == 0)
            {
                return;
            }
            
            var command = _commandHistory[_commandHistory.Count - 1];
            command.Unexecute();
            _redoHistory.Add(command);
            _commandHistory.RemoveAt(_commandHistory.Count - 1);
        }

        /// <summary>
        /// redo
        /// </summary>
        public virtual void Redo()
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
        
        private readonly List<ICommand> _commandHistory = new List<ICommand>();
        private readonly List<ICommand> _redoHistory = new List<ICommand>();
    }
}