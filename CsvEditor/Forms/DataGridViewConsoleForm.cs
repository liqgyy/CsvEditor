using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class DataGridViewConsoleForm : Form
{
	public DataGridViewConsoleForm()
	{
		InitializeComponent();
	}

	private void UpdateListBoxSize()
	{
		Size listBoxSize = m_LogListBox.Size;
		listBoxSize.Height = m_SplitContainer.Panel1.ClientSize.Height - 36;
		m_LogListBox.Size = listBoxSize;
	}

	#region UIEvent
	private void OnSplitContainer_Panel1_ClientSizeChanged(object sender, EventArgs e)
	{
		UpdateListBoxSize();
	}
	#endregion //End UIEvent
}