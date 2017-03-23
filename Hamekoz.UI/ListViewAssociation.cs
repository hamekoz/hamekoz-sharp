//
//  ListViewAssociation.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2017 Hamekoz - www.hamekoz.com.ar
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
using Hamekoz.Core;
using Humanizer;
using Xwt;

//TODO repensar para reutilizar como clase base ListViewAction permitiendo
namespace Hamekoz.UI
{
	/// <summary>
	/// List box with action to Add and Remove.
	/// </summary>
	public class ListViewAssociation<T> : HBox
	{
		#region Properties

		public string Title {
			get;
			set;
		} = typeof(T).Name.Humanize ();

		public bool ActionsVisible {
			get { return actions.Visible; }
			set { actions.Visible = value; }
		}

		public bool AddVisible {
			get { return add.Visible; }
			set { add.Visible = value; }
		}

		public bool RemoveVisible {
			get { return remove.Visible; }
			set { remove.Visible = value; }
		}

		public bool Editable {
			get { return actions.Sensitive; }
			set { actions.Sensitive = value; }
		}

		public IList<T> List {
			get { return listView.List; }
			set { listView.List = value; }
		}

		public void Add (T item)
		{
			listView.Add (item);
		}

		#endregion

		readonly ListView<T> listView = new ListView<T> ();

		readonly VBox actions = new VBox ();

		readonly Button add = new Button {
			TooltipText = Application.TranslationCatalog.GetString ("Add"),
			Image = Icons.ListAdd.WithSize (IconSize.Medium),
			ImagePosition = ContentPosition.Center
		};
		readonly Button remove = new Button {
			TooltipText = Application.TranslationCatalog.GetString ("Remove"),
			Image = Icons.Delete.WithSize (IconSize.Medium),
			ImagePosition = ContentPosition.Center
		};

		ListBoxFilter<T> listViewFilter;

		public ListViewAssociation (IController<T> controller)
		{
			add.Clicked += delegate {
				var dialogo = new Dialog {
					Title = string.Format (Application.TranslationCatalog.GetString ("Add {0}"), Title),
				};

				dialogo.Buttons.Add (Command.Cancel, Command.Add);

				listViewFilter = listViewFilter ?? new ListBoxFilter<T> {
					List = controller.List,
					MinHeight = 200,
					MinWidth = 300
				};
				var box = new HBox ();
				box.PackStart (listViewFilter, true, true);
				dialogo.Content = box;
				if (dialogo.Run () == Command.Add) {
					if (List.Contains (listViewFilter.SelectedItem))
						MessageDialog.ShowWarning (Application.TranslationCatalog.GetString ("The item already exists in the list."));
					else {
						controller.Load (listViewFilter.SelectedItem);
						listView.Add (listViewFilter.SelectedItem);
						OnAddItem (listViewFilter.SelectedItem);
						OnChanged ();
					}
				}

				box.Remove (listViewFilter);
				dialogo.Close ();
			};

			remove.Clicked += delegate {
				var row = listView.SelectedRow;
				if (row == -1) {
					MessageDialog.ShowMessage (string.Format (Application.TranslationCatalog.GetString ("Select a {0} to remove"), typeof(T).Name.Humanize ()));
				} else {
					if (OnPreventRemove (listView.SelectedItem))
						MessageDialog.ShowWarning (Application.TranslationCatalog.GetString ("A validation rule prevents you from deleting the selected item"));
					else {
						listView.SelectedItem = listView.SelectedItem;
						listView.Remove ();
						OnChanged ();
					}
				}
			};

			actions.PackStart (add);
			actions.PackStart (remove);
			PackStart (listView, true, true);
			PackEnd (actions, false, true);
		}

		public delegate bool PreventRemoveHandler (T item);

		public PreventRemoveHandler PreventRemove;

		protected bool OnPreventRemove (T item)
		{
			var preventRemove = PreventRemove;
			return preventRemove != null && preventRemove (item);
		}

		public event EventHandler Changed;

		protected virtual void OnChanged ()
		{
			var handler = Changed;
			if (handler != null)
				handler (this, null);
		}

		public delegate void AddItemHandler (T item);

		public event AddItemHandler AddItem;

		protected virtual void OnAddItem (T item)
		{
			var handler = AddItem;
			if (handler != null)
				handler (item);
		}

		public void RemoveColumnAt (int index)
		{
			listView.RemoveColumnAt (index);
		}
	}
}

