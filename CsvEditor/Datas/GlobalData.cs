public class GlobalData
{
    #region Registry
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

	public const string CSVLAYOUT_FILE_NAME = "Layout.bin";

	public const string SPECIFIC_CSVLAYOUT_FILE_NAME = "SpecificLayout.bin";
	#endregion // End Setting

	public const int CSV_NOTE_POLYGON_SIZE = 6;
}