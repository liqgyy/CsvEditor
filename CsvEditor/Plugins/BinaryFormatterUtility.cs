using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SerializeUtility
{
	private static byte[] ObjectToBuffer(object graph)
	{
		using (MemoryStream ms = new MemoryStream())
		{
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(ms, graph);
			return ms.GetBuffer();
		}
	}

	private static object BufferToObject(byte[] buffer)
	{
		using (MemoryStream ms = new MemoryStream(buffer))
		{
			BinaryFormatter bf = new BinaryFormatter();
			return bf.Deserialize(ms);
		}
	}

	private static string ObjectToBase64String(object graph)
	{
		return Convert.ToBase64String(ObjectToBuffer(graph));
	}

	private static object Base64StringToObject(string base64String)
    {
		return BufferToObject(Convert.FromBase64String(base64String));
    }

    /// <summary>
    /// 写入文件 需要处理异常
    /// </summary>
    /// <param name="fileFullName">文件完整路径</param>
    /// <param name="graph">需要保存的数据</param>
    public static void WriteFile(string fileFullName, object graph)
    {
		try
		{
			using (FileStream fs = new FileStream(fileFullName, FileMode.OpenOrCreate, FileAccess.Write))
			{
				BinaryFormatter bf = new BinaryFormatter();
				bf.Serialize(fs, graph);
			}
		}
		catch (Exception ex)
		{
			throw (ex);
		}
	}

    /// <summary>
    /// 读取文件 需要处理异常
    /// </summary>
    /// <param name="fileFullName">文件完整路径</param>
    /// <returns>读取到的数据</returns>
    public static object ReadFile(string fileFullName)
    {
        try
        {
			using (FileStream fs = new FileStream(fileFullName, FileMode.OpenOrCreate))
			{
				BinaryFormatter bf = new BinaryFormatter();
				return bf.Deserialize(fs);
			}
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }
}
