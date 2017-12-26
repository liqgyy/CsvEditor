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

    /// <summary>
    /// 当前Csv窗口，只能通过SetSelCsvForm赋值
    /// </summary>
    public CsvForm SelCsvForm { get; private set; }

	public TextBox CellEditTextBox { get { return m_CellEditTextBox; } }

    private GotoForm m_GotoForm;
    private SearchForm m_SearchForm;

	private BeforeCloseCsvFormEventType m_BeforeCloseCsvFormEventType = BeforeCloseCsvFormEventType.None;

	public MainForm()
    {
        Instance = this;
        InitializeComponent();

        Setting setting = Setting.Instance;

		BeyondCompare.Instance.AutoExePathToSetting();
		CodeCompare.Instance.AutoExePathToSetting();

		m_CellEditPanel.Dock = DockStyle.Fill;
		m_CellEditTipPanel.Dock = DockStyle.Fill;
	}

	/// <summary>
	/// 当前选中的csv是否存在且完成初始化
	/// </summary>
	/// <returns></returns>
	public bool SelCsvFormInitialized()
    {
        if (SelCsvForm == null || SelCsvForm.IsDisposed)
        {
            return false;
        }
        return SelCsvForm.Initialized;
    }

	/// <summary>
	/// 更新窗口标题
	/// 文件有改动在结尾加 *
	/// </summary>
	public void UpdateFormText()
    {
		if (SelCsvFormInitialized())
		{
			Text = SelCsvForm.Text;
		}
		else
		{
			Text = "CsvEditor";
		}
    }

	public void UpdateCellEdit()
	{
		m_CellEditPanel.Visible = false;
		m_CellEditTipPanel.Visible = false;
		m_CellEditTextBox.TextChanged -= OnCellEditTextBox_TextChanged;

		if (SelCsvFormInitialized())
		{
			if(SelCsvForm.MainDataGridView.SelectedCells.Count == 0)
			{
				m_CellEditTipPanel.Visible = true;
				m_CellEditTipLabel.Text = "当前未选中单元格";
			}
			else if (SelCsvForm.MainDataGridView.SelectedCells.Count == 1)
			{
				m_CellEditPanel.Visible = true;
				m_CellEditTextBox.Text = (string)SelCsvForm.MainDataGridView.SelectedCells[0].Value;
				m_CellEditTextBox.TextChanged += OnCellEditTextBox_TextChanged;
			}
			else if (SelCsvForm.MainDataGridView.SelectedCells.Count > 1)
			{
				m_CellEditTipPanel.Visible = true;
				m_CellEditTipLabel.Text = "不支持编辑多个单元格";
			}
		}
		else
		{
			m_CellEditTipPanel.Visible = true;
			m_CellEditTipLabel.Text = "";
		}
	}

	private void OpenFile()
	{
		if (SelCsvForm != null && !SelCsvForm.IsDisposed)
		{
			m_BeforeCloseCsvFormEventType = BeforeCloseCsvFormEventType.OpenFile;
			SelCsvForm.Close();
			return;
		}

		if (m_OpenCsvFileDialog.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		if (m_OpenCsvFileDialog.FileNames.Length == 0)
		{
			return;
		}

		string path = m_OpenCsvFileDialog.FileName;
		LoadFile(path);
	}

	/// <summary>
	/// 加载csv文件
	/// </summary>
	/// <param name="path">文件完整路径</param>
	private void LoadFile(string path)
    {
        CsvForm newCsvForm = new CsvForm(path);
        if (newCsvForm == null)
        {
            return;
        }
        newCsvForm.TopLevel = false;
        newCsvForm.Visible = true;
        newCsvForm.FormBorderStyle = FormBorderStyle.None;
        newCsvForm.Dock = DockStyle.Fill;
		newCsvForm.Show();
		m_SplitContainer.Panel1.Controls.Add(newCsvForm);
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
        m_SaveFileToolStripMenuItem.Enabled = false;
        m_SaveToFileToolStripMenuItem.Enabled = false;
        if (!SelCsvFormInitialized())
        {
            return;
        }
        m_SaveFileToolStripMenuItem.Enabled = SelCsvForm.DataChanged;
        m_SaveToFileToolStripMenuItem.Enabled = true;
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
	#endregion // End Update ToolStripMenu

	#region UIEvent
	public void OnCsvForm_FormClosed()
	{
		UpdateFormText();
		switch (m_BeforeCloseCsvFormEventType)
		{
			case BeforeCloseCsvFormEventType.OpenFile:
				OpenFile();
				break;
			case BeforeCloseCsvFormEventType.CloseForm:
				Close();
				break;
		}
		m_BeforeCloseCsvFormEventType = BeforeCloseCsvFormEventType.None;
	}

	/// <summary>
	/// 窗口加载时检查cmd传入的参数
	/// </summary>
	private void OnMainForm_Load(object sender, EventArgs e)
    {
        SetSelCsvForm(null);
		UpdateCellEdit();

		string[] commands = Environment.GetCommandLineArgs();
        // 关联csv文件
        if (commands != null && commands.Length > 1)
        {
			LoadFile(commands[1]);
        }
    }

	private void OnForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (SelCsvForm != null && !SelCsvForm.IsDisposed)
		{
			m_BeforeCloseCsvFormEventType = BeforeCloseCsvFormEventType.CloseForm;
			SelCsvForm.Close();
			e.Cancel = true;
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
		}
		else if (item == m_EditToolStripMenuItem)
		{
			UpdateEditToolStripMenu();
		}
	}

	/// <summary>
	/// 顶层菜单关闭时，启用菜单里的所有菜单项
	/// TODO 优化逻辑
	/// </summary>
	private void OnTopToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
	{
		ToolStripMenuItem item = (ToolStripMenuItem)sender;
		if (item == m_FileToolStripMenuItem)
		{
			m_SaveFileToolStripMenuItem.Enabled = true;
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
		OpenFile();
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
        if (SelCsvForm.DataChanged && item == m_SaveFileToolStripMenuItem)
        {
            SelCsvForm.SaveFile();
        }
        else if (item == m_SaveToFileToolStripMenuItem)
        {
            if (m_SaveCsvFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            if (m_SaveCsvFileDialog.FileNames.Length == 0)
            {
                return;
            }
            SelCsvForm.SaveFile(m_SaveCsvFileDialog.FileName);
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

	private void OnCellEditTextBox_TextChanged(object sender, EventArgs e)
	{
		SelCsvForm.MainDataGridView.SelectedCells[0].Value = m_CellEditTextBox.Text;
	}
	#endregion // END UIEvent

	/// <summary>
	/// 关闭csv窗口前的事件
	/// </summary>
	public enum BeforeCloseCsvFormEventType
	{
		None,
		OpenFile,
		CloseForm
	}
}