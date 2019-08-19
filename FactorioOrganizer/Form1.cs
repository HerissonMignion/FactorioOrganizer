﻿using System;
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



		public Form1()
		{
			InitializeComponent();
			this.KeyDown += new KeyEventHandler(this.Form1_KeyDown);
			this.KeyUp += new KeyEventHandler(this.Form1_KeyUp);
			
			this.Map = new oMap();

			this.Editer = new uiMapEditer(this.Map);
			this.Editer.Parent = this;
			this.Editer.VirtualWidth = 20f; // 10f

			this.TB = new uiToolBox(this.Editer);
			this.TB.Parent = this;

			

		}
		private void Form1_Load(object sender, EventArgs e)
		{

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

		private void ButtonSave_MouseClick(object sender, MouseEventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			DialogResult rep = sfd.ShowDialog();
			if (rep == DialogResult.OK)
			{
				string filepath = sfd.FileName;
				if (System.IO.Path.GetExtension(filepath) != ".factory") { filepath += ".factory"; }

				this.Editer.Map.Save(filepath);
			}
		}
		private void ButtonOpen_MouseClick(object sender, MouseEventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Multiselect = false;
			DialogResult rep = ofd.ShowDialog();
			if (rep == DialogResult.OK)
			{
				string filepath = ofd.FileName;
				oMap newm = new oMap(filepath);
				this.Editer.SetMap(newm);

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

			}
		}
		private void ButtonHelp_MouseClick(object sender, MouseEventArgs e)
		{
			FormHelp fh = new FormHelp();
			fh.TopMost = this.TopMost;
			fh.ShowDialog();

		}
		private void ButtonTopMost_MouseClick(object sender, MouseEventArgs e)
		{
			this.TopMost = !this.TopMost;
			this.ButtonTopMost.Text = "Top Most : " + this.TopMost.ToString();

			//test options. must click the button two times
			if (this.TopMost == false)
			{
				//foreach (sItem i in Crafts.listItems)
				//{
				//	Program.wdebug(i.Name);
				//}
				//foreach (oCraft c in Crafts.listCrafts)
				//{
				//	Program.wdebug(c.ToString());
				//}

				string modname = "vanilla";


				//List<FOType> allft = Utilz.GetListOfAllFOType();
				//foreach (FOType ft in allft)
				//{
				//	string strname = ft.ToString();
				//	bool isbelt = Utilz.IsBeltable(ft);
				//	bool isrecipe = Utilz.IsRecipe(ft);
				//	string newl = "Crafts.AddItem(new sItem(\"" + strname + "\", " + isbelt.ToString().ToLower() + ", " + isrecipe.ToString().ToLower() + ", \"" + modname + "\"), imghere);";

				//	Program.wdebug(newl);

				//}



				////create crafts
				//foreach (FOType ft in allft)
				//{
				//	string itemname = ft.ToString();
				//	if (itemname != "none")
				//	{
				//		bool isbelt = Utilz.IsBeltable(ft);
				//		bool isrecipe = Utilz.IsRecipe(ft);
				//		//create crafts
				//		if (isrecipe)
				//		{
				//			string strinputs = "new sItem[] { ";
				//			FOType[] inputs = Utilz.GetRecipeInputs(ft);
				//			bool isfirst = true;
				//			foreach (FOType ft2 in inputs)
				//			{
				//				//Crafts.GetItemFromName
				//				if (!isfirst) { strinputs += ", "; }

				//				strinputs += "Crafts.GetItemFromName(\"" + ft2.ToString() + "\")";

				//				isfirst = false;
				//			}
				//			strinputs += " }";

				//			string stroutputs = "new sItem[] { ";
				//			FOType[] outputs = Utilz.GetRecipeOutputs(ft);
				//			isfirst = true;
				//			foreach (FOType ft2 in outputs)
				//			{
				//				//Crafts.GetItemFromName
				//				if (!isfirst) { strinputs += ", "; }

				//				stroutputs += "Crafts.GetItemFromName(\"" + ft2.ToString() + "\")";

				//				isfirst = false;
				//			}
				//			stroutputs += " }";

				//			//Program.wdebug(stroutputs);


				//			bool isfurnace = Utilz.IsRecipeMadeInFurnace(ft);

				//			//oCraft newc = new oCraft(Crafts.ConvertFotTosItem(ft), Crafts.ConvertArrayFotTosItem(inputs), Crafts.ConvertArrayFotTosItem(outputs), isfurnace);
				//			//Crafts.AddCraft(newc);

				//			//Crafts.AddCraft(new oCraft())

				//			string repcode = "Crafts.AddCraft(new oCraft(Crafts.GetItemFromName(\"" + ft.ToString() + "\"), " + strinputs + ", " + stroutputs + ", " + isfurnace.ToString().ToLower() + "));";
				//			Program.wdebug(repcode);




				//		}
				//	}
				//}





			}

		}




		public void RefreshSize()
		{
			this.Editer.Top = 110; // 110
			this.Editer.Left = 5;
			this.Editer.Width = this.Width - 16 - (this.Editer.Left * 2);
			this.Editer.Height = this.Height - 39 - (this.Editer.Top + this.Editer.Left);
			this.Editer.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;

			this.TB.Top = 5;
			this.TB.Left = 5;
			this.TB.Width = this.Width - 15 - (2 * this.TB.Left) - 155; // -150
			this.TB.Height = 103; // 103
			this.TB.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;


		}
		





		



	}
}
