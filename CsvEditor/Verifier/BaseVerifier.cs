using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public abstract class BaseVerifier
{
	public abstract string GetName();
	public abstract string GetDisplayName();

	public abstract bool Verify(DataGridView dataGridView);
}