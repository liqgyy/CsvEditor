using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

public class CsvReader
{
	public static DataTable Read(string[] lines)
	{	
		DataTable csvData = new DataTable();

		int colCount = 0;
		List<string[]> rowsList = new List<string[]>();
		for (int rowIdx = 0; rowIdx < lines.Length; rowIdx++)
		{
			string[] cols = ReadLine(lines[rowIdx]);
			rowsList.Add(cols);
			colCount = cols.Length > colCount ? cols.Length : colCount;
		}

		for (int colIdx = 0; colIdx < colCount; colIdx++)
		{
			csvData.Columns.Add(colIdx.ToString(), typeof(string));
		}

		for (int rowIdx = 0; rowIdx < rowsList.Count; rowIdx++)
		{
			string[] cols = rowsList[rowIdx];
			DataRow newDataRow = csvData.NewRow();
			for (int colIdx = 0; colIdx < cols.Length; colIdx++)
			{
				newDataRow[colIdx] = cols[colIdx].Replace("\"\"", "\"");
			}
			csvData.Rows.Add(newDataRow);
		}
		return csvData;
	}

    public static string[] ReadLine(string line)
	{
		IEnumerable<string> result = from Match m in Regex.Matches(line,
			@"(((?<x>(?=[,\r\n]+))|""(?<x>([^""]|"""")+)""|(?<x>[^,\r\n]+)),?)",
			RegexOptions.ExplicitCapture )
				select m.Groups [ 1 ].Value;
		return result.ToArray();
    }
}