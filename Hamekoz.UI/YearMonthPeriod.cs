//
//  DatePeriod.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2015 Hamekoz - www.hamekoz.com.ar
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
	public class YearMonthPeriod : Widget
	{
		readonly YearMonthPicker dateStart = new YearMonthPicker {
			TooltipText = Application.TranslationCatalog.GetString ("Star date"),
			ExpandHorizontal = false,
			ExpandVertical = false,
			Date = DateTime.Now
		};

		readonly YearMonthPicker dateEnd = new YearMonthPicker {
			TooltipText = Application.TranslationCatalog.GetString ("End date"),
			ExpandHorizontal = false,
			ExpandVertical = false,
			Date = DateTime.Now,
		};

		Box box;

		public YearMonthPeriod (bool horizonal = false)
		{
			if (horizonal)
				box = new HBox ();
			else
				box = new VBox ();

			//HACK para resolver problema con fecha maxima fuera de periodo por ticks
			MaximumDate = DateTime.MaxValue.AddMilliseconds (-1d);
				
			box.PackStart (new Label (Application.TranslationCatalog.GetString ("Period")));
			box.PackStart (dateStart, horizonal, horizonal);
			box.PackStart (dateEnd, horizonal, horizonal);
			Content = box;

			dateStart.ValueChanged += Period_ValueChanged;
			dateEnd.ValueChanged += Period_ValueChanged;
		}

		void Period_ValueChanged (object sender, EventArgs e)
		{
			if (dateStart.Value > dateEnd.Value) {
				box.BackgroundColor = Xwt.Drawing.Colors.Red;	
				box.TooltipText = string.Format (Application.TranslationCatalog.GetString ("Invalid period, the start date must be less than or equal to the final, and the period must be between {0} and {1}"), MinimumDate.ToShortDateString (), MaximumDate.ToShortDateString ());

			} else {
				
				box.BackgroundColor = Xwt.Drawing.Colors.Transparent;
				box.TooltipText = string.Format (Application.TranslationCatalog.GetString ("The period must be between {0} and {1}"), MinimumDate.ToShortDateString (), MaximumDate.ToShortDateString ());
			}

			OnValueChanged (e);
		}

		public DateTime DateStart {
			get {
				return dateStart.Date;
			}
			set {
				dateStart.Date = value;
			}
		}

		public DateTime DateEnd {
			get {
				return dateEnd.Date;
			}
			set {
				dateEnd.Date = value;
			}
		}

		public DateTime MinimumDate {
			get {
				return dateStart.MinimumDate;
			}
			set {
				dateStart.MinimumDate = value;
				dateEnd.MinimumDate = value;
				box.TooltipText = string.Format (Application.TranslationCatalog.GetString ("The period must be between {0:Y} and {1:Y}"), dateStart.MinimumDate, MaximumDate);
			}
		}

		public DateTime MaximumDate {
			get {
				return dateEnd.MaximumDate;
			}
			set {
				dateEnd.MaximumDate = value;
				dateStart.MaximumDate = value;
				box.TooltipText = string.Format (Application.TranslationCatalog.GetString ("The period must be between {0:Y} and {1:Y}"), MinimumDate, dateEnd.MaximumDate);
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

