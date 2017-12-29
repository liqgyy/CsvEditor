partial class MergeLocalizationForm
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
			System.Windows.Forms.Label csvPathLabel;
			this.m_OpenCsvFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.m_CsvPathTextBox = new System.Windows.Forms.TextBox();
			this.m_OpenCsvFileDialogButton = new System.Windows.Forms.Button();
			this.m_CancelButton = new System.Windows.Forms.Button();
			this.m_OkButton = new System.Windows.Forms.Button();
			csvPathLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// csvPathLabel
			// 
			csvPathLabel.AutoSize = true;
			csvPathLabel.Location = new System.Drawing.Point(13, 12);
			csvPathLabel.Name = "csvPathLabel";
			csvPathLabel.Size = new System.Drawing.Size(119, 12);
			csvPathLabel.TabIndex = 0;
			csvPathLabel.Text = "更改后Csv文件路径：";
			// 
			// m_OpenCsvFileDialog
			// 
			this.m_OpenCsvFileDialog.FileName = "openFileDialog1";
			this.m_OpenCsvFileDialog.Filter = "*.csv|*.csv";
			// 
			// m_CsvPathTextBox
			// 
			this.m_CsvPathTextBox.Location = new System.Drawing.Point(13, 29);
			this.m_CsvPathTextBox.Name = "m_CsvPathTextBox";
			this.m_CsvPathTextBox.Size = new System.Drawing.Size(447, 21);
			this.m_CsvPathTextBox.TabIndex = 1;
			this.m_CsvPathTextBox.TextChanged += new System.EventHandler(this.OnCsvPathTextBox_TextChanged);
			// 
			// m_OpenCsvFileDialogButton
			// 
			this.m_OpenCsvFileDialogButton.Location = new System.Drawing.Point(466, 24);
			this.m_OpenCsvFileDialogButton.Name = "m_OpenCsvFileDialogButton";
			this.m_OpenCsvFileDialogButton.Size = new System.Drawing.Size(32, 31);
			this.m_OpenCsvFileDialogButton.TabIndex = 2;
			this.m_OpenCsvFileDialogButton.Text = "...";
			this.m_OpenCsvFileDialogButton.UseVisualStyleBackColor = true;
			this.m_OpenCsvFileDialogButton.Click += new System.EventHandler(this.OnOpenCsvFileDialogButton_Click);
			// 
			// m_CancelButton
			// 
			this.m_CancelButton.Location = new System.Drawing.Point(424, 65);
			this.m_CancelButton.Name = "m_CancelButton";
			this.m_CancelButton.Size = new System.Drawing.Size(75, 23);
			this.m_CancelButton.TabIndex = 3;
			this.m_CancelButton.Text = "取消";
			this.m_CancelButton.UseVisualStyleBackColor = true;
			this.m_CancelButton.Click += new System.EventHandler(this.OnCancelButton_Click);
			// 
			// m_OkButton
			// 
			this.m_OkButton.Location = new System.Drawing.Point(336, 65);
			this.m_OkButton.Name = "m_OkButton";
			this.m_OkButton.Size = new System.Drawing.Size(75, 23);
			this.m_OkButton.TabIndex = 4;
			this.m_OkButton.Text = "确定";
			this.m_OkButton.UseVisualStyleBackColor = true;
			this.m_OkButton.Click += new System.EventHandler(this.OnOkButton_Click);
			// 
			// MergeLocalizationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(511, 93);
			this.Controls.Add(this.m_OkButton);
			this.Controls.Add(this.m_CancelButton);
			this.Controls.Add(this.m_OpenCsvFileDialogButton);
			this.Controls.Add(this.m_CsvPathTextBox);
			this.Controls.Add(csvPathLabel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MergeLocalizationForm";
			this.Text = "本地化合并";
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.OpenFileDialog m_OpenCsvFileDialog;
	private System.Windows.Forms.TextBox m_CsvPathTextBox;
	private System.Windows.Forms.Button m_OpenCsvFileDialogButton;
	private System.Windows.Forms.Button m_CancelButton;
	private System.Windows.Forms.Button m_OkButton;
}