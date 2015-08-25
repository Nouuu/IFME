﻿namespace ifme.hitoha
{
	partial class Download
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
			this.lblFile = new System.Windows.Forms.Label();
			this.lblSave = new System.Windows.Forms.Label();
			this.lblStatus = new System.Windows.Forms.Label();
			this.pbDownload = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// lblFile
			// 
			this.lblFile.AutoSize = true;
			this.lblFile.Location = new System.Drawing.Point(12, 9);
			this.lblFile.Name = "lblFile";
			this.lblFile.Size = new System.Drawing.Size(27, 13);
			this.lblFile.TabIndex = 0;
			this.lblFile.Text = "File:";
			// 
			// lblSave
			// 
			this.lblSave.AutoSize = true;
			this.lblSave.Location = new System.Drawing.Point(12, 22);
			this.lblSave.Name = "lblSave";
			this.lblSave.Size = new System.Drawing.Size(35, 13);
			this.lblSave.TabIndex = 1;
			this.lblSave.Text = "Save:";
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Location = new System.Drawing.Point(12, 48);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(38, 13);
			this.lblStatus.TabIndex = 2;
			this.lblStatus.Text = "Status";
			// 
			// pbDownload
			// 
			this.pbDownload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pbDownload.Location = new System.Drawing.Point(12, 64);
			this.pbDownload.Name = "pbDownload";
			this.pbDownload.Size = new System.Drawing.Size(610, 23);
			this.pbDownload.TabIndex = 3;
			// 
			// Download
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(634, 99);
			this.Controls.Add(this.pbDownload);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.lblSave);
			this.Controls.Add(this.lblFile);
			this.Font = new System.Drawing.Font("Tahoma", 8F);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(650, 137);
			this.MinimumSize = new System.Drawing.Size(650, 137);
			this.Name = "Download";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Download";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Download_FormClosing);
			this.Load += new System.EventHandler(this.Download_Load);
			this.Shown += new System.EventHandler(this.Download_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblFile;
		private System.Windows.Forms.Label lblSave;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.ProgressBar pbDownload;

	}
}