namespace CsvEditor
{
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
			this.m_DataGridView = new System.Windows.Forms.DataGridView();
			this.m_GridContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.在上方插入行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.在下方插入行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.删除选中行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.m_DataGridView)).BeginInit();
			this.m_GridContextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_DataGridView
			// 
			this.m_DataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_DataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
			this.m_DataGridView.ContextMenuStrip = this.m_GridContextMenuStrip;
			this.m_DataGridView.Location = new System.Drawing.Point(-1, -1);
			this.m_DataGridView.Name = "m_DataGridView";
			this.m_DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect;
			this.m_DataGridView.Size = new System.Drawing.Size(946, 603);
			this.m_DataGridView.TabIndex = 0;
			this.m_DataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnDataGridView_CellEndEdit);
			this.m_DataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.OnDataGridView_CellMouseDown);
			// 
			// m_GridContextMenuStrip
			// 
			this.m_GridContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.m_GridContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.在上方插入行ToolStripMenuItem,
            this.在下方插入行ToolStripMenuItem,
            this.删除选中行ToolStripMenuItem});
			this.m_GridContextMenuStrip.Name = "m_GridContextMenuStrip";
			this.m_GridContextMenuStrip.Size = new System.Drawing.Size(149, 70);
			// 
			// 在上方插入行ToolStripMenuItem
			// 
			this.在上方插入行ToolStripMenuItem.Name = "在上方插入行ToolStripMenuItem";
			this.在上方插入行ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.在上方插入行ToolStripMenuItem.Text = "在上方插入行";
			// 
			// 在下方插入行ToolStripMenuItem
			// 
			this.在下方插入行ToolStripMenuItem.Name = "在下方插入行ToolStripMenuItem";
			this.在下方插入行ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.在下方插入行ToolStripMenuItem.Text = "在下方插入行";
			// 
			// 删除选中行ToolStripMenuItem
			// 
			this.删除选中行ToolStripMenuItem.Name = "删除选中行ToolStripMenuItem";
			this.删除选中行ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.删除选中行ToolStripMenuItem.Text = "删除选中行";
			// 
			// CsvForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(944, 601);
			this.Controls.Add(this.m_DataGridView);
			this.Name = "CsvForm";
			this.Text = "CsvForm";
			this.Load += new System.EventHandler(this.OnCsvForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.m_DataGridView)).EndInit();
			this.m_GridContextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

        #endregion

        private System.Windows.Forms.DataGridView m_DataGridView;
        private System.Windows.Forms.ContextMenuStrip m_GridContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 在上方插入行ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 在下方插入行ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除选中行ToolStripMenuItem;
    }
}