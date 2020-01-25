using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using FactorioOrganizer.Dialogs;
using FactorioOrganizer.RandomImports;

namespace FactorioOrganizer
{
	//this object/"graphical control" allow the user to interact with a ModImage
	public class uiModCraftManager
	{
		private Point MousePos { get { return this.ImageBox.PointToClient(Cursor.Position); } }
		public Rectangle MouseRec { get { return new Rectangle(this.MousePos, new Size(1, 1)); } }
		private bool IsMouseLeftDown = false; //true if the mouse left button is down
		private bool IsMouseLeftDownOnItemArea = false; //true if the mouse left button was down(ed) on the item area. it's used to not pop the right click menu if the user didn't clicked on an item.
		
		private PictureBox ImageBox;

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


		public oMod Mod = null;
		public bool AnyModLoaded { get { return this.Mod != null; } }
		public void SetMod(oMod ModToLoad)
		{
			this.Mod = ModToLoad;

			this.IndexOfTopItem = 0; //reset the scrolling
			this.UnselectIndex();

		}

		private uiModItemManager ItemManager = null;
		public bool IsItemManagerDefined { get { return this.ItemManager != null; } }
		public void SetItemManager(uiModItemManager themanager)
		{
			this.ItemManager = themanager;
		}


		public event EventHandler UserDidSomeChange; //raised when the user edit anything
		private void Raise_UserDidSomeChange()
		{
			if (this.UserDidSomeChange != null)
			{
				this.UserDidSomeChange(this, new EventArgs());
			}
		}

		private void voidnew()
		{
			this.ImageBox = new PictureBox();
			this.ImageBox.BorderStyle = BorderStyle.FixedSingle;
			this.ImageBox.BackColor = Color.Blue;
			this.ImageBox.SizeChanged += new EventHandler(this.ImageBox_SizeChanged);
			this.ImageBox.MouseDown += new MouseEventHandler(this.ImageBox_MouseDown);
			this.ImageBox.MouseUp += new MouseEventHandler(this.ImageBox_MouseUp);

			this.ImageBox.MouseMove += new MouseEventHandler(this.ImageBox_MouseMove);
			this.ImageBox.MouseLeave += new EventHandler(this.ImageBox_MouseLeave);
			this.ImageBox.MouseWheel += new MouseEventHandler(this.ImageBox_MouseWheel);


			this.CreateScrollBar();
			this.CreateMenu();

			this.RefreshImage();

			//this.TESTEST();
		}
		public uiModCraftManager()
		{
			this.voidnew();
			
		}
		private void ImageBox_SizeChanged(object sender, EventArgs e)
		{
			this.RefreshSize();
			this.RefreshImage();
		}
		private void ImageBox_MouseDown(object sender, MouseEventArgs e)
		{
			Rectangle mrec = this.MouseRec;
			if (e.Button == MouseButtons.Left)
			{
				this.IsMouseLeftDown = true;

				this.ECHECK_MouseLeftDown();
				//if mouse is not on any controls, we check what the user clicked
				if (!this.IsMouseOnAnyControl())
				{
					//check if the user clicked on the scroll zone
					if (mrec.X > this.Width - this.uiScrollZoneWidth)
					{
						//check if the user clicked on the scroll zone or if he clicked on the background of the button
						if (this.recScrollZone.IntersectsWith(mrec))
						{
							//because the user clicked on the scroll zone, we move the scroll bar to where he clicked
							int clicky = mrec.Y - this.recScrollZone.Y;
							this.UserClickOnScrollZone(clicky);
							//this.ReadjustScrollZone();
							//the image is refreshed outside the ifs blocks
						}

					}
					else
					{
						//check if the user clicked on the "menu" background or if he clicked on the zone of item
						if (mrec.Y > this.uiMenuHeight)
						{
							////the user clicked on the area of item
							this.IsMouseLeftDownOnItemArea = true;

						}
						else ////the user clicked on the "menu" background
						{

						}
					}

				}

				this.RefreshImage();
			}
			if (e.Button == MouseButtons.Right)
			{



			}
		}
		private void ImageBox_MouseUp(object sender, MouseEventArgs e)
		{
			Rectangle mrec = this.MouseRec;

			this.UnselectIndex();

			if (e.Button == MouseButtons.Left)
			{
				this.IsMouseLeftDown = false;

				this.ECHECK_MouseLeftUp();

				//if mouse is not on any controls, we check what the user clicked
				if (!this.IsMouseOnAnyControl())
				{
					//check if the mouse was on the scroll zone
					if (mrec.X > this.Width - this.uiScrollZoneWidth)
					{
						//check if the mouse was on the scroll zone or if it was on the background of the scrolling button
						if (this.recScrollZone.IntersectsWith(mrec))
						{

						}

					}
					else
					{
						//check if the user clicked on the "menu" background or if he is on the zone of item
						if (mrec.Y > this.uiMenuHeight)
						{
							////the user is on the area of item
							if (this.IsMouseLeftDownOnItemArea)
							{
								//get the index of the mouse
								int mouseindex = this.GetIndexOfMouse();
								//make sure that the index exist
								if (mouseindex != -1)
								{
									//oMod.ModItem mi = this.listItems[mouseindex];
									oMod.ModCraft mc = this.listCraft[mouseindex];

									string optEdit = "Edit ...";
									string optDuplicate = "Duplicate craft";

									string optRemove = "Remove";

									oRightClick3 rc = new oRightClick3();
									rc.AddChoice(optEdit);
									rc.AddChoice(optDuplicate);
									rc.AddSeparator();
									rc.AddChoice(optRemove);
									rc.AddSeparator();
									rc.AddSeparator();

									//show to the user what are the inputs and the outputs. the user can click on them
									rc.AddChoice("Recipe :");
									string str = "-" + mc.Recipe.ItemName; //this variable is recycled after
									if (mc.Recipe.ModName != "-") { str += "(" + mc.Recipe.ModName + ")"; }
									rc.AddChoice(str);
									rc.AddChoice("");
									rc.AddChoice("Inputs :");
									foreach (refItem ri in mc.Inputs)
									{
										str = "-" + ri.ItemName;
										if (ri.ModName != "-") { str += "(" + ri.ModName + ")"; }
										rc.AddChoice(str);
									}
									rc.AddChoice("");
									rc.AddChoice("Outputs :");
									foreach (refItem ri in mc.Outputs)
									{
										str = "-" + ri.ItemName;
										if (ri.ModName != "-") { str += "(" + ri.ModName + ")"; }
										rc.AddChoice(str);
									}


									//get the choice of the user
									string rep = rc.GetChoice();
									if (rep == optEdit)
									{
										//create and setup a dialog to edit the craft
										DlgModCraftEditer dmce = new DlgModCraftEditer(this.Mod);
										dmce.Title = "Edit Mod Craft";
										//set the params to fit the current state of the item
										dmce.SetRecipe(mc.Recipe);
										dmce.SetInputs(mc.Inputs);
										dmce.SetOutputs(mc.Outputs);
										dmce.SetIsMadeInFurnace(mc.IsMadeInFurnace);
										//pop it
										dmce.ShowDialog();
										if (dmce.DialogResult == DialogResult.OK)
										{
											//set the new params to the craft
											mc.Recipe = dmce.GetRecipe();
											mc.Inputs = dmce.GetInputs();
											mc.Outputs = dmce.GetOutputs();
											mc.IsMadeInFurnace = dmce.GetIsMadeInFurnace();

											this.Raise_UserDidSomeChange();
										}

									}
									if(rep == optDuplicate)
									{
										oMod.ModCraft newmc = mc.GetCopy();
										this.Mod.listCrafts.Insert(mouseindex + 1, newmc);
										this.SelectIndex(mouseindex + 1);

										this.Raise_UserDidSomeChange();
									}
									if (rep == optRemove)
									{
										//we pop to the user a confirmation dialog
										DialogResult UserSure = MessageBox.Show("Are you sure?", "Factorio Organizer", MessageBoxButtons.YesNo);
										if (UserSure == DialogResult.Yes)
										{
											//remove it
											this.listCraft.Remove(mc);

											this.Raise_UserDidSomeChange();
										}
									}

									//if the user clicked on an input or an output or the recipe, we "select" this item in the item manager
									if (rep.Length > 1)
									{
										//if the rep beggings by -, the user clicked on an item
										if (rep.Substring(0, 1) == "-")
										{
											//because the user clicked on an item, we search where this item is in the list and we select it for the user.
											//how we find which item the user clicked : we go through every existing item and generate for each item how it would have been write in the right click menu. we stop when we found the one that generate the exact same string.

											bool found = false; //becomes true when we find the item
											int ActualIndex = 0;
											foreach (oMod.ModItem mi in this.Mod.listItems)
											{
												//generate how it would have been inside the right click.
												str = "-" + mi.ItemName; //str is a recycled variable
												if (mi.ItemModName != "-") { str += "(" + mi.ItemModName + ")"; }

												//check if this item match what the user clicked
												if (str == rep)
												{
													//if the item is found, we set this variable to true and we tell the user where it is.
													found = true;

													//we tell the user where the item is.
													if (!this.IsItemManagerDefined)
													{
														MessageBox.Show("The item is located at the index : " + ActualIndex.ToString());
													}
													else
													{
														this.ItemManager.SelectIndex(ActualIndex);
														this.ItemManager.SetTopIndex(ActualIndex - 3);
													}

													break; //we quit
												}
												//next iteration
												ActualIndex++;
											}

											//if the item was not found, we tell the user that it's an external mod item
											if (!found)
											{
												MessageBox.Show("This item is not part of this mod.");
											}

										}
									}



								}

							}
						}
						else ////the user clicked on the "menu" background
						{

						}
					}

				}


				this.IsMouseLeftDownOnItemArea = false;
				this.RefreshImage();
			}
		}


		//a little frame skip so we don't refresh too much.
		private int zzzMouseMove_FrameSkip = 3;
		private int zzzMouseMove_FrameSkip_ActualFrame = 0;
		private void ImageBox_MouseMove(object sender, MouseEventArgs e)
		{
			//this.RefreshImage();
			if (this.zzzMouseMove_FrameSkip_ActualFrame <= 0)
			{
				Rectangle mrec = this.MouseRec;
				if (this.IsMouseLeftDown)
				{
					//if the user is trying to move the scroll bar, we move it
					if (this.recScrollZone.IntersectsWith(mrec))
					{
						//get the relative mouse position to the scroll zone and give it to the void that will move the scroll bar
						int clicky = mrec.Y - this.recScrollZone.Y;
						this.UserClickOnScrollZone(clicky);
					}


				}


				//refresh the image
				this.RefreshImage();

				this.zzzMouseMove_FrameSkip_ActualFrame = this.zzzMouseMove_FrameSkip;
			}
			this.zzzMouseMove_FrameSkip_ActualFrame--;
		}
		private void ImageBox_MouseLeave(object sender, EventArgs e)
		{
			this.RefreshImage();
		}
		private void ImageBox_MouseWheel(object sender, MouseEventArgs e)
		{

			if (e.Delta > 0)
			{
				this.ScrollUp(1);
			}
			if (e.Delta < 0)
			{
				this.ScrollDown(1);
			}


		}




		private List<oMod.ModCraft> listCraft
		{
			get
			{
				if (this.AnyModLoaded)
				{
					return this.Mod.listCrafts;
				}
				else
				{
					return null;
				}
			}
		}


		//inside the list of objects, this is the index of the item at the top. it's strongly related to the scrolling.
		private int IndexOfTopItem = 0;
		private int MinIndex
		{
			get
			{
				return 0;
			}
		}
		//this is max index, not object count
		private int MaxIndex
		{
			get
			{
				return this.ObjectCount - 1;
			}
		}
		//number of object in the list
		private int ObjectCount
		{
			get
			{
				if (this.AnyModLoaded)
				{
					return this.listCraft.Count;
				}
				else
				{
					return 0;
				}
			}
		}

		//to define the scrolling from outside. actually used by the find craft option
		public void SetTopIndex(int newtopindex, bool CallRefreshImage = true)
		{
			this.IndexOfTopItem = newtopindex;
			//check the bounds
			if (this.IndexOfTopItem < this.MinIndex) { this.IndexOfTopItem = this.MinIndex; }
			if (this.IndexOfTopItem > this.MaxIndex) { this.IndexOfTopItem = this.MaxIndex; }

			this.ReadjustScrollZone();

			if (CallRefreshImage) { this.RefreshImage(); }
		}


		//if mouse is not inside the item area or if the index doesn't exist, it will return -1.
		//return the index of the item under the mouse
		private int GetIndexOfMouse()
		{
			if (this.ObjectCount <= 0) { return -1; }
			Point mpos = this.MousePos;
			//multiple ifs to make sure the mouse is inside the item area
			if (mpos.X > 0 && mpos.X < this.Width - this.uiScrollZoneWidth)
			{
				if (mpos.Y > this.uiMenuHeight && mpos.Y < this.Height)
				{
					int ry = mpos.Y - this.uiMenuHeight; //relative position

					//ry is a mesure in pixel and now we add the total amount of vertical pixels upper than the area we see to get the relative mouse pos to the item of index 0.
					ry += this.uiItemHeight * this.IndexOfTopItem;

					//now a simple integer division. it's rounded down so we don't have anything else to do
					int mouseindex = ry / this.uiItemHeight;

					//some checks
					if (mouseindex < 0) { mouseindex = 0; }
					if (mouseindex > this.MaxIndex) { return -1; }

					return mouseindex;
				}
			}
			return -1;
		}


		//these voids executes a scroll of the specified length
		private void ScrollUp(int length = 1)
		{
			this.IndexOfTopItem -= length;
			if (this.IndexOfTopItem < this.MinIndex)
			{
				this.IndexOfTopItem = this.MinIndex;
			}
			this.ReadjustScrollZone();
			this.RefreshImage();
		}
		private void ScrollDown(int length = 1)
		{
			this.IndexOfTopItem += length;
			if (this.IndexOfTopItem > this.MaxIndex)
			{
				this.IndexOfTopItem = this.MaxIndex;
			}
			if (this.IndexOfTopItem < 0) { this.IndexOfTopItem = 0; }
			this.ReadjustScrollZone();
			this.RefreshImage();
		}


		#region Scroll Zone / Bar
		private uiButton btnScrollUp;
		private uiButton btnScrollDown;
		private Timer TimerAutoScroll;

		private Rectangle recScrollZone = new Rectangle(0, 0, 10, 100); //its size is adjusted by ReadjustScrollZone
		private Rectangle recScrollBar = new Rectangle(0, 0, 10, 10); //the bar inside the scroll zone. its size is adjusted by ReadjustScrollZone
		private int zzzLastReadjust_ObjectCount = -1; //save of the object count when last readjust of the scroll bar. used to automatically readjust the scroll bar
		private void IfNeededReadjustScrollZone()
		{
			if (this.zzzLastReadjust_ObjectCount != this.ObjectCount)
			{
				this.ReadjustScrollZone();
			}
		}
		private void ReadjustScrollZone()
		{
			this.zzzLastReadjust_ObjectCount = this.ObjectCount;

			//refresh the scroll zone rectangle
			this.recScrollZone.Y = this.btnScrollDown.Top + this.btnScrollDown.Height + 3;
			this.recScrollZone.X = this.btnScrollDown.Left;
			this.recScrollZone.Width = this.btnScrollDown.Width + 1;
			this.recScrollZone.Height = this.Height - 4 - this.recScrollZone.Top;

			//// the little bar rectangle
			this.recScrollBar.Width = this.recScrollZone.Width;
			this.recScrollBar.X = this.recScrollZone.X;

			//compute the height and the vertical position of the bar
			this.recScrollBar.Height = this.recScrollZone.Height;
			this.recScrollBar.Y = this.recScrollZone.Y;
			if (this.ObjectCount >= 2)
			{
				int AreaHeight = (this.Height - this.uiMenuHeight) + ((this.ObjectCount - 1) * this.uiItemHeight); //the total height of the area we scroll

				//a little check to be sure it has sense, and also to avoid division by 0
				if (AreaHeight < 10) { AreaHeight = 10; }

				int ScreenHeight = this.Height - this.uiMenuHeight; //the height of the area we see, of the total area we are scrolling
				this.recScrollBar.Height = this.recScrollZone.Height * ScreenHeight / AreaHeight;
				if (this.recScrollBar.Height < 20) { this.recScrollBar.Height = 20; } //prevent the scroll bar from being too small

				//compute its pos, according to the current scrolling
				int UiVariance = this.recScrollZone.Height - this.recScrollBar.Height; //this is the total amount of pixel we have to move the bar

				//the position of the scroll bar is all about %. % of the index at the top over the max index
				this.recScrollBar.Y = this.recScrollZone.Y + (int)(((float)(this.IndexOfTopItem) / (float)(this.MaxIndex)) * (float)UiVariance);
			}

		}


		//y=relative vertical pos to the scroll zone rectangle.
		//when the scrolling has to move according to where the user clicked on the scroll zone, call this void.
		private void UserClickOnScrollZone(int y)
		{
			if (this.ObjectCount >= 2)
			{
				try
				{

					//the scroll bar will be placed so its middle ypos will be where the mouse clicked.
					//to do that, we just have to substract half the height of the bar to the given y pos.

					int demiBarHeight = this.recScrollBar.Height / 2; //half the height of the scroll bar
					int ry = y - demiBarHeight; //new relative y. the one to use

					//some basic checks
					if (ry < 0) { this.IndexOfTopItem = 0; } //if the user clicked upper than half of the bar height at the most top position, we place the scroll to the beginning.
					else if (ry > this.recScrollZone.Height - this.recScrollBar.Height) { this.IndexOfTopItem = this.MaxIndex; } //if the user clicked lower than half of the bar height at the most bottom position, we place the scroll at the end. the full bar height is substracted because ry is already shifted of half the bar height.
					else
					{

						int UiVariance = this.recScrollZone.Height - this.recScrollBar.Height; //total moving space available

						//compute the index % of where the user clicked
						int newindex = this.MaxIndex * ry / UiVariance;

						//basic checks
						if (newindex < 0) { newindex = 0; }
						if (newindex > this.MaxIndex) { newindex = this.MaxIndex; }

						//apply the new index
						this.IndexOfTopItem = newindex;
					}
				}
				catch
				{
					this.ReadjustScrollZone();
				}
			}
			else
			{
				this.IndexOfTopItem = 0;
			}

			//readjust everything
			this.ReadjustScrollZone();
		}


		//create components related to the scroll bar
		private void CreateScrollBar()
		{
			this.btnScrollUp = new uiButton(this);
			this.btnScrollUp.Text = "/\\";
			this.btnScrollUp.SetSize(this.uiScrollZoneWidth - 2, 40);
			this.btnScrollUp.MouseLeftDown += new EventHandler(this.btnScrollUp_MouseLeftDown);
			this.btnScrollUp.MouseLeftUp += new EventHandler(this.btnScrollUp_MouseLeftUp);

			this.btnScrollDown = new uiButton(this);
			this.btnScrollDown.Text = "\\/";
			this.btnScrollDown.SetSize(this.uiScrollZoneWidth - 2, 40);
			this.btnScrollDown.MouseLeftDown += new EventHandler(this.btnScrollDown_MouseLeftDown);
			this.btnScrollDown.MouseLeftUp += new EventHandler(this.btnScrollDown_MouseLeftUp);

			this.TimerAutoScroll = new Timer();
			this.TimerAutoScroll.Interval = 100;
			this.TimerAutoScroll.Tick += new EventHandler(TimerAutoScroll_Tick);

		}
		private void ScrollBarRefreshSize()
		{
			//refresh button size
			this.btnScrollUp.Top = 2;
			this.btnScrollUp.Left = this.Width - 5 - this.btnScrollUp.Width;

			this.btnScrollDown.Top = this.btnScrollUp.Top + this.btnScrollUp.Height + 1;
			this.btnScrollDown.Left = this.btnScrollUp.Left;

			//refresh the scroll rectangles
			this.ReadjustScrollZone();


		}
		private void btnScrollUp_MouseLeftDown(object sender, EventArgs e)
		{
			this.ScrollUp(3);
			this.StartAutoScroll(AutoScrollDir.up);
		}
		private void btnScrollUp_MouseLeftUp(object sender, EventArgs e)
		{
			this.StopAutoScroll();
		}
		private void btnScrollDown_MouseLeftDown(object sender, EventArgs e)
		{
			this.ScrollDown(3);
			this.StartAutoScroll(AutoScrollDir.down);
		}
		private void btnScrollDown_MouseLeftUp(object sender, EventArgs e)
		{
			this.StopAutoScroll();
		}


		private int AutoScrollSpeed = 3; //shift to do each tick
		private enum AutoScrollDir
		{
			none,
			up,
			down
		}
		private AutoScrollDir ActualAutoScrollDir = AutoScrollDir.none;
		private void StartAutoScroll(AutoScrollDir sDir)
		{
			this.ActualAutoScrollDir = sDir;
			this.TimerAutoScroll.Interval = 500;
			this.TimerAutoScroll.Start();
		}
		private void StopAutoScroll()
		{
			this.TimerAutoScroll.Stop();
			this.ActualAutoScrollDir = AutoScrollDir.none;
		}
		private void TimerAutoScroll_Tick(object sender, EventArgs e)
		{
			this.TimerAutoScroll.Interval = 35;
			if (this.ActualAutoScrollDir == AutoScrollDir.up)
			{
				this.ScrollUp(this.AutoScrollSpeed);
			}
			if (this.ActualAutoScrollDir == AutoScrollDir.down)
			{
				this.ScrollDown(this.AutoScrollSpeed);
			}

		}


		#endregion

		#region Menu
		private uiButton btnNewCraft;


		private void CreateMenu()
		{
			this.btnNewCraft = new uiButton(this);
			this.btnNewCraft.Text = "New Craft";
			this.btnNewCraft.MouseLeftUp += new EventHandler(this.btnNewCraft_MouseLeftUp);



		}
		private void MenuRefreshSize()
		{
			this.btnNewCraft.SetPos(2, 2);
			this.btnNewCraft.SetSize(80, 30);

		}

		private void btnNewCraft_MouseLeftUp(object sender, EventArgs e)
		{

			DlgModCraftEditer dmce = new DlgModCraftEditer(this.Mod); // , newcraft
			dmce.Title = "New Mod Craft";
			dmce.ShowDialog();
			if (dmce.DialogResult == DialogResult.OK)
			{
				oMod.ModCraft mc = new oMod.ModCraft(dmce.GetRecipe(), dmce.GetInputs(), dmce.GetOutputs(), dmce.GetIsMadeInFurnace());
				this.listCraft.Add(mc);

				this.Raise_UserDidSomeChange();

			}

		}


		#endregion




		//a "selected index". this craft just has a blue back ground
		public int SelectedIndex = -1; //-1 if nothing is "selected"
		public void UnselectIndex() { this.SelectedIndex = -1; }
		public void SelectIndex(int index)
		{
			this.SelectedIndex = index;
		}



		private int uiMenuHeight = 50; //px height of the space reserved at the top for a "menu" and some controls inside
		private Pen uiSeparationPen = new Pen(Color.Silver, 3f);

		private int uiItemHeight = 120;
		private Font uiItemFont = new Font("lucida console", 10f); // consolas  10f

		private int uiScrollZoneWidth = 30; //width of the scroll area


		public void RefreshImage()
		{
			int imgWidth = this.Width;
			int imgHeight = this.Height;
			if (imgWidth < 100) { imgWidth = 100; }
			if (imgHeight < 100) { imgHeight = 100; }

			Bitmap img = new Bitmap(imgWidth, imgHeight);
			Graphics g = Graphics.FromImage(img);
			g.Clear(Color.FromArgb(16, 16, 16));

			Rectangle mrec = this.MouseRec;

			//it was often not readjusted when opening a mod file.
			this.IfNeededReadjustScrollZone();


			//////draw the separation between the "menu" and the item area
			g.DrawLine(this.uiSeparationPen, 5, this.uiMenuHeight, this.Width - this.uiScrollZoneWidth - 10, this.uiMenuHeight);



			//////draw the items

			if (this.ObjectCount > 0)
			{
				Rectangle recActualItem = new Rectangle(2, this.uiMenuHeight + 1, this.Width - this.uiScrollZoneWidth - 10, this.uiItemHeight - 2); //the rectangle that represents the area of the actual item



				int index = this.IndexOfTopItem;
				int mouseindex = this.GetIndexOfMouse();
				while (index <= this.MaxIndex && recActualItem.Y < this.Height)
				{
					oMod.ModCraft craft = this.listCraft[index];

					////draw the background
					Brush BackBrush = new SolidBrush(Color.FromArgb(32, 32, 32));
					if (index == this.SelectedIndex)
					{
						BackBrush = Brushes.DarkSlateGray; // DarkBlue DarkSlateGray
					}
					if (index == mouseindex && this.IsMouseLeftDownOnItemArea)
					{
						BackBrush = Brushes.Black;
					}
					g.FillRectangle(BackBrush, recActualItem);

					////draw the border rectangle
					Pen BorderPen = Pens.DimGray;
					if (index == mouseindex)
					{
						BorderPen = Pens.Silver;
					}
					g.DrawRectangle(BorderPen, recActualItem);

					////draw the item index in the bottom right corner
					//g.DrawString(index.ToString(), this.uiItemFont, Brushes.White, (float)(recActualItem.X), (float)(recActualItem.Y));
					SizeF IndexSizeF = g.MeasureString(index.ToString(), this.uiItemFont);
					g.DrawString(index.ToString(), this.uiItemFont, Brushes.White, (float)(recActualItem.X + recActualItem.Width) - IndexSizeF.Width, (float)(recActualItem.Y + recActualItem.Height) - IndexSizeF.Height);

					//////draw the actual image
					////all images are currently assumed to be drawed at real size which is 32x32
					//int MaxImageWidth = 40;
					//if (item.HasImage)
					//{
					//	//center
					//	//g.DrawImage(item.Img, recActualItem.X + 5, recActualItem.Y + (this.uiItemHeight / 2) - (item.Img.Height / 2));
					//	//bottom
					//	g.DrawImage(item.Img, recActualItem.X + 5, recActualItem.Y + this.uiItemHeight - item.Img.Height - 4);
					//}



					////draw the recipe
					g.DrawString("Recipe : " + craft.Recipe.ItemName + "   Recipe Mod : " + craft.Recipe.ModName, this.uiItemFont, Brushes.White, (float)(recActualItem.X), (float)(recActualItem.Y));

					////draw the inputs
					string InputLine1 = "Inputs  : ";
					bool Line1Full = false; //will become true if the line1 is full
					string InputLine2 = "          ";

					int InputIndex = 0;
					//we will add inputs one after the other until the line is full. then we will add them to the second line
					while (InputIndex < craft.Inputs.Length)
					{
						refItem ri = craft.Inputs[InputIndex];
						string strInputToAdd = ri.ItemName;
						if (ri.ModName != "-") { strInputToAdd += "(" + ri.ModName + ")"; } //if the mod is not -, we show what mod it is from

						if (!Line1Full)
						{
							int MaxWidth = recActualItem.Width; //maximum width allowed for the line
							string saveInputLine1 = InputLine1; //save the previous line1

							//add it to the line1
							if (InputIndex > 0) { InputLine1 += ", "; } // add the , to separate the inputs between them
							InputLine1 += strInputToAdd;
							//check if input line 1 is now too large or not
							int newwidth = (int)(g.MeasureString(InputLine1, this.uiItemFont).Width);

							//if line1 is now too large, we cancel to add and we change line
							if (newwidth > MaxWidth)
							{
								InputLine1 = saveInputLine1; //restore the previous line

								InputLine2 += strInputToAdd;
								//setup the variables for line 2
								Line1Full = true;
							}

						}
						else
						{
							//if line 1 is full
							
							//if (!IsFirstOfTheLine2) { InputLine2 += ", "; }
							InputLine2 += ", ";
							InputLine2 += strInputToAdd;
							
							//IsFirstOfTheLine2 = false;
						}

						//next iteration
						InputIndex++;
					}
					//draw the first line
					g.DrawString(InputLine1, this.uiItemFont, Brushes.SkyBlue, (float)(recActualItem.X), (float)(recActualItem.Y + 20));
					//draw the second line if there is one
					if (Line1Full)
					{
						g.DrawString(InputLine2, this.uiItemFont, Brushes.SkyBlue, (float)(recActualItem.X), (float)(recActualItem.Y + 40));
					}

					////draw the outputs

					string OutputLine1 = "Outputs : ";
					Line1Full = false;
					string OutputLine2 = "          ";

					int OutputIndex = 0;
					//we will add outputs one after the other until the line is full. then we will add them to the second line
					while (OutputIndex < craft.Outputs.Length)
					{
						refItem ri = craft.Outputs[OutputIndex];
						string strOutputToAdd = ri.ItemName;
						if (ri.ModName != "-") { strOutputToAdd += "(" + ri.ModName + ")"; } //if the mod is not -, we show what mod it is from

						if (!Line1Full)
						{
							int MaxWidth = recActualItem.Width; //maximum width allowed for the line
							string saveOutputLine1 = OutputLine1; //save the previous line1
							
							//add it to the line1
							if (OutputIndex > 0) { OutputLine1 += ", "; } // add the , to separate the inputs between them
							OutputLine1 += strOutputToAdd;
							//check if input line 1 is now too large or not
							int newwidth = (int)(g.MeasureString(OutputLine1, this.uiItemFont).Width);

							//if line1 is now too large, we cancel to add and we change line
							if (newwidth > MaxWidth)
							{
								OutputLine1 = saveOutputLine1; //restore the previous line

								OutputLine2 += strOutputToAdd;
								//setup the variables for line 2
								Line1Full = true;
							}

						}
						else
						{
							//if line 1 is full

							//if (!IsFirstOfTheLine2) { InputLine2 += ", "; }
							OutputLine2 += ", ";
							OutputLine2 += strOutputToAdd;

							//IsFirstOfTheLine2 = false;
						}

						//next iteration
						OutputIndex++;
					}
					//draw the first line
					g.DrawString(OutputLine1, this.uiItemFont, Brushes.Orchid, (float)(recActualItem.X), (float)(recActualItem.Y + 60));
					//draw the second line if there is one
					if (Line1Full)
					{
						g.DrawString(OutputLine2, this.uiItemFont, Brushes.Orchid, (float)(recActualItem.X), (float)(recActualItem.Y + 80));
					}


					////draw is made in furnace
					g.DrawString("Is made in furnace : " + craft.IsMadeInFurnace.ToString(), this.uiItemFont, Brushes.White, (float)(recActualItem.X), (float)(recActualItem.Y + 100));



					//next iteration
					index++;
					recActualItem.Y += this.uiItemHeight; //the rectangle itself doesn't have this height, but it's very important that the increment is precisely the said item height.

				}

			}
			else
			{
				g.DrawString("no craft", this.uiItemFont, Brushes.Silver, 5f, (float)(this.uiMenuHeight + 10));
			}


			//////draw the scroll bar

			g.FillRectangle(Brushes.Black, this.recScrollZone);
			g.FillRectangle(Brushes.Silver, this.recScrollBar);

			//////draw the controls
			Brush BrushButtonMouseUp = new SolidBrush(Color.FromArgb(64, 64, 64));
			Brush BrushButtonMouseDown = new SolidBrush(Color.FromArgb(16, 16, 16));
			foreach (uiButton b in this.listButton)
			{
				if (b.Visible)
				{
					bool IsMouseOnButton = b.rec.IntersectsWith(mrec);

					//draw the background
					Brush BackBrush = BrushButtonMouseUp;
					if (b.IsMouseLeftDownOnMe) { BackBrush = BrushButtonMouseDown; }
					g.FillRectangle(BackBrush, b.rec);

					//draw the border
					Pen BorderPen = Pens.DimGray;
					if (IsMouseOnButton || b.IsMouseLeftDownOnMe) { BorderPen = Pens.White; }
					g.DrawRectangle(BorderPen, b.rec);


					//draw the text
					SizeF TextSizeF = g.MeasureString(b.Text, b.TextFont);
					PointF TextPosF = new PointF((float)(b.Left) + ((float)(b.Width) / 2f) - (TextSizeF.Width / 2f), (float)(b.Top) + ((float)(b.Height) / 2f) - (TextSizeF.Height / 2f));
					g.DrawString(b.Text, b.TextFont, Brushes.White, TextPosF);

				}
			}


			g.Dispose();
			if (this.ImageBox.Image != null) { this.ImageBox.Image.Dispose(); }
			this.ImageBox.Image = img;
			GC.Collect();
		}
		public void RefreshSize()
		{
			this.ScrollBarRefreshSize();
			this.MenuRefreshSize();

		}




		// ///////////////////////////////////////////////////////////////////////////////////////////////////////
		// ///////////////////////////////////////////// UI CONTROLS /////////////////////////////////////////////
		// ///////////////////////////////////////////////////////////////////////////////////////////////////////

		private List<uiButton> listButton = new List<uiButton>();



		//return if the mouse is currently on any control
		private bool IsMouseOnAnyControl()
		{
			Rectangle mrec = this.MouseRec;
			//check for buttons
			foreach (uiButton b in this.listButton)
			{
				if (b.rec.IntersectsWith(mrec))
				{
					return true;
				}
			}
			return false;
		}

		// //// these ECHECK voids will call the echeck of every controls
		private void ECHECK_MouseLeftDown()
		{
			Rectangle mrec = this.MouseRec;
			//buttons
			foreach (uiButton b in this.listButton)
			{
				b.ECHECK_MouseLeftDown(mrec);
			}
		}
		private void ECHECK_MouseLeftUp()
		{
			Rectangle mrec = this.MouseRec;
			//buttons
			foreach (uiButton b in this.listButton)
			{
				b.ECHECK_MouseLeftUp(mrec);
			}
		}




		private class uiButton
		{
			public uiModCraftManager Parent;

			public Rectangle rec = new Rectangle(0, 0, 100, 30);
			public int Top
			{
				get { return this.rec.Y; }
				set { this.rec.Y = value; }
			}
			public int Left
			{
				get { return this.rec.X; }
				set { this.rec.X = value; }
			}
			public int Width
			{
				get { return this.rec.Width; }
				set { this.rec.Width = value; }
			}
			public int Height
			{
				get { return this.rec.Height; }
				set { this.rec.Height = value; }
			}
			public void SetPos(int newLeft, int newTop)
			{
				this.Top = newTop;
				this.Left = newLeft;
			}
			public void SetSize(int newWidth, int newHeight)
			{
				this.Width = newWidth;
				this.Height = newHeight;
			}

			public string Text = "notext";
			public Font TextFont = new Font("fonsolas", 10f);

			public bool Visible = true;


			public uiButton(uiModCraftManager StartParent)
			{
				this.Parent = StartParent;
				this.Parent.listButton.Add(this);

			}


			public bool IsMouseLeftDownOnMe = false;

			// //////// Events:
			public event EventHandler MouseLeftDown;
			public event EventHandler MouseLeftUp;
			private void Raise_MouseLeftDown()
			{
				if (this.MouseLeftDown != null)
				{
					this.MouseLeftDown(this, new EventArgs());
				}
			}
			private void Raise_MouseLeftUp()
			{
				if (this.MouseLeftUp != null)
				{
					this.MouseLeftUp(this, new EventArgs());
				}
			}


			// //// these ECHECK voids are called by the parent, for all controls. inside these voids, each controls checks if they have some events to raise.

			public void ECHECK_MouseLeftDown(Rectangle mrec)
			{
				if (this.Visible)
				{
					//check if the mouse is on this
					if (this.rec.IntersectsWith(mrec))
					{
						this.IsMouseLeftDownOnMe = true;

						//raise the events
						this.Raise_MouseLeftDown();

					}
				}
			}
			public void ECHECK_MouseLeftUp(Rectangle mrec)
			{
				if (this.Visible)
				{
					//check if the mouse left button was down
					if (this.IsMouseLeftDownOnMe)
					{
						//raise mouse left up
						this.Raise_MouseLeftUp();

					}
				}
				//in every case, mouse left is not down on this anymore
				this.IsMouseLeftDownOnMe = false;
			}


		}





	}
}
