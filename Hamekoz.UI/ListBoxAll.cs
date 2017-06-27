//
//  ListBoxAll.cs
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
using Xwt;

namespace Hamekoz.UI
{
	public class ListBoxAll<T> : VBox, IListBoxFilter<T>
	{
		Label label;
		ListBoxFilter<T> listBoxFilter;
		CheckBox allCheckBox;

		public ListBoxAll ()
		{
			label = new Label {
				Text = Application.TranslationCatalog.GetString ("List description"),
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
				Label = Application.TranslationCatalog.GetString ("All"),
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
			set { allCheckBox.Active = value; }
		}

		public string AllCheckBoxLabel {
			get { return allCheckBox.Label; }
			set { allCheckBox.Label = value; }
		}

		public bool AllCheckBoxVisible {
			get { return allCheckBox.Visible; }
			set { allCheckBox.Visible = value; }
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

		public void UnselectAll ()
		{
			listBoxFilter.UnselectAll ();
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