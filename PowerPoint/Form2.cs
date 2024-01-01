
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PowerPoint
{
    public partial class Form1
    {
        /// <summary>
        /// save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickSaveButton(object sender, EventArgs e)
        {
            _saveDialog.ShowDialog();
            if (_saveDialog.IsOk())
            {
                _presentationModel.Save();
            }
        }

        /// <summary>
        /// load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickLoadButton(object sender, EventArgs e)
        {
            _loadDialog.ShowDialog();
            if (_loadDialog.IsOk())
            {
                _presentationModel.Load();
            }
        }

        /// <summary>
        /// new page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ClickNewPageButton(object sender, EventArgs e)
        {
            // Button button = new Button();
            // button.BackColor = System.Drawing.SystemColors.ControlLightLight;
            // var width = splitContainer1.Panel1.Width - Constant.EIGHT;
            // var height = (int)(splitContainer1.Panel1.Width * Constant.RATIO) - Constant.EIGHT;
            // button.Size = new Size(width, height);
            // button.Click += HandleClickPage;
            // flowLayoutPanel1.Controls.Add(button);
            _presentationModel.AddPage();
        }

        /// <summary>
        /// click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleClickPage(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var index = flowLayoutPanel1.Controls.IndexOf(button);
            // Debug.Print(index.ToString());
            _presentationModel.SetPageIndex(index);
            dataGridView1.DataSource = _presentationModel.GetShapes();
        }

        /// <summary>
        /// change
        /// </summary>
        /// <param name="isAdd"></param>
        /// <param name="index"></param>
        public void HandlePageChanged(bool isAdd, int index)
        {
            if (isAdd)
            {
                HandlePageChangedMore(index);
            }
            else
            {
                flowLayoutPanel1.Controls.RemoveAt(index);
            }
            dataGridView1.DataSource = _presentationModel.GetShapes();
        }

        // more
        public void HandlePageChangedMore(int index)
        {
            Button button = new Button();
            button.BackColor = System.Drawing.SystemColors.ControlLightLight;
            var width = splitContainer1.Panel1.Width - Constant.EIGHT;
            var height = (int)(splitContainer1.Panel1.Width * Constant.RATIO) - Constant.EIGHT;
            button.Size = new Size(width, height);
            button.Click += HandleClickPage;
            button.Name = Constant.SLIDE;
            button.BackgroundImageLayout = ImageLayout.Stretch;
            flowLayoutPanel1.Controls.Add(button);
            flowLayoutPanel1.Controls.SetChildIndex(button, index);
        }
    }
}
