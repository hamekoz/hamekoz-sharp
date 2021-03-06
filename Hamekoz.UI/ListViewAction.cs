﻿//
//  ListViewAction.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2016 Hamekoz - www.hamekoz.com.ar
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
using Humanizer;
using Xwt;

namespace Hamekoz.UI
{
	/// <summary>
	/// List view with action to Add, Modify, Remove.
	/// </summary>
	public class ListViewAction<T> : HBox where T : new()
	{
		//TODO permitir al ListViewAction recibir tanto el tipo como una interfaz para su presentacion

		#region Properties

		public string Title {
			get;
			set;
		} = typeof(T).Name.Humanize ();

		public bool ShowOnRowActivated {
			get;
			set;
		} = true;

		public bool ActionsVisible {
			get { return actions.Visible; }
			set { actions.Visible = value; }
		}

		public bool AddVisible {
			get {
				return add.Visible;
			}
			set {
				add.Visible = value;
			}
		}

		public bool EditVisible {
			get {
				return edit.Visible;
			}
			set {
				edit.Visible = value;
			}
		}

		public bool RemoveVisible {
			get {
				return remove.Visible;
			}
			set {
				remove.Visible = value;
			}
		}

		public bool Editable {
			get {
				return actions.Sensitive;
			}
			set {
				actions.Sensitive = value;
			}
		}

		public bool AddSensitive {
			get {
				return add.Sensitive;
			}
			set {
				add.Sensitive = value;
			}
		}

		public IList<T> List {
			get {
				return listView.List;
			}
			set {
				listView.List = value;
			}
		}

		public IItemUI<T> ItemUI {
			get;
			set;
		}

		public void Add (T item)
		{
			listView.Add (item);
		}

		public T SelectedItem {
			get { return listView.SelectedItem; }
			set { listView.SelectedItem = value; }
		}

		public void Refresh ()
		{
			listView.Refresh ();
			OnChanged ();
		}

		public void ScrollTo (T item)
		{
			listView.ScrollTo (item);
		}

		public void ScrollTo (int row)
		{
			listView.ScrollToRow (row);
		}

		#endregion

		public void AddAction (Button action)
		{
			actions.PackStart (action);
		}

		readonly ListView<T> listView = new ListView<T> ();

		readonly VBox actions = new VBox ();

		readonly Button add = new Button {
			TooltipText = Application.TranslationCatalog.GetString ("Add"),
			Image = Icons.ListAdd.WithSize (IconSize.Medium),
			ImagePosition = ContentPosition.Center
		};
		readonly Button edit = new Button {
			TooltipText = Application.TranslationCatalog.GetString ("Edit"),
			Image = Icons.AccessoriesTextEditor.WithSize (IconSize.Medium),
			ImagePosition = ContentPosition.Center
		};
		readonly Button remove = new Button {
			TooltipText = Application.TranslationCatalog.GetString ("Remove"),
			Image = Icons.ListRemove.WithSize (IconSize.Medium),
			ImagePosition = ContentPosition.Center
		};

		public ListViewAction ()
		{
			add.Clicked += delegate {
				var dialogo = new Dialog {
					Title = string.Format (Application.TranslationCatalog.GetString ("New {0}"), Title),
				};

				dialogo.Buttons.Add (Command.Cancel, Command.Add);

				var item = new T ();
				ItemUI.ValuesClean ();
				ItemUI.Item = item;
				OnBeforeAdd (item);
				ItemUI.ValuesRefresh ();
				ItemUI.Editable (true);
				var w = (Widget)ItemUI;
				var box = new HBox ();
				box.PackStart (w, true, true);
				dialogo.Content = box;
				if (dialogo.Run () == Command.Add) {
					ItemUI.ValuesTake ();
					if (OnPreventAdd (ItemUI.Item))
						MessageDialog.ShowWarning (Application.TranslationCatalog.GetString ("A validation rule prevents you from add the item"));
					else if (OnSimilarity (List, ItemUI.Item))
						MessageDialog.ShowWarning (Application.TranslationCatalog.GetString ("A similar element already exists in the list. Try to modify the existing one"));
					else {
						listView.Add (item);
						listView.SelectedItem = item;
						ScrollTo (item);
						OnItemEdited (ItemUI.Item);
						OnChanged ();
					}
				}

				box.Remove (w);
				dialogo.Close ();
			};

			edit.Clicked += delegate {
				var dialogo = new Dialog {
					Title = string.Format (Application.TranslationCatalog.GetString ("Edit {0}"), Title),
				};

				dialogo.Buttons.Add (Command.Cancel, Command.Apply);

				var row = listView.SelectedRow;
				if (row == -1) {
					MessageDialog.ShowMessage (string.Format (Application.TranslationCatalog.GetString ("Select a {0} to edit"), Title));
				} else {
					ItemUI.ValuesClean ();
					ItemUI.Item = listView.SelectedItem;
					ItemUI.ValuesRefresh ();
					ItemUI.Editable (true);
					var widget = (Widget)ItemUI;
					var box = new HBox ();
					box.PackStart (widget, true, true);
					dialogo.Content = box;
					if (dialogo.Run () == Command.Apply) {
						ItemUI.ValuesTake ();
						if (OnSimilarity (List, ItemUI.Item))
							MessageDialog.ShowWarning (Application.TranslationCatalog.GetString ("A similar element already exists in the list. Try to modify the existing one"));
						else {
							listView.FillRow (row, listView.SelectedItem);
							OnItemEdited (ItemUI.Item);
							OnChanged ();
						}
					}
					box.Remove (widget);
					dialogo.Close ();
				}
			};

			remove.Clicked += delegate {
				var row = listView.SelectedRow;
				if (row == -1) {
					MessageDialog.ShowMessage (string.Format (Application.TranslationCatalog.GetString ("Select a {0} to remove"), Title));
				} else {
					if (OnPreventRemove (listView.SelectedItem))
						MessageDialog.ShowWarning (Application.TranslationCatalog.GetString ("A validation rule prevents you from deleting the selected item"));
					else {
						listView.Remove ();
						OnChanged ();
					}
				}
			};

			listView.RowActivated += delegate(object sender, ListViewRowEventArgs e) {
				OnRowActivated ();
				if (ShowOnRowActivated) {
					var dialogo = new Dialog {
						Title = Title,
					};

					if (e.RowIndex == -1) {
						MessageDialog.ShowMessage (string.Format (Application.TranslationCatalog.GetString ("Select a {0} to show"), Title));
					} else {
						ItemUI.ValuesClean ();
						ItemUI.Item = listView.SelectedItem;
						ItemUI.ValuesRefresh ();
						ItemUI.Editable (false);
						var widget = (Widget)ItemUI;
						var box = new HBox ();
						box.PackStart (widget, true, true);
						dialogo.Content = box;
						dialogo.Run ();
						box.Remove (widget);
						dialogo.Close ();
					}
				}
			};

			actions.PackStart (add);
			actions.PackStart (edit);
			actions.PackStart (remove);
			PackStart (listView, true, true);
			PackEnd (actions, false, true);
		}

		public delegate bool SimilarityHandler (IList<T> list, T item);

		public SimilarityHandler Similarity;

		protected bool OnSimilarity (IList<T> list, T item)
		{
			var similarity = Similarity;
			return similarity != null && similarity (list, item);
		}

		public delegate bool ValidationItemHandler (T item);

		public ValidationItemHandler PreventRemove;

		protected bool OnPreventRemove (T item)
		{
			var preventRemove = PreventRemove;
			return preventRemove != null && preventRemove (item);
		}

		public ValidationItemHandler PreventAdd;

		protected bool OnPreventAdd (T item)
		{
			var preventAdd = PreventAdd;
			return preventAdd != null && preventAdd (item);
		}

		public void RemoveColumnAt (int index)
		{
			listView.RemoveColumnAt (index);
		}

		public void RemoveColumnAt (params int[] index)
		{
			listView.RemoveColumnAt (index);
		}

		public event EventHandler Changed;

		protected virtual void OnChanged ()
		{
			var handler = Changed;
			if (handler != null)
				handler (this, null);
		}

		public event EventHandler RowActivated;

		protected virtual void OnRowActivated ()
		{
			var handler = RowActivated;
			if (handler != null)
				handler (this, null);
		}

		public delegate void BeforeAddHandler (T item);

		public event BeforeAddHandler BeforeAdd;

		void OnBeforeAdd (T item)
		{
			var handler = BeforeAdd;
			if (handler != null)
				handler (item);
		}

		public delegate void OnItemEditedHandler (T item);

		public event OnItemEditedHandler ItemEdited;

		void OnItemEdited (T item)
		{
			var handler = ItemEdited;
			if (handler != null)
				handler (item);
		}

		public void Reset ()
		{
			List = new List<T> ();
		}
	}
}

