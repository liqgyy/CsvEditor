﻿using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

public class ConvertUtility
{
    /// <summary>
    /// Number(1~x) TO Letter(A~Z)
    /// 如1 => A , 26 => Z , 27 => AA , 28 => AB
    /// </summary>
    /// <returns>转成的字符串 如:ABCD</returns>
    public static string NumberToLetter(int number)
	{
        string letter = "";
        
        while(number > 26)
        {
            int num = (int)((double)number / 26.0);
            number = number - num * 26;
            letter += ASCIIToChar(num + 64);
        }
        letter += ASCIIToChar(number + 64);
        return letter;
    }

    /// <summary>
    /// Letter(a~z A~Z) TO Number(1~x) 如A => 1 , Z => 26 , AA => 27 , AB => 28
    /// 其他非法字符串默认去除 如 4A.-B+3 => 28
    /// </summary>
    /// <returns>数字 1~x</returns>
    public static int LetterToNumber(string letter)
    {
        letter = letter.ToUpper();

        int number = 0;
        int multiple = 1;
        for (int charIdx = letter.Length - 1; charIdx >= 0; charIdx --)
        {
            char c = letter[charIdx];
            if (!char.IsLetter(c))
            {
                continue;
            }
            number += (((int)c) - 64) * multiple;
            multiple *= 26;
        }
        return number;
    }

    /// <summary>
    /// ASCII 转 Char(string类型 长度1)
    /// TIP string => char => ascii  // int ascii = (int)str[0];
    /// </summary>
    /// <param name="ascii">ASCII</param>
    /// <returns>string类型 长度1</returns>
    public static string ASCIIToChar(int ascii)
	{
		ASCIIEncoding asciiEncoding = new ASCIIEncoding();
		byte[] btNumber = new byte[] { (byte)ascii };
		return asciiEncoding.GetString(btNumber);
	}

    /// <summary>
    /// 通配符转正则
    /// </summary>
    /// <param name="wildcard">通配符</param>
    /// <returns>正则</returns>
    public static Regex WildcardToRegex(string wildcard)
    {
        return new Regex(WildcardToRegexStr(wildcard));
    }

    /// <summary>
    /// 通配符转正则字符串
    /// </summary>
    /// <param name="wildcard">通配符</param>
    /// <returns>正则字符串</returns>
    public static string WildcardToRegexStr(string wildcard)
    {
        return "^" + Regex.Escape(wildcard).Replace(@"\*", ".*").Replace(@"\?", ".") + "$";
    }

	public static string StringToMD5(string str)
	{
		if (string.IsNullOrEmpty(str))
		{
			return "";
		}

		try
		{
			StringBuilder hash = new StringBuilder();
			MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
			byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(str));

			for (int i = 0; i < bytes.Length; i++)
			{
				hash.Append(bytes[i].ToString("x2"));
			}
			return hash.ToString();
		}
		catch (Exception ex)
		{
			Debug.ShowExceptionMessageBox("转Md5失败\n" + str, ex);
			return Guid.NewGuid().ToString();
		}
	}
}
