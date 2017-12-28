using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class RegistryUtility
{
	/// <summary>
	/// 设置文件关联本程序
	/// </summary>
	/// <param name="fileExtend">文件扩展名 必须为.* 例:".txt" ".mp3"</param>
	/// <param name="progID">注册表里的progI. The proper format of a ProgID key name is [Vendor or Application].[Component].[Version], separated by periods and with no spaces, as in Word.Document.6. The Version portion is optional but strongly recommended (see Using Versioned PROGIDs).</param>
	/// <param name="progDescription">作用未知</param>
	/// <param name="playDescription">作用未知</param>
	public static void SetRegisterFileExtendWithThisApp(string fileExtend, string progID, string progDescription, string playDescription)
	{
		// 创建ProgID
		RegistryKey classesRootKey = null;
		RegistryKey progIDKey = null;
		RegistryKey defaultIconKey = null;
		RegistryKey shellKey = null;
		RegistryKey openKey = null;
		RegistryKey commandKey = null;
		RegistryKey playKey = null;
		try
		{
			classesRootKey = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry64);
			if (classesRootKey.OpenSubKey(progID) == null)
			{
				progIDKey = classesRootKey.CreateSubKey(progID);
				// 默认键值，显示给用户看的文件类型描述
				progIDKey.SetValue("", progDescription, RegistryValueKind.String);

				// 文件显示的图标
				defaultIconKey = progIDKey.CreateSubKey("DefaultIcon");
				// 默认键值，指定文件显示图标
				defaultIconKey.SetValue("", Application.ExecutablePath + ",0", RegistryValueKind.String);

				// shell//open//command
				shellKey = progIDKey.CreateSubKey("shell");
				openKey = shellKey.CreateSubKey("open");
				commandKey = openKey.CreateSubKey("command");
				commandKey.SetValue("", "\"" + Application.ExecutablePath + "\" \"%1\"", RegistryValueKind.String);

				// shell//play//command
				playKey = shellKey.CreateSubKey("play");
				playKey.SetValue("", playDescription, RegistryValueKind.String);
				commandKey = playKey.CreateSubKey("command");
				commandKey.SetValue("", "\"" + Application.ExecutablePath + "\" \"%1\"", RegistryValueKind.String);
			}
		}
		catch (Exception ex)
		{
			DebugUtility.ShowExceptionMessageBox("创建ProgID：" + progID + "失败", ex);
			CloseKey(classesRootKey);
			return;
		}
		finally
		{
			CloseKey(defaultIconKey);
			CloseKey(shellKey);
			CloseKey(openKey);
			CloseKey(commandKey);
			CloseKey(playKey);
			CloseKey(progIDKey);
		}

		// 修改对应文件类型的默认的关联程序
		// 假设文件类型在系统里已经注册，这里只是简单修改一下关联
		RegistryKey fileExtendKey = null;
		try
		{
			fileExtendKey = classesRootKey.OpenSubKey(fileExtend, true);
			if (fileExtendKey != null)
			{
				fileExtendKey.SetValue("", progID, RegistryValueKind.String);
				fileExtendKey.Close();
			}

			// 通知系统，文件关联已经是作用，不然可能要等到系统重启才看到效果
			SHChangeNotify(HChangeNotifyEventID.SHCNE_ASSOCCHANGED, HChangeNotifyFlags.SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero);
		}
		catch (Exception ex)
		{
			DebugUtility.ShowExceptionMessageBox("修改文件: " + fileExtend + " 关联程序失败", ex);
			return;
		}
		finally
		{
			CloseKey(fileExtendKey);
			CloseKey(classesRootKey);
		}
	}

	public static object GetBaseSubKeyValue(RegistryHive hKey, RegistryView view, string subName, string valueName)
	{
		RegistryKey baseKey = null;
		RegistryKey software = null;
		try
		{
			baseKey = RegistryKey.OpenBaseKey(hKey, view);
			software = baseKey.OpenSubKey(subName, false);
			return software.GetValue(valueName);
		}
		catch (Exception ex)
		{
			DebugUtility.ShowExceptionMessageBox("获取注册表值失败\n"
				+ hKey.ToString() + "\t"
				+ view.ToString() + "\t"
				+ subName + "\t"
				+ valueName, ex);
			return null;
		}
		finally
		{
			CloseKey(software);
			CloseKey(baseKey);
		}
	}

	public static void CloseKey(RegistryKey key)
	{
		if (key != null)
			key.Close();
	}

	#region DllImport
	[Flags]
	private enum HChangeNotifyFlags
	{
		SHCNF_DWORD = 0x0003,
		SHCNF_IDLIST = 0x0000,
		SHCNF_PATHA = 0x0001,
		SHCNF_PATHW = 0x0005,
		SHCNF_PRINTERA = 0x0002,
		SHCNF_PRINTERW = 0x0006,
		SHCNF_FLUSH = 0x1000,
		SHCNF_FLUSHNOWAIT = 0x2000
	}

	[Flags]
	private enum HChangeNotifyEventID
	{
		SHCNE_ALLEVENTS = 0x7FFFFFFF,
		SHCNE_ASSOCCHANGED = 0x08000000,
		SHCNE_ATTRIBUTES = 0x00000800,
		SHCNE_CREATE = 0x00000002,
		SHCNE_DELETE = 0x00000004,
		SHCNE_DRIVEADD = 0x00000100,
		SHCNE_DRIVEADDGUI = 0x00010000,
		SHCNE_DRIVEREMOVED = 0x00000080,
		SHCNE_EXTENDED_EVENT = 0x04000000,
		SHCNE_FREESPACE = 0x00040000,
		SHCNE_MEDIAINSERTED = 0x00000020,
		SHCNE_MEDIAREMOVED = 0x00000040,
		SHCNE_MKDIR = 0x00000008,
		SHCNE_NETSHARE = 0x00000200,
		SHCNE_NETUNSHARE = 0x00000400,
		SHCNE_RENAMEFOLDER = 0x00020000,
		SHCNE_RENAMEITEM = 0x00000001,
		SHCNE_RMDIR = 0x00000010,
		SHCNE_SERVERDISCONNECT = 0x00004000,
		SHCNE_UPDATEDIR = 0x00001000,
		SHCNE_UPDATEIMAGE = 0x00008000,
	}

	[DllImport("shell32.dll")]
	private static extern void SHChangeNotify(HChangeNotifyEventID wEventId, HChangeNotifyFlags uFlags, IntPtr dwItem1, IntPtr dwItem2);
	#endregion // End DllImport
}