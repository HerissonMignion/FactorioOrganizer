using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactorioOrganizer.Dialogs
{
	public partial class DlgGetIndex : Form
	{


		public string Title
		{
			get { return this.Text; }
			set { this.Text = value; }
		}




		public int Answer
		{
			get { return (int)(this.nudAnswer.Value); }
			set
			{
				this.nudAnswer.Value = (decimal)value;
			}
		}

		public int MinAnswer
		{
			get { return (int)(this.nudAnswer.Minimum); }
			set { this.nudAnswer.Minimum = (decimal)value; }
		}
		public int MaxAnswer
		{
			get { return (int)(this.nudAnswer.Maximum); }
			set { this.nudAnswer.Maximum = (decimal)value; }
		}




		public DlgGetIndex()
		{
			InitializeComponent();
		}
		private void DlgGetIndex_Load(object sender, EventArgs e)
		{


			//we pop the form centered to the mouse
			this.StartPosition = FormStartPosition.Manual;
			Point newpos = new Point(Cursor.Position.X - (this.Width / 2), Cursor.Position.Y - (this.Height / 2));
			if (newpos.X < 0) { newpos.X = 0; }
			if (newpos.Y < 0) { newpos.Y = 0; }
			this.Location = newpos;


		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}





	}
}
