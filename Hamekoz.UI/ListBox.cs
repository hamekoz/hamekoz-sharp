//
//  ListBoxFiltered.cs
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
using System.Reflection;
using Xwt;

namespace Hamekoz.UI
{
	public class ListBox<T> : ListBox
	{
		Type type = typeof(T);

		public ListBox ()
		{
			HorizontalPlacement = WidgetPlacement.Fill;
			VerticalPlacement = WidgetPlacement.Fill;
			ExpandHorizontal = true;
			ExpandVertical = true;
			SelectionChanged += (sender, e) => OnSelectionItemChanged (e);
		}

		string ItemLabel (T item)
		{
			string labelText;
			try {
				labelText = fieldDescription.GetValue (item, null).ToString ();
			} catch {
				labelText = item.ToString ();
			}
			return labelText;
		}

		PropertyInfo fieldDescription;

		public string FieldDescription {
			get {
				return fieldDescription.Name;
			}
			set {
				fieldDescription = type.GetProperty (value);
				ListFill ();
			}
		}

		void ListFill ()
		{
			Items.Clear ();
			foreach (var item in list) {
				Items.Add (item, ItemLabel (item));
			}
		}

		public new T SelectedItem {
			get { return (T)base.SelectedItem; }
			set { base.SelectedItem = value; }
		}

		public IList<T> SelectedItems {
			get {
				var selectedItems = new List<T> ();
				foreach (int item in SelectedRows) {
					selectedItems.Add ((T)Items [item]);
				}
				return selectedItems;
			}
		}

		IList<T> list = new List<T> ();

		public IList<T> List {
			get { return list; }
			set {
				list = value;
				ListFill ();
			}
		}

		public event ListBoxFilterSelectionChanged SelectionItemChanged;

		protected virtual void OnSelectionItemChanged (EventArgs e)
		{
			var handler = SelectionItemChanged;
			if (handler != null)
				handler (this, e);
		}
	}
}