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
			this.m_InsertUpRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_InsertDownRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
			this.m_DataGridView.Size = new System.Drawing.Size(946, 603);
			this.m_DataGridView.TabIndex = 0;
			this.m_DataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnDataGridView_CellEndEdit);
			this.m_DataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.OnDataGridView_CellMouseDown);
			this.m_DataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.OnDataGridView_ColumnHeaderMouseClick);
			this.m_DataGridView.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.OnDataGridView_RowHeaderMouseClick);
			// 
			// m_GridContextMenuStrip
			// 
			this.m_GridContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.m_GridContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_InsertUpRowToolStripMenuItem,
            this.m_InsertDownRowToolStripMenuItem});
			this.m_GridContextMenuStrip.Name = "m_GridContextMenuStrip";
			this.m_GridContextMenuStrip.Size = new System.Drawing.Size(149, 48);
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
        private System.Windows.Forms.ContextMenuStrip m_GridContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem m_InsertUpRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_InsertDownRowToolStripMenuItem;
		private System.Windows.Forms.DataGridView m_DataGridView;
	}
}