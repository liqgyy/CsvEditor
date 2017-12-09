using System;
using System.Windows.Forms;

public partial class GotoForm : Form
{
    private static string ms_RowTextBoxText = "";
    private static string ms_ColTextBoxText = "";

    public GotoForm()
    {
        InitializeComponent();
        m_RowTextBox.Text = ms_RowTextBoxText;
        m_ColTextBox.Text = ms_ColTextBoxText;
    }

    /// <summary>
    /// 跳转到某一个单元格
    /// </summary>
    /// <param name="row">行  范围1~RowCount</param>
    /// <param name="col">列  范围1~ColumnCount</param>
    private void Goto()
    {
        int row, col;
        if (!int.TryParse(m_RowTextBox.Text,out row))
        {
            row = 1;
        }
        if (!int.TryParse(m_ColTextBox.Text, out col))
        {
            col = ConvertUtility.LetterToNumber(m_ColTextBox.Text);
        }

        if (!MainForm.Instance.SelCsvFormInitialized())
        {
            MessageBox.Show("当前没有打开Csv文件", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        DataGridView dataGridView = MainForm.Instance.SelCsvForm.MainDataGridView;

        if (col < 1 || col > dataGridView.ColumnCount || row < 1 || row > dataGridView.RowCount)
        {
            string msgText = string.Format("输入的col或row超出范围.\n接受的范围:\n0 < col <= {0}\n0 < row <= {1}",
                dataGridView.ColumnCount, dataGridView.RowCount);
            MessageBox.Show(msgText, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        dataGridView.CurrentCell = dataGridView.Rows[row - 1].Cells[col - 1];
        return;
    }

    #region UIEvent
    private void OnGotoButton_Click(object sender, EventArgs e)
    {
        Goto();
    }

    private void OnTextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        // 能接收Number/Letter/Enter/Delete/Escape/Back
        if (e.KeyChar == (int)Keys.Enter)
        {
            Goto();
            return;
        }
        if(e.KeyChar == (int)Keys.Delete || e.KeyChar == (int)Keys.Escape || e.KeyChar == (int)Keys.Back)
        {
            return;
        }

        bool isNumber = char.IsDigit(e.KeyChar);
        bool isLetter = !isNumber && char.IsLetter(e.KeyChar);

        if (!isNumber && !isLetter)
        {
            e.Handled = true;
        }
        // 行只能是纯数字
        else if (isLetter && textBox == m_RowTextBox)
        {
            e.Handled = true;
        }
        // 列可以是纯数字或纯字母 第D列 <=> 第4列
        else if (textBox == m_ColTextBox && m_ColTextBox.Text.Length > 0)
        {
            int reslut;
            bool textBoxIsNumber = int.TryParse(m_ColTextBox.Text, out reslut);
            // ColText是纯数字
            if ((isLetter && textBoxIsNumber) || (isNumber && !textBoxIsNumber))
            {
                e.Handled = true;
            }
        }
    }

    private void TextBox_TextChanged(object sender, EventArgs e)
    {
        ms_RowTextBoxText = m_RowTextBox.Text;
        ms_ColTextBoxText = m_ColTextBox.Text;
    }
    #endregion UIEvent
}