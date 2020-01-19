using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactorioOrganizer.RandomImports;

namespace FactorioOrganizer.Dialogs
{
	public partial class DlgNewModItem : Form
	{


		private bool zzzAnyImageLoaded = false;
		private Bitmap zzzImg = null;
		public bool AnyImageLoaded
		{
			get { return this.zzzAnyImageLoaded; }
		}
		public Bitmap Img
		{
			get { return this.zzzImg; }
		}
		public void DefineImage(Bitmap img)
		{
			this.zzzAnyImageLoaded = img != null;
			this.zzzImg = img;
			this.ImageBox.BackgroundImage = img;
			this.RefreshEnabled();
		}


		public string ItemName
		{
			get { return this.tbItemName.Text; }
			set { this.tbItemName.Text = value; }
		}
		public string ModName
		{
			get
			{
				if (!this.cbExternItem.Checked) { return "-"; }
				return this.tbModName.Text;
			}
			set
			{
				this.tbModName.Text = value;
				this.RefreshEnabled();
			}
		}

		public bool CanBeABelt
		{
			get { return this.cbIsBelt.Checked; }
			set
			{
				this.cbIsBelt.Checked = value;
			}
		}




		//from outside, you can give the mod so this form can de a little bit more for the user. currently, it's used to give the user the image of all the items that currently exist as template.
		public oMod TheMod = null;





		public DlgNewModItem()
		{
			InitializeComponent();
		}
		private void DlgNewModItem_Load(object sender, EventArgs e)
		{
			//center the form to the mouse
			this.Left = Cursor.Position.X - (this.Width / 2);
			this.Top = Cursor.Position.Y - (this.Height / 2);
			if (this.Left < 0) { this.Left = 0; }
			if (this.Top < 0) { this.Top = 0; }


			//get an unused default item name
			for (int i = 1; i < 300; i++)
			{
				//generate the name
				string defaultname = "NewItem";
				if (i > 1) { defaultname += i.ToString(); }

				//test if the name is ok. ex: it could be already used
				if (this.IsItemNameValid(defaultname))
				{
					this.tbItemName.Text = defaultname;
					break; //we can stop the search
				}
			}



			this.LoadTemplates();
			this.RefreshEnabled();
		}





		public void RefreshEnabled()
		{
			//the mod name text box
			this.tbModName.Enabled = this.cbExternItem.Checked;

			//the edit image button
			this.btnEditImage.Enabled = this.AnyImageLoaded;

			//button ok
			this.btnOk.Enabled = this.tbItemName.Text.Length > 0 && this.AnyImageLoaded; //this.IsItemNameValid();

		}



		private void tbItemName_TextChanged(object sender, EventArgs e)
		{
			this.RefreshEnabled();
			if (!this.IsItemNameValid() && this.tbItemName.Text.Length > 0)
			{
				this.tbItemName.BackColor = Color.Crimson;
				this.tbItemName.ForeColor = Color.White;
			}
			else
			{
				this.tbItemName.BackColor = Color.White;
				this.tbItemName.ForeColor = Color.Black;
			}
		}
		private void tbModName_TextChanged(object sender, EventArgs e)
		{
			this.RefreshEnabled();
		}

		private void cbExternItem_CheckedChanged(object sender, EventArgs e)
		{
			this.RefreshEnabled();
		}




		private void btnBrowseImage_Click(object sender, EventArgs e)
		{
			//create an open dialog and show it to the user
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Multiselect = false;
			DialogResult rep = ofd.ShowDialog();
			if (rep == DialogResult.OK)
			{
				try
				{
					//get the selected file
					string filepath = ofd.FileName;
					Bitmap img = new Bitmap(filepath); //if filepath is not an image, this will generate an error

					if (img.Width == 32 && img.Height == 32)
					{
						//everything is fine, we can take this image
						this.DefineImage(img);
					}
					else //if the image is not 32x32, we must rescale it, or cancel
					{
						//MessageBox.Show("Images must be the size of 32x32.\nYou must rescale it.");
						MsgOnce.Show("image must be 32 GTNYSRTEAWGT", "Images must be the size of 32x32.\nYou must rescale it.");

						//create and setup a dialog that will allow the user to rescale its image to 32x32
						DlgRescaleImage dri = new DlgRescaleImage(img);
						dri.NewWidth = 32;
						dri.NewHeight = 32;
						dri.SetAsFixedSize();
						dri.Title = "New Mod Image - Rescale Image To 32x32";
						dri.ShowDialog();

						if (dri.DiagAccepted)
						{
							//if the dialog was accepted, we have nothing else to do than accepting the new image
							this.DefineImage(dri.ResultImage);
						}

					}
				}
				catch
				{
					MessageBox.Show("An error occurred");
				}
			}
		}
		private void btnCreateBlankImage_Click(object sender, EventArgs e)
		{
			//create a totaly white image and defines it.
			Bitmap newimg = new Bitmap(32, 32);
			Graphics g = Graphics.FromImage(newimg);
			g.Clear(Color.White);
			g.Dispose();
			this.DefineImage(newimg);

		}
		private void btnEditImage_Click(object sender, EventArgs e)
		{
			if (this.AnyImageLoaded)
			{
				oFormImageEditer fie = new oFormImageEditer(this.Img);
				fie.ShowDialog();
				if (fie.DiagAccepted)
				{
					this.DefineImage(fie.ImgEdited);
				}
			}
		}
		private void btnSelectAreaOfTheScreen_Click(object sender, EventArgs e)
		{
			//this dialog is like snipping tool, the user select an area of the screen
			SelectScreenForm ssf = new SelectScreenForm();
			ssf.ShowToUser();

			//get the rectangle that the user selected
			Rectangle rec = ssf.GetRectangle();
			//make sure the rectangle that the user selected is of a reasonnable size. if the user want to cancel, he can just release the mouse left button where he pressed the left button, thus making a very small rectangle. then there will be the message that the rectangle is too small.
			if (rec.Height > 10)
			{
				if (rec.Width > 10)
				{
					//we tell the user that the "screen shots" are automatically resized to 32x32.
					//this message is anoying and useless.
					//MsgOnce.Show("screen shot auto resized to 32", "\"Screen shots\" are automatically resized to 32x32");

					try
					{
						
						Bitmap newimg = new Bitmap(32, 32);
						Graphics g = Graphics.FromImage(newimg);
						g.Clear(Color.White);

						////we check the size of the rectangle. maybe the user was lucky and he selected an exact 32x32 rectangle.
						//if (rec.Height == 32 && rec.Width == 32)
						//{
						//	//g.CopyFromScreen(rec.Location, new Point(0, 0), new Size(32, 32));
						//	g.DrawImage(ssf.imgScreen, new Rectangle(0, 0, 32, 32), new Rectangle(rec.X, rec.Y, 32, 32), GraphicsUnit.Pixel);
						//}
						//else
						//{
						//	////create a new bitmap in which we will "import" the area of the screen that the user selected
						//	//Bitmap imgscreen = new Bitmap(rec.Width, rec.Height);
						//	//Graphics sg = Graphics.FromImage(imgscreen);
						//	//sg.CopyFromScreen(rec.Location, new Point(0, 0), rec.Size);
						//	//sg.Dispose();

						//	////now it copy imgscreen to newimg and at the size 32,32
						//	//g.DrawImage(imgscreen, 0, 0, 32, 32);

						//	g.DrawImage(ssf.imgScreen, new Rectangle(0, 0, 32, 32), rec, GraphicsUnit.Pixel);

						//}

						g.DrawImage(ssf.imgScreen, new Rectangle(0, 0, 32, 32), rec, GraphicsUnit.Pixel);

						g.Dispose();
						this.DefineImage(newimg);
					}
					catch
					{
						MessageBox.Show("An error occurred.");
					}
				}
				else
				{
					MessageBox.Show("The rectangle is too small horizontally.");
				}
			}
			else
			{
				MessageBox.Show("The rectangle is too small vertically.");
			}



		}




		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();

		}
		private void btnOk_Click(object sender, EventArgs e)
		{
			if (this.IsItemNameValid())
			{
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			else
			{
				MessageBox.Show("Item name already used or invalid.");
			}
		}




		//from the outside, we can put in this list every item name already used in the mod. this will be ignored if the user specified that it's an external mod item.
		public List<string> listIllegalItemName = new List<string>();

		private bool IsItemNameValid()
		{
			return this.IsItemNameValid(this.tbItemName.Text);
		}
		private bool IsItemNameValid(string name)
		{
			if (name.Length <= 0) { return false; }

			//check if the name is already used, if we, this, is not an external mod item
			if (!this.cbExternItem.Checked)
			{
				foreach (string subname in this.listIllegalItemName)
				{
					if (subname == name)
					{
						return false;
					}
				}
			}

			return true;
		}







		private void LoadTemplates()
		{
			Color TemplateBackColor = Color.Gainsboro;
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.imgTemplateBrick, TemplateBackColor);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.imgTemplateCircuit, TemplateBackColor);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.imgTemplateGearWheel, TemplateBackColor);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.imgTemplateLiquid, TemplateBackColor);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.imgTemplateOre, TemplateBackColor);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.imgTemplatePlate, TemplateBackColor);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.imgTemplateScience, TemplateBackColor);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.imgTemplateSolidFuel, TemplateBackColor);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.imgTemplateSteelPlate, TemplateBackColor);
			
			//if the mod was defined, it give the user, as templates, every existing image of items.
			if (this.TheMod != null)
			{
				foreach (oMod.ModItem mi in this.TheMod.listItems)
				{
					this.MakeNewButtonTemplate(mi.Img, Color.White);
				}
			}

			Color imgBackColor = Color.FromArgb(150, 150, 150);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.transport_belt, imgBackColor);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.underground_belt, imgBackColor);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.splitter, imgBackColor);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.inserter, imgBackColor);

			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.assembling_machine_1, imgBackColor);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.artillery_shell, imgBackColor);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.grenade, imgBackColor);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.pipe, imgBackColor);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.heat_pipe, imgBackColor);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.pipe_to_ground, imgBackColor);

			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.speed_module, imgBackColor);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.speed_module_2, imgBackColor);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.speed_module_3, imgBackColor);
			
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.explosive_rocket, imgBackColor);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.explosive_cannon_shell, imgBackColor);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.flamethrower_ammo, imgBackColor);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.gun_turret, imgBackColor);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.laser_turret, imgBackColor);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.flamethrower_turret, imgBackColor);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.artillery_turret, imgBackColor);

			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.flying_robot_frame, imgBackColor);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.construction_robot, imgBackColor);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.logistic_robot, imgBackColor);
			this.MakeNewButtonTemplate(FactorioOrganizer.Properties.Resources.logistic_chest_storage, imgBackColor);



		}
		private void MakeNewButtonTemplate(Bitmap img, Color BackC)
		{
			Button btn = new Button();
			btn.Parent = this.flpTemplateButtonPanel;
			btn.Size = new Size(40, 40);

			btn.Text = "";
			btn.BackgroundImageLayout = ImageLayout.Center;
			btn.BackgroundImage = img;
			btn.BackColor = BackC;
			btn.Tag = (object)img;

			btn.Click += new EventHandler(this.AnyButtonTemplate_Click);

		}
		private void AnyButtonTemplate_Click(object sender, EventArgs e)
		{
			Button btn = (Button)sender;
			Bitmap img = new Bitmap(btn.BackgroundImage);
			this.DefineImage(img);

		}

	}
}
