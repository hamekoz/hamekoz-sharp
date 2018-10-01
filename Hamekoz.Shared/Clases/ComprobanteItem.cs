//
//  ComprobanteItem.cs
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
using Hamekoz.Core;
using Hamekoz.Fiscal;
using Hamekoz.Negocio;

namespace Hamekoz.Negocio
{
	public class ComprobanteItem : IItem, IIdentifiable, IPersistible
	{
		#region IIdentifiable implementation

		public int Id {
			get;
			set;
		}

		#endregion

		#region IItem implementation

		string codigo;

		public string Codigo {
			get {
				return Articulo != null ? Articulo.Id.ToString () : codigo;
			}
			set {
				codigo = value;
			}
		}

		string descripcion;

		public string Descripcion {
			get {
				return Articulo != null ? Articulo.Nombre : descripcion;
			}
			set {
				descripcion = value;
			}
		}

		string IItemControladorFiscal.DescripcionCorta {
			get {
				return Articulo != null ? Articulo.NombreCorto : Descripcion;
			}
		}

		public decimal Cantidad {
			get;
			set;
		} = 1;

		public decimal Precio {
			get;
			set;
		}

		public decimal Neto {
			get {
				return PrecioNeto () * Cantidad;
			}
		}

		public IVA Iva {
			get;
			set;
		} = IVA.Veintiuno;

		decimal? iva;

		public decimal ImporteIVA {
			get { 
				return iva ?? Math.Round (Neto * Iva.Alicuota () / 100, 2);
			}
			set { 
				iva = value;
			}
		}

		public decimal Impuestos {
			get;
			set;
		}

		//TODO puede ser calculado segun Precio y PrecioConIVA, TasaIVA e Impuestos
		decimal total;

		public decimal Total {
			get {
				if (Id == 0)
					total = Neto + ImporteIVA + Impuestos;
				return total;
			}
			set {
				total = value;
			}
		}

		#endregion

		public bool PrecioConIVA {
			get;
			set;
		}

		public decimal PrecioNeto ()
		{
			if (PrecioConIVA) {
				return Math.Round (Precio / (1 + Iva.Alicuota () / 100), 2);
			} else {
				return Math.Round (Precio, 2);
			}
		}

		public decimal IVAUnitario ()
		{
			if (PrecioConIVA) {
				return Math.Round (Precio - (Precio / (1 + Iva.Alicuota () / 100)), 2);
			} else {
				return Math.Round (Precio * Iva.Alicuota () / 100, 2);
			}
		}

		//TODO evaluar si es realmente necesario, se podria ordenar por el Id y no numerar
		public int Renglon;

		//TODO evaluar si es necesario que sea una propiedad o se puede manejar con una clase Stock que tiene Articulo, Lote y Cantidad
		public Articulo Articulo { 
			get; 
			set; 
		}

		//TODO evaluar si es necesario
		public Lote Lote {
			get;
			set;
		}

		//TODO evaluar si es necesario
		public IRemito Remito {
			get;
			set;
		}
	}
}

