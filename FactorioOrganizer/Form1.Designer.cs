namespace FactorioOrganizer
{
	partial class Form1
	{
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		/// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Code généré par le Concepteur Windows Form

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.ButtonSaveAs = new System.Windows.Forms.Button();
			this.ButtonOpen = new System.Windows.Forms.Button();
			this.ButtonHelp = new System.Windows.Forms.Button();
			this.ButtonTopMost = new System.Windows.Forms.Button();
			this.TabContainer = new System.Windows.Forms.TabControl();
			this.ButtonModMore = new System.Windows.Forms.Button();
			this.ButtonAutoLoadMod = new System.Windows.Forms.Button();
			this.ButtonLoadSingleMod = new System.Windows.Forms.Button();
			this.cbShowGrid = new System.Windows.Forms.CheckBox();
			this.ButtonNew = new System.Windows.Forms.Button();
			this.ButtonSave = new System.Windows.Forms.Button();
			this.ButtonUnloadMods = new System.Windows.Forms.Button();
			this.ButtonLoadModsInsideFolder = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// ButtonSaveAs
			// 
			this.ButtonSaveAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonSaveAs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ButtonSaveAs.ForeColor = System.Drawing.Color.White;
			this.ButtonSaveAs.Location = new System.Drawing.Point(773, 42);
			this.ButtonSaveAs.Name = "ButtonSaveAs";
			this.ButtonSaveAs.Size = new System.Drawing.Size(75, 37);
			this.ButtonSaveAs.TabIndex = 1;
			this.ButtonSaveAs.Text = "Save as...";
			this.ButtonSaveAs.UseVisualStyleBackColor = false;
			this.ButtonSaveAs.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ButtonSaveAs_MouseClick);
			// 
			// ButtonOpen
			// 
			this.ButtonOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ButtonOpen.ForeColor = System.Drawing.Color.White;
			this.ButtonOpen.Location = new System.Drawing.Point(773, 2);
			this.ButtonOpen.Name = "ButtonOpen";
			this.ButtonOpen.Size = new System.Drawing.Size(75, 37);
			this.ButtonOpen.TabIndex = 2;
			this.ButtonOpen.Text = "Open";
			this.ButtonOpen.UseVisualStyleBackColor = false;
			this.ButtonOpen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ButtonOpen_MouseClick);
			// 
			// ButtonHelp
			// 
			this.ButtonHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonHelp.BackColor = System.Drawing.Color.SteelBlue;
			this.ButtonHelp.ForeColor = System.Drawing.Color.White;
			this.ButtonHelp.Location = new System.Drawing.Point(696, 570);
			this.ButtonHelp.Name = "ButtonHelp";
			this.ButtonHelp.Size = new System.Drawing.Size(152, 46);
			this.ButtonHelp.TabIndex = 3;
			this.ButtonHelp.Text = "HELP! / Readme";
			this.ButtonHelp.UseVisualStyleBackColor = false;
			this.ButtonHelp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ButtonHelp_MouseClick);
			// 
			// ButtonTopMost
			// 
			this.ButtonTopMost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonTopMost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ButtonTopMost.ForeColor = System.Drawing.Color.White;
			this.ButtonTopMost.Location = new System.Drawing.Point(696, 617);
			this.ButtonTopMost.Name = "ButtonTopMost";
			this.ButtonTopMost.Size = new System.Drawing.Size(152, 45);
			this.ButtonTopMost.TabIndex = 4;
			this.ButtonTopMost.Text = "Top most";
			this.ButtonTopMost.UseVisualStyleBackColor = false;
			this.ButtonTopMost.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ButtonTopMost_MouseClick);
			// 
			// TabContainer
			// 
			this.TabContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TabContainer.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.TabContainer.Location = new System.Drawing.Point(3, 2);
			this.TabContainer.Name = "TabContainer";
			this.TabContainer.SelectedIndex = 0;
			this.TabContainer.Size = new System.Drawing.Size(687, 130);
			this.TabContainer.TabIndex = 5;
			// 
			// ButtonModMore
			// 
			this.ButtonModMore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonModMore.BackColor = System.Drawing.Color.DimGray;
			this.ButtonModMore.ForeColor = System.Drawing.Color.White;
			this.ButtonModMore.Location = new System.Drawing.Point(696, 109);
			this.ButtonModMore.Name = "ButtonModMore";
			this.ButtonModMore.Size = new System.Drawing.Size(152, 46);
			this.ButtonModMore.TabIndex = 6;
			this.ButtonModMore.Text = "Mod Editer ...";
			this.ButtonModMore.UseVisualStyleBackColor = false;
			this.ButtonModMore.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ButtonModMore_MouseClick);
			// 
			// ButtonAutoLoadMod
			// 
			this.ButtonAutoLoadMod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonAutoLoadMod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ButtonAutoLoadMod.ForeColor = System.Drawing.Color.White;
			this.ButtonAutoLoadMod.Location = new System.Drawing.Point(696, 155);
			this.ButtonAutoLoadMod.Name = "ButtonAutoLoadMod";
			this.ButtonAutoLoadMod.Size = new System.Drawing.Size(152, 46);
			this.ButtonAutoLoadMod.TabIndex = 7;
			this.ButtonAutoLoadMod.Text = "Auto load every mods in the program directory";
			this.ButtonAutoLoadMod.UseVisualStyleBackColor = false;
			this.ButtonAutoLoadMod.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ButtonAutoLoadMod_MouseClick);
			// 
			// ButtonLoadSingleMod
			// 
			this.ButtonLoadSingleMod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonLoadSingleMod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ButtonLoadSingleMod.ForeColor = System.Drawing.Color.White;
			this.ButtonLoadSingleMod.Location = new System.Drawing.Point(696, 247);
			this.ButtonLoadSingleMod.Name = "ButtonLoadSingleMod";
			this.ButtonLoadSingleMod.Size = new System.Drawing.Size(152, 45);
			this.ButtonLoadSingleMod.TabIndex = 8;
			this.ButtonLoadSingleMod.Text = "Load a single mod";
			this.ButtonLoadSingleMod.UseVisualStyleBackColor = false;
			this.ButtonLoadSingleMod.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ButtonLoadSingleMod_MouseClick);
			// 
			// cbShowGrid
			// 
			this.cbShowGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cbShowGrid.AutoSize = true;
			this.cbShowGrid.ForeColor = System.Drawing.Color.White;
			this.cbShowGrid.Location = new System.Drawing.Point(696, 368);
			this.cbShowGrid.Name = "cbShowGrid";
			this.cbShowGrid.Size = new System.Drawing.Size(73, 17);
			this.cbShowGrid.TabIndex = 9;
			this.cbShowGrid.Text = "Show grid";
			this.cbShowGrid.UseVisualStyleBackColor = true;
			this.cbShowGrid.CheckedChanged += new System.EventHandler(this.cbShowGrid_CheckedChanged);
			// 
			// ButtonNew
			// 
			this.ButtonNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ButtonNew.ForeColor = System.Drawing.Color.White;
			this.ButtonNew.Location = new System.Drawing.Point(696, 2);
			this.ButtonNew.Name = "ButtonNew";
			this.ButtonNew.Size = new System.Drawing.Size(75, 37);
			this.ButtonNew.TabIndex = 10;
			this.ButtonNew.Text = "New";
			this.ButtonNew.UseVisualStyleBackColor = false;
			this.ButtonNew.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ButtonNew_MouseClick);
			// 
			// ButtonSave
			// 
			this.ButtonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ButtonSave.ForeColor = System.Drawing.Color.White;
			this.ButtonSave.Location = new System.Drawing.Point(696, 42);
			this.ButtonSave.Name = "ButtonSave";
			this.ButtonSave.Size = new System.Drawing.Size(75, 37);
			this.ButtonSave.TabIndex = 11;
			this.ButtonSave.Text = "Save";
			this.ButtonSave.UseVisualStyleBackColor = false;
			this.ButtonSave.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ButtonSave_MouseClick);
			// 
			// ButtonUnloadMods
			// 
			this.ButtonUnloadMods.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonUnloadMods.BackColor = System.Drawing.Color.Maroon;
			this.ButtonUnloadMods.ForeColor = System.Drawing.Color.White;
			this.ButtonUnloadMods.Location = new System.Drawing.Point(696, 293);
			this.ButtonUnloadMods.Name = "ButtonUnloadMods";
			this.ButtonUnloadMods.Size = new System.Drawing.Size(152, 45);
			this.ButtonUnloadMods.TabIndex = 12;
			this.ButtonUnloadMods.Text = "Unload every mods";
			this.ButtonUnloadMods.UseVisualStyleBackColor = false;
			this.ButtonUnloadMods.Click += new System.EventHandler(this.ButtonUnloadMods_Click);
			// 
			// ButtonLoadModsInsideFolder
			// 
			this.ButtonLoadModsInsideFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonLoadModsInsideFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(0)))));
			this.ButtonLoadModsInsideFolder.ForeColor = System.Drawing.Color.White;
			this.ButtonLoadModsInsideFolder.Location = new System.Drawing.Point(696, 201);
			this.ButtonLoadModsInsideFolder.Name = "ButtonLoadModsInsideFolder";
			this.ButtonLoadModsInsideFolder.Size = new System.Drawing.Size(152, 46);
			this.ButtonLoadModsInsideFolder.TabIndex = 13;
			this.ButtonLoadModsInsideFolder.Text = "Load every mods of a folder";
			this.ButtonLoadModsInsideFolder.UseVisualStyleBackColor = false;
			this.ButtonLoadModsInsideFolder.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ButtonLoadModsInsideFolder_MouseClick);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.DimGray;
			this.ClientSize = new System.Drawing.Size(850, 665);
			this.Controls.Add(this.ButtonLoadModsInsideFolder);
			this.Controls.Add(this.ButtonUnloadMods);
			this.Controls.Add(this.ButtonSave);
			this.Controls.Add(this.ButtonNew);
			this.Controls.Add(this.cbShowGrid);
			this.Controls.Add(this.ButtonLoadSingleMod);
			this.Controls.Add(this.ButtonAutoLoadMod);
			this.Controls.Add(this.ButtonModMore);
			this.Controls.Add(this.TabContainer);
			this.Controls.Add(this.ButtonTopMost);
			this.Controls.Add(this.ButtonHelp);
			this.Controls.Add(this.ButtonOpen);
			this.Controls.Add(this.ButtonSaveAs);
			this.KeyPreview = true;
			this.Name = "Form1";
			this.Text = "Factorio Organizer";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button ButtonSaveAs;
		private System.Windows.Forms.Button ButtonOpen;
		private System.Windows.Forms.Button ButtonHelp;
		private System.Windows.Forms.Button ButtonTopMost;
		private System.Windows.Forms.TabControl TabContainer;
		private System.Windows.Forms.Button ButtonModMore;
		private System.Windows.Forms.Button ButtonAutoLoadMod;
		private System.Windows.Forms.Button ButtonLoadSingleMod;
		private System.Windows.Forms.CheckBox cbShowGrid;
		private System.Windows.Forms.Button ButtonNew;
		private System.Windows.Forms.Button ButtonSave;
		private System.Windows.Forms.Button ButtonUnloadMods;
		private System.Windows.Forms.Button ButtonLoadModsInsideFolder;
	}
}

