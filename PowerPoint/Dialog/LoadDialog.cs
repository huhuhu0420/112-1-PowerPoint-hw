using System.Drawing;
using System.Windows.Forms;

namespace PowerPoint
{
    public class LoadDialog : Form
    {
        public LoadDialog()
        {
            InitializeComponent();
            Text = "SaveDialog";
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
                Text = "OK",
                DialogResult = DialogResult.OK,
                Size = new System.Drawing.Size(75, 23),
                Location = new System.Drawing.Point(this.Size.Width/2 + 5, 200),
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
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Location = new System.Drawing.Point(this.Size.Width/2 - 80, 200),
                Size = new System.Drawing.Size(75, 23),
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
        
        public void InitializeSaveText()
        {
            // saveText
            const string SAVE = "Load?";
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
