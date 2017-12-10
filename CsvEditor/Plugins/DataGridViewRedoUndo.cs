using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

/// <summary>
/// TODO 类名不合适
/// </summary>
public class DataGridViewRedoUndo
{
    public delegate void DoSomethingEventHandler(object sender, DoSomethingEventArgs e);
    public event DoSomethingEventHandler DoSomething;

    private Stack<IUndoRedo> m_UndoStack;
    private Stack<IUndoRedo> m_RedoStack;

    private DataGridView m_DataGridView;
    private DataTable m_DataTable;

    public DataGridViewRedoUndo(DataGridView dataGridView)
    {
        m_UndoStack = new Stack<IUndoRedo>();
        m_RedoStack = new Stack<IUndoRedo>();

        m_DataGridView = dataGridView;
    }

    public void SetDataTable(DataTable dataTable)
    {
        m_DataTable = dataTable;
    }

    public void DoAddRow(int row)
    {
        m_UndoStack.Push(new DoAddRowEvent { Row = row });
    }

    public void DoCellValueChange(int column, int row, string oldValue, string newValue)
    {
        m_UndoStack.Push(new DoCellValueChangeEvent { Column = column, Row = row, OldValue = oldValue, NewValue = newValue });
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
        IUndoRedo iur = m_UndoStack.Pop();
        iur.Undo(m_DataGridView, m_DataTable);
        m_RedoStack.Push(iur);
        DoSomething(this, new DoSomethingEventArgs(EventType.Undo, iur.GetDoType()));
    }

    public void Redo()
    {
        if (!CanRedo())
        {
            return;
        }
        IUndoRedo iur = m_RedoStack.Pop();
        iur.Redo(m_DataGridView, m_DataTable);
        m_UndoStack.Push(iur);
        DoSomething(this, new DoSomethingEventArgs(EventType.Redo, iur.GetDoType()));
    }

    private void Do(IUndoRedo iur)
    {
        m_UndoStack.Push(iur);
        m_RedoStack.Peek();
        DoSomething(this, new DoSomethingEventArgs(EventType.Do, iur.GetDoType()));
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
            //DataRow newRowData = m_MainDataTable.NewRow();
            //m_MainDataTable.Rows.InsertAt(newRowData, index);

            //m_DataGridView.ClearSelection();
            //m_DataGridView.Rows[index].Selected = true;
            //m_CopyDataTable = m_MainDataTable.Copy();
            //RedoUndo.DoAddRow(index);
        }
    }

    public class DoCellValueChangeEvent : IUndoRedo
    {
        public int Column;
        public int Row;
        public string OldValue;
        public string NewValue;

        public DoType GetDoType()
        {
            return DoType.CellValueChange;
        }

        public void Redo(DataGridView dataGridView, DataTable dataTable)
        {
            dataGridView.Rows[Row].Cells[Column].Value = NewValue;
        }

        public void Undo(DataGridView dataGridView, DataTable dataTable)
        {
            dataGridView.Rows[Row].Cells[Column].Value = OldValue;
        }
    }

    public class DoSomethingEventArgs : EventArgs
    {
        public EventType MyEventType;
        public DoType MyDoType;

        public DoSomethingEventArgs(EventType eventType, DoType doType)
        {
            MyEventType = eventType;
            MyDoType = doType;
        }
    }

    public enum EventType
    {
        Do,
        Redo,
        Undo
    }

    public enum DoType
    {
        AddRow,
        CellValueChange
    }
}