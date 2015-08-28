//
//  DialogCalendar.cs
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
using Gtk;

namespace Hamekoz.UI.Gtk
{
	public sealed partial class DialogCalendar : Dialog
	{
		public DateTime DefaultDate {
			get;
			set;
		}

		bool buttonsVisible;

		public bool ButtonsVisible {
			get {
				return buttonsVisible;
			}
			set {
				buttonsVisible = value;
				buttonToday.Parent.Visible = buttonsVisible;
			}
		}

		public DialogCalendar ()
		{
			Build ();

			calendar.DaySelectedDoubleClick += DaySelectedDoubleClick;
			FocusOutEvent += DialogFocusOutEvent;
			buttonToday.Clicked += buttonTodayClicked;
			buttonDefault.Clicked += buttonDefaultClicked;
		}

		public DateTime Date {
			get { return calendar.Date; }
			set { calendar.Date = value; }
		}

		protected override void OnShown ()
		{
			base.OnShown ();
			GrabFocus ();
		}

		protected void DaySelectedDoubleClick (object sender, EventArgs e)
		{
			Hide ();
		}

		protected void DialogFocusOutEvent (object o, FocusOutEventArgs args)
		{
			Hide ();
		}

		protected void buttonTodayClicked (object sender, EventArgs e)
		{
			calendar.Date = DateTime.Now;
		}


		protected void buttonDefaultClicked (object sender, EventArgs e)
		{
			calendar.Date = DefaultDate;
		}
	}
}


