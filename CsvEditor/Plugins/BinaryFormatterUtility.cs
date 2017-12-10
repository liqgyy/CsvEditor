using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SerializeUtility
{
    private static string ObjectToBase64String(object graph)
    {
        string base64String;
        MemoryStream ms = null;
        try
        {
            ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, graph);
            base64String = Convert.ToBase64String(ms.GetBuffer());
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
        return base64String;
    }

    private static object Base64StringToObject(string base64String)
    {
        object graph = null;
        MemoryStream ms = null;
        try
        {
            ms = new MemoryStream(Convert.FromBase64String(base64String));
            BinaryFormatter bf = new BinaryFormatter();
            graph = bf.Deserialize(ms);
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
        return graph;
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
        object graph = null;
        try
        {
            fs = new FileStream(fileFullName, FileMode.OpenOrCreate);
            BinaryFormatter bf = new BinaryFormatter();
            graph = bf.Deserialize(fs);
            fs.Close();
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
        return graph;
    }
}