//
//  FiscalHasar.cs
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
using System.Configuration;
using System.Reflection;

namespace Hamekoz.Fiscal
{
	public static class FiscalHasar
	{
		public enum Backend
		{
			Spooler,
			Ocx,
			IFH2G
		}

		public static IFiscalHasar GetControladorFiscal ()
		{
			var backendSettings = ConfigurationManager.AppSettings ["ImpresoraFiscal.Backend"];

			Backend backend;
			if (!Enum.TryParse (backendSettings, out backend))
				switch (Environment.OSVersion.Platform) {
				case PlatformID.Unix:
				case PlatformID.MacOSX:
					backend = Backend.Spooler;
					break;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					backend = Backend.Ocx;
					break;
				default:
					backend = Backend.IFH2G;
					break;
				}
			return GetControladorFiscal (backend);
		}

		public static IFiscalHasar GetControladorFiscal (Backend backend)
		{
			string assemblyName;
			switch (backend) {
			case Backend.IFH2G:
				assemblyName = "Hamekoz.Fiscal.Hasar.IFH2G";
				break;
			case Backend.Ocx:
				assemblyName = "Hamekoz.Fiscal.Hasar.OCX";
				break;
			case Backend.Spooler:
				assemblyName = "Hamekoz.Fiscal.Hasar.Spooler";
				break;
			default:
				throw new ArgumentException ("No se definio el backend para la comunicacion con el controlador fiscal");
			}

			Assembly assembly = Assembly.Load (assemblyName);
			string nombreControlador = string.Format ("{0}.ControladorFiscal", assemblyName);
			var controlador = (IFiscalHasar)assembly.CreateInstance (nombreControlador);
			return controlador;
		}
	}
}
