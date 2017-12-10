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
        RegistryUtility registryUtility = RegistryUtility.Instance;
        CodeCompare codeCompare = CodeCompare.Instance;

        SkinUtility.SetSkin();
    }

    public bool SelCsvFormInitialized()
    {
        if (SelCsvForm == null)
        {
            return false;
        }
        return SelCsvForm.Initialized;
    }

    public void UpdateAllToolStripMenu()
    {
        UpdateFileToolStripMenu();
        UpdateFileRevertToolStripMenu();
        UpdateEditToolStripMenu();       
    }

    public void UpdateAllTabControlTabPageText()
    {
        for (int tabPageIdx = 0; tabPageIdx < m_MainTabControl.TabPages.Count; tabPageIdx++)
        {
            TabPage tabPage = m_MainTabControl.TabPages[tabPageIdx];
            CsvForm csvForm = (CsvForm)tabPage.Controls[0];
            tabPage.Text = csvForm.Text;
        }
    }

    private void CloseCsvForm(int tabIdx)
    {
        CsvForm csvForm = (CsvForm)m_MainTabControl.TabPages[tabIdx].Controls[0];
        if (csvForm.TryClose())
        {
            m_OpenedCsvFormList.Remove(csvForm);
            m_MainTabControl.TabPages.RemoveAt(tabIdx);
            csvForm.Close();
        }
    }

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
        UpdateAllToolStripMenu();
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

    private void AddMenumItemToRevertFileToolStripMenu(string copyFileName)
    {
        ToolStripMenuItem newToolStripMenuItem = new ToolStripMenuItem();
        newToolStripMenuItem.Name = copyFileName;
        newToolStripMenuItem.Text = copyFileName;
        newToolStripMenuItem.Click += new EventHandler(OnRevertFileToolStripMenuItem_Click);

        m_RevertFileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem });
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

        DataGridView dataGridView = SelCsvForm.MainDataGridView;
        if (dataGridView.SelectedCells.Count > 0)
        {
            m_CopyEditToolStripMenuItem.Enabled = true;
            m_CutEditToolStripMenuItem.Enabled = true;
            m_PasteEditToolStripMenuItem.Enabled = true;
        }

        m_UndoEditToolStripMenuItem.Enabled = SelCsvForm.RedoUndo.CanUndo();
        m_RedoEditToolStripMenuItem.Enabled = SelCsvForm.RedoUndo.CanRedo();
    }
    #endregion // End Update ToolStripMenu

    #region UIEvent
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

    private void OnOpenFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (m_OpenCsvFileDialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }
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

    private void OnRevertFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (!SelCsvFormInitialized())
        {
            return;
        }
        ToolStripMenuItem item = (ToolStripMenuItem)sender;
        SelCsvForm.RevertToCopyFile(item.Text);
    }

    private void OnEditToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (!SelCsvFormInitialized())
        {
            return;
        }

        ToolStripMenuItem item = (ToolStripMenuItem)sender;
        if (item == m_UndoEditToolStripMenuItem)
        {
            SelCsvForm.EditUndo();
        }
        else if (item == m_RedoEditToolStripMenuItem)
        {
            SelCsvForm.EditRedo();
        }
        else if(item == m_CopyEditToolStripMenuItem)
        {
            SelCsvForm.EditCopy();
        }
        else if (item == m_CutEditToolStripMenuItem)
        {
            SelCsvForm.EditCut();
        }
        else if (item == m_PasteEditToolStripMenuItem)
        {
            SelCsvForm.EditPaste();
        }
    }

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

    private void OnSaveFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ToolStripMenuItem item = (ToolStripMenuItem)sender;
        if (item == m_SaveToSourceFileToolStripMenuItem)
        {
            SelCsvForm.SaveToSourceFile();
        }
        else if (item == m_SaveToCopyFileToolStripMenuItem)
        {
            SelCsvForm.SaveToCopyFile();
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
            SelCsvForm.SaveToPath(m_SaveCsvFileDialog.FileName);
        }
        UpdateAllToolStripMenu();
    }

    private void OnGotoEditToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (m_GotoForm == null || m_GotoForm.IsDisposed)
        {
            m_GotoForm = new GotoForm();
        }
        if (!m_GotoForm.Visible)
        {
            m_GotoForm.Show();
        }
    }

    private void OnSearchEditStripMenuItem_Click(object sender, EventArgs e)
    {
        if (m_SearchForm == null || m_SearchForm.IsDisposed)
        {
            m_SearchForm = new SearchForm();
        }
        if (!m_SearchForm.Visible)
        {
            m_SearchForm.Show();
        }
    }

    private void OnSettingToolStripMenuItem_Click(object sender, EventArgs e)
    {
        SettingForm settingForm = new SettingForm();
        settingForm.ShowDialog();
    }
    #endregion // END UIEvent
}