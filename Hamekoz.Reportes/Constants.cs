//
//  Constants.cs
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
using System.Reflection;

namespace Hamekoz.Reportes
{
	static class Constants
	{
		internal const string HamekozLogo = "http://www.hamekoz.com.ar/favicon.png";

		internal static string PoweredBy {
			get {
				return string.Format ("{0} v{1}",
					Assembly.GetExecutingAssembly ().GetName ().Name, 
					Assembly.GetExecutingAssembly ().GetName ().Version.ToString (2)
				);
			}
		}

		internal static string GeneratedBy {
			get {
				Assembly generator = Assembly.GetEntryAssembly ();
				AssemblyTitleAttribute title = ((AssemblyTitleAttribute)Attribute.GetCustomAttribute (generator, typeof(AssemblyTitleAttribute), false));

				return string.Format ("{0} v{1}",
					title.Title.Length > 0 ? title.Title : generator.GetName ().Name, 
					generator.GetName ().Version.ToString (2)
				);
			}
		}
	}
}

