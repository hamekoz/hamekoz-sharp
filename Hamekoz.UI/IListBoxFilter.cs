//
//  IListBoxFilter.cs
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

namespace Hamekoz.UI
{
	public interface IListBoxFilter
	{
		event ListBoxFilterSelectionChanged SelectionChanged;

		string FieldDescription { get; set; }

		bool RealTimeFilter { get; set; }

		void SetList<T> (IList<T> typedList);

		IList<object> List { get; set; }

		object SelectedItem { get; set; }

		T GetSelectedItem<T> ();

		bool MultipleSelection { get; set; }

		IList<object> SelectedItems { get; }

		IList<T> GetSelectedItems<T> ();
	}
}

