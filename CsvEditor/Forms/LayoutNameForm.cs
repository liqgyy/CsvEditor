using System;
using System.Windows.Forms;

public partial class LayoutNameForm : Form
{
	public Func<string, bool> OnApply;

	public LayoutNameForm(string layoutName = "New CsvLayout")
	{
		InitializeComponent();
		m_LayoutTextBox.Text = layoutName;
	}

	#region UIEvent
	private void OnOkButton_Click(object sender, EventArgs e)
	{
		string layoutName = m_LayoutTextBox.Text.Trim();
		if (string.IsNullOrEmpty(layoutName))
		{
			MessageBox.Show("请输入名称", "提示");
			return;
		}

		if (OnApply != null)
		{
			if (OnApply(layoutName))
			{
				Close();
				Dispose();
			}
		}
	}

	private void OnCancelButton_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void OnLayoutTextBox_TextChanged(object sender, EventArgs e)
	{
		m_OkButton.Enabled = !string.IsNullOrEmpty(m_LayoutTextBox.Text.Trim());
	}
	#endregion UIEvent	
}
