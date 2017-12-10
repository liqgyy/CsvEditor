using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//"C:\Program Files\Beyond Compare 4\BCompare.exe" / fv = "Table Compare" "Test4.csv" "Test5.csv"

/// <summary>
/// 比较工具
/// 使用 http://www.beyondcompare.cc/ 软件的cmd命令比较两个文件
/// </summary>
public class BeyondCompare
{
	private const string BEYOND_COMPARE_URL = "https://www.devart.com/codecompare";

    public static BeyondCompare Instance
    {
        get
        {
            if (ms_Instance == null)
            {
                ms_Instance = new BeyondCompare();
            }
            return ms_Instance;
        }
    }

    private static BeyondCompare ms_Instance;

    public bool Initialized = false;

    /// <summary>
    /// CodeCompare.exe的路径
    /// </summary>
    private string m_ExeFilePath;

    public BeyondCompare()
    {
        Initialized = Initialize();
    }

    /// <summary>
    /// 打开CodeCompare软件比较两个文件
    /// </summary>
    /// <param name="oldFilePath">旧文件</param>
    /// <param name="newFilePath">新文件</param>
    /// <param name="needCopy">为true时，比较文件副本</param>
    /// <returns>是否成功打开CodeCompare</returns>
    public bool Compare(string fileFullName1,
        string fileFullName2,
        bool needCopy = false)
    {
        if (string.IsNullOrEmpty(m_ExeFilePath))
        {
            return false;
        }

        if (needCopy)
        {
            try
            {
                string copyFileFullName = Path.GetTempPath() + "CsvEditorCompare1";
                File.Copy(fileFullName1, copyFileFullName);
                fileFullName1 = copyFileFullName;

                copyFileFullName = Path.GetTempPath() + "CsvEditorCompare2";
                File.Copy(fileFullName2, copyFileFullName);
                fileFullName2 = copyFileFullName;
            }
            catch (Exception ex)
            {
                Debug.ShowExceptionMessageBox("拷贝副本: " + fileFullName1 + " - " + fileFullName2 + " 失败", ex);
                return false;
            }
        }

        System.Diagnostics.Process cmdProcess = null;
        try
        {
            cmdProcess = new System.Diagnostics.Process();
            cmdProcess.StartInfo.FileName = "cmd.exe";
            // 是否使用操作系统shell启动
            cmdProcess.StartInfo.UseShellExecute = false;
            // 接受来自调用程序的输入信息
            cmdProcess.StartInfo.RedirectStandardInput = true;
            // 由调用程序获取输出信息
            cmdProcess.StartInfo.RedirectStandardOutput = true;
            // 重定向标准错误输出
            cmdProcess.StartInfo.RedirectStandardError = true;
            // 不显示程序窗口
            cmdProcess.StartInfo.CreateNoWindow = true;
            cmdProcess.Start();

            // 向cmd窗口发送输入信息
            // 向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
            // 同类的符号还有&&和||前者表示必须前一个命令执行成功才会执行后面的命令，后者表示必须前一个命令执行失败才会执行后面的命令
            cmdProcess.StandardInput.WriteLine(string.Format("\"{0}\" /fv=\"Table Compare\" \"{1}\" \"{2}\" & exit",
                m_ExeFilePath, fileFullName1, fileFullName2));

            Console.WriteLine(cmdProcess.StandardOutput.ReadToEnd());

            // 等待程序执行完退出进程
            cmdProcess.WaitForExit();
            cmdProcess.Close();
        }
        catch (Exception ex)
        {
            Debug.ShowExceptionMessageBox("比较文件: " + fileFullName1 + " - " + fileFullName2 + " 失败", ex);
            return false;
        }
        finally
        {
            if (cmdProcess != null)
                cmdProcess.Close();
        }
        return true;
    }

     /// <summary>
    // CodeCompare注册表里只有帮助文件的地址, 利用帮助文件的地址寻找CodeCompare.exe的地址
    /// </summary>
    /// <returns>初始化是否成功</returns>
    private bool Initialize()
    {
        RegistryKey localMachineKey = null;
        RegistryKey software = null;
        try
        {
            localMachineKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            software = localMachineKey.OpenSubKey(GlobalData.REGISTRY_KEY_BEYONDCOMPARE, false);
            m_ExeFilePath = (string)software.GetValue("ExePath");
        }
        catch (Exception ex)
        {
            Debug.ShowExceptionMessageBox("代码比较工具初始化失败,可能未正确安装BeyondCompare", ex);
            System.Diagnostics.Process.Start(BEYOND_COMPARE_URL);
        }
        finally
        {
            RegistryUtility.CloseRegistryKey(software);
            RegistryUtility.CloseRegistryKey(localMachineKey);
        }
        return true;
    }
}