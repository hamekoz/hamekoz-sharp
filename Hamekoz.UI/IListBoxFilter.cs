//
//  IListBoxFilter.cs
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
using System.Collections.Generic;
using System;
using Xwt;

namespace Hamekoz.UI
{
	public delegate void ListBoxFilterSelectionChanged (object sender, EventArgs e);

	[Obsolete ("Use IListBoxFilter<T>")]
	public interface IListBoxFilter
	{
		event ListBoxFilterSelectionChanged SelectionItemChanged;

		string FieldDescription { get; set; }

		bool RealTimeFilter { get; set; }

		void SetList<T> (IList<T> typedList);

		IList<object> List { get; set; }

		object SelectedItem { get; set; }

		T GetSelectedItem<T> ();

		[Obsolete ("Use SelectionMode from IListBoxFilter<T>")]
		bool MultipleSelection { get; set; }

		IList<object> SelectedItems { get; }

		IList<T> GetSelectedItems<T> ();
	}

	public interface IListBoxFilter<T>
	{
		event ListBoxFilterSelectionChanged SelectionItemChanged;

		string FieldDescription { get; set; }

		bool RealTimeFilter { get; set; }

		SelectionMode SelectionMode { get; set; }

		IList<T> List { get; set; }

		T SelectedItem { get; set; }

		IList<T> SelectedItems { get; }

		void UnselectAll ();
	}
}

