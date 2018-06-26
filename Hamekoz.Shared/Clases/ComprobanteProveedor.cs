//
//  ComprobanteProveedor.cs
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
using Hamekoz.Fiscal;

namespace Hamekoz.Negocio
{
	//UNDONE unificar logica comun en clase abstracta Comprobante
	//UNDONE separar datos de implementacion de comprobante electronico
	public partial class ComprobanteProveedor : Comprobante, IComprobante
	{
		public ComprobanteProveedor ()
		{
			//HACK aca no debria inicializase los objetos
			Proveedor = new Proveedor ();
			Tipo = new NumeracionDeComprobante ();
			Remito = new RemitoProveedor ();
			Remitos = new List<RemitoProveedor> ();
			CondicionDePago = new CondicionDePago ();
			Observaciones = string.Empty;
		}

		public Proveedor Proveedor {
			get {
				return (Proveedor)Responsable;
			}
			set {
				Responsable = value;
			}
		}

		//TODO revisar porque no deberia estar esta propiedad ya que esta en la clase base
		public RemitoProveedor Remito {
			get {
				return (RemitoProveedor)base.Remito;
			}
			set {
				base.Remito = value;
			}
		}

		public IList<RemitoProveedor> Remitos {
			get;
			set;
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

		#region IComprobante

		IResponsable IComprobante.Responsable {
			get {
				return Proveedor;
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
	}
}
