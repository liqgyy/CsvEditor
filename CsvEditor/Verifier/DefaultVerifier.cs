using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

class DefaultVerifier : BaseVerifier
{
	public override DataGridViewConsoleForm.Level Verify(DataGridView dataGridView, out List<DataGridViewConsoleForm.Message> messageList)
	{
		messageList = new List<DataGridViewConsoleForm.Message>();
		bool hasError = false;
		bool hasWarning = false;

		for (int rowIdx = 0; rowIdx < dataGridView.Rows.Count; rowIdx++)
		{
			DataGridViewRow dataRow = dataGridView.Rows[rowIdx];
			for (int colIdx = 0; colIdx < dataGridView.Columns.Count; colIdx++)
			{
				string value = (string)dataRow.Cells[colIdx].Value;

				if (!VerifierUtility.VerifyTabOrLineBreak(value))
				{
					hasError = true;

					DataGridViewConsoleForm.Message message = new DataGridViewConsoleForm.Message();
					message.Level = DataGridViewConsoleForm.Level.Error;
					message.Row = rowIdx;
					message.Column = colIdx;
					message.Caption = VerifierUtility.GetVerifyMessage(VerifierUtility.VerifyType.TabOrLineBreak);
					message.Text = value;
					messageList.Add(message);
				}
			}
		}

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
}