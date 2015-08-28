//
//  TreeViewHelpers.cs
//
//  Author:
//       Emiliano Gabriel Canedo <emilianocanedo@gmail.com>
//
//  Copyright (c) 2014 Hamekoz
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
using Gtk;
using Hamekoz.Extensions;

namespace Hamekoz.UI.Gtk
{
	public static class TreeViewHelpers
	{
		/// <summary>
		/// Helper for set the columns of a TreeView.
		/// </summary>
		/// <param name="tree">Tree.</param>
		/// <param name="columns">Columns.</param>
		public static void SetColumns (this TreeView tree, params string[] columns)
		{
			int iter = 0;
			foreach (string column in columns) {
				var treeColumn = new TreeViewColumn ();
				treeColumn.Title = column.ToHumanize ();
				var columnCell = new CellRendererText ();
				treeColumn.PackStart (columnCell, true);
				tree.AppendColumn (treeColumn);
				treeColumn.AddAttribute (columnCell, "text", iter);
				iter++;
			}
		}

		/// <summary>
		/// Sets the ListStore with the type string.
		/// </summary>
		/// <returns>The list store.</returns>
		/// <param name="numberOfColumns">Number of columns.</param>
		public static ListStore SetListStore (int numberOfColumns)
		{
			var types = new List<Type> ();
			for (int iter = 0; iter < numberOfColumns; iter++)
				types.Add (typeof(string));
			return new ListStore (types.ToArray ());
		}
	}
}

