//
//  ComprobanteCliente.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//       Ezequiel Taranto <ezequiel89@gmail.com>
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
using System.Collections.Generic;
using System.Linq;
using Hamekoz.Fiscal;

namespace Hamekoz.Negocio
{
	//UNDONE separar datos de implementacion de comprobante por controlador fiscal
	public partial class ComprobanteCliente : Comprobante, IComprobante, IComprobanteElectronico
	{
		public ComprobanteCliente ()
		{
			//HACK aca no deberia inicializarse los atributos complejos
			Cliente = new Cliente ();
			Cliente.CondicionDePago = new CondicionDePago ();
			Tipo = new NumeracionDeComprobante ();
			Remito = new RemitoCliente ();
			CondicionDePago = new CondicionDePago ();
			Zeta = new Zeta ();
		}

		public Cliente Cliente {
			get {
				return (Cliente)Responsable;
			}
			set {
				Responsable = value;
			}
		}

		public string CAE {
			get;
			set;
		}

		public string VencimientoCAE {
			get;
			set;
		}

		public string NumeroAFIP {
			get;
			set;
		}

		public string ComentariosAFIP {
			get;
			set;
		}

		public Zeta Zeta {
			get;
			set;
		}

		#region IComprobante

		IResponsable IComprobante.Responsable {
			get {
				return Cliente;
			}
		}

		string IComprobante.PuntoDeVenta {
			get {
				return Tipo.Pre;
			}
		}

		IList<IItem> IComprobante.Items {
			get {
				return Remito.Items.Cast<IItem> ().ToList ();
			}
		}

		#endregion

		public override string ToString ()
		{
			return string.Format ("{0} {1} {2}", Tipo.Abreviatura, Tipo.Letra, Numero);
		}
	}
}
