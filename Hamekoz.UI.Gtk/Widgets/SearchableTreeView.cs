//
//  SearchableTreeView.cs
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
using System.Collections.Generic;

namespace Hamekoz.UI.Gtk
{
	public delegate void ChangeIdEventHandler (int id);
	public delegate void ChangeEventHandler ();

	[System.ComponentModel.ToolboxItem (true)]
	public partial class SearchableTreeView : Bin
	{
		private TreeModelFilter filter;

		public event ChangeEventHandler ChangeEvent;

		protected virtual void OnChangeEvent ()
		{
			var handler = ChangeEvent;
			if (handler != null)
				handler ();
		}

		public event ChangeEventHandler ActivateEvent;

		protected virtual void OnActivateEvent ()
		{
			var handler = ActivateEvent;
			if (handler != null)
				handler ();
		}

		string actualString;

		public string ActualString {
			get {
				return actualString;
			}
			set {
				actualString = value;
			}
		}

		int actualId;

		public int ActualId {
			get {
				return actualId;
			}
			set {
				actualId = value;
			}
		}

		public SearchableTreeView ()
		{
			Build ();

			TreeViewColumn treeColumn = new TreeViewColumn ();
			CellRendererText columnCell = new CellRendererText ();
			treeColumn.PackStart (columnCell, true);
			treeview.AppendColumn (treeColumn);
			treeColumn.AddAttribute (columnCell, "text", 0);

			entryBuscar.Changed += delegate {
				filter.Refilter ();
			};

			treeview.CursorChanged += delegate(object sender, EventArgs e) {
				TreeIter iter;
				treeview.Selection.GetSelected (out iter);
				actualId = (int)treeview.Model.GetValue (iter, 1);
				actualString = (string)treeview.Model.GetValue (iter, 0);
				OnChangeEvent ();
			};

			treeview.RowActivated += delegate(object o, RowActivatedArgs args) {
				TreeIter iter;
				treeview.Selection.GetSelected (out iter);
				actualId = (int)treeview.Model.GetValue (iter, 1);
				actualString = (string)treeview.Model.GetValue (iter, 0);
				OnActivateEvent ();
			};
		}

		/// <summary>
		/// Loads the list.
		/// </summary>
		/// <param name="liststore">Liststore.</param>
		public void LoadList (ListStore liststore)
		{
			filter = new TreeModelFilter (liststore, null);
			filter.VisibleFunc = new TreeModelFilterVisibleFunc (FilterTree);
			treeview.Model = filter;
		}

		bool FilterTree (TreeModel model, TreeIter iter)
		{
			string name = model.GetValue (iter, 0).ToString ();

			if (entryBuscar.Text == string.Empty)
				return true;

			return GeneralHelpers.SearchCompare (entryBuscar.Text, name);
		}
	}
}

