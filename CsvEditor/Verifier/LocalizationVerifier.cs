using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class LocalizationVerifier : BaseVerifier
{
	public const string NAME = "Localization";

	public override void Verify(DataGridView dataGridView, ref List<DataGridViewConsoleForm.Message> messageList, ref bool hasError, ref bool hasWarning)
	{
		for (int rowIdx = 0; rowIdx < dataGridView.Rows.Count; rowIdx++)
		{
			DataGridViewRow dataRow = dataGridView.Rows[rowIdx];
			for (int colIdx = 0; colIdx < dataGridView.Columns.Count; colIdx++)
			{
				string value = (string)dataRow.Cells[colIdx].Value;

				if (!VerifierUtility.VerifyTabOrLineBreak(value))
				{
					hasError = true;
					messageList.Add(VerifierUtility.CreateTabOrLineBreakMessage(DataGridViewConsoleForm.Level.Error, rowIdx, colIdx, value));
				}

				if (!VerifierUtility.VerifyHeadAndTailWhiteSpace(value))
				{
					hasWarning = true;
					messageList.Add(VerifierUtility.CreateHeadAndTailWhiteSpaceMessage(DataGridViewConsoleForm.Level.Warning, rowIdx, colIdx, value));
				}
			}
		}
	}
}