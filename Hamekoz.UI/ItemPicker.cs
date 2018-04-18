//
//  ItemPicker.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//       Mariano Adrian Ripa <ripamariano@gmail.com>
//
//  Copyright (c) 2015 Hamekoz - www.hamekoz.com.ar
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
using System.Reflection;
using Xwt;

namespace Hamekoz.UI
{
	public class ItemPicker<T> : TextEntry, IListBoxFilter<T>
	{
		Type type = typeof(T);

		readonly ListBoxFilter<T> listBoxFilter = new ListBoxFilter<T> {
			RealTimeFilter = false,
			ExpandHorizontal = true,
			ExpandVertical = true,
			HeightRequest = 150,
			HorizontalScrollPolicy = ScrollPolicy.Never,
			VerticalScrollPolicy = ScrollPolicy.Automatic,
		};

		Button selectedItemClear = new Button {
			Label = Application.TranslationCatalog.GetString ("Clear"),
			Image = Icons.EditClearAll.WithSize (IconSize.Small)
		};

		public ItemPicker ()
		{
			ReadOnly = true;
			PlaceholderText = Application.TranslationCatalog.GetString ("Click o press Intro or Space to select one item");
			TooltipText = Application.TranslationCatalog.GetString ("Click o press Intro or Space to select one item from the list");

			var popoverBox = new VBox ();
			popoverBox.PackStart (listBoxFilter, true, true);
			popoverBox.PackEnd (selectedItemClear, false, true);

			var popover = new Popover {
				Content = popoverBox,
			};

			Activated += delegate {
				if (!DisabledPicker) {
					popover.Show (Popover.Position.Top, this);
					listBoxFilter.Search.SetFocus ();
				}

			};
			ButtonPressed += delegate {
				if (!DisabledPicker) {
					popover.Show (Popover.Position.Top, this);
					listBoxFilter.Search.SetFocus ();
				}
			};

			listBoxFilter.ListBox.RowActivated += delegate {
				if (listBoxFilter.SelectedItem != null) {
					popover.Hide ();
					SelectedItem = listBoxFilter.SelectedItem;
				}
			};

			selectedItemClear.Clicked += delegate {
				listBoxFilter.UnselectAll ();
				popover.Hide ();
				SelectedItem = default(T);
			};
		}

		public bool DisabledPicker { get; set; }

		public void ClearPicker ()
		{
			listBoxFilter.Search.Text = string.Empty;
		}

		#region IListBoxFilter implementation

		//TODO definir otro tipo de evento para separar el cambio de item por intervencion del usuario del cambio de item por codigo
		public event ListBoxFilterSelectionChanged SelectionItemChanged;

		PropertyInfo fieldDescription;

		public string FieldDescription {
			get {
				return listBoxFilter.FieldDescription;
			}
			set {
				listBoxFilter.FieldDescription = value;
				fieldDescription = type.GetProperty (FieldDescription);
				Text = string.Empty;
			}
		}

		public bool RealTimeFilter {
			get {
				return listBoxFilter.RealTimeFilter;
			}
			set {
				listBoxFilter.RealTimeFilter = value;
			}
		}

		public IList<T> List {
			get {
				return listBoxFilter.List;
			}
			set {
				listBoxFilter.List = value;
				Text = string.Empty;
			}
		}

		T selectedItem;

		public T SelectedItem {
			get {
				return selectedItem;
			}
			set {
				selectedItem = value;
				listBoxFilter.SelectedItem = value;
				OnSelectionItemChanged (null);
			}
		}

		public SelectionMode SelectionMode {
			get {
				return listBoxFilter.SelectionMode;
			}
			set {
				listBoxFilter.SelectionMode = value;
			}
		}

		public IList<T> SelectedItems {
			get {
				return listBoxFilter.SelectedItems;
			}
		}

		public void UnselectAll ()
		{
			listBoxFilter.UnselectAll ();
		}

		#endregion

		public void Clear ()
		{
			selectedItem = default(T);
			Text = string.Empty;
			UnselectAll ();
			listBoxFilter.Search.Text = string.Empty;
			listBoxFilter.Refresh ();
		}

		public void Reset ()
		{
			DisabledPicker = false;
			List = new List<T> ();
			Clear ();
		}

		protected virtual void OnSelectionItemChanged (EventArgs e)
		{
			//TODO evaluar si seria correcto disparar este evento solo cuando el componente tiene sensibilidad
			if (selectedItem != null) {
				try {
					Text = fieldDescription.GetValue (selectedItem, null).ToString ();
				} catch {
					Text = selectedItem.ToString ();
				}
			} else {
				Text = string.Empty;
			}

			var handler = SelectionItemChanged;
			if (handler != null)
				handler (this, e);
		}
	}
}

