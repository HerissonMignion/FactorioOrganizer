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
	public partial class Form1 : Form
	{

		public uiToolBox TB;
		public uiMapEditer Editer;
		private oMap Map;


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

						this.Editer.Map.Save(this.ActualFilePath);

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
						if (System.IO.Path.GetExtension(filepath) != ".factory") { filepath += ".factory"; } //make sure the file extension is .factory
						try
						{
							this.Editer.Map.Save(filepath);


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




		private void SetTitle(string text)
		{
			if (text.Length > 0)
			{
				this.Text = "Factorio Organizer - " + text;
			}
			else
			{
				this.Text = "Factorio Organizer";
			}
		}


		public Form1()
		{
			InitializeComponent();
			this.KeyDown += new KeyEventHandler(this.Form1_KeyDown);
			this.KeyUp += new KeyEventHandler(this.Form1_KeyUp);
			this.TabContainer.DrawItem += new DrawItemEventHandler(this.TabContainer_DrawItem);


			
			this.Map = new oMap();

			this.Editer = new uiMapEditer(this.Map);
			this.Editer.Parent = this;
			this.Editer.VirtualWidth = 20f; // 20f
			this.Editer.UserDidSomeChange += new EventHandler(this.Editer_UserDidSomeChange);


			//load every currently loaded mod
			this.LoadEveryMod();



			this.CreateCraftsEvents();
		}
		private void Form1_Load(object sender, EventArgs e)
		{

			this.SetTitle("");
			this.ButtonTopMost.Text = "Top Most : " + this.TopMost.ToString();
			this.RefreshSize();
		}
		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			this.Editer.KeyDown(e.KeyCode);
		}
		private void Form1_KeyUp(object sender, KeyEventArgs e)
		{
			this.Editer.KeyUp(e.KeyCode);
		}

		private void Editer_UserDidSomeChange(object sender, EventArgs e)
		{
			this.EditsWereMade = true;

		}

		private void ButtonNew_MouseClick(object sender, MouseEventArgs e)
		{
			bool canceled = this.AutoAskUserToSaveCurrent();
			if (!canceled)
			{
				
				//create a new map
				oMap newmap = new oMap();
				//define the map
				this.Editer.SetMap(newmap);
				this.Map = newmap;
				this.ClearActualFile();

				//we reset the edits variable
				this.EditsWereMade = false;

			}
		}
		private void ButtonOpen_MouseClick(object sender, MouseEventArgs e)
		{
			bool canceled = this.AutoAskUserToSaveCurrent();
			if (!canceled)
			{
				OpenFileDialog ofd = new OpenFileDialog();
				ofd.Multiselect = false;
				DialogResult rep = ofd.ShowDialog();
				if (rep == DialogResult.OK)
				{
					//save the actual map. if an error occurre while opening the file, we re-put this object into the map editer.
					oMap OldMap = this.Editer.Map;


					string filepath = ofd.FileName;
					//oMap newm = null; // new oMap(filepath);
					try
					{
						oMap newm = new oMap(filepath);


						//if an error occurred when opening the map, when creating the oMap object with the filepath, the following part will not execute and the map editer will not be affected a null map object.

						//define the map.
						this.Editer.SetMap(newm);
						this.Map = newm;


						//compute a position in the middle of the factory. it's to make sure that the user won't has to search for the factory / don't have to try to stay close to the pos (0;0)
						float mx = 0f;
						float my = 0f;
						int count = 0;
						foreach (MapObject mo in newm.listMO)
						{
							mx += mo.vpos.X;
							my += mo.vpos.Y;
							count++;
						}
						this.Editer.vpos.X = mx / (float)count;
						this.Editer.vpos.Y = my / (float)count;
						this.Editer.RefreshImage();


						//string filename = System.IO.Path.GetFileName(filepath);

						//set this to the new file opened
						this.DefineActualFile(filepath);
						this.EditsWereMade = false;

					}
					catch
					{
						MessageBox.Show("An error occurred");
					}
				}
			}
		}
		private void ButtonSave_MouseClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (this.AnyFileDefined)
				{
					//if the file is already defined, we just have to save
					this.Editer.Map.Save(this.ActualFilePath);

				}
				else
				{
					//if the file is not defined, it's like save as
					this.ButtonSaveAs_MouseClick(this, e);
				

				}


			}
			catch { }
		}
		private void ButtonSaveAs_MouseClick(object sender, MouseEventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			DialogResult rep = sfd.ShowDialog();
			if (rep == DialogResult.OK)
			{
				string filepath = sfd.FileName;
				if (System.IO.Path.GetExtension(filepath) != ".factory") { filepath += ".factory"; }
				
				this.Editer.Map.Save(filepath);
				//string filename = System.IO.Path.GetFileName(filepath);
				//this.SetTitle(filename);

				//set this to the new file path
				this.DefineActualFile(filepath);
				this.EditsWereMade = false; //reset this variable

			}
		}


		private void ButtonHelp_MouseClick(object sender, MouseEventArgs e)
		{
			//FormHelp fh = new FormHelp();
			//fh.TopMost = this.TopMost;
			//fh.ShowDialog();

			//lunch the process with the args for the form1 help form
			System.Diagnostics.Process.Start(Program.ProgramPath, "-help1");

		}
		private void ButtonTopMost_MouseClick(object sender, MouseEventArgs e)
		{
			this.TopMost = !this.TopMost;
			this.ButtonTopMost.Text = "Top Most : " + this.TopMost.ToString();

			//test options. must click the button two times
			if (this.TopMost == false)
			{






			}

		}

		private void ButtonModMore_MouseClick(object sender, MouseEventArgs e)
		{
			bool canceled = this.AutoAskUserToSaveCurrent();
			if (!canceled)
			{
				Program.ActualNextForm = Program.NextFormToShow.FormModEditerEmpty;
				this.Close();
				this.DestroyCraftsEvents();
			}
		}
		private void ButtonAutoLoadMod_MouseClick(object sender, MouseEventArgs e)
		{

		}
		private void ButtonLoadModsInsideFolder_MouseClick(object sender, MouseEventArgs e)
		{
			//create and setup a dialog for the user to select a folder
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			DialogResult rep = fbd.ShowDialog();
			if (rep == DialogResult.OK)
			{
				//get the selected folder path
				string FolderPath = fbd.SelectedPath;

				//call the void that will load every mod in this path
				this.LoadModsInFolder(FolderPath);

			}
		}
		private void ButtonLoadSingleMod_MouseClick(object sender, MouseEventArgs e)
		{
			//create and setup an open file dialog
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Multiselect = false;
			DialogResult rep = ofd.ShowDialog();
			if (rep == DialogResult.OK)
			{
				string FilePath = ofd.FileName;

				try
				{
					//load the mod object
					oMod NewMod = new oMod(FilePath);


					//mods load faster when the tab container does have to refresh while the process.
					this.TabContainer.Visible = false;
					Crafts.AddMod(NewMod);
					this.TabContainer.Visible = true;

					this.Editer.RefreshImage();
				}
				catch
				{
					MessageBox.Show("An error occurred");
				}
			}
		}
		private void ButtonUnloadMods_Click(object sender, EventArgs e)
		{
			bool canceled = this.AutoAskUserToSaveCurrent();
			if (!canceled)
			{
				Crafts.UnloadEveryMods();
				Program.ActualNextForm = Program.NextFormToShow.Form1;
				this.Close();
				this.DestroyCraftsEvents();
			}
			else
			{
				//we change the focus to a button that won't react with the space bar
				this.ButtonSave.Focus();
			}
		}

		private void cbShowGrid_CheckedChanged(object sender, EventArgs e)
		{
			//the editer is supposed to refresh itself automatically by this assignment
			this.Editer.ShowGrid = this.cbShowGrid.Checked;

			//we change the focus to a button that won't react with the space bar
			this.ButtonSave.Focus();
		}





		//load, in the correct order, every mods (identified by the file extension) found inside a folder.
		private void LoadModsInFolder(string FolderPath)
		{
			//generate a list of every extensions associated to a fomod file.
			//System.IO.Path.GetExtension return the . and the text file path after that dot. for this reason, everything put into this list begins with a .
			List<string> extensions = new List<string>();
			extensions.Add(".fomod");
			for (int i = 1; i <= 9; i++)
			{
				extensions.Add(".fomod" + i.ToString());
				extensions.Add(".fomod" + i.ToString().PadLeft(2, "0".ToCharArray()[0]));
			}
			for (int i = 10; i <= 99; i++)
			{
				extensions.Add(".fomod" + i.ToString().PadLeft(2, "0".ToCharArray()[0]));
			}

			//get every file in this path
			List<string> EveryFilePath = System.IO.Directory.GetFiles(FolderPath).ToList();

			////filter for fomod files.
			//this list contain, in the appropriate loading order, every mod file's path to load.
			List<string> EveryModPath = new List<string>();
			//because extension array is our first loop, the mod files will be added according to mod load order
			foreach (string ext in extensions)
			{
				//we search every file with this extension
				int index = EveryFilePath.Count - 1;
				while (index >= 0)
				{
					//the actual file path
					string FilePath = EveryFilePath[index];
					try
					{
						//get this file's extension
						string FileExt = System.IO.Path.GetExtension(FilePath);

						//check if this file's extension is the extension we are actually looking for.
						if (FileExt == ext)
						{
							//because this file's extension is the extension we are actually looking for, we add it to the EveryModPath list
							EveryModPath.Add(FilePath);

							//we remove this file from the list, because we know we won't have to analyse it anymore.
							EveryFilePath.RemoveAt(index);

						}

					}
					catch
					{

					}
					
					//next iteration
					index--;
				}

				//if there's no file left in the EveryFilePath list, we can stop because there's nothing left.
				if (EveryFilePath.Count <= 0) { break; }

			}

			////create the mod objects for every file
			List<oMod> listMods = new List<oMod>();
			//we create a mod for every file
			foreach (string ModFilePath in EveryModPath)
			{
				try
				{
					//generate the mod for this file
					oMod newmod = new oMod(ModFilePath);

					//add the mod to the list of mod to load
					listMods.Add(newmod);

				}
				catch
				{
					MessageBox.Show("An error occurred when loading " + ModFilePath);
				}
			}

			////load the mods

			//mods load faster when the tab container does have to refresh while the process.
			this.TabContainer.Visible = false;
			Crafts.AddMod(listMods);
			this.TabContainer.Visible = true;

			this.Editer.RefreshImage();


		}


		//a "dark theme" for the tabs.
		private void TabContainer_DrawItem(object sender, DrawItemEventArgs e)
		{
			Graphics g = e.Graphics;
			g.Clear(Color.FromArgb(64, 64, 64));

			for (int i = 0; i < this.TabContainer.TabPages.Count; i++)
			{
				Rectangle rec = this.TabContainer.GetTabRect(i);
				g.FillRectangle(new SolidBrush(Color.FromArgb(32, 32, 32)), rec);
				TabPage ActualPage = this.TabContainer.TabPages[i];

				PointF TextPos = new PointF((float)(rec.X), (float)(rec.Y + 2));
				g.DrawString(ActualPage.Text, ActualPage.Font, Brushes.White, TextPos);
			}
		}




		//load every mod currently existing in the static class Crafts
		private void LoadEveryMod()
		{
			foreach (string modname in Crafts.listLoadedModName)
			{
				//create its tool box
				uiToolBox newtb = new uiToolBox(this.Editer, modname);

				//generate the title to give to the tab
				string tabtitle = modname;
				if (tabtitle == "vanilla") { tabtitle = "Factorio"; }

				//create the tabpage
				this.AddTabPage(newtb, tabtitle);

			}
		}
		private void AddTabPage(uiToolBox tb, string TabTitle)
		{
			TabPage newpage = new TabPage();
			tb.Parent = newpage;
			tb.Dock = DockStyle.Fill;
			newpage.Text = TabTitle;
			//newpage.BackColor = Color.Black;
			newpage.Parent = this.TabContainer;

		}



		private void CreateCraftsEvents()
		{
			Crafts.ModAdded += new EventHandler<ModEventArgs>(this.Crafts_ModAdded);
		}
		private void DestroyCraftsEvents()
		{
			Crafts.ModAdded -= new EventHandler<ModEventArgs>(this.Crafts_ModAdded);
		}

		private void Crafts_ModAdded(object sender, ModEventArgs e)
		{
			if (this.Visible)
			{
				//gets the new mod
				oMod newmod = e.Mod;

				//gets the mod name
				string newmodname = newmod.ModName;

				//create the new tool box
				uiToolBox newtb = new uiToolBox(this.Editer, newmodname);

				//create the new tabpage
				this.AddTabPage(newtb, newmodname);
				




			}
			else
			{
				this.DestroyCraftsEvents();
			}
		}






		public void RefreshSize()
		{
			this.Editer.Top = this.TabContainer.Top + this.TabContainer.Height + 5; // 110
			this.Editer.Left = 5;
			this.Editer.Width = this.ButtonHelp.Left - 0 - (this.Editer.Left * 2); // this.Width - 16 - (this.Editer.Left * 2);
			this.Editer.Height = this.Height - 39 - (this.Editer.Top + this.Editer.Left);
			this.Editer.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;

			//this.TB.Top = 5;
			//this.TB.Left = 5;
			//this.TB.Width = this.Width - 15 - (2 * this.TB.Left) - 155; // -150
			//this.TB.Height = 103; // 103
			//this.TB.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;


		}

	}
}
