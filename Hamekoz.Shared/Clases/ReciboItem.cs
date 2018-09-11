//
//  ReciboItem.cs
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
using Hamekoz.Fiscal;

namespace Hamekoz.Negocio
{
	//TODO ver si es posible unificar con PagoItem
	public partial class ReciboItem : IPersistible, IIdentifiable, IItem
	{
		#region IIdentifiable implementation

		public int Id {
			get;
			set;
		}

		#endregion

		#region Recibo

		public int Renglon {
			get;
			set;
		}

		#endregion

		public CuentaContable CuentaContable {
			get;
			set;
		}

		public decimal Importe {
			get;
			set;
		}

		#region IItem implementation

		string IItem.Codigo {
			get {
				return CuentaContable == null ? string.Empty : CuentaContable.Codigo.ToString ();
			}
		}

		string descripcion;

		public string Descripcion {
			get {
				return CuentaContable == null ? descripcion : string.Format ("{0} {1}", CuentaContable, ChequeToString ());
			}
			set { descripcion = value; }
		}

		string IItem.DescripcionCorta {
			get {
				return CuentaContable == null ? descripcion : CuentaContable.Cuenta;
			}
		}

		decimal IItem.Cantidad {
			get {
				return 1;
			}
		}

		decimal IItem.Precio {
			get {
				return Importe;
			}
		}

		decimal IItem.Neto {
			get {
				return Importe;
			}
		}

		IVA IItem.Iva {
			get {
				return IVA.NoCorresponde;
			}
		}

		decimal IItem.ImporteIVA {
			get {
				return 0;
			}
		}

		decimal IItem.Impuestos {
			get {
				return 0;
			}
		}

		decimal IItem.Total {
			get {
				return Importe;
			}
		}

		#endregion

		#region Revisar

		public double Debitar {
			get;
			set;
		}

		public Retencion Retencion {
			get;
			set;
		}

		public int LoteDeTarjeta {
			get;
			set;
		}

		//FIX el cheque no deberia ser un atributo del detalle del recibo.
		public Cheque Cheque {
			get;
			set;
		}

		string ChequeToString ()
		{
			return Cheque == null ? string.Empty : string.Format ("Cheque: {0} Nro {1} al {2:d}", Cheque.Banco, Cheque.Numero, Cheque.Cobro);
		}

		#endregion

		public decimal CotizacionDelPeso {
			get;
			set;
		} = 1;

		public decimal CotizacionMoneda {
			get;
			set;
		} = 1;

		public bool Eliminado {
			get;
			set;
		}
	}
}
