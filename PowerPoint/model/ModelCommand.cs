using System.Drawing;
using PowerPoint.Command;

namespace PowerPoint
{
    public partial class Model
    {
#pragma warning disable IDE1006 // Naming Styles
        public event CommandManager.HandleUndoRedoHistoryEventHandler _undoRedoHistoryChanged;
#pragma warning restore IDE1006 // Naming Styles

        // handle
        public virtual void HandleInsertShape(Shape shape)
        {
            _commandManager.Execute(new AddCommand(this, shape, _shapes.Count - 1, _pageIndex));
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
                _commandManager.Execute(new RemoveCommand(this, _shapes[index], index, _pageIndex));
                RemoveShapeByIndex(index);
            }
        }

        // handle
        public virtual void HandleMoveShape(int index, SizeF bias)
        {
            _commandManager.Execute(new MoveCommand(this, index, bias));
        }
        
        // handle
        public virtual void HandleResizeShape(int index, Shape oldShape)
        {
            _commandManager.Execute(new ResizeCommand(this, oldShape, _shapes[index], index));
        }
        
        // handle
        public virtual void HandleDrawShape(Shape shape)
        {
            _commandManager.Execute(new DrawCommand(this, shape, _shapes.Count - 1, _pageIndex));
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
#pragma warning disable IDE1005 // Delegate invocation can be simplified.
                _undoRedoHistoryChanged(isUndo, isRedo);
#pragma warning restore IDE1005 // Delegate invocation can be simplified.
            }
        }
    }
}