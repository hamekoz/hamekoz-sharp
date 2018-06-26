//
//  MovimientoDeArticulos.cs
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
	//FIX renombrar en singular
	public partial class MovimientoDeArticulo : IPersistible, IIdentifiable
	{
		public MovimientoDeArticulo ()
		{
			//HACK aca no se deben inicializar objetos
			Tipo = new TipoDeMovimiento ();
			Articulo = new Articulo ();
		}

		public int Id {
			get;
			set;
		}

		public TipoDeMovimiento Tipo {
			get;
			set;
		}

		public DateTime Fecha {
			get;
			set;
		}

		public Articulo Articulo {
			get;
			set;
		}

		public Lote Lote {
			get;
			set;
		}

		public decimal Cantidad {
			get;
			set;
		}

		public Deposito Origen {
			get;
			set;
		}

		public Deposito Destino {
			get;
			set;
		}

		public string Comentarios {
			get;
			set;
		}
	}
}
