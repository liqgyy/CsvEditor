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
            // m_SearchTextBox
            // 
            this.m_SearchTextBox.Location = new System.Drawing.Point(100, 22);
            this.m_SearchTextBox.Multiline = true;
            this.m_SearchTextBox.Name = "m_SearchTextBox";
            this.m_SearchTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_SearchTextBox.Size = new System.Drawing.Size(250, 75);
            this.m_SearchTextBox.TabIndex = 0;
            // 
            // replaceLabel
            // 
            replaceLabel.AutoSize = true;
            replaceLabel.Location = new System.Drawing.Point(12, 117);
            replaceLabel.Name = "replaceLabel";
            replaceLabel.Size = new System.Drawing.Size(82, 15);
            replaceLabel.TabIndex = 2;
            replaceLabel.Text = "查找内容：";
            // 
            // m_ReplaceTextBox
            // 
            this.m_ReplaceTextBox.Location = new System.Drawing.Point(100, 117);
            this.m_ReplaceTextBox.Multiline = true;
            this.m_ReplaceTextBox.Name = "m_ReplaceTextBox";
            this.m_ReplaceTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_ReplaceTextBox.Size = new System.Drawing.Size(250, 75);
            this.m_ReplaceTextBox.TabIndex = 3;
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 278);
            this.Controls.Add(this.m_ReplaceTextBox);
            this.Controls.Add(replaceLabel);
            this.Controls.Add(searchLabel);
            this.Controls.Add(this.m_SearchTextBox);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchForm";
            this.Text = "Search";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox m_SearchTextBox;
    private System.Windows.Forms.TextBox m_ReplaceTextBox;
}