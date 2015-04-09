//
//  DatePicker.cs
//
//  Author:
//		Krzysztof Marecki <marecki.krzysztof@gmail.com>
//		Emiliano Canedo <emilianocanedo@gmail.com>
//
//	Copyright (c) 2010 Krzysztof Marecki
//	Copyright (c) 2014 Emiliano Canedo
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
using System.Globalization;
using Gtk;

namespace Hamekoz.UI.Gtk
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class DatePicker : Bin, Hamekoz.Interfaces.IFocusableWidget
	{
		DialogCalendar calendarDialog = new DialogCalendar ();

		bool FirstDateChange = false;

		public string CustomFormat { get; set; }

		public bool ButtonsVisible {
			get {
				return calendarDialog.ButtonsVisible;
			}
			set {
				calendarDialog.ButtonsVisible = value;
			}
		}

		public DateTime DefaultDate {
			get {
				return calendarDialog.DefaultDate;
			}
			set {
				if (value != new global::System.DateTime (0)) {
					calendarDialog.DefaultDate = value;
					Date = calendarDialog.DefaultDate;
				}
			}
		}

		public DateTime Date {
			get {
				DateTime date;
				if (DateTime.TryParse (entry.Text, out date))
					return date;
				else
					return DateTime.Now;
			}
			set {
				if (value != new global::System.DateTime (0)) {
					string format = GetDateFormat ();
					entry.Text = value.ToString (format);
				}
			}
		}

		public DatePicker ()
		{
			this.Build ();

			calendarDialog.Visible = false;

			this.WindowStateEvent += HandleWindowStateEvent;
			calendarDialog.Hidden += HandleCalendarDialogHidden;

			button.Clicked += HandleButtonClicked;
			entry.Changed += HandleEntryChanged;
			entry.FocusOutEvent += HandleEntryFocusOutEvent;
		}

		void HandleWindowStateEvent (object o, WindowStateEventArgs args)
		{
			OnFocusOut ();
		}

		public override void Dispose ()
		{
			base.Dispose ();

			if (calendarDialog != null) {
				calendarDialog.Destroy ();
			}
		}

		public void ClearDate ()
		{
			FirstDateChange = false;
			entry.Text = "";
		}

		string GetDateFormat ()
		{
			return string.IsNullOrEmpty (CustomFormat) ?
				CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern :
				CustomFormat;
		}

		void HandleButtonClicked (object sender, EventArgs e)
		{
			if (!FirstDateChange) {
				calendarDialog.Date = DateTime.Now;
			} else {
				calendarDialog.Date = Date;
			}

			int x, y;
			this.GdkWindow.GetOrigin (out x, out y);
			x += this.Allocation.Left;
			y += this.Allocation.Top + this.Allocation.Height;
			calendarDialog.Move (x, y);
			calendarDialog.Modal = true;
			calendarDialog.Show ();
		}

		void HandleCalendarDialogHidden (object sender, EventArgs e)
		{
			if (!FirstDateChange) {
				OnFirstDateChange ();
				FirstDateChange = true;
			} else {
				OnDateChanged ();
			}
			Date = calendarDialog.Date;
			entry.GrabFocus ();
			entry.Position = entry.Text.Length;
		}

		void HandleEntryChanged (object sender, EventArgs e)
		{
			OnDateChanged ();

		}

		[GLib.ConnectBefore]
		void HandleEntryFocusOutEvent (object sender, FocusOutEventArgs e)
		{
			OnFocusOut ();
		}

		public event EventHandler DateChanged;

		protected virtual void OnDateChanged ()
		{
			var handler = DateChanged;
			if (handler != null)
				handler (this, EventArgs.Empty);
		}

		public event EventHandler FocusOut;

		protected virtual void OnFocusOut ()
		{
			var handler = FocusOut;
			if (handler != null)
				handler (this, EventArgs.Empty);
		}

		public event Hamekoz.UI.Gtk.ChangeEventHandler FirstDateChangeEvent;

		protected virtual void OnFirstDateChange ()
		{
			var handler = FirstDateChangeEvent;
			if (handler != null)
				handler ();
		}
	}
}