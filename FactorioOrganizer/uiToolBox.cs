using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace FactorioOrganizer
{
	public class uiToolBox
	{
		public uiMapEditer Editer;

		private Panel panele;
		private Label TextBelt;
		private Label TextAssembler;


		public int Top
		{
			get { return this.panele.Top; }
			set { this.panele.Top = value; }
		}
		public int Left
		{
			get { return this.panele.Left; }
			set { this.panele.Left = value; }
		}
		public int Width
		{
			get { return this.panele.Width; }
			set { this.panele.Width = value; }
		}
		public int Height
		{
			get { return this.panele.Height; }
			set { this.panele.Height = value; }
		}
		public Control Parent
		{
			get { return this.panele.Parent; }
			set { this.panele.Parent = value; }
		}
		public DockStyle Dock
		{
			get { return this.panele.Dock; }
			set { this.panele.Dock = value; }
		}
		public AnchorStyles Anchor
		{
			get { return this.panele.Anchor; }
			set { this.panele.Anchor = value; }
		}

		
		private void voidnew()
		{
			this.panele = new Panel();
			this.panele.BorderStyle = BorderStyle.FixedSingle;
			this.panele.AutoScroll = true;
			this.panele.BackColor = Color.FromArgb(32, 32, 32);

			this.TextBelt = new Label();
			this.TextBelt.Parent = this.panele;
			this.TextBelt.Text = "Belt :";
			this.TextBelt.ForeColor = Color.White;

			this.TextAssembler = new Label();
			this.TextAssembler.Parent = this.panele;
			this.TextAssembler.Text = "Assembler :";
			this.TextAssembler.ForeColor = Color.White;


			this.CreateCraftsEvents();
		}
		//vanilla
		public uiToolBox(uiMapEditer StartEditer)
		{
			this.Editer = StartEditer;
			this.voidnew();

			this.CreateVanillaControls();
			this.RefreshSize();
		}
		//mods
		public uiToolBox(uiMapEditer StartEditer, string sModName)
		{
			this.Editer = StartEditer;
			this.voidnew();

			this.CreateModControls(sModName);
			this.RefreshSize();
		}



		private List<Button> listButtonBelt = new List<Button>();
		private List<Button> listButtonMachine = new List<Button>();


		//the mod name of the currently loaded items. the mod name of the items to load and show. for vanilla it's "vanilla"
		public string ModName = "";

		

		//creates all the default items
		private void CreateVanillaControls()
		{
			//loads every vanilla items
			this.CreateModControls("vanilla");

			//int index = 0;
			//while (index < Crafts.listItems.Count)
			//{
			//	oItem i = Crafts.listItems[index];
			//	//check if it's a vanilla item
			//	if (i.ModName == "vanilla" && i.ItemName != "none")
			//	{
			//		this.CreateNewButtonBoth(i);
			//	}
			//	//next iteration
			//	index++;
			//}
		}
		private void CreateModControls(string smodname)
		{
			this.ModName = smodname;
			
			//loads every items of that mod.
			int index = 0;
			while (index < Crafts.listItems.Count)
			{
				oItem i = Crafts.listItems[index];
				//check if it's an item of the mod
				if (i.ModName == smodname && i.ItemName != "none")
				{
					this.CreateNewButtonBoth(i);
				}
				//next iteration
				index++;
			}
		}


		private void CreateNewButtonBelt(Bitmap img, oItem i)
		{
			Button newb = new Button();
			newb.Parent = this.panele;
			newb.Image = img;
			newb.ImageAlign = ContentAlignment.MiddleCenter;
			this.listButtonBelt.Add(newb);
			//newb.Click += new EventHandler(this.AnyButton_Click);
			newb.MouseDown += new MouseEventHandler(this.AnyButton_MosueDown);
			newb.Tag = new object[] { MOType.Belt, i };
		}
		private void CreateNewButtonMachine(Bitmap img, oItem i)
		{
			Button newb = new Button();
			newb.Parent = this.panele;
			newb.Image = img;
			newb.ImageAlign = ContentAlignment.MiddleCenter;
			this.listButtonMachine.Add(newb);
			//newb.Click += new EventHandler(this.AnyButton_Click);
			newb.MouseDown += new MouseEventHandler(this.AnyButton_MosueDown);
			newb.Tag = new object[] { MOType.Machine, i };
		}
		private void CreateNewButtonBoth(oItem i)
		{
			Bitmap img = Crafts.GetAssociatedIcon(i);
			this.CreateNewButtonBelt(img, i);
			this.CreateNewButtonMachine(img, i);
		}
		private void AnyButton_MosueDown(object sender, MouseEventArgs e)
		{
			Button btn = (Button)sender;
			btn.Focus();
			MOType mt = (MOType)(((object[])(btn.Tag))[0]);
			oItem i = (oItem)(((object[])(btn.Tag))[1]);

			if (e.Button == MouseButtons.Left)
			{
				if (mt == MOType.Belt)
				{
					if (i.IsBelt) //we set addmode only if the item can be a belt
					{
						MapObject newmo = new MapObject(MOType.Belt, i);
						this.Editer.StartAddMode(newmo);
					}
				}
				if (mt == MOType.Machine)
				{
					if (i.IsRecipe) //we set addmode only if the item can be a machine
					{
						MapObject newmo = new MapObject(MOType.Machine, i);
						this.Editer.StartAddMode(newmo);
					}
				}
			}
			if (e.Button == MouseButtons.Right)
			{
				//if this button is as machine, we show the user the inputs and outputs of the recipe
				if (mt == MOType.Machine)
				{
					//if this button is as machine, i represents the recipe
					oCraft c = Crafts.GetCraftFromRecipe(i);
					oRightClick3 rc = new oRightClick3();
					rc.AddChoice(i.Name);
					if (c != null)
					{
						rc.AddSeparator();
						rc.AddSeparator();
						//add every outputs and inputs for the user
						rc.AddChoice("Outputs :");
						foreach (oItem subi in c.Outputs)
						{
							rc.AddChoice("-" + subi.Name);
						}
						rc.AddChoice("");
						rc.AddChoice("Inputs :");
						foreach (oItem subi in c.Inputs)
						{
							rc.AddChoice("-" + subi.Name);
						}
					}
					string rep = rc.ShowDialog(); //it simply show to the user what the inputs and outputs are
				}

				//if this button is a belt, we show the user every crafts that require this item as input
				if (mt == MOType.Belt)
				{
					oRightClick3 rc = new oRightClick3();
					rc.AddChoice(i.Name);
					rc.AddSeparator();
					rc.AddChoice("Used In Recipe :");
					//run through every crafts and check their inputs to see if the actual item is used in that craft
					foreach (oCraft c in Crafts.listCrafts)
					{
						//check the inputs and see if the item i is there
						foreach (oItem i2 in c.Inputs)
						{
							//check if this is the item we are searching
							if (i2.Name == i.Name)
							{
								//now that we have found the item in this craft, we add the craft recipe to the list and continue to the next item
								rc.AddChoice("-" + c.Recipe.Name);

								//now that we have found the item, we don't have to continue search in this craft
								break;
							}
						}

					}
					
					string rep = rc.ShowDialog();
					//if the user clicked on a recipe, we set the editer to add mode and with a machine of that recipe.
					//we get the item from name
					oItem therecipe = Crafts.GetItemFromName(rep.Replace("-", string.Empty)); //the .Replace can be a little problem if - is truly part of the item name.
					//if rep is not an item, GetItemFromName will not return null, it will return the none item.
					if (therecipe.Name.ToLower() != "none")
					{
						MapObject newmo = new MapObject(MOType.Machine, therecipe);
						this.Editer.StartAddMode(newmo);
					}


				}



			}

		}

		//remove every controls
		private void RemoveEveryButton()
		{
			//belt
			while (this.listButtonBelt.Count > 0)
			{
				Button btn = this.listButtonBelt[0];
				//remove the event
				btn.MouseDown -= new MouseEventHandler(this.AnyButton_MosueDown);

				btn.Parent = null;

				//dispose the button
				btn.Dispose();

				//remove the button from the list
				this.listButtonBelt.RemoveAt(0);
			}
			//machine
			while (this.listButtonMachine.Count > 0)
			{
				Button btn = this.listButtonMachine[0];
				//remove the event
				btn.MouseDown -= new MouseEventHandler(this.AnyButton_MosueDown);

				btn.Parent = null;

				//dispose the button
				btn.Dispose();

				//remove the button from the list
				this.listButtonMachine.RemoveAt(0);
			}
		}




		private void RefreshSize()
		{
			this.TextBelt.Top = 15; // 15
			this.TextBelt.Left = 5; // 5
			this.TextBelt.AutoSize = true;

			this.TextAssembler.Top = 55; // 55
			this.TextAssembler.Left = 5; // 5
			this.TextAssembler.AutoSize = true;
			


			int StartLeft = 70; // 70
			Size buttonsize = new Size(40, 40); // 40 40

			int actualleft = StartLeft;
			foreach (Button b in this.listButtonBelt)
			{
				MOType mt = (MOType)(((object[])(b.Tag))[0]);
				oItem i = (oItem)(((object[])(b.Tag))[1]);
				b.Left = actualleft;
				b.Top = 1;
				b.Size = buttonsize;

				//back color ////    couleur d'arrière plan
				b.BackColor = Color.Gainsboro;
				bool isbelt = i.IsBelt;
				if (!isbelt) { b.BackColor = Color.Crimson; }


				//next iteration
				actualleft += b.Width + 1;
			}

			actualleft = StartLeft;
			foreach (Button b in this.listButtonMachine)
			{
				MOType mt = (MOType)(((object[])(b.Tag))[0]);
				oItem i = (oItem)(((object[])(b.Tag))[1]);
				b.Left = actualleft;
				b.Top = 1 + buttonsize.Height + 2;
				b.Size = buttonsize;

				//back color ////    couleur d'arrière plan
				b.BackColor = Color.Gainsboro;
				bool ismachine = i.IsRecipe;
				if (!ismachine) { b.BackColor = Color.Crimson; }

				//next iteration
				actualleft += b.Width + 1;
			}


		}







		private void CreateCraftsEvents()
		{
			Crafts.ModImportFinished += new EventHandler(this.Crafts_ModImportFinished);
		}
		private void DestroyCraftsEvents()
		{
			Crafts.ModImportFinished -= new EventHandler(this.Crafts_ModImportFinished);
		}
		
		//when the import of mods is finished.
		private void Crafts_ModImportFinished(object sender, EventArgs e)
		{
			if (this.Parent != null)
			{
				////we must remove every button and recreate them all.

				//remove all control
				this.RemoveEveryButton();

				//recreate all control
				this.CreateModControls(this.ModName);

				this.RefreshSize();
			}
			else
			{
				this.DestroyCraftsEvents();
			}
		}








		

	}
}
