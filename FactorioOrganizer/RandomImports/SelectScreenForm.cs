using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace FactorioOrganizer.RandomImports
{

	//this "dialog" is like snipping tool, the user select an area of the screen
	class SelectScreenForm
	{

		private Form forme;
		private PictureBox ImageBox;

		private int ScreenWidth = -1;
		private int ScreenHeight = -1;

		public Bitmap imgScreen; //image originale de l'écran
		private Bitmap imgUser; //image affiché à l'utilisateur

		private Font fontBig = new Font("consolas", 20f);
		private Font fontBigBold = new Font("consolas", 20f, FontStyle.Bold);

		private string[] LittleText = new string[] { "1) Press and hold the mouse left button at one corner of the rectangle.", "2) Drag the mouse to the opposite corner of the rectangle.", "3) Release the mouse left button." };
		private Font fontLittle = new Font("consolas", 12f);
		private Font fontLittleBold = new Font("consolas", 12f, FontStyle.Bold);




		public void ShowToUser()
		{
			//calcul tout le stuff
			this.ScreenWidth = Screen.PrimaryScreen.Bounds.Width;
			this.ScreenHeight = Screen.PrimaryScreen.Bounds.Height;


			//récupère le contenue de l'écran
			this.imgScreen = new Bitmap(this.ScreenWidth, this.ScreenHeight);
			Graphics g = Graphics.FromImage(this.imgScreen);
			g.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(this.ScreenWidth, this.ScreenHeight));
			g.Dispose();

			//crée l'image à afficher à l'user
			this.imgUser = new Bitmap(this.imgScreen); //new Bitmap(this.ScreenWidth, this.ScreenHeight);
			float mulfact = 0.7f;
			this.AdjustBrightnessMatrix(this.imgUser, mulfact, mulfact, mulfact);
			g = Graphics.FromImage(this.imgUser);
			g.DrawString("Draw a rectangle on the screen", this.fontBigBold, Brushes.Black, 5f, 5f);
			g.DrawString("Draw a rectangle on the screen", this.fontBig, Brushes.White, 5f, 5f);
			//draw the "little text"
			float ActualY = 30f;
			foreach (string line in this.LittleText)
			{
				//draw the text
				g.DrawString(line, this.fontLittleBold, Brushes.Black, 5f, ActualY);
				g.DrawString(line, this.fontLittle, Brushes.White, 5f, ActualY);

				//next iteration
				ActualY += 15f;
			}

			g.Dispose();


			this.ImageBox.Image = this.imgUser;
			this.forme.WindowState = FormWindowState.Maximized;
			this.forme.ShowDialog();
		}



		public SelectScreenForm()
		{
			this.forme = new Form();
			this.forme.Text = "Select Screen Form";
			this.forme.FormBorderStyle = FormBorderStyle.None;
			this.forme.ShowInTaskbar = false;
			this.forme.BackColor = Color.Black;

			this.ImageBox = new PictureBox();
			this.ImageBox.Parent = this.forme;
			this.ImageBox.BackColor = Color.Blue;
			this.ImageBox.Dock = DockStyle.Fill;
			this.ImageBox.MouseDown += new MouseEventHandler(this.ImageBox_MouseDown);
			this.ImageBox.MouseUp += new MouseEventHandler(this.ImageBox_MouseUp);
			this.ImageBox.MouseMove += new MouseEventHandler(this.ImageBox_MouseMove);


			
		}
		private void ImageBox_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.posMouseDown.X = e.X;
				this.posMouseDown.Y = e.Y;
				this.isMouseLeftDown = true;
			}
		}
		private void ImageBox_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.isMouseLeftDown = false;
				this.posMouseUp.X = e.X;
				this.posMouseUp.Y = e.Y;

				this.ImageBox.Refresh();
				this.forme.Close();
			}
		}
		private void ImageBox_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.isMouseLeftDown)
			{
				this.ImageBox.Refresh();
				Graphics g = this.ImageBox.CreateGraphics();
				Point p1 = this.GetUpLeft(this.posMouseDown, e.Location);
				Point p2 = this.GetDownRight(this.posMouseDown, e.Location);
				Size rsize = new Size(p2.X - p1.X, p2.Y - p1.Y);

				//g.DrawImage(this.imgScreen, new Point[] { p1 }, new Rectangle(p1, rsize), GraphicsUnit.Pixel);
				g.DrawImage(this.imgScreen, new Rectangle(p1, rsize), p1.X, p1.Y, rsize.Width, rsize.Height, GraphicsUnit.Pixel);

				g.DrawRectangle(new Pen(Color.Black, 3f), p1.X, p1.Y, rsize.Width, rsize.Height);
				g.DrawRectangle(Pens.White, p1.X, p1.Y, rsize.Width, rsize.Height);

				g.Dispose();
			}
		}


		private bool isMouseLeftDown = false; //indique si mouse button left est down
		private Point posMouseDown = new Point(-1, -1); //position du mousedown de l'user
		private Point posMouseUp = new Point(-1, -1); //position du mouseup

		private Point GetUpLeft(Point p1, Point p2)
		{
			Point rep = new Point(p1.X, p1.Y);
			if (p2.X < p1.X)
			{
				rep.X = p2.X;
			}
			if (p2.Y < p1.Y)
			{
				rep.Y = p2.Y;
			}
			return rep;
		}
		private Point GetDownRight(Point p1, Point p2)
		{
			Point rep = new Point(p2.X, p2.Y);
			if (p1.X > p2.X)
			{
				rep.X = p1.X;
			}
			if (p1.Y > p2.Y)
			{
				rep.Y = p1.Y;
			}
			return rep;
		}


		//retourne le rectangle sélectionné par l'utilisateur
		public Rectangle GetRectangle()
		{
			Point p1 = this.GetUpLeft(this.posMouseDown, this.posMouseUp);
			Point p2 = this.GetDownRight(this.posMouseDown, this.posMouseUp);
			Size rsize = new Size(p2.X - p1.X, p2.Y - p1.Y);
			return new Rectangle(p1, rsize);
		}






		//this function is not entirely from me:
		public Bitmap AdjustBrightnessMatrix(Bitmap img, float RedMul, float GreenMul, float BlueMul)
		{
			//if (value == 0) { return img; } // No change, so just return

			//System.Drawing.Imaging.ColorMap cmm = new System.Drawing.Imaging.ColorMap();




			//float sb = (float)value / 255F;
			//float d = 1f / sb;
			float[][] colorMatrixElements =
						   {
								 new float[] {RedMul,  0,  0,  0, 0}, //new float[] {1,  0,  0,  0, 0},
                                 new float[] {0,  GreenMul,  0,  0, 0}, //new float[] {0,  1,  0,  0, 0},
                                 new float[] {0,  0,  BlueMul,  0, 0}, //new float[] {0,  0,  1,  0, 0},
                                 new float[] {0,  0,  0,  1, 0}, //new float[] {0,  0,  0,  1, 0},
                                 new float[] {0,  0,  0,  1, 0}  //new float[] {sb, sb, sb, 1, 1}
                           };

			System.Drawing.Imaging.ColorMatrix cm = new System.Drawing.Imaging.ColorMatrix(colorMatrixElements);
			System.Drawing.Imaging.ImageAttributes imgattr = new System.Drawing.Imaging.ImageAttributes();
			System.Drawing.Rectangle rc = new System.Drawing.Rectangle(0, 0, img.Width, img.Height);
			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
			imgattr.SetColorMatrix(cm);
			g.DrawImage(img, rc, 0, 0, img.Width, img.Height, System.Drawing.GraphicsUnit.Pixel, imgattr);

			imgattr.Dispose();
			g.Dispose();
			return img;
		}
	}
}
