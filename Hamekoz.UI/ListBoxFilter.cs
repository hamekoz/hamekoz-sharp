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
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using Xwt;
using Xwt.Drawing;

namespace Hamekoz.UI
{
	public delegate void ListBoxFilterSelectionChanged ();

	public class ListBoxFilter : VBox
	{
		Label label;
		SearchTextEntry search;
		ListBox listBox;
		CheckBox allCheckBox;
		IList<object> list = new List<object> ();

		public ListBoxFilter ()
		{
			label = new Label () {
				Text = "List description",
				Visible = false,
			};

			search = new SearchTextEntry () {
				PlaceholderText = "Filter",
			};

			search.Activated += Filter_Activated;
			search.Changed += Filter_Changed;

			listBox = new ListBox () {
				ExpandHorizontal = true,
				ExpandVertical = true,
				HorizontalPlacement = WidgetPlacement.Fill,
				VerticalPlacement = WidgetPlacement.Fill,
			};
			listBox.SelectionChanged += delegate {
				OnSelectionChanged ();
			};

			allCheckBox = new CheckBox () {
				Label = "All",
				AllowMixed = false,
				State = CheckBoxState.Off,
				Active = false,
				Visible = false,
			};
			allCheckBox.Clicked += AllCheckBox_Clicked;

			PackStart (label);
			PackStart (search);
			PackStart (listBox);
			PackStart (allCheckBox);

			listBox.ExpandVertical = true;
		}

		void FilterList ()
		{
			FilterList (string.Empty);
		}

		void FilterList (string filter)
		{
			if (filter == string.Empty) {
				search.BackgroundColor = Colors.White;
			} else {
				search.BackgroundColor = Colors.LightGreen;
			}
			filter = filter.ToUpper ();
			var filterList = list.Where (i => ItemLabel (i).ToUpper ().Contains (filter));
			listBox.Items.Clear ();
			foreach (var item in filterList) {
				listBox.Items.Add (item, ItemLabel (item));
			}
		}

		string ItemLabel (object item)
		{
			string label;
			try {
				label = item.GetType ().GetProperty (FieldDescription).GetValue (item, null).ToString ();
			} catch {
				label = item.ToString ();
			}
			return label;
		}

		void Filter_Activated (object sender, EventArgs e)
		{
			var s = sender as SearchTextEntry;
			FilterList (s.Text);
		}

		void Filter_Changed (object sender, EventArgs e)
		{
			var s = sender as SearchTextEntry;
			if (RealTimeFilter || s.Text == string.Empty) {
				FilterList (s.Text);
			} else {
				search.BackgroundColor = Colors.White;
			}
		}

		void AllCheckBox_Clicked (object sender, EventArgs e)
		{
			listBox.Sensitive = !allCheckBox.Active;
			search.Sensitive = !allCheckBox.Active;
			search.Text = string.Empty;
			if (allCheckBox.Active && MultipleSelection)
				listBox.SelectAll ();
			else
				listBox.UnselectAll ();
			OnSelectionChanged ();
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

		public List<T> GetSelectedItems<T> ()
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

		public string Label {
			get { return label.Text; }
			set {
				label.Text = value;
				label.Visible = value != string.Empty;
			}
		}

		public bool AllCheckBoxValue {
			get { return allCheckBox.Active; }
			set {
				allCheckBox.Active = value;
				allCheckBox.Visible = true;
			}
		}

		public string AllCheckBoxLabel {
			get { return allCheckBox.Label; }
			set {
				allCheckBox.Label = value;
				allCheckBox.Visible = value != string.Empty;
			}
		}

		public bool AllCheckBoxVisible {
			get { return allCheckBox.Visible; }
			set { allCheckBox.Visible = value; }
		}

		public string FilterPlaceholderText {
			get { return search.PlaceholderText; }
			set { search.PlaceholderText = value; }
		}

		public event ListBoxFilterSelectionChanged SelectionChanged;

		protected virtual void OnSelectionChanged ()
		{
			var handler = SelectionChanged;
			if (handler != null)
				handler ();
		}
	}
}