using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public partial class DataGridViewConsoleForm : Form
{
	private List<Message> m_MessageList;
	/// <summary>
	/// ListBox中的idx对应MessageList中的idx
	/// </summary>
	private List<int> m_MessageIndexList;
	private int m_ListBoxSelectedIndex = -1;

	public static void ShowForm(List<Message> messageList , string formText)
	{
		if (messageList == null || messageList.Count < 1)
		{
			return;
		}
		else
		{
			DataGridViewConsoleForm form = new DataGridViewConsoleForm(messageList);
			form.Text = "控制台 - " + formText;
			form.Show();
		}
	}

	private DataGridViewConsoleForm(List<Message> messageList)
	{
		InitializeComponent();
		UpdateListBoxSize();

		m_MessageList = messageList;
		UpdateListBox();
	}

	private void UpdateListBoxSize()
	{
		Size listBoxSize = m_MessageListBox.Size;
		listBoxSize.Height = m_SplitContainer.Panel1.ClientSize.Height - 36;
		m_MessageListBox.Size = listBoxSize;
	}

	private void UpdateListBox()
	{
		// 当前选中的消息的索引
		int messageSelectedIndex = 0;
		// 需要选中ListBox的索引
		int listBoxSelectIndex = 0;
		if (m_ListBoxSelectedIndex >= 0 && m_ListBoxSelectedIndex < m_MessageIndexList.Count)
		{
			messageSelectedIndex = m_MessageIndexList[m_ListBoxSelectedIndex];
		}

		m_MessageListBox.Items.Clear();
		m_MessageIndexList = new List<int>();

		int infoCount = 0;
		int warningCount = 0;
		int errorCount = 0;

		bool info = m_InfoCheckBox.Checked;
		bool warning = m_WarningCheckBox.Checked;
		bool error = m_ErrorCheckBox.Checked;

		for (int msgIdx = 0; msgIdx< m_MessageList.Count; msgIdx++)
		{
			Message iterMessage = m_MessageList[msgIdx];
			switch(iterMessage.Level)
			{
				case Level.Info:
					infoCount++;
					if (info)
					{
						AddItemToListBox(iterMessage, msgIdx);
					}
					break;
				case Level.Warning:
					warningCount++;
					if (warning)
					{
						AddItemToListBox(iterMessage, msgIdx);
					}
					break;
				case Level.Error:
					errorCount++;
					if (error)
					{
						AddItemToListBox(iterMessage, msgIdx);
					}
					break;
			}
			if (messageSelectedIndex == msgIdx)
			{
				listBoxSelectIndex = m_MessageListBox.Items.Count - 1;
			}
		}

		if (listBoxSelectIndex < m_MessageListBox.Items.Count)
			m_MessageListBox.SelectedIndex = listBoxSelectIndex;

		m_InfoCheckBox.Text = string.Format("{0} - {1}", LevelToString(Level.Info), infoCount);
		m_WarningCheckBox.Text = string.Format("{0} - {1}", LevelToString(Level.Warning), warningCount);
		m_ErrorCheckBox.Text = string.Format("{0} - {1}", LevelToString(Level.Error), errorCount);
	}

	private void AddItemToListBox(Message message, int index)
	{
		string item = string.Format("{0}: 第({1})行({2})列\t{3}", 
			LevelToString(message.Level),
			message.Row + 1, 
			ConvertUtility.NumberToLetter(message.Column + 1), 
			message.Caption);
		m_MessageListBox.Items.Add(item);
		m_MessageIndexList.Add(index);
	}

	private string LevelToString(Level level)
	{
		if (level == Level.Info)
		{
			return "信息";
		}
		else if (level == Level.Warning)
		{
			return "警告";
		}
		else if (level == Level.Error)
		{
			return "错误";
		}
		return "未知";
	}

	private Message GetMessageWithListBoxItemIndex(int selectedIndex)
	{
		if (selectedIndex >= 0 && selectedIndex < m_MessageIndexList.Count)
		{
			int messageIdx = m_MessageIndexList[selectedIndex];
			if (messageIdx >= 0 && messageIdx < m_MessageList.Count)
			{
				Message message = m_MessageList[messageIdx];
				return message;
			}
		}

		Message newMessage = new Message
		{
			Level = Level.None
		};
		return newMessage; 
	}

	#region UIEvent
	private void OnSplitContainer_Panel1_ClientSizeChanged(object sender, EventArgs e)
	{
		UpdateListBoxSize();
	}

	private void OnCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		UpdateListBox();
	}

	private void OnMessageListBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		m_DetailTextBox.Text = "";
		m_ListBoxSelectedIndex = m_MessageListBox.SelectedIndex;

		Message message = GetMessageWithListBoxItemIndex(m_ListBoxSelectedIndex);
		if (message.Level == Level.None)
		{
			return;
		}

		m_DetailTextBox.Text = message.Text.Replace("\n", "\r\n");
	}

	private void OnMessageListBox_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		int itemIndex = m_MessageListBox.IndexFromPoint(e.Location);
		Message message = GetMessageWithListBoxItemIndex(itemIndex);
		if (message.Level == Level.None)
		{
			return;
		}

		MainForm.Instance.GetCsvForm().SelectDataGridViewCell(message.Row, message.Column);
	}
	#endregion //End UIEvent

	public struct Message
	{
		public Level Level;
		public int Row;
		public int Column;
		public string Caption;
		public string Text;
	}

	public enum Level
	{
		/// <summary>
		/// 表示这条消息不存在
		/// </summary>
		None,	
		Info,
		Warning,
		Error
	}
}