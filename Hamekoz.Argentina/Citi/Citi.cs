//
//  Citi.cs
//
//  Author:
//       Mariano Ripa <ripamariano@gmail.com>
//
//  Copyright (c) 2016 Hamekoz - www.hamekoz.com.ar
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

				if (registro.Alicuotas > 1) {
					if (decimal.Parse (registro.IVA) != 0) {//es 21%
						registro.Neto = registro.Neto;
						registro.IVA = registro.IVA;
						registro.IVAAlicuota = "0005";
						sw.WriteLine (registro.ToFixedStringAlicuotas ());
					}
					if (decimal.Parse (registro.IVADif1) != 0) {//es 10.5%
						registro.Neto = registro.NetoDif1;
						registro.IVA = registro.IVADif1;
						registro.IVAAlicuota = "0004";
						sw.WriteLine (registro.ToFixedStringAlicuotas ());
					}
					if (decimal.Parse (registro.IVADif2) != 0) {//es 27%
						registro.Neto = registro.NetoDif2; 
						registro.IVA = registro.IVADif2;
						registro.IVAAlicuota = "0006";
						sw.WriteLine (registro.ToFixedStringAlicuotas ());
					}
				} else if (registro.Alicuotas == 1) {
					if (decimal.Parse (registro.IVA) != 0) {//es 21%
						registro.Neto = registro.Neto;
						registro.IVA = registro.IVA;
						registro.IVAAlicuota = "0005";
						sw.WriteLine (registro.ToFixedStringAlicuotas ());
					}
					if (decimal.Parse (registro.IVADif1) != 0) {//es 10.5%
						registro.Neto = registro.NetoDif1;
						registro.IVA = registro.IVADif1;
						registro.IVAAlicuota = "0004";
						sw.WriteLine (registro.ToFixedStringAlicuotas ());
					} 
					if (decimal.Parse (registro.IVADif2) != 0) {//es 10.5%
						registro.Neto = registro.NetoDif2; //es 27%
						registro.IVA = registro.IVADif2;
						registro.IVAAlicuota = "0006";
						sw.WriteLine (registro.ToFixedStringAlicuotas ());
					}
					if (decimal.Parse (registro.IVA) == 0 && decimal.Parse (registro.IVADif1) == 0 && decimal.Parse (registro.IVADif2) == 0) {						
						registro.Neto = "000000000000000";
						registro.IVA = "000000000000000";
						registro.IVAAlicuota = "0003";//0%
						sw.WriteLine (registro.ToFixedStringAlicuotas ());
					}
				} else { 					
					if (!EsComprobanteBoC (registro.TipoComprobante)) {						
						//NoGravado
						registro.IVAAlicuota = "0003";//0%
						sw.WriteLine (registro.ToFixedStringAlicuotas ());
					}
				}
			}
		
			sw.Close ();
		}

		static bool EsComprobanteBoC (string codigo)
		{
			if (codigo == "006" || codigo == "007" || codigo == "008" || codigo == "009" || codigo == "011" || codigo == "012" || codigo == "013" || codigo == "015")
				return true;
			else
				return false;
		}
		
	}
}



