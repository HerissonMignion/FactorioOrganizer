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
	public partial class DlgGetText : Form
	{


		public string Title
		{
			get { return this.Text; }
			set { this.Text = value; }
		}
		public string Message
		{
			get { return this.LabelMessage.Text; }
			set { this.LabelMessage.Text = value; }
		}


		public string Answer
		{
			get
			{
				return this.tbAnswer.Text;
			}
			set
			{
				this.tbAnswer.Text = value;
			}
		}
		private string zzzInitialAnswer = ""; //on form load, this.Answer is put into this.






		public DlgGetText()
		{
			InitializeComponent();
			this.tbAnswer.KeyDown += new KeyEventHandler(this.tbAnswer_KeyDown);
		}
		private void DlgGetText_Load(object sender, EventArgs e)
		{
			this.zzzInitialAnswer = this.Answer;


			//we pop the form centered to the mouse
			this.StartPosition = FormStartPosition.Manual;
			Point newpos = new Point(Cursor.Position.X - (this.Width / 2), Cursor.Position.Y - (this.Height / 2));
			if (newpos.X < 0) { newpos.X = 0; }
			if (newpos.Y < 0) { newpos.Y = 0; }
			this.Location = newpos;


			this.RefreshEnabled();
		}

		private void tbAnswer_TextChanged(object sender, EventArgs e)
		{
			this.RefreshEnabled();

			//change the color of the text box for red if it's a not valid answer
			if (!this.IsValidAnswer() && this.tbAnswer.Text.Length > 0 && this.tbAnswer.Text != this.zzzInitialAnswer)
			{
				this.tbAnswer.BackColor = Color.Crimson;
				this.tbAnswer.ForeColor = Color.White;
			}
			else
			{
				this.tbAnswer.BackColor = Color.White;
				this.tbAnswer.ForeColor = Color.Black;
			}

		}
		private void tbAnswer_KeyDown(object sender, KeyEventArgs e)
		{
			//if the user press enter, it's a if he pressed the ok button
			if (e.KeyCode == Keys.Return)
			{
				//we simulate the click on the ok button only if it is enabled
				if (this.btnOk.Enabled)
				{
					//call the void
					this.btnOk_Click(this, new EventArgs());
				}
			}
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





		public void RefreshEnabled()
		{
			this.btnOk.Enabled = this.IsValidAnswer();

			
		}





		//before calling .show dialog, we can put in this list every invalid answer
		public List<string> listInvalidAnswer = new List<string>();
		public bool IsValidAnswer()
		{
			string actualanswer = this.tbAnswer.Text;

			//make sure it's not empty
			if (actualanswer.Length <= 0) { return false; }

			//check if it's inside the list of forbiden answer
			foreach (string str in this.listInvalidAnswer)
			{
				if (str == actualanswer)
				{
					return false;
				}
			}

			//if everything is ok, we return true
			return true;
		}





	}
}
