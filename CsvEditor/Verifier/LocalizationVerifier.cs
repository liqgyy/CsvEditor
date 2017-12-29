﻿using System;
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

			// 魔法数字：Key不需要检测。 参考列, 国内去敏感词列跟中文列重复太多不检测
			int[][] repeats = VerifierUtility.VerifyRepeatCellInRow(dataRow, new int[] { 0, 1, 12, 14 });
			if (repeats != null)
			{
				messageList.Add(VerifierUtility.CreateRepeatCellInRowMessage(DataGridViewConsoleForm.Level.Info, rowIdx, repeats));
			}

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