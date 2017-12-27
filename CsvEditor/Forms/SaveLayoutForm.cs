using System;
using System.Windows.Forms;

public partial class SaveLayoutForm : Form
{
	public SaveLayoutForm()
	{
		InitializeComponent();
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

		if (CsvLayoutManager.Instance.ExistSpecific(layoutName))
		{
			if (MessageBox.Show(string.Format("名为\"{0}\"的布局已存在。\n是否要替换？", layoutName), "提示",MessageBoxButtons.YesNo) == DialogResult.No)
			{
				return;
			}
		}
		MainForm.Instance.SelCsvForm.SaveLayout();
		CsvLayout csvLayout = SerializeUtility.ObjectCopy(MainForm.Instance.SelCsvForm.GetLayout());
		csvLayout.Key = layoutName;
		CsvLayoutManager.Instance.AddSpecific(csvLayout);
		CsvLayoutManager.Instance.SaveSpecific();
		Close();
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
