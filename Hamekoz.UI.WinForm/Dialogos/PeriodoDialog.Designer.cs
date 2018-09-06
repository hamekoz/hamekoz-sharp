//
//  PeriodoDialog.Designer.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//       Hernan Vivani <hernan@vivani.com.ar> - http://hvivani.com.ar
//
//  Copyright (c) 2015 Hamekoz - www.hamekoz.com.ar
//  Copyright (c) 2010 SOffT - http://www.sofft.com.ar
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.



namespace Hamekoz.UI.WinForm
{
	partial class PeriodoDialog
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
			this.dtpDesde = new System.Windows.Forms.DateTimePicker ();
			this.dtpHasta = new System.Windows.Forms.DateTimePicker ();
			this.lblDesde = new System.Windows.Forms.Label ();
			this.lblHasta = new System.Windows.Forms.Label ();
			this.btnCancelar = new System.Windows.Forms.Button ();
			this.btnAceptar = new System.Windows.Forms.Button ();
			this.SuspendLayout ();
			//
			// dtpDesde
			//
			this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtpDesde.Location = new System.Drawing.Point (69, 14);
			this.dtpDesde.Name = "dtpDesde";
			this.dtpDesde.Size = new System.Drawing.Size (104, 20);
			this.dtpDesde.TabIndex = 19;
			this.dtpDesde.ValueChanged += new System.EventHandler (this.dtpDesde_ValueChanged);
			//
			// dtpHasta
			//
			this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtpHasta.Location = new System.Drawing.Point (69, 53);
			this.dtpHasta.Name = "dtpHasta";
			this.dtpHasta.Size = new System.Drawing.Size (104, 20);
			this.dtpHasta.TabIndex = 20;
			this.dtpHasta.ValueChanged += new System.EventHandler (this.dtpHasta_ValueChanged);
			//
			// lblDesde
			//
			this.lblDesde.AutoSize = true;
			this.lblDesde.Font = new System.Drawing.Font ("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDesde.ForeColor = System.Drawing.Color.FromArgb (((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.lblDesde.Location = new System.Drawing.Point (19, 18);
			this.lblDesde.Name = "lblDesde";
			this.lblDesde.Size = new System.Drawing.Size (47, 13);
			this.lblDesde.TabIndex = 21;
			this.lblDesde.Text = "Desde:";
			//
			// lblHasta
			//
			this.lblHasta.AutoSize = true;
			this.lblHasta.Font = new System.Drawing.Font ("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblHasta.ForeColor = System.Drawing.Color.FromArgb (((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.lblHasta.Location = new System.Drawing.Point (19, 57);
			this.lblHasta.Name = "lblHasta";
			this.lblHasta.Size = new System.Drawing.Size (44, 13);
			this.lblHasta.TabIndex = 22;
			this.lblHasta.Text = "Hasta:";
			//
			// btnCancelar
			//
			this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancelar.Location = new System.Drawing.Point (115, 93);
			this.btnCancelar.Name = "btnCancelar";
			this.btnCancelar.Size = new System.Drawing.Size (75, 23);
			this.btnCancelar.TabIndex = 24;
			this.btnCancelar.Text = "Cancelar";
			this.btnCancelar.UseVisualStyleBackColor = true;
			this.btnCancelar.Click += new System.EventHandler (this.btnCancelar_Click);
			//
			// btnAceptar
			//
			this.btnAceptar.Location = new System.Drawing.Point (22, 93);
			this.btnAceptar.Name = "btnAceptar";
			this.btnAceptar.Size = new System.Drawing.Size (75, 23);
			this.btnAceptar.TabIndex = 23;
			this.btnAceptar.Text = "Aceptar";
			this.btnAceptar.UseVisualStyleBackColor = true;
			this.btnAceptar.Click += new System.EventHandler (this.btnAceptar_Click);
			//
			// frmRangoFechas
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size (213, 128);
			this.ControlBox = false;
			this.Controls.Add (this.btnCancelar);
			this.Controls.Add (this.btnAceptar);
			this.Controls.Add (this.lblHasta);
			this.Controls.Add (this.lblDesde);
			this.Controls.Add (this.dtpHasta);
			this.Controls.Add (this.dtpDesde);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmRangoFechas";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Rango de Fechas";
			this.ResumeLayout (false);
			this.PerformLayout ();

		}

		#endregion

		private System.Windows.Forms.DateTimePicker dtpDesde;
		private System.Windows.Forms.DateTimePicker dtpHasta;
		private System.Windows.Forms.Label lblDesde;
		private System.Windows.Forms.Label lblHasta;
		private System.Windows.Forms.Button btnCancelar;
		private System.Windows.Forms.Button btnAceptar;
	}
}