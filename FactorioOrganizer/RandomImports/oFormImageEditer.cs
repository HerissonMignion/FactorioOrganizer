using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace FactorioOrganizer.RandomImports
{
	public class oFormImageEditer
	{
		private Point MousePos { get { return this.ImageBox.PointToClient(Cursor.Position); } }
		private Point ImageMousePos
		{
			get
			{
				Point rep = new Point(0, 0);

				Point mpos = this.MousePos;

				int dx = 0;
				int dy = 0;
				//if (this.ZoomLevel >= 3 && false)
				//{
				//	dx = this.ZoomLevel / 2;
				//	dy = this.ZoomLevel / 2;
				//}
				//ils sont arrondi vers le bas
				rep.X = (mpos.X + dx) / this.ZoomLevel;
				rep.Y = (mpos.Y + dy) / this.ZoomLevel;


				//check les bound
				if (rep.X < 0) { rep.X = 0; }
				if (rep.Y < 0) { rep.Y = 0; }
				if (rep.X >= this.ImgEdited.Width) { rep.X = this.ImgEdited.Width - 1; }
				if (rep.Y >= this.ImgEdited.Height) { rep.Y = this.ImgEdited.Height - 1; }
				return rep;
			}
		}




		public bool DiagAccepted = false; //true only if the user clicked the ok button



		public Bitmap ImgEdited;
		private Graphics gimgg;
		private void gimggcreate() { this.gimgg = Graphics.FromImage(this.ImgEdited); }
		private void gimggdelete() { this.gimgg.Dispose(); }


		#region control z

		private int MaxCzSize = 10; //taille max de la liste de save.
		private List<Bitmap> listImgSave = new List<Bitmap>(); //liste des image savé. les image les plus récente sont à la fin.
		private bool CanLoadCZ { get { return this.listImgSave.Count > 0; } } //indique si on peut loader une image précédante.


		private void czLoadBitmap(Bitmap src)
		{
			for (int y = 0; y < this.ImgEdited.Height; y++)
			{
				for (int x = 0; x < this.ImgEdited.Width; x++)
				{
					this.ImgEdited.SetPixel(x, y, src.GetPixel(x, y));
				}
			}
		}

		//crée une save actuel de l'image qui peut être chargé plus tard
		private void SaveCZ()
		{
			//crée la nouvelle image qui est une copy de l'image actuel
			Bitmap newsave = new Bitmap(this.ImgEdited);
			//l'ajoute à la liste des image précédante. sa place est à la fin de la list
			this.listImgSave.Add(newsave);

			//make sure que la list d'image savé ne dépasse pas la taille maximale
			while (this.listImgSave.Count > this.MaxCzSize) { this.listImgSave.RemoveAt(0); }

			this.ButtonEditCancel.Visible = true;
		}

		//load la save executé plus tôt
		private void LoadCZ()
		{
			if (this.CanLoadCZ)
			{
				//obtien l'image à loader
				Bitmap theimg = this.listImgSave[this.listImgSave.Count - 1]; //l'image à loader est la dernière de la liste

				//on retire l'image qu'on load
				this.listImgSave.RemoveAt(this.listImgSave.Count - 1);

				//on load cette image
				this.czLoadBitmap(theimg);


				//refresh à l'utilisateur
				this.uiRefreshImageToUser();
			}

			this.ButtonEditCancel.Visible = this.CanLoadCZ;
		}


		#endregion



		private Form forme;
		private Panel PanelImage; //ce panneau contien l'image à modifier. ce panneau possède des scrollbar pour déplacer l'image si elle est trop grosse ou trop zoomé
		private PictureBox ImageBox;


		private Label LabelZoom;
		private NumericUpDown nudZoom;

		private Button ButtonEditCancel;

		private Button ButtonPen;
		private Button ButtonEraser;
		private Button ButtonIEraser;
		private Button ButtonPickColor;
		private Button ButtonFillColor;
		private Button ButtonTheColor;

		private Label LabelSize;
		private NumericUpDown nudSize;

		private Button ButtonOk;
		private Button ButtonCancel;


		

		public void ShowDialog()
		{
			this.forme.ShowDialog();
		}
		public void Close()
		{
			this.forme.Close();
		}





		public oFormImageEditer(Bitmap sImageToEdit)
		{
			this.ImgEdited = new Bitmap(sImageToEdit);


			this.CreateInterface();
			this.RefreshTextSize();

			this.uiRefreshUiSize();
			this.uiRefreshImageToUser();
		}

		private void CreateInterface()
		{
			this.forme = new Form();
			this.forme.Text = "Image Editer";
			this.forme.Size = new Size(800, 760); // 800 700
			this.forme.ShowInTaskbar = false;
			this.forme.ShowIcon = false;
			this.forme.MinimizeBox = false;

			this.PanelImage = new Panel();
			this.PanelImage.Parent = this.forme;
			this.PanelImage.BorderStyle = BorderStyle.FixedSingle;
			this.PanelImage.AutoScroll = true;

			this.ImageBox = new PictureBox();
			this.ImageBox.Parent = this.PanelImage;
			this.ImageBox.Location = new Point(1, 1);
			//this.ImageBox.BorderStyle = BorderStyle.FixedSingle;
			this.ImageBox.BackgroundImage = this.ImgEdited;
			this.ImageBox.BackgroundImageLayout = ImageLayout.Stretch;
			this.ImageBox.MouseDown += new MouseEventHandler(this.ImageBox_MouseDown);
			this.ImageBox.MouseUp += new MouseEventHandler(this.ImageBox_MouseUp);
			this.ImageBox.MouseMove += new MouseEventHandler(this.ImageBox_MouseMove);
			this.ImageBox.MouseLeave += new EventHandler(this.ImageBox_MouseLeave);


			// interface
			this.LabelZoom = new Label();
			this.LabelZoom.Parent = this.forme;
			this.LabelZoom.Text = "Zoom";

			this.nudZoom = new NumericUpDown();
			this.nudZoom.Parent = this.forme;
			this.nudZoom.ValueChanged += new EventHandler(this.nudZoom_ValueChanged);
			this.nudZoom.Value = 7; // 7

			this.ButtonEditCancel = new Button();
			this.ButtonEditCancel.Parent = this.forme;
			this.ButtonEditCancel.Text = "<--- Cancel last edit";
			this.ButtonEditCancel.Visible = false;
			this.ButtonEditCancel.Click += new EventHandler(this.buttonEditCancel_Click);


			this.ButtonPen = new Button();
			this.ButtonPen.Parent = this.forme;
			this.ButtonPen.Text = "Pen";
			this.ButtonPen.Click += new EventHandler(this.ButtonPen_Click);

			this.ButtonEraser = new Button();
			this.ButtonEraser.Parent = this.forme;
			this.ButtonEraser.Text = "Eraser";
			this.ButtonEraser.Click += new EventHandler(this.ButtonEraser_Click);

			this.ButtonIEraser = new Button();
			this.ButtonIEraser.Parent = this.forme;
			this.ButtonIEraser.Text = "Transparent pen";
			this.ButtonIEraser.Click += new EventHandler(this.ButtonIEraser_Click);

			this.ButtonPickColor = new Button();
			this.ButtonPickColor.Parent = this.forme;
			this.ButtonPickColor.Text = "Pick color";
			this.ButtonPickColor.Click += new EventHandler(this.ButtonPickColor_Click);

			this.ButtonFillColor = new Button();
			this.ButtonFillColor.Parent = this.forme;
			this.ButtonFillColor.Text = "Fill color";
			this.ButtonFillColor.Click += new EventHandler(this.ButtonFillColor_Click);


			this.ButtonTheColor = new Button();
			this.ButtonTheColor.Parent = this.forme;
			this.ButtonTheColor.Text = "";
			this.ButtonTheColor.Click += new EventHandler(this.ButtonTheColor_Click);

			this.LabelSize = new Label();
			this.LabelSize.Parent = this.forme;
			this.LabelSize.Text = "Pen size:";
			this.LabelSize.AutoSize = true;

			this.nudSize = new NumericUpDown();
			this.nudSize.Parent = this.forme;
			this.nudSize.Minimum = 1;
			this.nudSize.Maximum = 15;
			this.nudSize.Value = 1;


			this.ButtonOk = new Button();
			this.ButtonOk.Parent = this.forme;
			this.ButtonOk.Text = "Ok";
			this.ButtonOk.Click += new EventHandler(this.ButtonOk_Click);

			this.ButtonCancel = new Button();
			this.ButtonCancel.Parent = this.forme;
			this.ButtonCancel.Text = "Cancel";
			this.ButtonCancel.Click += new EventHandler(this.ButtonCancel_Click);


		}
		public void RefreshTextSize()
		{
			this.PanelImage.Top = 70; // 50
			this.PanelImage.Left = 0;
			this.PanelImage.Width = this.forme.Width - 16 - this.PanelImage.Left;
			this.PanelImage.Height = this.forme.Height - 39 - this.PanelImage.Top;
			this.PanelImage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;


			this.LabelZoom.Location = new Point(5, 5);
			this.LabelZoom.AutoSize = true;

			this.nudZoom.Top = this.LabelZoom.Top - 3;
			this.nudZoom.Left = this.LabelZoom.Left + this.LabelZoom.Width + 4;
			this.nudZoom.Width = 40; // 40
			this.nudZoom.Minimum = 1;
			this.nudZoom.Maximum = 20;


			this.ButtonEditCancel.Top = this.nudZoom.Top + this.nudZoom.Height + 2;
			this.ButtonEditCancel.Left = this.LabelZoom.Left;
			this.ButtonEditCancel.Width = 80;
			this.ButtonEditCancel.Height = 40;


			this.ButtonPen.Top = this.nudZoom.Top;
			this.ButtonPen.Left = this.nudZoom.Left + this.nudZoom.Width + 15;
			this.ButtonPen.Height = this.nudZoom.Height + 11; // + 2
			this.ButtonPen.Width = 50;

			this.ButtonEraser.Top = this.ButtonPen.Top + this.ButtonPen.Height + 2;
			this.ButtonEraser.Left = this.ButtonPen.Left; // + this.ButtonPen.Width + 2;
			this.ButtonEraser.Height = this.ButtonPen.Height;
			this.ButtonEraser.Width = 50;
			
			this.ButtonIEraser.Top = this.ButtonPen.Top;
			this.ButtonIEraser.Left = this.ButtonPen.Left + this.ButtonPen.Width + 2;
			this.ButtonIEraser.Height = this.ButtonPen.Height;
			this.ButtonIEraser.Width = 100;

			this.ButtonPickColor.Top = this.ButtonEraser.Top;
			this.ButtonPickColor.Left = this.ButtonIEraser.Left;
			this.ButtonPickColor.Height = this.ButtonEraser.Height;
			this.ButtonPickColor.Width = 100;

			this.ButtonFillColor.Top = this.ButtonPen.Top;
			this.ButtonFillColor.Left = this.ButtonIEraser.Left + this.ButtonIEraser.Width + 2;
			this.ButtonFillColor.Width = 100;
			this.ButtonFillColor.Height = this.ButtonIEraser.Height;


			this.ButtonTheColor.Top = this.ButtonPen.Top;
			this.ButtonTheColor.Left = this.ButtonFillColor.Left + this.ButtonFillColor.Width + 5;
			this.ButtonTheColor.Width = 50;
			this.ButtonTheColor.Height = 45;


			this.LabelSize.Top = this.LabelZoom.Top;
			this.LabelSize.Left = this.ButtonTheColor.Left + this.ButtonTheColor.Width + 10;

			this.nudSize.Top = this.LabelSize.Top + this.LabelSize.Height + 2;
			this.nudSize.Left = this.LabelSize.Left;
			this.nudSize.Width = 50;


			this.ButtonOk.Size = new Size(40, 30);
			this.ButtonOk.Top = 5;
			this.ButtonOk.Left = this.forme.Width - 18 - this.ButtonOk.Width;
			this.ButtonOk.Anchor = AnchorStyles.Top | AnchorStyles.Right;

			this.ButtonCancel.Size = new Size(55, 30);
			this.ButtonCancel.Top = this.ButtonOk.Top;
			this.ButtonCancel.Left = this.ButtonOk.Left - 2 - this.ButtonCancel.Width;
			this.ButtonCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;



			//couleur des bouton de l'interface
			Color bcPen = Color.White;
			Color bcEraser = Color.White;
			Color bcIEraser = Color.White;
			Color bcPickColor = Color.White;
			Color bcFillColor = Color.White;
			if (this.ActualUserTool == UserTool.pen) { bcPen = Color.SkyBlue; }
			if (this.ActualUserTool == UserTool.eraser) { bcEraser = Color.SkyBlue; }
			if (this.ActualUserTool == UserTool.ieraser) { bcIEraser = Color.SkyBlue; }
			if (this.ActualUserTool == UserTool.pickcolor) { bcPickColor = Color.SkyBlue; }
			if (this.ActualUserTool == UserTool.fillcolor) { bcFillColor = Color.SkyBlue; }
			this.ButtonPen.BackColor = bcPen;
			this.ButtonEraser.BackColor = bcEraser;
			this.ButtonIEraser.BackColor = bcIEraser;
			this.ButtonPickColor.BackColor = bcPickColor;
			this.ButtonFillColor.BackColor = bcFillColor;


			this.ButtonTheColor.BackColor = this.UserColor;

		}





		#region event des control graphique
		private bool zzzIsMouseLeftDown = false;
		private bool zzzIsMouseInside = false;

		private void ImageBox_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.zzzIsMouseLeftDown = true;
				this.gimggcreate();

				if (this.ActualUserTool == UserTool.pen)
				{
					this.SaveCZ();

					Point impos = this.ImageMousePos;
					//this.ImgToEdit.SetPixel(impos.X, impos.Y, Color.Black);

					if (this.PenWidth <= 1)
					{
						this.ImgEdited.SetPixel(impos.X, impos.Y, this.UserColor);
					}
					else
					{
						this.gimgg.FillRectangle(this.UserColorB, impos.X - (this.PenWidth / 2), impos.Y - (this.PenWidth / 2), this.PenWidth, this.PenWidth);
					}


				}
				if (this.ActualUserTool == UserTool.eraser)
				{
					this.SaveCZ();

					Point impos = this.ImageMousePos;
					int ix = (impos.X - (this.EraserWidth / 2));
					int iy = (impos.Y - (this.EraserWidth / 2));

					this.gimgg.FillRectangle(Brushes.White, ix, iy, this.EraserWidth, this.EraserWidth);
					
				}
				if (this.ActualUserTool == UserTool.ieraser)
				{
					this.SaveCZ();

					Point impos = this.ImageMousePos;
					//valeurs de départ
					int ix = (impos.X - (this.PenWidth / 2));
					int iy = (impos.Y - (this.PenWidth / 2));
					//valeurs de fin
					int ex = ix + this.PenWidth - 1;
					int ey = iy + this.PenWidth - 1;


					//this.ImgEdited.SetPixel(ix, iy, Color.Red);
					for (int x = ix; x <= ex; x++)
					{
						for (int y = iy; y <= ey; y++)
						{
							//check si la pixel fait partie de l'image
							if (x >= 0 && y >= 0)
							{
								if (x < this.ImgEdited.Width && y < this.ImgEdited.Height)
								{
									this.ImgEdited.SetPixel(x, y, Color.Transparent);
								}
							}
						}
					}

					//this.gimgg.FillRectangle(Brushes.Transparent, ix, iy, this.PenWidth, this.PenWidth);
					//this.ImgEdited.SetPixel(ix, iy, Color.Transparent);
				}
				if (this.ActualUserTool == UserTool.pickcolor)
				{
					Point impos = this.ImageMousePos;
					int ix = impos.X;
					int iy = impos.Y;

					try
					{
						Color thec = this.ImgEdited.GetPixel(ix, iy);
						this.SetUserColor(thec);
						this.RefreshTextSize();
					}
					catch
					{
						this.SetUserColor(Color.Black);
						this.RefreshTextSize();
					}

				}
				if (this.ActualUserTool == UserTool.fillcolor)
				{
					this.SaveCZ();

					Point impos = this.ImageMousePos;
					int ix = impos.X;
					int iy = impos.Y;

					try
					{
						this.StartFillColor(ix, iy, this.UserColor);
					}
					catch
					{

					}
					

				}

			}

			this.uiRefreshImageToUser();
		}
		private void ImageBox_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.zzzIsMouseLeftDown = false;


				if (this.ActualUserTool == UserTool.pickcolor)
				{
					if (this.WasLastFillColor)
					{
						this.ActualUserTool = UserTool.fillcolor;
					}
					else
					{
						this.ActualUserTool = UserTool.pen;
					}
					this.RefreshTextSize();
				}

			}


			this.gimggdelete();
			this.uiRefreshImageToUser();
		}

		private int zzzmousemove_FrameSkip = 10; // 10
		private int zzzmousemove_FrameSkipState = 0;
		private void ImageBox_MouseMove(object sender, MouseEventArgs e)
		{
			this.zzzIsMouseInside = true;
			if (this.zzzIsMouseLeftDown)
			{

				if (this.ActualUserTool == UserTool.pen)
				{
					Point impos = this.ImageMousePos;
					if (this.PenWidth <= 1)
					{
						this.ImgEdited.SetPixel(impos.X, impos.Y, this.UserColor);
					}
					else
					{
						this.gimgg.FillRectangle(this.UserColorB, impos.X - (this.PenWidth / 2), impos.Y - (this.PenWidth / 2), this.PenWidth, this.PenWidth);
					}

				}
				if (this.ActualUserTool == UserTool.eraser)
				{

					Point impos = this.ImageMousePos;
					int ix = (impos.X - (this.EraserWidth / 2));
					int iy = (impos.Y - (this.EraserWidth / 2));

					this.gimgg.FillRectangle(Brushes.White, ix, iy, this.EraserWidth, this.EraserWidth);

					

					//if (this.zzzmousemove_FrameSkipState == 0)
					//{
					//	this.uiRefreshImageToUser();
					//}

				}
				if (this.ActualUserTool == UserTool.ieraser)
				{
					Point impos = this.ImageMousePos;
					//this.ImgEdited.SetPixel(impos.X, impos.Y, Color.Transparent);

					//valeurs de départ
					int ix = (impos.X - (this.PenWidth / 2));
					int iy = (impos.Y - (this.PenWidth / 2));
					//valeurs de fin
					int ex = ix + this.PenWidth - 1;
					int ey = iy + this.PenWidth - 1;


					//this.ImgEdited.SetPixel(ix, iy, Color.Red);
					for (int x = ix; x <= ex; x++)
					{
						for (int y = iy; y <= ey; y++)
						{
							//check si la pixel fait partie de l'image
							if (x >= 0 && y >= 0)
							{
								if (x < this.ImgEdited.Width && y < this.ImgEdited.Height)
								{
									this.ImgEdited.SetPixel(x, y, Color.Transparent);
								}
							}
						}
					}




				}


			}
			else
			{
				if (this.ActualUserTool == UserTool.pen)
				{
					this.ImageBox.Refresh();
					Graphics g = this.ImageBox.CreateGraphics();
					Point impos = this.ImageMousePos;

					int uix = (impos.X - (this.PenWidth / 2)) * this.ZoomLevel;
					int uiy = (impos.Y - (this.PenWidth / 2)) * this.ZoomLevel;
					int uiwidth = this.PenWidth * this.ZoomLevel;
					g.DrawRectangle(Pens.Red, uix, uiy, uiwidth, uiwidth);

					g.Dispose();
				}
				if (this.ActualUserTool == UserTool.eraser)
				{
					this.ImageBox.Refresh();
					Graphics g = this.ImageBox.CreateGraphics();
					Point impos = this.ImageMousePos;

					int uix = (impos.X - (this.EraserWidth / 2)) * this.ZoomLevel;
					int uiy = (impos.Y - (this.EraserWidth / 2)) * this.ZoomLevel;
					int uiwidth = this.EraserWidth * this.ZoomLevel;
					g.DrawRectangle(Pens.Red, uix, uiy, uiwidth, uiwidth);

					g.Dispose();
				}
				if (this.ActualUserTool == UserTool.ieraser)
				{
					//this.ImageBox.Refresh();
					//Graphics g = this.ImageBox.CreateGraphics();
					//Point impos = this.ImageMousePos;

					//int uix = (impos.X) * this.ZoomLevel;
					//int uiy = (impos.Y) * this.ZoomLevel;
					//int uiwidth = 1 * this.ZoomLevel;
					//g.DrawRectangle(Pens.Red, uix, uiy, uiwidth, uiwidth);

					//g.Dispose();


					this.ImageBox.Refresh();
					Graphics g = this.ImageBox.CreateGraphics();
					Point impos = this.ImageMousePos;

					int uix = (impos.X - (this.PenWidth / 2)) * this.ZoomLevel;
					int uiy = (impos.Y - (this.PenWidth / 2)) * this.ZoomLevel;
					int uiwidth = this.PenWidth * this.ZoomLevel;
					g.DrawRectangle(Pens.Red, uix, uiy, uiwidth, uiwidth);

					g.Dispose();

				}

			}



			//gestion du frameskip
			if (this.zzzmousemove_FrameSkipState <= 0)
			{
				this.uiRefreshImageToUser();
				this.zzzmousemove_FrameSkipState = this.zzzmousemove_FrameSkip;
			}
			this.zzzmousemove_FrameSkipState--;
		}
		private void ImageBox_MouseLeave(object sender, EventArgs e)
		{
			this.zzzIsMouseInside = false;
			if (this.ActualUserTool == UserTool.eraser)
			{
				//this.ImageBox.Refresh();
			}
			this.uiRefreshImageToUser();
		}


		private void nudZoom_ValueChanged(object sender, EventArgs e)
		{
			this.uiRefreshUiSize();
			this.uiRefreshImageToUser();

		}
		
		private void buttonEditCancel_Click(object sender, EventArgs e)
		{
			this.LoadCZ();
		}


		private void ButtonPen_Click(object sender, EventArgs e)
		{
			this.ActualUserTool = UserTool.pen;
			this.RefreshTextSize();
		}
		private void ButtonEraser_Click(object sender, EventArgs e)
		{
			this.ActualUserTool = UserTool.eraser;
			this.RefreshTextSize();
		}
		private void ButtonIEraser_Click(object sender, EventArgs e)
		{
			this.ActualUserTool = UserTool.ieraser;
			this.RefreshTextSize();
		}
		private void ButtonPickColor_Click(object sender, EventArgs e)
		{
			this.WasLastFillColor = this.ActualUserTool == UserTool.fillcolor;
			this.ActualUserTool = UserTool.pickcolor;
			this.RefreshTextSize();
		}
		private void ButtonFillColor_Click(object sender, EventArgs e)
		{
			this.ActualUserTool = UserTool.fillcolor;
			this.RefreshTextSize();
		}

		private void ButtonTheColor_Click(object sender, EventArgs e)
		{
			ColorDialog cdf = new ColorDialog();
			cdf.FullOpen = true;
			cdf.AnyColor = true;
			cdf.Color = this.UserColor;
			DialogResult rep = cdf.ShowDialog();
			if (rep == DialogResult.OK)
			{
				this.SetUserColor(cdf.Color);
				this.RefreshTextSize();
			}
		}

		private void ButtonOk_Click(object sender, EventArgs e)
		{
			this.DiagAccepted = true;
			this.Close();
		}
		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			this.DiagAccepted = false;
			this.Close();
		}



		#endregion
		#region edition de l'image
		private Color UserColor = Color.Black;
		private Brush UserColorB = Brushes.Black;
		private void SetUserColor(Color newcolor)
		{
			this.UserColor = newcolor;
			this.UserColorB = new SolidBrush(newcolor);
		}

		private int ZoomLevel
		{
			get
			{
				return (int)(this.nudZoom.Value);
			}
		}

		private enum UserTool
		{
			pen,
			eraser,
			ieraser,
			pickcolor,
			fillcolor
		}
		private UserTool ActualUserTool = UserTool.pen;

		private int PenWidth { get { return (int)(this.nudSize.Value); } } // = 1;
		private int EraserWidth { get { return this.PenWidth + 0; } } //= 5; //largeur du rectangle de l'efface en pixel

		bool WasLastFillColor = false; //lorsqu'on prend pick color, cette variable indique si l'outil précédant était le pen ou le fill color.


		//refresh la taille graphique de l'imagebox de l'édition
		public void uiRefreshUiSize()
		{
			this.ImageBox.Size = new Size(this.ImgEdited.Width * this.ZoomLevel, this.ImgEdited.Height * this.ZoomLevel);


		}
		//refresh l'image à afficher à l'utilisateur
		public void uiRefreshImageToUser()
		{
			Bitmap img = new Bitmap(this.ImageBox.Width, this.ImageBox.Height);
			Graphics g = Graphics.FromImage(img);
			g.Clear(Color.DimGray);
			
			Point mpos = this.MousePos;
			//dessine en arrière plan un cercle bleu pour que l'utilisateur puisse voir les zone transparente
			if (this.zzzIsMouseInside)
			{
				g.FillEllipse(Brushes.Blue, mpos.X - 30, mpos.Y - 30, 60, 60);
			}


			if (this.ZoomLevel == 1)
			{
				g.DrawImage(this.ImgEdited, 0, 0);
			}
			else
			{
				g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
				//g.DrawImage(this.ImgToEdit, new Rectangle(this.ZoomLevel / 2, 0, img.Width - 1, this.ZoomLevel * 1), new Rectangle(0, 0, this.ImgToEdit.Width, 1), GraphicsUnit.Pixel);
				g.DrawImage(this.ImgEdited, new Rectangle(this.ZoomLevel / 2, 0, img.Width, this.ZoomLevel * 1), new Rectangle(0, 0, this.ImgEdited.Width, 1), GraphicsUnit.Pixel);


				Region oldr = g.Clip;
				Region r = new Region(new Rectangle(0, 0, this.ZoomLevel / 2, img.Height));
				g.Clip = r;
				g.DrawImage(this.ImgEdited, new Rectangle(0, this.ZoomLevel / 2, this.ZoomLevel * 1, img.Height), new Rectangle(0, 0, 1, this.ImgEdited.Height), GraphicsUnit.Pixel);
				//g.DrawImage(this.ImgToEdit, new Rectangle(this.ZoomLevel / 2, this.ZoomLevel / 2, this.ZoomLevel * 1, img.Height), new Rectangle(0, 0, 1, this.ImgToEdit.Height), GraphicsUnit.Pixel);
				//g.Clear(Color.Red);
				g.Clip = oldr;


				//la petite pixel en haut à gauche
				if (this.ZoomLevel > 1)
				{
					g.FillRectangle(new SolidBrush(this.ImgEdited.GetPixel(0, 0)), 0, 0, this.ZoomLevel / 2, this.ZoomLevel / 2);
				}



				g.DrawImage(this.ImgEdited, this.ZoomLevel / 2, this.ZoomLevel / 2, img.Width, img.Height);
			}

			g.Dispose();
			if (this.ImageBox.Image != null) { this.ImageBox.Image.Dispose(); }
			this.ImageBox.Image = img;
			this.ImageBox.Refresh();
		}




		#endregion






		private bool[,] fillcolorDone; //grille qui indique quelle case ont été traité pendant le fill color
		private void StartFillColor(int x, int y, Color NewColor)
		{

			//initialise la grille
			this.fillcolorDone = new bool[this.ImgEdited.Width, this.ImgEdited.Height];
			for (int ay = 0; ay < this.ImgEdited.Height; ay++)
			{
				for (int ax = 0; ax < this.ImgEdited.Width; ax++)
				{
					this.fillcolorDone[ax, ay] = false;
				}
			}

			Color StartColor = this.ImgEdited.GetPixel(x, y);
			//this.ImgEdited.SetPixel(x, y, NewColor);

			this.fillcolor_SingleCell(x, y, StartColor, NewColor);

		}
		private void fillcolor_SingleCell(int x, int y, Color ValidColor, Color NewColor)
		{
			//check les bound
			if (x >= 0 && x < this.ImgEdited.Width)
			{
				if (y >= 0 && y < this.ImgEdited.Height)
				{
					//check si la case a déjà été fait
					if (!this.fillcolorDone[x, y])
					{
						Color CaseColor = this.ImgEdited.GetPixel(x, y);
						if (CaseColor.R == ValidColor.R && CaseColor.G == ValidColor.G && CaseColor.B == ValidColor.B && CaseColor.A == ValidColor.A)
						{
							//si toute les condition sont correcte et que la case est de la bonne couleur, il faut définir la nouvelle couleur de la case et relancer la void
							this.ImgEdited.SetPixel(x, y, NewColor);

							this.fillcolorDone[x, y] = true; //doit être fait avant de caller les autre

							this.fillcolor_SingleCell(x - 1, y, ValidColor, NewColor);
							this.fillcolor_SingleCell(x + 1, y, ValidColor, NewColor);
							this.fillcolor_SingleCell(x, y - 1, ValidColor, NewColor);
							this.fillcolor_SingleCell(x, y + 1, ValidColor, NewColor);

						}
					}
				}
			}
		}



	}
}
