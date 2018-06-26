//
//  CuentaBancaria.cs
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
using System.Collections.Generic;
using Hamekoz.Core;

namespace Hamekoz.Negocio
{
	public partial class CuentaBancaria : IIdentifiable, IPersistible
	{
		#region IIdentifiable implementation

		public int Id {
			get;
			set;
		}

		#endregion

		public Banco Banco {
			get;
			set;
		}

		//TODO refactorizar para reemplazar Banco y Sucursal por SucursalDeBanco
		public string Sucursal {
			get;
			set;
		}

		public string Numero {
			get;
			set;
		}

		public string CBU {
			get;
			set;
		}

		public string AliasCBU {
			get;
			set;
		}

		public CuentaContable CuentaContable {
			get;
			set;
		}

		public bool Inactiva {
			get;
			set;
		}

		public IList<CuentaBancariaSaldo> Saldos {
			get;
			set;
		}

		public override string ToString ()
		{
			return AliasCBU;
		}
	}

	public class CuentaBancariaSaldo : IIdentifiable, IPersistible
	{
		#region IIdentifiable implementation

		public int Id {
			get;
			set;
		}

		#endregion

		public DateTime Fecha {
			get;
			set;
		} = DateTime.Now;

		public decimal Saldo {
			get;
			set;
		}

		public decimal AcreditacionesPendientes {
			get;
			set;
		}

		public decimal DeduccionesPendientes {
			get;
			set;
		}
	}
}

