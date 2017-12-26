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
	/// <param name="text">内容</param>
	/// <param name="ex">异常</param>
	public static DialogResult ShowExceptionMessageBox(string text, Exception ex)
	{
        return ShowExceptionMessageBox(text, ex, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    /// <summary>
	/// 显示异常消息的MessageBox
	/// %%TODO Debug Release 有不同的处理
	/// </summary>
	/// <param name="text">内容</param>
	/// <param name="ex">异常</param>
    public static DialogResult ShowExceptionMessageBox(string text, Exception ex, MessageBoxButtons buttons)
    {
        return ShowExceptionMessageBox(text, ex, buttons, MessageBoxIcon.Error);
    }

    /// <summary>
	/// 显示异常消息的MessageBox
	/// %%TODO Debug Release 有不同的处理
	/// </summary>
	/// <param name="text">内容</param>
	/// <param name="ex">异常</param>
    public static DialogResult ShowExceptionMessageBox(string text, Exception ex, MessageBoxButtons buttons, MessageBoxIcon icon)
    {
		string caption = "";
#if DEBUG
		caption = string.Format("{0}\n\n{1}", text , ex.ToString());
		Console.WriteLine(caption);
#else
		caption = string.Format("{0}\n\n{1}", text , ex.Message);
#endif
		return MessageBox.Show(caption, "Exception!", buttons, icon);
    }
}