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
        System.Windows.Forms.Label m_Label;
        this.textBox1 = new System.Windows.Forms.TextBox();
        m_Label = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // textBox1
        // 
        this.textBox1.Location = new System.Drawing.Point(421, 68);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(100, 25);
        this.textBox1.TabIndex = 0;
        // 
        // m_Label
        // 
        m_Label.AutoSize = true;
        m_Label.Location = new System.Drawing.Point(46, 46);
        m_Label.Name = "m_Label";
        m_Label.Size = new System.Drawing.Size(82, 15);
        m_Label.TabIndex = 1;
        m_Label.Text = "查找内容：";
        // 
        // SearchForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(553, 253);
        this.Controls.Add(m_Label);
        this.Controls.Add(this.textBox1);
        this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "SearchForm";
        this.Text = "Search";
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox textBox1;
}