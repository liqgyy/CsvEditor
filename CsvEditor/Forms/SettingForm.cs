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
		m_SettingItems[(int)SettingItemType.Skin] = new SettingForm_Skin(this, "皮肤", m_SkinPanal);

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
        if(button == m_CancelButton)
        {
            if (ShowCloseFormMessageBox() == DialogResult.No)
            {
                return;
            }
            Setting.Load();
        }
        else if (button == m_OkButton)
        {
            Setting.Save();
        }
        for (int itemIdx = 0; itemIdx < m_SettingItems.Length; itemIdx++)
        {
            m_SettingItems[itemIdx].Close();
        }
        Dispose();
    }

    private void OnForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if(ShowCloseFormMessageBox() == DialogResult.No)
        {
            e.Cancel = true;
			return;
        }
		for (int itemIdx = 0; itemIdx < m_SettingItems.Length; itemIdx++)
		{
			m_SettingItems[itemIdx].Close();
		}
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
		DiffCompare,
		Skin = 0,
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

    public class SettingForm_Skin : SettingItem
    {
        public SettingForm_Skin(SettingForm settingForm, string text, Panel mainPanel) : base(settingForm, text, mainPanel) { }

        protected override void OnLoad()
        {
            Form.m_SkinUseCheckBox.CheckedChanged += OnSkinUseCheckBox_CheckedChanged;
            Form.m_SkinItemsListBox.SelectedIndexChanged += OnSkinItemsListBox_SelectedIndexChanged;

            Form.m_SkinUseCheckBox.Checked = Setting.Instance.UseSkin;

            string[] skins = SkinUtility.GetAllSkinSskName();
            Form.m_SkinItemsListBox.Items.Clear();
            for (int skinIdx = 0; skinIdx < skins.Length; skinIdx++)
            {
                Form.m_SkinItemsListBox.Items.Add(skins[skinIdx]);
                if (skins[skinIdx] == Setting.Instance.CurrentSkin)
                {
                    Form.m_SkinItemsListBox.SelectedIndex = skinIdx;
                }
            }
        }

        protected override void OnClose()
        {
            if (!SettingChanged)
            {
                return;
            }
            SkinUtility.SetSkin();
        }

        private void OnSkinUseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SettingChanged = SettingChanged || Setting.Instance.UseSkin != Form.m_SkinUseCheckBox.Checked;
            Setting.Instance.UseSkin = Form.m_SkinUseCheckBox.Checked;
            Form.m_SkinUseCheckBox.Enabled = Setting.Instance.UseSkin;
        }

        private void OnSkinItemsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string skin = (string)Form.m_SkinItemsListBox.SelectedItem;
            SettingChanged = SettingChanged || Setting.Instance.CurrentSkin != skin;
            Setting.Instance.CurrentSkin = skin;
            SkinUtility.SetSkin();
        }
    }

	public class SettingForm_DiffCompare : SettingItem
	{
		public SettingForm_DiffCompare(SettingForm settingForm, string text, Panel mainPanel) : base(settingForm, text, mainPanel){}


	}
}