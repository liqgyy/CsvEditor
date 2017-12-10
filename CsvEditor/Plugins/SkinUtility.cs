using System;
using System.IO;

public class SkinUtility
{
    public static void SetSkin()
    {
        if (!Setting.Instance.UseSkin)
        {
            return;
        }
        string skinFile = GetSkinFileFullName(Setting.Instance.CurrentSkin);
        if (!File.Exists(skinFile))
        {
            skinFile = GetSkinFileFullName(GlobalData.SKIN_DEFAULT_SSK);
        }
        MainForm.Instance.SkinEngine.SkinFile = skinFile;
    }

    public static string[] GetAllSkinSskName()
    {
        string[] skins = null;
        try
        {
            skins = Directory.GetFiles(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + GlobalData.SKIN_FOLDER_NAME, "*.ssk", SearchOption.TopDirectoryOnly);
            for(int skinIdx = 0; skinIdx < skins.Length; skinIdx ++)
            {
                skins[skinIdx] = Path.GetFileName(skins[skinIdx]).Replace(".ssk", "");
            }
        }
        catch (Exception ex)
        {
            Debug.ShowExceptionMessageBox("读取皮肤文件失败", ex);
            skins = new string[0];
        }
        return skins;
    }

    private static string GetSkinFileFullName(string sskName)
    {
        return AppDomain.CurrentDomain.SetupInformation.ApplicationBase + GlobalData.SKIN_FOLDER_NAME + "\\" + sskName + ".ssk";
    }
}