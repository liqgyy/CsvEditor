﻿partial class DataGridViewConsoleForm
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
			this.m_SplitContainer = new System.Windows.Forms.SplitContainer();
			this.m_LogListBox = new System.Windows.Forms.ListBox();
			this.m_InfoCheckBox = new System.Windows.Forms.CheckBox();
			this.m_WarningCheckBox = new System.Windows.Forms.CheckBox();
			this.m_ErrorCheckBox = new System.Windows.Forms.CheckBox();
			this.m_DetailTextBox = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.m_SplitContainer)).BeginInit();
			this.m_SplitContainer.Panel1.SuspendLayout();
			this.m_SplitContainer.Panel2.SuspendLayout();
			this.m_SplitContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_SplitContainer
			// 
			this.m_SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_SplitContainer.Location = new System.Drawing.Point(0, 0);
			this.m_SplitContainer.Name = "m_SplitContainer";
			this.m_SplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// m_SplitContainer.Panel1
			// 
			this.m_SplitContainer.Panel1.Controls.Add(this.m_LogListBox);
			this.m_SplitContainer.Panel1.Controls.Add(this.m_InfoCheckBox);
			this.m_SplitContainer.Panel1.Controls.Add(this.m_WarningCheckBox);
			this.m_SplitContainer.Panel1.Controls.Add(this.m_ErrorCheckBox);
			this.m_SplitContainer.Panel1.ClientSizeChanged += new System.EventHandler(this.OnSplitContainer_Panel1_ClientSizeChanged);
			// 
			// m_SplitContainer.Panel2
			// 
			this.m_SplitContainer.Panel2.Controls.Add(this.m_DetailTextBox);
			this.m_SplitContainer.Size = new System.Drawing.Size(926, 585);
			this.m_SplitContainer.SplitterDistance = 387;
			this.m_SplitContainer.TabIndex = 0;
			// 
			// m_LogListBox
			// 
			this.m_LogListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.m_LogListBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.m_LogListBox.FormattingEnabled = true;
			this.m_LogListBox.ItemHeight = 12;
			this.m_LogListBox.Location = new System.Drawing.Point(0, 37);
			this.m_LogListBox.Name = "m_LogListBox";
			this.m_LogListBox.Size = new System.Drawing.Size(926, 350);
			this.m_LogListBox.TabIndex = 3;
			// 
			// m_InfoCheckBox
			// 
			this.m_InfoCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.m_InfoCheckBox.AutoSize = true;
			this.m_InfoCheckBox.Location = new System.Drawing.Point(639, 12);
			this.m_InfoCheckBox.Name = "m_InfoCheckBox";
			this.m_InfoCheckBox.Size = new System.Drawing.Size(48, 16);
			this.m_InfoCheckBox.TabIndex = 2;
			this.m_InfoCheckBox.Text = "信息";
			this.m_InfoCheckBox.UseVisualStyleBackColor = true;
			// 
			// m_WarningCheckBox
			// 
			this.m_WarningCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.m_WarningCheckBox.AutoSize = true;
			this.m_WarningCheckBox.Location = new System.Drawing.Point(736, 12);
			this.m_WarningCheckBox.Name = "m_WarningCheckBox";
			this.m_WarningCheckBox.Size = new System.Drawing.Size(48, 16);
			this.m_WarningCheckBox.TabIndex = 1;
			this.m_WarningCheckBox.Text = "警告";
			this.m_WarningCheckBox.UseVisualStyleBackColor = true;
			// 
			// m_ErrorCheckBox
			// 
			this.m_ErrorCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.m_ErrorCheckBox.AutoSize = true;
			this.m_ErrorCheckBox.Location = new System.Drawing.Point(829, 12);
			this.m_ErrorCheckBox.Name = "m_ErrorCheckBox";
			this.m_ErrorCheckBox.Size = new System.Drawing.Size(48, 16);
			this.m_ErrorCheckBox.TabIndex = 0;
			this.m_ErrorCheckBox.Text = "错误";
			this.m_ErrorCheckBox.UseVisualStyleBackColor = true;
			// 
			// m_DetailTextBox
			// 
			this.m_DetailTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_DetailTextBox.Location = new System.Drawing.Point(0, 0);
			this.m_DetailTextBox.Multiline = true;
			this.m_DetailTextBox.Name = "m_DetailTextBox";
			this.m_DetailTextBox.Size = new System.Drawing.Size(926, 194);
			this.m_DetailTextBox.TabIndex = 0;
			// 
			// DataGridViewConsoleForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(926, 585);
			this.Controls.Add(this.m_SplitContainer);
			this.Name = "DataGridViewConsoleForm";
			this.Text = "控制台";
			this.m_SplitContainer.Panel1.ResumeLayout(false);
			this.m_SplitContainer.Panel1.PerformLayout();
			this.m_SplitContainer.Panel2.ResumeLayout(false);
			this.m_SplitContainer.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_SplitContainer)).EndInit();
			this.m_SplitContainer.ResumeLayout(false);
			this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.TextBox m_DetailTextBox;
	private System.Windows.Forms.ListBox m_LogListBox;
	private System.Windows.Forms.CheckBox m_InfoCheckBox;
	private System.Windows.Forms.CheckBox m_WarningCheckBox;
	private System.Windows.Forms.CheckBox m_ErrorCheckBox;
	private System.Windows.Forms.SplitContainer m_SplitContainer;
}