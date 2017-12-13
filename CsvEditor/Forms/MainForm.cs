using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class MainForm : Form
{
    public static MainForm Instance;

    private List<CsvForm> m_OpenedCsvFormList = new List<CsvForm>();
    /// <summary>
    /// 当前Csv窗口，只能通过SetSelCsvForm赋值
    /// </summary>
    public CsvForm SelCsvForm { get; private set; }

    private GotoForm m_GotoForm;
    private SearchForm m_SearchForm;

    public MainForm()
    {
        Instance = this;
        InitializeComponent();

        Setting setting = Setting.Instance;

		BeyondCompare.Instance.AutoExePathToSetting();
		CodeCompare.Instance.AutoExePathToSetting();

		SkinUtility.SetSkin();
    }

	/// <summary>
	/// 当前选中的csv是否存在且完成初始化
	/// </summary>
	/// <returns></returns>
    public bool SelCsvFormInitialized()
    {
        if (SelCsvForm == null)
        {
            return false;
        }
        return SelCsvForm.Initialized;
    }

	/// <summary>
	/// 更新Tab上的标题
	/// 文件没有保存到源文件就在开头加 ?
	/// 文件有改动就在结尾加 *
	/// </summary>
	public void UpdateAllTabControlTabPageText()
    {
        for (int tabPageIdx = 0; tabPageIdx < m_MainTabControl.TabPages.Count; tabPageIdx++)
        {
            TabPage tabPage = m_MainTabControl.TabPages[tabPageIdx];
            CsvForm csvForm = (CsvForm)tabPage.Controls[0];
            tabPage.Text = csvForm.Text;
        }
    }

	/// <summary>
	/// 关闭csv窗口
	/// </summary>
	/// <param name="tabIdx">窗口id</param>
	private void CloseCsvForm(int tabIdx)
	{
		CsvForm csvForm = (CsvForm)m_MainTabControl.TabPages[tabIdx].Controls[0];
		if (CloseCsvForm(csvForm))
		{
			m_MainTabControl.TabPages.RemoveAt(tabIdx);
		}
	}

	/// <summary>
	/// 关闭csv窗口
	/// </summary>
	private bool CloseCsvForm(CsvForm csvForm)
    {
        if (csvForm.DataChanged)
        {
			if (MessageBox.Show("文件未保存，确定关闭?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
			{
				// 防止用户误点，即使选择关闭也保存一份副本
				//csvForm.SaveToCopyFile();
				return false;
			}
			else
			{
				if (csvForm.CopyFileNameList.Count > 0)
				{
					csvForm.RevertToCopyFile(csvForm.CopyFileNameList[csvForm.CopyFileNameList.Count - 1]);
				}
				else
				{
					csvForm.RevertToCopyFile(csvForm.SourceCopyFileName);
				}
			}
		}
		if (csvForm.NeedSaveSourceFile)
		{
			csvForm.SaveToSourceFile();
			return false;
		}
		m_OpenedCsvFormList.Remove(csvForm);
		csvForm.Close();
		return true;
	}

	/// <summary>
	/// 加载csv文件
	/// </summary>
	/// <param name="fileFullPath">文件完整路径</param>
    private void LoadFile(string fileFullPath)
    {
        CsvForm newCsvForm = new CsvForm(fileFullPath);
        if (newCsvForm == null)
        {
            return;
        }
        newCsvForm.TopLevel = false;
        newCsvForm.Visible = true;
        newCsvForm.FormBorderStyle = FormBorderStyle.None;
        newCsvForm.Dock = DockStyle.Fill;

		m_MainTabControl.TabPages.Add(newCsvForm.Text + "  ");
		int tabIdx = m_MainTabControl.TabPages.Count - 1;
		m_MainTabControl.TabPages[tabIdx].Controls.Add(newCsvForm);
		m_MainTabControl.SelectTab(tabIdx);

		m_OpenedCsvFormList.Add(newCsvForm);
        SetSelCsvForm(newCsvForm);
    }

    private void SetSelCsvForm(CsvForm csvForm)
    {
        SelCsvForm = csvForm;

		// 所有Csv窗口都被关闭
		if (SelCsvForm == null)
		{
			if (m_GotoForm != null && !m_GotoForm.IsDisposed)
			{
				m_GotoForm.Close();
			}
			if (m_SearchForm != null && !m_SearchForm.IsDisposed)
			{
				m_SearchForm.Close();
			}
		}
	}

    #region Update ToolStripMenu
    private void UpdateFileToolStripMenu()
    {
        m_SaveToSourceFileToolStripMenuItem.Enabled = false;
        m_SaveToCopyFileToolStripMenuItem.Enabled = false;
        m_SaveToFileToolStripMenuItem.Enabled = false;
        if (!SelCsvFormInitialized())
        {
            return;
        }
        m_SaveToCopyFileToolStripMenuItem.Enabled = SelCsvForm.DataChanged;
        m_SaveToSourceFileToolStripMenuItem.Enabled = SelCsvForm.NeedSaveSourceFile && !SelCsvForm.DataChanged;
        m_SaveToFileToolStripMenuItem.Enabled = true;
    }

    private void UpdateFileRevertToolStripMenu()
    {
        m_RevertFileToolStripMenuItem.Enabled = false;
        if (!SelCsvFormInitialized() || (string.IsNullOrEmpty(SelCsvForm.SourceCopyFileName) && SelCsvForm.CopyFileNameList.Count == 0))
        {
            return;
        }
        m_RevertFileToolStripMenuItem.Enabled = true;
        m_RevertFileToolStripMenuItem.DropDownItems.Clear();
        for (int copyFileIdx = 0; copyFileIdx < SelCsvForm.CopyFileNameList.Count; copyFileIdx++)
        {
            string copyFileName = SelCsvForm.CopyFileNameList[copyFileIdx];
            AddMenumItemToRevertFileToolStripMenu(copyFileName);
        }
        if (!string.IsNullOrEmpty(SelCsvForm.SourceCopyFileName))
        {
            AddMenumItemToRevertFileToolStripMenu(SelCsvForm.SourceCopyFileName);
        }
    }
   
    private void UpdateEditToolStripMenu()
    {
        m_GotoEditToolStripMenuItem.Enabled = false;
        m_SearchEditStripMenuItem.Enabled = false;
        m_UndoEditToolStripMenuItem.Enabled = false;
        m_RedoEditToolStripMenuItem.Enabled = false;
        m_CopyEditToolStripMenuItem.Enabled = false;
        m_CutEditToolStripMenuItem.Enabled = false;
        m_PasteEditToolStripMenuItem.Enabled = false;
        if (!SelCsvFormInitialized())
        {
            return;
        }

        m_GotoEditToolStripMenuItem.Enabled = true;
        m_SearchEditStripMenuItem.Enabled = true;

        m_CopyEditToolStripMenuItem.Enabled = SelCsvForm.EditManager.CanCopy();
        m_CutEditToolStripMenuItem.Enabled = SelCsvForm.EditManager.CanCut();
        m_PasteEditToolStripMenuItem.Enabled = SelCsvForm.EditManager.CanPaste();

        m_UndoEditToolStripMenuItem.Enabled = SelCsvForm.EditManager.CanUndo();
        m_RedoEditToolStripMenuItem.Enabled = SelCsvForm.EditManager.CanRedo();
    }

	/// <summary>
	/// 添加一个副本文件名到还原文件菜单里
	/// </summary>
	private void AddMenumItemToRevertFileToolStripMenu(string copyFileName)
	{
		ToolStripMenuItem newToolStripMenuItem = new ToolStripMenuItem();
		newToolStripMenuItem.Name = copyFileName;
		newToolStripMenuItem.Text = copyFileName;
		newToolStripMenuItem.Click += new EventHandler(OnRevertFileToolStripMenuItem_Click);

		m_RevertFileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem });
	}
	#endregion // End Update ToolStripMenu

	#region UIEvent
	/// <summary>
	/// 窗口加载时检查cmd传入的参数
	/// </summary>
	private void OnMainForm_Load(object sender, EventArgs e)
    {
        SetSelCsvForm(null);

        string[] commands = Environment.GetCommandLineArgs();
        // 关联csv文件
        if (commands != null && commands.Length > 1)
        {
            for (int argIdx = 1; argIdx < commands.Length; argIdx++)
            {
                LoadFile(commands[argIdx]);
            }
        }
    }

	private void OnForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		for(int formIdx = 0; formIdx < m_OpenedCsvFormList.Count; formIdx++)
		{
			CsvForm csvForm = m_OpenedCsvFormList[formIdx];
			if (!csvForm.CanClose())
			{
				MessageBox.Show("有Csv窗口未关闭", "提示", MessageBoxButtons.OK);
				e.Cancel = true;
				break;
			}
		}
	}

	/// <summary>
	/// 鼠标中键点击Tab时关闭Csv窗口
	/// </summary>
	private void OnMainTabControl_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Middle)
        {
            for (int tabIdx = 0; tabIdx < m_MainTabControl.TabCount; tabIdx++)
            {
                Rectangle r = m_MainTabControl.GetTabRect(tabIdx);
                if (r.Contains(e.Location))
                {
                    CloseCsvForm(tabIdx);
                    break;
                }
            }
        }
    }

	/// <summary>
	/// 切换当前csv窗口
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
    private void OnMainTabControl_Selected(object sender, TabControlEventArgs e)
    {
        if (m_MainTabControl.TabPages.Count == 0)
        {
            SetSelCsvForm(null);
        }
        else
        {
            SetSelCsvForm((CsvForm)m_MainTabControl.TabPages[m_MainTabControl.SelectedIndex].Controls[0]);
        }
    }

	/// <summary>
	/// 顶层菜单打开时，更新菜单里的菜单项状态
	/// TODO 优化逻辑
	/// </summary>
	private void OnTopToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
	{
		ToolStripMenuItem item = (ToolStripMenuItem)sender;
		if (item == m_FileToolStripMenuItem)
		{
			UpdateFileToolStripMenu();
			UpdateFileRevertToolStripMenu();
		}
		else if (item == m_EditToolStripMenuItem)
		{
			UpdateEditToolStripMenu();
		}
	}

	/// <summary>
	/// 顶层菜单关闭时，显示菜单里的所有菜单项
	/// TODO 优化逻辑
	/// </summary>
	private void OnTopToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
	{
		ToolStripMenuItem item = (ToolStripMenuItem)sender;
		if (item == m_FileToolStripMenuItem)
		{
			m_SaveToSourceFileToolStripMenuItem.Enabled = true;
			m_SaveToCopyFileToolStripMenuItem.Enabled = true;
			m_SaveToFileToolStripMenuItem.Enabled = true;
		}
		else if (item == m_EditToolStripMenuItem)
		{
			m_GotoEditToolStripMenuItem.Enabled = true;
			m_SearchEditStripMenuItem.Enabled = true;
			m_UndoEditToolStripMenuItem.Enabled = true;
			m_RedoEditToolStripMenuItem.Enabled = true;
			m_CopyEditToolStripMenuItem.Enabled = true;
			m_CutEditToolStripMenuItem.Enabled = true;
			m_PasteEditToolStripMenuItem.Enabled = true;
		}
	}

	/// <summary>
	/// 打开文件
	/// </summary>
	private void OnOpenFileToolStripMenuItem_Click(object sender, EventArgs e)
	{
		SkinEngine.SkinDialogs = false;
		if (m_OpenCsvFileDialog.ShowDialog() != DialogResult.OK)
		{
			SkinEngine.SkinDialogs = true;
			return;
		}
		SkinEngine.SkinDialogs = true;

		if (m_OpenCsvFileDialog.FileNames.Length == 0)
		{
			return;
		}
		for (int fileIdx = 0; fileIdx < m_OpenCsvFileDialog.FileNames.Length; fileIdx++)
		{
			string fileFullPath = m_OpenCsvFileDialog.FileNames[fileIdx];

			bool isOpened = false;
			for (int csvFormIdx = 0; csvFormIdx < m_OpenedCsvFormList.Count; csvFormIdx++)
			{
				CsvForm openedCsvForm = m_OpenedCsvFormList[csvFormIdx];
				if (openedCsvForm != null && openedCsvForm.SourceFileFullName == fileFullPath)
				{
					isOpened = true;
					break;
				}
			}
			if (isOpened)
			{
				break;
			}

			LoadFile(fileFullPath);
		}
	}

	/// <summary>
	/// 还原到文件
	/// </summary>
	private void OnRevertFileToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!SelCsvFormInitialized())
		{
			return;
		}
		ToolStripMenuItem item = (ToolStripMenuItem)sender;
		SelCsvForm.RevertToCopyFile(item.Text);
	}

	/// <summary>
	/// 文件编辑 Undo\Redo\Copy\Cut\Paste
	/// </summary>
	private void OnEditToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!SelCsvFormInitialized())
		{
			return;
		}

		ToolStripMenuItem item = (ToolStripMenuItem)sender;
		if (item == m_UndoEditToolStripMenuItem)
		{
			SelCsvForm.EditManager.Undo();
		}
		else if (item == m_RedoEditToolStripMenuItem)
		{
			SelCsvForm.EditManager.Redo();
		}
		else if (item == m_CopyEditToolStripMenuItem)
		{
			SelCsvForm.EditManager.Copy();
		}
		else if (item == m_CutEditToolStripMenuItem)
		{
			SelCsvForm.EditManager.Cut();
		}
		else if (item == m_PasteEditToolStripMenuItem)
		{
			SelCsvForm.EditManager.Paste();
		}
	}

	/// <summary>
	/// 保存文件
	/// </summary>
	private void OnSaveFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
		if (!SelCsvFormInitialized())
		{
			return;
		}

		ToolStripMenuItem item = (ToolStripMenuItem)sender;
        //if (SelCsvForm.NeedSaveSourceFile && !SelCsvForm.DataChanged && item == m_SaveToSourceFileToolStripMenuItem)
        //{
        //    SelCsvForm.SaveToSourceFile();
        //}
        //else if (SelCsvForm.DataChanged && item == m_SaveToCopyFileToolStripMenuItem)
        //{
        //    SelCsvForm.SaveToCopyFile();
        //}
		if (item == m_SaveToSourceFileToolStripMenuItem && SelCsvForm.DataChanged)
		{
			SelCsvForm.SaveToCopyFile();
		}
		else if (item == m_SaveToFileToolStripMenuItem)
        {
            SkinEngine.SkinDialogs = false;
            if (m_SaveCsvFileDialog.ShowDialog() != DialogResult.OK)
            {
                SkinEngine.SkinDialogs = true;
                return;
            }
            SkinEngine.SkinDialogs = true;
            if (m_SaveCsvFileDialog.FileNames.Length == 0)
            {
                return;
            }
            SelCsvForm.SaveToPath(m_SaveCsvFileDialog.FileName);
        }
    }

	/// <summary>
	/// 打开转到窗口
	/// </summary>
    private void OnGotoEditToolStripMenuItem_Click(object sender, EventArgs e)
    {
		if (!SelCsvFormInitialized())
		{
			return;
		}
		if (m_GotoForm == null || m_GotoForm.IsDisposed)
        {
            m_GotoForm = new GotoForm();
        }
        if (!m_GotoForm.Visible)
        {
            m_GotoForm.Show();
        }
    }

	/// <summary>
	/// 打开查找和替换窗口
	/// </summary>
    private void OnSearchEditStripMenuItem_Click(object sender, EventArgs e)
    {
		if (!SelCsvFormInitialized())
		{
			return;
		}
        if (m_SearchForm == null || m_SearchForm.IsDisposed)
        {
            m_SearchForm = new SearchForm();
        }
        if (!m_SearchForm.Visible)
        {
            m_SearchForm.Show();
        }
    }

	/// <summary>
	/// 打开设置窗口
	/// </summary>
    private void OnSettingToolStripMenuItem_Click(object sender, EventArgs e)
    {
        SettingForm settingForm = new SettingForm();
        settingForm.ShowDialog();
    }
	#endregion // END UIEvent
}