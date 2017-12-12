using System;

/// <summary>
/// 每个csv文件对应一个Setting
/// </summary>
[Serializable]
public class CsvSetting
{
	public string FileFullName;
	public int[] ColumnWidths;
	public int[] RowHeights;
	public int RowHeadersWidth = -1;

	/// <summary>
	/// 小于0时不冻结
	/// </summary>
	public int FrozenColumn = -1;
	/// <summary>
	/// 小于0时不冻结
	/// </summary>
	public int FrozenRow = -1;

	public CsvSetting(string fileFullName)
	{
		FileFullName = fileFullName;
	}
}