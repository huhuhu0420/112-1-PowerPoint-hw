using System.Windows.Forms;
using System.Drawing;
namespace WindowPowerPoint
{
    public class BindingToolStripButton : ToolStripButton, IBindableComponent
    {
        public BindingToolStripButton()
        {
            _dataBindings = new ControlBindingsCollection(this);
            _bindingContext = new BindingContext();
        }
        
        public ControlBindingsCollection DataBindings
        {
            get
            {
                return _dataBindings;
            }
        }
        
        public BindingContext BindingContext
        {
            get
            {
                return _bindingContext;
            }
            set 
            { 
                _bindingContext = value; 
            }
        }
        
        private ControlBindingsCollection _dataBindings;
        private BindingContext _bindingContext;
    }
}