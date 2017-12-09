partial class GotoForm
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
            System.Windows.Forms.Label rowLabel;
            System.Windows.Forms.Label colLabel;
            this.m_RowTextBox = new System.Windows.Forms.TextBox();
            this.m_ColTextBox = new System.Windows.Forms.TextBox();
            this.m_GotoButton = new System.Windows.Forms.Button();
            rowLabel = new System.Windows.Forms.Label();
            colLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rowLabel
            // 
            rowLabel.AutoSize = true;
            rowLabel.Location = new System.Drawing.Point(12, 15);
            rowLabel.Name = "rowLabel";
            rowLabel.Size = new System.Drawing.Size(37, 15);
            rowLabel.TabIndex = 1;
            rowLabel.Text = "行：";
            // 
            // colLabel
            // 
            colLabel.AutoSize = true;
            colLabel.Location = new System.Drawing.Point(149, 15);
            colLabel.Name = "colLabel";
            colLabel.Size = new System.Drawing.Size(37, 15);
            colLabel.TabIndex = 2;
            colLabel.Text = "列：";
            // 
            // m_RowTextBox
            // 
            this.m_RowTextBox.Location = new System.Drawing.Point(55, 9);
            this.m_RowTextBox.MaxLength = 8;
            this.m_RowTextBox.Name = "m_RowTextBox";
            this.m_RowTextBox.Size = new System.Drawing.Size(72, 25);
            this.m_RowTextBox.TabIndex = 0;
            this.m_RowTextBox.Text = "0";
            this.m_RowTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_RowTextBox.WordWrap = false;
            this.m_RowTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.m_RowTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnTextBox_KeyPress);
            // 
            // m_ColTextBox
            // 
            this.m_ColTextBox.Location = new System.Drawing.Point(192, 9);
            this.m_ColTextBox.MaxLength = 8;
            this.m_ColTextBox.Name = "m_ColTextBox";
            this.m_ColTextBox.Size = new System.Drawing.Size(72, 25);
            this.m_ColTextBox.TabIndex = 3;
            this.m_ColTextBox.Text = "0";
            this.m_ColTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_ColTextBox.WordWrap = false;
            this.m_ColTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.m_ColTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnTextBox_KeyPress);
            // 
            // m_GotoButton
            // 
            this.m_GotoButton.Location = new System.Drawing.Point(282, 5);
            this.m_GotoButton.Name = "m_GotoButton";
            this.m_GotoButton.Size = new System.Drawing.Size(82, 34);
            this.m_GotoButton.TabIndex = 4;
            this.m_GotoButton.TabStop = false;
            this.m_GotoButton.Text = "转到";
            this.m_GotoButton.UseVisualStyleBackColor = true;
            this.m_GotoButton.Click += new System.EventHandler(this.OnGotoButton_Click);
            // 
            // GotoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 46);
            this.Controls.Add(this.m_GotoButton);
            this.Controls.Add(this.m_ColTextBox);
            this.Controls.Add(colLabel);
            this.Controls.Add(rowLabel);
            this.Controls.Add(this.m_RowTextBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GotoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "转到";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox m_RowTextBox;
    private System.Windows.Forms.TextBox m_ColTextBox;
    private System.Windows.Forms.Button m_GotoButton;
}