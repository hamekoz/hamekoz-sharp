//
//  TreeViewPicker.cs
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
using Hamekoz.Core;
using System.Collections.Generic;

namespace Hamekoz.UI.Gtk
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class TreeViewPicker : Bin
	{
		TreeViewDialog treeviewDialog = new TreeViewDialog ();

		public event ChangeEventHandler ChangeEvent;

		protected virtual void OnChangeEvent ()
		{
			var handler = ChangeEvent;
			if (handler != null)
				handler ();
		}

		public int ActualId {
			get {
				return treeviewDialog.ActualId;
			}
			set {
				treeviewDialog.ActualId = value;
			}
		}

		public string ActualString {
			get {
				return treeviewDialog.ActualString;
			}
			set {
				treeviewDialog.ActualString = value;
				entry.Text = treeviewDialog.ActualString;
			}
		}

		public TreeViewPicker ()
		{
			this.Build ();

			treeviewDialog.Visible = false;

			treeviewDialog.ActivatedEvent += delegate(string descripcion, int id) {
				entry.Text = descripcion;
				OnChangeEvent ();
			};

			button.Clicked += delegate(object sender, EventArgs e) {
				int x, y;
				this.GdkWindow.GetOrigin (out x, out y);	
				x += this.Allocation.Left;
				y += this.Allocation.Top + this.Allocation.Height;
				treeviewDialog.Move (x, y);
				treeviewDialog.Modal = true;
				treeviewDialog.Show ();
			};
		}

		/// <summary>
		/// Loads the list.
		/// </summary>
		/// <param name="liststore">Liststore.</param>
		public void LoadList (ListStore liststore)
		{
			this.treeviewDialog.LoadList (liststore);
		}
	}
}

