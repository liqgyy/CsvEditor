using System;

[Serializable]
public class CsvLayout
{
	public string Path;
	public int[] ColumnWidths;
	public int[] RowHeights;
	/// <summary>
	/// 第一列(行号)的宽度
	/// 小于0时不设置
	/// </summary>
	public int RowHeadersWidth = -1;

	/// <summary>
	/// 小于0时不冻结
	/// </summary>
	public int FrozenColumn = -1;
	/// <summary>
	/// 小于0时不冻结
	/// </summary>
	public int FrozenRow = -1;

	public CsvLayout(string path)
	{
		Path = path;
	}
}