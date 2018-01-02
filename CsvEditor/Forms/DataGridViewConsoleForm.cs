using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public partial class DataGridViewConsoleForm : Form
{
	private static DataGridViewConsoleForm ms_Instance;

	private List<Message> m_MessageList;
	/// <summary>
	/// ListBox中的idx对应MessageList中的idx
	/// </summary>
	private List<int> m_MessageIndexList;
	private int m_ListBoxSelectedIndex = -1;

	private DataGridView m_DataGridView;

	/// <summary>
	/// 测试功能，暂时隐藏
	/// </summary>
	private bool m_Collapse = false;
	private string m_LastCaption = "";
	private int m_CollapseCount = 0;

	public static void ShowForm(List<Message> messageList, DataGridView dataGridView, string formText)
	{
		if (messageList == null || messageList.Count < 1)
		{
			return;
		}
		else
		{
			if (ms_Instance != null)
			{
				ms_Instance.Close();
				ms_Instance.Dispose();
				ms_Instance = null;
			}
			ms_Instance = new DataGridViewConsoleForm(messageList, dataGridView);
			ms_Instance.Text = "控制台 - " + formText;
			ms_Instance.Show();
		}
	}

	private DataGridViewConsoleForm(List<Message> messageList, DataGridView dataGridView)
	{
		InitializeComponent();

		m_MessageList = messageList;
		m_DataGridView = dataGridView;

		UpdateListBoxSize();
		UpdateListBox();
	}

	private void UpdateListBoxSize()
	{
		Size listBoxSize = m_MessageListView.Size;
		listBoxSize.Height = m_SplitContainer.Panel1.ClientSize.Height - 36;
		m_MessageListView.Size = listBoxSize;
	}

	private void UpdateListBox()
	{
		m_Collapse = m_CollapseCheckBox.Checked;

		// 当前选中的消息的索引
		int messageSelectedIndex = 0;
		// 需要选中ListBox的索引
		int listBoxSelectIndex = 0;
		if (m_ListBoxSelectedIndex >= 0 && m_ListBoxSelectedIndex < m_MessageIndexList.Count)
		{
			messageSelectedIndex = m_MessageIndexList[m_ListBoxSelectedIndex];
		}

		m_MessageListView.Items.Clear();
		m_MessageIndexList = new List<int>();

		m_LastCaption = "";
		m_CollapseCount = 0;

		int infoCount = 0;
		int warningCount = 0;
		int errorCount = 0;

		bool info = m_InfoCheckBox.Checked;
		bool warning = m_WarningCheckBox.Checked;
		bool error = m_ErrorCheckBox.Checked;

		for (int msgIdx = 0; msgIdx< m_MessageList.Count; msgIdx++)
		{
			Message iterMessage = m_MessageList[msgIdx];

			switch (iterMessage.Level)
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
				listBoxSelectIndex = m_MessageListView.Items.Count - 1;
			}
		}

		AddCollapseToLastItem();

		if (listBoxSelectIndex < m_MessageListView.Items.Count)
		{
			m_MessageListView.Items[listBoxSelectIndex].Selected = true;
		}

		m_InfoCheckBox.Text = string.Format("{0} {1}", LevelToString(Level.Info), infoCount);
		m_WarningCheckBox.Text = string.Format("{0} {1}", LevelToString(Level.Warning), warningCount);
		m_ErrorCheckBox.Text = string.Format("{0} {1}", LevelToString(Level.Error), errorCount);
	}

	private void AddItemToListBox(Message message, int index)
	{
		if (m_Collapse)
		{
			if (m_LastCaption == message.Caption)
			{
				m_CollapseCount++;
				return;
			}
			AddCollapseToLastItem();
			m_LastCaption = message.Caption;
			m_CollapseCount = 0;
		}

		string item = FormatMessageCaption(message);
		ListViewItem newItem = new ListViewItem(LevelToString(message.Level));
		newItem.SubItems.Add(FormatMessagePosition(message));
		newItem.SubItems.Add(message.Caption);
		m_MessageListView.Items.Add(newItem);
		m_MessageIndexList.Add(index);
	}

	private string FormatMessagePosition(Message message)
	{
		string rowColumn = "";
		if (message.Row < 0 && message.Column < 0)
		{
			rowColumn = "整表";
		}
		else if (message.Row < 0)
		{
			rowColumn = string.Format("({0})列", ConvertUtility.NumberToLetter(message.Column + 1));
		}
		else if (message.Column < 0)
		{
			rowColumn = string.Format("({0})行", message.Row + 1);
		}
		else
		{
			rowColumn = string.Format("({0},{1})", message.Row + 1, ConvertUtility.NumberToLetter(message.Column + 1));
		}
		return rowColumn;
	}

	private string FormatMessageCaption(Message message)
	{
		string rowColumn = FormatMessagePosition(message);
		return string.Format("{0,-4}{1,-12}\t{2}",
			LevelToString(message.Level),
			rowColumn,			
			message.Caption);
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
		else
		{
			return "未知";
		}
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

	private void AddCollapseToLastItem()
	{
		if (m_CollapseCount > 0)
		{
			ListViewItem lastItem = m_MessageListView.Items[m_MessageListView.Items.Count - 1];
			string lastItemValue = lastItem.SubItems[2].Text;
			lastItemValue = string.Format("{0}\tCollapse:{1}", lastItemValue, m_CollapseCount + 1);
			lastItem.SubItems[2].Text = lastItemValue;
		}
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

	private void OnMessageListView_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (m_MessageListView.SelectedItems.Count > 0)
		{
			m_DetailTextBox.Text = "";
			m_ListBoxSelectedIndex = m_MessageListView.SelectedItems[0].Index;

			Message message = GetMessageWithListBoxItemIndex(m_ListBoxSelectedIndex);
			if (message.Level == Level.None)
			{
				return;
			}
			string caption = FormatMessageCaption(message);
			string text = string.Format("{0}\n{1}", caption.Replace("\t", "\n\n"), message.Text);
			m_DetailTextBox.Text = text.Replace("\n", "\r\n");
		}
	}

	private void OnMessageListView_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (m_MessageListView.SelectedItems.Count > 0)
		{
			int itemIndex = m_MessageListView.SelectedItems[0].Index;
			Message message = GetMessageWithListBoxItemIndex(itemIndex);
			if (message.Level == Level.None)
			{
				return;
			}

			DataGridViewUtility.SelectCell(m_DataGridView, message.Row, message.Column);
		}
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