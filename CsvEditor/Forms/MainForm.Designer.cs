using System.Drawing;

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
			this.m_RevertFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.m_SaveToSourceFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_SaveToCopyFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_SaveToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_GotoEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_SearchEditStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.m_UndoEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_RedoEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.m_CutEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_CopyEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_PasteEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.m_SettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_OpenCsvFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.m_SaveCsvFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.m_MainTabControl = new System.Windows.Forms.TabControl();
			this.SkinEngine = new Sunisoft.IrisSkin.SkinEngine();
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
			this.m_FileToolStripMenuItem.DropDownClosed += new System.EventHandler(this.OnTopToolStripMenuItem_DropDownClosed);
			this.m_FileToolStripMenuItem.DropDownOpening += new System.EventHandler(this.OnTopToolStripMenuItem_DropDownOpening);
			// 
			// m_OpenFIleToolStripMenuItem
			// 
			this.m_OpenFIleToolStripMenuItem.Name = "m_OpenFIleToolStripMenuItem";
			this.m_OpenFIleToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.m_OpenFIleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.m_OpenFIleToolStripMenuItem.Text = "打开";
			this.m_OpenFIleToolStripMenuItem.Click += new System.EventHandler(this.OnOpenFileToolStripMenuItem_Click);
			// 
			// m_RevertFileToolStripMenuItem
			// 
			this.m_RevertFileToolStripMenuItem.Name = "m_RevertFileToolStripMenuItem";
			this.m_RevertFileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.m_RevertFileToolStripMenuItem.Text = "还原到副本";
			this.m_RevertFileToolStripMenuItem.Visible = false;
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
			// 
			// m_SaveToSourceFileToolStripMenuItem
			// 
			this.m_SaveToSourceFileToolStripMenuItem.Name = "m_SaveToSourceFileToolStripMenuItem";
			this.m_SaveToSourceFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.m_SaveToSourceFileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.m_SaveToSourceFileToolStripMenuItem.Text = "保存";
			this.m_SaveToSourceFileToolStripMenuItem.Click += new System.EventHandler(this.OnSaveFileToolStripMenuItem_Click);
			// 
			// m_SaveToCopyFileToolStripMenuItem
			// 
			this.m_SaveToCopyFileToolStripMenuItem.Name = "m_SaveToCopyFileToolStripMenuItem";
			this.m_SaveToCopyFileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.m_SaveToCopyFileToolStripMenuItem.Text = "保存到副本";
			this.m_SaveToCopyFileToolStripMenuItem.Visible = false;
			this.m_SaveToCopyFileToolStripMenuItem.Click += new System.EventHandler(this.OnSaveFileToolStripMenuItem_Click);
			// 
			// m_SaveToFileToolStripMenuItem
			// 
			this.m_SaveToFileToolStripMenuItem.Name = "m_SaveToFileToolStripMenuItem";
			this.m_SaveToFileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.m_SaveToFileToolStripMenuItem.Text = "保存为";
			this.m_SaveToFileToolStripMenuItem.Click += new System.EventHandler(this.OnSaveFileToolStripMenuItem_Click);
			// 
			// m_EditToolStripMenuItem
			// 
			this.m_EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_GotoEditToolStripMenuItem,
            this.m_SearchEditStripMenuItem,
            this.toolStripSeparator2,
            this.m_UndoEditToolStripMenuItem,
            this.m_RedoEditToolStripMenuItem,
            this.toolStripSeparator4,
            this.m_CutEditToolStripMenuItem,
            this.m_CopyEditToolStripMenuItem,
            this.m_PasteEditToolStripMenuItem,
            this.toolStripSeparator3,
            this.m_SettingToolStripMenuItem});
			this.m_EditToolStripMenuItem.Name = "m_EditToolStripMenuItem";
			this.m_EditToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
			this.m_EditToolStripMenuItem.Text = "编辑";
			this.m_EditToolStripMenuItem.DropDownClosed += new System.EventHandler(this.OnTopToolStripMenuItem_DropDownClosed);
			this.m_EditToolStripMenuItem.DropDownOpening += new System.EventHandler(this.OnTopToolStripMenuItem_DropDownOpening);
			// 
			// m_GotoEditToolStripMenuItem
			// 
			this.m_GotoEditToolStripMenuItem.Name = "m_GotoEditToolStripMenuItem";
			this.m_GotoEditToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
			this.m_GotoEditToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.m_GotoEditToolStripMenuItem.Text = "转到";
			this.m_GotoEditToolStripMenuItem.Click += new System.EventHandler(this.OnGotoEditToolStripMenuItem_Click);
			// 
			// m_SearchEditStripMenuItem
			// 
			this.m_SearchEditStripMenuItem.Name = "m_SearchEditStripMenuItem";
			this.m_SearchEditStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
			this.m_SearchEditStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.m_SearchEditStripMenuItem.Text = "查找和替换";
			this.m_SearchEditStripMenuItem.Click += new System.EventHandler(this.OnSearchEditStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(176, 6);
			// 
			// m_UndoEditToolStripMenuItem
			// 
			this.m_UndoEditToolStripMenuItem.Name = "m_UndoEditToolStripMenuItem";
			this.m_UndoEditToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.m_UndoEditToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.m_UndoEditToolStripMenuItem.Text = "撤销";
			this.m_UndoEditToolStripMenuItem.Click += new System.EventHandler(this.OnEditToolStripMenuItem_Click);
			// 
			// m_RedoEditToolStripMenuItem
			// 
			this.m_RedoEditToolStripMenuItem.Name = "m_RedoEditToolStripMenuItem";
			this.m_RedoEditToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
			this.m_RedoEditToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.m_RedoEditToolStripMenuItem.Text = "重做";
			this.m_RedoEditToolStripMenuItem.Click += new System.EventHandler(this.OnEditToolStripMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(176, 6);
			// 
			// m_CutEditToolStripMenuItem
			// 
			this.m_CutEditToolStripMenuItem.Name = "m_CutEditToolStripMenuItem";
			this.m_CutEditToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.m_CutEditToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.m_CutEditToolStripMenuItem.Text = "剪切";
			this.m_CutEditToolStripMenuItem.Click += new System.EventHandler(this.OnEditToolStripMenuItem_Click);
			// 
			// m_CopyEditToolStripMenuItem
			// 
			this.m_CopyEditToolStripMenuItem.Name = "m_CopyEditToolStripMenuItem";
			this.m_CopyEditToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.m_CopyEditToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.m_CopyEditToolStripMenuItem.Text = "复制";
			this.m_CopyEditToolStripMenuItem.Click += new System.EventHandler(this.OnEditToolStripMenuItem_Click);
			// 
			// m_PasteEditToolStripMenuItem
			// 
			this.m_PasteEditToolStripMenuItem.Name = "m_PasteEditToolStripMenuItem";
			this.m_PasteEditToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.m_PasteEditToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.m_PasteEditToolStripMenuItem.Text = "粘贴";
			this.m_PasteEditToolStripMenuItem.Click += new System.EventHandler(this.OnEditToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(176, 6);
			// 
			// m_SettingToolStripMenuItem
			// 
			this.m_SettingToolStripMenuItem.Name = "m_SettingToolStripMenuItem";
			this.m_SettingToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.m_SettingToolStripMenuItem.Text = "首选项";
			this.m_SettingToolStripMenuItem.Click += new System.EventHandler(this.OnSettingToolStripMenuItem_Click);
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
			this.m_MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_MainTabControl.Location = new System.Drawing.Point(0, 25);
			this.m_MainTabControl.Margin = new System.Windows.Forms.Padding(0);
			this.m_MainTabControl.Name = "m_MainTabControl";
			this.m_MainTabControl.Padding = new System.Drawing.Point(10, 3);
			this.m_MainTabControl.SelectedIndex = 0;
			this.m_MainTabControl.Size = new System.Drawing.Size(767, 395);
			this.m_MainTabControl.TabIndex = 0;
			this.m_MainTabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.OnMainTabControl_Selected);
			this.m_MainTabControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnMainTabControl_MouseClick);
			// 
			// SkinEngine
			// 
			this.SkinEngine.@__DrawButtonFocusRectangle = true;
			this.SkinEngine.DisabledButtonTextColor = System.Drawing.Color.Gray;
			this.SkinEngine.DisabledMenuFontColor = System.Drawing.SystemColors.GrayText;
			this.SkinEngine.InactiveCaptionColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.SkinEngine.SerialNumber = "";
			this.SkinEngine.SkinFile = null;
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
			this.Text = "Csv编辑器";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnForm_FormClosing);
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
    private System.Windows.Forms.ToolStripMenuItem m_GotoEditToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem m_SearchEditStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripMenuItem m_SettingToolStripMenuItem;
    public Sunisoft.IrisSkin.SkinEngine SkinEngine;
    private System.Windows.Forms.ToolStripMenuItem m_UndoEditToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem m_RedoEditToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
}