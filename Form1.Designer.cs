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
			this.ButtonTest = new System.Windows.Forms.Button();
			this.ButtonSave = new System.Windows.Forms.Button();
			this.ButtonOpen = new System.Windows.Forms.Button();
			this.ButtonHelp = new System.Windows.Forms.Button();
			this.ButtonTopMost = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// ButtonTest
			// 
			this.ButtonTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonTest.Location = new System.Drawing.Point(457, 95);
			this.ButtonTest.Name = "ButtonTest";
			this.ButtonTest.Size = new System.Drawing.Size(57, 28);
			this.ButtonTest.TabIndex = 0;
			this.ButtonTest.Text = "Test";
			this.ButtonTest.UseVisualStyleBackColor = true;
			this.ButtonTest.Click += new System.EventHandler(this.ButtonTest_Click);
			// 
			// ButtonSave
			// 
			this.ButtonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ButtonSave.ForeColor = System.Drawing.Color.White;
			this.ButtonSave.Location = new System.Drawing.Point(580, 2);
			this.ButtonSave.Name = "ButtonSave";
			this.ButtonSave.Size = new System.Drawing.Size(75, 37);
			this.ButtonSave.TabIndex = 1;
			this.ButtonSave.Text = "Save as...";
			this.ButtonSave.UseVisualStyleBackColor = false;
			this.ButtonSave.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ButtonSave_MouseClick);
			// 
			// ButtonOpen
			// 
			this.ButtonOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ButtonOpen.ForeColor = System.Drawing.Color.White;
			this.ButtonOpen.Location = new System.Drawing.Point(657, 2);
			this.ButtonOpen.Name = "ButtonOpen";
			this.ButtonOpen.Size = new System.Drawing.Size(75, 37);
			this.ButtonOpen.TabIndex = 2;
			this.ButtonOpen.Text = "Open";
			this.ButtonOpen.UseVisualStyleBackColor = false;
			this.ButtonOpen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ButtonOpen_MouseClick);
			// 
			// ButtonHelp
			// 
			this.ButtonHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonHelp.BackColor = System.Drawing.Color.SteelBlue;
			this.ButtonHelp.ForeColor = System.Drawing.Color.White;
			this.ButtonHelp.Location = new System.Drawing.Point(580, 40);
			this.ButtonHelp.Name = "ButtonHelp";
			this.ButtonHelp.Size = new System.Drawing.Size(152, 37);
			this.ButtonHelp.TabIndex = 3;
			this.ButtonHelp.Text = "HELP! / Readme";
			this.ButtonHelp.UseVisualStyleBackColor = false;
			this.ButtonHelp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ButtonHelp_MouseClick);
			// 
			// ButtonTopMost
			// 
			this.ButtonTopMost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonTopMost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ButtonTopMost.ForeColor = System.Drawing.Color.White;
			this.ButtonTopMost.Location = new System.Drawing.Point(580, 78);
			this.ButtonTopMost.Name = "ButtonTopMost";
			this.ButtonTopMost.Size = new System.Drawing.Size(152, 31);
			this.ButtonTopMost.TabIndex = 4;
			this.ButtonTopMost.Text = "Top most";
			this.ButtonTopMost.UseVisualStyleBackColor = false;
			this.ButtonTopMost.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ButtonTopMost_MouseClick);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.DimGray;
			this.ClientSize = new System.Drawing.Size(736, 600);
			this.Controls.Add(this.ButtonTopMost);
			this.Controls.Add(this.ButtonHelp);
			this.Controls.Add(this.ButtonOpen);
			this.Controls.Add(this.ButtonSave);
			this.Controls.Add(this.ButtonTest);
			this.KeyPreview = true;
			this.Name = "Form1";
			this.Text = "Factorio Organizer";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button ButtonTest;
		private System.Windows.Forms.Button ButtonSave;
		private System.Windows.Forms.Button ButtonOpen;
		private System.Windows.Forms.Button ButtonHelp;
		private System.Windows.Forms.Button ButtonTopMost;
	}
}

