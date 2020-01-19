namespace FactorioOrganizer
{
	partial class FormModEditer
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
			this.ButtonReturnToForm1 = new System.Windows.Forms.Button();
			this.gbItems = new System.Windows.Forms.GroupBox();
			this.MainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.gbCrafts = new System.Windows.Forms.GroupBox();
			this.ButtonSaveAs = new System.Windows.Forms.Button();
			this.ButtonOpen = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.tbModName = new System.Windows.Forms.TextBox();
			this.ButtonNew = new System.Windows.Forms.Button();
			this.ButtonSave = new System.Windows.Forms.Button();
			this.ButtonHelp = new System.Windows.Forms.Button();
			this.MainTableLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// ButtonReturnToForm1
			// 
			this.ButtonReturnToForm1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ButtonReturnToForm1.Location = new System.Drawing.Point(2, 2);
			this.ButtonReturnToForm1.Name = "ButtonReturnToForm1";
			this.ButtonReturnToForm1.Size = new System.Drawing.Size(90, 42);
			this.ButtonReturnToForm1.TabIndex = 0;
			this.ButtonReturnToForm1.Text = "Return to the main window";
			this.ButtonReturnToForm1.UseVisualStyleBackColor = false;
			this.ButtonReturnToForm1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ButtonReturnToForm1_MouseClick);
			// 
			// gbItems
			// 
			this.gbItems.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbItems.ForeColor = System.Drawing.Color.White;
			this.gbItems.Location = new System.Drawing.Point(3, 3);
			this.gbItems.Name = "gbItems";
			this.gbItems.Size = new System.Drawing.Size(394, 725);
			this.gbItems.TabIndex = 0;
			this.gbItems.TabStop = false;
			this.gbItems.Text = "Items";
			// 
			// MainTableLayoutPanel
			// 
			this.MainTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainTableLayoutPanel.ColumnCount = 2;
			this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
			this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.MainTableLayoutPanel.Controls.Add(this.gbItems, 0, 0);
			this.MainTableLayoutPanel.Controls.Add(this.gbCrafts, 1, 0);
			this.MainTableLayoutPanel.Location = new System.Drawing.Point(98, 44);
			this.MainTableLayoutPanel.Name = "MainTableLayoutPanel";
			this.MainTableLayoutPanel.RowCount = 1;
			this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 731F));
			this.MainTableLayoutPanel.Size = new System.Drawing.Size(1024, 731);
			this.MainTableLayoutPanel.TabIndex = 1;
			// 
			// gbCrafts
			// 
			this.gbCrafts.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbCrafts.ForeColor = System.Drawing.Color.White;
			this.gbCrafts.Location = new System.Drawing.Point(403, 3);
			this.gbCrafts.Name = "gbCrafts";
			this.gbCrafts.Size = new System.Drawing.Size(618, 725);
			this.gbCrafts.TabIndex = 1;
			this.gbCrafts.TabStop = false;
			this.gbCrafts.Text = "Crafts";
			// 
			// ButtonSaveAs
			// 
			this.ButtonSaveAs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ButtonSaveAs.Location = new System.Drawing.Point(2, 183);
			this.ButtonSaveAs.Name = "ButtonSaveAs";
			this.ButtonSaveAs.Size = new System.Drawing.Size(90, 42);
			this.ButtonSaveAs.TabIndex = 2;
			this.ButtonSaveAs.Text = "Save as...";
			this.ButtonSaveAs.UseVisualStyleBackColor = false;
			this.ButtonSaveAs.Click += new System.EventHandler(this.ButtonSaveAs_Click);
			// 
			// ButtonOpen
			// 
			this.ButtonOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ButtonOpen.Location = new System.Drawing.Point(2, 99);
			this.ButtonOpen.Name = "ButtonOpen";
			this.ButtonOpen.Size = new System.Drawing.Size(90, 42);
			this.ButtonOpen.TabIndex = 3;
			this.ButtonOpen.Text = "Open";
			this.ButtonOpen.UseVisualStyleBackColor = false;
			this.ButtonOpen.Click += new System.EventHandler(this.ButtonOpen_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(135, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Mod name:";
			// 
			// tbModName
			// 
			this.tbModName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbModName.Location = new System.Drawing.Point(201, 6);
			this.tbModName.Name = "tbModName";
			this.tbModName.Size = new System.Drawing.Size(244, 20);
			this.tbModName.TabIndex = 5;
			this.tbModName.Text = "noname";
			this.tbModName.TextChanged += new System.EventHandler(this.tbModName_TextChanged);
			// 
			// ButtonNew
			// 
			this.ButtonNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ButtonNew.Location = new System.Drawing.Point(2, 57);
			this.ButtonNew.Name = "ButtonNew";
			this.ButtonNew.Size = new System.Drawing.Size(90, 42);
			this.ButtonNew.TabIndex = 6;
			this.ButtonNew.Text = "New";
			this.ButtonNew.UseVisualStyleBackColor = false;
			this.ButtonNew.Click += new System.EventHandler(this.ButtonNew_Click);
			// 
			// ButtonSave
			// 
			this.ButtonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ButtonSave.Location = new System.Drawing.Point(2, 141);
			this.ButtonSave.Name = "ButtonSave";
			this.ButtonSave.Size = new System.Drawing.Size(90, 42);
			this.ButtonSave.TabIndex = 7;
			this.ButtonSave.Text = "Save";
			this.ButtonSave.UseVisualStyleBackColor = false;
			this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
			// 
			// ButtonHelp
			// 
			this.ButtonHelp.BackColor = System.Drawing.Color.SteelBlue;
			this.ButtonHelp.Location = new System.Drawing.Point(2, 269);
			this.ButtonHelp.Name = "ButtonHelp";
			this.ButtonHelp.Size = new System.Drawing.Size(90, 42);
			this.ButtonHelp.TabIndex = 8;
			this.ButtonHelp.Text = "Help";
			this.ButtonHelp.UseVisualStyleBackColor = false;
			this.ButtonHelp.Click += new System.EventHandler(this.ButtonHelp_Click);
			// 
			// FormModEditer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.DimGray;
			this.ClientSize = new System.Drawing.Size(1134, 787);
			this.Controls.Add(this.ButtonHelp);
			this.Controls.Add(this.ButtonSave);
			this.Controls.Add(this.ButtonNew);
			this.Controls.Add(this.tbModName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ButtonOpen);
			this.Controls.Add(this.ButtonSaveAs);
			this.Controls.Add(this.MainTableLayoutPanel);
			this.Controls.Add(this.ButtonReturnToForm1);
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "FormModEditer";
			this.Text = "Mod File Maker/Editer";
			this.Load += new System.EventHandler(this.FormModEditer_Load);
			this.MainTableLayoutPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button ButtonReturnToForm1;
		private System.Windows.Forms.GroupBox gbItems;
		private System.Windows.Forms.TableLayoutPanel MainTableLayoutPanel;
		private System.Windows.Forms.GroupBox gbCrafts;
		private System.Windows.Forms.Button ButtonSaveAs;
		private System.Windows.Forms.Button ButtonOpen;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbModName;
		private System.Windows.Forms.Button ButtonNew;
		private System.Windows.Forms.Button ButtonSave;
		private System.Windows.Forms.Button ButtonHelp;
	}
}