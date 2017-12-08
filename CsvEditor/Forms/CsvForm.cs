using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsvEditor
{
	public partial class CsvForm : Form
	{
		/// <summary>
		/// 源文件 完整路径 如: C://a.csv
		/// </summary>
		public string SourceFileFullPath;
		/// <summary>
		/// 源文件 文件名 如: a.csv
		/// </summary>
		public string SourceFileName;

		/// <summary>
		/// 副本文件 完整路径
		/// </summary>
		public string SourceCopyFileName { get; private set; }
		/// <summary>
		/// 副本文件 文件名
		/// </summary>
		public List<string> CopyFileNameList { get; private set; }

        public bool Initialized = false;

        public bool NeedSaveSourceFile = false;
        public bool DataChanged = false;

        public DataGridView MainDataGridView { get { return m_DataGridView; } }
        public DataTable MainDataTable { get; private set; }

        public CsvForm(string fileFullPath)
		{
			InitializeComponent();

			CopyFileNameList = new List<string>();
			SourceFileFullPath = fileFullPath;
		}

        public bool TryClose()
        {
            if (DataChanged)
            {
                // TODO 弹出对话框询问是否保存
                return false;
            }
            if (NeedSaveSourceFile)
            {
                return false;
            }
            return true;
        }

        public void SaveToSourceFile()
        {
            if (SaveToPath(SourceFileFullPath))
            {
                DataChanged = false;
                NeedSaveSourceFile = false;
				try
				{
					CopySourceFile();
				}
				catch (Exception ex)
				{
					Debug.ShowExceptionMessageBox("拷贝文件副本失败: " + SourceFileFullPath, ex);
				}
			}
			UpdateFormText();
		}

        public void SaveToCopyFile()
        {
			string newCopyFileName = GetNewCopyFileName();
            if (SaveToPath(Path.GetTempPath() + newCopyFileName))
            {
				CopyFileNameList.Add(newCopyFileName);
				DataChanged = false;
            }
			UpdateFormText();
		}

		public bool SaveToPath(string path)
        {
            CsvExport myExport = new CsvExport("," , false);
            try
            {
                for (int rowIdx = 0; rowIdx < MainDataTable.Rows.Count; rowIdx++)
                {
                    myExport.AddRow();
                    DataRow dataRow = MainDataTable.Rows[rowIdx];
                    for (int colIdx = 0; colIdx < MainDataTable.Columns.Count; colIdx++)
                    {
                        myExport[colIdx.ToString()] = dataRow[colIdx];
                    }
                }
                
                myExport.ExportToFile(path);
            }
            catch (Exception ex)
            {
				Debug.ShowExceptionMessageBox("保存文件失败:" + path, ex);
                return false;
            }
            return true;
        }

		/// <summary>
		/// 还原到副本文件
		/// </summary>
		/// <param name="copyFileName"></param>
		public void RevertToCopyFile(string copyFileName)
		{
			if (DataChanged)
			{
				// TODO 提示用户当前内容没有保存
			}
			Initialized = false;
			Initialized = LoadCsvFileToDataTable(Path.GetTempPath() + copyFileName);
		}

		/// <summary>
		/// 拷贝源文件文件副本
		/// </summary>
		/// <returns>副本文件名</returns>
		private void CopySourceFile()
		{
			SourceCopyFileName = GetNewCopyFileName();
			File.Copy(SourceFileFullPath, Path.GetTempPath() + SourceCopyFileName);
		}

		/// <summary>
		/// 获得新的副本文件名。文件名唯一
		/// </summary>
		private string GetNewCopyFileName()
		{
			return SourceFileName + "." + Stopwatch.GetTimestamp() + "." + Guid.NewGuid();
		}

		private bool LoadCsvFileToDataTable(string fileFullName)
		{
			string fileText;
			try
			{
				fileText = File.ReadAllText(fileFullName);
			}
			catch (Exception ex)
			{
				Debug.ShowExceptionMessageBox("读取文件失败:" + fileFullName, ex);
				return false;
			}

			// 读取文件 -> csv
			string[][] csvTable;
			try
			{
				csvTable = CsvParser2.Parse(fileText);
			}
			catch (Exception ex)
			{
				Debug.ShowExceptionMessageBox("转csv失败:" + fileFullName, ex);
				return false;
			}

			// csv->DataTable
			try
			{
				int rowCount = csvTable.GetLength(0);
				int colCount = 0;

				for (int rowIdx = 0; rowIdx < rowCount; rowIdx++)
				{
					colCount = colCount < csvTable[rowIdx].Length ? csvTable[rowIdx].Length : colCount;
				}
				MainDataTable = new DataTable();
				for (int colIdx = 0; colIdx < colCount; colIdx++)
				{
					MainDataTable.Columns.Add(colIdx < csvTable[0].Length ? csvTable[0][colIdx] : colIdx.ToString(), typeof(string));
				}
				for (int rowIdx = 0; rowIdx < rowCount; rowIdx++)
				{
					DataRow newRowData = MainDataTable.NewRow();
					string[] csvRow = csvTable[rowIdx];
					for (int colIdx = 0; colIdx < csvRow.Length; colIdx++)
					{
						newRowData[colIdx] = csvRow[colIdx];
					}
					MainDataTable.Rows.Add(newRowData);
				}
				m_DataGridView.DataSource = MainDataTable;
			}
			catch (Exception ex)
			{
				Debug.ShowExceptionMessageBox("csv转DataTable失败:" + fileFullName, ex);
				return false;
			}
			return true;
		}

		/// <summary>
		/// 更新窗口Text
		/// 文件没有保存到源文件就在开头加 ?
		/// 文件有改动就在结尾加 *
		/// </summary>
		private void UpdateFormText()
		{
			string newFormText = (NeedSaveSourceFile ? "?" : "") + SourceFileName + (DataChanged ? "*" : "");
			if (Text != newFormText)
			{
				Text = newFormText;
				MainForm.Instance.UpdateAllTabControlTabPageText();
			}
		}

		#region UIEvent
		private void OnCsvForm_Load(object sender, EventArgs e)
		{
			// 初始化文件，创建文件副本
			try
			{
				SourceFileName = Path.GetFileName(SourceFileFullPath);
				Text = SourceFileName;

				CopySourceFile();
			}
			catch (Exception ex)
			{
				Debug.ShowExceptionMessageBox("初始化文件失败:" + SourceFileFullPath, ex);
				return;
			}

			Initialized = LoadCsvFileToDataTable(SourceFileFullPath);
		}

		private void OnDataGridViewData_Change()
        {
            NeedSaveSourceFile = true;
            DataChanged = true;

			UpdateFormText();			
			MainForm.Instance.UpdateAllToolStripMenu();
		}

        private void OnDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            OnDataGridViewData_Change();
        }

        private void OnDataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                //{

                //}
                // Add this
                //m_DataGridView.CurrentCell = m_DataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                //m_DataGridView.Rows[e.RowIndex].Selected = true;
                //m_DataGridView.Focus();
            }
        }
		#endregion UIEvent
	}
}