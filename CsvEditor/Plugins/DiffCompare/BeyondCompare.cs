using Microsoft.Win32;
using System.Windows.Forms;

/// <summary>
/// 比较工具
/// 使用 http://www.beyondcompare.cc/ 软件的cmd命令比较两个文件
/// </summary>
public class BeyondCompare : BaseDiffCompare
{
	public static BeyondCompare Instance
	{
		get
		{
			if (ms_Instance == null)
			{
				ms_Instance = new BeyondCompare();
			}
			return ms_Instance;
		}
	}
	private static BeyondCompare ms_Instance;

	private const string BEYOND_COMPARE_URL = "http://www.beyondcompare.cc/";

	public override bool Initialized()
	{
		return !string.IsNullOrEmpty(Setting.Instance.BeyondCompareExePath);
	}

	public override void AutoExePathToSetting()
	{
		if (!Setting.Instance.BeyondCompareAutoExePath)
		{
			return;
		}
		string exePath = (string)RegistryUtility.GetBaseSubKeyValue(RegistryHive.LocalMachine, RegistryView.Registry64, GlobalData.REGISTRY_KEY_BEYONDCOMPARE, "ExePath");
		if (string.IsNullOrEmpty(exePath))
		{
			exePath = "";
			if (Setting.Instance.FirstRun && MessageBox.Show("代码比较工具初始化失败,可能未正确安装BeyondCompare.\n是否打开官网下载工具?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				System.Diagnostics.Process.Start(BEYOND_COMPARE_URL);
			}
		}
		Setting.Instance.BeyondCompareExePath = exePath;
	}

	public override string GetCmdProcessLine(string fileFullName1,
		string fileFullName2,
		string fileTitle1,
		string fileTitle2)
	{
		return string.Format("\"{0}\" /fv=\"Table Compare\" \"{1}\" \"{2}\" & exit",
				Setting.Instance.BeyondCompareExePath, fileFullName1, fileFullName2);
	}
}