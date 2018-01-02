using Microsoft.Win32;
using System.Windows.Forms;

/// <summary>
/// 比较工具
/// 使用 https://www.devart.com/codecompare 软件的cmd命令比较两个文件
/// </summary>
public class CodeCompare : BaseDiffCompare
{
	public static CodeCompare Instance {
		get
		{
			if (ms_Instance == null)
			{
				ms_Instance = new CodeCompare();
			}
			return ms_Instance;
		}
	}
	private static CodeCompare ms_Instance;

	private const string CODE_COMPARE_URL = "https://www.devart.com/codecompare";

	public override bool Initialized()
	{
		return !string.IsNullOrEmpty(Setting.Instance.CodeCompareExePath);
	}

	public override void AutoExePathToSetting()
	{
		if (!Setting.Instance.CodeCompareAutoExePath)
		{
			return;
		}
		string exePath = (string)RegistryUtility.GetBaseSubKeyValue(RegistryHive.LocalMachine, RegistryView.Registry64, GlobalData.REGISTRY_KEY_CODECOMPARE, "HelpFile");
		if (string.IsNullOrEmpty(exePath))
		{
			exePath = "";
			if (Setting.Instance.FirstRun && MessageBox.Show("代码比较工具初始化失败,可能未正确安装CodeCompare.\n是否打开官网下载工具?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				System.Diagnostics.Process.Start(CODE_COMPARE_URL);
			}
		}
		else
		{
			exePath = exePath.Replace("CodeCompare.chm", "CodeCompare.exe");
		}
		Setting.Instance.CodeCompareExePath = exePath;
	}

	public override string GetCmdProcessLine(string fileFullName1,
		string fileFullName2,
		string fileTitle1,
		string fileTitle2)
	{
		return string.Format("\"{0}\" /T1=\"{1}\" /T2=\"{2}\" \"{3}\" \"{4}\" & exit",
				Setting.Instance.CodeCompareExePath, fileTitle1, fileTitle2, fileFullName1, fileFullName2);
	}
}