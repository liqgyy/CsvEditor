using System.Collections.Generic;
using System.Windows.Forms;

public abstract class BaseVerifier
{
	public abstract DataGridViewConsoleForm.Level Verify(DataGridView dataGridView, out List<DataGridViewConsoleForm.Message> messageList);
}