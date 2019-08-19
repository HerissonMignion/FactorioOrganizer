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
			this.Editer.VirtualWidth = 20f; // 10f

			this.TB = new uiToolBox(this.Editer);
			this.AddTabPage(this.TB, "Factorio");

			

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

		private void ButtonSave_MouseClick(object sender, MouseEventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			DialogResult rep = sfd.ShowDialog();
			if (rep == DialogResult.OK)
			{
				string filepath = sfd.FileName;
				if (System.IO.Path.GetExtension(filepath) != ".factory") { filepath += ".factory"; }
				
				this.Editer.Map.Save(filepath);
				string filename = System.IO.Path.GetFileName(filepath);
				this.SetTitle(filename);
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


				string filename = System.IO.Path.GetFileName(filepath);
				this.SetTitle(filename);

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



			}

		}

		private void ButtonModMore_Click(object sender, EventArgs e)
		{
			Program.ActualNextForm = Program.NextFormToShow.FormModEditerEmpty;
			this.Close();
		}
		private void ButtonAutoLoadMod_Click(object sender, EventArgs e)
		{

		}
		private void ButtonLoadSingleMod_Click(object sender, EventArgs e)
		{

		}


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



		private void AddTabPage(uiToolBox tb, string TabTitle)
		{
			TabPage newpage = new TabPage();
			tb.Parent = newpage;
			tb.Dock = DockStyle.Fill;
			newpage.Text = TabTitle;
			//newpage.BackColor = Color.Black;
			newpage.Parent = this.TabContainer;

		}




		public void RefreshSize()
		{
			this.Editer.Top = this.TabContainer.Top + this.TabContainer.Height + 5; // 110
			this.Editer.Left = 5;
			this.Editer.Width = this.Width - 16 - (this.Editer.Left * 2);
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
