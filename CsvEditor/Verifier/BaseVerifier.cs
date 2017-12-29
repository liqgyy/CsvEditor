using System.Collections.Generic;
using System.Windows.Forms;

public abstract class BaseVerifier
{
	public abstract bool Verify(DataGridView dataGridView, out List<DataGridViewConsoleForm.Message> messageList);
}