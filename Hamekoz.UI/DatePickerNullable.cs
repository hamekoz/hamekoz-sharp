//
//  DatePickerNullable.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2016 Hamekoz - www.hamekoz.com.ar
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
	//TODO implementar las propiedades normales de un DatePicker
	public class DatePickerNullable : HBox
	{
		public DateTime? DateTime {
			get {
				return check.Active ? datepicker.DateTime : (DateTime?)null;
			}
			set {
				check.Active = value.HasValue;
				if (check.Active) {
					datepicker.Sensitive = value.HasValue;
					datepicker.DateTime = value.Value;
				}
			}
		}

		public DateTime MinimumDateTime {
			get {
				return datepicker.MinimumDateTime;
			}
			set {
				datepicker.MinimumDateTime = value;
			}
		}

		public DateTime MaximumDateTime {
			get {
				return datepicker.MaximumDateTime;
			}
			set {
				datepicker.MaximumDateTime = value;
			}
		}

		public DatePickerStyle Style {
			get {
				return datepicker.Style;
			}
			set {
				datepicker.Style = value;
			}
		}
		//TODO descomentar cuando e integre en la version nuget
		//		public bool ReadOnly {
		//			get {
		//				return datepicker.ReadOnly;
		//			}
		//			set {
		//				datepicker.ReadOnly = value;
		//			}
		//		}
		//
		//		public bool WithCalendarButton {
		//			get {
		//				return datepicker.WithCalendarButton;
		//			}
		//			set {
		//				datepicker.WithCalendarButton = value;
		//			}
		//		}

		CheckBox check = new CheckBox ();
		readonly DatePicker datepicker = new DatePicker {
			Sensitive = false,
		};

		
		public DatePickerNullable ()
		{
			PackStart (check);
			PackStart (datepicker, true, true);

			check.Toggled += Check_Toggled;
			datepicker.ValueChanged += (sender, e) => OnValueChanged (e);
		}

		void Check_Toggled (object sender, EventArgs e)
		{
			datepicker.Sensitive = ((CheckBox)sender).Active;
			OnValueChanged (e);
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

