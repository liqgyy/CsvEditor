using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// TODO 优化内存
/// 不要每次Load都读取整个文件
/// </summary>
public class CsvLayoutManager
{
	private static CsvLayoutManager ms_Instance;
	public static CsvLayoutManager Instance
	{
		get
		{
			if (ms_Instance == null)
			{
				ms_Instance = new CsvLayoutManager();
			}
			return ms_Instance;
		}
	}

	private string m_SavePath;
	private string m_SpecificSavePath;

	private List<CsvLayout> m_LayoutList;
	private List<CsvLayout> m_SpecificLayoutList;

	public CsvLayout Load(string key)
	{
		return Load(m_LayoutList , key);
	}

	public void Save()
	{
		Save(m_SavePath, m_LayoutList);
	}

	public CsvLayout LoadSpecific(string key)
	{
		return Load(m_SpecificLayoutList, key);
	}

	public void SaveSpecific()
	{
		Save(m_SpecificSavePath, m_SpecificLayoutList);
	}

	public string[] GetSpecificKeys()
	{
		string[] keys = new string[m_SpecificLayoutList.Count];
		for(int layoutIdx = 0; layoutIdx < m_SpecificLayoutList.Count; layoutIdx++)
		{
			keys[layoutIdx] = m_SpecificLayoutList[layoutIdx].Key;
		}
		return keys;
	}

	private CsvLayoutManager()
	{
		m_SavePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + GlobalData.CSVLAYOUT_FILE_NAME;
		m_SpecificSavePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + GlobalData.SPECIFIC_CSVLAYOUT_FILE_NAME;

		m_LayoutList = LoadList(m_SavePath);
		m_SpecificLayoutList = LoadList(m_SpecificSavePath);
	}

	private List<CsvLayout> LoadList(string path)
	{
		if (string.IsNullOrEmpty(path) || !File.Exists(path))
		{
			return new List<CsvLayout>();
		}

		try
		{
			return (List<CsvLayout>)SerializeUtility.ReadFile(path);
		}
		catch (Exception ex)
		{
			Debug.ShowExceptionMessageBox("加载CsvLayout失败\n" + path, ex);
			return new List<CsvLayout>();
		}
	}

	private CsvLayout Load(List<CsvLayout> list, string key)
	{
		for (int layoutIdx = 0; layoutIdx < list.Count; layoutIdx++)
		{
			if (list[layoutIdx].Key == key)
			{
				return list[layoutIdx];
			}
		}

		CsvLayout newCsvLayout = new CsvLayout(key);
		list.Add(newCsvLayout);
		return newCsvLayout;
	}

	/// <summary>
	/// TODO 不要每次都保存到文件, 只在进程关闭的时候保存
	/// 现在考虑到多个进程之间可能会冲突,暂时这样处理
	/// </summary>
	private void Save(string path, List<CsvLayout> list)
	{
		try
		{
			SerializeUtility.WriteFile(path, list);
		}
		catch (Exception ex)
		{
			Debug.ShowExceptionMessageBox("保存CsvLayout失败\n" + path, ex);
		}
	}
}