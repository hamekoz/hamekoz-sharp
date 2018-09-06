//
//  SeleccionFechaDialog.Designer.cs
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
	partial class SeleccionFechaDialog
	{
		/// <summary>
		/// Variable del diseñador requerida.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Limpiar los recursos que se estén utilizando.
		/// </summary>
		/// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
		protected override void Dispose (bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose ();
			}
			base.Dispose (disposing);
		}

		#region Código generado por el Diseñador de Windows Forms

		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido del método con el editor de código.
		/// </summary>
		private void InitializeComponent ()
		{
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker ();
			this.btnCancelar = new System.Windows.Forms.Button ();
			this.btnAceptar = new System.Windows.Forms.Button ();
			this.SuspendLayout ();
			//
			// dateTimePicker1
			//
			this.dateTimePicker1.CustomFormat = "";
			this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker1.Location = new System.Drawing.Point (47, 12);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size (92, 20);
			this.dateTimePicker1.TabIndex = 26;
			this.dateTimePicker1.Value = new System.DateTime (2008, 12, 1, 0, 0, 0, 0);
			//
			// btnCancelar
			//
			this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancelar.Location = new System.Drawing.Point (93, 48);
			this.btnCancelar.Name = "btnCancelar";
			this.btnCancelar.Size = new System.Drawing.Size (75, 23);
			this.btnCancelar.TabIndex = 28;
			this.btnCancelar.Text = "Cancelar";
			this.btnCancelar.UseVisualStyleBackColor = true;
			this.btnCancelar.Click += new System.EventHandler (this.btnCancelar_Click);
			//
			// btnAceptar
			//
			this.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnAceptar.Location = new System.Drawing.Point (12, 48);
			this.btnAceptar.Name = "btnAceptar";
			this.btnAceptar.Size = new System.Drawing.Size (75, 23);
			this.btnAceptar.TabIndex = 27;
			this.btnAceptar.Text = "&Aceptar";
			this.btnAceptar.UseVisualStyleBackColor = true;
			this.btnAceptar.Click += new System.EventHandler (this.btnAceptar_Click);
			//
			// frmSeleccionFecha
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size (177, 79);
			this.ControlBox = false;
			this.Controls.Add (this.btnCancelar);
			this.Controls.Add (this.btnAceptar);
			this.Controls.Add (this.dateTimePicker1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "frmSeleccionFecha";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Seleccione la Fecha";
			this.ResumeLayout (false);

		}

		#endregion

		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.Button btnCancelar;
		private System.Windows.Forms.Button btnAceptar;
	}
}