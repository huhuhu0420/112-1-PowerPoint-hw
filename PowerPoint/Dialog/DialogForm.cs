using System.Drawing;
using System.Windows.Forms;

namespace PowerPoint
{
    public class DialogForm : Form
    {
        public DialogForm()
        {
            InitializeComponent();
            Text = "DialogForm";
            _canvasSize = new Size(Constant.TWO_HUNDRED_FIFTY, Constant.TWO_HUNDRED_FIFTY);
            _isOk = false;
        }

        // init
        public void InitializeComponent()
        {
            InitializeOkButton();
            InitializeCancelButton();
            InitializeTopLeftX();
            InitializeTopLeftY();
            InitializeBottomRightX();
            InitializeBottomRightY();
            InitializeTopLeftXText();
            InitializeTopLeftYText();
            InitializeBottomRightXText();
            InitializeBottomRightYText();
        }
        
        // init
        public void InitializeOkButton()
        {
            // okButton
            _okButton = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Location = new System.Drawing.Point(Constant.FOUR_SEVEN, Constant.TWO_HUNDRED),
                Size = new System.Drawing.Size(Constant.SEVEN_FIVE, Constant.TWENTY_THREE),
                TabIndex = 0,
                UseVisualStyleBackColor = true,
                Enabled = false
            };
            _okButton.Click += (sender, e) =>
            {
                HandleOkButtonClick();
            };
            Controls.Add(_okButton);
        }

        // init
        public void InitializeCancelButton()
        {
            // cancelButton
            _cancelButton = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Location = new System.Drawing.Point(Constant.ONE_THREE_SEVEN, Constant.TWO_HUNDRED),
                Size = new System.Drawing.Size(Constant.SEVEN_FIVE, Constant.TWENTY_THREE),
                TabIndex = 1,
                UseVisualStyleBackColor = true
            };
            _cancelButton.Click += (sender, e) =>
            {
                HandleCancelButtonClick();
            };
            Controls.Add(_cancelButton);
        }

        // init
        public void InitializeTopLeftX()
        {
            // topLeftX
            _topLeftX = new TextBox
            {
                Location = new System.Drawing.Point(Constant.TEN, Constant.SIXTY),
                Size = new System.Drawing.Size(Constant.ONE_HUNDRED, Constant.TWENTY_THREE),
                TabIndex = 2
            };
            _topLeftX.TextChanged += (sender, e) =>
            {
                HandleTextBoxTextChanged();
            };
            _topLeftX.AccessibleName = Constant.TOP_LEFT_X;
            Controls.Add(_topLeftX);
        }
        
        // init
        public void InitializeTopLeftY()
        {
            // topLeftY
            _topLeftY = new TextBox
            {
                Location = new System.Drawing.Point(Constant.ONE_HUNDRED_FIFTY, Constant.SIXTY),
                Size = new System.Drawing.Size(Constant.ONE_HUNDRED, Constant.TWENTY_THREE),
                TabIndex = 3
            };
            _topLeftY.TextChanged += (sender, e) =>
            {
                HandleTextBoxTextChanged();
            };
            _topLeftY.AccessibleName = Constant.TOP_LEFT_Y;
            Controls.Add(_topLeftY);
        }
        
        // init
        public void InitializeBottomRightX()
        {
            // bottomRightX
            _bottomRightX = new TextBox
            {
                Location = new System.Drawing.Point(Constant.TEN, Constant.ONE_HUNDRED_FOUR),
                Size = new System.Drawing.Size(Constant.ONE_HUNDRED, Constant.TWENTY_THREE),
                TabIndex = 4
            };
            _bottomRightX.TextChanged += (sender, e) =>
            {
                HandleTextBoxTextChanged();
            };
            _bottomRightX.AccessibleName = Constant.BOTTOM_RIGHT_X;
            Controls.Add(_bottomRightX);
        }
        
        // init
        public void InitializeBottomRightY()
        {
            // bottomRightY
            _bottomRightY = new TextBox
            {
                Location = new System.Drawing.Point(Constant.ONE_HUNDRED_FIFTY, Constant.ONE_HUNDRED_FOUR),
                Size = new System.Drawing.Size(Constant.ONE_HUNDRED, Constant.TWENTY_THREE),
                TabIndex = 5
            };
            _bottomRightY.TextChanged += (sender, e) =>
            {
                HandleTextBoxTextChanged();
            };
            _bottomRightY.AccessibleName = Constant.BOTTOM_RIGHT_Y;
            Controls.Add(_bottomRightY);
        }
        
        // init
        public void InitializeTopLeftXText()
        {
            // topLeftXText
            _topLeftXText = new TextBox
            {
                Text = "Top Left X",
                Location = new System.Drawing.Point(Constant.TEN, Constant.FOUR_ZERO),
                Size = new System.Drawing.Size(Constant.ONE_HUNDRED, Constant.TWENTY_THREE),
                TabIndex = 6,
                TextAlign = HorizontalAlignment.Center
            };
            _topLeftXText.Font = new Font(_topLeftXText.Font, FontStyle.Bold);
            _topLeftXText.BorderStyle = BorderStyle.None;
            _topLeftXText.Enabled = false;
            Controls.Add(_topLeftXText);
        }
        
        // init
        public void InitializeTopLeftYText()
        {
            // topLeftYText
            _topLeftYText = new TextBox
            {
                Text = "Top Left Y",
                Location = new System.Drawing.Point(Constant.ONE_HUNDRED_FIFTY, Constant.FOUR_ZERO),
                Size = new System.Drawing.Size(Constant.ONE_HUNDRED, Constant.TWENTY_THREE),
                TabIndex = 7,
                TextAlign = HorizontalAlignment.Center
            };
            _topLeftYText.Font = new Font(_topLeftYText.Font, FontStyle.Bold);
            _topLeftYText.BorderStyle = BorderStyle.None;
            _topLeftYText.Enabled = false;
            Controls.Add(_topLeftYText);
        }
        
        // init
        public void InitializeBottomRightXText()
        {
            // bottomRightXText
            _bottomRightXText = new TextBox
            {
                Text = "Bottom Right X",
                Location = new System.Drawing.Point(Constant.TEN, Constant.ONE_HUNDRED_TWENTY),
                Size = new System.Drawing.Size(Constant.ONE_HUNDRED, Constant.TWENTY_THREE),
                TabIndex = 8,
                TextAlign = HorizontalAlignment.Center
            };
            _bottomRightXText.Font = new Font(_bottomRightXText.Font, FontStyle.Bold);
            _bottomRightXText.BorderStyle = BorderStyle.None;
            _bottomRightXText.Enabled = false;
            Controls.Add(_bottomRightXText);
        }
        
        // init
        public void InitializeBottomRightYText()
        {
            // bottomRightYText
            _bottomRightYText = new TextBox
            {
                Text = "Bottom Right Y",
                Location = new System.Drawing.Point(Constant.ONE_HUNDRED_FIFTY, Constant.ONE_HUNDRED_TWENTY),
                Size = new System.Drawing.Size(Constant.ONE_HUNDRED, Constant.TWENTY_THREE),
                TabIndex = 9,
                TextAlign = HorizontalAlignment.Center
            };
            _bottomRightYText.Font = new Font(_bottomRightYText.Font, FontStyle.Bold);
            _bottomRightYText.BorderStyle = BorderStyle.None;
            _bottomRightYText.Enabled = false;
            Controls.Add(_bottomRightYText);
        }
        
        // set
        public void SetCanvasSize(Size size)
        {
            _canvasSize = size;
        }
        
        // handle
        public void HandleTextBoxTextChanged()
        {
            if (_topLeftX.Text == "" || _topLeftY.Text == "" || _bottomRightX.Text == "" || _bottomRightY.Text == "")
            {
                _okButton.Enabled = false;
            }
            else
            {
                HandleTextBoxTextChangedMore();
            }
        }

        //handle
        public void HandleTextBoxTextChangedMore()
        {
            if (float.TryParse(_topLeftX.Text, out var topLeftX) && float.TryParse(_topLeftY.Text, out var topLeftY) && float.TryParse(_bottomRightX.Text, out var bottomRightX) && float.TryParse(_bottomRightY.Text, out var bottomRightY))
            {
                if (IsInRangeX(topLeftX) && IsInRangeX(bottomRightX) && IsInRangeY(topLeftY) && IsInRangeY(bottomRightY) && topLeftX < bottomRightX && topLeftY < bottomRightY)
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

        // handle
        public void HandleOkButtonClick()
        {
            _isOk = true;
        }
        
        // handle
        public void HandleCancelButtonClick()
        {
            _isOk = false;
        }

        // isin range 
        public bool IsInRangeX(float number)
        {
            if (number < 0 || number > _canvasSize.Width)
            {
                return false;
            }
            return true;
        }
        
        // isin range 
        public bool IsInRangeY(float number)
        {
            if (number < 0 || number > _canvasSize.Height)
            {
                return false;
            }
            return true;
        }
        
        // isok
        public bool IsOk()
        {
            return _isOk;
        }
        
        private Button _okButton;
        private Button _cancelButton;
        private TextBox _topLeftXText;
        private TextBox _topLeftYText;
        private TextBox _bottomRightXText;
        private TextBox _bottomRightYText;
        private TextBox _topLeftX;
        private TextBox _topLeftY;
        private TextBox _bottomRightX;
        private TextBox _bottomRightY;
        private bool _isOk;
        private Size _canvasSize;
    }
}