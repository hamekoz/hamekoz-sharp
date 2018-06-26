//
//  Banco.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2014 Hamekoz - www.hamekoz.com.ar
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
using System.Collections.Generic;
using Hamekoz.Core;

namespace Hamekoz.Negocio
{
	public partial class Banco : IPersistible, IIdentifiable, IDescriptible
	{
		public int Id {
			get;
			set;
		}

		public string Nombre {
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

		public int Clearing {
			get;
			set;
		}

		public bool Inactivo {
			get;
			set;
		}

		//HACK la cuenta contable deberia estar en una relacion entre el banco y la cuenta
		//El id dela cuenta contable no esta en la tabla banco sino que en cuentacontable se encuentra el id del banco
		//pongo la relacion aqui por comodidad, es cargada en el negocio de banco
		public CuentaContable CuentaContable {
			get;
			set;
		}

		string IDescriptible.Descripcion {
			get {
				return Nombre;
			}
		}

		public override string ToString ()
		{
			return Nombre;
		}

		public IList<SucursalDeBanco> Sucursales {
			get;
			set;
		}
	}
}
