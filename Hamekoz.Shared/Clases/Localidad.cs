//
//  Localidad.cs
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
using System.Text;
using Hamekoz.Core;

namespace Hamekoz.Negocio
{
	//UNDONE completar clase Localidad
	public partial class Localidad : IPersistible, IIdentifiable, IDescriptible
	{
		public int Id {
			get;
			set;
		}

		public string Nombre {
			get;
			set;
		}

		//HACK el IsoManagement las localidad depende directamente de la provincia
		public Provincia Provincia {
			get;
			set;
		}

		public Municipio Municipio {
			get;
			set;
		}

		public bool Inactiva {
			get;
			set;
		}

		public override string ToString ()
		{
			return Nombre;
		}

		public string ToFullString ()
		{
			//TODO reformular cuando se corrija la logica de pertenencia
			var builder = new StringBuilder ();
			if (Provincia != null) {
				builder.Append (Provincia);
				builder.Append (" - ");
			}
			if (Municipio != null && !string.IsNullOrWhiteSpace (Municipio.Nombre)) {
				builder.Append (Municipio.Nombre);
				builder.Append (" - ");
			}
			builder.Append (Nombre);
			return builder.ToString ();
		}

		string IDescriptible.Descripcion {
			get {
				return Nombre;
			}
		}
	}
}

