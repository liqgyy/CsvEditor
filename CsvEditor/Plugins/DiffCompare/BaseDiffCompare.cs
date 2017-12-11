using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BaseDiffCompare
{
	public virtual void AutoExePathToSetting() { }

	/// <summary>
	/// 打开软件比较两个文件
	/// </summary>
	/// <param name="oldFilePath">旧文件</param>
	/// <param name="newFilePath">新文件</param>
	/// <param name="needCopy">为true时，比较文件副本</param>
	/// <returns>是否成功打开</returns>
	public bool Compare(string fileFullName1,
		string fileFullName2,
		string fileTitle1,
		string fileTitle2,
		bool needCopy = false)
	{
		if (!Initialized())
			return false;

		if (needCopy)
		{
			try
			{
				string copyFileFullName = Path.GetTempPath() + "CsvEditorCompare1";
				File.Copy(fileFullName1, copyFileFullName);
				fileFullName1 = copyFileFullName;

				copyFileFullName = Path.GetTempPath() + "CsvEditorCompare2";
				File.Copy(fileFullName2, copyFileFullName);
				fileFullName2 = copyFileFullName;
			}
			catch (Exception ex)
			{
				Debug.ShowExceptionMessageBox("拷贝副本: " + fileFullName1 + " - " + fileFullName2 + " 失败", ex);
				return false;
			}
		}

		System.Diagnostics.Process cmdProcess = null;
		try
		{
			cmdProcess = new System.Diagnostics.Process();
			cmdProcess.StartInfo.FileName = "cmd.exe";
			// 是否使用操作系统shell启动
			cmdProcess.StartInfo.UseShellExecute = false;
			// 接受来自调用程序的输入信息
			cmdProcess.StartInfo.RedirectStandardInput = true;
			// 由调用程序获取输出信息
			cmdProcess.StartInfo.RedirectStandardOutput = true;
			// 重定向标准错误输出
			cmdProcess.StartInfo.RedirectStandardError = true;
			// 不显示程序窗口
			cmdProcess.StartInfo.CreateNoWindow = true;
			cmdProcess.Start();

			// 向cmd窗口发送输入信息
			// 向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
			// 同类的符号还有&&和||前者表示必须前一个命令执行成功才会执行后面的命令，后者表示必须前一个命令执行失败才会执行后面的命令
			cmdProcess.StandardInput.WriteLine(GetCmdProcessLine(fileFullName1, fileFullName2, fileTitle1, fileTitle2));

			// UNDONE 暂时不需要接受数据
			//Console.WriteLine(cmdProcess.StandardOutput.ReadToEnd());

			// 等待程序执行完退出进程
			cmdProcess.WaitForExit();
			cmdProcess.Close();
		}
		catch (Exception ex)
		{
			Debug.ShowExceptionMessageBox("比较文件: " + fileFullName1 + " - " + fileFullName2 + " 失败", ex);
			return false;
		}
		finally
		{
			if (cmdProcess != null)
				cmdProcess.Close();
		}

		return true;
	}

	public virtual bool Initialized()
	{
		return false;
	}

	public virtual string GetCmdProcessLine(string fileFullName1,
		string fileFullName2,
		string fileTitle1,
		string fileTitle2)
	{
		return "";
	}
}

// TODD 利用GetChanges 做简单的版本比较工具
//DataTable cdt = m_MainDataTable.GetChanges();
//if (cdt != null)
//{
//    for (int i = 0; i < cdt.Rows.Count; i++)
//    {
//        if (cdt.Rows[i].RowState == DataRowState.Deleted)
//        {
//            Console.WriteLine("删除的行索引{0}，原来数值是{1}", i, cdt.Rows[i][0, DataRowVersion.Original]);
//        }
//        else if (cdt.Rows[i].RowState == DataRowState.Modified)
//        {
//            Console.WriteLine("修改的行索引{0}，原来数值是{1}，现在的新数值{2}", i, cdt.Rows[i][0, DataRowVersion.Original], cdt.Rows[i][0, DataRowVersion.Current]);
//        }
//        else if (cdt.Rows[i].RowState == DataRowState.Added)
//        {
//            Console.WriteLine("新添加行索引{0}，数值是{1}", i, cdt.Rows[i][0, DataRowVersion.Current]);
//        }
//    }
//}