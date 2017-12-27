using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

/// <summary>
/// TODO 添加m_UndoStack/m_RedoStack的上限
/// </summary>
public class CsvEditManager
{
    private CsvForm m_CsvForm;

    public CsvEditManager(CsvForm csvForm)
    {
        m_CsvForm = csvForm;

        m_UndoStack = new Stack<IUndoRedo>();
        m_RedoStack = new Stack<IUndoRedo>();
    }

	#region Copy\Cut\Paste
	public void Copy()
    {
        if (!CanCopy())
        {
            return;
        }

		if (m_CsvForm.MainDataGridView.IsCurrentCellInEditMode)
		{
			TextBox textBox = m_CsvForm.MainDataGridView.EditingControl as TextBox;
			Clipboard.SetDataObject(textBox.SelectedText);
		}
		else if (MainForm.Instance.CellEditTextBox.Focused)
		{
			Clipboard.SetDataObject(MainForm.Instance.CellEditTextBox.SelectedText);
		}
		else if (m_CsvForm.MainDataGridView.SelectedCells.Count > 0)
		{
			Clipboard.SetDataObject(m_CsvForm.MainDataGridView.GetClipboardContent());
		}
	}

    public void Cut()
    {
        if (!CanCut())
        {
            return;
        }
		Copy();


		if (m_CsvForm.MainDataGridView.IsCurrentCellInEditMode)
		{
			m_CsvForm.BeforeChangeCellValue();
			TextBox textBox = m_CsvForm.MainDataGridView.EditingControl as TextBox;
			CellValueChangeItem change = new CellValueChangeItem();
			try
			{
				int selectionStart = textBox.SelectionStart;
				change.OldValue = textBox.Text;
				change.Row = m_CsvForm.MainDataGridView.CurrentCell.RowIndex;
				change.Column = m_CsvForm.MainDataGridView.CurrentCell.ColumnIndex;
				change.NewValue = change.OldValue.Remove(textBox.SelectionStart, textBox.SelectionLength);

				textBox.Text = change.NewValue;
				textBox.SelectionStart = selectionStart;
				DidCellValueChange(change);
			}
			catch (Exception ex)
			{
				textBox.Text = change.OldValue;
				Debug.ShowExceptionMessageBox("剪切DataGridViewCell数据到剪切板失败", ex);
			}
			finally
			{
				m_CsvForm.AfterChangeCellValue();
			}
		}
		else if (MainForm.Instance.CellEditTextBox.Focused)
		{
			TextBox textBox = MainForm.Instance.CellEditTextBox;
			int selectionStart = textBox.SelectionStart;
			textBox.Text = textBox.Text.Remove(textBox.SelectionStart, textBox.SelectionLength);
			textBox.SelectionStart = selectionStart;
		}
		else if (m_CsvForm.MainDataGridView.SelectedCells.Count > 0)
		{
			m_CsvForm.BeforeChangeCellValue();
			List<CellValueChangeItem> changeList = new List<CellValueChangeItem>();
			for (int cellIdx = 0; cellIdx < m_CsvForm.MainDataGridView.SelectedCells.Count; cellIdx++)
			{
				DataGridViewCell cell = m_CsvForm.MainDataGridView.SelectedCells[cellIdx];

				CellValueChangeItem change = new CellValueChangeItem();
				change.Row = cell.RowIndex;
				change.Column = cell.ColumnIndex;
				change.OldValue = (string)cell.Value;
				change.NewValue = "";
				changeList.Add(change);

				cell.Value = "";
			}
			DidCellsValueChange(changeList);
			m_CsvForm.AfterChangeCellValue();
		}		
	}

	public void Paste()
    {
        if (!CanPaste())
        {
            return;
        }
		
		string clipboardStr = null;
		// 获取剪切板的数据
		try
		{
			DataObject clipboardData = (DataObject)Clipboard.GetDataObject();
			if (!clipboardData.GetDataPresent(DataFormats.Text))
			{
				return;
			}
			clipboardStr = clipboardData.GetText();
		}
		catch (Exception ex)
		{
			Debug.ShowExceptionMessageBox("获取剪切板数据失败", ex);
			return;
		}

		DataGridView dataGridView = m_CsvForm.MainDataGridView;

		// 粘贴到Cell
		if (dataGridView.IsCurrentCellInEditMode)
		{
			PasteCell(dataGridView, clipboardStr);
		}
		else if (MainForm.Instance.CellEditTextBox.Focused)
		{
			TextBox textBox = MainForm.Instance.CellEditTextBox;
			int selectionStart = textBox.SelectionStart;
			string newValue = textBox.Text;
			newValue = newValue.Remove(selectionStart, textBox.SelectionLength);
			newValue = newValue.Insert(selectionStart, clipboardStr);
			textBox.Text = newValue;
			textBox.SelectionStart = selectionStart + clipboardStr.Length;

		}
		// 粘贴到DataGridView
		else if (dataGridView.SelectedCells.Count > 0)
		{
			if (dataGridView.SelectedCells.Count == 1)
			{
				PasetCells(dataGridView, clipboardStr);
			}
			else
			{
				MessageBox.Show("选中多个单元格时不能粘贴", "提示", MessageBoxButtons.OK);
			}
		}
	}

	private void PasteCell(DataGridView dataGridView, string clipboardStr)
	{
		m_CsvForm.BeforeChangeCellValue();
		TextBox textBox = dataGridView.EditingControl as TextBox;
		CellValueChangeItem change = new CellValueChangeItem();
		try
		{
			change.OldValue = textBox.Text;
			change.Row = dataGridView.CurrentCell.RowIndex;
			change.Column = dataGridView.CurrentCell.ColumnIndex;

			int selectionStart = textBox.SelectionStart;
			string newValue = textBox.Text;
			newValue = newValue.Remove(selectionStart, textBox.SelectionLength);
			newValue = newValue.Insert(selectionStart, clipboardStr);
			// 在这之后不会抛出异常
			textBox.Text = newValue;
			textBox.SelectionStart = selectionStart + clipboardStr.Length;

			change.NewValue = newValue;
			DidCellValueChange(change);
		}
		catch (Exception ex)
		{
			textBox.Text = change.OldValue;
			Debug.ShowExceptionMessageBox("粘贴到Cell失败", ex);
		}
		finally
		{
			m_CsvForm.AfterChangeCellValue();
		}
	}

	private void PasetCells(DataGridView dataGridView, string clipboardStr)
	{
		// 引用下面两个链接
		// https://stackoverflow.com/questions/22833327/pasting-excel-data-into-a-blank-datagridview-index-out-of-range-exception
		// https://stackoverflow.com/questions/1679778/is-it-possible-to-paste-excel-csv-data-from-clipboard-to-datagridview-in-c
		List<CellValueChangeItem> changeList = new List<CellValueChangeItem>();
		try
		{
			m_CsvForm.BeforeChangeCellValue();
			string[] lines = Regex.Split(clipboardStr.TrimEnd("\r\n".ToCharArray()), "\r\n");
			// 当前行
			int currentRow = dataGridView.CurrentCell.RowIndex;
			// 当前列
			int currentCol = dataGridView.CurrentCell.ColumnIndex;
			DataGridViewCell currentCell;
			for (int lineIdx = 0; lineIdx < lines.Length; lineIdx++)
			{
				string line = lines[lineIdx];
				// 行超过表格限制，默认不添加新行
				if (currentRow >= dataGridView.RowCount)
				{
					throw (new ArgumentOutOfRangeException("currentRow", "粘贴的行超出范围"));
				}
				// 忽略空行
				if (string.IsNullOrEmpty(line))
				{
					continue;
				}
				string[] cells = line.Split('\t');
				for (int cellIdx = 0; cellIdx < cells.Length; ++cellIdx)
				{
					// 列超过表格限制，默认不添加新列
					if (currentCol + cellIdx >= dataGridView.ColumnCount)
					{
						throw (new ArgumentOutOfRangeException("currentCol", "粘贴的列超出范围"));
					}
					currentCell = dataGridView.Rows[currentRow].Cells[currentCol + cellIdx];
					string cell = cells[cellIdx];
					// 忽略空值
					if (string.IsNullOrEmpty(cell))
					{
						continue;
					}
					if (currentCell.Value == null || currentCell.Value.ToString() != cells[cellIdx])
					{
						CellValueChangeItem change = new CellValueChangeItem();
						change.Row = currentCell.RowIndex;
						change.Column = currentCell.ColumnIndex;
						change.OldValue = (string)currentCell.Value;
						change.NewValue = cells[cellIdx];

						currentCell.Value = cells[cellIdx];
						changeList.Add(change);
					}
				}
				currentRow++;
			}
		}
		catch (ArgumentOutOfRangeException ex)
		{
			MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK);
			if (changeList.Count > 0)
			{
				DoCellsValueChangeEvent doCellsValueChangeEvent = new DoCellsValueChangeEvent
				{
					ChangeList = changeList
				};
				doCellsValueChangeEvent.Undo(dataGridView, null);
			}
			changeList = null;
		}
		catch (Exception ex)
		{
			Debug.ShowExceptionMessageBox("粘贴到DataGridView失败", ex);
		}
		finally
		{
			DidCellsValueChange(changeList);
			m_CsvForm.AfterChangeCellValue();
		}
	}

	public bool CanCopy()
	{
		// 编辑模式中没有选中文本
		if (m_CsvForm.MainDataGridView.IsCurrentCellInEditMode)
		{
			TextBox textBox = m_CsvForm.MainDataGridView.EditingControl as TextBox;
			if (textBox.SelectionLength > 0)
			{
				return true;
			}
		}
		if (m_CsvForm.MainDataGridView.SelectedCells.Count > 0)
		{
			return true;
		}
		if (MainForm.Instance.CellEditTextBox.Focused)
		{
			if (MainForm.Instance.CellEditTextBox.SelectionLength > 0)
			{
				return true;
			}
		}
		return false;
	}

	public bool CanCut()
    {
        return CanCopy();
    }

    public bool CanPaste()
    {
		if (m_CsvForm.MainDataGridView.IsCurrentCellInEditMode)
		{
			return true;
		}
		if (m_CsvForm.MainDataGridView.SelectedCells.Count > 0)
		{
			return true;
		}
		if (MainForm.Instance.CellEditTextBox.Focused)
		{
			return true;
		}
		return false;
    }
    #endregion // End Copy\Cut\Paste

    #region Undo\Redo
    public delegate void DoSomethingEventHandler(object sender, DoSomethingEventArgs e);
    public event DoSomethingEventHandler DoSomething;

    private Stack<IUndoRedo> m_UndoStack;
    private Stack<IUndoRedo> m_RedoStack;

    public void DidAddRow(int row)
    {
        m_UndoStack.Push(new DoAddRowEvent { Row = row });
    }

    public void DidCellValueChange(int column, int row, string oldValue, string newValue)
    {
        CellValueChangeItem change = new CellValueChangeItem();
		change.Column = column;
		change.Row = row;
		change.OldValue = oldValue;
		change.NewValue = newValue;
		DidCellValueChange(change);
	}

	public void DidCellValueChange(CellValueChangeItem change)
	{
		DoCellsValueChangeEvent doCellsValueChangeEvent = new DoCellsValueChangeEvent();
		doCellsValueChangeEvent.ChangeList = new List<CellValueChangeItem>();
		doCellsValueChangeEvent.ChangeList.Add(change);

		m_UndoStack.Push(doCellsValueChangeEvent);
	}

	public void DidCellsValueChange(List<CellValueChangeItem> changeList)
    {
        if (changeList == null || changeList.Count == 0)
        {
            return;
        }
        DoCellsValueChangeEvent doCellsValueChangeEvent = new DoCellsValueChangeEvent
        {
            ChangeList = changeList
        };
        m_UndoStack.Push(doCellsValueChangeEvent);
    }

    public bool CanUndo()
    {
        return m_UndoStack.Count > 0;
    }

    public bool CanRedo()
    {
		return m_RedoStack.Count > 0;
    }

    public void Undo()
    {
        if (!CanUndo())
        {
            return;
        }
        m_CsvForm.BeforeChangeCellValue();
        IUndoRedo iur = m_UndoStack.Pop();
        iur.Undo(m_CsvForm.MainDataGridView, m_CsvForm.MainDataTable);
        m_RedoStack.Push(iur);
        DoSomething(this, new DoSomethingEventArgs(DoEventType.Undo, iur.GetDoType()));
        m_CsvForm.AfterChangeCellValue();

		MainForm.Instance.UpdateCellEdit();
    }

    public void Redo()
    {
        if (!CanRedo())
        {
            return;
        }
        m_CsvForm.BeforeChangeCellValue();
        IUndoRedo iur = m_RedoStack.Pop();
        iur.Redo(m_CsvForm.MainDataGridView, m_CsvForm.MainDataTable);
        m_UndoStack.Push(iur);
        DoSomething(this, new DoSomethingEventArgs(DoEventType.Redo, iur.GetDoType()));
        m_CsvForm.AfterChangeCellValue();

		MainForm.Instance.UpdateCellEdit();
	}

	private void Did(IUndoRedo iur)
    {
        m_UndoStack.Push(iur);
        m_RedoStack.Peek();
        DoSomething(this, new DoSomethingEventArgs(DoEventType.Do, iur.GetDoType()));
    }

    public interface IUndoRedo
    {
        void Undo(DataGridView dataGridView, DataTable dataTable);
        void Redo(DataGridView dataGridView, DataTable dataTable);
        DoType GetDoType();
    }

    public class DoAddRowEvent : IUndoRedo
    {
        public int Row;

        public DoType GetDoType()
        {
            return DoType.AddRow;
        }

        public void Redo(DataGridView dataGridView, DataTable dataTable)
        {
            DataRow newRowData = dataTable.NewRow();
            dataTable.Rows.InsertAt(newRowData, Row);

            dataGridView.ClearSelection();
            dataGridView.Rows[Row].Selected = true;
        }

        public void Undo(DataGridView dataGridView, DataTable dataTable)
        {
            dataGridView.Rows.RemoveAt(Row);
        }
    }

    public struct CellValueChangeItem
    {
        public int Column;
        public int Row;
        public string OldValue;
        public string NewValue;
    }

    public class DoCellsValueChangeEvent : IUndoRedo
    {
        public List<CellValueChangeItem> ChangeList;

        public DoType GetDoType()
        {
            return DoType.CellsValueChange;
        }

        public void Redo(DataGridView dataGridView, DataTable dataTable)
        {
            for(int changeIdx = 0; changeIdx < ChangeList.Count; changeIdx++)
            {
                CellValueChangeItem changeItem = ChangeList[changeIdx];
                dataGridView.Rows[changeItem.Row].Cells[changeItem.Column].Value = changeItem.NewValue;
            }
        }

        public void Undo(DataGridView dataGridView, DataTable dataTable)
        {
            for (int changeIdx = 0; changeIdx < ChangeList.Count; changeIdx++)
            {
                CellValueChangeItem changeItem = ChangeList[changeIdx];
                dataGridView.Rows[changeItem.Row].Cells[changeItem.Column].Value = changeItem.OldValue;
            }
        }
    }

    public class DoSomethingEventArgs : EventArgs
    {
        public DoEventType MyEventType;
        public DoType MyDoType;

        public DoSomethingEventArgs(DoEventType eventType, DoType doType)
        {
            MyEventType = eventType;
            MyDoType = doType;
        }
    }

    public enum DoEventType
    {
        Do,
        Redo,
        Undo
    }

    public enum DoType
    {
        AddRow,
        CellsValueChange
    }
    #endregion // End Undo\Redo
}