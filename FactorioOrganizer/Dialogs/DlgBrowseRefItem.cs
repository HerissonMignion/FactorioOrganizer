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

	//this dialog is for the user to specifies an item, inside or outside the mod file.

	/*
	 * how to use:
	 * 
	 * call show dialog.
	 * 
	 * get the properties ItemName and ItemModName.
	 * 
	 * 
	 */

	public partial class DlgBrowseRefItem : Form
	{
		public oMod TheMod;



		public string Title
		{
			get { return this.Text; }
			set { this.Text = value; }
		}


		public string ItemName = "";
		public string ItemModName = "";






		public DlgBrowseRefItem(oMod StartMod)
		{
			this.TheMod = StartMod;

			InitializeComponent();
		}
		private void DlgBrowseRefItem_Load(object sender, EventArgs e)
		{

			//we pop the form centered to the mouse
			this.StartPosition = FormStartPosition.Manual;
			Point newpos = new Point(Cursor.Position.X - (this.Width / 2), Cursor.Position.Y - (this.Height / 2));
			if (newpos.X < 0) { newpos.X = 0; }
			if (newpos.Y < 0) { newpos.Y = 0; }
			this.Location = newpos;



			this.LoadModItems();

			this.RefreshEnabled();
		}
		
		private void rbExternalModItem_CheckedChanged(object sender, EventArgs e)
		{
			this.RefreshEnabled();
		}
		private void rbInternalModItem_CheckedChanged(object sender, EventArgs e)
		{
			this.RefreshEnabled();
		}

		private void tbItemName_TextChanged(object sender, EventArgs e)
		{
			this.RefreshEnabled();
		}
		private void tbModName_TextChanged(object sender, EventArgs e)
		{
			this.RefreshEnabled();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			this.ItemName = this.tbItemName.Text;
			this.ItemModName = this.tbModName.Text;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}





		private bool IsValidItemName()
		{
			if (this.tbItemName.Text.Length <= 0) { return false; }

			return true;
		}
		private bool IsValidModName()
		{
			if (this.tbModName.Text.Length <= 0) { return false; }

			return true;
		}


		private void RefreshEnabled()
		{
			//external item
			bool isExternalItem = this.rbExternalModItem.Checked;

			this.tbItemName.Enabled = isExternalItem;
			this.tbModName.Enabled = isExternalItem;

			this.btnOk.Enabled = isExternalItem && this.IsValidItemName() && this.IsValidModName();
			this.btnCancel.Enabled = true; //isExternalItem;


			//internal item
			bool isInternalItem = this.rbInternalModItem.Checked;

			this.flpInternalItems.Enabled = isInternalItem;



		}






		//this void create a graphical button that contain a refItem for every ModItem of the mod
		private void LoadModItems()
		{
			foreach (oMod.ModItem mi in this.TheMod.listItems)
			{
				this.MakeButtonForModItem(mi);
			}
		}
		//make a single button for a given ModItem
		private void MakeButtonForModItem(oMod.ModItem mi)
		{
			Button newb = new Button();
			newb.TextAlign = ContentAlignment.MiddleLeft;
			newb.ImageAlign = ContentAlignment.MiddleRight;
			newb.Image = mi.Img;
			newb.Text = mi.ItemName + "\n" + mi.ItemModName;
			newb.Size = new Size(this.flpInternalItems.Width - 30, 40);
			newb.BackColor = Color.Gainsboro;
			newb.ForeColor = Color.Black;

			newb.Tag = new string[] { mi.ItemName, mi.ItemModName };
			newb.Parent = this.flpInternalItems;

			newb.Click += new EventHandler(this.AnyInternalItemButton_Click);
		}
		
		private void AnyInternalItemButton_Click(object sender, EventArgs e)
		{
			Button btn = (Button)sender;
			string[] tag = (string[])(btn.Tag);
			string btnItemName = tag[0];
			string btnItemModName = tag[1];

			this.ItemName = btnItemName;
			this.ItemModName = btnItemModName;
			this.DialogResult = DialogResult.OK;
			this.Close();

		}

	}
}
