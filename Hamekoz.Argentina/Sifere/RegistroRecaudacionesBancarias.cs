//
//  RegistroRecaudacionesBancarias.cs
//
//  Author:
//       Mariano Ripa <ripamariano@gmail.com>
//
//  Copyright (c) 2016 Hamekoz
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
	public class RegistroRecaudacionesBancarias
	{
		public string CodigoJurisdiccion {
			get;
			set;
		}

		public string Periodo {
			get;
			set;
		}

	
		public string TipoCuenta {
			get;
			set;
		}

		public string TipoMoneda {
			get;
			set;
		}

		public string CUIT {
			get;
			set;
		}

		public string CBU {
			get;
			set;
		}



		public string Importe {
			get;
			set;
		}



		/// <summary>
		/// Tos the fixed string.
		/// </summary>
		/// <returns>The fixed string.</returns>
		/// <see href="http://www.agip.gov.ar/web/files/DocTecnicoImpoOperacionesDise%F1odeRegistro.pdf"/>
		public string ToFixedString ()
		{
			string cadena = string.Format ("{0:D}{1}{2}{3}{4}{5}{6}"
				, CodigoJurisdiccion
				, CUIT
				, Periodo
				, CBU
				, TipoCuenta
				, TipoMoneda
				, Importe
			                );
			/*if (cadena.Length != 51) {
				throw new Exception (string.Format ("La longitud del registro a exportar es incorrecta."));
			}*/

			return cadena;
		}


	}
}

