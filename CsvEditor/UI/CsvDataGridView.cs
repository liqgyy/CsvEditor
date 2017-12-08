using System.Windows.Forms;

public sealed class CsvDataGridView : DataGridView
{
	protected override void OnColumnHeaderMouseClick(DataGridViewCellMouseEventArgs e)
	{
		SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
	}

	protected override void OnRowHeaderMouseClick(DataGridViewCellMouseEventArgs e)
	{
		SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
	}
}