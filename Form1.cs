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
            dataGridView1.DataSource = _presentationModel.GetShapes();
            dataGridView1.CellClick += ClickDataGridView1Cell;
            _presentationModel._modelChanged += HandleInvalidate;
        }

        /// <summary>
        /// click insert btn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsertButtonClick(object sender, EventArgs e)
        {
            _presentationModel.InsertShape((ShapeType)(comboBox1.SelectedIndex));
        }
        
        /// <summary>
        /// click datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickDataGridView1Cell(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                _presentationModel.RemoveShape(e.RowIndex);
                // Debug.WriteLine(e.RowIndex.ToString());
            }
        }

        public void HandleCanvasPressed(object sender,
            System.Windows.Forms.MouseEventArgs e)
        {
            _presentationModel.PointerPressed(new PointD(e.X, e.Y));
            Debug.Print("press");
        }
        
        public void HandleCanvasReleased(object sender,
            System.Windows.Forms.MouseEventArgs e)
        {
            _presentationModel.PointerReleased(new PointD(e.X, e.Y));
            Debug.Print("release");
        }
        public void HandleCanvasMoved(object sender,
            System.Windows.Forms.MouseEventArgs e)
        {
            _presentationModel.PointerMoved(new PointD(e.X, e.Y));
        }
        public void HandleCanvasPaint(object sender,
            System.Windows.Forms.PaintEventArgs e)
        {
            _presentationModel.Draw(e.Graphics);
        }
        public void HandleInvalidate()
        {
            Invalidate(true);
        }

        public void HandleLineButtonClick()
        {
            _presentationModel.Type = ShapeType.LINE;
        }

        public void HandleRectangleButtonClick()
        {
            _presentationModel.Type = ShapeType.RECTANGLE;
        }

        public void HandleCircleButtonClick()
        {
            _presentationModel.Type = ShapeType.CIRCLE;
        }
    }
}
