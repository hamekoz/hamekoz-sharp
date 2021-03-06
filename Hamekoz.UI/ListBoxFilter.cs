﻿//
//  ListBoxFiltered.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
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
using System.Linq;
using System.Reflection;
using Humanizer;
using Xwt;
using Xwt.Drawing;

namespace Hamekoz.UI
{
	public class ListBoxFilter<T> : VBox, IListBoxFilter<T>
	{
		Type type = typeof(T);
		Label label;
		CheckBox allCheckBox;

		readonly SearchTextEntry search;

		internal SearchTextEntry Search {
			get {
				return search;
			}
		}

		readonly ListBox listBox;

		internal ListBox ListBox {
			get {
				return listBox;
			}
		}

		IList<T> list = new List<T> ();

		public ListBoxFilter ()
		{
			label = new Label {
				Text = type.Name.Humanize (),
			};

			search = new SearchTextEntry {
				PlaceholderText = Application.TranslationCatalog.GetString ("Filter"),
			};

			search.Activated += FilterActivated;
			search.Changed += FilterChanged;

			allCheckBox = new CheckBox {
				Label = Application.TranslationCatalog.GetString ("All"),
				AllowMixed = false,
				State = CheckBoxState.Off,
				Active = false,
				Visible = false,
			};
			allCheckBox.Clicked += AllCheckBoxClicked;

			listBox = new ListBox {
				ExpandHorizontal = true,
				ExpandVertical = true,
				HorizontalPlacement = WidgetPlacement.Fill,
				VerticalPlacement = WidgetPlacement.Fill,
			};

			listBox.SelectionChanged += (sender, e) => OnSelectionItemChanged (e);

			var box = new HBox ();
			box.PackStart (label);
			box.PackEnd (search);
			box.PackEnd (allCheckBox);
			PackStart (box, false, true);
			PackStart (listBox, true, true);
			ExpandHorizontal = true;
			ExpandVertical = true;
		}

		void AllCheckBoxClicked (object sender, EventArgs e)
		{
			listBox.Sensitive = !allCheckBox.Active;
			search.Sensitive = !allCheckBox.Active;
			search.Text = string.Empty;
			if (allCheckBox.Active && SelectionMode == SelectionMode.Multiple)
				listBox.SelectAll ();
			else
				listBox.UnselectAll ();
			OnSelectionItemChanged (e);
			if (!allCheckBox.Active)
				search.SetFocus ();
		}

		public string Label {
			get { return label.Text; }
			set {
				label.Text = value;
				label.Visible = value != string.Empty;
			}
		}

		public bool LableVisible {
			get {
				return label.Visible;
			}
			set {
				label.Visible = value;
				search.ExpandHorizontal = !value;
			}
		}

		public bool AllCheckBoxValue {
			get { return allCheckBox.Active; }
			set { 
				allCheckBox.Active = value;
				AllCheckBoxClicked (this, null);
			}
		}

		public bool AllCheckBoxVisible {
			get { return allCheckBox.Visible; }
			set { allCheckBox.Visible = value; }
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
			set {
				listBox.SelectedItem = value;
				ScrollTo (value);
			}
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

		public ScrollPolicy HorizontalScrollPolicy {
			get { return listBox.HorizontalScrollPolicy; }
			set { listBox.HorizontalScrollPolicy = value; }
		}

		public ScrollPolicy VerticalScrollPolicy {
			get { return listBox.VerticalScrollPolicy; }
			set { listBox.VerticalScrollPolicy = value; }
		}

		public event ListBoxFilterSelectionChanged SelectionItemChanged;

		bool filtering;

		protected virtual void OnSelectionItemChanged (EventArgs e)
		{
			var handler = SelectionItemChanged;
			if (handler != null && !filtering)
				handler (this, e);
		}

		public void Refresh ()
		{
			FilterActivated (search, null);
		}

		public void UnselectAll ()
		{
			listBox.UnselectAll ();
		}

		public void ResetFilter ()
		{
			Search.Text = string.Empty;
			Refresh ();
		}

		public void ClearList ()
		{
			List = new List<T> ();
		}

		public void ScrollToFirst ()
		{
			if (list != null && list.Count > 0)
				listBox.ScrollToRow (0);
		}

		public void ScrollToLast ()
		{
			if (list != null && list.Count > 0)
				listBox.ScrollToRow (list.Count - 1);
		}

		public void ScrollTo (T item)
		{
			if (list != null && list.Count > 0)
				listBox.ScrollToRow (list.IndexOf (item));
		}
	}
}