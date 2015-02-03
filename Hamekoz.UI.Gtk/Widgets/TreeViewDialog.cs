//
//  DialogTreeView.cs
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
using Hamekoz.Interfaces;

namespace Hamekoz.UI.Gtk
{
	public delegate void DescriptibleEventHandler(string descripcion, int id);

	public partial class TreeViewDialog : Dialog
	{
		public event DescriptibleEventHandler ActivatedEvent;

		protected virtual void OnActivatedEvent (string descripcion, int id)
		{
			var handler = ActivatedEvent;
			if (handler != null)
				handler (descripcion, id);
		}

		public int ActualId {
			get {
				return searchabletreeview.ActualId;
			}
			set {
				searchabletreeview.ActualId = value;
			}
		}

		public string ActualString {
			get {
				return searchabletreeview.ActualString;
			}
			set {
				searchabletreeview.ActualString = value;
			}
		}

		public TreeViewDialog ()
		{
			Build ();

			this.searchabletreeview.ChangeEvent += delegate {
				//
			};

			this.searchabletreeview.ActivateEvent += delegate {
				OnActivatedEvent(searchabletreeview.ActualString, searchabletreeview.ActualId);
				Hide();
			};

			this.FocusOutEvent += delegate(object o, FocusOutEventArgs args) {
				OnActivatedEvent(searchabletreeview.ActualString, searchabletreeview.ActualId);
				Hide();
			};
		}

		public void LoadList (ListStore liststore)
		{
			this.searchabletreeview.LoadList (liststore);
		}
	}
}

