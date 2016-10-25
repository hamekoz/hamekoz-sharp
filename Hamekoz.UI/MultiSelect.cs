//
//  MultiSelect.cs
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
using System.Collections.Generic;
using Mono.Unix;
using Xwt;

namespace Hamekoz.UI
{
	public class MultiSelect<T> : HBox
	{
		string fildDescription;

		public string FildDescription {
			get {
				return fildDescription;
			}
			set {
				fildDescription = value;
				list.FieldDescription = value;
				selected.FieldDescription = value;
			}
		}

		public IList<T> ListAll {
			get { return list.List; }
			set { list.List = value; }
		}

		public IList<T> ListSelected {
			get { return selected.List; }
			set { selected.List = value; }
		}

		readonly VBox actionBox = new VBox {
			VerticalPlacement = WidgetPlacement.Center,
		};

		readonly ListBoxFilter<T> list = new ListBoxFilter<T> {
			SelectionMode = SelectionMode.Multiple,
		};
		readonly ListBoxFilter<T> selected = new ListBoxFilter<T> {
			SelectionMode = SelectionMode.Multiple,
		};

		readonly Button agregar = new Button {
			Image = Icons.GoPrevious.WithSize (IconSize.Medium),
			TooltipText = Catalog.GetString ("Add"),
		};

		readonly Button agregarTodos = new Button {
			Image = Icons.GoFirst.WithSize (IconSize.Medium),
			TooltipText = Catalog.GetString ("Add all"),
		};

		readonly Button quitar = new Button {
			Image = Icons.GoNext.WithSize (IconSize.Medium),
			TooltipText = Catalog.GetString ("Remove"),
		};

		readonly Button quitarTodos = new Button {
			Image = Icons.GoLast.WithSize (IconSize.Medium),
			TooltipText = Catalog.GetString ("Remove all"),
		};

		public MultiSelect ()
		{
			agregar.Clicked += delegate {
				if (list.SelectedItems.Count > 0) {
					foreach (var item in list.SelectedItems) {
						selected.List.Add (item);
						list.List.Remove (item);
					}
					selected.Refresh ();
					list.Refresh ();
				}
			};

			agregarTodos.Clicked += delegate {
				foreach (var item in list.List) {
					selected.List.Add (item);
				}
				selected.Refresh ();
				list.List.Clear ();
				list.Refresh ();
			};

			quitar.Clicked += delegate {
				if (selected.SelectedItems.Count > 0) {
					foreach (var item in selected.SelectedItems) {
						list.List.Add (item);
						selected.List.Remove (item);
					}
					selected.Refresh ();
					list.Refresh ();
				}
			};

			quitarTodos.Clicked += delegate {
				foreach (var item in selected.List) {
					list.List.Add (item);
				}
				selected.List.Clear ();
				selected.Refresh ();
				list.Refresh ();
			};

			actionBox.PackStart (agregar);
			actionBox.PackStart (quitar);
			actionBox.PackStart (agregarTodos);
			actionBox.PackStart (quitarTodos);

			PackStart (selected.WithLabel (Catalog.GetString ("Selected")), true, true);
			PackStart (actionBox);
			PackStart (list.WithLabel (Catalog.GetString ("Availables")), true, true);
		}
	}

	public class MultiSelectListView<T> : ListView
	{
		readonly ListStore store;
		readonly IDataField<bool> checkDataField = new DataField<bool> ();
		readonly IDataField<T> itemDataField = new DataField<T> ();

		IList<T> list;

		public IList<T> List {
			get {
				return list;
			}
			set {
				list = value;
				StoreFill ();
			}
		}

		IList<T> selectedItems;

		public IList<T> SelectedItems {
			get {
				return selectedItems;
			}
			set {
				selectedItems = value;
				StoreFill ();
				foreach (var item in selectedItems) {
					int r = list.IndexOf (item);
					store.SetValue (r, checkDataField, true);
				}
			}
		}

		public MultiSelectListView ()
		{
			store = new ListStore (checkDataField, itemDataField);
			DataSource = store;
		}

		void StoreFill ()
		{
			store.Clear ();
			foreach (var item in list) {
				var r = store.AddRow ();
				store.SetValue (r, checkDataField, false);
				store.SetValue (r, itemDataField, item);
			}
		}

		bool editable;

		public bool Editable {
			get {
				return editable;
			}
			set {
				editable = value;
				StoreFill ();
			}
		}
	}
}

