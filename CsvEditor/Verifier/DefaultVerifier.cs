using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

class DefaultVerifier : BaseVerifier
{
	public override string GetDisplayName()
	{
		return "默认";
	}

	public override string GetName()
	{
		return "Default";
	}

	public override bool Verify(DataGridView dataGridView)
	{
		return true;
	}
}