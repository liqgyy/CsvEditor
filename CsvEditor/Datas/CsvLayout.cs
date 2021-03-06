﻿using System;

[Serializable]
public class CsvLayout
{
	public string Key;
	public int[] ColumnWidths;

	/// <summary>
	/// 小于0时不冻结
	/// </summary>
	public int FrozenColumn = -1;
	/// <summary>
	/// 小于0时不冻结
	/// </summary>
	public int FrozenRow = -1;

	public string Verifier = "Default";

	public CsvLayout(string key)
	{
		Key = key;
	}
}