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
using System.Linq;
using System.Reflection;
using Mono.Unix;
using Xwt;
using Xwt.Drawing;

namespace Hamekoz.UI
{
	[Obsolete ("Use ListBoxFilter<T>")]
	public class ListBoxFilter : VBox, IListBoxFilter
	{
		SearchTextEntry search;

		internal SearchTextEntry Search {
			get {
				return search;
			}
		}

		ListBox listBox;

		internal ListBox ListBox {
			get {
				return listBox;
			}
		}

		IList<object> list = new List<object> ();

		public ListBoxFilter ()
		{
			search = new SearchTextEntry {
				PlaceholderText = Catalog.GetString ("Filter"),
			};

			search.Activated += FilterActivated;
			search.Changed += FilterChanged;

			listBox = new ListBox {
				ExpandHorizontal = true,
				ExpandVertical = true,
				HorizontalPlacement = WidgetPlacement.Fill,
				VerticalPlacement = WidgetPlacement.Fill,
			};
			listBox.SelectionChanged += (sender, e) => OnSelectionChanged (e);

			PackStart (search);
			PackStart (listBox, true, true);
			ExpandHorizontal = true;
			ExpandVertical = true;
		}

		void FilterList ()
		{
			FilterList (string.Empty);
		}

		void FilterList (string filter)
		{
			search.BackgroundColor = filter == string.Empty ? Colors.White : Colors.LightGreen;
			filter = filter.ToUpper ();
			var filterList = list.Where (i => ItemLabel (i).ToUpper ().Contains (filter));
			listBox.UnselectAll ();
			listBox.Items.Clear ();
			foreach (var item in filterList) {
				listBox.Items.Add (item, ItemLabel (item));
			}
		}

		string ItemLabel (object item)
		{
			string labelText;
			try {
				labelText = item.GetType ().GetProperty (FieldDescription).GetValue (item, null).ToString ();
			} catch {
				labelText = item.ToString ();
			}
			return labelText;
		}

		void FilterActivated (object sender, EventArgs e)
		{
			var s = sender as SearchTextEntry;
			FilterList (s.Text);
		}

		void FilterChanged (object sender, EventArgs e)
		{
			var s = sender as SearchTextEntry;
			if (RealTimeFilter || s.Text == string.Empty) {
				FilterList (s.Text);
			} else {
				search.BackgroundColor = Colors.White;
			}
		}

		string fieldDescription;

		public string FieldDescription {
			get {
				return fieldDescription;
			}
			set {
				fieldDescription = value;
				FilterList ();
			}
		}

		public object SelectedItem {
			get { return listBox.SelectedItem; }
			set { listBox.SelectedItem = value; }
		}

		public IList<object> SelectedItems {
			get {
				var selectedItems = new List<object> ();
				foreach (int item in listBox.SelectedRows) {
					selectedItems.Add (listBox.Items [item]);
				}
				return selectedItems;
			}
		}

		public IList<object> List {
			get { return list; }
			set {
				list = value;
				FilterList ();
			}
		}

		public void SetList<T> (IList<T> typedList)
		{
			List = typedList.Cast<object> ().ToList ();
		}

		public T GetSelectedItem<T> ()
		{
			return (T)SelectedItem;
		}

		public IList<T> GetSelectedItems<T> ()
		{
			return SelectedItems.Cast<T> ().ToList ();
		}

		public bool RealTimeFilter {
			get;
			set;
		}

		public bool MultipleSelection {
			get { return listBox.SelectionMode == SelectionMode.Multiple; }
			set {
				listBox.SelectionMode = value ? SelectionMode.Multiple : SelectionMode.Single;
			}
		}

		public event ListBoxFilterSelectionChanged SelectionItemChanged;

		protected virtual void OnSelectionChanged (EventArgs e)
		{
			var handler = SelectionItemChanged;
			if (handler != null)
				handler (this, e);
		}
	}

	public class ListBoxFilter<T> : VBox, IListBoxFilter<T>
	{
		Type type = typeof(T);

		SearchTextEntry search;

		internal SearchTextEntry Search {
			get {
				return search;
			}
		}

		ListBox listBox;

		internal ListBox ListBox {
			get {
				return listBox;
			}
		}

		IList<T> list = new List<T> ();

		public ListBoxFilter ()
		{
			search = new SearchTextEntry {
				PlaceholderText = Catalog.GetString ("Filter"),
			};

			search.Activated += FilterActivated;
			search.Changed += FilterChanged;

			listBox = new ListBox {
				ExpandHorizontal = true,
				ExpandVertical = true,
				HorizontalPlacement = WidgetPlacement.Fill,
				VerticalPlacement = WidgetPlacement.Fill,
			};

			listBox.SelectionChanged += (sender, e) => OnSelectionItemChanged (e);

			PackStart (search);
			PackStart (listBox, true, true);
			ExpandHorizontal = true;
			ExpandVertical = true;
		}

		void FilterList ()
		{
			FilterList (string.Empty);
		}

		void FilterList (string filter)
		{
			filtering = true;
			search.BackgroundColor = filter == string.Empty ? Colors.White : Colors.LightGreen;
			filter = filter.ToUpper ();
			var filterList = list.Where (i => ItemLabel (i).ToUpper ().Contains (filter));
			listBox.Items.Clear ();
			foreach (var item in filterList) {
				listBox.Items.Add (item, ItemLabel (item));
			}
			filtering = false;
			listBox.UnselectAll ();
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

		void FilterActivated (object sender, EventArgs e)
		{
			var s = sender as SearchTextEntry;
			FilterList (s.Text);
		}

		void FilterChanged (object sender, EventArgs e)
		{
			var s = sender as SearchTextEntry;
			if (RealTimeFilter || s.Text == string.Empty) {
				FilterList (s.Text);
			} else {
				search.BackgroundColor = Colors.White;
			}
		}

		PropertyInfo fieldDescription;

		public string FieldDescription {
			get {
				return fieldDescription.Name;
			}
			set {
				fieldDescription = type.GetProperty (value);
				FilterList ();
			}
		}

		public T SelectedItem {
			get { return (T)listBox.SelectedItem; }
			set { listBox.SelectedItem = value; }
		}

		public IList<T> SelectedItems {
			get {
				var selectedItems = new List<T> ();
				foreach (int item in listBox.SelectedRows) {
					selectedItems.Add ((T)listBox.Items [item]);
				}
				return selectedItems;
			}
		}

		public IList<T> List {
			get { return list; }
			set {
				list = value;
				FilterList ();
			}
		}

		public bool RealTimeFilter {
			get;
			set;
		}

		public SelectionMode SelectionMode {
			get { return listBox.SelectionMode; }
			set { listBox.SelectionMode = value; }
		}

		public event ListBoxFilterSelectionChanged SelectionItemChanged;

		bool filtering;

		protected virtual void OnSelectionItemChanged (EventArgs e)
		{
			var handler = SelectionItemChanged;
			if (handler != null && !filtering)
				handler (this, e);
		}

		public void ClearSearch ()
		{
			search.Text = string.Empty;
		}
	}
}