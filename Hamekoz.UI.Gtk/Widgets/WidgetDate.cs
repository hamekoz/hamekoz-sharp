//
//  WidgetDate.cs
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
using System;
using Gtk;

namespace Hamekoz.UI.Gtk
{
	[System.ComponentModel.ToolboxItem (true)]
	public sealed partial class WidgetDate : Bin
	{
		public string Label {
			get {
				return label.Text;
			}
			set {
				label.Text = value;
			}
		}

		public delegate void DateChangedHandler ();

		public event DateChangedHandler ChangeDate;

		protected void OnChangeDate ()
		{
			var handler = ChangeDate;
			if (handler != null)
				handler ();
		}

		public DateTime Date {
			get {
				return datepicker.Date;
			}
			set {
				datepicker.Date = value;
			}
		}

		public WidgetDate ()
		{
			Build ();

			datepicker.DefaultDate = DateTime.Now;

			datepicker.DateChanged += delegate {
				OnChangeDate ();
			};
		}

		public WidgetDate (DateTime fecha)
		{
			Build ();

			datepicker.DefaultDate = fecha;

			datepicker.DateChanged += delegate {
				OnChangeDate ();
			};
		}
	}
}

