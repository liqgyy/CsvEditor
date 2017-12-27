partial class LayoutManagerForm
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
			System.Windows.Forms.Label savedLayoutLabel;
			this.m_SavedLayoutListBox = new System.Windows.Forms.ListBox();
			this.m_RenameButton = new System.Windows.Forms.Button();
			this.m_DeleteButton = new System.Windows.Forms.Button();
			this.m_CloseButton = new System.Windows.Forms.Button();
			this.m_UpButton = new System.Windows.Forms.Button();
			this.m_DownButton = new System.Windows.Forms.Button();
			savedLayoutLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// savedLayoutLabel
			// 
			savedLayoutLabel.AutoSize = true;
			savedLayoutLabel.Location = new System.Drawing.Point(13, 13);
			savedLayoutLabel.Name = "savedLayoutLabel";
			savedLayoutLabel.Size = new System.Drawing.Size(89, 12);
			savedLayoutLabel.TabIndex = 0;
			savedLayoutLabel.Text = "已保存的布局：";
			// 
			// m_SavedLayoutListBox
			// 
			this.m_SavedLayoutListBox.FormattingEnabled = true;
			this.m_SavedLayoutListBox.ItemHeight = 12;
			this.m_SavedLayoutListBox.Location = new System.Drawing.Point(15, 33);
			this.m_SavedLayoutListBox.Name = "m_SavedLayoutListBox";
			this.m_SavedLayoutListBox.Size = new System.Drawing.Size(291, 316);
			this.m_SavedLayoutListBox.TabIndex = 1;
			this.m_SavedLayoutListBox.SelectedValueChanged += new System.EventHandler(this.OnSavedLayoutListBox_SelectedValueChanged);
			// 
			// m_RenameButton
			// 
			this.m_RenameButton.Location = new System.Drawing.Point(13, 360);
			this.m_RenameButton.Name = "m_RenameButton";
			this.m_RenameButton.Size = new System.Drawing.Size(75, 23);
			this.m_RenameButton.TabIndex = 2;
			this.m_RenameButton.Text = "重命名";
			this.m_RenameButton.UseVisualStyleBackColor = true;
			this.m_RenameButton.Click += new System.EventHandler(this.OnRenameButton_Click);
			// 
			// m_DeleteButton
			// 
			this.m_DeleteButton.Location = new System.Drawing.Point(94, 360);
			this.m_DeleteButton.Name = "m_DeleteButton";
			this.m_DeleteButton.Size = new System.Drawing.Size(75, 23);
			this.m_DeleteButton.TabIndex = 3;
			this.m_DeleteButton.Text = "删除";
			this.m_DeleteButton.UseVisualStyleBackColor = true;
			this.m_DeleteButton.Click += new System.EventHandler(this.OnDeleteButton_Click);
			// 
			// m_CloseButton
			// 
			this.m_CloseButton.Location = new System.Drawing.Point(251, 360);
			this.m_CloseButton.Name = "m_CloseButton";
			this.m_CloseButton.Size = new System.Drawing.Size(75, 23);
			this.m_CloseButton.TabIndex = 4;
			this.m_CloseButton.Text = "关闭";
			this.m_CloseButton.UseVisualStyleBackColor = true;
			this.m_CloseButton.Click += new System.EventHandler(this.OnCloseButton_Click);
			// 
			// m_UpButton
			// 
			this.m_UpButton.Location = new System.Drawing.Point(312, 33);
			this.m_UpButton.Name = "m_UpButton";
			this.m_UpButton.Size = new System.Drawing.Size(25, 31);
			this.m_UpButton.TabIndex = 5;
			this.m_UpButton.Text = "↑";
			this.m_UpButton.UseVisualStyleBackColor = true;
			this.m_UpButton.Click += new System.EventHandler(this.OnRearrangeButton_Click);
			// 
			// m_DownButton
			// 
			this.m_DownButton.Location = new System.Drawing.Point(312, 70);
			this.m_DownButton.Name = "m_DownButton";
			this.m_DownButton.Size = new System.Drawing.Size(25, 31);
			this.m_DownButton.TabIndex = 6;
			this.m_DownButton.Text = "↓";
			this.m_DownButton.UseVisualStyleBackColor = true;
			this.m_DownButton.Click += new System.EventHandler(this.OnRearrangeButton_Click);
			// 
			// LayoutManagerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(342, 393);
			this.Controls.Add(this.m_DownButton);
			this.Controls.Add(this.m_UpButton);
			this.Controls.Add(this.m_CloseButton);
			this.Controls.Add(this.m_DeleteButton);
			this.Controls.Add(this.m_RenameButton);
			this.Controls.Add(this.m_SavedLayoutListBox);
			this.Controls.Add(savedLayoutLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LayoutManagerForm";
			this.Text = "管理布局";
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion
	private System.Windows.Forms.ListBox m_SavedLayoutListBox;
	private System.Windows.Forms.Button m_RenameButton;
	private System.Windows.Forms.Button m_DeleteButton;
	private System.Windows.Forms.Button m_CloseButton;
	private System.Windows.Forms.Button m_UpButton;
	private System.Windows.Forms.Button m_DownButton;
}
