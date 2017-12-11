using System;
using System.IO;

public class CsvSettingManager
{
	/// <summary>
	/// csv文件保存的目录 结尾是\
	/// </summary>
	private static string ms_SavePath;

	public static CsvSetting LoadSetting(string fullName)
	{
		LoadOrCreateSavePath();

		string settingFileFullName = ms_SavePath + GetCsvSettingFileName(fullName);
		// 保存路径加载失败或文件不存在
		if (string.IsNullOrEmpty(ms_SavePath) || !File.Exists(settingFileFullName))
		{
			return new CsvSetting(fullName);
		}

		try
		{
			return (CsvSetting)SerializeUtility.ReadFile(settingFileFullName);
		}
		catch (Exception ex)
		{
			Debug.ShowExceptionMessageBox("加载CsvSetting失败\n" + settingFileFullName, ex);
			return new CsvSetting(fullName);
		}
	}

	public static void SaveSetting(CsvSetting setting)
	{
		// 保存路径加载失败
		if (string.IsNullOrEmpty(ms_SavePath))
		{
			return;
		}

		string settingFileFullName = ms_SavePath + GetCsvSettingFileName(setting.FileFullName);
		try
		{
			SerializeUtility.WriteFile(settingFileFullName, setting);
		}
		catch (Exception ex)
		{
			Debug.ShowExceptionMessageBox("保存CsvSetting失败\n" + settingFileFullName, ex);
		}
	}

	private static string GetCsvSettingFileName(string fullName)
	{
		return ConvertUtility.StringToMD5(fullName) + ".bin";
	}

	private static void LoadOrCreateSavePath()
	{
		if (ms_SavePath != null)
		{
			return;
		}
		try
		{
			ms_SavePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + GlobalData.CSVSETTING_SAVE_FOLDER + "\\";
			if (!Directory.Exists(ms_SavePath))
			{
				Directory.CreateDirectory(ms_SavePath);
			}
		}
		catch (Exception ex)
		{
			Debug.ShowExceptionMessageBox("加载CsvSetting文件保存路径失败", ex);
			ms_SavePath = "";
		}
	}
}