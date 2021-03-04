//
//  RegistroImportacionRetencionPercepcion.cs
//
//  Author:
//       Mariano Ripa <ripamariano@gmail.com>
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2016 Hamekoz - www.hamekoz.com.ar
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

namespace Hamekoz.Argentina.Sifere
{
	public class RegistroImportacionRetencionPercepcion
	{
		/// <summary>
		/// Gets or sets the codigo de norma.
		/// </summary>
		/// <value>The codigo de norma.</value>
		/// <see href="http://www.agip.gob.ar/web/agentes-recaudacion/ag-rec-arciba-codigo-de-normas.htm"/>
		public string CodigoJurisdiccion {
			get;
			set;
		}

		public string CUIT {
			get;
			set;
		}

		public DateTime Fecha {
			get;
			set;
		}

		public string Sucursal {
			get;
			set;
		}

		public string Constancia {
			get;
			set;
		}

		public string TipoDeComprobante {
			get;
			set;
		}

		public string Letra {
			get;
			set;
		}

		public string NroComprobante {
			get;
			set;
		}


		public string Importe {
			get;
			set;
		}


		public bool EsPercepcion {
			get;
			set;
		}

		/// <summary>
		/// To the fixed string.
		/// </summary>
		/// <returns>The fixed string.</returns>
		/// <see href="http://www.agip.gov.ar/web/files/DocTecnicoImpoOperacionesDise%F1odeRegistro.pdf"/>
		public string ToFixedStringPercepcion ()
		{
			string cadena;
			cadena = string.Format ("{0:D}{1}{2:d}{3}{4}{5:D1}{6}{7}"
				, CodigoJurisdiccion
				, CUIT
				, Fecha
				, Sucursal
				, Constancia.PadLeft (8, '0')
				, TipoDeComprobante
				, Letra
				, Importe
			);
			if ((cadena.Length != 51) && (cadena.Length != 38))
				throw new Exception (string.Format ("La longitud del registro a exportar es incorrecta.\nCódigo Juridicción: {0}\nCUIT: {1}", CodigoJurisdiccion, CUIT));

			return cadena;
		}

		public string ToFixedStringRetencion ()
		{
			string cadena;
			cadena = string.Format ("{0:D}{1}{2:d}{3}{4}{5:D1}{6}{7}{8}"
				, CodigoJurisdiccion
				, CUIT
				, Fecha
				, Sucursal
				, Constancia.PadLeft (16, '0')
				, TipoDeComprobante
				, Letra
				, NroComprobante
				, Importe
			);

			if ((cadena.Length != 79) && (cadena.Length != 66))
				throw new Exception (string.Format ("La longitud del registro a exportar es incorrecta.\nCódigo Juridicción: {0}\nCUIT: {1}", CodigoJurisdiccion, CUIT));

			return cadena;
		}
	}
}