//
//  Cotizacion.cs
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
using System;
using Hamekoz.Core;

namespace Hamekoz.Negocio
{
	public partial class Cotizacion : IPersistible, IIdentifiable
	{
		#region Singleton

		//HACK para manejar la moneda por defecto, deberia utilizar un controlador y un parametro global de la aplicacion

		/// <summary>
		/// Instancia unica del patron sigleton
		/// </summary>
		static Cotizacion cotizacion;

		/// <summary>
		/// Obtiene una instancia unica de Moneda
		/// </summary>
		public static Cotizacion Default {
			get {
				if (cotizacion == null) {
					cotizacion = new Cotizacion {
						Id = 0,
						Fecha = new DateTime (1992, 1, 1),
						Moneda = Moneda.Default,
						Valor = 1,
					};
				}
				return cotizacion;
			}
		}

		#endregion

		public int Id {
			get;
			set;
		}

		public Moneda Moneda {
			get;
			set;
		}

		public DateTime Fecha {
			get;
			set;
		}

		public decimal Valor {
			get;
			set;
		}

		public override string ToString ()
		{
			return string.Format ("{0}: 1 = {1:C}", Moneda.Nombre, Valor);
		}
	}
}

