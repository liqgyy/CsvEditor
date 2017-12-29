using System.Collections.Generic;
using System.Windows.Forms;

public abstract class BaseVerifier
{
	public DataGridViewConsoleForm.Level Verify(DataGridView dataGridView, out List<DataGridViewConsoleForm.Message> messageList)
	{
		messageList = new List<DataGridViewConsoleForm.Message>();
		bool hasError = false;
		bool hasWarning = false;

		Verify(dataGridView, ref messageList, ref hasError, ref hasWarning);

		if (hasError)
		{
			return DataGridViewConsoleForm.Level.Error;
		}
		else if (hasWarning)
		{
			return DataGridViewConsoleForm.Level.Warning;
		}
		else
		{
			return DataGridViewConsoleForm.Level.None;
		}
	}

	public abstract void Verify(DataGridView dataGridView, ref List<DataGridViewConsoleForm.Message> messageList, ref bool hasError, ref bool hasWarning);
}