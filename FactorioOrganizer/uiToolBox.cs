﻿using System;
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

		

		public uiToolBox(uiMapEditer StartEditer)
		{
			this.Editer = StartEditer;

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
			
			

			this.CreateVanillaControls();
			this.RefreshSize();
		}



		private List<Button> listButtonBelt = new List<Button>();
		private List<Button> listButtonMachine = new List<Button>();
		

		//creates all the default items
		private void CreateVanillaControls()
		{
			
			//loads every vanilla items
			int index = 0;
			while (index < Crafts.listItems.Count)
			{
				sItem i = Crafts.listItems[index];
				//check if it's a vanilla item
				if (i.ModName == "vanilla" && i.ItemName != "none")
				{
					this.CreateNewButtonBoth(i);
				}
				//next iteration
				index++;
			}
			
		}


		private void CreateNewButtonBelt(Bitmap img, sItem i)
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
		private void CreateNewButtonMachine(Bitmap img, sItem i)
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
		private void CreateNewButtonBoth(sItem i)
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
			sItem i = (sItem)(((object[])(btn.Tag))[1]);

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
					foreach (sItem subi in c.Outputs)
					{
						rc.AddChoice("-" + subi.Name);
					}
					rc.AddChoice("");
					rc.AddChoice("Inputs :");
					foreach (sItem subi in c.Inputs)
					{
						rc.AddChoice("-" + subi.Name);
					}
				}
				string rep = rc.ShowDialog();
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
				sItem i = (sItem)(((object[])(b.Tag))[1]);
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
				sItem i = (sItem)(((object[])(b.Tag))[1]);
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


	}
}
