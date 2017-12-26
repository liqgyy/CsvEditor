using System;
using System.IO;

public class CsvLayoutManager
{
	/// <summary>
	/// csv文件保存的目录 结尾是\
	/// </summary>
	private static string ms_SavePath;

	public static CsvLayout Load(string path)
	{
		LoadOrCreateSavePath();

		string layoutPath = ms_SavePath + GetLayoutFileName(path);
		// 保存路径加载失败或文件不存在
		if (string.IsNullOrEmpty(ms_SavePath) || !File.Exists(layoutPath))
		{
			return new CsvLayout(path);
		}

		try
		{
			return (CsvLayout)SerializeUtility.ReadFile(layoutPath);
		}
		catch (Exception ex)
		{
			Debug.ShowExceptionMessageBox("加载CsvLayout失败\n" + layoutPath, ex);
			return new CsvLayout(path);
		}
	}

	public static void Save(CsvLayout layout)
	{
		// 保存路径加载失败
		if (string.IsNullOrEmpty(ms_SavePath))
		{
			return;
		}

		string layoutPath = ms_SavePath + GetLayoutFileName(layout.Path);
		try
		{
			SerializeUtility.WriteFile(layoutPath, layout);
		}
		catch (Exception ex)
		{
			Debug.ShowExceptionMessageBox("保存CsvLayout失败\n" + layoutPath, ex);
		}
	}

	private static string GetLayoutFileName(string path)
	{
		return ConvertUtility.StringToMD5(path) + ".bin";
	}

	private static void LoadOrCreateSavePath()
	{
		if (ms_SavePath != null)
		{
			return;
		}
		try
		{
			ms_SavePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + GlobalData.CSVLAYOUT_SAVE_FOLDER + "\\";
			if (!Directory.Exists(ms_SavePath))
			{
				Directory.CreateDirectory(ms_SavePath);
			}
		}
		catch (Exception ex)
		{
			Debug.ShowExceptionMessageBox("加载CsvLayout文件保存路径失败", ex);
			ms_SavePath = "";
		}
	}
}