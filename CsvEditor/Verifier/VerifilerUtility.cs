using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class VerifilerUtility
{
	private static string[] ms_VerifyMessages;

	public static bool VerifyWithVerifiler(string verifilerName, DataGridView dataGridView, out List<DataGridViewConsoleForm.Message> messageList)
	{
		BaseVerifier verifiler = GetVerifierWithName(verifilerName);
		return verifiler.Verify(dataGridView, out messageList);
	}

	public static BaseVerifier GetVerifierWithName(string verifiler)
	{
		switch(verifiler)
		{
			default:
				return new DefaultVerifier();
		}
	}

	public static string GetVerifyMessage(VerifyType verifyType)
	{
		if (ms_VerifyMessages == null)
		{
			ms_VerifyMessages = new string[(int)VerifyType.End];

			ms_VerifyMessages[(int)VerifyType.TabOrLineBreak] = string.Format("包含非法字符(\"\\t\", \"\\r\\n\")\n请在保存前运行(移除所有制表符并转换所有换行符)工具");
		}
		return ms_VerifyMessages[(int)verifyType];
	}

	public static bool VerifyTabOrLineBreak(string value)
	{
		if (value.Contains('\t') || value.Contains("\r\n"))
		{
			return false;
		}
		return true;
	}

	public enum VerifyType
	{
		TabOrLineBreak = 0,
		End
	}
}