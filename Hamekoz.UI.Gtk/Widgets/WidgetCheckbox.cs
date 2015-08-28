//
//  WidgetCheckbox.cs
//
//  Author:
//       Emiliano Gabriel Canedo <emilianocanedo@gmail.com>
//		 Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
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
using Gtk;

namespace Hamekoz.UI.Gtk
{
	public delegate void ChangedHandler ();

	[System.ComponentModel.ToolboxItem (true)]
	public sealed partial class WidgetCheckbox : Bin
	{
		public string Label {
			get {
				return checkbutton.Label;
			}
			set {
				checkbutton.Label = value;
			}
		}

		public event ChangedHandler ChangeState;

		protected void OnChangeState ()
		{
			var handler = ChangeState;
			if (handler != null)
				handler ();
		}

		public bool Checked {
			get {
				return checkbutton.Active;
			}
			set {
				checkbutton.Active = value;
			}
		}

		public WidgetCheckbox ()
		{
			Build ();

			checkbutton.Clicked += delegate {
				OnChangeState ();
			};
		}
	}
}

