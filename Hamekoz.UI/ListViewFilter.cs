//
//  Search.cs
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
using Hamekoz.Core;
using Xwt;

namespace Hamekoz.UI
{
	public class ListViewFilter<T>: VBox, IListSelector<T> where T : ISearchable
	{
		public IList<T> List {
			get;
			set;
		}

		SearchTextEntry text = new SearchTextEntry ();
		readonly ListView<T> filterList = new ListView<T> {
			MinWidth = 600,
			MinHeight = 300,
			GridLinesVisible = GridLines.Both,
		};

		public void Refresh ()
		{
			filterList.List = List.Where (r => r.ToSearchString ().ToUpper ().Contains (text.Text.ToUpper ())).ToList ();
		}

		public ListViewFilter ()
		{
			text.SetFocus ();
			text.SetCompletions (typeof(T).GetProperties ().Select (p => p.Name).ToArray<string> ());
			text.Activated += (sender, e) => Refresh ();

			PackStart (text);
			PackStart (filterList, true);
		}

		public event ListBoxFilterSelectionChanged SelectionItemChanged {
			add {
				filterList.SelectionItemChanged += value;
			}
			remove {
				filterList.SelectionItemChanged -= value;
			}
		}

		public void UnselectAll ()
		{
			filterList.UnselectAll ();
		}

		public SelectionMode SelectionMode {
			get {
				return filterList.SelectionMode;
			}
			set {
				filterList.SelectionMode = value;
			}
		}

		public T SelectedItem {
			get {
				return filterList.SelectedItem;
			}
			set {
				filterList.SelectedItem = value;
			}
		}

		public IList<T> SelectedItems {
			get {
				return filterList.SelectedItems;
			}
		}

		//TODO refactorizar para utilizar un diccionario basado en las propiedades, y eliminar las entradas basado en la propiedad
		public void RemoveColumnAt (int index)
		{
			filterList.RemoveColumnAt (index);
		}

		public void RemoveColumnAt (params int[] indexs)
		{
			foreach (var index in indexs.OrderByDescending(c => c).ToArray()) {
				filterList.RemoveColumnAt (index);
			}
		}

		public void ScrollToFirst ()
		{
			filterList.ScrollToFirst ();
		}

		public void ScrollToLast ()
		{
			filterList.ScrollToLast ();
		}

		public void ScrollTo (T item)
		{
			filterList.ScrollTo (item);
		}

		public void ScrollTo (int row)
		{
			filterList.ScrollToRow (row);
		}
	}
}

