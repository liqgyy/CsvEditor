using System;
using System.Windows.Forms;

public partial class MergeLocalizationForm : Form
{
	public MergeLocalizationForm()
	{
		InitializeComponent();
	}

	#region UIEvent
	private void m_OkButton_Click(object sender, EventArgs e)
	{

	}

	private void OnCancelButton_Click(object sender, EventArgs e)
	{
		Close();
		Dispose();
	}

	private void OnOpenCsvFileDialogButton_Click(object sender, EventArgs e)
	{

	}

	private void OnCsvPathTextBox_TextChanged(object sender, EventArgs e)
	{

	}
	#endregion // END UIEvent
}