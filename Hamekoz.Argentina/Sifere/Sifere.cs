//
//  Sifere.cs
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


namespace Hamekoz.Argentina.Sifere
{
	public static class Sifere
	{
		public static int Exportar (List<RegistroImportacionRetencionPercepcion> registros, string archivo)
		{
			StreamWriter sw = File.CreateText (archivo);
			var c = 0;
			foreach (var registro in registros) {

				if (string.IsNullOrEmpty (registro.CUIT))
					c++;

				if (registro.esPercepcion)
					sw.WriteLine (registro.ToFixedStringPercepcion ());
				else
					sw.WriteLine (registro.ToFixedStringRetencion ());
			}
			sw.Close ();
			return c;
		}

		/*public static void Exportar (List<RegistroImportacionNotaDeCredito> registros, string archivo)
		{

			StreamWriter sw = File.CreateText (archivo);
			foreach (var registro in registros) {
				sw.WriteLine (registro.ToFixedString ());
			}
			sw.Close ();
		}*/


	}
}

