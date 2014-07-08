//
//  DialogCalendar.cs
//
//  Author:
//       Emiliano Gabriel Canedo <emilianocanedo@gmail.com>
//
//  Copyright (c) 2014 Emiliano Canedo
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

namespace DatePicker
{
	public partial class DialogCalendar : Gtk.Dialog
	{
		private DateTime defaultDate;

		public DateTime DefaultDate {
			get {
				return defaultDate;
			}
			set {
				defaultDate = value;
			}
		}

		private bool buttonsVisible;

		public bool ButtonsVisible {
			get {
				return buttonsVisible;
			}
			set {
				buttonsVisible = value;
				((HButtonBox)buttonToday.Parent).Visible = buttonsVisible;
			}
		}

		public DialogCalendar ()
		{
			this.Build ();

			calendar.DaySelectedDoubleClick += DaySelectedDoubleClick;
			this.FocusOutEvent += DialogFocusOutEvent;
			buttonToday.Clicked += buttonTodayClicked;
			buttonDefault.Clicked += buttonDefaultClicked;
			ButtonsVisible = false;
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

		protected virtual void DaySelectedDoubleClick (object sender, System.EventArgs e)
		{
			Hide ();
		}

		protected virtual void DialogFocusOutEvent (object o, Gtk.FocusOutEventArgs args)
		{
			Hide ();
		}

		protected virtual void buttonTodayClicked (object sender, EventArgs e)
		{
			calendar.Date = DateTime.Now;
		}


		protected virtual void buttonDefaultClicked (object sender, EventArgs e)
		{
			calendar.Date = defaultDate;
		}
	}
}


