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

	public CsvLayout LoadOrCreate(string key)
	{
		return LoadOrCreate(m_LayoutList, key);
	}

	public void Save()
	{
		Save(m_SavePath, m_LayoutList);
	}

	public void Replace(CsvLayout oldLayout, CsvLayout newLayout)
	{
		CsvLayout newLayoutCopy = SerializeUtility.ObjectCopy(newLayout);
		newLayoutCopy.Key = oldLayout.Key;
		for (int layoutIdx = 0; layoutIdx < m_LayoutList.Count; layoutIdx++)
		{
			if (oldLayout == m_LayoutList[layoutIdx])
			{
				m_LayoutList[layoutIdx] = newLayoutCopy;
				return;
			}
		}
	}

	#region Specific
	public CsvLayout LoadOrCreateSpecific(string key)
	{
		return LoadOrCreate(m_SpecificLayoutList, key);
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

	public bool ExistSpecific(string key)
	{
		if (string.IsNullOrEmpty(key))
		{
			return false;
		}

		for (int layoutIdx = 0; layoutIdx < m_SpecificLayoutList.Count; layoutIdx++)
		{
			if (key == m_SpecificLayoutList[layoutIdx].Key)
			{
				return true;
			}
		}
		return false;
	}

	public void AddSpecific(CsvLayout layout)
	{
		for (int layoutIdx = 0; layoutIdx < m_SpecificLayoutList.Count; layoutIdx++)
		{
			if (layout.Key == m_SpecificLayoutList[layoutIdx].Key)
			{
				m_SpecificLayoutList[layoutIdx] = layout;
				return;
			}
		}
		m_SpecificLayoutList.Add(layout);
	}

	public void SwapSpecific(int index1, int index2)
	{
		if (index1 < 0 || index2 < 0 || index1 > m_SpecificLayoutList.Count - 1 || index2 > m_SpecificLayoutList.Count - 1)
		{
			return;
		}

		CsvLayout layout1 = m_SpecificLayoutList[index1];
		CsvLayout layout2 = m_SpecificLayoutList[index2];
		m_SpecificLayoutList[index1] = layout2;
		m_SpecificLayoutList[index2] = layout1;
	}
	
	public void DeleteSpecific(int index)
	{
		if (index < 0 || index > m_SpecificLayoutList.Count - 1)
		{
			return;
		}

		m_SpecificLayoutList.RemoveAt(index);
	}
	#endregion // End Specific

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

	private CsvLayout LoadOrCreate(List<CsvLayout> list, string key)
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