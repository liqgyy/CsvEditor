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
            this.m_UnFrozenToolStripMenuItem});
            this.m_GridContextMenuStrip.Name = "m_GridContextMenuStrip";
            this.m_GridContextMenuStrip.Size = new System.Drawing.Size(169, 106);
            // 
            // m_InsertUpRowToolStripMenuItem
            // 
            this.m_InsertUpRowToolStripMenuItem.Name = "m_InsertUpRowToolStripMenuItem";
            this.m_InsertUpRowToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.m_InsertUpRowToolStripMenuItem.Text = "在上方插入行";
            this.m_InsertUpRowToolStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnInsertRowToolStripMenuItem_MouseDown);
            // 
            // m_InsertDownRowToolStripMenuItem
            // 
            this.m_InsertDownRowToolStripMenuItem.Name = "m_InsertDownRowToolStripMenuItem";
            this.m_InsertDownRowToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.m_InsertDownRowToolStripMenuItem.Text = "在下方插入行";
            this.m_InsertDownRowToolStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnInsertRowToolStripMenuItem_MouseDown);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(165, 6);
            // 
            // m_FrozenToolStripMenuItem
            // 
            this.m_FrozenToolStripMenuItem.Name = "m_FrozenToolStripMenuItem";
            this.m_FrozenToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.m_FrozenToolStripMenuItem.Text = "锁定";
            this.m_FrozenToolStripMenuItem.Click += new System.EventHandler(this.OnFrozenToolStripMenuItem_Click);
            // 
            // m_UnFrozenToolStripMenuItem
            // 
            this.m_UnFrozenToolStripMenuItem.Name = "m_UnFrozenToolStripMenuItem";
            this.m_UnFrozenToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.m_UnFrozenToolStripMenuItem.Text = "解除锁定";
            this.m_UnFrozenToolStripMenuItem.Click += new System.EventHandler(this.OnFrozenToolStripMenuItem_Click);
            // 
            // m_DataGridView
            // 
            this.m_DataGridView.AllowUserToOrderColumns = true;
            this.m_DataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.m_DataGridView.ContextMenuStrip = this.m_GridContextMenuStrip;
            this.m_DataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_DataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.m_DataGridView.Location = new System.Drawing.Point(0, 0);
            this.m_DataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.m_DataGridView.Name = "m_DataGridView";
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.m_DataGridView.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.m_DataGridView.Size = new System.Drawing.Size(1259, 751);
            this.m_DataGridView.TabIndex = 0;
            this.m_DataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.OnDataGridView_CellMouseDown);
            // 
            // CsvForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1259, 751);
            this.Controls.Add(this.m_DataGridView);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CsvForm";
            this.Text = "CsvForm";
            this.Load += new System.EventHandler(this.OnCsvForm_Load);
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
}