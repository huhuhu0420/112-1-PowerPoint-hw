using System.Windows.Forms;

namespace PowerPoint.PresentationModel
{
    public class PresentationModel
    {
        Model _model;
        public PresentationModel(Model model, Control canvas)
        {
            this._model = model;
        }

        public void Draw(System.Drawing.Graphics graphics)
        {
            _model.Draw(new WindowsFormsGraphicsAdaptor(graphics));
        }
    }
}