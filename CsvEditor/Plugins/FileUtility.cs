using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

public class FileUtility
{
	public static bool FilesAreEqual_Hash(string path1, string path2)
	{
		if (string.IsNullOrEmpty(path1) || string.IsNullOrEmpty(path2))
		{
			return false;
		}

		if (!File.Exists(path1) || !File.Exists(path2))
		{
			return false;
		}

		try
		{
			MD5 md5 = MD5.Create();
			using (FileStream fs1 = new FileStream(path1, FileMode.Open, FileAccess.Read))
			using (FileStream fs2 = new FileStream(path2, FileMode.Open, FileAccess.Read))
			{
				byte[] hash1 = md5.ComputeHash(fs1);
				byte[] hash2 = md5.ComputeHash(fs2);

				for (int byteIdx = 0; byteIdx < hash1.Length; byteIdx++)
				{
					if (hash1[byteIdx] != hash2[byteIdx])
					{
						return false;
					}
				}
			}
		}
		catch (Exception ex)
		{
			DebugUtility.ShowExceptionMessageBox(string.Format("对比\n文件:({0})\n文件:({1})\n内容失败", path1, path2), ex);
			return false;
		}

		return true;
	}
}