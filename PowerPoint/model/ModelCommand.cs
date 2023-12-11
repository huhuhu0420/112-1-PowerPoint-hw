using System.Drawing;
using PowerPoint.Command;

namespace PowerPoint
{
    public partial class Model
    {
        public virtual void HandleInsertShape(Shape shape)
        {
            _commandManager.Execute(new AddCommand(this, shape, _shapes.Count - 1));
        }
        
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
        
        public virtual void HandleMoveShape(int index, SizeF bias)
        {
            _commandManager.Execute(new MoveCommand(this, index, bias));
        }
        
        public virtual void HandleDrawShape(Shape shape)
        {
            _commandManager.Execute(new DrawCommand(this, shape, _shapes.Count - 1));
        }
        
        public virtual void Undo()
        {
            _commandManager.Undo();
        }
        
        public virtual void Redo()
        {
            _commandManager.Redo();
        }
    }
}