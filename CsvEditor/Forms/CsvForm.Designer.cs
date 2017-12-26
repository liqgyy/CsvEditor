partial class CsvForm
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.m_GridContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.m_InsertUpRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_InsertDownRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.m_FrozenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_UnFrozenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.m_AddColWidthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_AddRowHeightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.m_EditNoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_DataGridView = new System.Windows.Forms.DataGridView();
			this.m_GridContextMenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_DataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// m_GridContextMenuStrip
			// 
			this.m_GridContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.m_GridContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_InsertUpRowToolStripMenuItem,
            this.m_InsertDownRowToolStripMenuItem,
            this.toolStripSeparator1,
            this.m_FrozenToolStripMenuItem,
            this.m_UnFrozenToolStripMenuItem,
            this.toolStripSeparator2,
            this.m_AddColWidthToolStripMenuItem,
            this.m_AddRowHeightToolStripMenuItem,
            this.toolStripSeparator3,
            this.m_EditNoteToolStripMenuItem});
			this.m_GridContextMenuStrip.Name = "m_GridContextMenuStrip";
			this.m_GridContextMenuStrip.Size = new System.Drawing.Size(149, 176);
			// 
			// m_InsertUpRowToolStripMenuItem
			// 
			this.m_InsertUpRowToolStripMenuItem.Name = "m_InsertUpRowToolStripMenuItem";
			this.m_InsertUpRowToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.m_InsertUpRowToolStripMenuItem.Text = "在上方插入行";
			this.m_InsertUpRowToolStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnInsertRowToolStripMenuItem_MouseDown);
			// 
			// m_InsertDownRowToolStripMenuItem
			// 
			this.m_InsertDownRowToolStripMenuItem.Name = "m_InsertDownRowToolStripMenuItem";
			this.m_InsertDownRowToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.m_InsertDownRowToolStripMenuItem.Text = "在下方插入行";
			this.m_InsertDownRowToolStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnInsertRowToolStripMenuItem_MouseDown);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
			// 
			// m_FrozenToolStripMenuItem
			// 
			this.m_FrozenToolStripMenuItem.Name = "m_FrozenToolStripMenuItem";
			this.m_FrozenToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.m_FrozenToolStripMenuItem.Text = "冻结";
			this.m_FrozenToolStripMenuItem.Click += new System.EventHandler(this.OnFrozenToolStripMenuItem_Click);
			// 
			// m_UnFrozenToolStripMenuItem
			// 
			this.m_UnFrozenToolStripMenuItem.Name = "m_UnFrozenToolStripMenuItem";
			this.m_UnFrozenToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.m_UnFrozenToolStripMenuItem.Text = "解除冻结";
			this.m_UnFrozenToolStripMenuItem.Click += new System.EventHandler(this.OnFrozenToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
			// 
			// m_AddColWidthToolStripMenuItem
			// 
			this.m_AddColWidthToolStripMenuItem.Name = "m_AddColWidthToolStripMenuItem";
			this.m_AddColWidthToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.m_AddColWidthToolStripMenuItem.Text = "增加列宽";
			this.m_AddColWidthToolStripMenuItem.Click += new System.EventHandler(this.OnAddCellSizeToolStripMenuItem_Click);
			// 
			// m_AddRowHeightToolStripMenuItem
			// 
			this.m_AddRowHeightToolStripMenuItem.Name = "m_AddRowHeightToolStripMenuItem";
			this.m_AddRowHeightToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.m_AddRowHeightToolStripMenuItem.Text = "增加行高";
			this.m_AddRowHeightToolStripMenuItem.Click += new System.EventHandler(this.OnAddCellSizeToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(145, 6);
			// 
			// m_EditNoteToolStripMenuItem
			// 
			this.m_EditNoteToolStripMenuItem.Name = "m_EditNoteToolStripMenuItem";
			this.m_EditNoteToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.m_EditNoteToolStripMenuItem.Text = "批注";
			this.m_EditNoteToolStripMenuItem.Visible = false;
			this.m_EditNoteToolStripMenuItem.Click += new System.EventHandler(this.OnEditNoteToolStripMenuItem_Click);
			// 
			// m_DataGridView
			// 
			this.m_DataGridView.AllowUserToAddRows = false;
			this.m_DataGridView.AllowUserToDeleteRows = false;
			this.m_DataGridView.AllowUserToResizeRows = false;
			this.m_DataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.m_DataGridView.ContextMenuStrip = this.m_GridContextMenuStrip;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.m_DataGridView.DefaultCellStyle = dataGridViewCellStyle1;
			this.m_DataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_DataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
			this.m_DataGridView.Location = new System.Drawing.Point(0, 0);
			this.m_DataGridView.Name = "m_DataGridView";
			this.m_DataGridView.ReadOnly = true;
			this.m_DataGridView.RowHeadersWidth = 60;
			this.m_DataGridView.Size = new System.Drawing.Size(876, 384);
			this.m_DataGridView.TabIndex = 0;
			this.m_DataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.OnDataGridView_CellMouseDown);
			this.m_DataGridView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.OnDataGridView_CellPainting);
			this.m_DataGridView.SelectionChanged += new System.EventHandler(this.OnDataGridView_SelectionChanged);
			// 
			// CsvForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(876, 384);
			this.Controls.Add(this.m_DataGridView);
			this.DoubleBuffered = true;
			this.Name = "CsvForm";
			this.Text = "CsvForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnForm_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnForm_FormClosed);
			this.Load += new System.EventHandler(this.OnCsvForm_Load);
			this.Shown += new System.EventHandler(this.OnForm_Shown);
			this.m_GridContextMenuStrip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.m_DataGridView)).EndInit();
			this.ResumeLayout(false);

    }

	#endregion
    private System.Windows.Forms.ContextMenuStrip m_GridContextMenuStrip;
    private System.Windows.Forms.ToolStripMenuItem m_InsertUpRowToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem m_InsertDownRowToolStripMenuItem;
    private System.Windows.Forms.DataGridView m_DataGridView;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem m_FrozenToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem m_UnFrozenToolStripMenuItem;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
	private System.Windows.Forms.ToolStripMenuItem m_AddColWidthToolStripMenuItem;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
	private System.Windows.Forms.ToolStripMenuItem m_EditNoteToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem m_AddRowHeightToolStripMenuItem;
}