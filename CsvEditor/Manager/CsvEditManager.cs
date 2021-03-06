﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text;

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

	/*
	Excel复制粘贴的格式 "1\t5\r\n\"2\n3\"\t6\r\n4\t7\r\n"
	1         5
	2\r\n3    6
	4         7
	*/
	#region Copy\Cut\Paste
	public void Copy()
    {
        if (!CanCopy())
        {
            return;
        }

		DataGridView dataGridView = m_CsvForm.GetDataGridView();
		Debug.Assert(dataGridView != null);

		if (m_CsvForm.GetDataGridView().IsCurrentCellInEditMode)
		{
			TextBox textBox = m_CsvForm.GetDataGridView().EditingControl as TextBox;
			Clipboard.SetDataObject(textBox.SelectedText);
		}
		else if (MainForm.Instance.GetCellEditTextBox().Focused)
		{
			Clipboard.SetDataObject(MainForm.Instance.GetCellEditTextBox().SelectedText);
		}
		else if (m_CsvForm.GetDataGridView().SelectedCells.Count > 0)
		{
			try
			{
				// 转换为Array
				IList selectedCellList = m_CsvForm.GetDataGridView().SelectedCells;
				DataGridViewCell[] selectedCells = new DataGridViewCell[selectedCellList.Count];
				selectedCellList.CopyTo(selectedCells, 0);

				// x = Row, y = Col
				Point leftUp = new Point(int.MaxValue, int.MaxValue);
				Point rightDown = new Point(-1, -1);

				// 计算leftUp、rightDown
				for (int cellIdx = 0; cellIdx < selectedCells.Length; cellIdx++)
				{
					DataGridViewCell cell = selectedCells[cellIdx];
					if (cell.RowIndex < leftUp.X)
					{
						leftUp.X = cell.RowIndex;
					}
					if (cell.ColumnIndex < leftUp.Y)
					{
						leftUp.Y = cell.ColumnIndex;
					}

					if (cell.RowIndex > rightDown.X)
					{
						rightDown.X = cell.RowIndex;
					}
					if (cell.ColumnIndex > rightDown.Y)
					{
						rightDown.Y = cell.ColumnIndex;
					}
				}

				int cellCount = (rightDown.X - leftUp.X + 1) * (rightDown.Y - leftUp.Y + 1);
				if (cellCount != selectedCells.Length)
				{
					MessageBox.Show("不能对多重选择区域执行此操作.\n请选择单个区域,然后再试.", "提示");
				}
				else
				{
					CopyCells(dataGridView, leftUp, rightDown);
				}
			}
			catch (Exception ex)
			{
				DebugUtility.ShowExceptionMessageBox("从表格粘贴数据失败", ex);
			}
		}
	}

    public void Cut()
    {
        if (!CanCut())
        {
            return;
        }
		Copy();


		if (m_CsvForm.GetDataGridView().IsCurrentCellInEditMode)
		{
			m_CsvForm.BeforeChangeCellValue();
			TextBox textBox = m_CsvForm.GetDataGridView().EditingControl as TextBox;
			CellValueChangeItem change = new CellValueChangeItem();
			try
			{
				int selectionStart = textBox.SelectionStart;
				change.OldValue = textBox.Text;
				change.Row = m_CsvForm.GetDataGridView().CurrentCell.RowIndex;
				change.Column = m_CsvForm.GetDataGridView().CurrentCell.ColumnIndex;
				change.NewValue = change.OldValue.Remove(textBox.SelectionStart, textBox.SelectionLength);

				textBox.Text = change.NewValue;
				textBox.SelectionStart = selectionStart;
				DidCellValueChange(change);
			}
			catch (Exception ex)
			{
				textBox.Text = change.OldValue;
				DebugUtility.ShowExceptionMessageBox("剪切DataGridViewCell数据到剪切板失败", ex);
			}
			finally
			{
				m_CsvForm.AfterChangeCellValue();
			}
		}
		else if (MainForm.Instance.GetCellEditTextBox().Focused)
		{
			TextBox textBox = MainForm.Instance.GetCellEditTextBox();
			int selectionStart = textBox.SelectionStart;
			textBox.Text = textBox.Text.Remove(textBox.SelectionStart, textBox.SelectionLength);
			textBox.SelectionStart = selectionStart;
		}
		else if (m_CsvForm.GetDataGridView().SelectedCells.Count > 0)
		{
			m_CsvForm.BeforeChangeCellValue();

			// 转换为Array
			IList selectedCellList = m_CsvForm.GetDataGridView().SelectedCells;
			DataGridViewCell[] selectedCells = new DataGridViewCell[selectedCellList.Count];
			selectedCellList.CopyTo(selectedCells, 0);
			CellValueChangeItem[] changes = new CellValueChangeItem[selectedCells.Length];

			for (int cellIdx = 0; cellIdx < selectedCells.Length; cellIdx++)
			{
				DataGridViewCell cell = selectedCells[cellIdx];

				CellValueChangeItem change = new CellValueChangeItem();
				change.Row = cell.RowIndex;
				change.Column = cell.ColumnIndex;
				change.OldValue = (string)cell.Value;
				change.NewValue = "";
				changes[cellIdx] = change;

				cell.Value = "";
			}
			DidCellsValueChange(changes.ToList());
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
			DebugUtility.ShowExceptionMessageBox("获取剪切板数据失败", ex);
			return;
		}

		DataGridView dataGridView = m_CsvForm.GetDataGridView();

		// 粘贴到Cell
		if (dataGridView.IsCurrentCellInEditMode)
		{
			PasteCell(dataGridView, clipboardStr);
			MainForm.Instance.UpdateCellEdit();
		}
		else if (MainForm.Instance.GetCellEditTextBox().Focused)
		{
			TextBox textBox = MainForm.Instance.GetCellEditTextBox();
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
			MainForm.Instance.UpdateCellEdit();
		}
	}

	public bool CanCopy()
	{
		// 编辑模式中没有选中文本
		if (m_CsvForm.GetDataGridView().IsCurrentCellInEditMode)
		{
			TextBox textBox = m_CsvForm.GetDataGridView().EditingControl as TextBox;
			if (textBox.SelectionLength > 0)
			{
				return true;
			}
		}
		if (m_CsvForm.GetDataGridView().SelectedCells.Count > 0)
		{
			return true;
		}
		if (MainForm.Instance.GetCellEditTextBox().Focused)
		{
			if (MainForm.Instance.GetCellEditTextBox().SelectionLength > 0)
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
		if (m_CsvForm.GetDataGridView().IsCurrentCellInEditMode)
		{
			return true;
		}
		if (m_CsvForm.GetDataGridView().SelectedCells.Count > 0)
		{
			return true;
		}
		if (MainForm.Instance.GetCellEditTextBox().Focused)
		{
			return true;
		}
		return false;
    }

	private void CopyCells(DataGridView dataGridView, Point leftUp, Point rightDown)
	{
		List<DataGridViewConsoleForm.Message> messageList = new List<DataGridViewConsoleForm.Message>();

		// 这里是参考DataGridView.GetClipboardContent()
		DataObject dataObject = new DataObject();
		string cellContent = null;
		DataGridViewColumn dataGridViewColumn, nextDataGridViewColumn;
		StringBuilder sbContent = new StringBuilder(1024);

		int lRowIndex = leftUp.X;
		int uRowIndex = rightDown.X;
		DataGridViewColumn lColumn = dataGridView.Columns[leftUp.Y];
		DataGridViewColumn uColumn = dataGridView.Columns[rightDown.Y];

		Debug.Assert(lRowIndex != -1);
		Debug.Assert(uRowIndex != -1);
		Debug.Assert(lColumn != null);
		Debug.Assert(uColumn != null);
		Debug.Assert(lColumn.Index <= uColumn.Index);
		Debug.Assert(lRowIndex <= uRowIndex);

		// Cycle through the visible rows from lRowIndex to uRowIndex.
		int rowIndex = lRowIndex;
		int nextRowIndex = -1;
		Debug.Assert(rowIndex != -1);
		while (rowIndex != -1)
		{
			if (rowIndex != uRowIndex)
			{
				nextRowIndex = dataGridView.Rows.GetNextRow(rowIndex, DataGridViewElementStates.Visible);
				Debug.Assert(nextRowIndex != -1);
			}
			else
			{
				nextRowIndex = -1;
			}

			// Cycle through the visible columns from lColumn to uColumn
			dataGridViewColumn = lColumn;
			Debug.Assert(dataGridViewColumn != null);
			while (dataGridViewColumn != null)
			{
				if (dataGridViewColumn != uColumn)
				{
					nextDataGridViewColumn = dataGridView.Columns.GetNextColumn(dataGridViewColumn, DataGridViewElementStates.Visible, DataGridViewElementStates.None);
					Debug.Assert(nextDataGridViewColumn != null);
				}
				else
				{
					nextDataGridViewColumn = null;
				}

				cellContent = (string)dataGridView.Rows.SharedRow(rowIndex).Cells[dataGridViewColumn.Index].Value;

				// 验证单元格内容
				if (!string.IsNullOrEmpty(cellContent))
				{
					cellContent.Replace("\t", "");

					if (cellContent[0] == '\"')
					{
						DataGridViewConsoleForm.Message message = new DataGridViewConsoleForm.Message();
						message.Level = DataGridViewConsoleForm.Level.Warning;
						message.Row = rowIndex;
						message.Column = dataGridViewColumn.Index;
						message.Caption = "第一个字符是(\")";
						message.Text = "不支持复制到Excel(可以复制，但是可能会串行错行,或两个单元格被合并为一个)";
						messageList.Add(message);
					}

					if (cellContent.Contains("\n"))
					{
						cellContent = cellContent.Replace("\r\n", "\n");
						cellContent = "\"" + cellContent + "\"";
					}
				}

				if (nextDataGridViewColumn == null)
				{
					if (nextRowIndex != -1)
					{
						cellContent = cellContent + "\r\n";
					}
				}
				else
				{
					cellContent = cellContent + "\t";
				}

				sbContent.Append(cellContent);
				dataGridViewColumn = nextDataGridViewColumn;
			}
			rowIndex = nextRowIndex;
		}
#if DEBUG
		string content = sbContent.ToString();
		Clipboard.SetDataObject(content);
#else
		Clipboard.SetDataObject(sbContent.ToString());
#endif
		DataGridViewConsoleForm.ShowForm(messageList, dataGridView, "复制");
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
			DebugUtility.ShowExceptionMessageBox("粘贴到Cell失败", ex);
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
					throw (new ArgumentOutOfRangeException(null,
						string.Format("粘贴数据({0})行的第({1})行到表中第({2})行失败\n表一共有({3})行",
						lines.Length,
						lineIdx + 1,
						currentRow + 1,
						dataGridView.RowCount)));
				}

				string[] cells = line.Split('\t');
				for (int cellIdx = 0; cellIdx < cells.Length; ++cellIdx)
				{
					// 列超过表格限制，默认不添加新列
					if (currentCol + cellIdx >= dataGridView.ColumnCount)
					{
						throw (new ArgumentOutOfRangeException(null,
							string.Format("粘贴数据({0})列的第({1})列到表中第({2})列失败\n表一共有({3})列",
								cells.Length,
								cellIdx + 1,
								ConvertUtility.NumberToLetter(currentCol + cellIdx + 1),
								ConvertUtility.NumberToLetter(dataGridView.ColumnCount))));
					}
					currentCell = dataGridView.Rows[currentRow].Cells[currentCol + cellIdx];
					string cell = cells[cellIdx];
					if (currentCell.Value == null || currentCell.Value.ToString() != cell)
					{
						// 如果cell是多行数据，去除两侧的引号
						if (cell.Contains("\n") && cell[0] == '"' && cell[cell.Length - 1] == '"')
						{
							cell = cell.Substring(1, cell.Length - 2);
						}

						CellValueChangeItem change = new CellValueChangeItem();
						change.Row = currentCell.RowIndex;
						change.Column = currentCell.ColumnIndex;
						change.OldValue = (string)currentCell.Value;
						change.NewValue = cell;

						currentCell.Value = cell;
						changeList.Add(change);
					}
				}
				currentRow++;
			}
		}
		catch (ArgumentOutOfRangeException ex)
		{
			MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK);
			// 粘贴失败，还原到粘贴前
#if !DEBUG
			if (changeList.Count > 0)
			{
				DoCellsValueChangeEvent doCellsValueChangeEvent = new DoCellsValueChangeEvent
				{
					ChangeList = changeList
				};
				doCellsValueChangeEvent.Undo(dataGridView, null);
			}
			changeList = null;
#endif
		}
		catch (Exception ex)
		{
			DebugUtility.ShowExceptionMessageBox("粘贴到DataGridView失败", ex);
		}
		finally
		{
			DidCellsValueChange(changeList);
			m_CsvForm.AfterChangeCellValue();
		}
	}
	#endregion // End Copy\Cut\Paste

	#region Undo\Redo
	public delegate void DoSomethingEventHandler(object sender, DoSomethingEventArgs e);
    public event DoSomethingEventHandler DoSomething;

    private Stack<IUndoRedo> m_UndoStack;
    private Stack<IUndoRedo> m_RedoStack;

    public void DidAddRow(int row)
    {
		DoAddRowEvent doAddRowEvent = new DoAddRowEvent { Row = row };
		Did(doAddRowEvent);
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

		Did(doCellsValueChangeEvent);
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
		Did(doCellsValueChangeEvent);
	}

	public void DidManyThings(List<IUndoRedo> thingList)
	{
		if (thingList == null || thingList.Count == 0)
		{
			return;
		}
		DoManyThingsEvent doManyThings = new DoManyThingsEvent
		{
			ThingsList = thingList
		};
		Did(doManyThings);
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
		if (CanUndo())
		{
			try
			{
				m_CsvForm.BeforeChangeCellValue();
				IUndoRedo iur = m_UndoStack.Pop();
				iur.Undo(m_CsvForm.GetDataGridView(), m_CsvForm.GetDataTable());
				m_RedoStack.Push(iur);
				DoSomething(this, new DoSomethingEventArgs(DoEventType.Undo, iur.GetDoType()));
				m_CsvForm.AfterChangeCellValue();

				MainForm.Instance.UpdateCellEdit();
			}
			catch (System.Exception ex)
			{
				DebugUtility.ShowExceptionMessageBox("撤销失败", ex);
			}
		}
	}

	public void Redo()
	{
		if (CanRedo())
		{
			try
			{
				m_CsvForm.BeforeChangeCellValue();
				IUndoRedo iur = m_RedoStack.Pop();
				iur.Redo(m_CsvForm.GetDataGridView(), m_CsvForm.GetDataTable());
				m_UndoStack.Push(iur);
				DoSomething(this, new DoSomethingEventArgs(DoEventType.Redo, iur.GetDoType()));
				m_CsvForm.AfterChangeCellValue();

				MainForm.Instance.UpdateCellEdit();
			}
			catch (System.Exception ex)
			{
				DebugUtility.ShowExceptionMessageBox("重做失败", ex);
			}
		}
	}

	private void Did(IUndoRedo iur)
    {
		try
		{
			m_UndoStack.Push(iur);
			m_RedoStack.Clear();
		}
		catch (System.Exception ex)
		{
			DebugUtility.ShowExceptionMessageBox("添加撤销操作失败", ex);
		}
        DoSomething(this, new DoSomethingEventArgs(DoEventType.Did, iur.GetDoType()));

		// 暂时不处理 Stack超出上限
		//if (m_UndoStack.Count >= GlobalData.UNDO_MAX_COUNT)
		//{
		//	Stack<IUndoRedo> undoCopyStack = new Stack<IUndoRedo>();
		//	for(int undoIdx = 0; undoIdx < GlobalData.UNDO_SAVE_COUNT; undoIdx++)
		//	{
		//		undoCopyStack.Push(m_UndoStack.Pop());
		//	}

		//	m_UndoStack.Clear();
		//	while(undoCopyStack.Count > 0)
		//	{
		//		m_UndoStack.Push(undoCopyStack.Pop());
		//	}
		//}
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
			DataGridViewUtility.InsertNewRow(dataGridView, dataTable, Row);
        }

        public void Undo(DataGridView dataGridView, DataTable dataTable)
        {
            dataGridView.Rows.RemoveAt(Row);
        }
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

	public class DoManyThingsEvent : IUndoRedo
	{
		public List<IUndoRedo> ThingsList;

		public DoType GetDoType()
		{
			return DoType.ManyThings;
		}

		public void Redo(DataGridView dataGridView, DataTable dataTable)
		{
			for(int thingIdx = 0; thingIdx < ThingsList.Count; thingIdx++)
			{
				IUndoRedo thing = ThingsList[thingIdx];
				thing.Redo(dataGridView, dataTable);
			}
		}

		public void Undo(DataGridView dataGridView, DataTable dataTable)
		{
			for (int thingIdx = ThingsList.Count - 1; thingIdx >= 0; thingIdx--)
			{
				IUndoRedo thing = ThingsList[thingIdx];
				thing.Undo(dataGridView, dataTable);
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

	public struct CellValueChangeItem
	{
		public int Column;
		public int Row;
		public string OldValue;
		public string NewValue;
	}

	public enum DoEventType
    {
        Did,
        Redo,
        Undo
    }

    public enum DoType
    {
        AddRow,
        CellsValueChange,
		ManyThings
    }
	#endregion // End Undo\Redo
}