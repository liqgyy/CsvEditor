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
			System.Windows.Forms.Label codeCompareExePathLabel;
			System.Windows.Forms.Label codeCompareLabel;
			System.Windows.Forms.Label beyondCompareExePathLabel;
			System.Windows.Forms.Label beyondCompareLabel;
			this.m_CancelButton = new System.Windows.Forms.Button();
			this.m_OkButton = new System.Windows.Forms.Button();
			this.m_SettingItemListBox = new System.Windows.Forms.ListBox();
			this.m_SkinPanal = new System.Windows.Forms.Panel();
			this.m_DiffComparePanal = new System.Windows.Forms.Panel();
			this.m_CodeCompareAutoExePathCheckBox = new System.Windows.Forms.CheckBox();
			this.m_CodeCompareChooseExePathButton = new System.Windows.Forms.Button();
			this.m_CodeCompareChooseExePathTextBox = new System.Windows.Forms.TextBox();
			this.m_BeyondCompareChooseExePathButton = new System.Windows.Forms.Button();
			this.m_BeyondCompareChooseExePathTextBox = new System.Windows.Forms.TextBox();
			this.m_BeyondCompareAutoExePathCheckBox = new System.Windows.Forms.CheckBox();
			this.m_SkinItemsListBox = new System.Windows.Forms.ListBox();
			this.m_SkinUseCheckBox = new System.Windows.Forms.CheckBox();
			this.m_DiffCompareOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			downPanal = new System.Windows.Forms.Panel();
			m_MainSplitContainer = new System.Windows.Forms.SplitContainer();
			codeCompareExePathLabel = new System.Windows.Forms.Label();
			codeCompareLabel = new System.Windows.Forms.Label();
			beyondCompareExePathLabel = new System.Windows.Forms.Label();
			beyondCompareLabel = new System.Windows.Forms.Label();
			downPanal.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(m_MainSplitContainer)).BeginInit();
			m_MainSplitContainer.Panel1.SuspendLayout();
			m_MainSplitContainer.Panel2.SuspendLayout();
			m_MainSplitContainer.SuspendLayout();
			this.m_SkinPanal.SuspendLayout();
			this.m_DiffComparePanal.SuspendLayout();
			this.SuspendLayout();
			// 
			// downPanal
			// 
			downPanal.Controls.Add(this.m_CancelButton);
			downPanal.Controls.Add(this.m_OkButton);
			downPanal.Dock = System.Windows.Forms.DockStyle.Bottom;
			downPanal.Location = new System.Drawing.Point(0, 362);
			downPanal.Margin = new System.Windows.Forms.Padding(2);
			downPanal.Name = "downPanal";
			downPanal.Size = new System.Drawing.Size(715, 53);
			downPanal.TabIndex = 1;
			// 
			// m_CancelButton
			// 
			this.m_CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_CancelButton.Location = new System.Drawing.Point(616, 12);
			this.m_CancelButton.Margin = new System.Windows.Forms.Padding(2);
			this.m_CancelButton.Name = "m_CancelButton";
			this.m_CancelButton.Size = new System.Drawing.Size(78, 31);
			this.m_CancelButton.TabIndex = 1;
			this.m_CancelButton.Text = "取消";
			this.m_CancelButton.UseVisualStyleBackColor = true;
			this.m_CancelButton.Click += new System.EventHandler(this.OnCloseButton_Click);
			// 
			// m_OkButton
			// 
			this.m_OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_OkButton.Location = new System.Drawing.Point(526, 12);
			this.m_OkButton.Margin = new System.Windows.Forms.Padding(2);
			this.m_OkButton.Name = "m_OkButton";
			this.m_OkButton.Size = new System.Drawing.Size(76, 31);
			this.m_OkButton.TabIndex = 0;
			this.m_OkButton.Text = "确定";
			this.m_OkButton.UseVisualStyleBackColor = true;
			this.m_OkButton.Click += new System.EventHandler(this.OnCloseButton_Click);
			// 
			// m_MainSplitContainer
			// 
			m_MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			m_MainSplitContainer.Location = new System.Drawing.Point(0, 0);
			m_MainSplitContainer.Margin = new System.Windows.Forms.Padding(2);
			m_MainSplitContainer.Name = "m_MainSplitContainer";
			// 
			// m_MainSplitContainer.Panel1
			// 
			m_MainSplitContainer.Panel1.Controls.Add(this.m_SettingItemListBox);
			// 
			// m_MainSplitContainer.Panel2
			// 
			m_MainSplitContainer.Panel2.Controls.Add(this.m_SkinPanal);
			m_MainSplitContainer.Panel2.Controls.Add(this.m_DiffComparePanal);
			m_MainSplitContainer.Size = new System.Drawing.Size(715, 362);
			m_MainSplitContainer.SplitterDistance = 150;
			m_MainSplitContainer.SplitterWidth = 3;
			m_MainSplitContainer.TabIndex = 2;
			// 
			// m_SettingItemListBox
			// 
			this.m_SettingItemListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.m_SettingItemListBox.Font = new System.Drawing.Font("宋体", 9F);
			this.m_SettingItemListBox.FormattingEnabled = true;
			this.m_SettingItemListBox.ItemHeight = 12;
			this.m_SettingItemListBox.Location = new System.Drawing.Point(9, 10);
			this.m_SettingItemListBox.Margin = new System.Windows.Forms.Padding(2);
			this.m_SettingItemListBox.Name = "m_SettingItemListBox";
			this.m_SettingItemListBox.Size = new System.Drawing.Size(132, 340);
			this.m_SettingItemListBox.TabIndex = 0;
			this.m_SettingItemListBox.SelectedValueChanged += new System.EventHandler(this.OnSettingItemListBox_SelectedValueChanged);
			// 
			// m_SkinPanal
			// 
			this.m_SkinPanal.Controls.Add(this.m_SkinItemsListBox);
			this.m_SkinPanal.Controls.Add(this.m_SkinUseCheckBox);
			this.m_SkinPanal.Location = new System.Drawing.Point(0, 0);
			this.m_SkinPanal.Margin = new System.Windows.Forms.Padding(2);
			this.m_SkinPanal.Name = "m_SkinPanal";
			this.m_SkinPanal.Size = new System.Drawing.Size(0, 0);
			this.m_SkinPanal.TabIndex = 0;
			this.m_SkinPanal.Visible = false;
			// 
			// m_DiffComparePanal
			// 
			this.m_DiffComparePanal.Controls.Add(this.m_CodeCompareAutoExePathCheckBox);
			this.m_DiffComparePanal.Controls.Add(this.m_CodeCompareChooseExePathButton);
			this.m_DiffComparePanal.Controls.Add(this.m_CodeCompareChooseExePathTextBox);
			this.m_DiffComparePanal.Controls.Add(codeCompareExePathLabel);
			this.m_DiffComparePanal.Controls.Add(codeCompareLabel);
			this.m_DiffComparePanal.Controls.Add(this.m_BeyondCompareChooseExePathButton);
			this.m_DiffComparePanal.Controls.Add(this.m_BeyondCompareChooseExePathTextBox);
			this.m_DiffComparePanal.Controls.Add(beyondCompareExePathLabel);
			this.m_DiffComparePanal.Controls.Add(this.m_BeyondCompareAutoExePathCheckBox);
			this.m_DiffComparePanal.Controls.Add(beyondCompareLabel);
			this.m_DiffComparePanal.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_DiffComparePanal.Location = new System.Drawing.Point(0, 0);
			this.m_DiffComparePanal.Margin = new System.Windows.Forms.Padding(2);
			this.m_DiffComparePanal.Name = "m_DiffComparePanal";
			this.m_DiffComparePanal.Size = new System.Drawing.Size(562, 362);
			this.m_DiffComparePanal.TabIndex = 2;
			this.m_DiffComparePanal.Visible = false;
			// 
			// m_CodeCompareAutoExePathCheckBox
			// 
			this.m_CodeCompareAutoExePathCheckBox.AutoSize = true;
			this.m_CodeCompareAutoExePathCheckBox.Location = new System.Drawing.Point(482, 187);
			this.m_CodeCompareAutoExePathCheckBox.Name = "m_CodeCompareAutoExePathCheckBox";
			this.m_CodeCompareAutoExePathCheckBox.Size = new System.Drawing.Size(72, 16);
			this.m_CodeCompareAutoExePathCheckBox.TabIndex = 9;
			this.m_CodeCompareAutoExePathCheckBox.Text = "自动获取";
			this.m_CodeCompareAutoExePathCheckBox.UseVisualStyleBackColor = true;
			// 
			// m_CodeCompareChooseExePathButton
			// 
			this.m_CodeCompareChooseExePathButton.Location = new System.Drawing.Point(438, 180);
			this.m_CodeCompareChooseExePathButton.Name = "m_CodeCompareChooseExePathButton";
			this.m_CodeCompareChooseExePathButton.Size = new System.Drawing.Size(31, 29);
			this.m_CodeCompareChooseExePathButton.TabIndex = 8;
			this.m_CodeCompareChooseExePathButton.Text = "...";
			this.m_CodeCompareChooseExePathButton.UseVisualStyleBackColor = true;
			// 
			// m_CodeCompareChooseExePathTextBox
			// 
			this.m_CodeCompareChooseExePathTextBox.Location = new System.Drawing.Point(16, 185);
			this.m_CodeCompareChooseExePathTextBox.Name = "m_CodeCompareChooseExePathTextBox";
			this.m_CodeCompareChooseExePathTextBox.Size = new System.Drawing.Size(412, 21);
			this.m_CodeCompareChooseExePathTextBox.TabIndex = 7;
			// 
			// codeCompareExePathLabel
			// 
			codeCompareExePathLabel.AutoSize = true;
			codeCompareExePathLabel.Location = new System.Drawing.Point(14, 158);
			codeCompareExePathLabel.Name = "codeCompareExePathLabel";
			codeCompareExePathLabel.Size = new System.Drawing.Size(95, 12);
			codeCompareExePathLabel.TabIndex = 6;
			codeCompareExePathLabel.Text = "可执行文件路径:";
			// 
			// codeCompareLabel
			// 
			codeCompareLabel.AutoSize = true;
			codeCompareLabel.Location = new System.Drawing.Point(14, 130);
			codeCompareLabel.Name = "codeCompareLabel";
			codeCompareLabel.Size = new System.Drawing.Size(71, 12);
			codeCompareLabel.TabIndex = 5;
			codeCompareLabel.Text = "CodeCompare";
			// 
			// m_BeyondCompareChooseExePathButton
			// 
			this.m_BeyondCompareChooseExePathButton.Location = new System.Drawing.Point(438, 70);
			this.m_BeyondCompareChooseExePathButton.Name = "m_BeyondCompareChooseExePathButton";
			this.m_BeyondCompareChooseExePathButton.Size = new System.Drawing.Size(31, 29);
			this.m_BeyondCompareChooseExePathButton.TabIndex = 4;
			this.m_BeyondCompareChooseExePathButton.Text = "...";
			this.m_BeyondCompareChooseExePathButton.UseVisualStyleBackColor = true;
			// 
			// m_BeyondCompareChooseExePathTextBox
			// 
			this.m_BeyondCompareChooseExePathTextBox.Location = new System.Drawing.Point(16, 73);
			this.m_BeyondCompareChooseExePathTextBox.Name = "m_BeyondCompareChooseExePathTextBox";
			this.m_BeyondCompareChooseExePathTextBox.Size = new System.Drawing.Size(412, 21);
			this.m_BeyondCompareChooseExePathTextBox.TabIndex = 3;
			// 
			// beyondCompareExePathLabel
			// 
			beyondCompareExePathLabel.AutoSize = true;
			beyondCompareExePathLabel.Location = new System.Drawing.Point(14, 47);
			beyondCompareExePathLabel.Name = "beyondCompareExePathLabel";
			beyondCompareExePathLabel.Size = new System.Drawing.Size(95, 12);
			beyondCompareExePathLabel.TabIndex = 2;
			beyondCompareExePathLabel.Text = "可执行文件路径:";
			// 
			// m_BeyondCompareAutoExePathCheckBox
			// 
			this.m_BeyondCompareAutoExePathCheckBox.AutoSize = true;
			this.m_BeyondCompareAutoExePathCheckBox.Location = new System.Drawing.Point(482, 78);
			this.m_BeyondCompareAutoExePathCheckBox.Name = "m_BeyondCompareAutoExePathCheckBox";
			this.m_BeyondCompareAutoExePathCheckBox.Size = new System.Drawing.Size(72, 16);
			this.m_BeyondCompareAutoExePathCheckBox.TabIndex = 1;
			this.m_BeyondCompareAutoExePathCheckBox.Text = "自动获取";
			this.m_BeyondCompareAutoExePathCheckBox.UseVisualStyleBackColor = true;
			// 
			// beyondCompareLabel
			// 
			beyondCompareLabel.AutoSize = true;
			beyondCompareLabel.Location = new System.Drawing.Point(14, 20);
			beyondCompareLabel.Name = "beyondCompareLabel";
			beyondCompareLabel.Size = new System.Drawing.Size(83, 12);
			beyondCompareLabel.TabIndex = 0;
			beyondCompareLabel.Text = "BeyondCompare";
			// 
			// m_SkinItemsListBox
			// 
			this.m_SkinItemsListBox.FormattingEnabled = true;
			this.m_SkinItemsListBox.ItemHeight = 12;
			this.m_SkinItemsListBox.Location = new System.Drawing.Point(16, 34);
			this.m_SkinItemsListBox.Margin = new System.Windows.Forms.Padding(2);
			this.m_SkinItemsListBox.Name = "m_SkinItemsListBox";
			this.m_SkinItemsListBox.Size = new System.Drawing.Size(527, 316);
			this.m_SkinItemsListBox.TabIndex = 1;
			// 
			// m_SkinUseCheckBox
			// 
			this.m_SkinUseCheckBox.AutoSize = true;
			this.m_SkinUseCheckBox.Location = new System.Drawing.Point(16, 10);
			this.m_SkinUseCheckBox.Margin = new System.Windows.Forms.Padding(2);
			this.m_SkinUseCheckBox.Name = "m_SkinUseCheckBox";
			this.m_SkinUseCheckBox.Size = new System.Drawing.Size(72, 16);
			this.m_SkinUseCheckBox.TabIndex = 0;
			this.m_SkinUseCheckBox.Text = "使用皮肤";
			this.m_SkinUseCheckBox.UseVisualStyleBackColor = true;
			// 
			// m_DiffCompareOpenFileDialog
			// 
			this.m_DiffCompareOpenFileDialog.DefaultExt = "*.exe|*.exe";
			this.m_DiffCompareOpenFileDialog.FileName = "选择Exe文件";
			// 
			// SettingForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(715, 415);
			this.Controls.Add(m_MainSplitContainer);
			this.Controls.Add(downPanal);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(2);
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
			this.m_DiffComparePanal.ResumeLayout(false);
			this.m_DiffComparePanal.PerformLayout();
			this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Button m_CancelButton;
    private System.Windows.Forms.Button m_OkButton;
    private System.Windows.Forms.ListBox m_SettingItemListBox;
    private System.Windows.Forms.Panel m_SkinPanal;
    private System.Windows.Forms.ListBox m_SkinItemsListBox;
    private System.Windows.Forms.CheckBox m_SkinUseCheckBox;
	private System.Windows.Forms.Panel m_DiffComparePanal;
	private System.Windows.Forms.CheckBox m_BeyondCompareAutoExePathCheckBox;
	private System.Windows.Forms.Button m_BeyondCompareChooseExePathButton;
	private System.Windows.Forms.TextBox m_BeyondCompareChooseExePathTextBox;
	private System.Windows.Forms.CheckBox m_CodeCompareAutoExePathCheckBox;
	private System.Windows.Forms.Button m_CodeCompareChooseExePathButton;
	private System.Windows.Forms.TextBox m_CodeCompareChooseExePathTextBox;
	private System.Windows.Forms.OpenFileDialog m_DiffCompareOpenFileDialog;
}
