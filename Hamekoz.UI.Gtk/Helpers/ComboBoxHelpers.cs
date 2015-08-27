//
//  ComboBoxHelpers.cs
//
//  Author:
//       Emiliano Gabriel Canedo <emilianocanedo@gmail.com>
//
//  Copyright (c) 2014 Hamekoz
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
using System.Collections.Generic;
using Gtk;
using Hamekoz.Core;

namespace Hamekoz.UI.Gtk
{
	public static class ComboBoxHelpers
	{
		public static void SetByString (this ComboBox combo, string text, int position)
		{
			int iterator = 0;
			TreeIter iter;
			bool state = true;
			while (state) {
				combo.Active = iterator;
				combo.GetActiveIter (out iter);
				if ((string)combo.Model.GetValue (iter, position) == text) {
					combo.SetActiveIter (iter);
					state = false;
				}
				iterator++;
			}
		}

		public static void SetById (this ComboBox combo, int id, int position)
		{
			int iterator = 0;
			TreeIter iter;
			bool state = true;
			while (state) {
				combo.Active = iterator;
				combo.GetActiveIter (out iter);
				if ((int)combo.Model.GetValue (iter, position) == id) {
					combo.SetActiveIter (iter);
					state = false;
				}
				iterator++;
			}
		}

		public static int GetCurrentId (this ComboBox combo, int position)
		{
			TreeIter iter;
			combo.GetActiveIter (out iter);
			return (int)combo.Model.GetValue (iter, position);
		}

		public static string GetCurrentString (this ComboBox combo, int position)
		{
			TreeIter iter;
			combo.GetActiveIter (out iter);
			return (string)combo.Model.GetValue (iter, position);
		}

		public static void SetByFilter<T> (this ComboBox combo, string filter, IList<T> list)
		{
			foreach (IDescriptible descriptible in list) {
				if (GeneralHelpers.SearchCompare (filter, descriptible.Descripcion)) {
					combo.SetById (descriptible.Id, 0);
					break;
				}
			}
		}

		public static void LoadByList<T> (this ComboBox combo, IList<T> list)
		{
			var listStore = new ListStore (typeof(String), typeof(int));
			foreach (IDescriptible descriptible in list) {
				listStore.AppendValues (descriptible.Descripcion, descriptible.Id);
			}
			combo.Model = listStore;
		}

		public static void LoadByFilter<T> (this ComboBox combo, string filter, IList<T> list)
		{
			bool checker = false;
			var listStore = new ListStore (typeof(String), typeof(int));
			foreach (IDescriptible descriptible in list) {
				if (GeneralHelpers.SearchCompare (filter, descriptible.Descripcion)) {
					listStore.AppendValues (descriptible.Descripcion, descriptible.Id);
					checker = true;
				}
			}

			if (checker) {
				combo.Model = listStore;
			} else {
				listStore.AppendValues ("no hay coincidencias", 0);
				combo.Model = listStore;

			}
		}

		public static void LoadByEntry<T> (this ComboBoxEntry combo, IList<T> list)
		{
			string filter = combo.Entry.Text;

			if (filter == "") {
				combo.LoadByList (list);
				combo.QueueDraw ();
			} else {
				combo.LoadByFilter (filter, list);
				combo.QueueDraw ();
			}
		}

		public static void GetComboEntryId<T> (this ComboBoxEntry combo, IList<T> list, out int ID)
		{
			ID = new int ();
			foreach (IDescriptible item in list) {
				if (GeneralHelpers.SearchCompare (combo.ActiveText, item.Descripcion)) {
					ID = item.Id;
					break;
				} else {
					ID = 0;
				}
			}
		}
	}
}

