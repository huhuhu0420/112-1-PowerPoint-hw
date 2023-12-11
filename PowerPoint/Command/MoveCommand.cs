using System.Drawing;

namespace PowerPoint.Command
{
    public class MoveCommand : ICommand
    {
        public MoveCommand(Model model, int index, SizeF bias)
        {
            _model = model;
            _index = index;
            _bias = bias;
        }
        
        /// <summary>
        /// execute
        /// </summary>
        public void Execute()
        {
            _model.MoveShapeByBias(_bias, _index);
        }
        
        /// <summary>
        /// unexecute
        /// </summary>
        public void Unexecute()
        {
            _model.MoveShapeByBias(new SizeF(-1 * _bias.Width, -1 * _bias.Height), _index);
        }

        readonly Model _model;
        readonly int _index;
        private SizeF _bias;
    }
}