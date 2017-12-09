using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class BinaryFormatterUtility
{
    /// <summary>
    /// 写入文件 需要处理异常
    /// </summary>
    /// <param name="fileFullName">文件完整路径</param>
    /// <param name="graph">需要保存的数据</param>
    public static void Write(string fileFullName, object graph)
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
    public static object Read(string fileFullName)
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