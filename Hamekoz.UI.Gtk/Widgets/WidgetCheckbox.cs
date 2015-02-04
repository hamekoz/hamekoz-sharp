//
//  WidgetCheckbox.cs
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
using System;
using Gtk;

namespace Hamekoz.UI.Gtk
{
	public delegate void ChangedHandler();

	[System.ComponentModel.ToolboxItem (true)]
	public partial class WidgetDetallado : Bin
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

		protected virtual void OnChangeState ()
		{
			var handler = ChangeState;
			if (handler != null)
				handler ();
		}

		public bool Estado {
			get {
				return checkbutton.Active;
			}
			set {
				checkbutton.Active = value;
			}
		}

		public WidgetDetallado ()
		{
			this.Build ();

			checkbutton.Clicked += delegate {
				OnChangeState();
			};
		}
	}
}

