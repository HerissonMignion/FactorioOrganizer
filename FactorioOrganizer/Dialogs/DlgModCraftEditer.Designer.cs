namespace FactorioOrganizer.Dialogs
{
	partial class DlgModCraftEditer
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
			this.btnBrowseRecipe = new System.Windows.Forms.Button();
			this.gbRecipe = new System.Windows.Forms.GroupBox();
			this.tbRecipeModName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tbRecipeItemName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.btnAddInput = new System.Windows.Forms.Button();
			this.btnAddOutput = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.flpInputs = new System.Windows.Forms.FlowLayoutPanel();
			this.flpOutputs = new System.Windows.Forms.FlowLayoutPanel();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.cbIsMadeInFurnace = new System.Windows.Forms.CheckBox();
			this.btnRecipeToOutput = new System.Windows.Forms.Button();
			this.gbRecipe.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnBrowseRecipe
			// 
			this.btnBrowseRecipe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowseRecipe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.btnBrowseRecipe.Location = new System.Drawing.Point(256, 16);
			this.btnBrowseRecipe.Name = "btnBrowseRecipe";
			this.btnBrowseRecipe.Size = new System.Drawing.Size(109, 36);
			this.btnBrowseRecipe.TabIndex = 1;
			this.btnBrowseRecipe.Text = "Browse recipe ...";
			this.btnBrowseRecipe.UseVisualStyleBackColor = false;
			this.btnBrowseRecipe.Click += new System.EventHandler(this.btnBrowseRecipe_Click);
			// 
			// gbRecipe
			// 
			this.gbRecipe.Controls.Add(this.tbRecipeModName);
			this.gbRecipe.Controls.Add(this.label2);
			this.gbRecipe.Controls.Add(this.tbRecipeItemName);
			this.gbRecipe.Controls.Add(this.label1);
			this.gbRecipe.Controls.Add(this.btnBrowseRecipe);
			this.gbRecipe.ForeColor = System.Drawing.Color.White;
			this.gbRecipe.Location = new System.Drawing.Point(12, 12);
			this.gbRecipe.Name = "gbRecipe";
			this.gbRecipe.Size = new System.Drawing.Size(371, 106);
			this.gbRecipe.TabIndex = 2;
			this.gbRecipe.TabStop = false;
			this.gbRecipe.Text = "Recipe";
			// 
			// tbRecipeModName
			// 
			this.tbRecipeModName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbRecipeModName.Location = new System.Drawing.Point(9, 77);
			this.tbRecipeModName.Name = "tbRecipeModName";
			this.tbRecipeModName.Size = new System.Drawing.Size(230, 20);
			this.tbRecipeModName.TabIndex = 5;
			this.tbRecipeModName.Text = "-";
			this.tbRecipeModName.TextChanged += new System.EventHandler(this.tbRecipeModName_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 61);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(320, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Item\'s mod name : (\"-\" for current mod, \"vanilla\" for vanilla factorio)";
			// 
			// tbRecipeItemName
			// 
			this.tbRecipeItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbRecipeItemName.Location = new System.Drawing.Point(9, 32);
			this.tbRecipeItemName.Name = "tbRecipeItemName";
			this.tbRecipeItemName.Size = new System.Drawing.Size(230, 20);
			this.tbRecipeItemName.TabIndex = 3;
			this.tbRecipeItemName.TextChanged += new System.EventHandler(this.tbRecipeItemName_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Item name :";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.White;
			this.label3.Location = new System.Drawing.Point(9, 140);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(62, 20);
			this.label3.TabIndex = 3;
			this.label3.Text = "Inputs :";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.White;
			this.label4.Location = new System.Drawing.Point(327, 140);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(74, 20);
			this.label4.TabIndex = 4;
			this.label4.Text = "Outputs :";
			// 
			// btnAddInput
			// 
			this.btnAddInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.btnAddInput.ForeColor = System.Drawing.Color.White;
			this.btnAddInput.Location = new System.Drawing.Point(77, 132);
			this.btnAddInput.Name = "btnAddInput";
			this.btnAddInput.Size = new System.Drawing.Size(110, 38);
			this.btnAddInput.TabIndex = 5;
			this.btnAddInput.Text = "+ Add Input";
			this.btnAddInput.UseVisualStyleBackColor = false;
			this.btnAddInput.Click += new System.EventHandler(this.btnAddInput_Click);
			// 
			// btnAddOutput
			// 
			this.btnAddOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.btnAddOutput.ForeColor = System.Drawing.Color.White;
			this.btnAddOutput.Location = new System.Drawing.Point(407, 132);
			this.btnAddOutput.Name = "btnAddOutput";
			this.btnAddOutput.Size = new System.Drawing.Size(110, 38);
			this.btnAddOutput.TabIndex = 6;
			this.btnAddOutput.Text = "+ Add Output";
			this.btnAddOutput.UseVisualStyleBackColor = false;
			this.btnAddOutput.Click += new System.EventHandler(this.btnAddOutput_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.ForeColor = System.Drawing.Color.White;
			this.label5.Location = new System.Drawing.Point(9, 177);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(147, 13);
			this.label5.TabIndex = 7;
			this.label5.Text = "Click on an input to remove it.";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.ForeColor = System.Drawing.Color.White;
			this.label6.Location = new System.Drawing.Point(328, 177);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(154, 13);
			this.label6.TabIndex = 8;
			this.label6.Text = "Click on an output to remove it.";
			// 
			// flpInputs
			// 
			this.flpInputs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.flpInputs.AutoScroll = true;
			this.flpInputs.BackColor = System.Drawing.Color.Black;
			this.flpInputs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.flpInputs.Location = new System.Drawing.Point(12, 193);
			this.flpInputs.Name = "flpInputs";
			this.flpInputs.Size = new System.Drawing.Size(313, 314);
			this.flpInputs.TabIndex = 9;
			// 
			// flpOutputs
			// 
			this.flpOutputs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.flpOutputs.AutoScroll = true;
			this.flpOutputs.BackColor = System.Drawing.Color.Black;
			this.flpOutputs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.flpOutputs.Location = new System.Drawing.Point(331, 193);
			this.flpOutputs.Name = "flpOutputs";
			this.flpOutputs.Size = new System.Drawing.Size(313, 314);
			this.flpOutputs.TabIndex = 10;
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.btnOk.ForeColor = System.Drawing.Color.White;
			this.btnOk.Location = new System.Drawing.Point(551, 12);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(93, 42);
			this.btnOk.TabIndex = 11;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = false;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.btnCancel.ForeColor = System.Drawing.Color.White;
			this.btnCancel.Location = new System.Drawing.Point(452, 12);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(93, 42);
			this.btnCancel.TabIndex = 12;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// cbIsMadeInFurnace
			// 
			this.cbIsMadeInFurnace.AutoSize = true;
			this.cbIsMadeInFurnace.ForeColor = System.Drawing.Color.White;
			this.cbIsMadeInFurnace.Location = new System.Drawing.Point(452, 69);
			this.cbIsMadeInFurnace.Name = "cbIsMadeInFurnace";
			this.cbIsMadeInFurnace.Size = new System.Drawing.Size(113, 17);
			this.cbIsMadeInFurnace.TabIndex = 13;
			this.cbIsMadeInFurnace.Text = "Is made in furnace";
			this.cbIsMadeInFurnace.UseVisualStyleBackColor = true;
			// 
			// btnRecipeToOutput
			// 
			this.btnRecipeToOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.btnRecipeToOutput.ForeColor = System.Drawing.Color.White;
			this.btnRecipeToOutput.Location = new System.Drawing.Point(523, 132);
			this.btnRecipeToOutput.Name = "btnRecipeToOutput";
			this.btnRecipeToOutput.Size = new System.Drawing.Size(110, 38);
			this.btnRecipeToOutput.TabIndex = 14;
			this.btnRecipeToOutput.Text = "Add current recipe to output";
			this.btnRecipeToOutput.UseVisualStyleBackColor = false;
			this.btnRecipeToOutput.Click += new System.EventHandler(this.btnRecipeToOutput_Click);
			// 
			// DlgModCraftEditer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.DimGray;
			this.ClientSize = new System.Drawing.Size(656, 519);
			this.Controls.Add(this.btnRecipeToOutput);
			this.Controls.Add(this.cbIsMadeInFurnace);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.flpOutputs);
			this.Controls.Add(this.flpInputs);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.btnAddOutput);
			this.Controls.Add(this.btnAddInput);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.gbRecipe);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DlgModCraftEditer";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "no title";
			this.Load += new System.EventHandler(this.DlgModCraftEditer_Load);
			this.gbRecipe.ResumeLayout(false);
			this.gbRecipe.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnBrowseRecipe;
		private System.Windows.Forms.GroupBox gbRecipe;
		private System.Windows.Forms.TextBox tbRecipeModName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbRecipeItemName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnAddInput;
		private System.Windows.Forms.Button btnAddOutput;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.FlowLayoutPanel flpInputs;
		private System.Windows.Forms.FlowLayoutPanel flpOutputs;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.CheckBox cbIsMadeInFurnace;
		private System.Windows.Forms.Button btnRecipeToOutput;
	}
}