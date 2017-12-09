using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    #region UIEvent
    private void OnSearchNextButton_Click(object sender, EventArgs e)
    {

    }

    private void OnReplaceButton_Click(object sender, EventArgs e)
    {

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