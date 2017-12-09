using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class GotoForm : Form
{
    public GotoForm()
    {
        InitializeComponent();
    }

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
        MainForm.Instance.GotoCsvGridDataViewCell(row, col);
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
    #endregion UIEvent
}