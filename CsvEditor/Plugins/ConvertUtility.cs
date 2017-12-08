using System.Text;

public class ConvertUtility
{
	/// <summary>
	/// 数字转A~Z字符串 如0 => A , 25 => Z
	/// TODO 转多位字符 如 26 => AA , 27 => AB
	/// </summary>
	/// <param name="number">数字 从0开始</param>
	/// <returns>转成的字符串 如:ABCD</returns>
	public static string NumberToAZString(int number)
	{
		string result = "";
		result = ASCIIToChar(number + 65);
		return result;
	}

	public static string ASCIIToChar(int ascii)
	{
		ASCIIEncoding asciiEncoding = new ASCIIEncoding();
		byte[] btNumber = new byte[] { (byte)ascii };
		return asciiEncoding.GetString(btNumber);
	}
}
