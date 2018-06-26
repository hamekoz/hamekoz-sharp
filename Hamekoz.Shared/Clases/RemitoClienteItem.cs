//
//  RemitoClienteItem.cs
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
using Hamekoz.Fiscal;

namespace Hamekoz.Negocio
{
	[Obsolete ("Reemplazar su uso por RemitoItem")]
	public partial class RemitoClienteItem : RemitoItem, IItem, IRemitoItem
	{
		//UNDONE Los valores deben calcularse solo si no esta grabado el remito, en caso contrario son lectura directa
		double porcentaje;

		[Obsolete ("Definir el precio con la variacion del porcentaje para que se realicen los demas calculos")]
		public void SetPorcentaje (double porcentaje)
		{
			this.porcentaje = porcentaje;
		}

		public IRemito Remito {
			get;
			set;
		}

		public new decimal Total {
			get {
				if (Id == 0)
					base.Total = Math.Round (Precio * Cantidad, 2);	
				return base.Total;
			}
		}

		public new decimal Neto {
			get {
				return Math.Round (Articulo.Neto * Cantidad * (1 + (decimal)porcentaje / 100), 2);
			}
		}

		public new decimal Impuestos {
			get {
				if (Id == 0)
					base.Impuestos = Math.Round (Articulo.ImpuestosInternos * Cantidad * (1 + (decimal)porcentaje / 100), 2);
				return base.Impuestos;
			}
		}

		public new decimal IVA {
			get {
				if (Id == 0)
					base.IVA = Math.Round (Articulo.IVA * Cantidad * (1 + (decimal)porcentaje / 100), 2);
				return base.IVA;
			}
		}

		public new decimal TasaIVA {
			get {
				return Articulo.TasaDeIVA;
			}
		}

		#region IItem

		string IItem.Codigo {
			get {
				return Articulo.Codigo;
			}
		}

		string IItem.Descripcion {
			get {
				return Articulo.Nombre;
			}
		}

		string IItem.DescripcionCorta {
			get {
				return Articulo.NombreCorto;
			}
		}

		#endregion
	}
}
