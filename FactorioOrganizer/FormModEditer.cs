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
	public partial class FormModEditer : Form
	{




		private void SetTitle(string text)
		{
			if (text.Length > 0)
			{
				this.Text = "Mod File Maker/Editer - " + text;
			}
			else
			{
				this.Text = "Mod File Maker/Editer";
			}
		}


		private string ActualFilePath = ""; //file path of the actual file
		private bool AnyFileDefined = false;
		//define the file actually "loaded"
		private void DefineActualFile(string filepath)
		{
			this.ActualFilePath = filepath;
			this.AnyFileDefined = true;
			this.SetTitle(System.IO.Path.GetFileName(filepath));
		}
		//remove all information about the file that would actually be "loaded"
		private void ClearActualFile()
		{
			this.ActualFilePath = "";
			this.AnyFileDefined = false;
			this.SetTitle("");
		}

		private bool EditsWereMade = false; //this variable will become true if the user did any changes to the map.


		//return if the user "cancelled" the dialog.
		//this function automatically ask the user if he wants to save the current file and if yes, it will make him save the file somewhere so, outside this function, we don't have to manage this.
		//this is only called when the user is doing something (opening an other file, exiting, click new) that will delete its data.
		private bool AutoAskUserToSaveCurrent()
		{
			//if there wasn't any change, we don't pop anything (because there's nothing to save) and we return false to mean that the user did not click the cancel button
			if (!this.EditsWereMade) { return false; }

			//we pop the dialog
			DialogResult rep = MessageBox.Show("Do you want to save your changes?", "Factorio Organizer", MessageBoxButtons.YesNoCancel);
			if (rep == DialogResult.Yes)
			{
				if (this.AnyFileDefined)
				{
					//if the file is already defined, we just have to save it
					try
					{

						//this.Editer.Map.Save(this.ActualFilePath);
						this.Mod.Save(this.ActualFilePath);


					}
					catch
					{
						MessageBox.Show("An error occurred");
						return true; //we return that the user cancelled so its data is not lost
					}
				}
				else
				{
					//if the file was not defined, we must ask the user where to save it
					SaveFileDialog sfd = new SaveFileDialog();
					DialogResult drep = sfd.ShowDialog();
					if (drep == DialogResult.OK)
					{
						string filepath = sfd.FileName;
						if (System.IO.Path.GetExtension(filepath) != ".fomod") { filepath += ".fomod"; } //make sure the file extension is .factory
						try
						{
							//this.Editer.Map.Save(filepath);
							this.Mod.Save(filepath);

						}
						catch
						{
							MessageBox.Show("An error occurred");
							return true; //we return that the user cancelled so its data is not lost
						}
					}
					else
					{
						//if the user cancelled, we return true
						return true;
					}
				}
			}



			return rep == DialogResult.Cancel;
		}





		//the "managers"
		private uiModItemManager ManagerItem;
		private uiModCraftManager ManagerCraft;
		//private uiModImageManager ManagerImage;

			


		private void voidnew()
		{
			InitializeComponent();
			this.FormClosing += new FormClosingEventHandler(this.FormModEditer_FormClosing);

			this.ManagerItem = new uiModItemManager();
			this.ManagerItem.Parent = this.gbItems;
			this.ManagerItem.Dock = DockStyle.Fill;
			this.ManagerItem.UserDidSomeChange += new EventHandler(this.ManagerItem_UserDidSomeChange);

			this.ManagerCraft = new uiModCraftManager();
			this.ManagerCraft.Parent = this.gbCrafts;
			this.ManagerCraft.Dock = DockStyle.Fill;
			this.ManagerCraft.UserDidSomeChange += new EventHandler(this.ManagerCraft_UserDidSomeChange);

			this.ManagerItem.SetCraftManager(this.ManagerCraft);
			this.ManagerCraft.SetItemManager(this.ManagerItem);

			//this.ManagerImage = new uiModImageManager();
			//this.ManagerImage.Parent = this.gbImages;
			//this.ManagerImage.Dock = DockStyle.Fill;

		}
		//create and show/allow to edit and save an empty mod
		public FormModEditer()
		{
			this.voidnew();

			this.SetMod(new oMod());
		}
		//edit a mod
		public FormModEditer(oMod sMod)
		{
			this.voidnew();
			this.ButtonReturnToForm1.Visible = false;

			this.SetMod(sMod);
		}
		private void FormModEditer_Load(object sender, EventArgs e)
		{
			this.SetTitle("");

		}
		private void FormModEditer_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				//we auto ask the user to save its work if it's not saved
				bool canceled = this.AutoAskUserToSaveCurrent();
				if (canceled)
				{
					//if the user canceled, we cancel
					e.Cancel = true;
				}
			}
		}
		private void ManagerItem_UserDidSomeChange(object sender, EventArgs e)
		{
			this.EditsWereMade = true;
			//Program.wdebug("manager item edit");

		}
		private void ManagerCraft_UserDidSomeChange(object sender, EventArgs e)
		{
			this.EditsWereMade = true;
			//Program.wdebug("manager craft edit");

		}


		private void tbModName_TextChanged(object sender, EventArgs e)
		{
			string NewName = this.tbModName.Text;
			if (NewName.Length > 0)
			{
				this.tbModName.BackColor = Color.White;
				this.tbModName.ForeColor = Color.Black;

				this.Mod.ModName = NewName;

			}
			else
			{
				this.tbModName.BackColor = Color.Crimson;
				this.tbModName.ForeColor = Color.White;
			}

		}

		private void ButtonReturnToForm1_MouseClick(object sender, MouseEventArgs e)
		{
			bool canceled = this.AutoAskUserToSaveCurrent();
			if (!canceled)
			{

				this.EditsWereMade = false; //whatever if the user decided to save or not, the event FormClosing will be raised and we must not ask the user to save a second time.

				//if the user canceled, we set the next form to show to form1 and we close this
				Program.ActualNextForm = Program.NextFormToShow.Form1;
				this.Close();
			}
		}
		private void ButtonNew_Click(object sender, EventArgs e)
		{

			////TESTEST
			//for (int i = 1; i < 350; i++)
			//{
			//	//create the bitmap
			//	Bitmap miimg = new Bitmap(32, 32);
			//	Graphics g = Graphics.FromImage(miimg);
			//	g.Clear(Color.White);
			//	g.DrawString("_" + i.ToString(), new Font("consolas", 10f), Brushes.Black, 0f, 0f);
			//	g.Dispose();

			//	oMod.ModItem mi = new oMod.ModItem("TestItem_" + i.ToString().PadLeft(4, "0".ToCharArray()[0]), "-", miimg);

			//	//add it
			//	this.Mod.AddItem(mi);

			//}


			bool canceled = this.AutoAskUserToSaveCurrent();
			if (!canceled)
			{
				//build a new mod object
				oMod newmod = new oMod();

				//set the new mod
				this.SetMod(newmod);

				//reset the actual defined file
				this.ClearActualFile();

				//we reset the edits variable
				this.EditsWereMade = false;
				
			}
		}
		private void ButtonOpen_Click(object sender, EventArgs e)
		{
			bool canceled = this.AutoAskUserToSaveCurrent();
			if (!canceled)
			{

				OpenFileDialog ofd = new OpenFileDialog();
				ofd.Multiselect = false;
				DialogResult rep = ofd.ShowDialog();
				if (rep == DialogResult.OK)
				{
					try
					{
						string OpenFilePath = ofd.FileName;

						oMod newmod = new oMod(OpenFilePath);
						this.SetMod(newmod);


						//set this to the new file opened
						this.DefineActualFile(OpenFilePath);
						this.EditsWereMade = false; //reset this variable

					}
					catch
					{
						MessageBox.Show("An error occurred.");
					}
				}
			}
		}
		private void ButtonSave_Click(object sender, EventArgs e)
		{
			if (this.AnyFileDefined)
			{
				//if the file is already defined, we just have to save
				//this.Editer.Map.Save(this.ActualFilePath);
				try
				{
					this.Mod.Save(this.ActualFilePath);
					this.EditsWereMade = false;
				}
				catch
				{
					MessageBox.Show("An error occurred.");
				}

			}
			else
			{
				//if the file is not defined, it's like save as
				this.ButtonSaveAs_Click(this, e);


			}

		}
		private void ButtonSaveAs_Click(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			DialogResult rep = sfd.ShowDialog();
			if (rep == DialogResult.OK)
			{
				string SaveFilePath = sfd.FileName;
				if (System.IO.Path.GetExtension(SaveFilePath) != ".fomod") { SaveFilePath += ".fomod"; }

				try
				{
					this.Mod.Save(SaveFilePath);

					//set this to the new filepath
					this.DefineActualFile(SaveFilePath);
					this.EditsWereMade = false; //reset this variable
				}
				catch
				{
					MessageBox.Show("An error occurred.");
				}

			}
		}
		
		private void ButtonHelp_Click(object sender, EventArgs e)
		{
			//lunch the process with the args for the mod help form
			System.Diagnostics.Process.Start(Program.ProgramPath, "-helpmods");
		}


		#region research

		private void tbResearch_TextChanged(object sender, EventArgs e)
		{
			//if there are currently an item or craft selected, if we don't unselect them, the reserch will start from where the selection is. we want the research to start from the beginning.
			this.ManagerItem.UnselectIndex();
			this.ManagerCraft.UnselectIndex();
		}

		private void ButtonResearchItem_Click(object sender, EventArgs e)
		{
			//we get the string to search
			string str = this.tbResearch.Text;

			//we start the search where we left it. "where we left it" is because the user can click multiple times on the search button to go to the next item that match the research string. when a math is found, we select that item. so, to continue to the next item when the user click again on the search button, we start where this item is.
			//when no item is selected, the default value of the variable SelectedIndex is -1. this line of code under this comment start the search at SelectedIndex + 1, so we don't have to manage differently when the user start the search and when the search is already going on.
			int ActualIndex = this.ManagerItem.SelectedIndex + 1; // + 1 because we continue to the next item. the actual item would match again.

			//we go through every item until we find an item that match or until we get to the end.
			bool found = false; //becomes true when we find a match
			while (ActualIndex < this.Mod.listItems.Count)
			{
				//we get the item
				oMod.ModItem mi = this.Mod.listItems[ActualIndex];
				//we get the name of the item
				string ItemName = mi.ItemName;

				//we use regex to check if the name contain the researched string
				bool IsMatchItemName = System.Text.RegularExpressions.Regex.IsMatch(ItemName, str, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				//bool result1 = System.Text.RegularExpressions.Regex.IsMatch(ActualFileName.Replace("@", ""), Pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

				//if the item name match, we select that item to the user and we stop the research here, for now.
				if (IsMatchItemName)
				{
					found = true;

					//select the item
					this.ManagerItem.SelectIndex(ActualIndex);
					this.ManagerItem.SetTopIndex(ActualIndex - 3);

					//we end the search here
					break;

				}

				//next iteration
				ActualIndex++;
			}

			//if no more match were found, we restart the research
			if (!found)
			{
				this.ManagerItem.UnselectIndex();

				//we refresh so the user realize that the item was unselected.
				this.ManagerItem.RefreshImage();
			}

		}
		private void ButtonResearchCraft_Click(object sender, EventArgs e)
		{
			//we get the string to search
			string str = this.tbResearch.Text;

			int ActualIndex = this.ManagerCraft.SelectedIndex + 1; // + 1 because we continue to the next item. the actual item would match again.

			bool found = false; //will become true when we will find the craft
			while (ActualIndex < this.Mod.listCrafts.Count)
			{
				//we get the craft
				oMod.ModCraft mc = this.Mod.listCrafts[ActualIndex];
				//we get the name of the recipe
				string RecipeName = mc.Recipe.ItemName;

				//we use regex to check if the name contain the researched string
				bool IsMatchRecipeName = System.Text.RegularExpressions.Regex.IsMatch(RecipeName, str, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

				//if the recipe name match, we select that craft to the user and we stop the research here, for now.
				if (IsMatchRecipeName)
				{
					found = true;

					//select the craft
					this.ManagerCraft.SelectIndex(ActualIndex);
					this.ManagerCraft.SetTopIndex(ActualIndex - 1);

					//we end the search here
					break;
				}
				
				//next iteration
				ActualIndex++;
			}

			//if no more match were found, we restart the research
			if (!found)
			{
				this.ManagerCraft.UnselectIndex();

				//we refresh so the user realize that the craft was unselected.
				this.ManagerCraft.RefreshImage();
			}

		}


		#endregion




		public oMod Mod;

		public void SetMod(oMod ModToLoad)
		{
			this.Mod = ModToLoad;
			this.ManagerItem.SetMod(ModToLoad);
			this.ManagerCraft.SetMod(ModToLoad);
			//this.ManagerImage.SetMod(ModToLoad);


			this.tbModName.Text = ModToLoad.ModName; //the event text changed of the textbox will be raised, but because we previously changed this.Mod to the new mod, nothing bad will happen.


			//refresh the managers
			this.ManagerItem.RefreshImage();
			this.ManagerCraft.RefreshImage();

		}

	}
}
