//
//  Extensions.cs
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
using Xwt;

namespace Hamekoz.UI
{
	public static class Extensions
	{
		public static Box WithLabel (this Widget widget, string label, bool horizontal = false)
		{
			Box box;
			if (horizontal) {
				box = new HBox ();
			} else {
				box = new VBox ();
			}
			box.ExpandVertical = true;
			box.ExpandHorizontal = true;
			box.PackStart (new Label (label));
			box.PackStart (widget, true, true);
			return box;
		}
	}
}

