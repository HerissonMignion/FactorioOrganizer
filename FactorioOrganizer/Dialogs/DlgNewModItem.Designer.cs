namespace FactorioOrganizer.Dialogs
{
	partial class DlgNewModItem
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.tbItemName = new System.Windows.Forms.TextBox();
			this.cbExternItem = new System.Windows.Forms.CheckBox();
			this.tbModName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnBrowseImage = new System.Windows.Forms.Button();
			this.btnCreateBlankImage = new System.Windows.Forms.Button();
			this.btnEditImage = new System.Windows.Forms.Button();
			this.flpTemplateButtonPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.label3 = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.ImageBox = new System.Windows.Forms.PictureBox();
			this.cbIsBelt = new System.Windows.Forms.CheckBox();
			this.btnSelectAreaOfTheScreen = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.ImageBox)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Item name :";
			// 
			// tbItemName
			// 
			this.tbItemName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbItemName.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbItemName.Location = new System.Drawing.Point(80, 6);
			this.tbItemName.Name = "tbItemName";
			this.tbItemName.Size = new System.Drawing.Size(439, 20);
			this.tbItemName.TabIndex = 1;
			this.tbItemName.Text = "NewItem";
			this.tbItemName.TextChanged += new System.EventHandler(this.tbItemName_TextChanged);
			// 
			// cbExternItem
			// 
			this.cbExternItem.AutoSize = true;
			this.cbExternItem.ForeColor = System.Drawing.Color.White;
			this.cbExternItem.Location = new System.Drawing.Point(12, 41);
			this.cbExternItem.Name = "cbExternItem";
			this.cbExternItem.Size = new System.Drawing.Size(96, 17);
			this.cbExternItem.TabIndex = 2;
			this.cbExternItem.Text = "Is external item";
			this.cbExternItem.UseVisualStyleBackColor = true;
			this.cbExternItem.CheckedChanged += new System.EventHandler(this.cbExternItem_CheckedChanged);
			// 
			// tbModName
			// 
			this.tbModName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbModName.Location = new System.Drawing.Point(12, 78);
			this.tbModName.Name = "tbModName";
			this.tbModName.Size = new System.Drawing.Size(507, 20);
			this.tbModName.TabIndex = 3;
			this.tbModName.TextChanged += new System.EventHandler(this.tbModName_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(12, 62);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(291, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Mod name : (\"-\" for current mod, \"vanilla\" for vanilla factorio)";
			// 
			// btnBrowseImage
			// 
			this.btnBrowseImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowseImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.btnBrowseImage.ForeColor = System.Drawing.Color.White;
			this.btnBrowseImage.Location = new System.Drawing.Point(333, 133);
			this.btnBrowseImage.Name = "btnBrowseImage";
			this.btnBrowseImage.Size = new System.Drawing.Size(102, 30);
			this.btnBrowseImage.TabIndex = 6;
			this.btnBrowseImage.Text = "Browse image ...";
			this.btnBrowseImage.UseVisualStyleBackColor = false;
			this.btnBrowseImage.Click += new System.EventHandler(this.btnBrowseImage_Click);
			// 
			// btnCreateBlankImage
			// 
			this.btnCreateBlankImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCreateBlankImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.btnCreateBlankImage.ForeColor = System.Drawing.Color.White;
			this.btnCreateBlankImage.Location = new System.Drawing.Point(333, 164);
			this.btnCreateBlankImage.Name = "btnCreateBlankImage";
			this.btnCreateBlankImage.Size = new System.Drawing.Size(149, 30);
			this.btnCreateBlankImage.TabIndex = 7;
			this.btnCreateBlankImage.Text = "Create blank image 32x32";
			this.btnCreateBlankImage.UseVisualStyleBackColor = false;
			this.btnCreateBlankImage.Click += new System.EventHandler(this.btnCreateBlankImage_Click);
			// 
			// btnEditImage
			// 
			this.btnEditImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnEditImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.btnEditImage.ForeColor = System.Drawing.Color.White;
			this.btnEditImage.Location = new System.Drawing.Point(436, 133);
			this.btnEditImage.Name = "btnEditImage";
			this.btnEditImage.Size = new System.Drawing.Size(100, 30);
			this.btnEditImage.TabIndex = 8;
			this.btnEditImage.Text = "Edit image";
			this.btnEditImage.UseVisualStyleBackColor = false;
			this.btnEditImage.Click += new System.EventHandler(this.btnEditImage_Click);
			// 
			// flpTemplateButtonPanel
			// 
			this.flpTemplateButtonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.flpTemplateButtonPanel.AutoScroll = true;
			this.flpTemplateButtonPanel.BackColor = System.Drawing.Color.Black;
			this.flpTemplateButtonPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.flpTemplateButtonPanel.Location = new System.Drawing.Point(121, 133);
			this.flpTemplateButtonPanel.Name = "flpTemplateButtonPanel";
			this.flpTemplateButtonPanel.Size = new System.Drawing.Size(210, 119);
			this.flpTemplateButtonPanel.TabIndex = 9;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.Color.White;
			this.label3.Location = new System.Drawing.Point(118, 117);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(85, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "Image template :";
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.btnOk.ForeColor = System.Drawing.Color.White;
			this.btnOk.Location = new System.Drawing.Point(465, 226);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(74, 35);
			this.btnOk.TabIndex = 11;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = false;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.btnCancel.ForeColor = System.Drawing.Color.White;
			this.btnCancel.Location = new System.Drawing.Point(389, 226);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(74, 35);
			this.btnCancel.TabIndex = 12;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// ImageBox
			// 
			this.ImageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ImageBox.BackColor = System.Drawing.Color.Black;
			this.ImageBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.ImageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ImageBox.Location = new System.Drawing.Point(15, 152);
			this.ImageBox.Name = "ImageBox";
			this.ImageBox.Size = new System.Drawing.Size(100, 100);
			this.ImageBox.TabIndex = 5;
			this.ImageBox.TabStop = false;
			// 
			// cbIsBelt
			// 
			this.cbIsBelt.AutoSize = true;
			this.cbIsBelt.Checked = true;
			this.cbIsBelt.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbIsBelt.ForeColor = System.Drawing.Color.White;
			this.cbIsBelt.Location = new System.Drawing.Point(12, 110);
			this.cbIsBelt.Name = "cbIsBelt";
			this.cbIsBelt.Size = new System.Drawing.Size(89, 17);
			this.cbIsBelt.TabIndex = 13;
			this.cbIsBelt.Text = "Can be a belt";
			this.cbIsBelt.UseVisualStyleBackColor = true;
			// 
			// btnSelectAreaOfTheScreen
			// 
			this.btnSelectAreaOfTheScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSelectAreaOfTheScreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.btnSelectAreaOfTheScreen.ForeColor = System.Drawing.Color.White;
			this.btnSelectAreaOfTheScreen.Location = new System.Drawing.Point(333, 195);
			this.btnSelectAreaOfTheScreen.Name = "btnSelectAreaOfTheScreen";
			this.btnSelectAreaOfTheScreen.Size = new System.Drawing.Size(149, 30);
			this.btnSelectAreaOfTheScreen.TabIndex = 14;
			this.btnSelectAreaOfTheScreen.Text = "Select an area of the screen";
			this.btnSelectAreaOfTheScreen.UseVisualStyleBackColor = false;
			this.btnSelectAreaOfTheScreen.Click += new System.EventHandler(this.btnSelectAreaOfTheScreen_Click);
			// 
			// DlgNewModItem
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.DimGray;
			this.ClientSize = new System.Drawing.Size(543, 265);
			this.Controls.Add(this.btnSelectAreaOfTheScreen);
			this.Controls.Add(this.cbIsBelt);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.flpTemplateButtonPanel);
			this.Controls.Add(this.btnEditImage);
			this.Controls.Add(this.btnCreateBlankImage);
			this.Controls.Add(this.btnBrowseImage);
			this.Controls.Add(this.ImageBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbModName);
			this.Controls.Add(this.cbExternItem);
			this.Controls.Add(this.tbItemName);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DlgNewModItem";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "New Mod Item";
			this.Load += new System.EventHandler(this.DlgNewModItem_Load);
			((System.ComponentModel.ISupportInitialize)(this.ImageBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbItemName;
		private System.Windows.Forms.CheckBox cbExternItem;
		private System.Windows.Forms.TextBox tbModName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.PictureBox ImageBox;
		private System.Windows.Forms.Button btnBrowseImage;
		private System.Windows.Forms.Button btnCreateBlankImage;
		private System.Windows.Forms.Button btnEditImage;
		private System.Windows.Forms.FlowLayoutPanel flpTemplateButtonPanel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.CheckBox cbIsBelt;
		private System.Windows.Forms.Button btnSelectAreaOfTheScreen;
	}
}