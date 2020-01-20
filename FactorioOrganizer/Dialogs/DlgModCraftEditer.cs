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
	public partial class DlgModCraftEditer : Form
	{
		public oMod TheMod;



		public string Title
		{
			get { return this.Text; }
			set { this.Text = value; }
		}




		//we give it the ModCraft to edit
		public DlgModCraftEditer(oMod StartMod)
		{
			this.TheMod = StartMod;

			InitializeComponent();



		}
		private void DlgModCraftEditer_Load(object sender, EventArgs e)
		{

			this.RefreshEnabled();
		}




		private void btnBrowseRecipe_Click(object sender, EventArgs e)
		{
			//create and setup a dialog to "browse" an item
			DlgBrowseRefItem dbri = new DlgBrowseRefItem(this.TheMod);
			dbri.Title = "Browse a recipe";
			dbri.ShowDialog();
			if (dbri.DialogResult == DialogResult.OK)
			{
				//Program.wdebug(dbri.ItemModName + "_$_" + dbri.ItemName);

				this.SetRecipe(dbri.ItemName, dbri.ItemModName);

			}

		}

		private void tbRecipeItemName_TextChanged(object sender, EventArgs e)
		{
			this.RefreshEnabled();
		}
		private void tbRecipeModName_TextChanged(object sender, EventArgs e)
		{
			this.RefreshEnabled();
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
		
		private void btnAddInput_Click(object sender, EventArgs e)
		{
			//create and setup a dialog to "browse" an item
			DlgBrowseRefItem dbri = new DlgBrowseRefItem(this.TheMod);
			dbri.Title = "Add Input";
			dbri.ShowDialog();
			if (dbri.DialogResult == DialogResult.OK)
			{
				//check that this item hasn't already been added
				if (!this.IsAlreadyInput(dbri.ItemName, dbri.ItemModName))
				{
					this.AddInput(dbri.ItemName, dbri.ItemModName);

				}
			}
			
		}
		private void btnAddOutput_Click(object sender, EventArgs e)
		{
			//create and setup a dialog to "browse" an item
			DlgBrowseRefItem dbri = new DlgBrowseRefItem(this.TheMod);
			dbri.Title = "Add Output";
			dbri.ShowDialog();
			if (dbri.DialogResult == DialogResult.OK)
			{
				//check that this item hasn't already been added
				if (!this.IsAlreadyOutput(dbri.ItemName, dbri.ItemModName))
				{
					this.AddOutput(dbri.ItemName, dbri.ItemModName);

				}
			}

		}


		private void RefreshEnabled()
		{
			this.btnOk.Enabled = this.IsValidRecipeItemName() && this.IsValidRecipeModName();


		}
		private bool IsValidRecipeItemName()
		{
			if (this.RecipeItemName.Length <= 0) { return false; }

			return true;
		}
		private bool IsValidRecipeModName()
		{
			if (this.RecipeModName.Length <= 0) { return false; }

			return true;
		}



		public void SetRecipe(refItem ri)
		{
			this.SetRecipe(ri.ItemName, ri.ModName);
		}
		public void SetRecipe(string ItemName, string ModName)
		{
			this.RecipeItemName = ItemName;
			this.RecipeModName = ModName;
		}
		public string RecipeItemName
		{
			get { return this.tbRecipeItemName.Text; }
			set { this.tbRecipeItemName.Text = value; }
		}
		public string RecipeModName
		{
			get { return this.tbRecipeModName.Text; }
			set { this.tbRecipeModName.Text = value; }
		}
		public refItem GetRecipe()
		{
			return new refItem(this.RecipeItemName, this.RecipeModName);
		}

		public void SetInputs(refItem[] inputs)
		{
			this.RemoveAllInputs();
			foreach (refItem ri in inputs)
			{
				this.AddInput(ri.ItemName, ri.ModName);
			}
		}
		public refItem[] GetInputs()
		{
			List<refItem> rep = new List<refItem>();
			foreach (Button btn in this.listBtnInput)
			{
				string[] tag = (string[])(btn.Tag);
				rep.Add(new refItem(tag[0], tag[1]));
			}
			return rep.ToArray();
		}

		public void SetOutputs(refItem[] outputs)
		{
			this.RemoveAllOutputs();
			foreach (refItem ri in outputs)
			{
				this.AddOutput(ri.ItemName, ri.ModName);
			}
		}
		public refItem[] GetOutputs()
		{
			List<refItem> rep = new List<refItem>();
			foreach (Button btn in this.listBtnOutput)
			{
				string[] tag = (string[])(btn.Tag);
				rep.Add(new refItem(tag[0], tag[1]));
			}
			return rep.ToArray();
		}

		public void SetIsMadeInFurnace(bool newval)
		{
			this.cbIsMadeInFurnace.Checked = newval;
		}
		public bool GetIsMadeInFurnace()
		{
			return this.cbIsMadeInFurnace.Checked;
		}




		#region inputs
		//this list represent all inputs of the craft. if we want the inputs, we must extract them form the .Tag property of the buttons.
		private List<Button> listBtnInput = new List<Button>();
		private void RemoveAllInputs()
		{
			while (this.listBtnInput.Count > 0)
			{
				Button btn = this.listBtnInput[0];
				
				btn.Parent = null;
				btn.Dispose();
				this.listBtnInput.RemoveAt(0);
			}
		}

		//return if a specified item is already in the input list
		private bool IsAlreadyInput(string iItemName, string iModName)
		{
			foreach (Button btn in this.listBtnInput)
			{
				string[] tag = (string[])(btn.Tag);
				string btnItemName = tag[0];
				string btnModName = tag[1];
				//check if it's the same object
				if (btnItemName == iItemName && btnModName == iModName)
				{
					return true;
				}
			}
			return false;
		}


		private void AddInput(string iItemName, string iModName)
		{
			Button newb = new Button();
			newb.TextAlign = ContentAlignment.MiddleLeft;
			newb.ImageAlign = ContentAlignment.MiddleRight;
			newb.Click += new EventHandler(this.AnyButtonInput_Click);

			newb.Size = new Size(this.flpInputs.Width - 30, 40);
			newb.BackColor = Color.Gainsboro;
			newb.ForeColor = Color.Black;

			newb.Text = iItemName + "\n" + iModName;
			//get the image, if there is one
			oMod.ModItem mi = this.TheMod.GetModItemFromStats(iItemName, iModName);
			if (mi != null)
			{
				if (mi.Img != null)
				{
					newb.Image = mi.Img;
				}
			}

			//asign its tag
			newb.Tag = new string[] { iItemName, iModName };

			this.listBtnInput.Add(newb); //add the button to the list
			newb.Parent = this.flpInputs;
		}
		private void AnyButtonInput_Click(object sender, EventArgs e)
		{
			Button btn = (Button)sender;

			btn.Parent = null;
			this.listBtnInput.Remove(btn); //remove itself from the list, very important
			btn.Dispose();

		}


		#endregion
		#region outputs
		//this list represent all outputs of the craft. if we want the inputs, we must extract them form the .Tag property of the buttons.
		private List<Button> listBtnOutput = new List<Button>();
		private void RemoveAllOutputs()
		{
			while (this.listBtnOutput.Count > 0)
			{
				Button btn = this.listBtnOutput[0];

				btn.Parent = null;
				btn.Dispose();
				this.listBtnOutput.RemoveAt(0);
			}
		}

		//return if a specified item is already in the input list
		private bool IsAlreadyOutput(string iItemName, string iModName)
		{
			foreach (Button btn in this.listBtnOutput)
			{
				string[] tag = (string[])(btn.Tag);
				string btnItemName = tag[0];
				string btnModName = tag[1];
				//check if it's the same object
				if (btnItemName == iItemName && btnModName == iModName)
				{
					return true;
				}
			}
			return false;
		}


		private void AddOutput(string iItemName, string iModName)
		{
			Button newb = new Button();
			newb.TextAlign = ContentAlignment.MiddleLeft;
			newb.ImageAlign = ContentAlignment.MiddleRight;
			newb.Click += new EventHandler(this.AnyButtonOutput_Click);

			newb.Size = new Size(this.flpOutputs.Width - 30, 40);
			newb.BackColor = Color.Gainsboro;
			newb.ForeColor = Color.Black;

			newb.Text = iItemName + "\n" + iModName;
			//get the image, if there is one
			oMod.ModItem mi = this.TheMod.GetModItemFromStats(iItemName, iModName);
			if (mi != null)
			{
				if (mi.Img != null)
				{
					newb.Image = mi.Img;
				}
			}

			//asign its tag
			newb.Tag = new string[] { iItemName, iModName };

			this.listBtnOutput.Add(newb); //add the button to the list
			newb.Parent = this.flpOutputs;
		}
		private void AnyButtonOutput_Click(object sender, EventArgs e)
		{
			Button btn = (Button)sender;

			btn.Parent = null;
			this.listBtnOutput.Remove(btn); //remove itself from the list, very important
			btn.Dispose();
		}

		#endregion

	}
}
