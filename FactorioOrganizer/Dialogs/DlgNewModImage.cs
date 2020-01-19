using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace FactorioOrganizer.Dialogs
{
	public class DlgNewModImage
	{
		private Form forme;

		private PictureBox ImageBox;
		private Label lblFileName;
		private TextBox tbFileName;

		private Button btnBrowseFile;
		private Button btnCreateBlankImage;
		private Button btnEdit;

		private Button btnOk;
		private Button btnCancel;



		public bool DiagAccepted = false; //true only if the user clicked the ok button.



		public string Title
		{
			get { return this.forme.Text; }
			set { this.forme.Text = value; }
		}

		//this is the list of all the file name that cannot be accepted because they are already used.
		//must be filled or substitued ouside of this.
		public List<string> listAlreadyExistingName = new List<string>();
		//return if a file name already exist
		private bool IsFileNameAlreadyUsed(string name)
		{
			return this.listAlreadyExistingName.Exists(x => x == name);
		}



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

		public string FileName
		{
			get { return this.tbFileName.Text; }
		}




		public void ShowDialog()
		{

			this.RefreshSize();

			//gets the mouse pos and position the window in the middle of it
			Point mpos = Cursor.Position;
			this.forme.Top = mpos.Y - (this.forme.Height / 2);
			this.forme.Left = mpos.X - (this.forme.Width / 2);

			//make sure form is inbound
			if (this.forme.Left < 0) { this.forme.Left = 0; }
			if (this.forme.Top < 0) { this.forme.Top = 0; }
			if (this.forme.Left + this.forme.Width > Screen.PrimaryScreen.Bounds.Width) { this.forme.Left = Screen.PrimaryScreen.Bounds.Width - this.forme.Width; }


			//show the form
			this.RefreshEnabled();
			this.forme.ShowDialog();
		}

		


		public DlgNewModImage()
		{
			this.forme = new Form();
			this.forme.ShowInTaskbar = false;
			this.forme.MinimizeBox = false;
			this.forme.MaximizeBox = false;
			this.forme.Text = "New Mod Image";
			this.forme.FormBorderStyle = FormBorderStyle.FixedSingle;
			this.forme.ShowIcon = false;
			this.forme.StartPosition = FormStartPosition.Manual;


			this.ImageBox = new PictureBox();
			this.ImageBox.Parent = this.forme;
			this.ImageBox.BackColor = Color.Black;
			this.ImageBox.BorderStyle = BorderStyle.FixedSingle;
			this.ImageBox.BackgroundImageLayout = ImageLayout.Zoom;

			this.lblFileName = new Label();
			this.lblFileName.Parent = this.forme;
			this.lblFileName.Text = "File Name :";
			this.lblFileName.AutoSize = true;

			this.tbFileName = new TextBox();
			this.tbFileName.Parent = this.forme;
			this.tbFileName.Text = "";
			this.tbFileName.TextChanged += new EventHandler(this.tbFileName_TextChanged);

			
			this.btnBrowseFile = new Button();
			this.btnBrowseFile.Parent = this.forme;
			this.btnBrowseFile.Text = "Browse image ...";
			this.btnBrowseFile.Click += new EventHandler(this.btnBrowseFile_Click);

			this.btnCreateBlankImage = new Button();
			this.btnCreateBlankImage.Parent = this.forme;
			this.btnCreateBlankImage.Text = "Create blank image 32x32";
			this.btnCreateBlankImage.Click += new EventHandler(this.btnCreateBlankImage_Click);

			this.btnEdit = new Button();
			this.btnEdit.Parent = this.forme;
			this.btnEdit.Text = "Edit image";
			this.btnEdit.Click += new EventHandler(this.btnEdit_Click);
			

			this.btnOk = new Button();
			this.btnOk.Parent = this.forme;
			this.btnOk.Text = "Ok";
			this.btnOk.Click += new EventHandler(this.btnOk_Click);

			this.btnCancel = new Button();
			this.btnCancel.Parent = this.forme;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);



		}
		public void RefreshSize()
		{
			this.forme.Size = new Size(500, 175);

			this.ImageBox.Location = new Point(5, 5);
			this.ImageBox.Size = new Size(128, 128);
			
			this.lblFileName.Left = this.ImageBox.Left + this.ImageBox.Width + 5;
			this.lblFileName.Top = this.ImageBox.Top;

			this.tbFileName.Left = this.lblFileName.Left;
			this.tbFileName.Top = this.lblFileName.Top + this.lblFileName.Height + 1;
			this.tbFileName.Width = this.forme.Width - 18 - this.tbFileName.Left - 3;


			this.btnBrowseFile.Left = this.tbFileName.Left;
			this.btnBrowseFile.Top = this.tbFileName.Top + this.tbFileName.Height + 15;
			this.btnBrowseFile.Size = new Size(100, 30);

			this.btnCreateBlankImage.Left = this.btnBrowseFile.Left;
			this.btnCreateBlankImage.Top = this.btnBrowseFile.Top + this.btnBrowseFile.Height + 2;
			this.btnCreateBlankImage.Size = new Size(150, 30);

			this.btnEdit.Left = this.btnBrowseFile.Left + this.btnBrowseFile.Width + 2;
			this.btnEdit.Top = this.btnBrowseFile.Top;
			this.btnEdit.Size = new Size(70, 30);

			this.btnOk.Size = new Size(40, 30);
			this.btnOk.Top = this.forme.Height - 41 - this.btnOk.Height;
			this.btnOk.Left = this.forme.Width - 18 - this.btnOk.Width;

			this.btnCancel.Size = new Size(70, 30);
			this.btnCancel.Top = this.btnOk.Top;
			this.btnCancel.Left = this.btnOk.Left - 2 - this.btnCancel.Width;



		}
		public void RefreshEnabled()
		{
			this.btnEdit.Enabled = this.AnyImageLoaded; //we cannot edit an image that doesn't exist


			bool IsValidFileName = this.tbFileName.Text.Length > 0; //check if it's a valid filename
			bool IsAlreadyUsedName = this.IsFileNameAlreadyUsed(this.tbFileName.Text);
			if (!IsAlreadyUsedName)
			{
				this.lblFileName.Text = "File Name :";
				this.lblFileName.ForeColor = Color.Black;
			}
			else
			{
				this.lblFileName.Text = "File name already used";
				this.lblFileName.ForeColor = Color.DarkRed;
			}


			this.btnOk.Enabled = this.AnyImageLoaded && IsValidFileName && !IsAlreadyUsedName;


		}
		



		private void tbFileName_TextChanged(object sender, EventArgs e)
		{
			this.RefreshEnabled(); //if the new text inputed by the user is not a valid file name, we must disable the ok button. it's also this void that will change the backcolor to red if the file name is already used.
		}
		private void btnBrowseFile_Click(object sender, EventArgs e)
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
					string filename = System.IO.Path.GetFileName(filepath);
					string validfilename = filename; //a file name cleaned up from every caracters that we don't want in the final filename
					Bitmap img = new Bitmap(filepath); //if filepath is not an image, this will generate an error
					
					if (img.Width == 32 && img.Height == 32)
					{
						//everything is fine, we can take this image
						this.DefineImage(img);
						this.tbFileName.Text = validfilename;
					}
					else //if the image is not 32x32, we must rescale it, or cancel
					{
						//MessageBox.Show("Images must be the size of 32x32.\nYou must rescale it.");
						MsgOnce.Show("image must be 32 SHRBTIHSTNG", "Images must be the size of 32x32.\nYou must rescale it.");

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
							this.tbFileName.Text = validfilename;
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
		private void btnEdit_Click(object sender, EventArgs e)
		{
			RandomImports.oFormImageEditer fie = new RandomImports.oFormImageEditer(this.Img);
			fie.ShowDialog();
			if (fie.DiagAccepted)
			{
				this.DefineImage(fie.ImgEdited);
			}
		}





		private void btnOk_Click(object sender, EventArgs e)
		{
			this.DiagAccepted = true;
			this.forme.Close();

		}
		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DiagAccepted = false;
			this.forme.Close();

		}







	}
}
