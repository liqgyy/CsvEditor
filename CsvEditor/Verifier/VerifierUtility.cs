using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public class VerifierUtility
{
	private static string[] ms_VerifyMessages;

	public static string GetVerifierDisplayName(string verifierName)
	{
		BaseVerifier verifier = GetVerifierWithName(verifierName);
		return GetVerifierDisplayName(verifier.GetType());
	}

	public static string GetVerifierDisplayName(Type type)
	{
		if (type == typeof(LocalizationVerifier))
		{
			return "本地化";
		}
		else
		{
			return "默认";
		}
	}

	public static BaseVerifier GetVerifierWithName(string verifier)
	{
		switch(verifier)
		{
			case DefaultVerifier.NAME:
				return new DefaultVerifier();
			case LocalizationVerifier.NAME:
				return new LocalizationVerifier();
			default:
				return new DefaultVerifier();
		}
	}

	public static DataGridViewConsoleForm.Level VerifyWithVerifier(string verifierName, DataGridView dataGridView, out List<DataGridViewConsoleForm.Message> messageList)
	{
		BaseVerifier verifier = GetVerifierWithName(verifierName);
		return verifier.Verify(dataGridView, out messageList);
	}

	public static string GetVerifyMessage(VerifyType verifyType)
	{
		if (ms_VerifyMessages == null)
		{
			ms_VerifyMessages = new string[(int)VerifyType.End];

			ms_VerifyMessages[(int)VerifyType.TabOrLineBreak] = string.Format("包含非法字符(\"\\t\", \"\\r\\n\")\n请在保存前运行(移除所有制表符并转换所有换行符)工具");
			ms_VerifyMessages[(int)VerifyType.HeadAndTailWhiteSpace] = string.Format("头尾有空白字符");
			ms_VerifyMessages[(int)VerifyType.RepeatCellInRow] = string.Format("行内有内容重复的单元格");

			for (int iMessage = 0; iMessage < ms_VerifyMessages.Length; iMessage++)
			{
				if (string.IsNullOrEmpty(ms_VerifyMessages[iMessage]))
				{
					MessageBox.Show(string.Format("是不是添加了新校验规则({0})后没填写校验提示消息", (VerifyType)iMessage), "提示");
				}
			}
		}
		return ms_VerifyMessages[(int)verifyType];
	}

	#region TabOrLineBreak
	public static bool VerifyTabOrLineBreak(string value)
	{
		if (value.Contains('\t') || value.Contains("\r\n"))
		{
			return false;
		}
		return true;
	}

	public static DataGridViewConsoleForm.Message CreateTabOrLineBreakMessage(DataGridViewConsoleForm.Level level, int rowIdx, int colIdx, string cellValue)
	{
		DataGridViewConsoleForm.Message message = new DataGridViewConsoleForm.Message();
		message.Level = level;
		message.Row = rowIdx;
		message.Column = colIdx;
		message.Caption = GetVerifyMessage(VerifyType.TabOrLineBreak);
		message.Text = string.Format("({0})", cellValue);
		return message;
	}
	#endregion

	#region HeadAndTailWhiteSpace
	public static bool VerifyHeadAndTailWhiteSpace(string value)
	{
		return value.Trim() == value;
	}

	public static DataGridViewConsoleForm.Message CreateHeadAndTailWhiteSpaceMessage(DataGridViewConsoleForm.Level level, int rowIdx, int colIdx, string cellValue)
	{
		DataGridViewConsoleForm.Message message = new DataGridViewConsoleForm.Message();
		message.Level = level;
		message.Row = rowIdx;
		message.Column = colIdx;
		message.Caption = GetVerifyMessage(VerifyType.HeadAndTailWhiteSpace);
		message.Text = string.Format("({0})", cellValue);
		return message;
	}
	#endregion

	#region RepeatCellInRow
	public static int[][] VerifyRepeatCellInRow(DataGridViewRow row, int[] exclueds)
	{
		string[] strs = new string[row.Cells.Count];
		for (int iCell = 0; iCell < strs.Length; iCell++)
		{
			strs[iCell] = (string)row.Cells[iCell].Value;
		}
		return StringUtility.CheckRepeat(strs, exclueds);
	}

	public static DataGridViewConsoleForm.Message CreateRepeatCellInRowMessage(DataGridViewConsoleForm.Level level, int rowIdx, int[][] repeats)
	{
		DataGridViewConsoleForm.Message message = new DataGridViewConsoleForm.Message();
		message.Level = level;
		message.Row = rowIdx;
		message.Column = -1;
		message.Caption = GetVerifyMessage(VerifyType.RepeatCellInRow);

		StringBuilder textSb = new StringBuilder("重复的列");
		for (int iRepeats = 0; iRepeats < repeats.Length; iRepeats++)
		{
			textSb.AppendLine();
			int[] repeat = repeats[iRepeats];
			for (int iRepeat = 0; iRepeat < repeat.Length; iRepeat++)
			{
				textSb.Append(ConvertUtility.NumberToLetter(repeat[iRepeat] + 1));
				if (iRepeat < repeat.Length - 1)
				{
					textSb.Append(", ");
				}
			}
		}
		message.Text = textSb.ToString();
		return message;
	}
	#endregion

	public enum VerifyType
	{
		TabOrLineBreak = 0,
		HeadAndTailWhiteSpace,
		RepeatCellInRow,
		End
	}
}