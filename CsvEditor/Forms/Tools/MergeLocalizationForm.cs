using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

public partial class MergeLocalizationForm : Form
{
	private List<CsvEditManager.IUndoRedo> m_ManyThingList = new List<CsvEditManager.IUndoRedo>();
	private List<CsvEditManager.CellValueChangeItem> m_CellChangeList = new List<CsvEditManager.CellValueChangeItem>();
	private List<DataGridViewConsoleForm.Message> m_MessageList = new List<DataGridViewConsoleForm.Message>();

	public MergeLocalizationForm()
	{
		InitializeComponent();

		UpdateOkButtonEnbale();
	}

	private void Merge(DataTable originalCsv, string[][] changedCsv)
	{
		CsvForm csvForm = MainForm.Instance.GetCsvForm();
		csvForm.BeforeChangeCellValue();

		m_ManyThingList = new List<CsvEditManager.IUndoRedo>();
		m_CellChangeList = new List<CsvEditManager.CellValueChangeItem>();
		m_MessageList = new List<DataGridViewConsoleForm.Message>();

		for (int iRowInChangedCSV = 0; iRowInChangedCSV < changedCsv.Length; iRowInChangedCSV++)
		{
			string[] changedDataInRow = changedCsv[iRowInChangedCSV];

			string key = changedDataInRow[0];
			changedDataInRow[0] = key.Trim();
			// 跳过空key
			if (string.IsNullOrWhiteSpace(key))
			{
				continue;
			}
			// 为什么要把Trim的结果赋值回changedDataInRow[0]？
			// 因为changedCSV的key中有可能出现空格或换行符，工具生成的CSV文件中的新增和修改的部分都是直接用changedCSV里的内容，所以需要把changedCSV里的keyTrim一下
			if (key.Trim() == "String ID")
			{
				// 如果key是String ID，则是表头,直接无视
				continue;
			}

			bool existed = false;
			for (int iRowInOriginalCSV = 0; iRowInOriginalCSV < originalCsv.Rows.Count; iRowInOriginalCSV++)
			{
				DataRow originalDataInRow = originalCsv.Rows[iRowInOriginalCSV];
				string originalKey = (string)originalDataInRow[0];
				if (originalKey.Trim() == key.Trim())
				{
					// key前后有空格
					if (key != key.Trim())
					{
						DataGridViewConsoleForm.Message message = new DataGridViewConsoleForm.Message();
						message.Level = DataGridViewConsoleForm.Level.Warning;
						message.Column = 0;
						message.Row = iRowInOriginalCSV;
						message.Caption = "更改后Csv中Key的头尾有空格";
						message.Text = string.Format("我帮你去除了空格\n去空格前的Key:({0})", key);
						m_MessageList.Add(message);
					}

					existed = true;
					CopyCsvRowToDataRow(iRowInOriginalCSV, changedDataInRow, originalDataInRow);
					break;
				}
			}

			if (!existed)
			{
				// 添加新行
				DataRow newRow = originalCsv.NewRow();
				for (int iCell = 0; iCell < changedDataInRow.Length; iCell++)
				{
					newRow[iCell] = "";
				}
				CsvEditManager.DoAddRowEvent doAddRowEvent = new CsvEditManager.DoAddRowEvent
				{
					Row = originalCsv.Rows.Count
				};
				m_ManyThingList.Add(doAddRowEvent);

				CopyCsvRowToDataRow(originalCsv.Rows.Count, changedDataInRow, newRow);
				originalCsv.Rows.Add(newRow);
			}
		}

		CsvEditManager.DoCellsValueChangeEvent doCellsValueChangeEvent = new CsvEditManager.DoCellsValueChangeEvent
		{
			ChangeList = m_CellChangeList
		};
		m_ManyThingList.Add(doCellsValueChangeEvent);

		csvForm.EditManager.DidManyThings(m_ManyThingList);
		csvForm.AfterChangeCellValue();
		csvForm.UpdateGridHeader();

		DataGridViewConsoleForm.ShowForm(m_MessageList, csvForm.GetDataGridView(), "本地化合并");
	}

	private void CopyCsvRowToDataRow(int rowIdx, string[] row, DataRow dataRow)
	{
		int changedCount = 0;
		System.Text.StringBuilder oldSb = new System.Text.StringBuilder();
		System.Text.StringBuilder newSb = new System.Text.StringBuilder();
		for (int iCell = 0; iCell < row.Length; iCell++)
		{
			string iterCell = row[iCell];
			string oldCell = (string)dataRow[iCell];

			oldSb.Append(oldCell);
			newSb.Append(iterCell);
			if (iCell != row.Length - 1)
			{
				oldSb.Append("\t");
				newSb.Append("\t");
			}

			if (iterCell != oldCell)
			{	
				changedCount++;
				CsvEditManager.CellValueChangeItem changeItem = new CsvEditManager.CellValueChangeItem();
				changeItem.Row = rowIdx;
				changeItem.Column = iCell;
				changeItem.OldValue = oldCell;
				changeItem.NewValue = iterCell;
				m_CellChangeList.Add(changeItem);

				dataRow[iCell] = iterCell;
			}

			if (string.IsNullOrWhiteSpace(iterCell))
			{
				DataGridViewConsoleForm.Message message = new DataGridViewConsoleForm.Message();
				message.Level = DataGridViewConsoleForm.Level.Warning;
				message.Column = iCell;
				message.Row = rowIdx;
				message.Caption = "更改后Csv中该值是空";
				message.Text = string.Format("源值：\n({0})", oldCell.ToString());
				m_MessageList.Add(message);
			}
		}

		if (changedCount > 0)
		{
			DataGridViewConsoleForm.Message message = new DataGridViewConsoleForm.Message();
			message.Level = DataGridViewConsoleForm.Level.Info;
			message.Column = -1;
			message.Row = rowIdx;
			message.Caption = string.Format("修改了({0})个单元格", changedCount);
			message.Text = string.Format("源：\n({0})\n合并后:\n({1})", oldSb.ToString(), newSb.ToString());
			m_MessageList.Add(message);
		}
	}

	private void UpdateOkButtonEnbale()
	{
		string path = m_CsvPathTextBox.Text;
		if (string.IsNullOrEmpty(path))
		{
			m_OkButton.Enabled = false;
		}
		else
		{
			m_OkButton.Enabled = File.Exists(path);
		}
	}

	#region UIEvent
	private void OnOkButton_Click(object sender, EventArgs e)
	{
		string path = m_CsvPathTextBox.Text;
		if (string.IsNullOrEmpty(path))
		{
			MessageBox.Show("请选择文件", "提示");
			return;
		}

		if (File.Exists(path))
		{
			string[][] changedCsv = FileUtility.LoadFileToCsv(path);
			if (changedCsv == null)
			{
				return;
			}

			if (changedCsv.Length > 1 && changedCsv[0].Length > 1 && changedCsv[0][0] == "String ID")
			{
				DataTable originalCsv = MainForm.Instance.GetCsvForm().GetDataTable();
				if (changedCsv[0].Length != originalCsv.Columns.Count)
				{
					MessageBox.Show(string.Format("源表有({0})列，更改后Csv有({1})列，列数不一样，请检查",
						originalCsv.Columns.Count,
						changedCsv[0].Length), "提示");
					return;
				}
				Merge(originalCsv, changedCsv);
				MessageBox.Show("合并本地化完成", "提示");
			}
			else
			{
				MessageBox.Show(string.Format("选择的表({0})可能不是本地化表", path), "提示");
			}
		}
		else
		{
			MessageBox.Show("选择的文件不存在", "提示");
		}
		Close();
	}

	private void OnCancelButton_Click(object sender, EventArgs e)
	{
		Close();
		Dispose();
	}

	private void OnOpenCsvFileDialogButton_Click(object sender, EventArgs e)
	{
		if (m_OpenCsvFileDialog.ShowDialog() == DialogResult.OK)
		{
			m_CsvPathTextBox.Text = m_OpenCsvFileDialog.FileName;
		}
	}

	private void OnCsvPathTextBox_TextChanged(object sender, EventArgs e)
	{
		UpdateOkButtonEnbale();
	}
	#endregion // END UIEvent
}