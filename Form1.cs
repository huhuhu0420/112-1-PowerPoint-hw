using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerPoint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = _model.GetShapes();
        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
            _model.InsertShape((ShapeType)(comboBox1.SelectedIndex));
        }
        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                _model.RemoveShape(e.RowIndex);
                // Debug.WriteLine(e.RowIndex.ToString());
            }
        }
    }
}
