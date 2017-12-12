﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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
		Clipboard.SetDataObject(m_CsvForm.MainDataGridView.GetClipboardContent());
    }

    public void Cut()
    {
        if (!CanCut())
        {
            return;
        }
		Clipboard.SetDataObject(m_CsvForm.MainDataGridView.GetClipboardContent());

		m_CsvForm.BeforeChangeCellValue();
		List<CellValueChangeItem> changeList = new List<CellValueChangeItem>();
		for(int cellIdx = 0; cellIdx < m_CsvForm.MainDataGridView.SelectedCells.Count; cellIdx++)
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

	public void Paste()
    {
        if (!CanPaste())
        {
            return;
        }
		
		DataObject clipboardData = null;
		string clipboardStr = null;
		// 获取剪切板的数据
		try
		{
			clipboardData = (DataObject)Clipboard.GetDataObject();
			if (!clipboardData.GetDataPresent(DataFormats.Text))
			{
				return;
			}
			clipboardStr = clipboardData.GetData(DataFormats.Text).ToString();
		}
		catch (Exception ex)
		{
			Debug.ShowExceptionMessageBox("获取剪切板数据失败", ex);
			return;
		}
		finally
		{
			clipboardData = null;
		}

		DataGridView dataGridView = m_CsvForm.MainDataGridView;

		// 粘贴剪切板的数据到DataGridViewCell
		if (dataGridView.IsCurrentCellInEditMode)
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
				newValue.Remove(selectionStart, textBox.SelectionLength);
				newValue.Insert(selectionStart, clipboardStr);
				// 在这之后不会抛出异常
				textBox.Text = newValue;

				change.NewValue = newValue;
				DidCellValueChange(change);
			}
			catch (Exception ex)
			{
				textBox.Text = change.OldValue;
				Debug.ShowExceptionMessageBox("粘贴剪切板的数据到DataGridViewCell失败", ex);
			}
			finally
			{
				m_CsvForm.AfterChangeCellValue();
			}
			return;
		}

		// 粘贴剪切板的数据到DataGridView
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
					break;
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
						break;
					}
					currentCell = dataGridView.Rows[currentRow].Cells[currentCol + cellIdx];
					// 当前的设计需求不会出现ReadOnly = true的情况
					if (currentCell.ReadOnly)
					{
						continue;
					}
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
		catch (Exception ex)
		{
			Debug.ShowExceptionMessageBox("粘贴剪切板的数据到DataGridView失败", ex);
			return;
		}
		finally
		{
			DidCellsValueChange(changeList);
			m_CsvForm.AfterChangeCellValue();
		}
	}

	public bool CanCopy()
    {
		//if (m_CsvForm.MainDataGridView.IsCurrentCellInEditMode)
		//{
		//	TextBox textBox = m_CsvForm.MainDataGridView.EditingControl as TextBox;
		//	return !string.IsNullOrEmpty(textBox.SelectedText);
		//}
		//if (m_CsvForm.MainDataGridView.SelectedCells.Count == 0)
		//{
		//	return false;
		//}
        return true;
    }

    public bool CanCut()
    {
        return CanCopy();
    }

    public bool CanPaste()
    {
        return true;
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