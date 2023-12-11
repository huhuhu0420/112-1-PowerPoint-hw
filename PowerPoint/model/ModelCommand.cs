using System.Drawing;
using PowerPoint.Command;

namespace PowerPoint
{
    public partial class Model
    {
        public event CommandManager.HandleUndoRedoHistoryEventHandler _undoRedoHistoryChanged;
        
        // handle
        public virtual void HandleInsertShape(Shape shape)
        {
            _commandManager.Execute(new AddCommand(this, shape, _shapes.Count - 1));
        }

        // handle
        public virtual void HandleRemoveShape(int index)
        {
            if (index == -1 && _selectIndex != -1)
            {
                index = _selectIndex;
            }
            if (index != -1)
            {
                _commandManager.Execute(new RemoveCommand(this, _shapes[index], index));
                RemoveShapeByIndex(index);
            }
        }

        // handle
        public virtual void HandleMoveShape(int index, SizeF bias)
        {
            _commandManager.Execute(new MoveCommand(this, index, bias));
        }
        
        // handle
        public virtual void HandleDrawShape(Shape shape)
        {
            _commandManager.Execute(new DrawCommand(this, shape, _shapes.Count - 1));
        }
        
        // undo
        public virtual void Undo()
        {
            _commandManager.Undo();
        }
        
        // redo
        public virtual void Redo()
        {
            _commandManager.Redo();
        }
        
        /// <summary>
        /// set
        /// </summary>
        /// <param name="isUndo"></param>
        /// <param name="isRedo"></param>
        public void SetUndoRedoHistory(bool isUndo, bool isRedo)
        {
            if (_undoRedoHistoryChanged != null)
            {
                _undoRedoHistoryChanged(isUndo, isRedo);
            }
        }
    }
}