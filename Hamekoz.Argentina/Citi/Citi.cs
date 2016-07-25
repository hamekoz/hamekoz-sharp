//
//  Citi.cs
//
//  Author:
//       Mariano Ripa <ripamariano@gmail.com>
//
//  Copyright (c) 2016 Hamekoz
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
using System.Collections.Generic;
using System.IO;

namespace Hamekoz.Argentina.Citi
{
	public static class Citi
	{
		public static void Exportar (List<RegistroImportacionCitiVentas> registros, string archivo)
		{
			StreamWriter sw = File.CreateText (archivo);
			sw.NewLine = "\r\n";
			foreach (var registro in registros) {
				sw.WriteLine (registro.ToFixedString ());
			}
			sw.Close ();
		}

		public static void ExportarAlicuotas (List<RegistroImportacionCitiVentas> registros, string archivo)
		{
			StreamWriter sw = File.CreateText (archivo);
			sw.NewLine = "\r\n";
			foreach (var registro in registros) {
				sw.WriteLine (registro.ToFixedStringAlicuotas ());
			}
			sw.Close ();
		}

		public static void Exportar (List<RegistroImportacionCitiCompras> registros, string archivo)
		{

			StreamWriter sw = File.CreateText (archivo);
			sw.NewLine = "\r\n";
			foreach (var registro in registros) {
				sw.WriteLine (registro.ToFixedString ());
			}
			sw.Close ();
		}

		public static void ExportarAlicuotas (List<RegistroImportacionCitiCompras> registros, string archivo)
		{
			StreamWriter sw = File.CreateText (archivo);
			sw.NewLine = "\r\n";
			foreach (var registro in registros) {

				if (decimal.Parse (registro.Neto) == 0) {//no es 21%

					if (decimal.Parse (registro.NetoDif1) != 0) {//es 10.5%
						registro.Neto = registro.NetoDif1;
						registro.IVA = registro.IVADif1;
						registro.IVAAlicuota = "0004";
					} else {
						registro.Neto = registro.NetoDif2; //es 27%
						registro.IVA = registro.IVADif2;
						registro.IVAAlicuota = "0006";
					}
				}

				sw.WriteLine (registro.ToFixedStringAlicuotas ());
			}
			sw.Close ();
		}

		
	}
}



