//
//  Splash.designer.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2014 Hamekoz
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace Hamekoz.UI.WinForm
{
	partial class Splash
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose (bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose ();
			}
			base.Dispose (disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
			this.progressBarSplash = new System.Windows.Forms.ProgressBar ();
			this.pictureBoxSplash = new System.Windows.Forms.PictureBox ();
			this.labelSplash = new System.Windows.Forms.Label ();
			this.labelApp = new System.Windows.Forms.Label ();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxSplash)).BeginInit ();
			this.SuspendLayout ();
			// 
			// progressBarSplash
			// 
			this.progressBarSplash.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.progressBarSplash.Location = new System.Drawing.Point (0, 277);
			this.progressBarSplash.Name = "progressBarSplash";
			this.progressBarSplash.Size = new System.Drawing.Size (450, 23);
			this.progressBarSplash.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progressBarSplash.TabIndex = 0;
			// 
			// pictureBoxSplash
			// 
			this.pictureBoxSplash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBoxSplash.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBoxSplash.Location = new System.Drawing.Point (0, 0);
			this.pictureBoxSplash.Name = "pictureBoxSplash";
			this.pictureBoxSplash.Size = new System.Drawing.Size (450, 277);
			this.pictureBoxSplash.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBoxSplash.TabIndex = 2;
			this.pictureBoxSplash.TabStop = false;
			// 
			// labelSplash
			// 
			this.labelSplash.AutoSize = true;
			this.labelSplash.BackColor = System.Drawing.Color.Transparent;
			this.labelSplash.Location = new System.Drawing.Point (12, 261);
			this.labelSplash.Name = "labelSplash";
			this.labelSplash.Size = new System.Drawing.Size (59, 13);
			this.labelSplash.TabIndex = 3;
			this.labelSplash.Text = "Iniciando...";
			// 
			// labelApp
			// 
			this.labelApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelApp.BackColor = System.Drawing.Color.Transparent;
			this.labelApp.Location = new System.Drawing.Point (222, 258);
			this.labelApp.Name = "labelApp";
			this.labelApp.Size = new System.Drawing.Size (216, 16);
			this.labelApp.TabIndex = 4;
			this.labelApp.Text = "App 1.0.0";
			this.labelApp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// Splash
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size (450, 300);
			this.Controls.Add (this.labelApp);
			this.Controls.Add (this.labelSplash);
			this.Controls.Add (this.pictureBoxSplash);
			this.Controls.Add (this.progressBarSplash);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Splash";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Splash";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxSplash)).EndInit ();
			this.ResumeLayout (false);
			this.PerformLayout ();

		}

		#endregion

		private System.Windows.Forms.ProgressBar progressBarSplash;
		private System.Windows.Forms.PictureBox pictureBoxSplash;
		private System.Windows.Forms.Label labelSplash;
		private System.Windows.Forms.Label labelApp;
	}
}