//
//  SeleccionValorDialog.Designer.cs
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
	partial class SeleccionValorDialog
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
			this.btnCancelar = new System.Windows.Forms.Button ();
			this.btnAceptar = new System.Windows.Forms.Button ();
			this.txtValor = new System.Windows.Forms.TextBox ();
			this.SuspendLayout ();
			//
			// btnCancelar
			//
			this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancelar.Location = new System.Drawing.Point (93, 48);
			this.btnCancelar.Name = "btnCancelar";
			this.btnCancelar.Size = new System.Drawing.Size (75, 23);
			this.btnCancelar.TabIndex = 2;
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
			this.btnAceptar.TabIndex = 1;
			this.btnAceptar.Text = "&Aceptar";
			this.btnAceptar.UseVisualStyleBackColor = true;
			this.btnAceptar.Click += new System.EventHandler (this.btnAceptar_Click);
			//
			// txtValor
			//
			this.txtValor.Location = new System.Drawing.Point (47, 11);
			this.txtValor.MaxLength = 20;
			this.txtValor.Name = "txtValor";
			this.txtValor.Size = new System.Drawing.Size (85, 20);
			this.txtValor.TabIndex = 0;
			this.txtValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			//
			// frmSeleccionValor
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size (177, 79);
			this.ControlBox = false;
			this.Controls.Add (this.txtValor);
			this.Controls.Add (this.btnCancelar);
			this.Controls.Add (this.btnAceptar);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "frmSeleccionValor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Ingrese el Valor";
			this.Load += new System.EventHandler (this.frmSeleccionValor_Load);
			this.ResumeLayout (false);
			this.PerformLayout ();

		}

		#endregion

		private System.Windows.Forms.Button btnCancelar;
		private System.Windows.Forms.Button btnAceptar;
		private System.Windows.Forms.TextBox txtValor;
	}
}