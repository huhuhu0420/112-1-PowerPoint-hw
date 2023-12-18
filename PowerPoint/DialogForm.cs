using System.Drawing;
using System.Windows.Forms;

namespace PowerPoint
{
    public class DialogForm : Form
    {
        public DialogForm()
        {
            InitializeComponent();
            _canvasSize = new Size(250, 250);
        }

        // init
        public void InitializeComponent()
        {
            // okButton
            _okButton = new Button();
            _okButton.Text = "OK";
            _okButton.DialogResult = DialogResult.OK;
            _okButton.Location = new System.Drawing.Point(47, 200);
            _okButton.Size = new System.Drawing.Size(75, 23);
            _okButton.TabIndex = 0;
            _okButton.UseVisualStyleBackColor = true;
            _okButton.Enabled = false;
            Controls.Add(_okButton);
            
            // cancelButton
            _cancelButton = new Button();
            _cancelButton.Text = "Cancel";
            _cancelButton.DialogResult = DialogResult.Cancel;
            _cancelButton.Location = new System.Drawing.Point(137, 200);
            _cancelButton.Size = new System.Drawing.Size(75, 23);
            _cancelButton.TabIndex = 1;
            _cancelButton.UseVisualStyleBackColor = true;
            Controls.Add(_cancelButton);
            
            // topLeftX
            _topLeftX = new TextBox();
            _topLeftX.Location = new System.Drawing.Point(10, 60);
            _topLeftX.Size = new System.Drawing.Size(100, 23);
            _topLeftX.TabIndex = 2;
            _topLeftX.TextChanged += (sender, e) => { HandleTextBoxTextChanged(); };
            Controls.Add(_topLeftX);
            
            // topLeftY
            _topLeftY = new TextBox();
            _topLeftY.Location = new System.Drawing.Point(150, 60);
            _topLeftY.Size = new System.Drawing.Size(100, 23);
            _topLeftY.TabIndex = 3;
            _topLeftY.TextChanged += (sender, e) => { HandleTextBoxTextChanged(); };
            Controls.Add(_topLeftY);
            
            // bottomRightX
            _bottomRightX = new TextBox();
            _bottomRightX.Location = new System.Drawing.Point(10, 140);
            _bottomRightX.Size = new System.Drawing.Size(100, 23);
            _bottomRightX.TabIndex = 4;
            _bottomRightX.TextChanged += (sender, e) => { HandleTextBoxTextChanged(); };
            Controls.Add(_bottomRightX);
            
            // bottomRightY
            _bottomRightY = new TextBox();
            _bottomRightY.Location = new System.Drawing.Point(150, 140);
            _bottomRightY.Size = new System.Drawing.Size(100, 23);
            _bottomRightY.TabIndex = 5;
            _bottomRightY.TextChanged += (sender, e) => { HandleTextBoxTextChanged(); };
            Controls.Add(_bottomRightY);
            
        }
            
        // set
        public void SetCanvasSize(Size size)
        {
            _canvasSize = size;
        }
        
        public void HandleTextBoxTextChanged()
        {
            if (_topLeftX.Text == "" || _topLeftY.Text == "" || _bottomRightX.Text == "" || _bottomRightY.Text == "")
            {
                _okButton.Enabled = false;
            }
            else if (float.TryParse(_topLeftX.Text, out var topLeftX) && float.TryParse(_topLeftY.Text, out var topLeftY) && float.TryParse(_bottomRightX.Text, out var bottomRightX) && float.TryParse(_bottomRightY.Text, out var bottomRightY))
            {
                if (IsInRangeX(topLeftX) && IsInRangeX(bottomRightX) && IsInRangeY(topLeftY) && IsInRangeY(bottomRightY))
                {
                    Global.TopLeftX = topLeftX;
                    Global.TopLeftY = topLeftY;
                    Global.BottomRightX = bottomRightX;
                    Global.BottomRightY = bottomRightY;
                    _okButton.Enabled = true;
                }
                else
                {
                    _okButton.Enabled = false;
                }
            }
        }

        public bool IsInRangeX(float number)
        {
            if (number < 0 || number > _canvasSize.Width)
            {
                return false;
            }
            return true;
        }
        
        public bool IsInRangeY(float number)
        {
            if (number < 0 || number > _canvasSize.Height)
            {
                return false;
            }
            return true;
        }
        
        private Button _okButton;
        private Button _cancelButton;
        private TextBox _topLeftX;
        private TextBox _topLeftY;
        private TextBox _bottomRightX;
        private TextBox _bottomRightY;
        private Size _canvasSize;
    }
}