﻿//
//  NumericExtensions.cs
//
//  Author:
//       Ezequiel Taranto <ezequiel89@gmail.com>
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
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

using Hamekoz.Core;

namespace Hamekoz.Extensions
{
    public static class NumericExtensions
    {
        /// <summary>
        /// Convert decimal to words
        /// </summary>
        /// <returns>The words.</returns>
        /// <param name="number">Number.</param>
        public static string ToWords(this decimal number)
        {
            return Numalet.ToCardinal(number);
        }

        /// <summary>
        /// Convert int to words.
        /// </summary>
        /// <returns>The words.</returns>
        /// <param name="number">Number.</param>
        public static string ToWords(this int number)
        {
            return Numalet.ToCardinal(number);
        }

        /// <summary>
        /// Convert float to words
        /// </summary>
        /// <returns>The words.</returns>
        /// <param name="number">Number.</param>
        public static string ToWords(this float number)
        {
            return Numalet.ToCardinal(number);
        }

        /// <summary>
        /// Convert double to words
        /// </summary>
        /// <returns>The words.</returns>
        /// <param name="number">Number.</param>
        public static string ToWords(this double number)
        {
            return Numalet.ToCardinal(number);
        }

        public static string ToEnglishFormat(this double number, int digits = 2)
        {
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            return Math.Round(number, digits, MidpointRounding.AwayFromZero).ToString(nfi);
        }

        public static string ToEnglishFormat(this decimal number, int digits = 2)
        {
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            return Math.Round(number, digits, MidpointRounding.AwayFromZero).ToString(nfi);
        }
    }
}