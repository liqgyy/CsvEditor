partial class SettingForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            System.Windows.Forms.Panel downPanal;
            System.Windows.Forms.SplitContainer m_MainSplitContainer;
            this.m_CancelButton = new System.Windows.Forms.Button();
            this.m_OkButton = new System.Windows.Forms.Button();
            this.m_SettingItemListBox = new System.Windows.Forms.ListBox();
            this.m_SkinPanal = new System.Windows.Forms.Panel();
            this.m_SkinItemsListBox = new System.Windows.Forms.ListBox();
            this.m_SkinUseCheckBox = new System.Windows.Forms.CheckBox();
            downPanal = new System.Windows.Forms.Panel();
            m_MainSplitContainer = new System.Windows.Forms.SplitContainer();
            downPanal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(m_MainSplitContainer)).BeginInit();
            m_MainSplitContainer.Panel1.SuspendLayout();
            m_MainSplitContainer.Panel2.SuspendLayout();
            m_MainSplitContainer.SuspendLayout();
            this.m_SkinPanal.SuspendLayout();
            this.SuspendLayout();
            // 
            // downPanal
            // 
            downPanal.Controls.Add(this.m_CancelButton);
            downPanal.Controls.Add(this.m_OkButton);
            downPanal.Dock = System.Windows.Forms.DockStyle.Bottom;
            downPanal.Location = new System.Drawing.Point(0, 453);
            downPanal.Name = "downPanal";
            downPanal.Size = new System.Drawing.Size(953, 66);
            downPanal.TabIndex = 1;
            // 
            // m_CancelButton
            // 
            this.m_CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_CancelButton.Location = new System.Drawing.Point(822, 15);
            this.m_CancelButton.Name = "m_CancelButton";
            this.m_CancelButton.Size = new System.Drawing.Size(104, 39);
            this.m_CancelButton.TabIndex = 1;
            this.m_CancelButton.Text = "取消";
            this.m_CancelButton.UseVisualStyleBackColor = true;
            this.m_CancelButton.Click += new System.EventHandler(this.OnCloseButton_Click);
            // 
            // m_OkButton
            // 
            this.m_OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_OkButton.Location = new System.Drawing.Point(702, 15);
            this.m_OkButton.Name = "m_OkButton";
            this.m_OkButton.Size = new System.Drawing.Size(102, 39);
            this.m_OkButton.TabIndex = 0;
            this.m_OkButton.Text = "确定";
            this.m_OkButton.UseVisualStyleBackColor = true;
            this.m_OkButton.Click += new System.EventHandler(this.OnCloseButton_Click);
            // 
            // m_MainSplitContainer
            // 
            m_MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            m_MainSplitContainer.Location = new System.Drawing.Point(0, 0);
            m_MainSplitContainer.Name = "m_MainSplitContainer";
            // 
            // m_MainSplitContainer.Panel1
            // 
            m_MainSplitContainer.Panel1.Controls.Add(this.m_SettingItemListBox);
            // 
            // m_MainSplitContainer.Panel2
            // 
            m_MainSplitContainer.Panel2.Controls.Add(this.m_SkinPanal);
            m_MainSplitContainer.Size = new System.Drawing.Size(953, 453);
            m_MainSplitContainer.SplitterDistance = 200;
            m_MainSplitContainer.TabIndex = 2;
            // 
            // m_SettingItemListBox
            // 
            this.m_SettingItemListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.m_SettingItemListBox.Font = new System.Drawing.Font("宋体", 9F);
            this.m_SettingItemListBox.FormattingEnabled = true;
            this.m_SettingItemListBox.ItemHeight = 15;
            this.m_SettingItemListBox.Location = new System.Drawing.Point(12, 13);
            this.m_SettingItemListBox.Name = "m_SettingItemListBox";
            this.m_SettingItemListBox.Size = new System.Drawing.Size(174, 424);
            this.m_SettingItemListBox.TabIndex = 0;
            this.m_SettingItemListBox.SelectedValueChanged += new System.EventHandler(this.OnSettingItemListBox_SelectedValueChanged);
            // 
            // m_SkinPanal
            // 
            this.m_SkinPanal.Controls.Add(this.m_SkinItemsListBox);
            this.m_SkinPanal.Controls.Add(this.m_SkinUseCheckBox);
            this.m_SkinPanal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_SkinPanal.Location = new System.Drawing.Point(0, 0);
            this.m_SkinPanal.Name = "m_SkinPanal";
            this.m_SkinPanal.Size = new System.Drawing.Size(749, 453);
            this.m_SkinPanal.TabIndex = 0;
            this.m_SkinPanal.Visible = false;
            // 
            // m_SkinItemsListBox
            // 
            this.m_SkinItemsListBox.FormattingEnabled = true;
            this.m_SkinItemsListBox.ItemHeight = 15;
            this.m_SkinItemsListBox.Location = new System.Drawing.Point(21, 43);
            this.m_SkinItemsListBox.Name = "m_SkinItemsListBox";
            this.m_SkinItemsListBox.Size = new System.Drawing.Size(701, 394);
            this.m_SkinItemsListBox.TabIndex = 1;
            // 
            // m_SkinUseCheckBox
            // 
            this.m_SkinUseCheckBox.AutoSize = true;
            this.m_SkinUseCheckBox.Location = new System.Drawing.Point(21, 13);
            this.m_SkinUseCheckBox.Name = "m_SkinUseCheckBox";
            this.m_SkinUseCheckBox.Size = new System.Drawing.Size(89, 19);
            this.m_SkinUseCheckBox.TabIndex = 0;
            this.m_SkinUseCheckBox.Text = "使用皮肤";
            this.m_SkinUseCheckBox.UseVisualStyleBackColor = true;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 519);
            this.Controls.Add(m_MainSplitContainer);
            this.Controls.Add(downPanal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "首选项";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnForm_FormClosing);
            downPanal.ResumeLayout(false);
            m_MainSplitContainer.Panel1.ResumeLayout(false);
            m_MainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(m_MainSplitContainer)).EndInit();
            m_MainSplitContainer.ResumeLayout(false);
            this.m_SkinPanal.ResumeLayout(false);
            this.m_SkinPanal.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Button m_CancelButton;
    private System.Windows.Forms.Button m_OkButton;
    private System.Windows.Forms.ListBox m_SettingItemListBox;
    private System.Windows.Forms.Panel m_SkinPanal;
    private System.Windows.Forms.ListBox m_SkinItemsListBox;
    private System.Windows.Forms.CheckBox m_SkinUseCheckBox;
}
