//
//  Articulo.cs
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
using System.Collections.Generic;
using Hamekoz.Core;

namespace Hamekoz.Negocio
{
	public partial class Articulo : IPersistible, IIdentifiable, IDescriptible, ISearchable
	{
		public int Id {
			get;
			set;
		}

		public string Codigo {
			get;
			set;
		}

		public string Nombre {
			get;
			set;
		}

		public string NombreCorto {
			get;
			set;
		}

		public Rubro Rubro {
			get;
			set;
		}

		public Estados Estado {
			get;
			set;
		}

		public Medidas Medida {
			get;
			set;
		}

		public double StockMinimo {
			get;
			set;
		}

		public double StockMaximo {
			get;
			set;
		}

		public double CantidadPorBulto {
			get;
			set;
		} = 1;

		public decimal Precio {
			get;
			set;
		}

		//TODO reemplazar por Iva de tipo IVA
		public decimal TasaDeIVA {
			get;
			set;
		}

		public decimal Neto {
			get {
				return Math.Round (Precio - IVA - ImpuestosInternos, 2);
			}
		}

		//TODO renombrar por ImporteIVA
		public decimal IVA {
			get {
				return Math.Round (Precio - Precio / (1 + TasaDeIVA / 100), 2);
			}
		}

		public decimal ImpuestosInternos {
			get;
			set;
		}

		public IList<string> EANs {
			get;
			set;
		}

		public IList<ProveedorDeArticulo> Proveedores {
			get;
			set;
		}

		public Articulo ()
		{
			Estado = Estados.Gestion;
			Medida = Medidas.Unidad;
			TasaDeIVA = 21;
			Proveedores = new List<ProveedorDeArticulo> ();
		}

		#region IDescriptible

		string IDescriptible.Descripcion {
			get {
				return Nombre;
			}
		}

		#endregion

		public override string ToString ()
		{
			return Nombre;
		}

		#region ISearchable implementation

		public virtual string ToSearchString ()
		{
			return string.Format ("[Articulo: Id={0}, Codigo={1}, Nombre={2}, NombreCorto={3}, Rubro={4}, Estado={5}, Medida={6}, StockMinimo={7}, Precio={8}, TasaDeIVA={9}, Neto={10}, IVA={11}, ImpuestosInternos={12}]", Id, Codigo, Nombre, NombreCorto, Rubro, Estado, Medida, StockMinimo, Precio, TasaDeIVA, Neto, IVA, ImpuestosInternos);
		}

		#endregion
	}
}