//
//  AutocompletableComboBoxEntry.cs
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
using System;
using Gtk;
using System.Collections.Generic;
using Hamekoz.Interfaces;

namespace Hamekoz.UI.Gtk
{
	public delegate void ChangeIdEventHandler(int id);

	public delegate void ChangeEventHandler();

	public class AutocompletableComboBoxEntry<T>
	{
		public event ChangeIdEventHandler ChangeIdEvent;

		public event ChangeEventHandler ChangeEvent;

		public AutocompletableComboBoxEntry (ComboBoxEntry combo, IList<T> list)
		{
			combo.Entry.Activated += delegate {
				foreach (IDescriptible item in list) {
					if (String.Compare (item.Descripcion, combo.Entry.Text) == 1) {
						ComboBoxHelpers.SetByString (combo, item.Descripcion, 0);
						ChangeEvent();
						break;
					}
				}
			};

			combo.Entry.KeyPressEvent += delegate(object o, KeyPressEventArgs args) {

				if (Gdk.Key.Down == args.Event.Key) {
					TreeIter iter;
					combo.GetActiveIter (out iter);
					combo.Model.IterNext (ref iter);
					ChangeIdEvent ((int)combo.Model.GetValue (iter, 1));
				}

				if (Gdk.Key.Up == args.Event.Key) {
					TreeIter iter;
					combo.GetActiveIter (out iter);
					int position = Convert.ToInt32(combo.Model.GetStringFromIter(iter)) - 1;
					combo.Model.GetIterFromString (out iter, position.ToString());
					ChangeIdEvent ((int)combo.Model.GetValue (iter, 1));
				}

				if (Gdk.Key.Tab == args.Event.Key) {
					foreach (IDescriptible item in list) {
						if (String.Compare (item.Descripcion, combo.Entry.Text) == 1) {
							ComboBoxHelpers.SetByString (combo, item.Descripcion, 0);
							ChangeEvent();
							break;
						}
					}
				}
			};

			combo.Model.RowChanged += delegate {
				ChangeEvent();
			};
		}
	}
}

