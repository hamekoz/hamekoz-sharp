//
//  IDescriptible.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2014 Claudio Rodrigo Pereyra Diaz
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

namespace Hamekoz.Core
{
	public interface IDescriptible
	{
		/// <summary>
		/// Gets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		int Id { get; }

		/// <summary>
		/// Gets the descripcion.
		/// </summary>
		/// <value>The descripcion.</value>
		string Descripcion { get; }
	}
}

namespace Hamekoz.Interfaces
{
	/// <summary>
	/// Interfaz que debe implementar clases que puedan ser cargadas a un combo
	/// </summary>
	[Obsolete ("Use a specific property to descript the object")]
	public interface IDescriptible
	{
		/// <summary>
		/// Gets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		int Id { get; }

		/// <summary>
		/// Gets the descripcion.
		/// </summary>
		/// <value>The descripcion.</value>
		string Descripcion { get; }
	}
}

