using System;
using System.Windows.Forms;

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
        Initialized = true;
    }

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
        if (Matching((string)dataGridView.CurrentCell.Value))
        {
            return;
        }
    }

    private void OnReplaceAllButton_Click(object sender, EventArgs e)
    {

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
    }
    #endregion // END UIEvent
}