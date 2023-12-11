using System.Collections.Generic;
using System.Diagnostics;

namespace PowerPoint.Command
{
    public class CommandManager
    {
        public delegate void HandleUndoRedoHistoryEventHandler(bool isUndo, bool isRedo);
#pragma warning disable IDE1006 // Naming Styles
        public event HandleUndoRedoHistoryEventHandler _undoRedoHistoryChanged;
#pragma warning restore IDE1006 // Naming Styles

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
#pragma warning disable IDE1005 // Delegate invocation can be simplified.
                _undoRedoHistoryChanged(true, false);
#pragma warning restore IDE1005 // Delegate invocation can be simplified.
            }
        }   
        
        /// <summary>
        /// undo
        /// </summary>
        public virtual void HandleUndo()
        {
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
            HandleUndo();
        }

        /// <summary>
        /// handle
        /// </summary>
        public virtual void HandleRedo()
        {
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
            HandleRedo();
        }
        
        private readonly List<ICommand> _commandHistory;
        private readonly List<ICommand> _redoHistory;
    }
}