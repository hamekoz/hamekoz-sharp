//
//  ItemPicker.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2015 Hamekoz
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
using Mono.Unix;
using Xwt;

namespace Hamekoz.UI
{
	public class ItemPicker : TextEntry, IListBoxFilter
	{
		readonly ListBoxFilter listBoxFilter = new ListBoxFilter {
			RealTimeFilter = false,
			ExpandHorizontal = true,
			ExpandVertical = true,
			HeightRequest = 150,
			WidthRequest = 350,
		};

		public ItemPicker ()
		{
			ReadOnly = true;
			PlaceholderText = Catalog.GetString ("Click o press Intro or Space to select one item");
			TooltipText = Catalog.GetString ("Click o press Intro or Space to select one item from the list");

			var popover = new Popover {
				Content = listBoxFilter,
			};

			Activated += delegate {
				popover.Show (Popover.Position.Top, this);
			};
			ButtonPressed += delegate {
				popover.Show (Popover.Position.Top, this);
			}; 

			popover.Closed += delegate {
				if (SelectedItem != null) {
					try {
						Text = SelectedItem.GetType ().GetProperty (FieldDescription).GetValue (SelectedItem, null).ToString ();
					} catch {
						Text = SelectedItem.ToString ();
					}
				} else {
					Text = string.Empty;
				}
			};
			listBoxFilter.ListBox.RowActivated += delegate {
				if (SelectedItem != null) {
					popover.Hide ();
				}
			};

			listBoxFilter.SelectionItemChanged += SelectionItemChanged;
		}

		#region IListBoxFilter implementation

		public event ListBoxFilterSelectionChanged SelectionItemChanged;

		public void SetList<T> (IList<T> typedList)
		{
			listBoxFilter.SetList<T> (typedList);
			Text = string.Empty;
		}

		public T GetSelectedItem<T> ()
		{
			return listBoxFilter.GetSelectedItem<T> ();
		}

		public IList<T> GetSelectedItems<T> ()
		{
			return listBoxFilter.GetSelectedItems<T> ();
		}

		public string FieldDescription {
			get {
				return listBoxFilter.FieldDescription;
			}
			set {
				listBoxFilter.FieldDescription = value;
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

		public IList<object> List {
			get {
				return listBoxFilter.List;
			}
			set {
				listBoxFilter.List = value;
				Text = string.Empty;
			}
		}

		public object SelectedItem {
			get {
				return listBoxFilter.SelectedItem;
			}
			set {
				listBoxFilter.SelectedItem = value;
				if (value == null)
					Text = string.Empty;
			}
		}

		public bool MultipleSelection {
			get {
				return listBoxFilter.MultipleSelection;
			}
			set {
				listBoxFilter.MultipleSelection = value;
			}
		}

		public IList<object> SelectedItems {
			get {
				return listBoxFilter.SelectedItems;
			}
		}

		#endregion

		//		protected virtual void OnSelectionItemChanged ()
		//		{
		//			var handler = SelectionItemChanged;
		//			if (handler != null)
		//				handler ();
		//		}
	}
}

