//
//  MasterTreeView.cs
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
using System.Xml;
using System.Collections.Generic;
using Hamekoz.UI.Gtk;

namespace Hamekoz.UI.Gtk
{
	public delegate void MenuSelectionHandler (string Id);

	public class MasterTreeView
	{
		#region ATTRIBUTES_AND_CONSTRUCTORS

		public event MenuSelectionHandler OnChangeSelection;

		private TreeStore list = new TreeStore (typeof(string), typeof(string), typeof(string));

		//TREE FOR STYLE CONTROL
		private List<string> nodes = new List<string> ();
		private List<string> leafs = new List<string> ();
		private List<string> sensibilityInvertedItems = new List<string> ();

		private string ActualId;

		public void Initializer ()
		{
			ActualId = "0";
		}

		public MasterTreeView (ref TreeView tree, string xmlInputAttach)
		{
			tree.AppendColumn ("Menú Principal", new CellRendererText (), "text", 0);

			XmlDocument document = new XmlDocument ();
			document.Load (xmlInputAttach);

			if (document.FirstChild.HasChildNodes) {
				foreach (XmlNode child in document.FirstChild.ChildNodes) {
					addNode (child);
				}
			}

			TreeModelFilter filter = new TreeModelFilter (list, null);
			filter.VisibleFunc = new TreeModelFilterVisibleFunc (FilterFunc);
			tree.Model = filter;

			tree.Columns [0].SetCellDataFunc (tree.Columns [0].Cells [0], new TreeCellDataFunc (RenderFunc));
			tree.CursorChanged += HandleCursorChanged;
			tree.RowActivated += HandleRowActivated;
		}

		#endregion

		#region RECURSIVE_ADDNODE

		void addNode (XmlNode nodo)
		{
			TreeIter nuevoIter = list.AppendValues (nodo.Attributes ["nombre"].Value, nodo.Attributes ["id"].Value, nodo.Attributes ["new"].Value);

			if (nodo.HasChildNodes) {
				nodes.Add (nodo.Attributes ["id"].Value);

				if ((nodo.Attributes ["new"].Value) == "yes")
					sensibilityInvertedItems.Add (nodo.Attributes ["id"].Value);

				foreach (XmlNode child in nodo.ChildNodes) {
					addNode (child, nuevoIter);
				}
			} else {
				leafs.Add (nodo.Attributes ["id"].Value);

				if ((nodo.Attributes ["new"].Value) == "yes")
					sensibilityInvertedItems.Add (nodo.Attributes ["id"].Value);
			}
		}

		void addNode (XmlNode nodo, TreeIter iterador)
		{
			TreeIter nuevoIter = list.AppendValues (iterador, nodo.Attributes ["nombre"].Value, nodo.Attributes ["id"].Value, nodo.Attributes ["new"].Value);

			if (nodo.HasChildNodes) {
				nodes.Add (nodo.Attributes ["id"].Value);

				if ((nodo.Attributes ["new"].Value) == "yes")
					sensibilityInvertedItems.Add (nodo.Attributes ["id"].Value);

				foreach (XmlNode child in nodo.ChildNodes) {
					addNode (child, nuevoIter);
				}
			} else {
				leafs.Add (nodo.Attributes ["id"].Value);

				if ((nodo.Attributes ["new"].Value) == "yes")
					sensibilityInvertedItems.Add (nodo.Attributes ["id"].Value);
			}
		}

		#endregion

		#region METHODS

		void RenderFunc (TreeViewColumn tree_column, CellRenderer cell, TreeModel tree_model, TreeIter iter)
		{
			foreach (string node in nodes) {
				if (tree_model.GetValue (iter, 1).ToString () == node) {
					(cell as CellRendererText).Font = "bold";
					(cell as CellRendererText).Sensitive = true;
				}
			}

			foreach (string leaf in leafs) {
				if (tree_model.GetValue (iter, 1).ToString () == leaf) {
					(cell as CellRendererText).Font = "italic";
					(cell as CellRendererText).Sensitive = true;
				}
			}

			#if !DEBUG
			foreach (string item in sensibilityInvertedItems) {
			if (tree_model.GetValue (iter, 1).ToString () == item) {
			(cell as CellRendererText).Sensitive = false;
			}
			}
			#endif

		}

		bool FilterFunc (TreeModel model, TreeIter iter)
		{
			//ALL THE LIST*
			return true;
		}

		#endregion

		#region EVENTS_HANDLERS

		void HandleRowActivated (object o, RowActivatedArgs args)
		{
			TreeIter iter;
			TreeView view = (TreeView)o;

			if (view.Model.GetIter (out iter, args.Path)) {

				if (view.GetRowExpanded (args.Path)) {
					view.CollapseRow (args.Path);
				} else {
					view.ExpandRow (args.Path, false);
				}
			}
		}

		#endregion

		#region CURSOR_CHANGED_LOGIC

		//HACK Llevar a funciones semanticamente atómicas

		void HandleCursorChanged (object sender, EventArgs e)
		{
			TreeView view = (TreeView)sender;
			TreeModel model;
			TreeIter iter;

			if (view.Selection.GetSelected (out model, out iter)) {

				string ID = model.GetValue (iter, 1).ToString ();
				bool state = true;

				#if !DEBUG
				if (sensibilityInvertedItems.Contains (ID)) {
				view.Selection.UnselectAll ();
				state = false;
				}
				#endif

				if (state) {
					if (nodes.Contains (ID)) {
						view.Selection.UnselectAll ();
					} else {
						if (ActualId != ID) {
							VerifyStorableId (ID);
						}
					}
				}
			}
		}

		void VerifyStorableId (string ID)
		{
			if (!Supervisor.Instance.WorkInProgress) {
				ChangeSelection (ID);
			} else {
				DialogSave guardar = new DialogSave ();

				ResponseType result = (ResponseType)guardar.Run ();

				if (result == ResponseType.Accept) {
					Supervisor.Instance.RunSaveEvent ();
					ChangeSelection (ID);
				}

				if (result == ResponseType.Close) {
					Supervisor.Instance.WorkInProgress = false;
					ChangeSelection (ID);
				}
			}
		}

		void ChangeSelection (string ID)
		{
			ActualId = ID;
			OnChangeSelection (ID);
		}

		#endregion
	}
}