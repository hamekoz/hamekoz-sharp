//
//  Medidas.cs
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
using System;
using Hamekoz.Core;

namespace Hamekoz.Negocio
{
	//TODO cambiar valor 6 por 1 y 7 por 6 en base de datos existente
	[Obsolete ("Usar clase Medida")]
	public enum Medidas
	{
		Unidad,
		Metro,
		Kilo,
		Litro,
		Metro3,
		Gramo,
		Punto,
		Metro2,
		Pulgada,
		Megabyte,
		Gigabyte,
		Minuto,
		Docena,
		Centimetro3,
	}

	public class Medida : IPersistible, IIdentifiable, IDescriptible
	{
		public int Id {
			get;
			set;
		}

		public string Descripcion {
			get;
			set;
		}

		public bool Inactiva {
			get;
			set;
		}

		public override string ToString ()
		{
			return Descripcion;
		}
	}
}