using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace FactorioOrganizer
{
	public class uiMapEditer
	{
		private Point MousePos { get { return this.ImageBox.PointToClient(Cursor.Position); } }
		private PointF MouseVirtualPos
		{
			get
			{
				Point mpos = this.MousePos;
				return this.ConvertUiToVirtual(mpos.X, mpos.Y);
			}
		}

		private PictureBox ImageBox;

		public oMap Map;
		public void SetMap(oMap newmap)
		{
			this.Map = newmap;
			this.RefreshImage();
			GC.Collect();
		}



		public int Top
		{
			get { return this.ImageBox.Top; }
			set { this.ImageBox.Top = value; }
		}
		public int Left
		{
			get { return this.ImageBox.Left; }
			set { this.ImageBox.Left = value; }
		}
		public int Width
		{
			get { return this.ImageBox.Width; }
			set { this.ImageBox.Width = value; }
		}
		public int Height
		{
			get { return this.ImageBox.Height; }
			set { this.ImageBox.Height = value; }
		}
		public Control Parent
		{
			get { return this.ImageBox.Parent; }
			set { this.ImageBox.Parent = value; }
		}
		public DockStyle Dock
		{
			get { return this.ImageBox.Dock; }
			set { this.ImageBox.Dock = value; }
		}
		public AnchorStyles Anchor
		{
			get { return this.ImageBox.Anchor; }
			set { this.ImageBox.Anchor = value; }
		}




		//position virtuel
		public PointF vpos = new PointF(0f, 0f); //position du centre de l'image
		public float VirtualWidth = 5f; //represents the width the the virtual area shown in the picture box








		public uiMapEditer(oMap StartMap)
		{
			this.Map = StartMap;

			this.ImageBox = new PictureBox();
			this.ImageBox.BorderStyle = BorderStyle.FixedSingle;
			this.ImageBox.SizeChanged += new EventHandler(this.ImageBox_SizeChanged);
			this.ImageBox.MouseDown += new MouseEventHandler(this.ImageBox_MouseDown);
			this.ImageBox.MouseUp += new MouseEventHandler(this.ImageBox_MouseUp);
			this.ImageBox.MouseWheel += new MouseEventHandler(this.ImageBox_MouseWheel);
			this.ImageBox.MouseMove += new MouseEventHandler(this.ImageBox_MouseMove);

			this.CreateTimerDragAndDrop();

			this.RefreshImage();
		}
		private void ImageBox_SizeChanged(object sender, EventArgs e)
		{
			this.RefreshImage();
		}
		private void ImageBox_MouseDown(object sender, MouseEventArgs e)
		{
			PointF mpos = this.MouseVirtualPos; //this.ConvertUiToVirtual(this.MousePos.X, this.MousePos.Y);

			if (e.Button == MouseButtons.Left)
			{
				if (!this.IsAddMode) //if no "mode" (move the map/map object) is "in process" ////    si aucun mode est en cour
				{
					MapObject mo = this.Map.GetObjThatTouch(mpos.X, mpos.Y);
					//if rep == null, the user clicked ~"in the void"=(user didn't clicked on any object) so we begin drag and drop //// si rep == null, l'utilisateur a clické dans le vide et on fait un drag and drop
					if (mo == null)
					{
						this.StartDragAndDrop();


					}
					else //the user clicked on an object ////    l'utilisateur a clické sur un object
					{
						this.StartDragAndDropMO(mo);


					}
				}
				else if (this.IsAddMode)
				{

				}
			}
			if (e.Button == MouseButtons.Right)
			{
				//if the user is in addmode AND he make a right click, instead of finishing addmode it'll place a copy of that object on the map and addmode continue ////    si l'utilisateur est en addmode et qu'il fait un click droit, plutot que terminer le addmode, il va installer une copy de l'object et le addmode se poursuit
				if (this.IsAddMode)
				{
					MapObject copy = this.addmodeMO.GetCopy();
					PointF mvpos = this.MouseVirtualPos;
					copy.vpos.X = mvpos.X;
					copy.vpos.Y = mvpos.Y;
					this.Map.listMO.Add(copy);
					this.RefreshImage();
				}
			}

		}
		private void ImageBox_MouseUp(object sender, MouseEventArgs e)
		{
			PointF mvpos = this.MouseVirtualPos; //this.ConvertUiToVirtual(this.MousePos.X, this.MousePos.Y);


			if (e.Button == MouseButtons.Left)
			{
				if (this.IsDragAndDrop) { this.StopDragAndDrop(); }
				if (this.IsDragAndDropMO) { this.StopDragAndDropMO(); }
				if (this.IsAddMode) { this.StopAddMode(); }

			}
			if (e.Button == MouseButtons.Right)
			{
				if (!this.IsAddMode && !this.IsDragAndDrop && !this.IsDragAndDropMO)
				{
					MapObject mo = this.Map.GetObjThatTouch(mvpos.X, mvpos.Y);
					if (mo != null)
					{
						string optToggleNeedCoal = "Toggle Need Coal";
						string optRemove = "Remove";

						oRightClick3 rc = new oRightClick3();
						//rc.Width = 300;
						rc.AddChoice(optToggleNeedCoal);
						rc.AddSeparator();
						rc.AddChoice(optRemove);

						//ajoute les inputs et output
						if (mo.MapType == MOType.Machine)
						{
							rc.AddSeparator();
							rc.AddSeparator();
							rc.AddChoice("Outputs :");
							foreach (FOType ft in mo.Outputs)
							{
								rc.AddChoice("-" + ft.ToString());
							}
							rc.AddChoice("");
							rc.AddChoice("Inputs :");
							foreach (FOType ft in mo.Inputs)
							{
								rc.AddChoice("-" + ft.ToString());
							}

						}

						string rep = rc.GetChoice();
						if (rep == optToggleNeedCoal)
						{
							mo.NeedCoal = !mo.NeedCoal;
						}
						if (rep == optRemove)
						{
							try
							{
								this.Map.listMO.Remove(mo);
							}
							catch { }
						}

						//check the one who correspond to the click FOType, if there is one ////    check si ca correspond à un FOType
						if (mo.MapType == MOType.Machine)
						{
							List<FOType> allft = Utilz.GetListOfAllFOType();
							foreach (FOType ft in allft)
							{
								if (ft.ToString() == rep.Replace("-", string.Empty).Trim())
								{
									MapObject newmo = new MapObject(MOType.Belt, ft);
									this.StartAddMode(newmo);
									break;
								}
							}
						}



						this.RefreshImage();
					}
				}
			}

		}
		private void ImageBox_MouseWheel(object sender, MouseEventArgs e)
		{
			float asdfspeed = 1.225f;
			if (e.Delta > 10)
			{
				this.VirtualWidth /= asdfspeed;
				this.RefreshImage();
			}
			if (e.Delta < -10)
			{
				this.VirtualWidth *= asdfspeed;
				this.RefreshImage();
			}
		}
		private void ImageBox_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.IsDragAndDropMO)
			{
				this.dadmoMO.vpos = this.MouseVirtualPos;
				
				this.ImageBox.Refresh();
				Graphics g = this.ImageBox.CreateGraphics();
				int uiradius = (int)(this.dadmoMO.VirtualWidth * (float)(this.Width) / this.VirtualWidth / 2f + 0.5f); //graphical radius ////    rayon graphique de l'objet
				Point uipos = this.ConvertVirtualToUi(this.dadmoMO.vpos.X, this.dadmoMO.vpos.Y);
				if (this.dadmoMO.MapType == MOType.Belt)
				{
					g.DrawEllipse(Pens.Silver, uipos.X - uiradius, uipos.Y - uiradius, 2 * uiradius, 2 * uiradius);
				}
				else
				{
					g.DrawRectangle(Pens.Silver, uipos.X - uiradius, uipos.Y - uiradius, 2 * uiradius, 2 * uiradius);
				}
				g.Dispose();
			}
			if (this.IsAddMode)
			{
				this.addmodeMO.vpos = this.MouseVirtualPos;

				this.ImageBox.Refresh();
				Graphics g = this.ImageBox.CreateGraphics();
				int uiradius = (int)(this.addmodeMO.VirtualWidth * (float)(this.Width) / this.VirtualWidth / 2f + 0.5f); //graphical radius ////    rayon graphique de l'objet
				Point uipos = this.ConvertVirtualToUi(this.addmodeMO.vpos.X, this.addmodeMO.vpos.Y);
				if (this.addmodeMO.MapType == MOType.Belt)
				{
					g.DrawEllipse(Pens.Silver, uipos.X - uiradius, uipos.Y - uiradius, 2 * uiradius, 2 * uiradius);
				}
				else
				{
					g.DrawRectangle(Pens.Silver, uipos.X - uiradius, uipos.Y - uiradius, 2 * uiradius, 2 * uiradius);
				}
				g.Dispose();
			}
			if (this.keyBackSpaceDown)
			{
				this.keyBackSpaceDown = true;
				//remove the element under the mouse ////    supprimme l'élément sous la souris
				PointF mvpos = this.MouseVirtualPos;
				MapObject mo = this.Map.GetObjThatTouch(mvpos.X, mvpos.Y);
				if (mo != null)
				{
					try
					{
						this.Map.listMO.Remove(mo);
					}
					catch { }
					this.RefreshImage();
				}
			}
		}

		


		private PointF ConvertUiToVirtual(int x, int y)
		{
			PointF rep = new PointF(0f, 0f);
			float rap = this.VirtualWidth / (float)(this.Width);
			rep.X = this.vpos.X + (((float)x - ((float)(this.Width) / 2f)) * rap);
			rep.Y = this.vpos.Y + ((((float)(this.Height) / 2f) - (float)y) * rap);
			return rep;
		}
		private PointF ConvertUiToVirtual(Point uipos)
		{
			return this.ConvertUiToVirtual(uipos.X, uipos.Y);
		}
		private Point ConvertVirtualToUi(float vx, float vy)
		{
			Point rep = new Point(0, 0);
			float rap = (float)(this.Width) / this.VirtualWidth;
			rep.X = (int)(((float)(this.Width) / 2f) + ((vx - this.vpos.X) * rap) + 0.5f); //convertion to integer is always rounded down. i add 0.5 to round it to the closest integer. i think negative number are rounded up (rounded down in absolute value) but we don't care about neg number because they are out of the screen.
			rep.Y = (int)(((float)(this.Height) / 2f) - ((vy - this.vpos.Y) * rap) + 0.5f);
			return rep;
		}
		private Point ConvertVirtualToUi(PointF vpos)
		{
			return this.ConvertVirtualToUi(vpos.X, vpos.Y);
		}


		public void RefreshImage()
		{
			int imgWidth = this.Width;
			int imgHeight = this.Height;
			if (imgWidth < 100) { imgWidth = 100; }
			if (imgHeight < 100) { imgHeight = 100; }
			Bitmap img = new Bitmap(imgWidth, imgHeight);
			Graphics g = Graphics.FromImage(img);
			g.Clear(Color.FromArgb(32, 32, 32));



			//draw links between objects ////    dessine les lien entre les object
			foreach (MapObject mo in this.Map.listMO)
			{
				Point mouipos = this.ConvertVirtualToUi(mo.vpos.X, mo.vpos.Y);

				try
				{
					if (mo.MapType == MOType.Belt)
					{
						MapObject[] twomo = this.Map.GetCompatible2ObjsCloseTo(mo);
						if (twomo[0] != null)
						{
							Point mouipos2 = this.ConvertVirtualToUi(twomo[0].vpos.X, twomo[0].vpos.Y);
							g.DrawLine(Pens.Silver, mouipos, mouipos2);
						}
						if (twomo[1] != null)
						{
							//i make sure the angle is not too small. it's just one of the check that makes the drawing of links not anoying. the purpose of the program is to draw somewhat useull links. i thought like 15 min about a program to help desing factories and i thought about how i would program the drawing of links and i come with these idea : 2 links per belt, angle not too small.
							//check the angle with the cosine law ////    il doit checker l'angle avec la loi des cosinus
							float a = (float)(Math.Sqrt((twomo[1].vpos.X - mo.vpos.X) * (twomo[1].vpos.X - mo.vpos.X) + (twomo[1].vpos.Y - mo.vpos.Y) * (twomo[1].vpos.Y - mo.vpos.Y)));
							float b = (float)(Math.Sqrt((twomo[0].vpos.X - mo.vpos.X) * (twomo[0].vpos.X - mo.vpos.X) + (twomo[0].vpos.Y - mo.vpos.Y) * (twomo[0].vpos.Y - mo.vpos.Y)));
							float c = (float)(Math.Sqrt((twomo[1].vpos.X - twomo[0].vpos.X) * (twomo[1].vpos.X - twomo[0].vpos.X) + (twomo[1].vpos.Y - twomo[0].vpos.Y) * (twomo[1].vpos.Y - twomo[0].vpos.Y)));
							float angle = (float)(Math.Acos(((a * a) + (b * b) - (c * c)) / 2f / a / b));
							if (angle > 1f) //i tried 1, i was satisfied
							{
								Point mouipos2 = this.ConvertVirtualToUi(twomo[1].vpos.X, twomo[1].vpos.Y);
								g.DrawLine(Pens.Silver, mouipos, mouipos2);
							}
						}
					}
					else if (mo.MapType == MOType.Machine)
					{

						//draw links of every output
						foreach (FOType ft in mo.Outputs)
						{
							MapObject closest = this.Map.FindClosestBeltWithInput(mo, ft);
							if (closest != null)
							{
								Point mouipos2 = this.ConvertVirtualToUi(closest.vpos.X, closest.vpos.Y);
								g.DrawLine(Pens.Orchid, mouipos, mouipos2); // Crimson
							}
						}
						

						//draw links of every inputs
						mo.IsAllInputPresent = true;
						foreach (FOType ft in mo.Inputs)
						{
							MapObject closest = this.Map.GetCompatibleBeltCloseTo(mo, ft); //get the closest one ////    obtien selui qui est le plus proche
							if (closest != null)
							{
								Point mouipos2 = this.ConvertVirtualToUi(closest.vpos.X, closest.vpos.Y);
								g.DrawLine(Pens.SkyBlue, mouipos, mouipos2);
							}
							else
							{
								mo.IsAllInputPresent = false;
							}
						}

						//check if it's a furnace and if so, it draw coal if needed ////    check le charbon des four
						if (mo.IsFurnace)
						{
							if (mo.NeedCoal)
							{
								MapObject close2 = this.Map.GetCompatibleBeltCloseTo(mo, FOType.Coal); //get the closest coal belt ////    récupère la belt de charbon la plus près
								if (close2 != null)
								{
									Point mouipos2 = this.ConvertVirtualToUi(close2.vpos.X, close2.vpos.Y);
									g.DrawLine(Pens.SkyBlue, mouipos, mouipos2);
								}
								else
								{
									mo.IsAllInputPresent = false;
								}
							}
						}



					}

				}
				catch { }

			}

			Brush brushMachineBackBrush = new SolidBrush(Color.FromArgb(64, 64, 64));

			//draw object
			foreach (MapObject mo in this.Map.listMO)
			{
				Point uipos = this.ConvertVirtualToUi(mo.vpos.X, mo.vpos.Y);

				//draw background
				int uiradius = (int)(mo.VirtualWidth * (float)(this.Width) / this.VirtualWidth / 2f + 0.5f); //graphical radius ////    rayon graphique de l'objet
				if (mo.MapType == MOType.Belt)
				{
					g.FillEllipse(Brushes.DimGray, uipos.X - uiradius, uipos.Y - uiradius, 2 * uiradius, 2 * uiradius);
				}
				if (mo.MapType == MOType.Machine)
				{
					Brush moBackColor = brushMachineBackBrush; //Brushes.DimGray;
					Brush FurnaceLabelColor = Brushes.White;
					if (!mo.IsAllInputPresent) { moBackColor = Brushes.Crimson; FurnaceLabelColor = Brushes.White; }
					g.FillRectangle(moBackColor, uipos.X - uiradius, uipos.Y - uiradius, 2 * uiradius, 2 * uiradius);
					g.DrawRectangle(Pens.Silver, uipos.X - uiradius, uipos.Y - uiradius, 2 * uiradius, 2 * uiradius);
					if (mo.IsFurnace && uiradius >= 25)
					{
						Font fonte = new Font("consolas", 10f);
						g.DrawString("Furnace", fonte, FurnaceLabelColor, (float)(uipos.X - uiradius), (float)(uipos.Y - uiradius));
						if (mo.NeedCoal)
						{
							g.DrawString("NeedCoal", fonte, FurnaceLabelColor, (float)(uipos.X - uiradius), (float)(uipos.Y - uiradius + 15));
						}
					}
				}

				//get and draw image
				Bitmap moimg = mo.GetImage();
				if (moimg != null)
				{
					//draw the image only if the image is not too big compared to the graphical size of the object ////    dessine l'image seulement si elle n'est pas raisonnablement plus grande
					if (moimg.Width <= uiradius * 8)
					{
						try
						{
							g.DrawImage(moimg, uipos.X - (moimg.Width / 2), uipos.Y - (moimg.Height / 2));
						}
						catch { }
					}
				}



			}

			g.Dispose();
			if (this.ImageBox.Image != null) { this.ImageBox.Image.Dispose(); }
			this.ImageBox.Image = img;
			this.ImageBox.Refresh();
			GC.Collect();
		}





		#region drag and drop

		private Timer TimerDrag;
		private void CreateTimerDragAndDrop()
		{
			this.TimerDrag = new Timer();
			this.TimerDrag.Interval = 150;
			this.TimerDrag.Tick += new EventHandler(this.TimerDrag_Tick);
		}


		private bool IsDragAndDrop = false; //indique si un dad est en cour
		private PointF dadStartVPos = new PointF(0f, 0f);
		private Point dadStartPos = new Point(-1, -1);
		private void StartDragAndDrop()
		{
			this.IsDragAndDrop = true;
			this.dadStartPos = this.MousePos;
			this.dadStartVPos = this.MouseVirtualPos;
			this.TimerDrag.Start();

			this.keyBackSpaceDown = false;
		}
		private void StopDragAndDrop()
		{
			if (this.IsDragAndDrop)
			{
				this.IsDragAndDrop = false;
				PointF mvpos = this.MouseVirtualPos;
				this.vpos.X += this.dadStartVPos.X - mvpos.X;
				this.vpos.Y += this.dadStartVPos.Y - mvpos.Y;
				this.TimerDrag.Stop();
				this.RefreshImage();
			}
		}

		private void TimerDrag_Tick(object sender, EventArgs e)
		{
			Point mpos = this.MousePos;

			int dx = mpos.X - this.dadStartPos.X;
			int dy = mpos.Y - this.dadStartPos.Y;

			Graphics g = this.ImageBox.CreateGraphics();
			g.Clear(Color.FromArgb(24, 24, 24));
			g.DrawImage(this.ImageBox.Image, dx, dy);
			g.Dispose();


			if (!this.IsDragAndDrop) { this.TimerDrag.Stop(); }
		}


		#endregion
		#region drag and drop MapObject

		private MapObject dadmoMO = null; //the one actually moved ////    MapObject actuellement déplacé
		private bool IsDragAndDropMO = false; //indicate if a dadmo is "in process" ////    indique si un dadmo est en cour
		//private PointF dadmoDelta = new PointF(0f, 0f); //décalage entre la souris
		private void StartDragAndDropMO(MapObject mo)
		{
			this.IsDragAndDropMO = true;
			this.dadmoMO = mo;
			//PointF mvpos = this.MouseVirtualPos;


			this.dadmoMO.vpos = this.MouseVirtualPos;
			//this.RefreshImage();
		}
		private void StopDragAndDropMO()
		{
			this.IsDragAndDropMO = false;
			this.dadmoMO.vpos = this.MouseVirtualPos;
			this.RefreshImage();
		}

		#endregion
		#region add mode

		private bool IsAddMode = false; //indicate if we are in addmode ////    indique si this est actuellement en AddMode
		private MapObject addmodeMO = null; //the object to add
		public void StartAddMode(MapObject mo)
		{
			this.IsAddMode = true;
			this.addmodeMO = mo;
		}
		public void StopAddMode()
		{
			this.IsAddMode = false;
			PointF mvpos = this.MouseVirtualPos;
			this.addmodeMO.vpos = mvpos; //define position ////    défini la position
			this.Map.listMO.Add(this.addmodeMO); //add it to the map ////    l'ajoute à la map
			this.RefreshImage();
		}
		public void CancelAddMode()
		{
			this.IsAddMode = false;
			this.RefreshImage();
		}

		#endregion




		//keyboard shortcut ////    raccourci clavier
		private bool keyBackSpaceDown = false;
		public void KeyDown(Keys k)
		{
			if (k == Keys.Back || k == Keys.Space)
			{
				this.keyBackSpaceDown = true;
				//remove the element under the mouse ////    supprimme l'élément sous la souris
				PointF mvpos = this.MouseVirtualPos;
				MapObject mo = this.Map.GetObjThatTouch(mvpos.X, mvpos.Y); //return null if there's nothing
				if (mo != null)
				{
					try
					{
						this.Map.listMO.Remove(mo);
					}
					catch { }
					this.RefreshImage();
				}
			}
			if (k == Keys.Q)
			{
				//copy the element under the mouse ////    copie l'élément sous la souris
				if (!this.IsAddMode)
				{
					PointF mvpos = this.MouseVirtualPos;
					MapObject mo = this.Map.GetObjThatTouch(mvpos.X, mvpos.Y); //return null if there's nothing
					if (mo != null)
					{
						try
						{
							MapObject copy = mo.GetCopy();
							this.StartAddMode(copy);
						}
						catch { }
						this.RefreshImage();
					}
				}
				else //if the user is already in addmode, we want q to cancel addmode ////    si l'utilisateur est déjà en addmode, q va le faire quitter le addmode
				{
					this.CancelAddMode();
				}
			}
			if (k == Keys.W)
			{ //copy the element under the mouse but it makes a copy for the other MOType ////    copie l'élément sous la souris, mais si c'est une machine, il va donner une belt avec le output de la machine et inversement
				if (!this.IsAddMode)
				{
					PointF mvpos = this.MouseVirtualPos;
					MapObject mo = this.Map.GetObjThatTouch(mvpos.X, mvpos.Y); //return null if there's nothing
					if (mo != null)
					{
						try
						{
							if (mo.MapType == MOType.Machine)
							{
								FOType ft = mo.TheRecipe;
								//if (mo.MapType == MOType.Machine)
								//{
								//	ft = mo.TheRecipe;
								//}
								MapObject copy = new MapObject(MOType.Belt, ft);
								this.StartAddMode(copy);
							}
							if (mo.MapType == MOType.Belt)
							{
								FOType ft = mo.BeltOutput;
								MapObject copy = new MapObject(MOType.Machine, ft);
								this.StartAddMode(copy);
							}
						}
						catch { }
						this.RefreshImage();
					}
				}

			}
			if (k == Keys.F)
			{ //toggle NeedCoal
				PointF mvpos = this.MouseVirtualPos;
				MapObject mo = this.Map.GetObjThatTouch(mvpos.X, mvpos.Y); //return null if there's nothing
				if (mo != null)
				{
					try
					{
						mo.NeedCoal = !mo.NeedCoal;
					}
					catch { }
					this.RefreshImage();
				}
			}

			//alternative zoom control
			if (k == Keys.D1 || k == Keys.NumPad1)
			{
				this.VirtualWidth *= 1.5f;
				this.RefreshImage();
			}
			if (k == Keys.D2 || k == Keys.NumPad2)
			{
				this.VirtualWidth /= 1.5f;
				this.RefreshImage();
			}
		}
		public void KeyUp(Keys k)
		{
			if (k == Keys.Back || k == Keys.Space)
			{
				this.keyBackSpaceDown = false;
			}
		}


	}
}
