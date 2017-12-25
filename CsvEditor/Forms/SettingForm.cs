using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class SettingForm : Form
{
    private SettingItem[] m_SettingItems;

    public SettingForm()
    {
        InitializeComponent();

		m_SettingItems = new SettingItem[(int)SettingItemType.End];
		m_SettingItems[(int)SettingItemType.DiffCompare] = new SettingForm_DiffCompare(this, "差异比较", m_DiffComparePanal);

		m_SettingItemListBox.Items.Clear();
		for (int itemIdx = 0; itemIdx < m_SettingItems.Length; itemIdx++)
		{
			SettingItem settingItem = m_SettingItems[itemIdx];
			Trace.Assert(settingItem != null, "设置项(" + (SettingItemType)itemIdx + ")没有初始化");
			m_SettingItemListBox.Items.Add(settingItem.Text);
			settingItem.MainPanel.Visible = false;
			settingItem.MainPanel.Dock = DockStyle.Fill;
		}
		m_SettingItemListBox.SelectedIndex = 0;
	}

	public bool SettingChanged()
    {
        for(int itemIdx = 0; itemIdx < m_SettingItems.Length; itemIdx++)
        {
            if (m_SettingItems[itemIdx].SettingChanged)
            {
                return true;
            }
        }
		return false;
    }

    /// <summary>
    /// 显示是否关闭窗口的消息框
    /// </summary>
    /// <returns>Yes => 关闭窗口; No => 不关闭</returns>
    public DialogResult ShowCloseFormMessageBox()
    {
        if (!SettingChanged())
        {
            return DialogResult.Yes;
        }
        return MessageBox.Show("设置未保存，是否关闭？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
    }

    #region UIEvent
    private void OnCloseButton_Click(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        if (button == m_OkButton)
        {
			Setting.Save();
			for (int itemIdx = 0; itemIdx < m_SettingItems.Length; itemIdx++)
			{
				m_SettingItems[itemIdx].SettingChanged = false;
			}
		}
		Close();
    }

    private void OnForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if(ShowCloseFormMessageBox() == DialogResult.No)
        {
            e.Cancel = true;
			return;
        }
		MessageBox.Show("部分设置需要重启后生效", "提示");
	}

    private void OnSettingItemListBox_SelectedValueChanged(object sender, EventArgs e)
    {
        int selectedIndex = ((ListBox)m_SettingItemListBox).SelectedIndex;
        for (int itemIdx = 0; itemIdx < m_SettingItems.Length; itemIdx++)
        {
            SettingItem settingItem = m_SettingItems[itemIdx];
            if (selectedIndex == itemIdx)
            {
				settingItem.MainPanel.Visible = true;
				settingItem.Show();
            }
            else
            {
                settingItem.MainPanel.Visible = false;
                settingItem.Hide();
            }
        }
    }
    #endregion // END UIEvent

    private enum SettingItemType
    {
		Begin = -1,
		DiffCompare,
        End
    }

    public class SettingItem
    {
        public SettingItem(SettingForm settingForm, string text, Panel mainPanel)
        {
            Form = settingForm;
            Text = text;
            MainPanel = mainPanel;
        }
        public SettingForm Form;
        public string Text;
        public Panel MainPanel;

        public bool SettingChanged = false;
        protected bool m_Initialized = false;

        public void Show()
        {
            // 不在构造函数里Load是为了减少"卡死"的时间
            if (!m_Initialized)
            {
                OnLoad();
                m_Initialized = true;
            }
            OnShow();
        }
        public void Hide()
        {
            OnHide();
        }
        public void Close()
        {
            OnClose();
            SettingChanged = false;
        }

        protected virtual void OnLoad() { }
        protected virtual void OnShow() { }
        protected virtual void OnHide() { }
        protected virtual void OnClose() { }
    }

	public class SettingForm_DiffCompare : SettingItem
	{
		public SettingForm_DiffCompare(SettingForm settingForm, string text, Panel mainPanel) : base(settingForm, text, mainPanel) { }

		protected override void OnLoad()
		{
            Form.m_BeyondCompareAutoExePathCheckBox.CheckedChanged += OnAutoExePathCheckBox_CheckedChanged;
            Form.m_BeyondCompareChooseExePathTextBox.TextChanged += OnExePathTextBox_TextChanged;
			Form.m_BeyondCompareChooseExePathButton.Click += OnExePathButton_Click;
			Form.m_BeyondCompareAutoExePathCheckBox.Checked = Setting.Instance.BeyondCompareAutoExePath;
			Form.m_BeyondCompareChooseExePathTextBox.Text = Setting.Instance.BeyondCompareExePath;

			Form.m_CodeCompareAutoExePathCheckBox.CheckedChanged += OnAutoExePathCheckBox_CheckedChanged;
			Form.m_CodeCompareChooseExePathTextBox.TextChanged += OnExePathTextBox_TextChanged;
			Form.m_CodeCompareChooseExePathButton.Click += OnExePathButton_Click;
			Form.m_CodeCompareAutoExePathCheckBox.Checked = Setting.Instance.CodeCompareAutoExePath;
			Form.m_CodeCompareChooseExePathTextBox.Text = Setting.Instance.CodeCompareExePath;
		}

		protected override void OnClose()
		{
			Form.m_BeyondCompareAutoExePathCheckBox.CheckedChanged -= OnAutoExePathCheckBox_CheckedChanged;
			Form.m_BeyondCompareChooseExePathTextBox.TextChanged -= OnExePathTextBox_TextChanged;
			Form.m_BeyondCompareChooseExePathButton.Click -= OnExePathButton_Click;

			Form.m_CodeCompareAutoExePathCheckBox.CheckedChanged -= OnAutoExePathCheckBox_CheckedChanged;
			Form.m_CodeCompareChooseExePathTextBox.TextChanged -= OnExePathTextBox_TextChanged;
			Form.m_CodeCompareChooseExePathButton.Click -= OnExePathButton_Click;
		}

		private void OnExePathButton_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			// 打开选择文件窗口
			if (button == Form.m_BeyondCompareChooseExePathButton)
			{
				Form.m_DiffCompareOpenFileDialog.InitialDirectory = Setting.Instance.BeyondCompareExePath;
			}
			else if (button == Form.m_CodeCompareChooseExePathButton)
			{
				Form.m_DiffCompareOpenFileDialog.InitialDirectory = Setting.Instance.CodeCompareExePath;
			}

			if (Form.m_DiffCompareOpenFileDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			string exePath = Form.m_DiffCompareOpenFileDialog.FileName;
			if (button == Form.m_BeyondCompareChooseExePathButton)
			{
				Setting.Instance.BeyondCompareExePath = exePath;
			}
			else if (button == Form.m_CodeCompareChooseExePathButton)
			{
				Setting.Instance.CodeCompareExePath = exePath;
			}
		}

		private void OnExePathTextBox_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;

			if (textBox == Form.m_BeyondCompareChooseExePathTextBox)
			{
				SettingChanged = SettingChanged || Setting.Instance.BeyondCompareExePath != Form.m_BeyondCompareChooseExePathTextBox.Text;
				Setting.Instance.BeyondCompareExePath = Form.m_BeyondCompareChooseExePathTextBox.Text;
			}
			else if(textBox == Form.m_CodeCompareChooseExePathTextBox)
			{
				SettingChanged = SettingChanged || Setting.Instance.CodeCompareExePath != Form.m_CodeCompareChooseExePathTextBox.Text;
				Setting.Instance.CodeCompareExePath = Form.m_CodeCompareChooseExePathTextBox.Text;
			}
		}

		private void OnAutoExePathCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;

			if (checkBox == Form.m_BeyondCompareAutoExePathCheckBox)
			{
				SettingChanged = SettingChanged || Setting.Instance.BeyondCompareAutoExePath != Form.m_BeyondCompareAutoExePathCheckBox.Checked;
				Setting.Instance.BeyondCompareAutoExePath = Form.m_BeyondCompareAutoExePathCheckBox.Checked;
				Form.m_BeyondCompareChooseExePathTextBox.Enabled = !Setting.Instance.BeyondCompareAutoExePath;
				Form.m_BeyondCompareChooseExePathButton.Enabled = !Setting.Instance.BeyondCompareAutoExePath;
				BeyondCompare.Instance.AutoExePathToSetting();
				Form.m_BeyondCompareChooseExePathTextBox.Text = Setting.Instance.BeyondCompareExePath;
			}
			else if (checkBox == Form.m_CodeCompareAutoExePathCheckBox)
			{
				SettingChanged = SettingChanged || Setting.Instance.CodeCompareAutoExePath != Form.m_CodeCompareAutoExePathCheckBox.Checked;
				Setting.Instance.CodeCompareAutoExePath = Form.m_CodeCompareAutoExePathCheckBox.Checked;
				Form.m_CodeCompareChooseExePathTextBox.Enabled = !Setting.Instance.CodeCompareAutoExePath;
				Form.m_CodeCompareChooseExePathButton.Enabled = !Setting.Instance.CodeCompareAutoExePath;
				BeyondCompare.Instance.AutoExePathToSetting();
				Form.m_CodeCompareChooseExePathTextBox.Text = Setting.Instance.CodeCompareExePath;
			}
		}
	}
}