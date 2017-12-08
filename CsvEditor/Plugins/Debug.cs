﻿using System;
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
	public static void ShowExceptionMessageBox(string text, Exception ex)
	{
		MessageBox.Show(text + "\n" + ex.ToString(), "Throw Exception!", MessageBoxButtons.OK, MessageBoxIcon.Error);
	}
}