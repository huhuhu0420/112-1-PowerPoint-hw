﻿using System.Runtime.Remoting.Channels;
using System.Windows.Forms;
using WindowPowerPoint;

namespace PowerPoint
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.shapeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.information = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.slide1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.InsertButton = new System.Windows.Forms.Button();
            this.InformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.lineButton = new BindingToolStripButton();
            this.squareButton = new BindingToolStripButton();
            this.circleButton = new BindingToolStripButton();
            this.mouseButton = new BindingToolStripButton();
            this.panel1 = new PowerPoint.DoubleBufferedPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.delete, this.shapeName, this.information });
            this.dataGridView1.Location = new System.Drawing.Point(666, 90);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(254, 407);
            this.dataGridView1.TabIndex = 0;
            // 
            // delete
            // 
            this.delete.HeaderText = "刪除";
            this.delete.MinimumWidth = 6;
            this.delete.Name = "delete";
            this.delete.ReadOnly = true;
            this.delete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.delete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.delete.Text = "刪除";
            this.delete.UseColumnTextForButtonValue = true;
            this.delete.Width = 65;
            // 
            // shapeName
            // 
            this.shapeName.DataPropertyName = "ShapeName";
            this.shapeName.HeaderText = "形狀";
            this.shapeName.MinimumWidth = 6;
            this.shapeName.Name = "shapeName";
            this.shapeName.ReadOnly = true;
            this.shapeName.Width = 65;
            // 
            // information
            // 
            this.information.DataPropertyName = "Info";
            this.information.HeaderText = "資訊";
            this.information.MinimumWidth = 6;
            this.information.Name = "information";
            this.information.ReadOnly = true;
            this.information.Width = 125;
            // 
            // slide1
            // 
            this.slide1.Location = new System.Drawing.Point(12, 53);
            this.slide1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.slide1.Name = "slide1";
            this.slide1.Size = new System.Drawing.Size(148, 107);
            this.slide1.TabIndex = 1;
            this.slide1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] { "線", "矩形", "圓形" });
            this.comboBox1.Location = new System.Drawing.Point(788, 56);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(110, 26);
            this.comboBox1.TabIndex = 3;
            // 
            // InsertButton
            // 
            this.InsertButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.InsertButton.Location = new System.Drawing.Point(685, 53);
            this.InsertButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.InsertButton.Name = "InsertButton";
            this.InsertButton.Size = new System.Drawing.Size(55, 29);
            this.InsertButton.TabIndex = 4;
            this.InsertButton.Text = "新增";
            this.InsertButton.UseVisualStyleBackColor = true;
            this.InsertButton.Click += new System.EventHandler(this.InsertButtonClick);
            // 
            // InformationToolStripMenuItem
            // 
            this.InformationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.AboutToolStripMenuItem });
            this.InformationToolStripMenuItem.Name = "InformationToolStripMenuItem";
            this.InformationToolStripMenuItem.Size = new System.Drawing.Size(52, 24);
            this.InformationToolStripMenuItem.Text = "說明";
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.AboutToolStripMenuItem.Text = "關於";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.InformationToolStripMenuItem });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(932, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.lineButton, this.squareButton, this.circleButton, this.mouseButton });
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(932, 27);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // lineButton
            // 
            this.lineButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lineButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.lineButton.Name = "lineButton";
            this.lineButton.Size = new System.Drawing.Size(34, 24);
            this.lineButton.Text = "📏";
            // 
            // squareButton
            // 
            this.squareButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.squareButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.squareButton.Name = "squareButton";
            this.squareButton.Size = new System.Drawing.Size(26, 24);
            this.squareButton.Text = "⬜";
            // 
            // circleButton
            // 
            this.circleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.circleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.circleButton.Name = "circleButton";
            this.circleButton.Size = new System.Drawing.Size(26, 24);
            this.circleButton.Text = "○";
            // 
            // mouseButton
            // 
            this.mouseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mouseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mouseButton.Name = "mouseButton";
            this.mouseButton.Size = new System.Drawing.Size(28, 24);
            this.mouseButton.Text = "🖱";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Location = new System.Drawing.Point(166, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(494, 442);
            this.panel1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 510);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.InsertButton);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.slide1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private BindingToolStripButton mouseButton;

        private PowerPoint.DoubleBufferedPanel panel1;

        private BindingToolStripButton lineButton;

        private BindingToolStripButton squareButton;

        private BindingToolStripButton circleButton;

        private System.Windows.Forms.ToolStrip toolStrip1;

        private System.Windows.Forms.Button InsertButton;

        private System.Windows.Forms.ComboBox comboBox1;

        private System.Windows.Forms.ToolStripMenuItem InformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;

        private System.Windows.Forms.MenuStrip menuStrip1;

        private System.Windows.Forms.Button slide1;

        private System.Windows.Forms.DataGridView dataGridView1;

        private System.Windows.Forms.DataGridViewButtonColumn delete;
        
        private System.Windows.Forms.DataGridViewTextBoxColumn shapeName;
        
        private System.Windows.Forms.DataGridViewTextBoxColumn information;
        #endregion
        
        private PresentationModel.PresentationModel _presentationModel = new PresentationModel.PresentationModel();
    }
}

