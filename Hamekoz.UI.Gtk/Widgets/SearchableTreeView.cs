//
//  SearchableTreeView.cs
//
//  Author:
//       Emiliano Gabriel Canedo <emilianocanedo@gmail.com>
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
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
using Gtk;

namespace Hamekoz.UI.Gtk
{
	public delegate void ChangeIdEventHandler (int id);
	public delegate void ChangeEventHandler ();

	[System.ComponentModel.ToolboxItem (true)]
	public sealed partial class SearchableTreeView : Bin
	{
		TreeModelFilter filter;

		public event ChangeEventHandler ChangeEvent;

		protected void OnChangeEvent ()
		{
			var handler = ChangeEvent;
			if (handler != null)
				handler ();
		}

		public event ChangeEventHandler ActivateEvent;

		protected void OnActivateEvent ()
		{
			var handler = ActivateEvent;
			if (handler != null)
				handler ();
		}


		public string ActualString {
			get;
			set;
		}


		public int ActualId {
			get;
			set;
		}

		public SearchableTreeView ()
		{
			Build ();

			var treeColumn = new TreeViewColumn ();
			var columnCell = new CellRendererText ();
			treeColumn.PackStart (columnCell, true);
			treeview.AppendColumn (treeColumn);
			treeColumn.AddAttribute (columnCell, "text", 0);

			entryBuscar.Changed += delegate {
				filter.Refilter ();
			};

			treeview.CursorChanged += delegate {
				TreeIter iter;
				treeview.Selection.GetSelected (out iter);
				ActualId = (int)treeview.Model.GetValue (iter, 1);
				ActualString = (string)treeview.Model.GetValue (iter, 0);
				OnChangeEvent ();
			};

			treeview.RowActivated += delegate {
				TreeIter iter;
				treeview.Selection.GetSelected (out iter);
				ActualId = (int)treeview.Model.GetValue (iter, 1);
				ActualString = (string)treeview.Model.GetValue (iter, 0);
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

			return entryBuscar.Text == string.Empty || GeneralHelpers.SearchCompare (entryBuscar.Text, name);

		}
	}
}

