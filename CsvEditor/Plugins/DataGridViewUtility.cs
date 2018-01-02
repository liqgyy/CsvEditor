using System.Data;
using System.Windows.Forms;

public class DataGridViewUtility
{
	/// <summary>
	/// 选中单元格
	/// </summary>
	public static bool SelectCell(DataGridView dataGridView,int row, int column)
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

	/// <summary>
	/// 插入新行
	/// </summary>
	public static void InsertNewRow(DataGridView dataGridView, DataTable dataTable, int index, string defalutValue = "")
	{
		DataRow newRowData = dataTable.NewRow();
		dataTable.Rows.InsertAt(newRowData, index);

		DataGridViewRow newRow = dataGridView.Rows[index];
		for (int colIdx = 0; colIdx < newRow.Cells.Count; colIdx++)
		{
			newRow.Cells[colIdx].Value = defalutValue;
		}
	}
}