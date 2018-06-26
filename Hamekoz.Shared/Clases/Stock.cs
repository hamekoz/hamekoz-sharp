//
//  Stock.cs
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
	//UNDONE revisar el sentido de esta clase
	public partial  class Stock : ISearchable
	{
		public string Rubro {
			get {
				return Articulo.Rubro.Descripcion;
			}
		}

		public Articulo Articulo { get; set; }

		public string Medida {
			get {
				return Articulo.Medida.ToString ();
			}
		}

		public decimal Cantidad { get; set; }

		public override string ToString ()
		{
			return string.Format ("Id = {0}, Codigo: {1}, Articulo: {2}, Rubro: {3}, Medida: {4}"
				, Articulo.Id
				, Articulo.Codigo
				, Articulo.Nombre
				, Rubro, Medida
			);
		}

		#region ISearchable implementation

		public string ToSearchString ()
		{
			return string.Format ("[Stock: Rubro={0}, Articulo={1}, Medida={2}, Cantidad={3}]", Rubro, Articulo, Medida, Cantidad);
		}

		#endregion
	}
}
