namespace FactorioOrganizer.Dialogs
{
	partial class DlgRescaleImage
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
			this.ButtonOk = new System.Windows.Forms.Button();
			this.ButtonCancel = new System.Windows.Forms.Button();
			this.gbResult = new System.Windows.Forms.GroupBox();
			this.pImageContainer = new System.Windows.Forms.Panel();
			this.ImageBox = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.nudWidth = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.nudHeight = new System.Windows.Forms.NumericUpDown();
			this.gbResult.SuspendLayout();
			this.pImageContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ImageBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
			this.SuspendLayout();
			// 
			// ButtonOk
			// 
			this.ButtonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonOk.Location = new System.Drawing.Point(469, 12);
			this.ButtonOk.Name = "ButtonOk";
			this.ButtonOk.Size = new System.Drawing.Size(69, 33);
			this.ButtonOk.TabIndex = 0;
			this.ButtonOk.Text = "Ok";
			this.ButtonOk.UseVisualStyleBackColor = true;
			this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
			// 
			// ButtonCancel
			// 
			this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonCancel.Location = new System.Drawing.Point(394, 12);
			this.ButtonCancel.Name = "ButtonCancel";
			this.ButtonCancel.Size = new System.Drawing.Size(69, 33);
			this.ButtonCancel.TabIndex = 1;
			this.ButtonCancel.Text = "Cancel";
			this.ButtonCancel.UseVisualStyleBackColor = true;
			this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
			// 
			// gbResult
			// 
			this.gbResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbResult.Controls.Add(this.pImageContainer);
			this.gbResult.Location = new System.Drawing.Point(12, 51);
			this.gbResult.Name = "gbResult";
			this.gbResult.Size = new System.Drawing.Size(526, 383);
			this.gbResult.TabIndex = 2;
			this.gbResult.TabStop = false;
			this.gbResult.Text = "Result";
			// 
			// pImageContainer
			// 
			this.pImageContainer.AutoScroll = true;
			this.pImageContainer.Controls.Add(this.ImageBox);
			this.pImageContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pImageContainer.Location = new System.Drawing.Point(3, 16);
			this.pImageContainer.Name = "pImageContainer";
			this.pImageContainer.Size = new System.Drawing.Size(520, 364);
			this.pImageContainer.TabIndex = 0;
			// 
			// ImageBox
			// 
			this.ImageBox.Location = new System.Drawing.Point(3, 3);
			this.ImageBox.Name = "ImageBox";
			this.ImageBox.Size = new System.Drawing.Size(216, 182);
			this.ImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.ImageBox.TabIndex = 0;
			this.ImageBox.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(63, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "New width :";
			// 
			// nudWidth
			// 
			this.nudWidth.Location = new System.Drawing.Point(75, 20);
			this.nudWidth.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
			this.nudWidth.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.nudWidth.Name = "nudWidth";
			this.nudWidth.Size = new System.Drawing.Size(58, 20);
			this.nudWidth.TabIndex = 4;
			this.nudWidth.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.nudWidth.ValueChanged += new System.EventHandler(this.nudWidth_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(139, 22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(67, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "New height :";
			// 
			// nudHeight
			// 
			this.nudHeight.Location = new System.Drawing.Point(206, 20);
			this.nudHeight.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
			this.nudHeight.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.nudHeight.Name = "nudHeight";
			this.nudHeight.Size = new System.Drawing.Size(58, 20);
			this.nudHeight.TabIndex = 6;
			this.nudHeight.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.nudHeight.ValueChanged += new System.EventHandler(this.nudHeight_ValueChanged);
			// 
			// DlgRescaleImage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(550, 446);
			this.Controls.Add(this.nudHeight);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.nudWidth);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.gbResult);
			this.Controls.Add(this.ButtonCancel);
			this.Controls.Add(this.ButtonOk);
			this.MinimizeBox = false;
			this.Name = "DlgRescaleImage";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Rescale Image";
			this.Load += new System.EventHandler(this.DlgRescaleImage_Load);
			this.gbResult.ResumeLayout(false);
			this.pImageContainer.ResumeLayout(false);
			this.pImageContainer.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ImageBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button ButtonOk;
		private System.Windows.Forms.Button ButtonCancel;
		private System.Windows.Forms.GroupBox gbResult;
		private System.Windows.Forms.Panel pImageContainer;
		private System.Windows.Forms.PictureBox ImageBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown nudWidth;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown nudHeight;
	}
}