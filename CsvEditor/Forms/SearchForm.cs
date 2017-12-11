using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static CsvEditManager;

/// <summary>
/// 搜索窗口
/// UNDONE 选定范围的查找替换实现难度高，优先级低，先不做
/// </summary>
public partial class SearchForm : Form
{
    public bool Initialized = false;

    private static bool ms_IsWildcard = true;
    private static bool ms_IsCase = false;
    private static string ms_SearchText = "";
    private static string ms_ReplaceText = "";

    public SearchForm()
    {
        InitializeComponent();

        m_WildcardCheckBox.Checked = ms_IsWildcard;
        m_CaseCheckBox.Checked = ms_IsCase;
        m_SearchTextBox.Text = ms_SearchText;
        m_ReplaceTextBox.Text = ms_ReplaceText;

        // TEMP 未实现不区分大小写的替换
        m_ReplaceButton.Enabled = ms_IsCase;
        m_ReplaceAllButton.Enabled = ms_IsCase;
        Initialized = true;
    }

    /// <summary>
    /// 从起始位置的下一个位置开始搜索，搜索到文件结尾后从文件开始位置搜索到起始位置
    /// startRow,startCol 必须是dataGridView里存在的Cell，否则会死循环
    /// </summary>
    /// <param name="startRow">起始行</param>
    /// <param name="startCol">起始列</param>
    /// <returns>搜索到的Cell null是没搜索到</returns>
    public DataGridViewCell Searching(DataGridView dataGridView, int startRow, int startCol)
    {
        int nowRow = startRow;
        int nowCol = startCol;
        while (true)
        {
            nowCol++;
            if (nowCol >= dataGridView.ColumnCount)
            {
                nowCol = 0;
                nowRow++;
                if (nowRow >= dataGridView.RowCount)
                {
                    nowCol = 0;
                    nowRow = 0;
                }
            }
            // 没有找到
            if (nowRow == startRow && nowCol == startCol)
            {
                return null;
            }
            DataGridViewCell cell = dataGridView.Rows[nowRow].Cells[nowCol];
            if (Matching((string)cell.Value))
            {
                return cell;
            }
        }
    }

    /// <summary>
    /// 用当前的选项匹配
    /// </summary>
    /// <param name="target">被匹配的字符串</param>
    /// <returns>是否匹配</returns>
    public bool Matching(string target)
    {
        if (string.IsNullOrEmpty(target) || string.IsNullOrEmpty(ms_SearchText))
        {
            return false;
        }

        //不匹配大小写
        if (!ms_IsCase)
        {
            target = target.ToUpper();
            ms_SearchText = ms_SearchText.ToUpper();
        }

        if (ms_IsWildcard)
        {
            return ConvertUtility.WildcardToRegex(ms_SearchText).IsMatch(target);
        }
        else
        {
            return target.Contains(ms_SearchText);
        }
    }

    /// <summary>
    /// 替换
    /// TODO 实现不区分大小写的替换
    /// </summary>
    /// <param name="target">目标字符串</param>
    /// <returns>替换结果</returns>
    public string Replacing(string target)
    {
        if (string.IsNullOrEmpty(target) || string.IsNullOrEmpty(ms_SearchText) || ms_ReplaceText == null)
        {
            return target;
        }
        if (ms_IsWildcard)
        {
            return ConvertUtility.WildcardToRegex(ms_SearchText).Replace(target, ms_ReplaceText);
        }
        else
        {
            return target.Replace(ms_SearchText, ms_ReplaceText);
        }
    }

    #region UIEvent
    private void OnSearchNextButton_Click(object sender, EventArgs e)
    {
        if (!MainForm.Instance.SelCsvFormInitialized())
        {
            MessageBox.Show("当前没有打开Csv文件", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        DataGridView dataGridView = MainForm.Instance.SelCsvForm.MainDataGridView;

        int startRow = 0;
        int startCol = 0;
        if (dataGridView.CurrentCell != null)
        {
            startRow = dataGridView.CurrentCell.RowIndex;
            startCol = dataGridView.CurrentCell.ColumnIndex;
        }

        DataGridViewCell cell = Searching(dataGridView, startRow, startCol);
        if (cell != null)
        {
            dataGridView.ClearSelection();
            dataGridView.CurrentCell = cell;
        }
    }

    private void OnReplaceButton_Click(object sender, EventArgs e)
    {
        if (!MainForm.Instance.SelCsvFormInitialized())
        {
            MessageBox.Show("当前没有打开Csv文件", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        DataGridView dataGridView = MainForm.Instance.SelCsvForm.MainDataGridView;

        // 当前单元格
        if (dataGridView.CurrentCell != null && Matching((string)dataGridView.CurrentCell.Value))
        {
            string oldValue = (string)dataGridView.CurrentCell.Value;
            string newValue = Replacing(oldValue);
            MainForm.Instance.SelCsvForm.EditManager.DidCellValueChange(dataGridView.CurrentCell.ColumnIndex,
               dataGridView.CurrentCell.RowIndex,
               oldValue,
               newValue);

            MainForm.Instance.SelCsvForm.BeforeChangeCellValue();
            dataGridView.CurrentCell.Value = newValue;
            MainForm.Instance.SelCsvForm.AfterChangeCellValue();
            return;
        }

        int startRow = 0;
        int startCol = 0;
        if (dataGridView.CurrentCell != null)
        {
            startRow = dataGridView.CurrentCell.RowIndex;
            startCol = dataGridView.CurrentCell.ColumnIndex;
        }

        MainForm.Instance.SelCsvForm.BeforeChangeCellValue();
        DataGridViewCell cell = Searching(dataGridView, startRow, startCol);
        if (cell != null)
        {
            string oldValue = (string)cell.Value;
            string newValue = Replacing(oldValue);
            MainForm.Instance.SelCsvForm.EditManager.DidCellValueChange(cell.ColumnIndex, cell.RowIndex, oldValue, newValue);

            cell.Value = Replacing(oldValue);
            dataGridView.ClearSelection();
            dataGridView.CurrentCell = cell;
        }
        MainForm.Instance.SelCsvForm.AfterChangeCellValue();
    }

    private void OnReplaceAllButton_Click(object sender, EventArgs e)
    {
        if (!MainForm.Instance.SelCsvFormInitialized())
        {
            MessageBox.Show("当前没有打开Csv文件", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        DataGridView dataGridView = MainForm.Instance.SelCsvForm.MainDataGridView;

        MainForm.Instance.SelCsvForm.BeforeChangeCellValue();
        List<CellValueChangeItem> ChangeList = new List<CellValueChangeItem>();
        DataGridViewCell cell = Searching(dataGridView, 0, 0);
        while(cell != null)
        {
            CellValueChangeItem changeItem = new CellValueChangeItem();
            changeItem.OldValue = (string)cell.Value;
            changeItem.NewValue = Replacing(changeItem.OldValue);
            changeItem.Row = cell.RowIndex;
            changeItem.Column = cell.ColumnIndex;
            ChangeList.Add(changeItem);

            cell.Value = changeItem.NewValue;
            cell = Searching(dataGridView, cell.RowIndex, cell.ColumnIndex);
        }
        MainForm.Instance.SelCsvForm.EditManager.DidCellsValueChange(ChangeList);
        MainForm.Instance.SelCsvForm.AfterChangeCellValue();
    }

    private void OnValueChanged(object sender, EventArgs e)
    {
        if (!Initialized)
        {
            return;
        }
        ms_IsWildcard = m_WildcardCheckBox.Checked;
        ms_IsCase = m_CaseCheckBox.Checked;
        ms_SearchText = m_SearchTextBox.Text;
        ms_ReplaceText = m_ReplaceTextBox.Text;

        // TEMP 未实现不区分大小写的替换
        m_ReplaceButton.Enabled = ms_IsCase;
        m_ReplaceAllButton.Enabled = ms_IsCase;
    }
    #endregion // END UIEvent
}