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

namespace CsvEditor
{
	public partial class MainForm : Form
	{
        public static MainForm Instance;

        private List<CsvForm> m_OpenedCsvFormList = new List<CsvForm>();
        private CsvForm m_SelCsvForm;

        public MainForm()
		{
            Instance = this;
            InitializeComponent();

			RegistryUtility registryUtility = RegistryUtility.Instance;
			CodeCompare codeCompare = CodeCompare.Instance;
		}

		public void UpdateAllToolStripMenu()
        {
            UpdateFileToolStripMenu();
			UpdateFileRevertToolStripMenu();
			UpdateEditToolStripMenu();
		}

		public void UpdateAllTabControlTabPageText()
        {
            for(int tabPageIdx = 0; tabPageIdx < m_MainTabControl.TabPages.Count; tabPageIdx++)
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
            m_SelCsvForm = newCsvForm;

            UpdateAllToolStripMenu();
        }

		private bool SelCsvFormInitialized()
		{
			if (m_SelCsvForm == null)
			{
				return false;
			}
			return m_SelCsvForm.Initialized;
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
			m_SaveToSourceFileToolStripMenuItem.Enabled = m_SelCsvForm.NeedSaveSourceFile || m_SelCsvForm.DataChanged;
			m_SaveToCopyFileToolStripMenuItem.Enabled = m_SelCsvForm.DataChanged;
			m_SaveToFileToolStripMenuItem.Enabled = true;
		}

		private void UpdateFileRevertToolStripMenu()
		{
			m_RevertFileToolStripMenuItem.Enabled = false;
			if (!SelCsvFormInitialized() || (string.IsNullOrEmpty(m_SelCsvForm.SourceCopyFileName) && m_SelCsvForm.CopyFileNameList.Count == 0))
			{
				return;
			}
			m_RevertFileToolStripMenuItem.Enabled = true;
			m_RevertFileToolStripMenuItem.DropDownItems.Clear();
			for (int copyFileIdx = 0; copyFileIdx < m_SelCsvForm.CopyFileNameList.Count; copyFileIdx ++)
			{
				string copyFileName = m_SelCsvForm.CopyFileNameList[copyFileIdx];
				AddMenumItemToRevertFileToolStripMenu(copyFileName);
			}
			if (!string.IsNullOrEmpty(m_SelCsvForm.SourceCopyFileName))
			{
				AddMenumItemToRevertFileToolStripMenu(m_SelCsvForm.SourceCopyFileName);
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
			m_CopyEditToolStripMenuItem.Enabled = false;
			m_CutEditToolStripMenuItem.Enabled = false;
			m_PasteEditToolStripMenuItem.Enabled = false;
			if (!SelCsvFormInitialized())
			{
				return;
			}

			DataGridView dataGridView = m_SelCsvForm.MainDataGridView;
			if (dataGridView == null)
			{
				return;
			}
			if (dataGridView.SelectedCells.Count > 0)
			{
				m_CopyEditToolStripMenuItem.Enabled = true;
				m_CutEditToolStripMenuItem.Enabled = true;
				m_PasteEditToolStripMenuItem.Enabled = true;
			}
		}
		#endregion // End Update ToolStripMenu

		#region UIEvent
		private void OnMainForm_Load(object sender, EventArgs e)
		{
			UpdateAllToolStripMenu();

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
				for(int csvFormIdx = 0; csvFormIdx < m_OpenedCsvFormList.Count; csvFormIdx++)
				{
					CsvForm openedCsvForm = m_OpenedCsvFormList[csvFormIdx];
					if (openedCsvForm != null && openedCsvForm.SourceFileFullPath == fileFullPath)
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
			m_SelCsvForm.RevertToCopyFile(item.Text);
		}

		private void OnEditToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem item = (ToolStripMenuItem)sender;
			if (item == m_CopyEditToolStripMenuItem)
			{
			}
			else if (item == m_CutEditToolStripMenuItem)
			{
			}
			else if (item == m_PasteEditToolStripMenuItem)
			{
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
                m_SelCsvForm = null;
            }
            else
            {
                m_SelCsvForm = (CsvForm)m_MainTabControl.TabPages[m_MainTabControl.SelectedIndex].Controls[0];
            }
            UpdateAllToolStripMenu();
        }
      
        private void OnSaveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (item == m_SaveToSourceFileToolStripMenuItem)
            {
                m_SelCsvForm.SaveToSourceFile();
            }
            else if (item == m_SaveToCopyFileToolStripMenuItem)
            {
                m_SelCsvForm.SaveToCopyFile();
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
                m_SelCsvForm.SaveToPath(m_SaveCsvFileDialog.FileName);
            }
			UpdateAllToolStripMenu();
		}
		#endregion // END UIEvent
	}
}
