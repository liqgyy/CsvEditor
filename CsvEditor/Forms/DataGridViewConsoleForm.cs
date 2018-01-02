using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public partial class DataGridViewConsoleForm : Form
{
	private List<Message> m_Messages;
	/// <summary>
	/// ListView中的index对应Messages中的index
	/// </summary>
	private List<int> m_ListViewItemIndexToMessagesIndex;
	private int m_ListViewSelectedIndex = -1;

	private DataGridView m_DataGridView;

	/// <summary>
	/// 测试功能，暂时隐藏
	/// </summary>
	private bool m_Collapse = false;
	private string m_LastCaption = "";
	private int m_CollapseCount = 0;

	public static void ShowForm(List<Message> messages, DataGridView dataGridView, string formText)
	{
		if (messages == null || messages.Count < 1)
		{
			return;
		}
		else
		{
			DataGridViewConsoleForm form = new DataGridViewConsoleForm(messages, dataGridView);
			form.Text = "控制台 - " + formText;
			form.Show();
		}
	}

	private DataGridViewConsoleForm(List<Message> messageList, DataGridView dataGridView)
	{
		InitializeComponent();

		m_Messages = messageList;
		m_DataGridView = dataGridView;

		UpdateListViewSize();
		UpdateListView();
	}

	private void UpdateListViewSize()
	{
		Size size = m_MessageListView.Size;
		size.Height = m_SplitContainer.Panel1.ClientSize.Height - 36;
		m_MessageListView.Size = size;
	}

	private void UpdateListView()
	{
		m_Collapse = m_CollapseCheckBox.Checked;

		// 当前选中的消息的索引
		int messageSelectedIndex = 0;
		// 需要选中ListView的索引
		int selectIndex = 0;
		if (m_ListViewSelectedIndex >= 0 && m_ListViewSelectedIndex < m_ListViewItemIndexToMessagesIndex.Count)
		{
			messageSelectedIndex = m_ListViewItemIndexToMessagesIndex[m_ListViewSelectedIndex];
		}

		m_MessageListView.Items.Clear();
		m_ListViewItemIndexToMessagesIndex = new List<int>();

		m_LastCaption = "";
		m_CollapseCount = 0;

		int infoCount = 0;
		int warningCount = 0;
		int errorCount = 0;

		bool info = m_InfoCheckBox.Checked;
		bool warning = m_WarningCheckBox.Checked;
		bool error = m_ErrorCheckBox.Checked;

		for (int msgIdx = 0; msgIdx< m_Messages.Count; msgIdx++)
		{
			Message iterMessage = m_Messages[msgIdx];

			switch (iterMessage.Level)
			{
				case Level.Info:
					infoCount++;
					if (info)
					{
						AddMessageToListView(iterMessage, msgIdx);
					}
					break;
				case Level.Warning:
					warningCount++;
					if (warning)
					{
						AddMessageToListView(iterMessage, msgIdx);
					}
					break;
				case Level.Error:
					errorCount++;
					if (error)
					{
						AddMessageToListView(iterMessage, msgIdx);
					}
					break;
			}
			if (messageSelectedIndex == msgIdx)
			{
				selectIndex = m_MessageListView.Items.Count - 1;
			}
		}

		AddCollapseToLastItem();

		if (selectIndex >= 0 && selectIndex < m_MessageListView.Items.Count)
		{
			m_MessageListView.Items[selectIndex].Selected = true;
		}

		m_InfoCheckBox.Text = string.Format("{0} {1}", LevelToString(Level.Info), infoCount);
		m_WarningCheckBox.Text = string.Format("{0} {1}", LevelToString(Level.Warning), warningCount);
		m_ErrorCheckBox.Text = string.Format("{0} {1}", LevelToString(Level.Error), errorCount);
	}

	private void AddMessageToListView(Message message, int index)
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
		m_ListViewItemIndexToMessagesIndex.Add(index);
	}

	private string FormatMessagePosition(Message message)
	{
		string rowColumn = "";
		if (message.Row < 0 && message.Column < 0)
		{
			rowColumn = "na,na";
		}
		else if (message.Row < 0)
		{
			rowColumn = string.Format("na,{0}", ConvertUtility.NumberToLetter(message.Column + 1));
		}
		else if (message.Column < 0)
		{
			rowColumn = string.Format("{0},na", message.Row + 1);
		}
		else
		{
			rowColumn = string.Format("{0},{1}", message.Row + 1, ConvertUtility.NumberToLetter(message.Column + 1));
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

	private Message GetMessage(int listViewItemIndex)
	{
		if (listViewItemIndex >= 0 && listViewItemIndex < m_ListViewItemIndexToMessagesIndex.Count)
		{
			int messageIdx = m_ListViewItemIndexToMessagesIndex[listViewItemIndex];
			if (messageIdx >= 0 && messageIdx < m_Messages.Count)
			{
				Message message = m_Messages[messageIdx];
				return message;
			}
		}
		return null; 
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
		UpdateListViewSize();
	}

	private void OnCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		UpdateListView();
	}

	private void OnMessageListView_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (m_MessageListView.SelectedItems.Count > 0)
		{
			m_DetailTextBox.Text = "";
			m_ListViewSelectedIndex = m_MessageListView.SelectedItems[0].Index;

			Message message = GetMessage(m_ListViewSelectedIndex);
			if (message == null)
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
			Message message = GetMessage(itemIndex);
			if (message == null)
			{
				return;
			}

			DataGridViewUtility.SelectCell(m_DataGridView, message.Row, message.Column);
		}
	}
	#endregion //End UIEvent

	public class Message
	{
		public Level Level;
		public int Row;
		public int Column;
		public string Caption;
		public string Text;
	}

	public enum Level
	{
		Info,
		Warning,
		Error
	}	
}