using System;
using System.Windows.Forms;

public partial class LayoutManagerForm : Form
{
	public LayoutManagerForm()
	{
		InitializeComponent();

		UpdateListBox();
		UpdateButtonEnable();
	}

	private void UpdateListBox()
	{
		m_SavedLayoutListBox.Items.Clear();
		string[] layoutKeys = CsvLayoutManager.Instance.GetSpecificKeys();
		m_SavedLayoutListBox.Items.AddRange(layoutKeys);
	}

	private void UpdateButtonEnable()
	{
		m_UpButton.Enabled = false;
		m_DownButton.Enabled = false;
		m_RenameButton.Enabled = false;
		m_DeleteButton.Enabled = false;

		int selectedIndex = m_SavedLayoutListBox.SelectedIndex;
		if (selectedIndex >= 0)
		{
			m_RenameButton.Enabled = true;
			m_DeleteButton.Enabled = true;
			if (selectedIndex > 0)
			{
				m_UpButton.Enabled = true;
			}
			if (selectedIndex < m_SavedLayoutListBox.Items.Count - 1)
			{
				m_DownButton.Enabled = true;
			}
		}
	}

	#region UIEvent
	private void OnRenameButton_Click(object sender, EventArgs e)
	{
		LayoutNameForm layoutNameForm = new LayoutNameForm((string)m_SavedLayoutListBox.SelectedItem);
		layoutNameForm.StartPosition = FormStartPosition.CenterParent;
		layoutNameForm.Text = "重命名布局";
		layoutNameForm.OnApply = OnRenameLayout;
		layoutNameForm.ShowDialog();
	}

	private void OnDeleteButton_Click(object sender, EventArgs e)
	{
		if (MessageBox.Show(string.Format("是否确定要删除布局\"{0}\"?", m_SavedLayoutListBox.SelectedItem), 
			"提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
		{
			int selectedIdx = m_SavedLayoutListBox.SelectedIndex;
			CsvLayoutManager.Instance.DeleteSpecific(selectedIdx);
			CsvLayoutManager.Instance.SaveSpecific();

			UpdateListBox();
			int targetIdx = selectedIdx;
			if (targetIdx > m_SavedLayoutListBox.Items.Count - 1)
			{
				targetIdx--;
			}
			m_SavedLayoutListBox.SelectedIndex = targetIdx;
			UpdateButtonEnable();
		}
	}

	private void OnCloseButton_Click(object sender, EventArgs e)
	{
		Close();
		Dispose();
	}

	private void OnRearrangeButton_Click(object sender, EventArgs e)
	{
		Button item = (Button)sender;
		int offset = 0;

		if (item == m_UpButton)
		{
			offset = -1;
		}
		else if (item == m_DownButton)
		{
			offset = 1;
		}
		else
		{
			return;
		}

		int sourceIdx = m_SavedLayoutListBox.SelectedIndex;
		int targetIdx = sourceIdx + offset;

		CsvLayoutManager.Instance.SwapSpecific(sourceIdx, targetIdx);
		CsvLayoutManager.Instance.SaveSpecific();

		UpdateListBox();
		m_SavedLayoutListBox.SelectedIndex = targetIdx;
		UpdateButtonEnable();
	}

	private void OnSavedLayoutListBox_SelectedValueChanged(object sender, EventArgs e)
	{
		UpdateButtonEnable();
	}
	#endregion // END UIEvent

	private bool OnRenameLayout(string layoutName)
	{
		string oldName = (string)m_SavedLayoutListBox.SelectedItem;
		if (oldName.Equals(layoutName))
		{
			return true;
		}
		if (CsvLayoutManager.Instance.ExistSpecific(layoutName))
		{
			MessageBox.Show(string.Format("", layoutName), "提示", MessageBoxButtons.OK);
			return false;
		}

		CsvLayout layout = CsvLayoutManager.Instance.LoadOrCreateSpecific(oldName);
		layout.Key = layoutName;
		CsvLayoutManager.Instance.SaveSpecific();

		int selectedIdx = m_SavedLayoutListBox.SelectedIndex;
		UpdateListBox();
		m_SavedLayoutListBox.SelectedIndex = selectedIdx;
		UpdateButtonEnable();
		return true;
	}
}