using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using static DataGridViewRedoUndo;

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

    public DataGridViewRedoUndo RedoUndo;

    public bool Initialized = false;

    public bool NeedSaveSourceFile = false;
    public bool DataChanged = false;

    public DataGridView MainDataGridView { get { return m_DataGridView; } }

    private DataTable m_MainDataTable;
    private DataTable m_CopyDataTable;

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

        RedoUndo = new DataGridViewRedoUndo(m_DataGridView);
    }

    #region Edit
    public void EditUndo()
    {
        m_DataGridView.CellValueChanged -= OnDataGridView_CellValueChanged;
        RedoUndo.Undo();
        m_CopyDataTable = m_MainDataTable.Copy();
        m_DataGridView.CellValueChanged += OnDataGridView_CellValueChanged;
    }

    public void EditRedo()
    {
        m_DataGridView.CellValueChanged -= OnDataGridView_CellValueChanged;
        RedoUndo.Redo();
        m_CopyDataTable = m_MainDataTable.Copy();
        m_DataGridView.CellValueChanged += OnDataGridView_CellValueChanged;
    }

    public void EditCopy()
    {

    }

    public void EditCut()
    {

    }

    public void EditPaste()
    {

    }
    #endregion // End Edit

    public bool TryClose()
    {
        if (DataChanged || NeedSaveSourceFile)
        {
            if (MessageBox.Show("文件未保存，确定关闭?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return false;
            }
        }
        return true;
    }

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
            // TODD 利用GetChanges 做简单的版本比较工具
            //DataTable cdt = m_MainDataTable.GetChanges();
            //if (cdt != null)
            //{
            //    for (int i = 0; i < cdt.Rows.Count; i++)
            //    {
            //        if (cdt.Rows[i].RowState == DataRowState.Deleted)
            //        {
            //            Console.WriteLine("删除的行索引{0}，原来数值是{1}", i, cdt.Rows[i][0, DataRowVersion.Original]);
            //        }
            //        else if (cdt.Rows[i].RowState == DataRowState.Modified)
            //        {
            //            Console.WriteLine("修改的行索引{0}，原来数值是{1}，现在的新数值{2}", i, cdt.Rows[i][0, DataRowVersion.Original], cdt.Rows[i][0, DataRowVersion.Current]);
            //        }
            //        else if (cdt.Rows[i].RowState == DataRowState.Added)
            //        {
            //            Console.WriteLine("新添加行索引{0}，数值是{1}", i, cdt.Rows[i][0, DataRowVersion.Current]);
            //        }
            //    }
            //}
            CodeCompare.Instance.Compare(SourceFileFullName, Path.GetTempPath() + m_CurrentCopyFileName, "源文件", "副本");
            BeyondCompare.Instance.Compare(SourceFileFullName, Path.GetTempPath() + m_CurrentCopyFileName);
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
        CsvExport myExport = new CsvExport(",", false);
        try
        {
            for (int rowIdx = 0; rowIdx < m_MainDataTable.Rows.Count; rowIdx++)
            {
                myExport.AddRow();
                DataRow dataRow = m_MainDataTable.Rows[rowIdx];
                for (int colIdx = 0; colIdx < m_MainDataTable.Columns.Count; colIdx++)
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
            m_MainDataTable = new DataTable();
            int rowCount = csvTable.GetLength(0);
            int colCount = -1;
            for (int rowIdx = 0; rowIdx < rowCount; rowIdx++)
            {
                DataRow newRowData = m_MainDataTable.NewRow();
                string[] csvRow = csvTable[rowIdx];
                for (int colIdx = 0; colIdx < csvRow.Length; colIdx++)
                {
                    if (colIdx > colCount)
                    {
                        m_MainDataTable.Columns.Add(colIdx.ToString(), typeof(string));
                        colCount = colIdx;
                    }
                    newRowData[colIdx] = csvRow[colIdx];
                }
                m_MainDataTable.Rows.Add(newRowData);
            }

            m_DataGridView.DataSource = m_MainDataTable;
            m_CopyDataTable = m_MainDataTable.Copy();
            m_MainDataTable.AcceptChanges();
            RedoUndo.SetDataTable(m_MainDataTable);
            UpdateGridHeader();
            m_DataGridView.CellValueChanged += OnDataGridView_CellValueChanged;
            RedoUndo.DoSomething += OnRedoUndo_DoSomethingChange;
        }
        catch (Exception ex)
        {
            Debug.ShowExceptionMessageBox("csv转DataTable失败:" + fileFullName, ex);
            return false;
        }
        return true;
    }

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

        TextBox textBox = m_DataGridView.EditingControl as TextBox;
        int nStart = textBox.SelectionStart;
        m_DataGridView.CurrentCell.Value = textBox.Text.Insert(nStart, "\r\n");
        return base.ProcessCmdKey(ref msg, keyData);
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
        if (e.MyDoType == DataGridViewRedoUndo.DoType.CellValueChange)
        {
            OnDataGridViewData_Change();
        }
        else if(e.MyDoType == DataGridViewRedoUndo.DoType.AddRow)
        {
            OnDataGridViewData_Change();
            UpdateGridHeader();
        }
    }

    /// <summary>
    /// 数据改变时 
    /// 更新标题 & 更新菜单栏
    /// </summary>
    private void OnDataGridViewData_Change()
    {
        NeedSaveSourceFile = true;
        DataChanged = true;
        
        UpdateFormText();
        MainForm.Instance.UpdateAllToolStripMenu();
    }

    /// <summary>
    /// 切换SelectionMode 
    /// 改变DataGridView选择的行列 
    /// 更新菜单项(Un)Frozen状态
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

            // TODO 右键菜单里的操作只针对单行、单列 未来可能支持多行、多列操作
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
        DataRow newRowData = m_MainDataTable.NewRow();
        m_MainDataTable.Rows.InsertAt(newRowData, index);

        m_DataGridView.ClearSelection();
        m_DataGridView.Rows[index].Selected = true;
        m_CopyDataTable = m_MainDataTable.Copy();
        RedoUndo.DoAddRow(index);

        OnDataGridViewData_Change();
        UpdateGridHeader();
        m_DataGridView.CellValueChanged += OnDataGridView_CellValueChanged;
    }

    /// <summary>
    /// 单元格值改变时添加Do事件到RedoUndo里
    /// </summary>
    private void OnDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        RedoUndo.DoCellValueChange(e.ColumnIndex, e.RowIndex, m_CopyDataTable.Rows[e.RowIndex][e.ColumnIndex].ToString(), m_MainDataTable.Rows[e.RowIndex][e.ColumnIndex].ToString());
        m_CopyDataTable = m_MainDataTable.Copy();
        OnDataGridViewData_Change();
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
    #endregion UIEvent
}