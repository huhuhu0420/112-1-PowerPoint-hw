using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerPoint
{
    public partial class Form1 : Form
    {
        public Form1(Model model)
        {
            _model = model;
            _presentationModel = new PresentationModel.PresentationModel(_model);
            InitializeComponent();
            HandleSomething();
            HandleMore();
            HandleContainerResize();
        }

        /// <summary>
        /// handle
        /// </summary>
        public void HandleSomething()
        {
            dataGridView1.DataSource = _presentationModel.GetShapes();
            dataGridView1.CellClick += ClickDataGridView1Cell;
            _presentationModel._modelChanged += HandleModelChanged;            
            _presentationModel._cursorChanged += SetCursor;
            _presentationModel._undoRedoHistoryChanged += HandleUndoRedoButton;
            this.lineButton.Click += HandleLineButtonClick;
            this.squareButton.Click += HandleRectangleButtonClick;
            this.circleButton.Click += HandleCircleButtonClick;
            this.mouseButton.Click += HandleMouseButtonClick;
            this.undoButton.Click += HandleUndoButtonClick;
            this.redoButton.Click += HandleRedoButtonClick;
            this.panel1.MouseDown += HandleCanvasPressed;
            this.panel1.MouseUp += HandleCanvasReleased;
            this.panel1.MouseMove += HandleCanvasMoved;
            this.panel1.Paint += HandleCanvasPaint;
            this.KeyDown += FormKeyDown;
        }

        /// <summary>
        /// handle
        /// </summary>
        public void HandleMore()
        {
            lineButton.DataBindings.Add(Constant.CHECKED, _presentationModel, Constant.IS_LINE_CHECKED);
            squareButton.DataBindings.Add(Constant.CHECKED, _presentationModel, Constant.IS_RECTANGLE_CHECKED);
            circleButton.DataBindings.Add(Constant.CHECKED, _presentationModel, Constant.IS_CIRCLE_CHECKED);
            mouseButton.DataBindings.Add(Constant.CHECKED, _presentationModel, Constant.IS_MOUSE_CHECKED);
            _brief = new Bitmap(this.panel1.Width, this.panel1.Height);
            splitContainer1.Panel1.Resize += (sender, e) => HandleContainerResize();
            splitContainer1.Resize += (sender, e) => HandleContainerResize();
            splitContainer2.Panel1.Resize += (sender, args) => HandleContainerResize();
            splitContainer2.Resize += (sender, args) => HandleContainerResize();
            SizeChanged += (sender, args) => HandleContainerResize();
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.Columns[Constant.TWO].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            HandleUndoRedoButton(false, false);
            _dialog = new DialogForm();
        }
        
        /// <summary>
        /// handle resize
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleContainerResize()
        {
            slide1.Width = splitContainer1.Panel1.Width - Constant.EIGHT;
            slide1.Height = (int)(slide1.Width * Constant.RATIO);
            panel1.Width = splitContainer2.Panel1.Width - Constant.EIGHT;
            panel1.Height = (int)(panel1.Width * Constant.RATIO);
            if (splitContainer2.Panel1.Height < panel1.Height)
            {
                panel1.Height = splitContainer2.Panel1.Height - Constant.EIGHT;
                panel1.Width = (int)(panel1.Height / Constant.RATIO) - Constant.EIGHT;
                panel1.Height = (int)(panel1.Width * Constant.RATIO);
            }
            if (splitContainer1.Panel1.Height < slide1.Height)
            {
                slide1.Height = splitContainer1.Panel1.Height;
                slide1.Width = (int)(slide1.Height / Constant.RATIO) - Constant.EIGHT;
                slide1.Height = (int)(slide1.Width * Constant.RATIO);
            }
            _presentationModel.SetCanvasSize(panel1.Width, panel1.Height);
            _dialog.SetCanvasSize(new Size(panel1.Width, panel1.Height));
        }
        
        /// <summary>
        /// handle
        /// </summary>
        /// <param name="isUndo"></param>
        /// <param name="isRedo"></param>
        public void HandleUndoRedoButton(bool isUndo, bool isRedo)
        {
            undoButton.Enabled = isUndo;
            redoButton.Enabled = isRedo;
        }
        
        /// <summary>
        /// key down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                _presentationModel.DeleteShape();
            }
        }
        
        /// <summary>
        /// brief
        /// </summary>
        private void GenerateBrief()
        {
            _brief = new Bitmap(this.panel1.Width, this.panel1.Height);
            this.panel1.DrawToBitmap(_brief, new System.Drawing.Rectangle(0, 0, this.panel1.Width, this.panel1.Height));
            slide1.Image = new Bitmap(_brief, slide1.Size);
        }

        /// <summary>
        /// click insert btn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsertButtonClick(object sender, EventArgs e)
        {
            _dialog.ShowDialog();
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
            _presentationModel.PressedPointer(new PointF(e.X, e.Y));
            // Debug.Print("press");
        }
        
        /// <summary>
        /// handle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleCanvasReleased(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _presentationModel.ReleasedPointer(new PointF(e.X, e.Y));
        }
        
        /// <summary>
        /// handle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleCanvasMoved(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _presentationModel.MovedPointer(new PointF(e.X, e.Y));
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
            GenerateBrief();
        }
        
        /// <summary>
        /// set
        /// </summary>
        /// <param name="cursor"></param>
        public void SetCursor(Cursor cursor)
        {
            Cursor = cursor;
        }

        /// <summary>
        /// click
        /// </summary>
        public void HandleLineButtonClick(object sender, EventArgs e)
        {
            _presentationModel.HandleButtonClick((int)ShapeType.LINE);
        }

        /// <summary>
        /// click
        /// </summary>
        public void HandleRectangleButtonClick(object sender, EventArgs e)
        {
            _presentationModel.HandleButtonClick((int)ShapeType.RECTANGLE);
        }

        /// <summary>
        /// click
        /// </summary>
        public void HandleCircleButtonClick(object sender, EventArgs e)
        {
            _presentationModel.HandleButtonClick((int)ShapeType.CIRCLE);
        }

        /// <summary>
        /// click
        /// </summary>
        public void HandleMouseButtonClick(object sender, EventArgs e)
        {
            _presentationModel.HandleButtonClick((int)ShapeType.ARROW);
        }
        
        /// <summary>
        /// undo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleUndoButtonClick(object sender, EventArgs e)
        {
            _presentationModel.Undo();
        }
        
        /// <summary>
        /// redo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleRedoButtonClick(object sender, EventArgs e)
        {
            _presentationModel.Redo();
        }

        private Bitmap _brief;
    }
}
