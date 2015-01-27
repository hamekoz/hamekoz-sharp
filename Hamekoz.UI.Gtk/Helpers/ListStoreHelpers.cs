//
//  ListStoreHelpers.cs
//
//  Author:
//       Emiliano Gabriel Canedo <emilianocanedo@gmail.com>
//
//  Copyright (c) 2015 ecanedo
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
using System.Collections.Generic;


namespace Hamekoz.UI.Gtk
{
	public static class ListStoreHelpers
	{
		public static bool SearchIntAtPosition (this ListStore list, int value, int position) {
			TreeIter iter;
			bool state = false;

			if (list.GetIterFirst (out iter)) {
				do {
					int current = (int)list.GetValue (iter, position);

					if (current == value) {
						state = true;
						break;
					}
				} while (list.IterNext (ref iter));
			}

			return state;
		}

		public static bool SearchIntAtPosition (this ListStore list, int value, int position, out TreeIter outIter) {
			TreeIter iter;
			bool state = false;

			if (list.GetIterFirst (out iter)) {
				do {
					int current = (int)list.GetValue (iter, position);

					if (current == value) {
						state = true;
						break;
					}
				} while (list.IterNext (ref iter));
			}

			outIter = iter;
			return state;
		}
	}
}

