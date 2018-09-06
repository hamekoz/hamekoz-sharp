//
//  SeleccionFechaDialog.cs
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

using System;
using System.Windows.Forms;

namespace Hamekoz.UI.WinForm
{
	public partial class SeleccionFechaDialog : Form
	{
		public SeleccionFechaDialog()
		{
			InitializeComponent();
			dateTimePicker1.Value = DateTime.Now;
		}

		/// <summary>
		/// Obtiene el la fecha seleccionada
		/// </summary>
		public DateTime Fecha
		{
			get { return dateTimePicker1.Value; }
		}

		/// <summary>
		/// Obtiene el Año y Mes seleccionado en formato numerico AAAAMM
		/// </summary>
		public int AnioMes
		{
			get { return dateTimePicker1.Value.Year * 100 + dateTimePicker1.Value.Month; }
		}

		void btnCancelar_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		void btnAceptar_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
