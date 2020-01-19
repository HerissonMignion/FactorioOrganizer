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

			this.ManagerItem = new uiModItemManager();
			this.ManagerItem.Parent = this.gbItems;
			this.ManagerItem.Dock = DockStyle.Fill;
			this.ManagerItem.UserDidSomeChange += new EventHandler(this.ManagerItem_UserDidSomeChange);

			this.ManagerCraft = new uiModCraftManager();
			this.ManagerCraft.Parent = this.gbCrafts;
			this.ManagerCraft.Dock = DockStyle.Fill;
			this.ManagerCraft.UserDidSomeChange += new EventHandler(this.ManagerCraft_UserDidSomeChange);

			this.ManagerItem.SetCraftManager(this.ManagerCraft);

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
			Program.ActualNextForm = Program.NextFormToShow.Form1;
			this.Close();
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
