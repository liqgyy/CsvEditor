public class GlobalData
{
    #region Registry
    public const string REGISTRY_KEY_SOFTWARE = "SOFTWARE\\CsvEditor";
    public const string REGISTRY_KEY_INITIALIZED = "Initialized";
    /// <summary>
    /// 为true时强制初始化
    /// TODO 发布时改为false
    /// </summary>
    public const bool REGISTRY_FORCE_INITIALIZE = true;

    public const string REGISTRY_KEY_CODECOMPARE = "SOFTWARE\\Devart\\Code Compare";
    public const string REGISTRY_KEY_BEYONDCOMPARE = "SOFTWARE\\Scooter Software\\Beyond Compare 4";
    #endregion // End Registry

    #region Setting
    /// <summary>
    /// 为true时强制初始化
    /// TODO 发布时改为false
    /// </summary>
    public const bool SETTING_FORCE_INITIALIZE = false;

    public const string SETTING_FILE_NAME = "Setting.bin";
    #endregion // End Setting

    public const string SKIN_FOLDER_NAME = "Skins";
    public const string SKIN_DEFAULT_SSK = "office2007";
}