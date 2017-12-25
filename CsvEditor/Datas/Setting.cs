using System;
using System.IO;
using System.Windows.Forms;

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

    private static string ms_SaveFileFullName = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + GlobalData.SETTING_FILE_NAME;

	#region DiffCompare
	public bool BeyondCompareAutoExePath;
	public string BeyondCompareExePath;

	public bool CodeCompareAutoExePath;
	public string CodeCompareExePath;
	#endregion

	public Setting()
    {
		BeyondCompareAutoExePath = true;
		BeyondCompareExePath = "";
		CodeCompareAutoExePath = true;
		CodeCompareExePath = "";

		RegistryUtility.SetRegisterFileExtendWithThisApp(".csv", "CsvEditor.CSV", "CsvEditor的csv文件", "在CsvEditor中打开");
    }

    /// <summary>
    /// 保存到文件
    /// </summary>
    public static void Save()
    {
        try
        {
            SerializeUtility.WriteFile(ms_SaveFileFullName, Instance);
        }
        catch (Exception ex)
        {
            Debug.ShowExceptionMessageBox("保存设置失败\n" + ms_SaveFileFullName, ex);
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
			return;
        }

        try
        {
            ms_Instance = (Setting)SerializeUtility.ReadFile(ms_SaveFileFullName);
        }
        catch (Exception ex)
        {
            if (Debug.ShowExceptionMessageBox("读取设置失败\n是否删除设置文件重新读取?\n" + ms_SaveFileFullName, ex, MessageBoxButtons.YesNo) == DialogResult.Yes)
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
        }
        catch (Exception ex)
        {
			Debug.ShowExceptionMessageBox("删除设置文件失败,使用默认设置.", ex);
        }
        
        if (ms_Instance == null)
        {
            ms_Instance = new Setting();
        }
        return ms_Instance;
    }
}