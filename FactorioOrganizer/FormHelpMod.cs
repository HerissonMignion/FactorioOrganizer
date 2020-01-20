using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactorioOrganizer
{
	public partial class FormHelpMod : Form
	{
		public FormHelpMod()
		{
			InitializeComponent();
		}

		private void FormHelpMod_Load(object sender, EventArgs e)
		{
			this.MainTextBox.Select(0, 0);
		}
	}
}
