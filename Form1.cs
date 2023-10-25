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
            _presentationModel._modelChanged += HandleModelChanged;            
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

        /// <summary>
        /// handle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleCanvasPressed(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _presentationModel.PressedPointer(new Point(e.X, e.Y));
            // Debug.Print("press");
        }
        
        /// <summary>
        /// handle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleCanvasReleased(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _presentationModel.ReleasedPointer(new Point(e.X, e.Y));
            ToolStripButton[] buttonArray = { lineButton, squareButton, circleButton }; 
            _presentationModel.HandleCanvasRelease(buttonArray);
            Cursor = Cursors.Arrow;
        }
        
        /// <summary>
        /// handle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleCanvasMoved(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _presentationModel.MovedPointer(new Point(e.X, e.Y));
        }
        
        /// <summary>
        /// handle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleCanvasPaint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            _presentationModel.Draw(e.Graphics);
        }

        /// <summary>
        /// handle
        /// </summary>
        public void HandleModelChanged()
        {
            Invalidate(true);
        }

        /// <summary>
        /// click
        /// </summary>
        public void HandleLineButtonClick(object sender, EventArgs e)
        {
            _presentationModel.Type = ShapeType.LINE;
            _presentationModel.IsDrawing = true;
            ToolStripButton[] buttonArray = { lineButton, squareButton, circleButton };
            _presentationModel.HandleButtonClick(buttonArray, (int)ShapeType.LINE);
            Cursor = Cursors.Cross;
        }

        /// <summary>
        /// click
        /// </summary>
        public void HandleRectangleButtonClick(object sender, EventArgs e)
        {
            _presentationModel.Type = ShapeType.RECTANGLE;
            _presentationModel.IsDrawing = true;
            ToolStripButton[] buttonArray = { lineButton, squareButton, circleButton };
            _presentationModel.HandleButtonClick(buttonArray, (int)ShapeType.RECTANGLE);
            Cursor = Cursors.Cross;
        }

        /// <summary>
        /// click
        /// </summary>
        public void HandleCircleButtonClick(object sender, EventArgs e)
        {
            _presentationModel.Type = ShapeType.CIRCLE;
            _presentationModel.IsDrawing = true;
            ToolStripButton[] buttonArray = { lineButton, squareButton, circleButton };
            _presentationModel.HandleButtonClick(buttonArray, (int)ShapeType.CIRCLE);
            Cursor = Cursors.Cross;
        }
    }
}
