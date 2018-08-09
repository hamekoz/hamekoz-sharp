//
//  RegistroImportacionRetencionPercepcion.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2018 Hamekoz - www.hamekoz.com.ar
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
using System;
using Hamekoz.Extensions;

namespace Hamekoz.Argentina.Arba
{
	public class RegistroImportacionRetencionPercepcion
	{
		public enum TipoDeOperacion
		{
			/// <summary>
			/// Alta
			/// </summary>
			A,
			/// <summary>
			/// Baja
			/// </summary>
			B,
			/// <summary>
			/// Modificacion
			/// </summary>
			M,
		}

		public static string FileNamePercepciones (string cuit, int actividad, DateTime fecha)
		{
			return string.Format ("AR-{0}-{1:yyyyMM}0-{2}-PercepcionesClientes{3:yyyyMMdd}.txt"
				, cuit.Limpiar ()
				, fecha
				, actividad
				, DateTime.Now);
		}

		public static string FileNameRetenciones (string cuit, int actividad, DateTime fecha)
		{
			//1 -> Primera quincena
			//2 -> Segunda quincena
			int periodo = fecha.Day < 14 ? 1 : 2;

			return string.Format ("AR-{0}-{1:yyyyMM}{2}-{3}-RetencionesProveedores{4:yyyyMMdd}.txt"
				, cuit.Limpiar ()
				, fecha
				, periodo
				, actividad
				, DateTime.Now);
		}

		public string CUIT {
			get;
			set;
		}

		public DateTime Fecha {
			get;
			set;
		}

		public string TipoDeComprobante {
			get;
			set;
		}

		public string LetraDelComprobante {
			get;
			set;
		}

		public string Sucursal {
			get;
			set;
		}

		public string NroDeComprobante {
			get;
			set;
		}

		public decimal MontoImponible {
			get;
			set;
		}

		public decimal Importe {
			get;
			set;
		}

		/// <summary>
		/// Tos the fixed string.
		/// </summary>
		/// <returns>The fixed string.</returns>
		/// <see href=""/>
		public string ToFixedStringPercepcion ()
		{
			//UNDONE
			string montoImponible = MontoImponible.ToString ("F2").PadLeft (12, '0');
			string importe = Importe.ToString ("F2").PadLeft (11, '0');
			if (TipoDeComprobante == "C") {
				montoImponible = "-" + montoImponible.Substring (1);
				importe = "-" + importe.Substring (1);
			}
			string cadena = string.Format ("{0}{1:d}{2}{3}{4}{5}{6}{7}A"
				, CUIT
				, Fecha
				, TipoDeComprobante == "N" ? "D" : TipoDeComprobante
				, LetraDelComprobante
				, Sucursal.PadLeft (4, '0')
				, NroDeComprobante.PadLeft (8, '0')
				, montoImponible
				, importe);

			return cadena;
		}

		/// <summary>
		/// Tos the fixed string.
		/// </summary>
		/// <returns>The fixed string.</returns>
		/// <see href=""/>
		public string ToFixedStringRetencion ()
		{
			//UNDONE
			string cadena = string.Format ("{0}{1:d}{2}{3}{4}A"
				, CUIT
				, Fecha
				, Sucursal.PadLeft (4, '0')
				, NroDeComprobante.PadLeft (8, '0')
				, Importe.ToString ("F2").PadLeft (11, '0'));

			return cadena;
		}
	}
}

