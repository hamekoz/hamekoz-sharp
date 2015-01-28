//
//  DynamicTreeView.cs
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
using Hamekoz.Interfaces;
using System.Reflection;
using System.Collections.Generic;

namespace Hamekoz.UI.Gtk
{
	public class DynamicTreeView<T>
	{
		List<string> stringList;
		private ListStore Store;
		/// <summary>
		/// Initializes a new instance of the <see cref="DynamicTreeView"/> class.
		/// Set the treeView, according to the properties of the class T.
		/// </summary>
		/// <param name="tree">TreeView instance.</param>
		public DynamicTreeView (ref TreeView tree)
		{
			Type reflectionClass = typeof(T);
			PropertyInfo[] properties = reflectionClass.GetProperties ();
			stringList = new List<string>();
			foreach (var property in properties)
				stringList.Add (property.Name);
			tree.SetColumns (stringList.ToArray());
			Store = TreeViewHelpers.SetListStore (stringList.Count);
			tree.Model = Store;
		}
		/// <summary>
		/// Loads the tree, according to a list of instances of the class T.
		/// </summary>
		/// <param name="modelClass">Model class.</param>
		public void LoadByList (IEnumerable<T> modelClass)
		{
			foreach (var item in modelClass) {
				List<string> valores = new List<string> ();
				foreach (var propertyName in stringList) {
					valores.Add(item.GetType().GetProperty(propertyName).GetValue(item, null).ToString());
				}
				Store.AppendValues(valores.ToArray());
			}
		}

		public void Clear ()
		{
			Store.Clear ();
		}
	}
}

