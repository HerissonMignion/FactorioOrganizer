using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactorioOrganizer.Dialogs
{
	//this dialog allow the user to rescale an image and confirm or cancel. it will automatically generate the result image of the desired size.
	public partial class DlgRescaleImage : Form
	{


		private Bitmap zzzOriginalImage;

		private Bitmap zzzResultImage = null;
		public Bitmap ResultImage
		{
			get { return this.zzzResultImage; }
		}


		public string Title
		{
			get { return this.Text; }
			set { this.Text = value; }
		}

		public bool DiagAccepted = false; //will be true if and only if the user closed the dialog with the ok button


		public int NewWidth
		{
			get { return (int)(this.nudWidth.Value); }
			set { this.nudWidth.Value = (decimal)value; }
		}
		public int NewHeight
		{
			get { return (int)(this.nudHeight.Value); }
			set { this.nudHeight.Value = (decimal)value; }
		}

		//disable the controls so the user cannot chose any size
		public void SetAsFixedSize()
		{
			this.nudWidth.Enabled = false;
			this.nudHeight.Enabled = false;
		}
		//enable the controls so the user can chose any size
		public void SetAsVariableSize()
		{
			this.nudWidth.Enabled = true;
			this.nudHeight.Enabled = true;
		}



		public DlgRescaleImage(Bitmap TheImage)
		{
			InitializeComponent();

			this.zzzOriginalImage = TheImage; //save the image

			try
			{
				//auto define the actual image size
				this.nudWidth.Value = (decimal)(this.zzzOriginalImage.Width);
				this.nudHeight.Value = (decimal)(this.zzzOriginalImage.Height);
			}
			catch //an error can occure if define a size outside of the min and max values
			{
				this.nudWidth.Value = 100m;
				this.nudHeight.Value = 100m;
			}


		}
		private void DlgRescaleImage_Load(object sender, EventArgs e)
		{


			this.MakeResultImage();

		}

		//remake the image
		private void MakeResultImage()
		{
			int imgwidth = (int)(this.nudWidth.Value);
			int imgheight = (int)(this.nudHeight.Value);

			Bitmap rimg = new Bitmap(imgwidth, imgheight);
			Graphics g = Graphics.FromImage(rimg);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
			g.DrawImage(this.zzzOriginalImage, 0 - 1, 0 - 1, imgwidth + 1, imgheight + 1);
			g.Dispose();

			if (this.zzzResultImage != null) { this.zzzResultImage.Dispose(); }
			this.ImageBox.Image = rimg;
			this.zzzResultImage = rimg;
		}


		private void nudWidth_ValueChanged(object sender, EventArgs e)
		{
			this.MakeResultImage();
		}
		private void nudHeight_ValueChanged(object sender, EventArgs e)
		{
			this.MakeResultImage();
		}




		private void ButtonOk_Click(object sender, EventArgs e)
		{
			this.DiagAccepted = true;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			this.DiagAccepted = false;
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}





	}
}
