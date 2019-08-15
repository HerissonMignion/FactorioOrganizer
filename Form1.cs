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
			//this.GenTestMap();

			this.ButtonTest.Visible = false;
			this.ButtonTopMost.Text = "Top Most : " + this.TopMost.ToString();
			this.RefreshSize();
		}
		private void ButtonTest_Click(object sender, EventArgs e)
		{
			//this.Editer.StartAddMode(new MapObject(MOType.Machine, FOType.CopperCable));

			//List<FOType> allt = Utilz.GetListOfAllFOType();
			//foreach (FOType t in allt)
			//{
			//	Program.wdebug(t.ToString());
			//}



			//Random rnd = new Random();
			//for (int i = 0; i < 100; i++)
			//{
			//	string str = ((float)(rnd.NextDouble() * rnd.NextDouble() * rnd.NextDouble() * rnd.NextDouble() * rnd.NextDouble() * rnd.NextDouble() * 10000d)).ToString("N20");
			//	Program.wdebug(oMap.TrimStrNumber(str)); //it was public static for the tests
			//}


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




		private void GenTestMap()
		{
			//belt iron
			for (int i = 0; i < 20; i++)
			{
				MapObject mo = new MapObject(MOType.Belt, FOType.IronPlate);
				mo.vpos.X = (float)i * 1.5f;
				mo.vpos.Y = 0f;
				this.Map.listMO.Add(mo);
			}
			//belt cooper
			for (int i = 0; i < 20; i++)
			{
				MapObject mo = new MapObject(MOType.Belt, FOType.CopperPlate);
				mo.vpos.X = (float)i * 1.5f;
				mo.vpos.Y = -2f;
				this.Map.listMO.Add(mo);
			}

			//machine gear
			for (int i = 0; i < 10; i++)
			{
				MapObject mo = new MapObject(MOType.Machine, FOType.IronGear);
				mo.vpos.X = (float)i * 4f;
				mo.vpos.Y = 3f;
				this.Map.listMO.Add(mo);
			}

			//belt gear
			for (int i = 0; i < 20; i++)
			{
				MapObject mo = new MapObject(MOType.Belt, FOType.IronGear);
				mo.vpos.X = (float)i * 1.5f;
				mo.vpos.Y = 8f;
				this.Map.listMO.Add(mo);
			}


		}

	}
}
