//
//  ListViewDinamic.cs
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
	public class ListViewDinamic<T> : ListView
	{
		readonly Type type;
		readonly ListStore store;
		readonly List<IDataField<object>> datafields;

		IList<T> list;

		public T Current {
			get { return list [SelectedRow]; }
		}

		public IList<T> List {
			get {
				return list;
			}
			set {
				list = value;
				store.Clear ();
				foreach (var item in list) {
					var r = store.AddRow ();
					foreach (var column in Columns) {
						store.SetValue (r, datafields [Columns.IndexOf (column)], type.GetProperty (column.Title).GetValue (item, null));
					}
				}
			}
		}

		public ListViewDinamic ()
		{
			type = typeof(T);
			datafields = new List<IDataField<object>> ();
			PropertyInfo[] properties = type.GetProperties ();
			foreach (var property in properties) {
				var datafield = new DataField<object> ();
				//datafield.FieldType = property.PropertyType;
				datafields.Add (datafield);
			}
			store = new ListStore (datafields.ToArray ());
			for (int i = 0; i < properties.Length; i++) {
				Columns.Add (properties [i].Name, datafields [i]);
			}
			DataSource = store;
		}
	}
}

