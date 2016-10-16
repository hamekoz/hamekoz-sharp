//
//  Helpers.cs
//
//  Author:
//       Claudio Rodrigo Pereyra Diaz <claudiorodrigo@pereyradiaz.com.ar>
//
//  Copyright (c) 2016 Hamekoz  - www.hamekoz.com.ar
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Configuration;
using System.Globalization;
using System.Threading;

namespace Hamekoz.Core
{
	public static class Settings
	{
		public static void SetCultura()
		{
			Console.WriteLine("Seteando configuración regional:");
			Console.WriteLine(CultureInfo.CurrentCulture.NativeName);
			Thread.CurrentThread.CurrentCulture = new CultureInfo(ConfigurationManager.AppSettings["CultureInfo"], true);
			Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol = ConfigurationManager.AppSettings["CurrencySymbol"];
			Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = ConfigurationManager.AppSettings["ShortTimePattern"];
			Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = ConfigurationManager.AppSettings["ShortDatePattern"];
			Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ConfigurationManager.AppSettings["NumberDecimalSeparator"];
			Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = ConfigurationManager.AppSettings["CurrencyDecimalSeparator"];
			Console.WriteLine(CultureInfo.CurrentCulture.NativeName);
		}

		const string appConfigEmpresa = "Empresa";

		public static string Empresa {
			get {
				return ConfigurationManager.AppSettings[appConfigEmpresa];
			}
		}
	}
}
