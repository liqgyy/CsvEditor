using System.Drawing;

namespace CsvEditor
{
	partial class MainForm
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.m_MenuStrip = new System.Windows.Forms.MenuStrip();
			this.m_FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_OpenFIleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.m_SaveToSourceFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_SaveToCopyFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_SaveToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_CutEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_CopyEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_PasteEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_OpenCsvFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.m_SaveCsvFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.m_MainTabControl = new System.Windows.Forms.TabControl();
			this.m_RevertFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_MenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_MenuStrip
			// 
			this.m_MenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.m_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_FileToolStripMenuItem,
            this.m_EditToolStripMenuItem});
			this.m_MenuStrip.Location = new System.Drawing.Point(0, 0);
			this.m_MenuStrip.Name = "m_MenuStrip";
			this.m_MenuStrip.Size = new System.Drawing.Size(767, 25);
			this.m_MenuStrip.TabIndex = 1;
			this.m_MenuStrip.Text = "EenuStrip";
			// 
			// m_FileToolStripMenuItem
			// 
			this.m_FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_OpenFIleToolStripMenuItem,
            this.m_RevertFileToolStripMenuItem,
            this.toolStripSeparator1,
            this.m_SaveToSourceFileToolStripMenuItem,
            this.m_SaveToCopyFileToolStripMenuItem,
            this.m_SaveToFileToolStripMenuItem});
			this.m_FileToolStripMenuItem.Name = "m_FileToolStripMenuItem";
			this.m_FileToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
			this.m_FileToolStripMenuItem.Text = "文件";
			// 
			// m_OpenFIleToolStripMenuItem
			// 
			this.m_OpenFIleToolStripMenuItem.Name = "m_OpenFIleToolStripMenuItem";
			this.m_OpenFIleToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
			this.m_OpenFIleToolStripMenuItem.Text = "打开";
			this.m_OpenFIleToolStripMenuItem.Click += new System.EventHandler(this.OnOpenFileToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(223, 6);
			// 
			// m_SaveToSourceFileToolStripMenuItem
			// 
			this.m_SaveToSourceFileToolStripMenuItem.Name = "m_SaveToSourceFileToolStripMenuItem";
			this.m_SaveToSourceFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
			this.m_SaveToSourceFileToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
			this.m_SaveToSourceFileToolStripMenuItem.Text = "保存到源文件";
			this.m_SaveToSourceFileToolStripMenuItem.Click += new System.EventHandler(this.OnSaveFileToolStripMenuItem_Click);
			// 
			// m_SaveToCopyFileToolStripMenuItem
			// 
			this.m_SaveToCopyFileToolStripMenuItem.Name = "m_SaveToCopyFileToolStripMenuItem";
			this.m_SaveToCopyFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.m_SaveToCopyFileToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
			this.m_SaveToCopyFileToolStripMenuItem.Text = "保存到副本";
			this.m_SaveToCopyFileToolStripMenuItem.Click += new System.EventHandler(this.OnSaveFileToolStripMenuItem_Click);
			// 
			// m_SaveToFileToolStripMenuItem
			// 
			this.m_SaveToFileToolStripMenuItem.Name = "m_SaveToFileToolStripMenuItem";
			this.m_SaveToFileToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
			this.m_SaveToFileToolStripMenuItem.Text = "保存为";
			this.m_SaveToFileToolStripMenuItem.Click += new System.EventHandler(this.OnSaveFileToolStripMenuItem_Click);
			// 
			// m_EditToolStripMenuItem
			// 
			this.m_EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_CutEditToolStripMenuItem,
            this.m_CopyEditToolStripMenuItem,
            this.m_PasteEditToolStripMenuItem});
			this.m_EditToolStripMenuItem.Name = "m_EditToolStripMenuItem";
			this.m_EditToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
			this.m_EditToolStripMenuItem.Text = "编辑";
			// 
			// m_CutEditToolStripMenuItem
			// 
			this.m_CutEditToolStripMenuItem.Name = "m_CutEditToolStripMenuItem";
			this.m_CutEditToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.m_CutEditToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.m_CutEditToolStripMenuItem.Text = "剪切";
			this.m_CutEditToolStripMenuItem.Click += new System.EventHandler(this.OnEditToolStripMenuItem_Click);
			// 
			// m_CopyEditToolStripMenuItem
			// 
			this.m_CopyEditToolStripMenuItem.Name = "m_CopyEditToolStripMenuItem";
			this.m_CopyEditToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.m_CopyEditToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.m_CopyEditToolStripMenuItem.Text = "复制";
			this.m_CopyEditToolStripMenuItem.Click += new System.EventHandler(this.OnEditToolStripMenuItem_Click);
			// 
			// m_PasteEditToolStripMenuItem
			// 
			this.m_PasteEditToolStripMenuItem.Name = "m_PasteEditToolStripMenuItem";
			this.m_PasteEditToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.m_PasteEditToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.m_PasteEditToolStripMenuItem.Text = "粘贴";
			this.m_PasteEditToolStripMenuItem.Click += new System.EventHandler(this.OnEditToolStripMenuItem_Click);
			// 
			// m_OpenCsvFileDialog
			// 
			this.m_OpenCsvFileDialog.FileName = "OpenCsvFileDialog";
			this.m_OpenCsvFileDialog.Filter = "*.csv|*.csv";
			this.m_OpenCsvFileDialog.FilterIndex = 0;
			this.m_OpenCsvFileDialog.Multiselect = true;
			this.m_OpenCsvFileDialog.Title = "打开文件";
			// 
			// m_SaveCsvFileDialog
			// 
			this.m_SaveCsvFileDialog.DefaultExt = "csv";
			this.m_SaveCsvFileDialog.FileName = "NewCsvFile";
			this.m_SaveCsvFileDialog.Filter = "*.csv|*.csv";
			// 
			// m_MainTabControl
			// 
			this.m_MainTabControl.AllowDrop = true;
			this.m_MainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_MainTabControl.Location = new System.Drawing.Point(0, 22);
			this.m_MainTabControl.Margin = new System.Windows.Forms.Padding(0);
			this.m_MainTabControl.Name = "m_MainTabControl";
			this.m_MainTabControl.Padding = new System.Drawing.Point(10, 3);
			this.m_MainTabControl.SelectedIndex = 0;
			this.m_MainTabControl.Size = new System.Drawing.Size(767, 398);
			this.m_MainTabControl.TabIndex = 0;
			this.m_MainTabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.OnMainTabControl_Selected);
			this.m_MainTabControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnMainTabControl_MouseClick);
			// 
			// m_RevertFileToolStripMenuItem
			// 
			this.m_RevertFileToolStripMenuItem.Name = "m_RevertFileToolStripMenuItem";
			this.m_RevertFileToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
			this.m_RevertFileToolStripMenuItem.Text = "还原到副本";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(767, 420);
			this.Controls.Add(this.m_MainTabControl);
			this.Controls.Add(this.m_MenuStrip);
			this.MainMenuStrip = this.m_MenuStrip;
			this.Name = "MainForm";
			this.Text = "CsvEditor";
			this.Load += new System.EventHandler(this.OnMainForm_Load);
			this.m_MenuStrip.ResumeLayout(false);
			this.m_MenuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.MenuStrip m_MenuStrip;
		private System.Windows.Forms.ToolStripMenuItem m_FileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem m_OpenFIleToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog m_OpenCsvFileDialog;
        private System.Windows.Forms.ToolStripMenuItem m_EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_CutEditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_CopyEditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_PasteEditToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem m_SaveToSourceFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_SaveToCopyFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_SaveToFileToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog m_SaveCsvFileDialog;
        private System.Windows.Forms.TabControl m_MainTabControl;
		private System.Windows.Forms.ToolStripMenuItem m_RevertFileToolStripMenuItem;
	}
}

