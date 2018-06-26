//
//  Comprobante.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2017 Hamekoz - www.hamekoz.com.ar
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
	public partial class ImpuestoItem : IPersistible, IIdentifiable
	{
		#region IIdentifiable implementation

		public int Id {
			get;
			set;
		}

		#endregion

		public Impuesto Impuesto {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the base imponible.
		/// </summary>
		/// <value>The base imponible.</value>
		public decimal BaseImponible {
			get;
			set;
		}

		public decimal Alicuota {
			get;
			set;
		}

		public decimal ValorFijo {
			get;
			set;
		}

		//TODO calcular la propiedad Importe = BaseImponible * Alicuota / 100 + ValorFijo
		decimal importe;

		public decimal Importe {
			get {
				return Math.Round (importe, 2);
			}
			set {
				importe = value;
			}
		}

		/// <summary>
		/// Utilizado para indicar cuando el impuesto fue declaro ante algun organismo gubertamental de recaudacion
		/// </summary>
		/// <value><c>true</c> if declarado; otherwise, <c>false</c>.</value>
		public bool Declarado {
			get;
			set;
		}

		public string AlicuotaToString ()
		{
			return (Alicuota / 100).ToString ("P");
		}
	}

}

