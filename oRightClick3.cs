using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace FactorioOrganizer
{
	public class oRightClick3
	{
		
		private Form forme = null;
		private PictureBox ImageBox = null;

		private int uiSpaceUpDown = 2;
		private int uiSpaceRightLeft = 2;
		private int uiSeparatorHeight = 3;
		public int uiChoiceHeight = 22; // 20
		public Font ChoiceFont = new Font("lucida console", 10f); // consolas 15


		public int GetHeight()
		{
			int ActualHeight = this.uiSpaceUpDown;
			foreach (rc3Layer l in this.listLayer)
			{
				if (l.ActualLayerType == rc3Layer.LayerType.separation) { ActualHeight += this.uiSeparatorHeight; }
				if (l.ActualLayerType == rc3Layer.LayerType.choice) { ActualHeight += this.uiChoiceHeight; }
			}
			ActualHeight += this.uiSpaceUpDown;
			return ActualHeight;
		}

		private int zzzWidth = 250; //valeur de width défini par l'extérieur
		public bool ManualWidth = false; //indique si la largeur a été défini manuellement
		public int Width
		{
			get
			{
				if (this.ManualWidth)
				{
					return this.zzzWidth;
				}
				else
				{
					return this.GetWidth();
				}
			}
			set
			{
				this.zzzWidth = value;
				this.ManualWidth = true;
			}
		}
		private int GetWidth()
		{
			int ActualWidth = 150;
			Graphics g = Graphics.FromImage(new Bitmap(10, 10));
			//fait uniquement les layer avec un seul choix
			foreach (rc3Layer l in this.listLayer)
			{
				if (l.ActualLayerType == rc3Layer.LayerType.choice)
				{
					if (l.AllChoice.Length == 1)
					{
						int TextWidth = (int)(g.MeasureString(l.AllChoice[0], this.ChoiceFont).Width);
						if (TextWidth > ActualWidth) { ActualWidth = TextWidth; }
					}
				}
			}
			g.Dispose();
			ActualWidth += this.uiSpaceUpDown * 2;
			return ActualWidth;
		}



		private Point zzzShowPos = new Point(-1, -1);
		public bool ManualShowPos = false; //indique si la position d'affichage a été défini manuellement
		public Point ShowPos
		{
			get
			{
				return this.zzzShowPos;
			}
			set
			{
				this.zzzShowPos = value;
				this.ManualShowPos = true;
			}
		}



		private class rc3Layer
		{
			public enum LayerType
			{
				choice,
				separation
			}
			public LayerType ActualLayerType = LayerType.separation;

			public string[] AllChoice;

			//séparation :
			public rc3Layer()
			{
				this.ActualLayerType = LayerType.separation;
			}
			//choice :
			public rc3Layer(string TheChoice)
			{
				this.ActualLayerType = LayerType.choice;
				this.AllChoice = new string[] { TheChoice };
			}
			public rc3Layer(string[] Choices)
			{
				this.ActualLayerType = LayerType.choice;
				this.AllChoice = Choices;
			}

		}
		private List<rc3Layer> listLayer; // = new List<rc3Layer>();
		public void AddSeparator()
		{
			this.listLayer.Add(new rc3Layer());
		}
		public void AddChoice(string TheChoice)
		{
			this.listLayer.Add(new rc3Layer(TheChoice));
		}
		public void AddChoice(string[] Choices)
		{
			if (Choices.Length > 0)
			{
				this.listLayer.Add(new rc3Layer(Choices));
			}
		}


		


		private string zzzResult = "";
		public string Result { get { return this.zzzResult; } }

		public string GetChoice()
		{
			return this.ShowDialog();
		}
		public string ShowDialog()
		{

			//// crée la form et la picturebox
			this.forme = new Form();
			this.forme.MinimumSize = new Size(1, 1);
			this.forme.ShowInTaskbar = false;
			this.forme.FormBorderStyle = FormBorderStyle.None;
			this.forme.StartPosition = FormStartPosition.Manual;
			this.forme.Size = new Size(350, 350);

			this.ImageBox = new PictureBox();
			this.ImageBox.Parent = this.forme;
			this.ImageBox.Dock = DockStyle.Fill;
			this.ImageBox.BackColor = Color.Blue;
			this.ImageBox.MouseMove += new MouseEventHandler(this.ImageBox_MouseMove);
			this.ImageBox.MouseLeave += new EventHandler(this.ImageBox_MouseLeave);
			this.ImageBox.MouseUp += new MouseEventHandler(this.ImageBox_MouseUp);

			////taille de la form
			this.forme.Height = this.GetHeight();
			this.forme.Width = this.Width;


			//// calcul la position où placer la form (au cas où elle sort de l'écran)
			Point fsp = Cursor.Position;
			Size ScreenSize = Screen.PrimaryScreen.Bounds.Size;
			if (fsp.X + this.forme.Width > ScreenSize.Width) { fsp.X -= this.forme.Width; }
			if (fsp.Y + this.forme.Height > ScreenSize.Height) { fsp.Y -= this.forme.Height; }
			if (fsp.X < 0) { fsp.X = 0; }
			if (fsp.Y < 0) { fsp.Y = 0; }

			if (this.ManualShowPos)
			{
				fsp = this.ShowPos;
			}
			

			//// s'affiche à l'utilisateur
			this.forme.Location = fsp;
			this.RefreshImage();
			this.forme.ShowDialog();



			//// dispose tout
			try
			{
				this.forme.Dispose();
				this.ImageBox.Dispose();
			}
			catch { }


			////end
			return this.zzzResult;
		}





		//void new()
		public oRightClick3()
		{
			this.listLayer = new List<rc3Layer>();
			this.AddChoice("Cancel"); // Annuler


		}

		private void ImageBox_MouseMove(object sender, MouseEventArgs e)
		{
			this.RefreshImage();
			GC.Collect();
		}
		private void ImageBox_MouseLeave(object sender, EventArgs e)
		{
			this.RefreshImage();
			GC.Collect();
		}
		private void ImageBox_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				int imgWidth = this.ImageBox.Width;
				int imgHeight = this.ImageBox.Height;

				Point mpos = this.ImageBox.PointToClient(Cursor.Position);
				Rectangle mrec = new Rectangle(mpos.X, mpos.Y, 1, 1);
				bool isMouseInside = mpos.X >= 0 && mpos.Y >= 0 && mpos.X < imgWidth && mpos.Y < imgHeight;

				if (isMouseInside)
				{
					int ActualTop = this.uiSpaceUpDown;
					foreach (rc3Layer l in this.listLayer)
					{
						if (l.ActualLayerType == rc3Layer.LayerType.separation)
						{
							ActualTop += this.uiSeparatorHeight;
						}
						if (l.ActualLayerType == rc3Layer.LayerType.choice)
						{
							Rectangle itemRec = new Rectangle(this.uiSpaceRightLeft, ActualTop, imgWidth - (2 * this.uiSpaceRightLeft), this.uiChoiceHeight);
							bool isMouseTouchItem = isMouseInside && mpos.Y >= ActualTop && mpos.Y < ActualTop + this.uiChoiceHeight;

							if (isMouseTouchItem)
							{

								if (l.AllChoice.Length == 1)
								{
									this.zzzResult = l.AllChoice[0];
									this.forme.Close();
								}
								else
								{
									for (int index = 0; index <= l.AllChoice.Length - 1; index++)
									{
										int xleft = this.uiSpaceRightLeft + (itemRec.Width * index / l.AllChoice.Length);
										int xleftP1 = this.uiSpaceRightLeft + (itemRec.Width * (index + 1) / l.AllChoice.Length);
										if (xleftP1 > imgWidth - this.uiSpaceRightLeft) { xleftP1 = imgWidth - this.uiSpaceRightLeft; }
										if (xleft < mpos.X && mpos.X <= xleftP1)
										{
											this.zzzResult = l.AllChoice[index];
											this.forme.Close();
										}
									}
								}
							}
							
							ActualTop += this.uiChoiceHeight;
						}
					}

				}


			}
		}



		public enum ColorTheme
		{
			Light,
			Dark,
			Water
		}
		public ColorTheme ActualTheme = ColorTheme.Dark;

		private Color BackColor
		{
			get
			{
				if (this.ActualTheme == ColorTheme.Light) { return Color.Gainsboro; }
				if (this.ActualTheme == ColorTheme.Dark) { return Color.Black; }
				if (this.ActualTheme == ColorTheme.Water) { return Color.LightSkyBlue; }
				return Color.Gainsboro;
			}
		}
		private Pen BorderPen
		{
			get
			{
				if (this.ActualTheme == ColorTheme.Light) { return Pens.Gray; }
				if (this.ActualTheme == ColorTheme.Dark) { return Pens.Gray; }
				if (this.ActualTheme == ColorTheme.Water) { return Pens.RoyalBlue; }
				return Pens.Gray;
			}
		}
		private Pen SeparatorPen
		{
			get
			{
				if (this.ActualTheme == ColorTheme.Light) { return Pens.Gray; }
				if (this.ActualTheme == ColorTheme.Dark) { return Pens.DimGray; }
				if (this.ActualTheme == ColorTheme.Water) { return Pens.RoyalBlue; }
				return Pens.Gray;
			}
		}
		private Brush MouseOnItemBackColor
		{
			get
			{
				if (this.ActualTheme == ColorTheme.Light) { return Brushes.Silver; }
				if (this.ActualTheme == ColorTheme.Dark) { return new SolidBrush(Color.FromArgb(48, 48, 48)); }
				if (this.ActualTheme == ColorTheme.Water) { return Brushes.White; }
				return Brushes.Silver;
			}
		}
		private Brush ForeBrush
		{
			get
			{
				if (this.ActualTheme == ColorTheme.Light) { return Brushes.Black; }
				if (this.ActualTheme == ColorTheme.Dark) { return Brushes.White; }
				if (this.ActualTheme == ColorTheme.Water) { return Brushes.Black; }
				return Brushes.Black;
			}
		}



		private void RefreshImage()
		{
			int imgWidth = this.ImageBox.Width;
			int imgHeight = this.ImageBox.Height;
			Bitmap img = new Bitmap(imgWidth, imgHeight);
			Graphics g = Graphics.FromImage(img);
			g.Clear(this.BackColor);

			Point mpos = this.ImageBox.PointToClient(Cursor.Position);
			Rectangle mrec = new Rectangle(mpos.X, mpos.Y, 1, 1);
			bool isMouseInside = mpos.X >= 0 && mpos.Y >= 0 && mpos.X < imgWidth && mpos.Y < imgHeight;

			

			//dessine la bordure
			g.DrawRectangle(this.BorderPen, 0, 0, imgWidth - 1, imgHeight - 1);


			Pen hSepPen = this.SeparatorPen;
			Brush hForeBrush = this.ForeBrush;

			////dessine les item
			int ActualTop = this.uiSpaceUpDown;
			foreach (rc3Layer l in this.listLayer)
			{
				if (l.ActualLayerType == rc3Layer.LayerType.separation)
				{
					g.DrawLine(hSepPen, this.uiSpaceRightLeft, ActualTop + 1, imgWidth - this.uiSpaceRightLeft - 1, ActualTop + 1);

					ActualTop += this.uiSeparatorHeight;
				}
				else if (l.ActualLayerType == rc3Layer.LayerType.choice)
				{
					Rectangle itemRec = new Rectangle(this.uiSpaceRightLeft, ActualTop, imgWidth - (2 * this.uiSpaceRightLeft), this.uiChoiceHeight);
					bool isMouseTouchItem = isMouseInside && mpos.Y >= ActualTop && mpos.Y < ActualTop + this.uiChoiceHeight;
					////back color
					if (isMouseTouchItem)
					{
						if (l.AllChoice.Length == 1)
						{
							g.FillRectangle(this.MouseOnItemBackColor, itemRec);
						}
						else
						{
							//obtien l'index de la souris
							double mprct = (double)(mpos.X - itemRec.X) / (double)(itemRec.Width);
							int index = (int)(mprct * (double)(l.AllChoice.Length));
							if (index >= l.AllChoice.Length) { index = l.AllChoice.Length - 1; }


							int xleft = this.uiSpaceRightLeft + (itemRec.Width * index / l.AllChoice.Length);
							int xleftP1 = this.uiSpaceRightLeft + (itemRec.Width * (index + 1) / l.AllChoice.Length);
							if (xleftP1 > imgWidth - this.uiSpaceRightLeft) { xleftP1 = imgWidth - this.uiSpaceRightLeft; }
							g.FillRectangle(this.MouseOnItemBackColor, xleft, ActualTop, xleftP1 - xleft, this.uiChoiceHeight);

							

						}
					}

					////dessine le text
					if (l.AllChoice.Length == 1)
					{
						SizeF TextSize = g.MeasureString(l.AllChoice[0], this.ChoiceFont);
						g.DrawString(l.AllChoice[0], this.ChoiceFont, hForeBrush, this.uiSpaceRightLeft, ActualTop + (this.uiChoiceHeight / 2) - (int)(TextSize.Height / 2f));

					}
					else
					{
						for (int index = 0; index <= l.AllChoice.Length - 1; index++)
						{
							int xleft = this.uiSpaceRightLeft + (itemRec.Width * index / l.AllChoice.Length);
							SizeF TextSize = g.MeasureString(l.AllChoice[index], this.ChoiceFont);
							g.DrawString(l.AllChoice[index], this.ChoiceFont, hForeBrush, xleft, ActualTop + (this.uiChoiceHeight / 2) - (int)(TextSize.Height / 2f));
						}
					}

					ActualTop += this.uiChoiceHeight;
				}
			}




			g.Dispose();
			if (this.ImageBox.Image != null) { this.ImageBox.Image.Dispose(); }
			this.ImageBox.Image = img;
		}




		

	}
}
