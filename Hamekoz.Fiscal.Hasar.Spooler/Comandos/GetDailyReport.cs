//
//  GetDailyReport.cs
//
//  Author:
//       Ezequiel Taranto <ezequiel89@gmail.com>
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

namespace Hamekoz.Fiscal.Hasar.Spooler
{
	public class GetDailyReport : Comando
	{
		const string cmd = "<";

		public string FechaCierre { get; set; }

		public int NroZ { get; set; }

		public int NroUltimoTicket { get; set; }

		public int NroUltimoFacturaA { get; set; }

		public float MontoVendido { get; set; }

		public float IVA { get; set; }

		public float ImpuestosInternos { get; set; }

		public float MontoPercepciones { get; set; }

		public int NroUltimoNC { get; set; }

		public int NroUltimoNCA { get; set; }

		public float MontoCreditoNC { get; set; }

		public float MontoIVANC { get; set; }

		public float MontoImpuestosInternosNC { get; set; }

		public float MontoPercepcionesNC { get; set; }

		readonly string nroZOFecha;
		readonly string calificador;

		public string Comando ()
		{
			return string.Format ("{0}{1}{2}{1}{3}", cmd, separador, nroZOFecha, calificador);
		}

		public GetDailyReport (string nroZOFecha, string calificador)
		{
			this.nroZOFecha = nroZOFecha;
			this.calificador = calificador;
		}
	}
}

