//
//  ListView.cs
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

namespace Hamekoz.UI
{
	public class ListView<T> : ListView, IListSelector<T>
	{
		readonly Type type;
		readonly ListStore store;
		readonly DataField<T> itemDataField = new DataField<T> ();
		readonly List<IDataField> datafields;

		IList<T> list;

		public T SelectedItem {
			get {
				T selected = default(T);
				if (SelectedRow >= 0 && SelectedRow < list.Count) {
					selected = store.GetValue (SelectedRow, itemDataField);
				}
				return selected;
			}
			set {
				Refresh ();
				int index = list.IndexOf (value);
				SelectRow (index);
			}
		}

		public IList<T> SelectedItems {
			get {
				var selected = new List<T> ();
				foreach (var index in SelectedRows)
					selected.Add (store.GetValue (index, itemDataField));
				return selected;
			}
			set {
				Refresh ();
				foreach (var item in value) {
					int index = list.IndexOf (item);
					SelectRow (index);
				}
			}
		}

		public IList<T> List {
			get {
				return list;
			}
			set {
				list = value;
				Refresh ();
			}
		}

		public void Clear ()
		{
			List = new List<T> ();
		}

		public void Refresh ()
		{
			store.Clear ();
			foreach (var item in list)
				FillRow (item);
		}

		public void Add (T item)
		{
			List.Add (item);
			FillRow (item);
		}

		public void Remove ()
		{
			foreach (var row in SelectedRows)
				Remove (row);
		}

		public void Remove (int row)
		{
			var item = store.GetValue (row, itemDataField);
			List.Remove (item);
			store.RemoveRow (row);
		}

		public void FillRow (T item)
		{
			var row = store.AddRow ();
			FillRow (row, item);
		}

		public void FillRow (int row, T item)
		{
			foreach (var column in Columns) {
				var field = datafields [Columns.IndexOf (column)];
				var propertyName = column.Title.Dehumanize ();
				var property = type.GetProperty (propertyName);
				switch (Type.GetTypeCode (property.PropertyType)) {
				case TypeCode.Int32:
					if (property.PropertyType.IsEnum) {
						var value = property.GetValue (item, null);
						store.SetValue (row, (IDataField<string>)field, ((Enum)value).Humanize ());
					} else {
						store.SetValue<string> (row, (IDataField<string>)field, string.Format ("{0:D}", (int)property.GetValue (item, null)));
					}
					break;
				case TypeCode.Double:
					store.SetValue<string> (row, (IDataField<string>)field, string.Format ("{0:#0.####}", (double)property.GetValue (item, null)));
					break;
				case TypeCode.Decimal:
					store.SetValue<string> (row, (IDataField<string>)field, string.Format ("{0:N}", (decimal)property.GetValue (item, null)));
					break;
				case TypeCode.Boolean:
					store.SetValue<bool> (row, (IDataField<bool>)field, (bool)property.GetValue (item, null));
					break;
				case TypeCode.DateTime:
					var date = (DateTime)property.GetValue (item, null);
					string dateString = date == date.Date ? date.ToShortDateString () : date.ToString ();
					store.SetValue<string> (row, (IDataField<string>)field, dateString);
					break;
				case TypeCode.String:
					store.SetValue<string> (row, (IDataField<string>)field, (string)property.GetValue (item, null));
					break;
				default:
					if (property.PropertyType == typeof(DateTime?)) {
						var datenull = (DateTime?)property.GetValue (item, null);
						string datenullString = datenull.HasValue ? (datenull.Value == datenull.Value.Date ? datenull.Value.ToShortDateString () : datenull.ToString ()) : string.Empty;
						store.SetValue<string> (row, (IDataField<string>)field, datenullString);
						break;
					} else {
						store.SetValue (row, (IDataField<object>)field, property.GetValue (item, null));
						break;	
					}
				}
				store.SetValue (row, itemDataField, item);
			}
		}

		//TODO evaluar como poder definir que columnas y en que orden mostrar en la grilla. Una idea es que la clase implemente intefaces de vista, y se pase al constructor la interfaaz a utilizar para la presentacion
		public ListView ()
		{
			type = typeof(T);
			datafields = new List<IDataField> ();
			PropertyInfo[] properties = type.GetProperties ();
			foreach (var property in properties) {
				IDataField datafield;
				switch (Type.GetTypeCode (property.PropertyType)) {
				case TypeCode.Boolean:
					datafield = new DataField<bool> ();
					break;
				case TypeCode.DateTime:
				case TypeCode.String:
				case TypeCode.Int32:
				case TypeCode.Double:
				case TypeCode.Decimal:
					datafield = new DataField<string> ();
					break;
				default:
					if (property.PropertyType == typeof(DateTime?)) {
						datafield = new DataField<string> ();
						break;
					} else {
						datafield = new DataField<object> ();
						break;
					}
				}
				datafields.Add (datafield);
			}
			datafields.Add (itemDataField);
			store = new ListStore (datafields.ToArray ());
			for (int i = 0; i < properties.Length; i++) {
				switch (Type.GetTypeCode (properties [i].PropertyType)) {
				case TypeCode.Boolean:
					var boolColumn = new ListViewColumn (properties [i].Name.Humanize (), new CheckBoxCellView ((IDataField<bool>)datafields [i])) {
						CanResize = true,
						//FIXME revisar porque la ordenacion esta causando que en el resfresco no se muestre contenido en algunas celdas
//						SortDataField = datafields [i]
					};
					Columns.Add (boolColumn);
					break;
				
				case TypeCode.Int32:
				case TypeCode.Double:
				case TypeCode.Decimal:
					var numberColumn = new ListViewColumn (properties [i].Name.Humanize (), new TextCellView (datafields [i])) {
						CanResize = true,
						//TODO ver como aliniear las celdas de contenido, esta propiedad solo alinea la celda de la cabecera
						//Alignment = Alignment.End,
//						SortDataField = datafields [i],
					};
					Columns.Add (numberColumn);
					break;
				case TypeCode.DateTime:
				case TypeCode.String:
					var textColumn = new ListViewColumn (properties [i].Name.Humanize (), new TextCellView (datafields [i])) {
						CanResize = true,
//						SortDataField = datafields [i],
					};
					Columns.Add (textColumn);
					break;
				default:
					var otherColumn = new ListViewColumn (properties [i].Name.Humanize (), new TextCellView (datafields [i])) {
						CanResize = true,
						//UNDONE ver como hacer para poder ordenar una columna en que el tipo es T
						//SortDataField = datafields [i]
					};
					Columns.Add (otherColumn);
					break;
				}
			}
			DataSource = store;
			SelectionChanged += (sender, e) => OnSelectionItemChanged (e);
		}

		//TODO refactorizar para utilizar un diccionario basado en las propiedades, y eliminar las entradas basado en la propiedad
		public void RemoveColumnAt (int index)
		{
			Columns.RemoveAt (index);
			datafields.RemoveAt (index);
		}

		public void RemoveColumnAt (params int[] indexs)
		{
			foreach (var index in indexs.OrderByDescending(c => c).ToArray()) {
				Columns.RemoveAt (index);
				datafields.RemoveAt (index);
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

