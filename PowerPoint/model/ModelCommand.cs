using PowerPoint.Command;

namespace PowerPoint
{
    public partial class Model
    {
        public virtual void HandleInsertShape(ShapeType shapeType)
        {
            var shape = _shapeFactory.CreateShape(shapeType);
            _commandManager.Execute(new AddCommand(this, shape));
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
            }
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