using System.Windows.Forms;

public class DataGridViewUtility
{
	/// <summary>
	/// 选中单元格
	/// </summary>
	public static bool SelectDataGridViewCell(DataGridView dataGridView,int row, int column)
	{
		// 超出范围
		if (row >= dataGridView.RowCount || column >= dataGridView.ColumnCount)
		{
			return false;
		}

		dataGridView.ClearSelection();
		if (row >= 0 && column >= 0)
		{
			dataGridView.CurrentCell = dataGridView.Rows[row].Cells[column];
		}
		else if (row < 0 && column >= 0)
		{
			dataGridView.CurrentCell = dataGridView.Rows[0].Cells[column];
			dataGridView.Columns[column].Selected = true;
		}
		else if (row >= 0 && column < 0)
		{
			dataGridView.CurrentCell = dataGridView.Rows[row].Cells[0];
			dataGridView.Rows[row].Selected = true;
		}
		else if (row < 0 && column < 0)
		{
			dataGridView.SelectAll();
		}
		return true;
	}
}