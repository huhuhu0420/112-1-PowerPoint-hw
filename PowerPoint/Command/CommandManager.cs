using System.Collections.Generic;
using System.Diagnostics;

namespace PowerPoint.Command
{
    public class CommandManager
    {
        public delegate void HandleUndoRedoHistoryEventHandler(bool isUndo, bool isRedo);
        public event HandleUndoRedoHistoryEventHandler _undoRedoHistoryChanged;
        
        public CommandManager()
        {
            _commandHistory = new List<ICommand>();
            _redoHistory = new List<ICommand>();
        }
        
        /// <summary>
        /// ex
        /// </summary>
        /// <param name="command"></param>
        public virtual void Execute(ICommand command)
        {
            _commandHistory.Add(command);
            _redoHistory.Clear();
            if (_undoRedoHistoryChanged != null)
            {
                _undoRedoHistoryChanged(true, false);
            }
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
            if (_undoRedoHistoryChanged != null)
            {
                if (_commandHistory.Count == 0)
                {
                    _undoRedoHistoryChanged(false, true);
                }
                else
                {
                    _undoRedoHistoryChanged(true, true);
                }
            }
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
            if (_undoRedoHistoryChanged != null)
            {
                if (_redoHistory.Count == 0)
                {
                    _undoRedoHistoryChanged(true, false);
                }
                else
                {
                    _undoRedoHistoryChanged(true, true);
                }
            }
        }
        
        private readonly List<ICommand> _commandHistory;
        private readonly List<ICommand> _redoHistory;
    }
}