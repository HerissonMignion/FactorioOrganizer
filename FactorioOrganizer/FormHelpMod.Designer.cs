namespace FactorioOrganizer
{
	partial class FormHelpMod
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHelpMod));
			this.MainPanel = new System.Windows.Forms.Panel();
			this.MainTextBox = new System.Windows.Forms.TextBox();
			this.MainPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainPanel
			// 
			this.MainPanel.AutoScroll = true;
			this.MainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.MainPanel.Controls.Add(this.MainTextBox);
			this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainPanel.Location = new System.Drawing.Point(0, 0);
			this.MainPanel.Name = "MainPanel";
			this.MainPanel.Size = new System.Drawing.Size(681, 581);
			this.MainPanel.TabIndex = 0;
			// 
			// MainTextBox
			// 
			this.MainTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.MainTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.MainTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MainTextBox.ForeColor = System.Drawing.Color.White;
			this.MainTextBox.Location = new System.Drawing.Point(0, 0);
			this.MainTextBox.Multiline = true;
			this.MainTextBox.Name = "MainTextBox";
			this.MainTextBox.ReadOnly = true;
			this.MainTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.MainTextBox.Size = new System.Drawing.Size(681, 581);
			this.MainTextBox.TabIndex = 0;
			this.MainTextBox.Text = resources.GetString("MainTextBox.Text");
			// 
			// FormHelpMod
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.DimGray;
			this.ClientSize = new System.Drawing.Size(681, 581);
			this.Controls.Add(this.MainPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormHelpMod";
			this.ShowInTaskbar = false;
			this.Text = "Help Mods";
			this.Load += new System.EventHandler(this.FormHelpMod_Load);
			this.MainPanel.ResumeLayout(false);
			this.MainPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel MainPanel;
		private System.Windows.Forms.TextBox MainTextBox;
	}
}