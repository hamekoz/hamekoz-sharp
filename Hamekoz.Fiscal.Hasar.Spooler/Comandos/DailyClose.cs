//
//  DailyClose.cs
//
//  Author:
//       Ezequiel Taranto <ezequiel89@gmail.com>
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

namespace Hamekoz.Fiscal.Hasar.Spooler
{
	public class DailyClose : Comando
	{
		const string cmd = "9";

		public int NroDeInforme { get; set; }

		public int CantidadDocFiscalesCancelados { get; set; }

		public int CantidadDocNoFiscalesHomologados { get; set; }

		public int CantidadDocNoFiscales { get; set; }

		public int CantidadDocFiscalesEmitidos { get; set; }

		public string NroUltimoTicketEmitido { get; set; }

		public string NroUltimoTicketFacturaA { get; set; }

		public float MontoVendidoDocFiscales { get; set; }

		public float MontoIVA { get; set; }

		public float MontoImpuestosInternos { get; set; }

		public float MontoPercepciones { get; set; }

		public string NroUltimoTicketNotaCreditoBC { get; set; }

		public string NroUltimoTicketNotaCreditoA { get; set; }

		public float MontoCreditoEnTicketsNC { get; set; }

		public float MontoIVANC { get; set; }

		public float MontoImpuestosInternosNC { get; set; }

		public float MontoPersepcionesTicketNC { get; set; }

		readonly bool tipo;

		public string Comando ()
		{
			return string.Format ("{0}{1}{2}", cmd, separador, tipo ? "Z" : "X");
		}

		/// <summary>
		/// Imprime Cierre de Jornada Fiscal Z = true X = false
		/// </summary>
		/// <param name = "tipo">If set to <c>true</c> z.</param>
		public DailyClose (bool tipo)
		{
			this.tipo = tipo;
		}
	}
}

