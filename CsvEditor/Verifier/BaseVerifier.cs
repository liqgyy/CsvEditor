using System.Collections.Generic;
using System.Windows.Forms;

public abstract class BaseVerifier
{
	public abstract string GetDisplayName();

	public abstract bool Verify(DataGridView dataGridView, out List<DataGridViewConsoleForm.Message> messageList);
}