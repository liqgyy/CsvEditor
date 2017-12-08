using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

public class Debug
{
	/// <summary>
	/// 显示异常消息的MessageBox
	/// %%TODO Debug Release 有不同的处理
	/// </summary>
	/// <param name="caption">标题</param>
	/// <param name="ex">异常</param>
	public static void ShowExceptionMessageBox(string caption, Exception ex)
	{
		MessageBox.Show(ex.ToString(), caption);
	}
}