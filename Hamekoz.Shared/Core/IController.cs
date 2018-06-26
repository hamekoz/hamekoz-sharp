//
//  IController.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
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

namespace Hamekoz.Core
{
	public interface IController<T>
	{
		T Create ();

		void Load (T instance);

		void Save (T instance);

		void Remove (T instance);

		IList<T> List { get; }

		bool Reload { get; set; }

		//TODO agregar un metodo para realizar validaciones
		//bool Validate(T instance);

		//TODO agregar un metodo para realizar acciones o validaciones antes y despues de las acciones que modifican los datos
		//void BeforeSave(T instance);
		//void AfterSave(T instance);
		//void BeforeRemove(T instance);
		//void AfterRemove(T instance);
	}
}

