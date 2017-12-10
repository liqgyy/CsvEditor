using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

public class CsvEdit
{
    private CsvForm m_CsvForm;

    public CsvEdit(CsvForm csvForm)
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
    }

    public void Cut()
    {
        if (!CanCut())
        {
            return;
        }
    }

    public void Paste()
    {
        if (!CanPaste())
        {
            return;
        }
    }

    public bool CanCopy()
    {
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