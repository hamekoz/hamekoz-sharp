//
//  DatePeriod.cs
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
using Mono.Unix;
using Xwt;

namespace Hamekoz.UI
{
	public class DatePeriod : Widget
	{
		readonly DatePicker dateStart = new DatePicker {
			Style = DatePickerStyle.Date,
			TooltipText = Catalog.GetString ("Star date"),
			ExpandHorizontal = false,
			ExpandVertical = false,
			DateTime = DateTime.Now.Date
		};

		readonly DatePicker dateEnd = new DatePicker {
			Style = DatePickerStyle.Date,
			TooltipText = Catalog.GetString ("End date"),
			ExpandHorizontal = false,
			ExpandVertical = false,
			DateTime = DateTime.Now.Date.AddDays (1).AddMilliseconds (-1),
		};

		public DatePeriod (bool horizonal = false)
		{
			Box box;
			if (horizonal)
				box = new HBox ();
			else
				box = new VBox ();

			box.PackStart (new Label (Catalog.GetString ("Period")));
			box.PackStart (dateStart, horizonal, horizonal);
			box.PackStart (dateEnd, horizonal, horizonal);
			Content = box;

			dateStart.ValueChanged += Period_ValueChanged;
			dateEnd.ValueChanged += Period_ValueChanged;
		}

		void Period_ValueChanged (object sender, EventArgs e)
		{
			var datepicker = sender as DatePicker;
			//HACK prevent that year change minor to 1900 change other year part
			if (datepicker.DateTime.Year > 1900) {
				if (dateStart.DateTime > dateEnd.DateTime) {
					if (sender == dateStart) {
						dateEnd.DateTime = dateStart.DateTime;
					}
					if (sender == dateEnd) {
						dateStart.DateTime = dateEnd.DateTime;
					}
				}
				OnValueChanged (e);
			}
		}

		bool withCalendarButton = true;

		public bool WithCalendarButton {
			get {
				return withCalendarButton;
			}
			set {
				withCalendarButton = value;
				dateStart.WithCalendarButton = withCalendarButton;
				dateEnd.WithCalendarButton = withCalendarButton;
			}
		}

		public DateTime DateStart {
			get {
				return dateStart.DateTime;
			}
			set {
				dateStart.DateTime = value;
			}
		}

		public DateTime DateEnd {
			get {
				return dateEnd.DateTime;
			}
			set {
				dateEnd.DateTime = value;
			}
		}

		public DateTime MinimumDate {
			get {
				return dateStart.MinimumDateTime;
			}
			set {
				dateStart.MinimumDateTime = value;
				dateEnd.MinimumDateTime = value;
			}
		}

		public DateTime MaximumDate {
			get {
				return dateEnd.MaximumDateTime;
			}
			set {
				dateEnd.MaximumDateTime = value;
				dateStart.MaximumDateTime = value;
			}
		}

		public event EventHandler ValueChanged;

		protected virtual void OnValueChanged (EventArgs e)
		{
			var handler = ValueChanged;
			if (handler != null)
				handler (this, e);
		}
	}
}

