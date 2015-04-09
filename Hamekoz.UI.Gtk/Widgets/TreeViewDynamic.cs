//
//  TreeViewDynamic.cs
//
//  Author:
//       Emiliano Gabriel Canedo <emilianocanedo@gmail.com>
//
//  Copyright (c) 2015 ecanedo
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

using Gtk;
using System;
using Hamekoz.Core;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Hamekoz.UI.Gtk
{
	public delegate void ObjectHandler (object obj);

	[System.ComponentModel.ToolboxItem (true)]
	public partial class TreeViewDynamic : Bin
	{
		private List<string> stringList;
		private ListStore store;

		private object currentObject;

		public object CurrentObject {
			get {
				return currentObject;
			}
		}

		public event ObjectHandler EventCursorChanged;

		protected virtual void OnEventCursorChanged (object obj)
		{
			var handler = EventCursorChanged;
			if (handler != null)
				handler (obj);
		}

		private Type classType;

		public Type ClassType {
			get {
				return classType;
			}
			set {
				classType = value;
				foreach (TreeViewColumn col in treeview.Columns) {
					treeview.RemoveColumn (col);
				}
				PropertyInfo[] properties = classType.GetProperties ();
				stringList = new List<string> ();
				foreach (var property in properties)
					stringList.Add (property.Name);
				treeview.SetColumns (stringList.ToArray ());
				store = TreeViewHelpers.SetListStore (stringList.Count);
				treeview.Model = store;
			}
		}

		List<object> model;

		public List<object> Model {
			get {
				return model;
			}
		}

		public void SetList<T> (IList<T> modelClass)
		{
			ClassType = typeof(T);
			model = modelClass.Cast<object> ().ToList ();
			store.Clear ();
			foreach (var item in modelClass) {
				List<string> valores = new List<string> ();
				foreach (var propertyName in stringList) {
					valores.Add (item.GetType ().GetProperty (propertyName).GetValue (item, null).ToString ());
				}
				store.AppendValues (valores.ToArray ());
			}
		}

		public TreeViewDynamic ()
		{
			this.Build ();

			treeview.CursorChanged += delegate {
				int index;
				TreeIter treeIter;
				TreeModel treeModel;
				treeview.Selection.GetSelected (out treeModel, out treeIter);
				index = treeModel.GetPath (treeIter).Indices [0];
				currentObject = model [index];
				OnEventCursorChanged (model [index]);
			};
		}
	}
}

