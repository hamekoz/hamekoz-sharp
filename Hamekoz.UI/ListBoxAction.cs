﻿//
//  ListBoxAction.cs
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
using System.Linq;
using Hamekoz.Core;
using Humanizer;
using Xwt;

namespace Hamekoz.UI
{
	/// <summary>
	/// List box with action to Add and Remove.
	/// </summary>
	public class ListBoxAction<T> : HBox
	{
		#region Properties

		public string Title {
			get;
			set;
		}

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
			get { return listBox.List; }
			set { listBox.List = value; }
		}

		public IList<T> ListAvailable {
			get;
			set;
		}

		#endregion

		readonly ListBox<T> listBox = new ListBox<T> ();

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

		public ListBoxAction (IController<T> controller = null)
		{
			Title = TypeName ();

			add.Clicked += delegate {
				var dialogo = new Dialog {
					Title = string.Format (Application.TranslationCatalog.GetString ("Add {0}"), Title),
				};

				dialogo.Buttons.Add (Command.Cancel, Command.Add);

				var w = new ListBoxFilter<T> {
					List = ListAvailable.Except (List).ToList () ?? controller.List.Except (List).ToList (),
					MinHeight = 200,
					MinWidth = 300,
					Label = TypeName (),
				};
				var box = new HBox ();
				box.PackStart (w, true, true);
				dialogo.Content = box;
				if (dialogo.Run () == Command.Add) {
					if (w.SelectedItems.Count == 0) {
						MessageDialog.ShowWarning (Application.TranslationCatalog.GetString ("Must select a item."));
					} else if (List.Contains (w.SelectedItem)) {
						MessageDialog.ShowWarning (Application.TranslationCatalog.GetString ("The item already exists in the list."));
					} else {
						OnAddItem (w.SelectedItem);
						listBox.List.Add (w.SelectedItem);
						listBox.Items.Add (w.SelectedItem);
						OnChanged ();
					}
				}

				box.Remove (w);
				dialogo.Close ();
			};

			remove.Clicked += delegate {
				var row = listBox.SelectedRow;
				if (row == -1) {
					MessageDialog.ShowMessage (string.Format (Application.TranslationCatalog.GetString ("Select a {0} to remove"), TypeName ()));
				} else {
					if (OnPreventRemove (listBox.SelectedItem))
						MessageDialog.ShowWarning (Application.TranslationCatalog.GetString ("A validation rule prevents you from deleting the selected item"));
					else {
						Add (listBox.SelectedItem);
					}
				}
			};

			actions.PackStart (add);
			actions.PackStart (remove);
			PackStart (listBox, true, true);
			PackEnd (actions, false, true);
		}

		public void Add (T item)
		{
			OnAddItem (item);
			listBox.List.Add (item);
			listBox.Items.Add (item);
			OnChanged ();
		}

		string TypeName ()
		{
			var type = typeof(T);
			return type.IsInterface ? type.Name.Remove (0, 1).Humanize () : type.Name.Humanize ();
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

		public delegate void RemoveItemHandler (T item);

		public event RemoveItemHandler RemoveItem;

		protected virtual void OnRemoveItem (T item)
		{
			var handler = RemoveItem;
			if (handler != null)
				handler (item);
		}

		public void Reset ()
		{
			List = new List<T> ();
		}

		public void ResetAll ()
		{
			List = new List<T> ();
			ListAvailable = new List<T> ();
		}
	}
}

