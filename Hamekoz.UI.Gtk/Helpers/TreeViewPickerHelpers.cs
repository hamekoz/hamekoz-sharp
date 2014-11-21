//
//  TreeViewPickerHelpers.cs
//
//  Author:
//       Emiliano Gabriel Canedo <emilianocanedo@gmail.com>
//
//  Copyright (c) 2014 ecanedo
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

using Gtk;
using System;
using Hamekoz.Interfaces;
using System.Collections.Generic;

namespace Hamekoz.UI.Gtk
{
	public static class TreeViewPickerHelpers
	{
		public static void SetById<T> (this TreeViewPicker tree, int Id, IList<T> list)
		{
			foreach (IDescriptible descriptible in list) {
				if (descriptible.Id == Id) {
					tree.ActualId = descriptible.Id;
					tree.ActualString = descriptible.Descripcion;
				}
			}
		}
	}
}


