partial class SearchForm
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
            System.Windows.Forms.Label searchLabel;
            System.Windows.Forms.Label replaceLabel;
            this.m_SearchTextBox = new System.Windows.Forms.TextBox();
            this.m_ReplaceTextBox = new System.Windows.Forms.TextBox();
            this.m_SearchNextButton = new System.Windows.Forms.Button();
            this.m_ReplaceButton = new System.Windows.Forms.Button();
            this.m_ReplaceAllButton = new System.Windows.Forms.Button();
            this.m_WildcardCheckBox = new System.Windows.Forms.CheckBox();
            this.m_CaseCheckBox = new System.Windows.Forms.CheckBox();
            searchLabel = new System.Windows.Forms.Label();
            replaceLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // searchLabel
            // 
            searchLabel.AutoSize = true;
            searchLabel.Location = new System.Drawing.Point(12, 22);
            searchLabel.Name = "searchLabel";
            searchLabel.Size = new System.Drawing.Size(82, 15);
            searchLabel.TabIndex = 1;
            searchLabel.Text = "查找内容：";
            // 
            // replaceLabel
            // 
            replaceLabel.AutoSize = true;
            replaceLabel.Location = new System.Drawing.Point(27, 117);
            replaceLabel.Name = "replaceLabel";
            replaceLabel.Size = new System.Drawing.Size(67, 15);
            replaceLabel.TabIndex = 2;
            replaceLabel.Text = "替换为：";
            // 
            // m_SearchTextBox
            // 
            this.m_SearchTextBox.Location = new System.Drawing.Point(100, 22);
            this.m_SearchTextBox.Multiline = true;
            this.m_SearchTextBox.Name = "m_SearchTextBox";
            this.m_SearchTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_SearchTextBox.Size = new System.Drawing.Size(250, 75);
            this.m_SearchTextBox.TabIndex = 0;
            this.m_SearchTextBox.TextChanged += new System.EventHandler(this.OnValueChanged);
            // 
            // m_ReplaceTextBox
            // 
            this.m_ReplaceTextBox.Location = new System.Drawing.Point(100, 117);
            this.m_ReplaceTextBox.Multiline = true;
            this.m_ReplaceTextBox.Name = "m_ReplaceTextBox";
            this.m_ReplaceTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_ReplaceTextBox.Size = new System.Drawing.Size(250, 75);
            this.m_ReplaceTextBox.TabIndex = 3;
            this.m_ReplaceTextBox.TextChanged += new System.EventHandler(this.OnValueChanged);
            // 
            // m_SearchNextButton
            // 
            this.m_SearchNextButton.Location = new System.Drawing.Point(376, 22);
            this.m_SearchNextButton.Name = "m_SearchNextButton";
            this.m_SearchNextButton.Size = new System.Drawing.Size(111, 31);
            this.m_SearchNextButton.TabIndex = 4;
            this.m_SearchNextButton.TabStop = false;
            this.m_SearchNextButton.Text = "查找下一个";
            this.m_SearchNextButton.UseVisualStyleBackColor = true;
            this.m_SearchNextButton.Click += new System.EventHandler(this.OnSearchNextButton_Click);
            // 
            // m_ReplaceButton
            // 
            this.m_ReplaceButton.Location = new System.Drawing.Point(376, 117);
            this.m_ReplaceButton.Name = "m_ReplaceButton";
            this.m_ReplaceButton.Size = new System.Drawing.Size(111, 31);
            this.m_ReplaceButton.TabIndex = 5;
            this.m_ReplaceButton.TabStop = false;
            this.m_ReplaceButton.Text = "替换";
            this.m_ReplaceButton.UseVisualStyleBackColor = true;
            this.m_ReplaceButton.Click += new System.EventHandler(this.OnReplaceButton_Click);
            // 
            // m_ReplaceAllButton
            // 
            this.m_ReplaceAllButton.Location = new System.Drawing.Point(376, 161);
            this.m_ReplaceAllButton.Name = "m_ReplaceAllButton";
            this.m_ReplaceAllButton.Size = new System.Drawing.Size(111, 31);
            this.m_ReplaceAllButton.TabIndex = 6;
            this.m_ReplaceAllButton.TabStop = false;
            this.m_ReplaceAllButton.Text = "全部替换";
            this.m_ReplaceAllButton.UseVisualStyleBackColor = true;
            this.m_ReplaceAllButton.Click += new System.EventHandler(this.OnReplaceAllButton_Click);
            // 
            // m_WildcardCheckBox
            // 
            this.m_WildcardCheckBox.AutoSize = true;
            this.m_WildcardCheckBox.Location = new System.Drawing.Point(15, 214);
            this.m_WildcardCheckBox.Name = "m_WildcardCheckBox";
            this.m_WildcardCheckBox.Size = new System.Drawing.Size(104, 19);
            this.m_WildcardCheckBox.TabIndex = 7;
            this.m_WildcardCheckBox.TabStop = false;
            this.m_WildcardCheckBox.Text = "使用通配符";
            this.m_WildcardCheckBox.UseVisualStyleBackColor = true;
            this.m_WildcardCheckBox.CheckedChanged += new System.EventHandler(this.OnValueChanged);
            // 
            // m_CaseCheckBox
            // 
            this.m_CaseCheckBox.AutoSize = true;
            this.m_CaseCheckBox.Location = new System.Drawing.Point(161, 214);
            this.m_CaseCheckBox.Name = "m_CaseCheckBox";
            this.m_CaseCheckBox.Size = new System.Drawing.Size(104, 19);
            this.m_CaseCheckBox.TabIndex = 8;
            this.m_CaseCheckBox.TabStop = false;
            this.m_CaseCheckBox.Text = "区分大小写";
            this.m_CaseCheckBox.UseVisualStyleBackColor = true;
            this.m_CaseCheckBox.CheckedChanged += new System.EventHandler(this.OnValueChanged);
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 256);
            this.Controls.Add(this.m_CaseCheckBox);
            this.Controls.Add(this.m_WildcardCheckBox);
            this.Controls.Add(this.m_ReplaceAllButton);
            this.Controls.Add(this.m_ReplaceButton);
            this.Controls.Add(this.m_SearchNextButton);
            this.Controls.Add(this.m_ReplaceTextBox);
            this.Controls.Add(replaceLabel);
            this.Controls.Add(searchLabel);
            this.Controls.Add(this.m_SearchTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查找和替换";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox m_SearchTextBox;
    private System.Windows.Forms.TextBox m_ReplaceTextBox;
    private System.Windows.Forms.Button m_SearchNextButton;
    private System.Windows.Forms.Button m_ReplaceButton;
    private System.Windows.Forms.Button m_ReplaceAllButton;
    private System.Windows.Forms.CheckBox m_WildcardCheckBox;
    private System.Windows.Forms.CheckBox m_CaseCheckBox;
}