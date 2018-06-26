//
//  Empresa.cs
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
	public partial class Empresa : IPersistible, IIdentifiable
	{
		public int Id {
			get;
			set;
		}

		public string RazonSocial {
			get;
			set;
		}

		public string CUIT {
			get;
			set;
		}

		public string NumeroDeIngresosBrutos {
			get;
			set;
		}

		public bool AgenteDeRecaudacionARBA {
			get;
			set;
		}

		public string NumeroDeAgenteARBA {
			get;
			set;
		}

		public string TipoDeAgenteARBA {
			get;
			set;
		}

		public bool AgenteDeRecaudacionAGIP {
			get;
			set;
		}

		public object NumeroDeAgenteAGIP {
			get;
			set;
		}

		public object TipoDeAgenteAGIP {
			get;
			set;
		}

		public string Actividad {
			get;
			set;
		}

		//TODO esto deberia ser una clase de tipo de domicilio pero temporalmente lo defino como string
		public string Domicilio {
			get;
			set;
		}

		//TODO esto deberia ser una clase de tipo de domicilio pero temporalmente lo defino como string
		public string DomicilioLegal {
			get;
			set;
		}

		public string Telefonos {
			get;
			set;
		}

		public string Web {
			get;
			set;
		}

		public string DomicilioConTelefonos ()
		{
			return string.Format ("{0} - {1}", Domicilio, Telefonos);
		}

	}
}