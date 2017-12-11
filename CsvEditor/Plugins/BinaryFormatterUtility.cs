using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SerializeUtility
{
	private static byte[] ObjectToBuffer(object graph)
	{
		MemoryStream ms = null;
		try
		{
			ms = new MemoryStream();
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(ms, graph);
			return ms.GetBuffer();
		}
		catch (Exception ex)
		{
			throw (ex);
		}
		finally
		{
			if (ms != null)
			{
				ms.Close();
			}
		}
	}

	private static object BufferToObject(byte[] buffer)
	{
		MemoryStream ms = null;
		try
		{
			ms = new MemoryStream(buffer);
			BinaryFormatter bf = new BinaryFormatter();
			return bf.Deserialize(ms);
		}
		catch (Exception ex)
		{
			throw (ex);
		}
		finally
		{
			if (ms != null)
			{
				ms.Close();
			}
		}
	}

	private static string ObjectToBase64String(object graph)
    {
        try
        {
            return Convert.ToBase64String(ObjectToBuffer(graph));
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    private static object Base64StringToObject(string base64String)
    {
        try
        {
            return BufferToObject(Convert.FromBase64String(base64String));
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    /// <summary>
    /// 写入文件 需要处理异常
    /// </summary>
    /// <param name="fileFullName">文件完整路径</param>
    /// <param name="graph">需要保存的数据</param>
    public static void WriteFile(string fileFullName, object graph)
    {
        FileStream fs = null;
        try
        {
            fs = new FileStream(fileFullName, FileMode.OpenOrCreate, FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, graph);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            if (fs != null)
            {
                fs.Close();
            }
        }
    }

    /// <summary>
    /// 读取文件 需要处理异常
    /// </summary>
    /// <param name="fileFullName">文件完整路径</param>
    /// <returns>读取到的数据</returns>
    public static object ReadFile(string fileFullName)
    {
        FileStream fs = null;
        try
        {
            fs = new FileStream(fileFullName, FileMode.OpenOrCreate);
            BinaryFormatter bf = new BinaryFormatter();
            return bf.Deserialize(fs);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            if (fs != null)
            {
                fs.Close();
            }
        }
    }
}