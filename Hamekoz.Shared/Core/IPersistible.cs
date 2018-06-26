//
//  IPersistible.cs
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

namespace Hamekoz.Core
{
	/// <summary>
	/// Mark a class as data persistible.
	/// </summary>
	public interface IPersistible
	{
	}

	public interface IPersistible<T> where T : IPersistible
	{
		/// <summary>
		/// Read the specified instance.
		/// </summary>
		/// <param name="instance">Instance.</param>
		void Read (T instance);

		/// <summary>
		/// Insert the specified instance.
		/// </summary>
		/// <param name="instance">Instance.</param>
		void Insert (T instance);

		/// <summary>
		/// Update the specified instance.
		/// </summary>
		/// <param name="instance">Instance.</param>
		void Update (T instance);

		/// <summary>
		/// Delete the specified instance.
		/// </summary>
		/// <param name="instance">Instance.</param>
		void Delete (T instance);
	}
}
