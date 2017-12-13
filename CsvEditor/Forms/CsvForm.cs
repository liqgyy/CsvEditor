﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static CsvEditManager;
using System.Text;

public partial class CsvForm : Form
{
    /// <summary>
    /// 源文件 完整路径 如: C://a.csv
    /// </summary>
    public string SourceFileFullName;

    /// <summary>
    /// 源文件的副本 文件名
    /// </summary>
    public string SourceCopyFileName { get; private set; }

    /// <summary>
    /// 副本文件列表 文件名
    /// </summary>
    public List<string> CopyFileNameList { get; private set; }

    public CsvEditManager EditManager;

    public bool Initialized = false;

    public bool NeedSaveSourceFile = false;
    public bool DataChanged = false;

    public DataGridView MainDataGridView { get { return m_DataGridView; } }
    public DataTable MainDataTable;
    public DataTable CopyDataTable;

	private CsvSetting m_Setting;

    /// <summary>
    /// 源文件 文件名 如: a.csv
    /// </summary>
    private string m_SourceFileName;

    /// <summary>
    /// 当前的副本文件
    /// </summary>
    private string m_CurrentCopyFileName;

    public CsvForm(string fileFullPath)
    {
		InitializeComponent();

        CopyFileNameList = new List<string>();
        SourceFileFullName = fileFullPath;
		m_Setting = CsvSettingManager.LoadSetting(SourceFileFullName);

		EditManager = new CsvEditManager(this);
	}	

	public void BeforeChangeCellValue()
    {
        m_DataGridView.CellValueChanged -= OnDataGridView_CellValueChanged;

    }

    public void AfterChangeCellValue()
    {
        CopyDataTable = MainDataTable.Copy();
        m_DataGridView.CellValueChanged += OnDataGridView_CellValueChanged;
    }

	public bool CanClose()
	{
		if (DataChanged || NeedSaveSourceFile)
		{
			return false;
		}
		return true;
	}

	#region File
	public void SaveToSourceFile()
    {
        if (DataChanged)
        {
            // 应该先保存到副本，正常情况不会触发这里
            return;
        }

        // 保存到源文件前询问是否打开CodeCompare
        if (MessageBox.Show("是否打开文件比较工具", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
        {
			BeyondCompare.Instance.Compare(SourceFileFullName, Path.GetTempPath() + m_CurrentCopyFileName, "源文件", "副本");
			CodeCompare.Instance.Compare(SourceFileFullName, Path.GetTempPath() + m_CurrentCopyFileName, "源文件", "副本");
            return;
        }

        if (SaveToPath(SourceFileFullName))
        {
            DataChanged = false;
            NeedSaveSourceFile = false;
            try
            {
                CopySourceFile();
            }
            catch (Exception ex)
            {
                Debug.ShowExceptionMessageBox("拷贝文件副本失败: " + SourceFileFullName, ex);
            }
			MessageBox.Show("源文件保存成功\n" + SourceFileFullName, "提示");
        }
        UpdateFormText();
    }

    public void SaveToCopyFile()
    {
        m_CurrentCopyFileName = GetNewCopyFileName();
        if (SaveToPath(Path.GetTempPath() + m_CurrentCopyFileName))
        {
            CopyFileNameList.Add(m_CurrentCopyFileName);
            DataChanged = false;
        }
        UpdateFormText();
    }

    public bool SaveToPath(string path)
    {
		SaveCsvSetting();

		// 保存文件
		CsvExport myExport = new CsvExport(",", false);
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
            if (MessageBox.Show("当前文件未保存，是否还原?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
        }
        Initialized = false;
        Initialized = LoadCsvFileToDataTable(Path.GetTempPath() + copyFileName);
        // 还原为源文件的副本
        if (copyFileName == SourceCopyFileName)
        {
            NeedSaveSourceFile = false;
        }
        m_CurrentCopyFileName = copyFileName;
        UpdateFormText();
    }
	#endregion // End File

	/// <summary>
	/// 监听回车键  
	/// 如果单元格正在被编辑, 就在光标处添加换行(\r\n)
	/// </summary>
	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if (keyData != Keys.Enter)
		{
			return base.ProcessCmdKey(ref msg, keyData);
		}
		if (!m_DataGridView.IsCurrentCellInEditMode)
		{
			return base.ProcessCmdKey(ref msg, keyData);
		}

		try
		{
			TextBox textBox = m_DataGridView.EditingControl as TextBox;
			int nStart = textBox.SelectionStart;
			string oldValue = textBox.Text;
			string newValue = oldValue.Insert(nStart, "\r\n");
			textBox.Text = newValue;
			EditManager.DidCellValueChange(m_DataGridView.CurrentCell.ColumnIndex, m_DataGridView.CurrentCell.RowIndex, oldValue, newValue);
		}
		catch (Exception ex)
		{
			Debug.ShowExceptionMessageBox("插入换行符失败", ex);
		}
		return base.ProcessCmdKey(ref msg, keyData);
	}

	/// <summary>
	/// 拷贝源文件文件副本
	/// </summary>
	/// <returns>副本文件名</returns>
	private void CopySourceFile()
    {
        SourceCopyFileName = GetNewCopyFileName();
        File.Copy(SourceFileFullName, Path.GetTempPath() + SourceCopyFileName);
    }

    /// <summary>
    /// 获得新的副本文件名。文件名唯一
    /// </summary>
    private string GetNewCopyFileName()
    {
        return m_SourceFileName + "." + Stopwatch.GetTimestamp() + "." + Guid.NewGuid();
    }

    private bool LoadCsvFileToDataTable(string fileFullName)
    {
        string fileText;
        try
        {
            fileText = File.ReadAllText(fileFullName, Encoding.UTF8);
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
            MainDataTable = new DataTable();
            int rowCount = csvTable.GetLength(0);
            int colCount = -1;
            for (int rowIdx = 0; rowIdx < rowCount; rowIdx++)
            {
                DataRow newRowData = MainDataTable.NewRow();
                string[] csvRow = csvTable[rowIdx];
                for (int colIdx = 0; colIdx < csvRow.Length; colIdx++)
                {
                    if (colIdx > colCount)
                    {
                        MainDataTable.Columns.Add(colIdx.ToString(), typeof(string));
                        colCount = colIdx;
                    }
                    newRowData[colIdx] = csvRow[colIdx];
                }
                MainDataTable.Rows.Add(newRowData);
            }

            m_DataGridView.DataSource = MainDataTable;
            CopyDataTable = MainDataTable.Copy();
            MainDataTable.AcceptChanges();
            UpdateGridHeader();

			LoadCellSize();
			LoadFrozen();

			m_DataGridView.CellValueChanged += OnDataGridView_CellValueChanged;
            EditManager.DoSomething += OnRedoUndo_DoSomethingChange;
        }
        catch (Exception ex)
        {
            Debug.ShowExceptionMessageBox("csv转DataTable失败:" + fileFullName, ex);
            return false;
        }
        return true;
    }

	#region CsvSetting
	/// <summary>
	/// 保存CsvSetting
	/// </summary>
	private void SaveCsvSetting()
	{
		SaveCellSize();
		SaveFrozen();
		CsvSettingManager.SaveSetting(m_Setting);
	}

	private void LoadCellSize()
	{
		// Column
		if (m_Setting.ColumnWidths != null)
		{
			for (int colIdx = 0; colIdx < m_Setting.ColumnWidths.Length; colIdx++)
			{
				if (colIdx == m_DataGridView.Columns.Count)
				{
					break;
				}
				m_DataGridView.Columns[colIdx].Width = m_Setting.ColumnWidths[colIdx];
			}			
		}

		// Row
		if (m_Setting.RowHeadersWidth > 0)
		{
			m_DataGridView.RowHeadersWidth = m_Setting.RowHeadersWidth;
		}
		if (m_Setting.RowHeights != null)
		{
			for (int rowIdx = 0; rowIdx < m_Setting.RowHeights.Length; rowIdx++)
			{
				if (rowIdx == m_DataGridView.Rows.Count)
				{
					break;
				}
				m_DataGridView.Rows[rowIdx].Height = m_Setting.RowHeights[rowIdx];
			}
		}
	}

	private void SaveCellSize()
	{
		// Column
		m_Setting.ColumnWidths = new int[m_DataGridView.Columns.Count];
		for (int colIdx = 0; colIdx < m_DataGridView.Columns.Count; colIdx++)
		{
			m_Setting.ColumnWidths[colIdx] = m_DataGridView.Columns[colIdx].Width;
		}

		// Row
		m_Setting.RowHeadersWidth = m_DataGridView.RowHeadersWidth;
		m_Setting.RowHeights = new int[m_DataGridView.Rows.Count];
		for (int rowIdx = 0; rowIdx < m_DataGridView.Rows.Count; rowIdx++)
		{
			m_Setting.RowHeights[rowIdx] = m_DataGridView.Rows[rowIdx].Height;
		}
	}
	
	private void LoadFrozen()
	{
		if (m_Setting.FrozenRow >= 0)
		{
			m_DataGridView.Rows[m_Setting.FrozenRow >= m_DataGridView.RowCount ? m_DataGridView.RowCount - 1 : m_Setting.FrozenRow].Frozen = true;
		}
		if (m_Setting.FrozenColumn >= 0)
		{
			m_DataGridView.Columns[m_Setting.FrozenColumn >= m_DataGridView.ColumnCount ? m_DataGridView.ColumnCount - 1 : m_Setting.FrozenColumn].Frozen = true;
		}
	}

	private void SaveFrozen()
	{
		m_Setting.FrozenRow = -1;
		for (int rowIdx = 0; rowIdx < m_DataGridView.RowCount; rowIdx++)
		{
			if (!m_DataGridView.Rows[rowIdx].Frozen)
			{
				break;
			}
			m_Setting.FrozenRow = rowIdx;
		}

		m_Setting.FrozenColumn = -1;
		for (int colIdx = 0; colIdx < m_DataGridView.ColumnCount; colIdx++)
		{
			if (!m_DataGridView.Columns[colIdx].Frozen)
			{
				break;
			}
			m_Setting.FrozenColumn = colIdx;
		}
	}	
	#endregion // End CsvSetting

	/// <summary>
	/// 更新标题
	/// 行:数字1~x
	/// 列:字母A~Z
	/// </summary>
	private void UpdateGridHeader()
    {
        for (int colIdx = 0; colIdx < m_DataGridView.Columns.Count; colIdx++)
        {
            // 列数不能超过26
            m_DataGridView.Columns[colIdx].HeaderText = ConvertUtility.NumberToLetter(colIdx + 1);
            m_DataGridView.Columns[colIdx].SortMode = DataGridViewColumnSortMode.Programmatic;
        }
        // 更新行号
        for (int rowIdx = 0; rowIdx < m_DataGridView.RowCount; rowIdx++)
        {
            m_DataGridView.Rows[rowIdx].HeaderCell.Value = (rowIdx + 1).ToString();
        }
    }

    /// <summary>
    /// 更新窗口Text
    /// 文件没有保存到源文件就在开头加 ?
    /// 文件有改动就在结尾加 *
    /// </summary>
    private void UpdateFormText()
    {
        string newFormText = (NeedSaveSourceFile ? "?" : "") + m_SourceFileName + (DataChanged ? "*" : "");
        if (Text != newFormText)
        {
            Text = newFormText;
            MainForm.Instance.UpdateAllTabControlTabPageText();
        }
    }
  
    #region UIEvent
    /// <summary>
    /// 窗口加载时读取csv文件
    /// </summary>
    private void OnCsvForm_Load(object sender, EventArgs e)
    {
        // 初始化文件，创建文件副本
        try
        {
            m_SourceFileName = Path.GetFileName(SourceFileFullName);
            Text = m_SourceFileName;

            CopySourceFile();
        }
        catch (Exception ex)
        {
            Debug.ShowExceptionMessageBox("初始化文件失败:" + SourceFileFullName, ex);
            return;
        }

        Initialized = LoadCsvFileToDataTable(SourceFileFullName);
	}

	/// <summary>
	/// RedoUndo触发  
	/// 因为(Re\Un)Do时要取消DataGridView的监听, 所以在这里进行 数据改变 & 更新标题
	/// </summary>
	private void OnRedoUndo_DoSomethingChange(object sender, DoSomethingEventArgs e)
    {
        if (e.MyDoType == CsvEditManager.DoType.CellsValueChange)
        {
            OnDataGridViewData_Change();
        }
        else if(e.MyDoType == CsvEditManager.DoType.AddRow)
        {
            OnDataGridViewData_Change();
            UpdateGridHeader();
        }
    }

    /// <summary>
    /// 数据改变时更新标题
    /// </summary>
    private void OnDataGridViewData_Change()
    {
        NeedSaveSourceFile = true;
        DataChanged = true;
        
        UpdateFormText();
    }

    /// <summary>
	/// 点击DataGridViewCell时
    /// 切换SelectionMode 
    /// 改变DataGridView选择的行列 
    /// 更新菜单项状态
    /// </summary>
    private void OnDataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.ColumnIndex < 0 && e.RowIndex < 0)
        {
        }
        else if (e.ColumnIndex < 0)
        {
            m_DataGridView.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
        }
        else if (e.RowIndex < 0)
        {
            m_DataGridView.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
        }
        // 在右键菜单弹出前触发, 在这里初始化右键菜单
        if (e.Button == MouseButtons.Right)
        {
            m_InsertDownRowToolStripMenuItem.Enabled = false;
            m_InsertUpRowToolStripMenuItem.Enabled = false;
            m_FrozenToolStripMenuItem.Enabled = false;
            m_UnFrozenToolStripMenuItem.Enabled = false;
			m_AddColWidthToolStripMenuItem.Enabled = false;
			m_NoteToolStripMenuItem.Enabled = false;

            // UNDONE 右键菜单里的不支持多行、多列操作
            if (e.ColumnIndex < 0 && e.RowIndex < 0)
            {
                return;
            }
            // 点击行标题
            else if (e.ColumnIndex < 0)
            {
                m_InsertDownRowToolStripMenuItem.Enabled = true;
                m_InsertUpRowToolStripMenuItem.Enabled = true;

                m_DataGridView.ClearSelection();
                m_DataGridView.Rows[e.RowIndex].Selected = true;

                if (m_DataGridView.Rows[e.RowIndex].Frozen)
                {
                    m_UnFrozenToolStripMenuItem.Enabled = true;
                }
                else
                {
                    m_FrozenToolStripMenuItem.Enabled = true;
                }
            }
            // 点击列标题
            else if (e.RowIndex < 0)
            {
                m_DataGridView.ClearSelection();
                m_DataGridView.Columns[e.ColumnIndex].Selected = true;

                if (m_DataGridView.Columns[e.ColumnIndex].Frozen)
                {
                    m_UnFrozenToolStripMenuItem.Enabled = true;
                }
                else
                {
                    m_FrozenToolStripMenuItem.Enabled = true;
                }

				m_AddColWidthToolStripMenuItem.Enabled = true;
			}
			// 点击单元格
			else
			{
				m_NoteToolStripMenuItem.Enabled = true;
			}
		}
    }

    /// <summary>
    /// 插入行并添加Do事件到RedoUndo里
    /// </summary>
    private void OnInsertRowToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
    {
        m_DataGridView.CellValueChanged -= OnDataGridView_CellValueChanged;
        if (!Initialized)
        {
            return;
        }

        int offset = 0;
        ToolStripMenuItem item = (ToolStripMenuItem)sender;
        if (item == m_InsertDownRowToolStripMenuItem)
        {
            offset = 1;
        }
        else if (item == m_InsertUpRowToolStripMenuItem)
        {
            offset = 0;
        }
        else
        {
            return;
        }
        if (m_DataGridView.SelectedRows.Count < 1)
        {
            return;
        }

        int index = m_DataGridView.SelectedRows[0].Index + offset;
        DataRow newRowData = MainDataTable.NewRow();
        MainDataTable.Rows.InsertAt(newRowData, index);

        m_DataGridView.ClearSelection();
        m_DataGridView.Rows[index].Selected = true;
        CopyDataTable = MainDataTable.Copy();
        EditManager.DidAddRow(index);

        OnDataGridViewData_Change();
        UpdateGridHeader();
        m_DataGridView.CellValueChanged += OnDataGridView_CellValueChanged;
    }

    /// <summary>
    /// 单元格值改变时添加Do事件到RedoUndo里
    /// </summary>
    private void OnDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
		if (e.RowIndex < 0 || e.ColumnIndex < 0)
		{
			return;
		}
        EditManager.DidCellValueChange(e.ColumnIndex, e.RowIndex, CopyDataTable.Rows[e.RowIndex][e.ColumnIndex].ToString(), MainDataTable.Rows[e.RowIndex][e.ColumnIndex].ToString());
        CopyDataTable = MainDataTable.Copy();
        OnDataGridViewData_Change();
    }

	/// <summary>
	/// 绘制单元格时，添加批注提示
	/// </summary>
	private void OnDataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
	{
		if (e.ColumnIndex < 0 || e.RowIndex < 0)
		{
			return;
		}

		DataGridViewCell cell = m_DataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
		if (cell.Selected)
		{
			return;
		}

		e.Paint(e.ClipBounds, e.PaintParts);

		// 批注提示
		using (Brush gridBrush = new SolidBrush(Color.Red))
		{
			Point[] polygonPoints = new Point[3];
			polygonPoints[0] = new Point(e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
			polygonPoints[1] = new Point(e.CellBounds.Right - 1, e.CellBounds.Bottom - 1 - GlobalData.CSV_NOTE_POLYGON_SIZE);
			polygonPoints[2] = new Point(e.CellBounds.Right - 1 - GlobalData.CSV_NOTE_POLYGON_SIZE, e.CellBounds.Bottom - 1);
			e.Graphics.FillPolygon(gridBrush, polygonPoints);
			e.Handled = true;
		}
	}

	/// <summary>
	/// 锁定行或列
	/// </summary>
	private void OnFrozenToolStripMenuItem_Click(object sender, EventArgs e)
    {
        bool frozen = true;
        ToolStripMenuItem item = (ToolStripMenuItem)sender;
        if (item == m_FrozenToolStripMenuItem)
        {
            frozen = true;
        }
        else if (item == m_UnFrozenToolStripMenuItem)
        {
            frozen = false;
        }
        for (int rowIdx = 0; rowIdx < m_DataGridView.SelectedRows.Count; rowIdx++)
        {
            m_DataGridView.SelectedRows[rowIdx].Frozen = frozen;
		}
        for (int colIdx = 0; colIdx < m_DataGridView.SelectedColumns.Count; colIdx++)
        {
            m_DataGridView.SelectedColumns[colIdx].Frozen = frozen;
        }
        UpdateGridHeader();
    }

	/// <summary>
	/// 增加列宽
	/// </summary>
	private void OnAddColWidthToolStripMenuItem_Click(object sender, EventArgs e)
	{
		for (int colIdx = 0; colIdx < m_DataGridView.SelectedColumns.Count; colIdx++)
		{
			// 魔法数字：列宽增加
			m_DataGridView.SelectedColumns[colIdx].Width += 120;
		}
	}

	private void OnForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		SaveCsvSetting();
	}

	private void OnNoteToolStripMenuItem_Click(object sender, EventArgs e)
	{

	}
	#endregion UIEvent	
}