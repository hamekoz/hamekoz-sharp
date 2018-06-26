//
//  DateTimeExtensions.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <rodrigo@hamekoz.com.ar>
//
//  Copyright (c) 2014 Hamekoz - www.hamekoz.com.ar
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

namespace Hamekoz.Extensions
{
	public static class DateTimeExtensions
	{
		/// <summary>
		/// Weeks the of year with Sunday as first day of week.
		/// </summary>
		/// <returns>The number of week.</returns>
		/// <param name="date">Date.</param>
		public static int WeekOfYear (this DateTime date)
		{
			Calendar calendario = DateTimeFormatInfo.CurrentInfo.Calendar;
			return calendario.GetWeekOfYear (date, CalendarWeekRule.FirstFullWeek, DayOfWeek.Sunday);
		}

		/// <summary>
		/// Weeks the of year.
		/// </summary>
		/// <returns>The number of week.</returns>
		/// <param name="date">Date.</param>
		/// <param name="firstDay">First day of week.</param>
		public static int WeekOfYear (this DateTime date, DayOfWeek firstDay)
		{
			Calendar calendario = DateTimeFormatInfo.CurrentInfo.Calendar;
			return calendario.GetWeekOfYear (date, CalendarWeekRule.FirstFullWeek, firstDay);
		}

		/// <summary>
		/// Consider all the day .
		/// </summary>
		/// <returns>The day untill 23:59:59 hs.</returns>
		/// <param name="date">Date.</param>
		public static DateTime AllDay (this DateTime date)
		{
			//HACK deberia utilizar AddMilliseconds pero Windows lo redondea. Por eso utilizo AddSeconds
			return date.Date.AddDays (1).AddSeconds (-1);
		}

		/// <summary>
		/// Firsts the day of month from date.
		/// </summary>
		/// <returns>The first day of month.</returns>
		/// <param name="date">Date.</param>
		public static DateTime FirstDayOfMonth (this DateTime date)
		{
			return new DateTime (date.Year, date.Month, 1);
		}

		/// <summary>
		/// The last day of month from date.
		/// </summary>
		/// <returns>The last day of month.</returns>
		/// <param name="date">Date.</param>
		public static DateTime LastDayOfMonth (this DateTime date)
		{
			var value = date.FirstDayOfMonth ();
			value = value.AddMonths (1);
			value = value.AddDays (-1);
			return value;
		}

		public static DateTime FirstDateOfWeekISO8601 (int year, int weekOfYear)
		{
			var jan1 = new DateTime (year, 1, 1);
			int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

			DateTime firstThursday = jan1.AddDays (daysOffset);
			var cal = CultureInfo.CurrentCulture.Calendar;
			int firstWeek = cal.GetWeekOfYear (firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

			var weekNum = weekOfYear;
			if (firstWeek <= 1)
				weekNum -= 1;
			var result = firstThursday.AddDays (weekNum * 7);
			return result.AddDays (-3);
		}
	}
}