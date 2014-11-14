//
//  StringExtensions.cs
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

namespace Hamekoz.Extensions
{
	public static class StringExtensions
	{
		/// <summary>
		/// String to the camel case convension.
		/// </summary>
		/// <returns>String camelized.</returns>
		/// <param name="the_string">The string.</param>
		public static string ToCamelCase(this string the_string)
		{
			// If there are 0 or 1 characters, just return the string.
			if (the_string == null || the_string.Length < 2) return the_string;

			// Split the string into words.
			string[] words = the_string.Split(
				new char[] { },
				StringSplitOptions.RemoveEmptyEntries);

			// Combine the words.
			string result = words[0].ToLower();
			for (int i = 1; i < words.Length; i++)
			{
				result +=
					words[i].Substring(0, 1).ToUpper() +
					words[i].Substring(1);
			}

			return result;
		}
	}
}