using System.Drawing;
using System.Windows.Forms;

namespace PowerPoint
{
    public class LoadDialog : Form
    {
        public LoadDialog()
        {
            InitializeComponent();
            Text = Constant.LOAD_DIALOG;
            _isOk = false;
        }
        
        // init
        public void InitializeComponent()
        {
            InitializeOkButton();
            InitializeCancelButton();
            InitializeSaveText();
        }
        
        // init
        public void InitializeOkButton()
        {
            // okButton
            _okButton = new Button
            {
                Text = Constant.OK,
                DialogResult = DialogResult.OK,
                Size = new System.Drawing.Size(Constant.SEVEN_FIVE, Constant.TWENTY_THREE),
                Location = new System.Drawing.Point(this.Size.Width / 2 + Constant.FIVE, Constant.TWO_HUNDRED),
                TabIndex = 0,
                UseVisualStyleBackColor = true,
                Enabled = true
            };
            _okButton.Click += (sender, e) =>
            {
                _isOk = true;
            };
            Controls.Add(_okButton);
        }
        
        // init
        public void InitializeCancelButton()
        {
            // cancelButton
            _cancelButton = new Button
            {
                Text = Constant.CANCEL,
                DialogResult = DialogResult.Cancel,
                Location = new System.Drawing.Point(this.Size.Width / 2 - Constant.EIGHTY, Constant.TWO_HUNDRED),
                Size = new System.Drawing.Size(Constant.SEVEN_FIVE, Constant.TWENTY_THREE),
                TabIndex = 1,
                UseVisualStyleBackColor = true,
                Enabled = true
            };
            _cancelButton.Click += (sender, e) =>
            {
                _isOk = false;
            };
            Controls.Add(_cancelButton);
        }
        
        // init
        public void InitializeSaveText()
        {
            // saveText
            const string SAVE = Constant.LOAD;
            _saveText = new TextBox
            {
                Text = SAVE,
                TabIndex = 1,
                Enabled = true
            };
            _saveText.Font = new Font(_saveText.Font.ToString(), 24, FontStyle.Bold);
            _saveText.Location = new Point(this.Size.Width / 2 - _saveText.Size.Width / 2, 100);
            _saveText.BorderStyle = BorderStyle.None;
            _saveText.Enabled = false;
            // _saveText.TextAlign = HorizontalAlignment.Left;
            Controls.Add(_saveText);
        }
        
        // ok
        public bool IsOk()
        {
            return _isOk;
        }
        
        private bool _isOk;
        private Button _okButton;
        private Button _cancelButton;
        private TextBox _saveText;
    }
}
