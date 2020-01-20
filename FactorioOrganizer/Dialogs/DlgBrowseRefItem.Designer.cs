namespace FactorioOrganizer.Dialogs
{
	partial class DlgBrowseRefItem
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
			this.rbExternalModItem = new System.Windows.Forms.RadioButton();
			this.rbInternalModItem = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.tbItemName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tbModName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.flpInternalItems = new System.Windows.Forms.FlowLayoutPanel();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// rbExternalModItem
			// 
			this.rbExternalModItem.AutoSize = true;
			this.rbExternalModItem.ForeColor = System.Drawing.Color.White;
			this.rbExternalModItem.Location = new System.Drawing.Point(12, 12);
			this.rbExternalModItem.Name = "rbExternalModItem";
			this.rbExternalModItem.Size = new System.Drawing.Size(85, 17);
			this.rbExternalModItem.TabIndex = 0;
			this.rbExternalModItem.Text = "External item";
			this.rbExternalModItem.UseVisualStyleBackColor = true;
			this.rbExternalModItem.CheckedChanged += new System.EventHandler(this.rbExternalModItem_CheckedChanged);
			// 
			// rbInternalModItem
			// 
			this.rbInternalModItem.AutoSize = true;
			this.rbInternalModItem.Checked = true;
			this.rbInternalModItem.ForeColor = System.Drawing.Color.White;
			this.rbInternalModItem.Location = new System.Drawing.Point(12, 132);
			this.rbInternalModItem.Name = "rbInternalModItem";
			this.rbInternalModItem.Size = new System.Drawing.Size(98, 17);
			this.rbInternalModItem.TabIndex = 1;
			this.rbInternalModItem.TabStop = true;
			this.rbInternalModItem.Text = "Internal file item";
			this.rbInternalModItem.UseVisualStyleBackColor = true;
			this.rbInternalModItem.CheckedChanged += new System.EventHandler(this.rbInternalModItem_CheckedChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(12, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Item name :";
			// 
			// tbItemName
			// 
			this.tbItemName.Location = new System.Drawing.Point(15, 48);
			this.tbItemName.Name = "tbItemName";
			this.tbItemName.Size = new System.Drawing.Size(239, 20);
			this.tbItemName.TabIndex = 3;
			this.tbItemName.TextChanged += new System.EventHandler(this.tbItemName_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(12, 79);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(320, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Item\'s mod name : (\"-\" for current mod, \"vanilla\" for vanilla factorio)";
			// 
			// tbModName
			// 
			this.tbModName.Location = new System.Drawing.Point(15, 95);
			this.tbModName.Name = "tbModName";
			this.tbModName.Size = new System.Drawing.Size(239, 20);
			this.tbModName.TabIndex = 5;
			this.tbModName.TextChanged += new System.EventHandler(this.tbModName_TextChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.Color.White;
			this.label3.Location = new System.Drawing.Point(12, 152);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Select an item :";
			// 
			// flpInternalItems
			// 
			this.flpInternalItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flpInternalItems.AutoScroll = true;
			this.flpInternalItems.BackColor = System.Drawing.Color.Black;
			this.flpInternalItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.flpInternalItems.Location = new System.Drawing.Point(12, 168);
			this.flpInternalItems.Name = "flpInternalItems";
			this.flpInternalItems.Size = new System.Drawing.Size(326, 270);
			this.flpInternalItems.TabIndex = 7;
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(282, 4);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(56, 33);
			this.btnOk.TabIndex = 8;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(220, 4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(56, 33);
			this.btnCancel.TabIndex = 9;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// DlgBrowseRefItem
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.DimGray;
			this.ClientSize = new System.Drawing.Size(350, 450);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.flpInternalItems);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.tbModName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbItemName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.rbInternalModItem);
			this.Controls.Add(this.rbExternalModItem);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DlgBrowseRefItem";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "no title";
			this.Load += new System.EventHandler(this.DlgBrowseRefItem_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RadioButton rbExternalModItem;
		private System.Windows.Forms.RadioButton rbInternalModItem;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbItemName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbModName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.FlowLayoutPanel flpInternalItems;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
	}
}