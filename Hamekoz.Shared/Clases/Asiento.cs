//
//  Asiento.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//       Juan Angel Dinamarca <juan.angel.dinamarca@gmail.com>
//
//  Copyright (c) 2015 Hamekoz - www.hamekoz.com.ar
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
using System.Linq;
using Hamekoz.Core;

namespace Hamekoz.Negocio
{
	public partial class Asiento : IPersistible, IIdentifiable, IAnulable, ISearchable
	{
		public int Id {
			get;
			set;
		}

		public string Detalle {
			get;
			set;
		}

		public DateTime Fecha {
			get;
			set;
		}

		public string Comprobante {
			get;
			set;
		}

		public DateTime FechaContable {
			get;
			set;
		}

		public Moneda Moneda {
			get;
			set;
		}

		//UNDONE ver si tiene sentido tener aca la empresa
		public Empresa Empresa {
			get;
			set;
		}

		public List<AsientoItem> Items { get; set; }

		public IList<AsientoItem> ItemsSumarizados {
			get {
				var lista = from i in Items
				            group i by new { Cuenta = i.CuentaContable } into asiento
				            select new AsientoItem {
					CuentaContable = asiento.Key.Cuenta,
					Debe = asiento.Sum (a => a.Debe),
					Haber = asiento.Sum (a => a.Haber)
				};
				return lista.ToList ();
			}
		}

		public decimal Debe {
			get { return Math.Round (Items.Sum (i => i.Debe), 2); }
		}

		public decimal Haber {
			get { return Math.Round (Items.Sum (i => i.Haber), 2); }
		}

		//TODO ver si tiene realmente sentido tener almacenado el centro de costo en la cabecera del asiento ya que tambien se almacena el centro de costo en cada item
		public CentroDeCosto CentroDeCosto {
			get;
			set;
		}

		public bool Anulado { get; set; }

		public Asiento ()
		{
			Items = new List<AsientoItem> ();
		}

		public override string ToString ()
		{
			return Id.ToString ();
		}

		#region ISearchable implementation

		public string ToSearchString ()
		{
			throw new NotImplementedException ();
		}

		#endregion

		public Empleado CreadoPor {
			get;
			set;
		}

		public Empleado ModificadoPor {
			get;
			set;
		}

		public DateTime ModificadoEn {
			get;
			set;
		}
	}
}