//
//  DecimalExtensions.cs
//
//  Author:
//       Ezequiel Taranto <ezequiel89@gmail.com>
//
//  Copyright (c) 2014 etaranto
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
	public static class NumericExtensions
	{
		/// <summary>
		/// Convert decimal to words
		/// </summary>
		/// <returns>The words.</returns>
		/// <param name="number">Number.</param>
		public static string ToWords (this decimal number)
		{
			return Numalet.ToCardinal (number);
		}

		/// <summary>
		/// Convert int to words.
		/// </summary>
		/// <returns>The words.</returns>
		/// <param name="number">Number.</param>
		public static string ToWords (this int number)
		{
			return Numalet.ToCardinal (number);
		}

		/// <summary>
		/// Convert float to words
		/// </summary>
		/// <returns>The words.</returns>
		/// <param name="number">Number.</param>
		public static string ToWords (this float number)
		{
			return Numalet.ToCardinal (number);
		}

		/// <summary>
		/// Convert double to words
		/// </summary>
		/// <returns>The words.</returns>
		/// <param name="number">Number.</param>
		public static string ToWords (this double number)
		{
			return Numalet.ToCardinal (number);
		}

		public static string ToEnglishFormat (this double number)
		{
			NumberFormatInfo nfi = new System.Globalization.CultureInfo ("en-US", false).NumberFormat;
			return number.ToString (nfi);
		}

	}
}

