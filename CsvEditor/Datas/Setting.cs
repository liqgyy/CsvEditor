using System;
using System.IO;
using System.Windows.Forms;

// TODO 优化读取设置文件失败时的逻辑
[Serializable]
public class Setting
{
    private static Setting ms_Instance = null;
    public static Setting Instance {
        get
        {
            if (ms_Instance == null)
            {
                Load();
            }
            return ms_Instance;
        }
    }

    private static string ms_SaveFileFullName = Directory.GetCurrentDirectory() + "\\" + GlobalData.SETTING_FILE_NAME;


    public bool UseSkin;
    public string CurrentSkin;

    public Setting()
    {
        UseSkin = false;
        CurrentSkin = GlobalData.SKIN_DEFAULT_SSK;
    }

    /// <summary>
    /// 保存到文件
    /// </summary>
    public static void Save()
    {
        try
        {
            BinaryFormatterUtility.Write(ms_SaveFileFullName, Instance);
        }
        catch (Exception ex)
        {
            Debug.ShowExceptionMessageBox("保存设置失败", ex);
        }
    }

    /// <summary>
    /// 读取设置到ms_Instance
    /// </summary>
    public static void Load()
    {
        // 配置文件不存在
        if (GlobalData.SETTING_FORCE_INITIALIZE || !File.Exists(ms_SaveFileFullName))
        {
            ms_Instance = new Setting();
        }

        try
        {
            ms_Instance = (Setting)BinaryFormatterUtility.Read(ms_SaveFileFullName);
        }
        catch (Exception ex)
        {
            if (Debug.ShowExceptionMessageBox("读取设置失败\n是否删除设置文件重新读取?", ex, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ms_Instance = DeleteAndLoad();
            }
        }
    }

    private static Setting DeleteAndLoad()
    {
        try
        {
            if (File.Exists(ms_SaveFileFullName))
            {
                File.Delete(ms_SaveFileFullName);
            }
            ms_Instance = (Setting)BinaryFormatterUtility.Read(ms_SaveFileFullName);
        }
        catch (Exception ex)
        {
            if (Debug.ShowExceptionMessageBox("读取设置失败\n是否删除设置文件重新读取?", ex, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ms_Instance = DeleteAndLoad();
            }
        }
        
        if (ms_Instance == null)
        {
            ms_Instance = new Setting();
        }
        return ms_Instance;
    }
}