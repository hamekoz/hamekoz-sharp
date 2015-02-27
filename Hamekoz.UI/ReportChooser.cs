//
//  ReportChooser.cs
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
using Xwt;

namespace Hamekoz.UI
{
	public class ReportChooser : HPaned
	{
		public ReportChooser ()
		{
			var listBox = new VBox ();
			var parameters = new VBox ();
			var searchEntry = new SearchTextEntry ();
			var listTree = new TreeView ();
			var store = new ListStore ();

			listBox.PackStart (searchEntry);
			listBox.PackStart (listTree);

			parameters.PackStart (new Label ("No parameters requiered"));

			Panel1.Content = listBox;
			Panel2.Content = parameters;
		}
	}
}

