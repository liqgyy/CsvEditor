partial class LayoutNameForm
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
			System.Windows.Forms.Label layoutLabel;
			this.m_OkButton = new System.Windows.Forms.Button();
			this.m_CancelButton = new System.Windows.Forms.Button();
			this.m_LayoutTextBox = new System.Windows.Forms.TextBox();
			layoutLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// layoutLabel
			// 
			layoutLabel.AutoSize = true;
			layoutLabel.Location = new System.Drawing.Point(13, 9);
			layoutLabel.Name = "layoutLabel";
			layoutLabel.Size = new System.Drawing.Size(59, 12);
			layoutLabel.TabIndex = 0;
			layoutLabel.Text = "布局名称:";
			// 
			// m_OkButton
			// 
			this.m_OkButton.Location = new System.Drawing.Point(105, 59);
			this.m_OkButton.Name = "m_OkButton";
			this.m_OkButton.Size = new System.Drawing.Size(75, 23);
			this.m_OkButton.TabIndex = 2;
			this.m_OkButton.Text = "确定";
			this.m_OkButton.UseVisualStyleBackColor = true;
			this.m_OkButton.Click += new System.EventHandler(this.OnOkButton_Click);
			// 
			// m_CancelButton
			// 
			this.m_CancelButton.Location = new System.Drawing.Point(186, 59);
			this.m_CancelButton.Name = "m_CancelButton";
			this.m_CancelButton.Size = new System.Drawing.Size(75, 23);
			this.m_CancelButton.TabIndex = 3;
			this.m_CancelButton.Text = "取消";
			this.m_CancelButton.UseVisualStyleBackColor = true;
			this.m_CancelButton.Click += new System.EventHandler(this.OnCancelButton_Click);
			// 
			// m_LayoutTextBox
			// 
			this.m_LayoutTextBox.Location = new System.Drawing.Point(15, 28);
			this.m_LayoutTextBox.Name = "m_LayoutTextBox";
			this.m_LayoutTextBox.Size = new System.Drawing.Size(246, 21);
			this.m_LayoutTextBox.TabIndex = 1;
			this.m_LayoutTextBox.Text = "New Layout";
			this.m_LayoutTextBox.TextChanged += new System.EventHandler(this.OnLayoutTextBox_TextChanged);
			// 
			// SaveLayoutForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(273, 87);
			this.Controls.Add(this.m_CancelButton);
			this.Controls.Add(this.m_OkButton);
			this.Controls.Add(this.m_LayoutTextBox);
			this.Controls.Add(layoutLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SaveLayoutForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "保存布局";
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.TextBox m_LayoutTextBox;
	private System.Windows.Forms.Button m_OkButton;
	private System.Windows.Forms.Button m_CancelButton;
}