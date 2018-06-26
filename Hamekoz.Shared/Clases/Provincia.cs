//
//  Provincia.cs
//
//  Author:
//       Emiliano Gabriel Canedo <emilianocanedo@gmail.com>
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
	public partial class Provincia : IPersistible, IIdentifiable, IDescriptible
	{
		public int Id {
			get;
			set;
		}

		public string Nombre {
			get;
			set;
		}

		public Pais Pais {
			get;
			set;
		}

		public bool Inactiva {
			get;
			set;
		}

		public IList<Municipio> Municipios {
			get;
			set;
		}

		//HACK el IsoManagement las localidad depende directamente de la provincia
		public IList<Localidad> Localidades {
			get;
			set;
		}

		public override string ToString ()
		{
			return Nombre;
		}

		public string ToFullString ()
		{
			return Pais != null ? string.Format ("{0} - {1}", Pais.Nombre, Nombre) : Nombre;
		}

		string IDescriptible.Descripcion {
			get {
				return Nombre;
			}
		}
	}
}

