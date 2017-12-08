using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// 代码比较工具
/// 使用 https://www.devart.com/codecompare 软件的cmd命令比较两个文件
/// </summary>
public class CodeCompare
{
	public static CodeCompare Instance
	{
		get
		{
			if (m_Instance == null)
			{
				m_Instance = new CodeCompare();
			}
			return m_Instance;
		}
	}

	private static CodeCompare m_Instance;

	public bool Initialized = false;

	public CodeCompare()
	{
		Initialized = Initialize();
	}

	private bool Initialize()
	{
		// CodeCompare注册表里只有帮助文件的地址, 利用帮助文件的地址寻找CodeCompare.exe的地址
		RegistryKey software = null;
		try
		{
			software = Registry.LocalMachine.CreateSubKey(GlobalData.RegistryKeyCodeCompare, true);
			string ccHelpFile = (string)software.GetValue("HelpFile");
		}
		catch (Exception ex)
		{
			Debug.ShowExceptionMessageBox("代码比较工具初始化失败,可能未正确安装CodeCompare", ex);
			System.Diagnostics.Process.Start("https://www.devart.com/codecompare");
		}
		finally
		{
			RegistryUtility.CloseRegistryKey(software);
		}
		return true;
	}
}