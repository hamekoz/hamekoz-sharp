//
//  PeriodoDialog.cs
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
	/// <summary>
	/// Formulario de dialogo para seleccion de fechas
	/// </summary>
	public partial class PeriodoDialog : Form
	{
		#region Variables Internas

		#endregion

		#region Propiedades

		/// <summary>
		/// Obtiene o establece fecha inicial del rango. Por defecto fecha actual
		/// </summary>
		public DateTime FechaDesde
		{
			get
			{
				return dtpDesde.Value.Date;
			}
			set
			{
				dtpDesde.Value = value;
			}
		}

		/// <summary>
		/// Obtiene o establece fecha final del rango. Por defecto fecha actual
		/// </summary>
		public DateTime FechaHasta
		{
			get
			{
				return dtpHasta.Value.Date;
			}
			set
			{
				dtpDesde.Value = value;
			}
		}

		/// <summary>
		/// Establece si el dialogo es para un rago de fechas (por defecto) o solo para una fecha Hasta.
		/// </summary>
		public bool HabilitarRango
		{
			set
			{
				dtpDesde.Enabled = value;
			}
		}

		#endregion

		#region Metodos

		/// <summary>
		/// Crea una instancia del formulario de dialogo para seleccion de fechas
		/// </summary>
		public PeriodoDialog()
		{
			InitializeComponent();
			dtpDesde.Value = DateTime.Now.Date;
			dtpDesde.Value = DateTime.Now.Date;
		}

		void dtpDesde_ValueChanged(object sender, EventArgs e)
		{
			if (dtpDesde.Value > dtpHasta.Value)
				dtpDesde.Value = dtpHasta.Value;
		}

		void dtpHasta_ValueChanged(object sender, EventArgs e)
		{
			if (dtpHasta.Value < dtpDesde.Value)
				dtpHasta.Value = dtpDesde.Value;
		}

		#endregion

		void btnAceptar_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		void btnCancelar_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
