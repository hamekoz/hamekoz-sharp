//
//  YearMonthPicker.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2017 Hamekoz - www.hamekoz.com.ar
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
using System.Linq;
using Xwt;

namespace Hamekoz.UI
{
	public class YearMonthPicker : HBox
	{

		public int Year {
			get {
				return (int)year.Value;
			}
			set {
				year.Value = value;
			}
		}

		public int Month {
			get {
				return month.SelectedIndex + 1;
			}
			set {
				month.SelectedIndex = value - 1;
			}
		}

		public string MonthName {
			get {
				return month.SelectedText;
			}
			set {
				month.SelectedText = value;
			}
		}

		public int MaximumYear {
			get {
				return (int)year.MaximumValue;
			}
			set {
				year.MaximumValue = value;
				maximumDate = new DateTime (value, 12, 31);
			}
		}

		public int MinimumYear {
			get {
				return (int)year.MinimumValue;
			}
			set {
				year.MinimumValue = value;
				minimumDate = new DateTime (value, 1, 1);
			}
		}

		DateTime maximumDate;

		public DateTime MaximumDate {
			get {
				return maximumDate;
			}
			set {
				maximumDate = value;
				MaximumYear = minimumDate.Year;
			}
		}

		DateTime minimumDate;

		public DateTime MinimumDate {
			get {
				return minimumDate;
			}
			set {
				minimumDate = value;
				MinimumYear = minimumDate.Year;
			}
		}

		/// <summary>
		/// Value expresed as 8 digit integer that represent yyyyMM. (yyyy * 100 + MM)
		/// </summary>
		public int Value {
			get {
				return Year * 100 + Month;
			}
		}

		public DateTime Date {
			get {
				return new DateTime (Year, Month, 1);
			}
			set {
				Year = value.Year;
				Month = value.Month;
			}
		}

		readonly ComboBox month = new ComboBox ();
		readonly SpinButton year = new SpinButton {
			ClimbRate = 5,
			Digits = 0,
			IncrementValue = 1,
			MaximumValue = 9999,
			MinimumValue = 1900,
			Value = DateTime.Now.Year
		};

		public YearMonthPicker ()
		{
			foreach (var item in CultureInfo.CurrentUICulture.DateTimeFormat.MonthNames.Take(12))
				month.Items.Add (item);
			Month = DateTime.Now.Month;

			PackStart (year);
			PackStart (month);

			year.ValueChanged += (sender, e) => OnYearChanged (e);
			month.SelectionChanged += (sender, e) => OnMonthChanged (e);
		}

		public event EventHandler ValueChanged;

		protected virtual void OnValueChanged (EventArgs e)
		{
			var handler = ValueChanged;
			if (handler != null)
				handler (this, e);
		}

		public event EventHandler YearChanged;

		protected virtual void OnYearChanged (EventArgs e)
		{
			var handler = YearChanged;
			if (handler != null)
				handler (this, e);
			OnValueChanged (e);
		}

		public event EventHandler MonthChanged;

		protected virtual void OnMonthChanged (EventArgs e)
		{
			var handler = MonthChanged;
			if (handler != null)
				handler (this, e);
			OnValueChanged (e);
		}
	}
}

