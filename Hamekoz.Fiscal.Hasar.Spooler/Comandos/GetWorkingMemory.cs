//
//  GetWorkingMemory.cs
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
	public class GetWorkingMemory
	{
		const string cmd = "g";

		public int CantidadTicketsCancelados { get; set; }

		public int CantidadDocNoFiscalesEmitidos { get; set; }

		public int CantidadDocFiscalesEmitidos { get; set; }

		public int NroUltimoTicketEmitido { get; set; }

		public int NroUltimoDocFiscalAEmitido { get; set; }

		public float MontoVentasEnDocFiscales { get; set; }

		public float MontoIVAEnDocFicales { get; set; }

		public float MontoImpuestosInternosEnDocFiscales { get; set; }

		public float MontoPercepcionesEnDocFiscales { get; set; }

		public int NroUltimoTicketNCEmitido { get; set; }

		public int NroUltimoTIcketNCAEmitido  { get; set; }

		public float MontoCreditoNC { get; set; }

		public float MontoIVANC { get; set; }

		public float MontoImpuestosInternosNC { get; set; }

		public float MontoPercepcionesNC { get; set; }

		public int CantidadNCCanceladas { get; set; }

		public string Comando ()
		{
			return cmd;
		}
	}
}

