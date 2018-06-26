//
//  Domicilio.cs
//
//  Author:
//       Ezequiel Taranto <ezequiel89@gmail.com>
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//       Mariano Adrian Ripa <ripamariano@gmail.com>
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
using Hamekoz.Core;

namespace Hamekoz.Negocio
{
	public partial class Domicilio : IPersistible, IIdentifiable
	{
		public int Id {
			get;
			set;
		}

		public string Direccion {
			get;
			set;
		} = string.Empty;

		public string CodigoPostal {
			get;
			set;
		} = string.Empty;

		public Localidad Localidad {
			get;
			set;
		}

		//TODO Referenciar desde Localidad.Municipio
		public Municipio Municipio {
			get;
			set;
		}

		//TODO Referenciar desde Localidad.Municipio.Provincia
		public Provincia Provincia {
			get;
			set;
		}

		//TODO Referenciar desde Localidad.Municipio.Provincia.Pais
		public Pais Pais {
			get;
			set;
		}

		public bool Inactivo { 
			get; 
			set;
		}

		public override string ToString ()
		{
			var build = new System.Text.StringBuilder ();
			build.Append (Direccion);
			if (CodigoPostal != string.Empty) {
				build.Append (" - ");
				build.Append (" CP: ");
				build.Append (CodigoPostal);
			}
            if (Localidad != null)
            {


                if (Localidad?.Nombre != string.Empty)
                {
                    build.Append(" - ");
                    build.Append(Localidad.Nombre);
                }
                //			if (Localidad?.Municipio?.Nombre != string.Empty) {
                //				build.Append (" - ");
                //				build.Append (Localidad?.Municipio?.Nombre);
                //			}
                if (Localidad.Municipio != null)
                {
                    if (Localidad?.Municipio?.Provincia?.Nombre != string.Empty)
                    {
                        build.Append(" - ");
                        build.Append(Localidad?.Municipio?.Provincia?.Nombre);
                    }
                    if (Localidad?.Municipio?.Provincia?.Pais?.Nombre != string.Empty)
                    {
                        build.Append(" - ");
                        build.Append(Localidad?.Municipio?.Provincia?.Pais?.Nombre);
                    }
                }
                else
                {
                    if (Localidad?.Provincia?.Nombre != string.Empty)
                    {
                        build.Append(" - ");
                        build.Append(Localidad?.Municipio?.Provincia?.Nombre);
                    }
                    if (Localidad?.Provincia?.Pais?.Nombre != string.Empty)
                    {
                        build.Append(" - ");
                        build.Append(Localidad?.Municipio?.Provincia?.Pais?.Nombre);
                    }
                }
            }

			return build.ToString ();
		}
	}
}

