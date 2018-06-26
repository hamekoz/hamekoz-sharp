//
//  NumeracionDeComprobante.cs
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
	//UNDONE refactorizar para separar el tipo del comprobante de los puntos de venta
	public partial class NumeracionDeComprobante : IPersistible, IIdentifiable, IDescriptible
	{
		public int Id {
			get;
			set;
		}

		public string Descripcion {
			get;
			set;
		}

		public string Abreviatura {
			get;
			set;
		}

		public string Letra {
			get;
			set;
		}

		public int UltimoNumero {
			get;
			set;
		}

		//TODO evaluar pasar a que sea un valor int
		public string Pre {
			get;
			set;
		}

		//UNDONE reivar si tiene sentido almacenar la sucursal en el tipo de comprobante
		public Sucursal Sucursal {
			get;
			set;
		}

		public int IdEmpresa {
			get;
			set;
		}

		public TipoDeControladorFiscal Tipo {
			get;
			set;
		}

		public bool Inactivo {
			get;
			set;
		}

		//FIX esto deberia ser una funcion en lugar de una propiedad
		public string UltimoNumeroConFormato {
			get {
				if (Pre != null)
					return Pre.PadLeft (4, '0') + "-" + UltimoNumero.ToString ().PadLeft (8, '0');
				else
					return string.Empty;
			}
		}

		public override string ToString ()
		{
			return string.Format ("{0} {1}", Abreviatura, Letra);
		}
	}
}
