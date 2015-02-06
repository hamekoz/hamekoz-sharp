//
//  DateTimeExtensions.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <rodrigo@hamekoz.com.ar>
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
			return date.Date.AddDays (1).AddMilliseconds (-1);
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
	}
}