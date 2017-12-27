//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Text;
//using System.Threading.Tasks;

//public class ProcessUtility
//{
//	private const int WM_COPYDATA = 0x004A;
	
//	public void SendMessageToProcess(string msg)
//	{
//		Process process = Process.GetCurrentProcess();
//		Process[] processs = Process.GetProcessesByName(process.ProcessName);

//		for(int processIdx = 0; processIdx < processs.Length; processIdx++)
//		{
//			Process iterProcess = processs[processIdx];
//			if (iterProcess == process)
//			{
//				continue;
//			}
//			IntPtr mainWindow = iterProcess.MainWindowHandle;
//			if (mainWindow != IntPtr.Zero)
//			{
//				byte[] arr = System.Text.Encoding.Default.GetBytes(msg);
//				int len = arr.Length;
//				COPYDATASTRUCT cdata;
//				cdata.dwData = (IntPtr)100;
//				cdata.lpData = msg;
//				cdata.cData = len + 1;
//				SendMessage(mainWindow, WM_COPYDATA, 0, ref cdata);
//			}
//		}
//	}


//	public struct COPYDATASTRUCT
//	{
//		public IntPtr dwData;
//		public int cData;
//		[MarshalAs(UnmanagedType.LPStr)]
//		public string lpData;
//	}

//	[DllImport("User32.dll")]
//	private static extern int SendMessage(IntPtr hwnd, int msg, int wParam, ref COPYDATASTRUCT IParam);
//}