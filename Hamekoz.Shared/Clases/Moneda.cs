//
//  Moneda.cs
//
//  Author:
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
using Hamekoz.Core;

namespace Hamekoz.Negocio
{
	public partial class Moneda : IPersistible, IIdentifiable
	{
		#region Singleton

		//HACK para manejar la moneda por defecto, deberia utilizar un controlador y un parametro global de la aplicacion

		/// <summary>
		/// Instancia unica del patron sigleton
		/// </summary>
		static Moneda moneda;

		/// <summary>
		/// Obtiene una instancia unica de Moneda
		/// </summary>
		public static Moneda Default {
			get {
				if (moneda == null) {
					moneda = new Moneda {
						Id = 1,
						Codigo = "ARS",
						Nombre = "PESO",
						Simbolo = "$",
						Cotizacion = 1,
					};
				}
				return moneda;
			}
		}

		#endregion

		public int Id {
			get;
			set;
		}

		public string Codigo {
			get;
			set;
		}

		public int Numero {
			get;
			set;
		}

		public string Simbolo {
			get;
			set;
		}


		public string Nombre {
			get;
			set;
		}

		public int Decimales {
			get;
			set;
		}

		public decimal Cotizacion {
			get;
			set;
		}

		public bool Obsoleta {
			get;
			set;
		}

		public decimal CotizacionInversa ()
		{
			if (Cotizacion == 0) {
				return 0;
			} else {
				return 1 / Cotizacion;
			}
		}
	}
}