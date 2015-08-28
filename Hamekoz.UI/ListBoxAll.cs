//
//  ListBoxAll.cs
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
	[Obsolete ("Use ListBoxAll<T>")]
	public class ListBoxAll : VBox, IListBoxFilter
	{
		Label label;
		ListBoxFilter listBoxFilter;
		CheckBox allCheckBox;

		public ListBoxAll ()
		{
			label = new Label {
				Text = Catalog.GetString ("List description"),
				Visible = false,
			};

			listBoxFilter = new ListBoxFilter {
				ExpandHorizontal = true,
				ExpandVertical = true,
				HorizontalPlacement = WidgetPlacement.Fill,
				VerticalPlacement = WidgetPlacement.Fill,
			};
			listBoxFilter.SelectionItemChanged += OnSelectionChanged;

			allCheckBox = new CheckBox {
				Label = Catalog.GetString ("All"),
				AllowMixed = false,
				State = CheckBoxState.Off,
				Active = false,
				Visible = false,
			};
			allCheckBox.Clicked += AllCheckBoxClicked;

			PackStart (label);
			PackStart (listBoxFilter);
			PackStart (allCheckBox);

			listBoxFilter.ExpandVertical = true;
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

		void AllCheckBoxClicked (object sender, EventArgs e)
		{
			listBoxFilter.Sensitive = !allCheckBox.Active;
			listBoxFilter.Search.Sensitive = !allCheckBox.Active;
			listBoxFilter.Search.Text = string.Empty;
			if (allCheckBox.Active && MultipleSelection)
				listBoxFilter.ListBox.SelectAll ();
			else
				listBoxFilter.ListBox.UnselectAll ();
			OnSelectionChanged (sender, e);
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

		#region IListBoxFilter implementation

		public event ListBoxFilterSelectionChanged SelectionItemChanged;

		public void SetList<T> (IList<T> typedList)
		{
			listBoxFilter.SetList<T> (typedList);
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
			}
		}

		public object SelectedItem {
			get {
				return listBoxFilter.SelectedItem;
			}
			set {
				listBoxFilter.SelectedItem = value;
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

		protected virtual void OnSelectionChanged (object sender, EventArgs e)
		{
			var handler = SelectionItemChanged;
			if (handler != null)
				handler (this, e);
		}
	}

	public class ListBoxAll<T> : VBox, IListBoxFilter<T>
	{
		Label label;
		ListBoxFilter<T> listBoxFilter;
		CheckBox allCheckBox;

		public ListBoxAll ()
		{
			label = new Label {
				Text = Catalog.GetString ("List description"),
				Visible = false,
			};

			listBoxFilter = new ListBoxFilter<T> {
				ExpandHorizontal = true,
				ExpandVertical = true,
				HorizontalPlacement = WidgetPlacement.Fill,
				VerticalPlacement = WidgetPlacement.Fill,
			};
			listBoxFilter.SelectionItemChanged += (sender, e) => OnSelectionItemChanged (e);

			allCheckBox = new CheckBox {
				Label = Catalog.GetString ("All"),
				AllowMixed = false,
				State = CheckBoxState.Off,
				Active = false,
				Visible = false,
			};
			allCheckBox.Clicked += AllCheckBoxClicked;

			PackStart (label);
			PackStart (listBoxFilter);
			PackStart (allCheckBox);

			listBoxFilter.ExpandVertical = true;
		}

		void AllCheckBoxClicked (object sender, EventArgs e)
		{
			listBoxFilter.Sensitive = !allCheckBox.Active;
			listBoxFilter.Search.Sensitive = !allCheckBox.Active;
			listBoxFilter.Search.Text = string.Empty;
			if (allCheckBox.Active && SelectionMode == SelectionMode.Multiple)
				listBoxFilter.ListBox.SelectAll ();
			else
				listBoxFilter.ListBox.UnselectAll ();
			OnSelectionItemChanged (e);
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

		#region IListBoxFilter implementation

		public event ListBoxFilterSelectionChanged SelectionItemChanged;

		public string FieldDescription {
			get {
				return listBoxFilter.FieldDescription;
			}
			set {
				listBoxFilter.FieldDescription = value;
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
			}
		}

		public T SelectedItem {
			get {
				return listBoxFilter.SelectedItem;
			}
			set {
				listBoxFilter.SelectedItem = value;
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

		#endregion

		protected virtual void OnSelectionItemChanged (EventArgs e)
		{
			var handler = SelectionItemChanged;
			if (handler != null)
				handler (this, e);
		}
	}
}