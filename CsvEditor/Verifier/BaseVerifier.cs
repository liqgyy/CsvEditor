using System.Collections.Generic;
using System.Windows.Forms;

public abstract class BaseVerifier
{
	/// <summary>
	/// 校验DataGridView中的数据
	/// </summary>
	/// <param name="dataGridView"></param>
	/// <param name="messageList">要输出到控制台的信息（包含但不限于错误、警告）</param>
	/// <returns>数据错误等级</returns>
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
			return DataGridViewConsoleForm.Level.Info;
		}
	}

	public abstract void Verify(DataGridView dataGridView, ref List<DataGridViewConsoleForm.Message> messageList, ref bool hasError, ref bool hasWarning);
}